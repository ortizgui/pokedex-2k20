using System;

namespace Pokedex.Domain.Dtos.PokemonType
{
    public class AddPokemonTypeDto
    {
        public int PokemonId { get; set; }
        public int TypeId { get; set; }
    }
}