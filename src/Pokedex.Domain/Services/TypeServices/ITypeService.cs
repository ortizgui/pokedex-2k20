using System.Collections.Generic;
using System.Threading.Tasks;
using Pokedex.Domain.Entities;

namespace Pokedex.Domain.Services.TypeServices
{
    public interface ITypeService
    {
         Task AddTypes(List<TypesEntity> newTypes, int pokemonId);
         Task<List<TypesEntity>> GetTypesByPokemonId(int pokemonId);
    }
}