using System;
using Pokedex.Domain.Commands;
using Pokedex.Domain.Entities;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Mapster;
using Pokedex.Domain.Dtos.Pokemon;
using Pokedex.Domain.ExternalServices;
using Pokedex.Domain.Repositories;

namespace Pokedex.Domain.Handler
{
    public class PokemonGetByNumberHandler : IRequestHandler<PokemonGetByNumberCommand, ServiceResponse<GetPokemonDto>>
    {
        private readonly IPokemonExternalService _pokemonExternalService;
        private readonly IPokemonRepository _pokemonRepository;

        public PokemonGetByNumberHandler(IPokemonExternalService pokemonExternalService,
                                        IPokemonRepository pokemonRepository)
        {
            _pokemonExternalService = pokemonExternalService;
            _pokemonRepository = pokemonRepository;
        }

        public async Task<ServiceResponse<GetPokemonDto>> Handle(PokemonGetByNumberCommand request, CancellationToken cancellationToken)
        { 
            ServiceResponse<GetPokemonDto> serviceResponse = new ServiceResponse<GetPokemonDto>();

            GetPokemonDto pokemonDto;
                
            pokemonDto = await _pokemonRepository.GetPokemonByNumber(request.Number);

            if (String.IsNullOrEmpty(pokemonDto.Name))
            {
                pokemonDto = await _pokemonExternalService.GetPokemonByNumber(request.Number);
                await _pokemonRepository.InsertPokemonAsync(pokemonDto.Adapt<AddPokemonDto>());
            }

            serviceResponse.Data = pokemonDto;

            if (pokemonDto == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Sorry, we can't find any info about that pokemon.";
            }

            return serviceResponse;
        }
    }
}
