using System.Collections.Generic;
using System.Threading.Tasks;
using Mapster;
using Pokedex.Domain.Dtos.PokemonType;
using Pokedex.Domain.Dtos.Type;
using Pokedex.Domain.Entities;
using Pokedex.Domain.ExternalServices;
using Pokedex.Domain.Services.PokemonTypeServices;

namespace Pokedex.Domain.Services.TypeServices
{
    public class TypeService : ITypeService
    {
        private readonly ITypeExternalService _typeExternalService;
        private readonly IPokemonTypeService _pokemonTypeService;
        public TypeService(ITypeExternalService typeExternalService,
                            IPokemonTypeService pokemonTypeService)
        {
            _typeExternalService = typeExternalService;
            _pokemonTypeService = pokemonTypeService;
        }
        public async Task AddTypes(List<TypesEntity> newTypes, int pokemonId)
        {
            GetTypeDto typeDto;

            foreach (var types in newTypes)
            {
                typeDto = await _typeExternalService.AddTypeDb(types.Type.Adapt<AddTypeDto>());

                await _pokemonTypeService.AddPokemonType(pokemonId, typeDto.Id);
            }
        }

        public async Task<List<TypesEntity>> GetTypesByPokemonId(int pokemonId)
        {
            List<TypesEntity> typeList = new List<TypesEntity>();
            List<GetPokemonTypeDto> pokemonTypes = await _pokemonTypeService.GetPokemonTypeByPokemonId(pokemonId);

            foreach (var pt in pokemonTypes)
            {
                var typesEntity = new TypesEntity();
                var getTypeDto = await _typeExternalService.GetTypeByIdDb(pt.TypeId);

                var typeEntity = new TypeEntity()
                {
                    Id = getTypeDto.Id,
                    Name = getTypeDto.Name
                };

                typesEntity.Type = typeEntity;

                typeList.Add(typesEntity);
            }

            return typeList;
        }
    }
}