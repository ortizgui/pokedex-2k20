using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Mapster;
using Pokedex.Domain.Dtos.Pokemon;
using Pokedex.Domain.Repositories;

namespace Pokedex.Infrastructure.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly IAmazonDynamoDB _amazonDynamoDb;
        private readonly AmazonDynamoDBClient _client;
        private readonly string _tableName;
        public PokemonRepository(IAmazonDynamoDB amazonDynamoDb)
        {
            _amazonDynamoDb = amazonDynamoDb;
            _client = new AmazonDynamoDBClient();
            _tableName = "PokemonTable";
        }
        public async Task DeletePokemon(int pokemonNumber)
        {
            var request = new DeleteItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue> { { "Number", new AttributeValue { N = pokemonNumber.ToString() } } }
            };

            await _amazonDynamoDb.DeleteItemAsync(request);
        }

        public async Task<GetPokemonDto> GetPokemonByNumber(int pokemonNumber)
        {
            var request = new GetItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue> { { "Number", new AttributeValue { N = pokemonNumber.ToString() } } }
            };

            var response = await _amazonDynamoDb.GetItemAsync(request);
            
            var pokemonDto = new GetPokemonDto()
            {
               Number = Convert.ToInt16(response.Item["Number"].N),
               Name = response.Item["Name"].S,
               Order = Convert.ToInt16(response.Item["Order"].N),
               Height = Convert.ToInt16(response.Item["Height"].N),
               Weight = Convert.ToInt16(response.Item["Weight"].N)
            };
            
            return pokemonDto;
        }

        public async Task<GetPokemonDto> GetPokemonByName(string pokemonName)
        {
            var request = new GetItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue> { { "Name", new AttributeValue { S = pokemonName.ToString() } } }
            };

            var response = await _amazonDynamoDb.GetItemAsync(request);
            
            var pokemonDto = new GetPokemonDto()
            {
                Number = Convert.ToInt16(response.Item["Number"].N),
                Name = response.Item["Name"].S,
                Order = Convert.ToInt16(response.Item["Order"].N),
                Height = Convert.ToInt16(response.Item["Height"].N),
                Weight = Convert.ToInt16(response.Item["Weight"].N)
            };
            
            return pokemonDto;
        }

        public async Task InsertPokemonAsync(AddPokemonDto pokemonDto)
        {
            var request = new PutItemRequest
            {
                TableName = _tableName,
                Item = new Dictionary<string, AttributeValue>
                {
                    { "Number", new AttributeValue { N = pokemonDto.Number.ToString() }},
                    { "Name", new AttributeValue { S = pokemonDto.Name }},
                    { "Order", new AttributeValue { N = pokemonDto.Order.ToString() }},
                    { "Height", new AttributeValue { N = pokemonDto.Height.ToString() }},
                    { "Weight", new AttributeValue { N = pokemonDto.Weight.ToString() }},
                    { "DateCreated", new AttributeValue { S = DateTime.Now.ToString() }},
                }
            };

            await _amazonDynamoDb.PutItemAsync(request);
        }
    }
}