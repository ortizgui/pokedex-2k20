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
    public class PokemonDeleteHandler : IRequestHandler<PokemonDeleteCommand, ServiceResponse<GetPokemonDto>>
    {
        private readonly IPokemonExternalService _pokemonExternalService;
        private readonly IPokemonRepository _pokemonRepository;

        public PokemonDeleteHandler(IPokemonExternalService pokemonExternalService,
                                        IPokemonRepository pokemonRepository)
        {
            _pokemonExternalService = pokemonExternalService;
            _pokemonRepository = pokemonRepository;
        }

        public async Task<ServiceResponse<GetPokemonDto>> Handle(PokemonDeleteCommand request, CancellationToken cancellationToken)
        { 
            ServiceResponse<GetPokemonDto> serviceResponse = new ServiceResponse<GetPokemonDto>();

            try
            {
                var pokemonDto = await _pokemonRepository.GetPokemon(EnumPokemonSelectOptions.Number,
                                                                        request.Number.ToString());

                if (String.IsNullOrEmpty(pokemonDto.Name))
                    throw new ArgumentException();
                
                await _pokemonRepository.DeletePokemon(request.Number);
                serviceResponse.Message = $"Pokemon: {pokemonDto.Name}, has been removed.";
            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Sorry, we can't find any info about that pokemon.";
            }

            return serviceResponse;
        }
    }
}
