using System.Collections.Generic;
using Pokedex.Domain.Entities;

namespace Pokedex.Domain.Dtos.Type
{
    public class AddTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}