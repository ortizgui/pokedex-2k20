using System.Threading.Tasks;
using Pokedex.Domain.Dtos.Pokemon;

namespace Pokedex.Domain.Repositories
{
    public interface IPokemonRepository
    {
        Task<GetPokemonDto> GetPokemonByNumber(int pokemonId);
        Task<GetPokemonDto> GetPokemonByName(string pokemonName);
        Task InsertPokemonAsync(AddPokemonDto pokemonDto);
        Task DeletePokemon(int pokemonId);
    }
}