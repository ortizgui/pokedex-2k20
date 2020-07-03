using Pokedex.Domain.Dtos.Pokemon;
using System.Threading.Tasks;

namespace Pokedex.Domain.ExternalServices
{
    public interface IPokemonExternalService
    {
        Task<GetPokemonDto> GetPokemonByName(string pokemonName);
        Task<GetPokemonDto> GetPokemonByNumber(int pokemonNumber);
    }
}
