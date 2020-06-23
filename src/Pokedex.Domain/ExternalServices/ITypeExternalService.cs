using System.Threading.Tasks;
using Pokedex.Domain.Dtos.Type;

namespace Pokedex.Domain.ExternalServices
{
    public interface ITypeExternalService
    {
        Task<GetTypeDto> AddTypeDb(AddTypeDto type);
        Task<GetTypeDto> GetTypeByIdDb(int typeId);
        Task<GetTypeDto> GetTypeByNameDb(string typeName);
    }
}