namespace Pokedex.Domain.Entities
{
    public class PokemonEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
    }
}