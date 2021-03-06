using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
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

        [HttpGet("number/{Number}")]
        public async Task<IActionResult> GetPokemonByNumber([FromRoute]PokemonGetByNumberCommand command)
        {
            var serviceResponse = await _mediator.Send(command);
            return Ok(serviceResponse);
        }
        
        [HttpDelete("{Number}")]
        public async Task<IActionResult> DeletePokemonByNumber([FromRoute]PokemonDeleteCommand command)
        {
            var serviceResponse = await _mediator.Send(command);
            return Ok(serviceResponse);
        }
    }
}