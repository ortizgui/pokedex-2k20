using System;
using Pokedex.Domain.Commands;
using Pokedex.Domain.Entities;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Mapster;
using Pokedex.Domain.Dtos.Pokemon;
using Pokedex.Domain.Dtos.Repository;
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
            
            TypeAdapterConfig<GetPokemonDto, AddPokemonRepositoryDto>
                .NewConfig()
                .Ignore(dest => dest.DateCreated);
        }

        public async Task<ServiceResponse<GetPokemonDto>> Handle(PokemonGetByNumberCommand request, CancellationToken cancellationToken)
        { 
            ServiceResponse<GetPokemonDto> serviceResponse = new ServiceResponse<GetPokemonDto>();

            var pokemonDto = await _pokemonRepository.GetPokemon(EnumPokemonSelectOptions.Number, request.Number.ToString());

            if (String.IsNullOrEmpty(pokemonDto.Name))
                pokemonDto = await GetPokemonInfoApi(request.Number);

            serviceResponse.Data = pokemonDto;

            if (pokemonDto == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Sorry, we can't find any info about that pokemon.";
            }

            return serviceResponse;
        }

        private async Task<GetPokemonDto> GetPokemonInfoApi(int pokemonNumber)
        {
            GetPokemonDto pokemonDto;
            
            try
            {
                pokemonDto = await _pokemonExternalService.GetPokemonByNumber(pokemonNumber);
                if (!String.IsNullOrEmpty(pokemonDto.Name))
                    await _pokemonRepository.SavePokemon(pokemonDto.Adapt<AddPokemonRepositoryDto>());
            }
            catch (Exception e)
            {
                pokemonDto = null;
            }

            return pokemonDto;
        }
    }
}
