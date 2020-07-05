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
    public class PokemonGetHandler : IRequestHandler<PokemonGetCommand, ServiceResponse<GetPokemonDto>>
    {
        private readonly IPokemonRepository _pokemonRepository;

        public PokemonGetHandler(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;

            TypeAdapterConfig<GetPokemonDto, SavePokemonRepositoryDto>
                .NewConfig()
                .Ignore(dest => dest.DateCreated);
        }

        public async Task<ServiceResponse<GetPokemonDto>> Handle(PokemonGetCommand request,
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
    }
}