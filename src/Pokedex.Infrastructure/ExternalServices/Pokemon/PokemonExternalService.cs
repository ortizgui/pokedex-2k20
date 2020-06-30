using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Pokedex.Domain.Entities;
using Pokedex.Domain.ExternalServices;
using RestSharp;
using System.Threading.Tasks;
using Mapster;
using Pokedex.Domain.Dtos.Pokemon;
using Pokedex.Infrastructure.ExternalServices.Dtos;

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
                .Map(dest => dest.Number, src => src.Id)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.Types);
        }

        public async Task<GetPokemonDto> GetPokemonByNameApi(string pokemonName)
        {
            var request = new RestRequest($"api/v2/pokemon/{pokemonName}", Method.GET);

            IRestResponse response = await _pokeClient.ExecuteAsync(request);

            var pokemonDto = Newtonsoft.Json.JsonConvert
                                .DeserializeObject<GetPokemonDto>(response.Content);

            return pokemonDto;
        }

        public async Task<GetPokemonDto> GetPokemonByNumberApi(int pokemonNumber)
        {
            var request = new RestRequest($"api/v2/pokemon/{pokemonNumber}", Method.GET);

            IRestResponse response = await _pokeClient.ExecuteAsync(request);

            var pokemonApiDto = Newtonsoft.Json.JsonConvert
                .DeserializeObject<GetPokemonApiDto>(response.Content);
            
            var pokemonDto = pokemonApiDto.Adapt<GetPokemonDto>();

            pokemonDto.Types = new List<string>(NormalizePokemonTypes(pokemonApiDto.Types));

            return pokemonDto;
        }

        public async Task<GetPokemonDto> AddPokemonDb(AddPokemonDto pokemon)
        {
            throw new NotImplementedException();
            // var dbPokemon = await GetPokemonByNameDb(pokemon.Name);

            // if(dbPokemon == null)
            // {
            //     var addPokemon = pokemon.Adapt<PokemonEntity>();

            //     await _context.TB_POKEMONS.AddAsync(addPokemon);
            //     await _context.SaveChangesAsync();

            //     dbPokemon = await GetPokemonByNameDb(pokemon.Name);
            // }

            // return dbPokemon.Adapt<GetPokemonDto>();
        }

        public async Task<GetPokemonDto> GetPokemonByNameDb(string pokemonName)
        {
            throw new NotImplementedException();
            // var dbPokemon = await _context.TB_POKEMONS
            //                         .FirstOrDefaultAsync(p => p.Name == pokemonName);
            
            // return dbPokemon.Adapt<GetPokemonDto>();
        }

        public async Task<GetPokemonDto> GetPokemonByNumberDb(int pokemonNumber)
        {
            throw new NotImplementedException();
            // var dbPokemon = await _context.TB_POKEMONS
            //                         .FirstOrDefaultAsync(p => p.Number == pokemonNumber);

            // return dbPokemon.Adapt<GetPokemonDto>();
        }

        private List<string> NormalizePokemonTypes(List<TypesGroupApiDto> typesGroup)
        {
            List<string> pokemonTypes = new List<string>();
            
            foreach (var types in typesGroup)
            {
                pokemonTypes.Add(types.Type.Name);    
            }

            return pokemonTypes;
        }
    }
}
