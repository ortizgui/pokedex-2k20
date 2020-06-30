using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using Pokedex.Domain.Entities;
using Pokedex.Domain.ExternalServices;
using Pokedex.Infrastructure.ExternalServices.Pokemon;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging;
using Jaeger.Samplers;
using OpenTracing;
using System.Reflection;
using Jaeger;
using OpenTracing.Util;
using Amazon.DynamoDBv2;
using Pokedex.Infrastructure.Repositories;
using Pokedex.Domain.Repositories;

namespace Pokedex.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load("Pokedex.Domain");

            services.AddControllers();

            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc(name:"v1", new OpenApiInfo { Title = "Pokédex", Version = "v1" });
                });

            services.AddMediatR(assembly);
            services.AddScoped<IPokemonExternalService, PokemonExternalService>();
            services.AddScoped<IPokemonRepository, PokemonRepository>();

            var pokeApiSection = Configuration.GetSection("PokeApi");
            services.Configure<AppSettings>(pokeApiSection);

            var dynamoDbConfig = Configuration.GetSection("DynamoDb");
            var runLocalDynamoDb = dynamoDbConfig.GetValue<bool>("LocalMode");

            if (runLocalDynamoDb)
            {
                services.AddSingleton<IAmazonDynamoDB>(sp =>
                {
                    var clientConfig = new AmazonDynamoDBConfig { ServiceURL = dynamoDbConfig.GetValue<string>("LocalServiceUrl") };
                    return new AmazonDynamoDBClient(clientConfig);
                });
            }
            else
            {
                services.AddAWSService<IAmazonDynamoDB>();
            }

            services.AddSingleton<ITracer>(serviceProvider =>  
            {  
                string serviceName = Assembly.GetEntryAssembly().GetName().Name;  
            
                ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();  
            
                ISampler sampler = new ConstSampler(sample: true);  
            
                ITracer tracer = new Tracer.Builder(serviceName)  
                    .WithLoggerFactory(loggerFactory)  
                    .WithSampler(sampler)  
                    .Build();  
            
                GlobalTracer.Register(tracer);  
            
                return tracer;  
            });  
            
            services.AddOpenTracing();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url:"/swagger/v1/swagger.json", name:"Pokédex V1");
                c.RoutePrefix = string.Empty;
            });

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
