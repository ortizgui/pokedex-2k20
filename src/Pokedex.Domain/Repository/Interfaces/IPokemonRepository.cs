using Pokedex.Domain.Entities;
using System.Threading.Tasks;

namespace Pokedex.Domain.Repository.Interfaces
{
    public interface IPokemonRepository
    {
        Task<ServiceResponse<PokemonEntity>> GetByNumber(PokemonEntity pokemon);
    }
}