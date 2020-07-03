using System.Threading.Tasks;
using Pokedex.Domain.Dtos.Pokemon;
using Pokedex.Domain.Dtos.Repository;
using Pokedex.Domain.Entities;

namespace Pokedex.Domain.Repositories
{
    public interface IPokemonRepository
    {
        Task<GetPokemonDto> GetPokemon(EnumPokemonSelectOptions selectOption, string pokemonIdentity);
        Task SavePokemon(AddPokemonRepositoryDto pokemonRepositoryDto);
        Task DeletePokemon(int pokemonId);
    }
}