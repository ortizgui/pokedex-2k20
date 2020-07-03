using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Pokedex.Domain.Dtos.Pokemon;
using Pokedex.Domain.Dtos.Repository;
using Pokedex.Domain.Entities;
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

        public async Task<GetPokemonDto> GetPokemon(EnumPokemonSelectOptions selectOption, string pokemonIdentity)
        {
            var request = new GetItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue> { { selectOption.ToString(), new AttributeValue { N = pokemonIdentity.ToString() } } }
            };

            var response = await _amazonDynamoDb.GetItemAsync(request);
            
            if (!response.IsItemSet)
                return new GetPokemonDto();
            
            var pokemonDto = new GetPokemonDto()
            {
               Number = Convert.ToInt16(response.Item["Number"].N),
               Name = response.Item["Name"].S,
               Order = Convert.ToInt16(response.Item["Order"].N),
               Height = Convert.ToInt16(response.Item["Height"].N),
               Weight = Convert.ToInt16(response.Item["Weight"].N),
            };
            
            return pokemonDto;
        }

        public async Task SavePokemon(AddPokemonRepositoryDto pokemonRepositoryDto)
        {
            var request = new PutItemRequest
            {
                TableName = _tableName,
                Item = new Dictionary<string, AttributeValue>
                {
                    { "Number", new AttributeValue { N = pokemonRepositoryDto.Number.ToString() }},
                    { "Name", new AttributeValue { S = pokemonRepositoryDto.Name }},
                    { "Order", new AttributeValue { N = pokemonRepositoryDto.Order.ToString() }},
                    { "Height", new AttributeValue { N = pokemonRepositoryDto.Height.ToString() }},
                    { "Weight", new AttributeValue { N = pokemonRepositoryDto.Weight.ToString() }},
                    { "DateCreated", new AttributeValue { S = pokemonRepositoryDto.DateCreated.ToString() }},
                }
            };

            await _amazonDynamoDb.PutItemAsync(request);
        }
    }
}