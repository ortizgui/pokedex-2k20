using Pokedex.Domain.Entities;
using Pokedex.Domain.Repository.Interfaces;
using System.Threading.Tasks;

namespace Pokedex.Domain.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private PokemonEntity pokemonSample;

        public PokemonRepository()
        {
            pokemonSample = new PokemonEntity()
            {
                Id = 4,
                Name = "Squirtle"
            };
        }
        public async Task<ServiceResponse<PokemonEntity>> GetByNumber(PokemonEntity pokemon)
        {
            ServiceResponse<PokemonEntity> serviceResponse = new ServiceResponse<PokemonEntity>();

            serviceResponse.Data = pokemonSample;
            return await Task.FromResult(serviceResponse);
        }
    }
}