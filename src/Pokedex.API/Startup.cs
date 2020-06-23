using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MediatR;
using Pokedex.Domain.Repository;
using Pokedex.Domain.Repository.Interfaces;
using Pokedex.Domain.Entities;
using Pokedex.Domain.ExternalServices;
using Pokedex.Infrastructure.ExternalServices.Pokemon;
using Pokedex.Domain.Services.PokemonServices;
using Pokedex.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Pokedex.Infrastructure.ExternalServices.Type;
using Pokedex.Infrastructure.ExternalServices.PokemonType;
using Pokedex.Domain.Services.TypeServices;
using Pokedex.Domain.Services.PokemonTypeServices;
using Microsoft.OpenApi.Models;

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

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptions => sqlServerOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            var assembly = AppDomain.CurrentDomain.Load("Pokedex.Domain");

            services.AddControllers();

            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc(name:"v1", new OpenApiInfo { Title = "Pokédex", Version = "v1" });
                });

            services.AddMediatR(assembly);
            services.AddScoped<IPokemonExternalService, PokemonExternalService>();
            services.AddScoped<ITypeExternalService, TypeExternalService>();
            services.AddScoped<IPokemonTypeExternalService, PokemonTypeExternalService>();
            services.AddScoped<IPokemonService, PokemonService>();
            services.AddScoped<ITypeService, TypeService>();
            services.AddScoped<IPokemonTypeService, PokemonTypeService>();

            var pokeApiSection = Configuration.GetSection("PokeApi");
            services.Configure<AppSettings>(pokeApiSection);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
