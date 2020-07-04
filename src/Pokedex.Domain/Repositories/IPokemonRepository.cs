using System.Threading.Tasks;
using Pokedex.Domain.Dtos.Pokemon;
using Pokedex.Domain.Dtos.Repository;
using Pokedex.Domain.Entities;

namespace Pokedex.Domain.Repositories
{
    public interface IPokemonRepository
    {
        Task<GetPokemonRepositoryDto> GetPokemon(EnumPokemonSelectOptions selectOption, string pokemonIdentity);
        Task SavePokemon(SavePokemonRepositoryDto savePokemon);
        Task DeletePokemon(int pokemonId);
    }
}