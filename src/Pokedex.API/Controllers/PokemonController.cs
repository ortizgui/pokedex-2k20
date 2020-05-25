using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Pokedex.Domain.Commands;
using System;

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

        [HttpGet("{pokemonNumber}")]
        public async Task<IActionResult> GetPokemonByNumber(string pokemonNumber)
        {
            var command = new PokemonGetByNumberCommand { Number = Int32.Parse(pokemonNumber) };
            var serviceResponse = _mediator.Send(command);
            return Ok(serviceResponse.Result);
        }
    }
}