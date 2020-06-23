using Pokedex.Domain.Commands;
using Pokedex.Domain.Entities;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Pokedex.Domain.Services.PokemonServices;
using Pokedex.Domain.Dtos.Pokemon;

namespace Pokedex.Domain.Handler
{
    public class PokemonGetHandler : IRequestHandler<PokemonGetByNumberCommand, ServiceResponse<GetPokemonDto>>
    {
        private readonly IPokemonService _pokemonService;

        public PokemonGetHandler(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        public async Task<ServiceResponse<GetPokemonDto>> Handle(PokemonGetByNumberCommand request, CancellationToken cancellationToken)
        { 
            ServiceResponse<GetPokemonDto> serviceResponse = new ServiceResponse<GetPokemonDto>();

            var pokemonDto = await _pokemonService.BuildPokemonByNumber(request.Number);

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
