using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace Pokedex.Domain.Entities
{
    [DynamoDBTable("PokemonTable")]
    public class PokemonTable
    {
        [DynamoDBHashKey]
        public int Number { get; set; }
        [DynamoDBProperty]
        public string Name { get; set; }
        [DynamoDBProperty]
        public int Order { get; set; }
        [DynamoDBProperty]
        public int Height { get; set; }
        [DynamoDBProperty]
        public int Weight { get; set; }
        [DynamoDBProperty]
        public List<string> Types { get; set; }
        [DynamoDBProperty]
        public DateTime DateCreated { get; set; }
        [DynamoDBProperty]
        public DateTime? DateUpdated { get; set; } = null;
    }
}