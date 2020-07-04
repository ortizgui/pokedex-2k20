using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Domain.Commands;

namespace Pokedex.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PokemonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> PostPokemon([FromBody] PokemonPostCommand command)
        {
            var serviceResponse = await _mediator.Send(command);
            return Ok(serviceResponse);
        }

        [HttpGet("{Number}")]
        public async Task<IActionResult> GetPokemonByNumber([FromRoute] PokemonGetByNumberCommand command)
        {
            var serviceResponse = await _mediator.Send(command);
            return Ok(serviceResponse);
        }

        [HttpPut]
        public async Task<IActionResult> PutPokemon([FromBody] PokemonPutCommand command)
        {
            var serviceResponse = await _mediator.Send(command);
            return Ok(serviceResponse);
        }

        [HttpDelete("{Number}")]
        public async Task<IActionResult> DeletePokemonByNumber([FromRoute] PokemonDeleteCommand command)
        {
            var serviceResponse = await _mediator.Send(command);
            return Ok(serviceResponse);
        }
    }
}