using System;
using System.Reflection;
using Amazon.DynamoDBv2;
using Jaeger;
using Jaeger.Samplers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OpenTracing;
using OpenTracing.Util;
using Pokedex.Domain.Entities;
using Pokedex.Domain.ExternalServices;
using Pokedex.Domain.Repositories;
using Pokedex.Infrastructure.ExternalServices.Pokemon;
using Pokedex.Infrastructure.Repositories;

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

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Pokédex", Version = "v1"}); });

            services.AddMediatR(assembly);
            services.AddScoped<IPokemonExternalService, PokemonExternalService>();
            services.AddScoped<IPokemonRepository, PokemonRepository>();

            var pokeApiSection = Configuration.GetSection("PokeApi");
            services.Configure<AppSettings>(pokeApiSection);

            var dynamoDbConfig = Configuration.GetSection("DynamoDb");
            var runLocalDynamoDb = dynamoDbConfig.GetValue<bool>("LocalMode");

            if (runLocalDynamoDb)
                services.AddSingleton<IAmazonDynamoDB>(sp =>
                {
                    var clientConfig = new AmazonDynamoDBConfig
                        {ServiceURL = dynamoDbConfig.GetValue<string>("LocalServiceUrl")};
                    return new AmazonDynamoDBClient(clientConfig);
                });
            else
                services.AddAWSService<IAmazonDynamoDB>();

            services.AddSingleton(serviceProvider =>
            {
                var serviceName = Assembly.GetEntryAssembly().GetName().Name;

                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                ISampler sampler = new ConstSampler(true);

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
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pokédex V1");
                c.RoutePrefix = string.Empty;
            });

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}