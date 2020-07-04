using System.Threading.Tasks;
using Pokedex.Domain.Dtos.Pokemon;

namespace Pokedex.Domain.ExternalServices
{
    public interface IPokemonExternalService
    {
        Task<GetPokemonDto> GetPokemonByName(string pokemonName);
        Task<GetPokemonDto> GetPokemonByNumber(int pokemonNumber);
    }
}