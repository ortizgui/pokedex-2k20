using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pokedex.Domain.Dtos.PokemonType;
using Pokedex.Infrastructure.Data;
using Pokedex.Domain.Entities;
using System.Linq;
using System.Collections.Generic;
using Mapster;
using Pokedex.Domain.ExternalServices;

namespace Pokedex.Infrastructure.ExternalServices.PokemonType
{
    public class PokemonTypeExternalService : IPokemonTypeExternalService
    {
        private readonly ApplicationDbContext _context;

        public PokemonTypeExternalService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddPokemonTypeDb(AddPokemonTypeDto newPokemonTypeDto)
        {
            var dbPokemonType = await _context.TB_POKEMONTYPE
                                    .FirstOrDefaultAsync(pt => pt.PokemonId == newPokemonTypeDto.PokemonId && 
                                                         pt.TypeId == newPokemonTypeDto.TypeId);

            if(dbPokemonType == null)
            {
                await _context.TB_POKEMONTYPE.AddAsync(newPokemonTypeDto.Adapt<PokemonTypeEntity>());
                await _context.SaveChangesAsync();

                dbPokemonType = await _context.TB_POKEMONTYPE
                                    .FirstOrDefaultAsync(pt => pt.PokemonId == newPokemonTypeDto.PokemonId && 
                                                         pt.TypeId == newPokemonTypeDto.TypeId);
            }
        }

        public async Task<List<GetPokemonTypeDto>> GetPokemonTypeByPokemonIdDb(int pokemonId)
        {
            List<GetPokemonTypeDto> pokemonTypes = new List<GetPokemonTypeDto>();

            foreach (var types in _context.TB_POKEMONTYPE
                                            .Where(pt => pt.PokemonId == pokemonId))
            {
                pokemonTypes.Add(types.Adapt<GetPokemonTypeDto>());
            }

            return pokemonTypes;
        }

        public async Task DeletePokemonTypeByPokemonIdDb(int pokemonId)
        {
            foreach (var pokemonTypes in _context.TB_POKEMONTYPE
                                            .Where(pt => pt.PokemonId == pokemonId))
            {
                _context.TB_POKEMONTYPE.Remove(pokemonTypes.Adapt<PokemonTypeEntity>());
            }
            _context.SaveChanges(); 
        }
    }
}