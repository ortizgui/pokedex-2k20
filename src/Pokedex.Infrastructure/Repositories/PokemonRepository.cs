using Pokedex.Domain.Dtos.Pokemon;
using Pokedex.Domain.Repositories;

namespace Pokedex.Infrastructure.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        public void DeletePokemon(int pokemonId)
        {
            throw new System.NotImplementedException();
        }

        public GetPokemonDto GetPokemonByID(int pokemonId)
        {
            throw new System.NotImplementedException();
        }

        public GetPokemonDto GetPokemonByName(string pokemonName)
        {
            throw new System.NotImplementedException();
        }

        public void InsertPokemon(AddPokemonDto pokemon)
        {
            throw new System.NotImplementedException();
        }

        public void UpdatePokemon(UpdatePokemonDto pokemon)
        {
            throw new System.NotImplementedException();
        }
    }
}