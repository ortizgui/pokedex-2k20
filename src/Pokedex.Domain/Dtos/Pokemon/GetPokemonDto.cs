namespace Pokedex.Domain.Dtos.Pokemon
{
    public class GetPokemonDto
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public int Height { get; set; }

        public int Weight { get; set; }
        //public List<string> Types { get; set; }
    }
}