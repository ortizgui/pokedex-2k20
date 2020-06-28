using System.Threading.Tasks;
using Pokedex.Domain.ExternalServices;
using Mapster;
using Pokedex.Domain.Dtos.Pokemon;

namespace Pokedex.Domain.Services.PokemonServices
{
    public class PokemonService : IPokemonService
    {
        private readonly IPokemonExternalService _pokemonExternalService;
        public PokemonService(IPokemonExternalService pokemonExternalService)
        {
            _pokemonExternalService = pokemonExternalService;

            TypeAdapterConfig<GetPokemonDto, AddPokemonDto>
                .NewConfig()
                .Map(dest => dest.Number, src => src.Id)
                .Ignore(dest => dest.Id);
        }

        public async Task<GetPokemonDto> BuildPokemonByNumber(int pokemonNumber)
        {
            //GetPokemonDto registeredPokemon;

            var pokemonApi = await _pokemonExternalService.GetPokemonByNumberApi(pokemonNumber);

            await AddPokemon(pokemonApi.Adapt<AddPokemonDto>());

            return new GetPokemonDto();
        }

        private async Task<int> AddPokemon(AddPokemonDto newPokemonDto)
        {
            var pokemonDb = await _pokemonExternalService.AddPokemonDb(newPokemonDto);

            return pokemonDb.Number;
        }
    }
}