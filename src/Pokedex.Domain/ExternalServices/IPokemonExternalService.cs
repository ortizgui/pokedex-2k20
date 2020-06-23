﻿using Pokedex.Domain.Dtos.Pokemon;
using Pokedex.Domain.Dtos.PokemonType;
using Pokedex.Domain.Dtos.Type;
using Pokedex.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Domain.ExternalServices
{
    public interface IPokemonExternalService
    {
        Task<GetPokemonDto> GetPokemonByNameApi(string pokemonName);
        Task<GetPokemonDto> GetPokemonByNumberApi(int pokemonNumber);
        Task<GetPokemonDto> AddPokemonDb(AddPokemonDto pokemon);
        Task<GetPokemonDto> GetPokemonByNameDb(string pokemonName);
        Task<GetPokemonDto> GetPokemonByNumberDb(int pokemonNumber);
    }
}
