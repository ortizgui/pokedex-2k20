namespace Pokedex.Domain.Entities
{
    public class PokemonTypeEntity
    {
        public int PokemonId { get; set; }
        public PokemonEntity Pokemon { get; set; }
        public int TypeId { get; set; }
        public TypeEntity Type { get; set; }
    }
}