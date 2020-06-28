using Pokedex.Domain.Dtos.Pokemon;

namespace Pokedex.Domain.Repositories
{
    public interface IPokemonRepository
    {
        GetPokemonDto GetPokemonByID(int pokemonId);
        GetPokemonDto GetPokemonByName(string pokemonName);
        void InsertPokemon(AddPokemonDto pokemon);        
        void DeletePokemon(int pokemonId);        
        void UpdatePokemon(UpdatePokemonDto pokemon);  
    }
}