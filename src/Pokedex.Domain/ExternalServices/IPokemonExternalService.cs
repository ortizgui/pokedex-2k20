using Pokedex.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Domain.ExternalServices
{
    public interface IPokemonExternalService
    {
        Task<ServiceResponse<PokemonEntity>> GetPokemonByNumber(int pokemonNumber);
        Task<ServiceResponse<PokemonEntity>> GetPokemonByName(string pokemonName);
    }
}
