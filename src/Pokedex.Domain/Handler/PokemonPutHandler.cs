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
    public class PokemonPutHandler : IRequestHandler<PokemonPutCommand, ServiceResponse<GetPokemonDto>>
    {
        private readonly IPokemonRepository _pokemonRepository;

        public PokemonPutHandler(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }

        public async Task<ServiceResponse<GetPokemonDto>> Handle(PokemonPutCommand request, CancellationToken cancellationToken)
        { 
            var serviceResponse = new ServiceResponse<GetPokemonDto>();

            var pokemon = await _pokemonRepository.GetPokemon(EnumPokemonSelectOptions.Number, request.Number.ToString());

            if (pokemon == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Sorry, you don't have registered Pokémon number: {request.Number}. Can't perform update operation.";

                return serviceResponse;
            }

            var updatedPokemon = request.Adapt<SavePokemonRepositoryDto>();

            updatedPokemon.DateCreated = pokemon.DateCreated;
            updatedPokemon.DateUpdated = DateTime.Now;
            
            await _pokemonRepository.SavePokemon(updatedPokemon);
            
            serviceResponse.Data = updatedPokemon.Adapt<GetPokemonDto>();
            serviceResponse.Message = $"Pokémon: {updatedPokemon.Name} has been updated successfully.";

            return serviceResponse;
        }
    }
}
