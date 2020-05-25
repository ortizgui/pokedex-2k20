using MediatR;
using Pokedex.Domain.Entities;

namespace Pokedex.Domain.Commands
{
    public class PokemonGetByNumberCommand : IRequest<ServiceResponse<PokemonEntity>>
    {
        public int Number { get; set; }
    }
}