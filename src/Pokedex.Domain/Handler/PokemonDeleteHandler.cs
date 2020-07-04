using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Pokedex.Domain.Commands;
using Pokedex.Domain.Dtos.Pokemon;
using Pokedex.Domain.Entities;
using Pokedex.Domain.Repositories;

namespace Pokedex.Domain.Handler
{
    public class PokemonDeleteHandler : IRequestHandler<PokemonDeleteCommand, ServiceResponse<GetPokemonDto>>
    {
        private readonly IPokemonRepository _pokemonRepository;

        public PokemonDeleteHandler(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }

        public async Task<ServiceResponse<GetPokemonDto>> Handle(PokemonDeleteCommand request,
            CancellationToken cancellationToken)
        {
            var serviceResponse = new ServiceResponse<GetPokemonDto>();

            try
            {
                var pokemonDto = await _pokemonRepository.GetPokemon(EnumPokemonSelectOptions.Number,
                    request.Number.ToString());

                if (string.IsNullOrEmpty(pokemonDto.Name))
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