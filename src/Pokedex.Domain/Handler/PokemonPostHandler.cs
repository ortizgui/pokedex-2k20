using System;
using Pokedex.Domain.Commands;
using Pokedex.Domain.Entities;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Mapster;
using Pokedex.Domain.Dtos.Pokemon;
using Pokedex.Domain.Dtos.Repository;
using Pokedex.Domain.ExternalServices;
using Pokedex.Domain.Repositories;

namespace Pokedex.Domain.Handler
{
    public class PokemonPostHandler : IRequestHandler<PokemonPostCommand, ServiceResponse<GetPokemonDto>>
    {
        private readonly IPokemonRepository _pokemonRepository;

        public PokemonPostHandler(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }

        public async Task<ServiceResponse<GetPokemonDto>> Handle(PokemonPostCommand request, CancellationToken cancellationToken)
        { 
            var serviceResponse = new ServiceResponse<GetPokemonDto>();

            var pokemon = await _pokemonRepository.GetPokemon(EnumPokemonSelectOptions.Number, request.Number.ToString());

            if (pokemon != null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Sorry, you already have registered Pokémon number: {request.Number}.";

                return serviceResponse;
            }
            
            await _pokemonRepository.SavePokemon(request.Adapt<SavePokemonRepositoryDto>());
            
            serviceResponse.Data = request.Adapt<GetPokemonDto>();

            serviceResponse.Message = $"Pokémon: {request.Name} has been added successfully.";

            return serviceResponse;
        }
    }
}
