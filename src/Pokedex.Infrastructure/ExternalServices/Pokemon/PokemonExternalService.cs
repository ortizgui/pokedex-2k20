using System.Collections.Generic;
using System.Threading.Tasks;
using Mapster;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Pokedex.Domain.Dtos.Pokemon;
using Pokedex.Domain.Entities;
using Pokedex.Domain.ExternalServices;
using Pokedex.Infrastructure.ExternalServices.Dtos;
using RestSharp;

namespace Pokedex.Infrastructure.ExternalServices.Pokemon
{
    public class PokemonExternalService : IPokemonExternalService
    {
        private readonly AppSettings _appSettings;
        private readonly RestClient _pokeClient;

        public PokemonExternalService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _pokeClient = new RestClient(_appSettings.PokeUrl);

            TypeAdapterConfig<GetPokemonApiDto, GetPokemonDto>
                .NewConfig()
                .Map(dest => dest.Number, src => src.Id);
            //.Ignore(dest => dest.Types);
        }

        public async Task<GetPokemonDto> GetPokemonByName(string pokemonName)
        {
            var request = new RestRequest($"api/v2/pokemon/{pokemonName}", Method.GET);

            var response = await _pokeClient.ExecuteAsync(request);

            var pokemonApiDto = JsonConvert
                .DeserializeObject<GetPokemonApiDto>(response.Content);

            var pokemonDto = pokemonApiDto.Adapt<GetPokemonDto>();

            //pokemonDto.Types = new List<string>(NormalizePokemonTypes(pokemonApiDto.Types));

            return pokemonDto;
        }

        public async Task<GetPokemonDto> GetPokemonByNumber(int pokemonNumber)
        {
            var request = new RestRequest($"api/v2/pokemon/{pokemonNumber}", Method.GET);

            var response = await _pokeClient.ExecuteAsync(request);

            var pokemonApiDto = JsonConvert
                .DeserializeObject<GetPokemonApiDto>(response.Content);

            var pokemonDto = pokemonApiDto.Adapt<GetPokemonDto>();

            //pokemonDto.Types = new List<string>(NormalizePokemonTypes(pokemonApiDto.Types));

            return pokemonDto;
        }

        private List<string> NormalizePokemonTypes(List<TypesGroupApiDto> typesGroup)
        {
            var pokemonTypes = new List<string>();

            foreach (var types in typesGroup) pokemonTypes.Add(types.Type.Name);

            return pokemonTypes;
        }
    }
}