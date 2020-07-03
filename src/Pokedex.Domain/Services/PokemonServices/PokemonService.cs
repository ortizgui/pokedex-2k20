using System;
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
            GetPokemonDto pokemonDto;
                
            pokemonDto = await _pokemonRepository.GetPokemonByNumber(pokemonNumber);

            if (String.IsNullOrEmpty(pokemonDto.Name))
            {
                pokemonDto = await _pokemonExternalService.GetPokemonByNumber(pokemonNumber);
                await AddPokemon(pokemonDto.Adapt<AddPokemonDto>());
            }

            return pokemonDto;
        }

        private async Task AddPokemon(AddPokemonDto newPokemonDto)
        {
            await _pokemonRepository.InsertPokemonAsync(newPokemonDto);
        }
    }
}