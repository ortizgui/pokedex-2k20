using Amazon.DynamoDBv2.DataModel;

namespace Pokedex.Infrastructure.Repositories.Entities
{
    [DynamoDBTable("PokemonTable")]
    public class PokemonTable
    {
        public Pokemon Pokemon { get; set; }
    }
}