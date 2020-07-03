using System;

namespace Pokedex.Domain.Dtos.Repository
{
    public class AddPokemonRepositoryDto
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        //public List<string> Types { get; set; }
    }
}