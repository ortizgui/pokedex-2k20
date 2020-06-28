using System;
using Amazon.DynamoDBv2.DataModel;

namespace Pokedex.Infrastructure.Repositories.Entities
{
    public class Pokemon
    {
        [DynamoDBHashKey]
        public int Id { get; set; }
        [DynamoDBProperty]
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
        public DateTime DateCreated { get; set; }
        [DynamoDBProperty]
        public DateTime? DateUpdated { get; set; } = null;
    }
}