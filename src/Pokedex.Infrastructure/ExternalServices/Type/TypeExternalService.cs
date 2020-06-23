using System.Collections.Generic;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Pokedex.Domain.Dtos.Pokemon;
using Pokedex.Domain.Dtos.PokemonType;
using Pokedex.Domain.Dtos.Type;
using Pokedex.Domain.Entities;
using Pokedex.Infrastructure.Data;
using Pokedex.Domain.ExternalServices;

namespace Pokedex.Infrastructure.ExternalServices.Type
{
    public class TypeExternalService : ITypeExternalService
    {
        private readonly ApplicationDbContext _context;

        public TypeExternalService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetTypeDto> AddTypeDb(AddTypeDto type)
        {
            var typeDto = await GetTypeByNameDb(type.Name);

            if(typeDto == null)
            {
                var addType = type.Adapt<TypeEntity>();

                await _context.TB_TYPES.AddAsync(addType);
                await _context.SaveChangesAsync();

                typeDto = await GetTypeByNameDb(type.Name);
            }

            return typeDto.Adapt<GetTypeDto>();
        }

        public async Task<GetTypeDto> GetTypeByIdDb(int typeId)
        {
            var dbType = await _context.TB_TYPES
                                    .FirstOrDefaultAsync(t => t.Id == typeId);

            var typeDto = dbType.Adapt<GetTypeDto>();
                                    
            return typeDto;
        }

        public async Task<GetTypeDto> GetTypeByNameDb(string typeName)
        {
            var dbType = await _context.TB_TYPES
                                    .FirstOrDefaultAsync(t => t.Name == typeName);

            var typeDto = dbType.Adapt<GetTypeDto>();
                                    
            return typeDto;
        }
    }
}