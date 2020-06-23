using System.Collections.Generic;

namespace Pokedex.Domain.Entities
{
    public class TypeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PokemonTypeEntity> PokemonTypes { get; set; }
    }
}