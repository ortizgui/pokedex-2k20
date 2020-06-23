using System.Collections.Generic;
using System.Threading.Tasks;
using Pokedex.Domain.Dtos.PokemonType;

namespace Pokedex.Domain.ExternalServices
{
    public interface IPokemonTypeExternalService
    {
         Task AddPokemonTypeDb(AddPokemonTypeDto newPokemonTypeDto);
         Task<List<GetPokemonTypeDto>> GetPokemonTypeByPokemonIdDb(int pokemonId);
         Task DeletePokemonTypeByPokemonIdDb(int pokemonId);
    }
}