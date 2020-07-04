using System;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using Pokedex.Domain.Commands;
using Pokedex.Domain.Dtos.Pokemon;
using Pokedex.Domain.Dtos.Repository;
using Pokedex.Domain.Entities;
using Pokedex.Domain.ExternalServices;
using Pokedex.Domain.Repositories;

namespace Pokedex.Domain.Handler
{
    public class PokemonGetHandler : IRequestHandler<PokemonGetByNumberCommand, ServiceResponse<GetPokemonDto>>
    {
        private readonly IPokemonExternalService _pokemonExternalService;
        private readonly IPokemonRepository _pokemonRepository;

        public PokemonGetHandler(IPokemonExternalService pokemonExternalService,
            IPokemonRepository pokemonRepository)
        {
            _pokemonExternalService = pokemonExternalService;
            _pokemonRepository = pokemonRepository;

            TypeAdapterConfig<GetPokemonDto, SavePokemonRepositoryDto>
                .NewConfig()
                .Ignore(dest => dest.DateCreated);
        }

        public async Task<ServiceResponse<GetPokemonDto>> Handle(PokemonGetByNumberCommand request,
            CancellationToken cancellationToken)
        {
            var serviceResponse = new ServiceResponse<GetPokemonDto>();

            var pokemonRepositoryDto =
                await _pokemonRepository.GetPokemon(EnumPokemonSelectOptions.Number, request.Number.ToString());

            if (pokemonRepositoryDto == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Sorry, we can't find any info about that pokemon.";
            }

            serviceResponse.Data = pokemonRepositoryDto.Adapt<GetPokemonDto>();

            return serviceResponse;
        }

        private async Task<GetPokemonDto> GetPokemonInfoApi(int pokemonNumber)
        {
            GetPokemonDto pokemonDto;

            try
            {
                pokemonDto = await _pokemonExternalService.GetPokemonByNumber(pokemonNumber);
                if (!string.IsNullOrEmpty(pokemonDto.Name))
                    await _pokemonRepository.SavePokemon(pokemonDto.Adapt<SavePokemonRepositoryDto>());
            }
            catch (Exception e)
            {
                pokemonDto = null;
            }

            return pokemonDto;
        }
    }
}