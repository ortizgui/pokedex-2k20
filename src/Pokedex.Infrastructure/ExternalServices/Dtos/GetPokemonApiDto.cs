using System.Collections.Generic;

namespace Pokedex.Infrastructure.ExternalServices.Dtos
{
    public class GetPokemonApiDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public List<TypesGroupApiDto> Types { get; set; }
    }
}
