using Microsoft.Extensions.Options;
using Pokedex.Domain.Entities;
using Pokedex.Domain.ExternalServices;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Pokedex.Domain.Dtos.Pokemon;
using Newtonsoft;
using Pokedex.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Mapster;
using Pokedex.Domain.Dtos.PokemonType;
using System.Linq;
using Newtonsoft.Json;

namespace Pokedex.Infrastructure.ExternalServices.Pokemon
{
    public class PokemonExternalService : IPokemonExternalService
    {
        private readonly AppSettings _appSettings;
        private readonly RestClient _pokeClient;
        private readonly ApplicationDbContext _context;

        public PokemonExternalService(IOptions<AppSettings> appSettings, ApplicationDbContext context)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _pokeClient = new RestClient(_appSettings.PokeUrl);
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

            var pokemonDto = Newtonsoft.Json.JsonConvert
                                .DeserializeObject<GetPokemonDto>(response.Content);

            return pokemonDto;
        }

        public async Task<GetPokemonDto> AddPokemonDb(AddPokemonDto pokemon)
        {
            var dbPokemon = await GetPokemonByNameDb(pokemon.Name);

            if(dbPokemon == null)
            {
                var addPokemon = pokemon.Adapt<PokemonEntity>();

                await _context.TB_POKEMONS.AddAsync(addPokemon);
                await _context.SaveChangesAsync();

                dbPokemon = await GetPokemonByNameDb(pokemon.Name);
            }

            return dbPokemon.Adapt<GetPokemonDto>();
        }

        public async Task<GetPokemonDto> GetPokemonByNameDb(string pokemonName)
        {
            var dbPokemon = await _context.TB_POKEMONS
                                    .FirstOrDefaultAsync(p => p.Name == pokemonName);
            
            return dbPokemon.Adapt<GetPokemonDto>();
        }

        public async Task<GetPokemonDto> GetPokemonByNumberDb(int pokemonNumber)
        {
            var dbPokemon = await _context.TB_POKEMONS
                                    .FirstOrDefaultAsync(p => p.Number == pokemonNumber);

            return dbPokemon.Adapt<GetPokemonDto>();
        }
    }
}
