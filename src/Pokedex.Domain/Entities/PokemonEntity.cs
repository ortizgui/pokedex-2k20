using System;

namespace Pokedex.Domain.Entities
{
    public class PokemonEntity
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateUpdated { get; set; } = null;
    }
}