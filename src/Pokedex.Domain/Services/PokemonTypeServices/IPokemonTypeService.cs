using System.Collections.Generic;
using System.Threading.Tasks;
using Pokedex.Domain.Dtos.PokemonType;

namespace Pokedex.Domain.Services.PokemonTypeServices
{
    public interface IPokemonTypeService
    {
         Task AddPokemonType(int pokemonId, int typeId);
         Task<List<GetPokemonTypeDto>> GetPokemonTypeByPokemonId(int pokemonId);
    }
}