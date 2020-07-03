using System.Threading.Tasks;
using Pokedex.Domain.Dtos.Pokemon;
using Pokedex.Domain.Entities;

namespace Pokedex.Domain.Repositories
{
    public interface IPokemonRepository
    {
        Task<GetPokemonDto> GetPokemon(EnumPokemonSelectOptions selectOption, string pokemonIdentity);
        Task SavePokemon(AddPokemonDto pokemonDto);
        Task DeletePokemon(int pokemonId);
    }
}