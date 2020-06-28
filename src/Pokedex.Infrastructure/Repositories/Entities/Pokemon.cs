using System;
using Amazon.DynamoDBv2.DataModel;

namespace Pokedex.Infrastructure.Repositories.Entities
{
    public class Pokemon
    {
        [DynamoDBHashKey]
        public int Id { get; set; }
        [DynamoDBGlobalSecondaryIndexHashKey]
        public int Number { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; } = null;
    }
}