using System.Threading.Tasks;
using Pokedex.Domain.ExternalServices;
using Mapster;
using Pokedex.Domain.Dtos.Pokemon;
using Pokedex.Domain.Repositories;

namespace Pokedex.Domain.Services.PokemonServices
{
    public class PokemonService : IPokemonService
    {
        private readonly IPokemonExternalService _pokemonExternalService;
        private readonly IPokemonRepository _pokemonRepository;
        public PokemonService(IPokemonExternalService pokemonExternalService,
                                IPokemonRepository pokemonRepository)
        {
            _pokemonExternalService = pokemonExternalService;
            _pokemonRepository = pokemonRepository;
        }

        public async Task<GetPokemonDto> BuildPokemonByNumber(int pokemonNumber)
        {
            var pokemonApi = await _pokemonExternalService.GetPokemonByNumberApi(pokemonNumber);

            var response = _pokemonRepository.GetPokemonByNumber(pokemonNumber);

            //await AddPokemon(pokemonApi.Adapt<AddPokemonDto>());

            return new GetPokemonDto();
        }

        private async Task AddPokemon(AddPokemonDto newPokemonDto)
        {
            await _pokemonRepository.InsertPokemonAsync(newPokemonDto);
        }
    }
}