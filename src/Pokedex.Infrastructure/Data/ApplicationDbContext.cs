using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Pokedex.Domain.Entities;
using System.IO;

namespace Pokedex.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) 
        {  }

        public DbSet<PokemonEntity> TB_POKEMONS {get; set;}
        public DbSet<TypeEntity> TB_TYPES { get; set; }
        public DbSet<PokemonTypeEntity> TB_POKEMONTYPE { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PokemonTypeEntity>()
                .HasKey(cs => new { cs.PokemonId, cs.TypeId });
        }
    }
}