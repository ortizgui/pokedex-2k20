using MediatR;
using Pokedex.Domain.Dtos.Pokemon;
using Pokedex.Domain.Entities;

namespace Pokedex.Domain.Commands
{
    public class PokemonPostCommand : IRequest<ServiceResponse<GetPokemonDto>>
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
    }
}