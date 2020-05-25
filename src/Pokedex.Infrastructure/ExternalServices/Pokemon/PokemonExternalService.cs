using Microsoft.Extensions.Options;
using Pokedex.Domain.Entities;
using Pokedex.Domain.ExternalServices;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Pokedex.Infrastructure.ExternalServices.Dtos;
using Newtonsoft;
using Mapster;

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
        }
        public Task<ServiceResponse<PokemonEntity>> GetPokemonByName(string pokemonName)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ServiceResponse<PokemonEntity>> GetPokemonByNumber(int pokemonNumber)
        {
            ServiceResponse<PokemonEntity> serviceResponse = new ServiceResponse<PokemonEntity>();

            var request = new RestRequest($"api/v2/pokemon/{pokemonNumber}", Method.GET);

            IRestResponse response = _pokeClient.Execute(request);

            var pokemonObj = Newtonsoft.Json.JsonConvert.DeserializeObject<GetPokemonDto>(response.Content);

            var pokemon = pokemonObj.Adapt<PokemonEntity>();

            serviceResponse.Data = pokemon;

            return await Task.FromResult(serviceResponse);
        }
    }
}
