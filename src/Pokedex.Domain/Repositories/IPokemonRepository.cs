using System.Threading.Tasks;
using Pokedex.Domain.Dtos.Pokemon;

namespace Pokedex.Domain.Repositories
{
    public interface IPokemonRepository
    {
        GetPokemonDto GetPokemonByID(int pokemonId);
        GetPokemonDto GetPokemonByName(string pokemonName);
        Task InsertPokemonAsync(AddPokemonDto pokemonDto);  
        void DeletePokemon(int pokemonId);        
        void UpdatePokemon(UpdatePokemonDto pokemon);  
    }
}