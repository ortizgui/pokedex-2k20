using MediatR;
using Pokedex.Domain.Dtos.Pokemon;
using Pokedex.Domain.Entities;

namespace Pokedex.Domain.Commands
{
    public class PokemonDeleteCommand : IRequest<ServiceResponse<GetPokemonDto>>
    {
        public int Number { get; set; }
    }
}