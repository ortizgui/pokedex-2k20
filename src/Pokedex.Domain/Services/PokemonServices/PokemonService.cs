using System.Threading.Tasks;
using Pokedex.Domain.Entities;
using Pokedex.Domain.ExternalServices;
using Pokedex.Domain.Services.PokemonServices;
using Mapster;
using Pokedex.Domain.Dtos.Pokemon;
using Pokedex.Domain.Dtos.Type;
using Pokedex.Domain.Dtos.PokemonType;
using System.Collections.Generic;
using System;
using Pokedex.Domain.Services.TypeServices;

namespace Pokedex.Domain.Services.PokemonServices
{
    public class PokemonService : IPokemonService
    {
        private readonly IPokemonExternalService _pokemonExternalService;
        private readonly ITypeService _typeService;

        public PokemonService(IPokemonExternalService pokemonExternalService,
                                ITypeService typeService)
        {
            _pokemonExternalService = pokemonExternalService;
            _typeService = typeService;

            TypeAdapterConfig<GetPokemonDto, AddPokemonDto>
                .NewConfig()
                .Map(dest => dest.Number, src => src.Id)
                .Ignore(dest => dest.Id);
        }

        public async Task<GetPokemonDto> BuildPokemonByNumber(int pokemonNumber)
        {
            GetPokemonDto registeredPokemon;

            registeredPokemon = await _pokemonExternalService.GetPokemonByNumberDb(pokemonNumber);

            if (registeredPokemon == null)
            {
                var pokemonApi = await _pokemonExternalService.GetPokemonByNumberApi(pokemonNumber);

                registeredPokemon = await _pokemonExternalService.GetPokemonByNumberDb(await AddPokemon(pokemonApi.Adapt<AddPokemonDto>()));
            }

            registeredPokemon.Types = await _typeService.GetTypesByPokemonId(registeredPokemon.Id);

            return registeredPokemon;
        }

        private async Task<int> AddPokemon(AddPokemonDto newPokemonDto)
        {
            var pokemonDb = await _pokemonExternalService.AddPokemonDb(newPokemonDto);

            await _typeService.AddTypes(newPokemonDto.Types, pokemonDb.Id);

            return pokemonDb.Number;
        }
    }
}