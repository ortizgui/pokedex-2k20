using System.Threading.Tasks;
using Pokedex.Domain.Dtos.Pokemon;

namespace Pokedex.Domain.Services.PokemonServices
{
    public interface IPokemonService
    {
         Task<GetPokemonDto> BuildPokemonByNumber(int pokemonNumber);
    }
}