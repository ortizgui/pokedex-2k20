using System.Collections.Generic;
using System.Threading.Tasks;
using Pokedex.Domain.Dtos.PokemonType;
using Pokedex.Domain.ExternalServices;

namespace Pokedex.Domain.Services.PokemonTypeServices
{
    public class PokemonTypeService : IPokemonTypeService
    {
        private readonly IPokemonTypeExternalService _pokemonTypeExternalService;

        public PokemonTypeService(IPokemonTypeExternalService pokemonTypeExternalService)
        {
            _pokemonTypeExternalService = pokemonTypeExternalService;
        }

        public async Task AddPokemonType(int pokemonId, int typeId)
        {
            var newPokemonType = new AddPokemonTypeDto
            {
                PokemonId = pokemonId,
                TypeId = typeId
            };

            await _pokemonTypeExternalService.AddPokemonTypeDb(newPokemonType);
        }

        public Task<List<GetPokemonTypeDto>> GetPokemonTypeByPokemonId(int pokemonId)
        {
            return _pokemonTypeExternalService.GetPokemonTypeByPokemonIdDb(pokemonId);
        }
    }
}