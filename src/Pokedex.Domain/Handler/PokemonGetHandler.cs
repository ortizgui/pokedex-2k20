using Pokedex.Domain.Commands;
using Pokedex.Domain.Entities;
using Pokedex.Domain.ExternalServices;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Mapster;

namespace Pokedex.Domain.Handler
{
    public class PokemonGetHandler : IRequestHandler<PokemonGetByNumberCommand, ServiceResponse<PokemonEntity>>
    {
        private readonly IMediator _mediator;
        private readonly IPokemonExternalService _pokemonExternalService;

        public PokemonGetHandler(IMediator mediator, IPokemonExternalService pokemonExternalService)
        {
            _mediator = mediator;
            _pokemonExternalService = pokemonExternalService;
        }

        public async Task<ServiceResponse<PokemonEntity>> Handle(PokemonGetByNumberCommand request, CancellationToken cancellationToken)
        { 
            return await _pokemonExternalService.GetPokemonByNumber(request.Number);
        }
    }
}
