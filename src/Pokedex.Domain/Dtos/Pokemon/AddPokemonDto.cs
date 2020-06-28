using System.Collections.Generic;
using Pokedex.Domain.Entities;

namespace Pokedex.Domain.Dtos.Pokemon
{
    public class AddPokemonDto
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public List<TypesEntity> Types { get; set; }
    }
}