using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Mapster;
using Pokedex.Domain.Dtos.Pokemon;
using Pokedex.Domain.Repositories;
using Pokedex.Infrastructure.Repositories.Entities;

namespace Pokedex.Infrastructure.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly IAmazonDynamoDB _amazonDynamoDb;
        private readonly string _tableName;
        public PokemonRepository(IAmazonDynamoDB amazonDynamoDb)
        {
            _amazonDynamoDb = amazonDynamoDb;
            _tableName = "PokemonTable";
        }
        public async Task DeletePokemon(int pokemonNumber)
        {
            var request = new DeleteItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue> { { "Number", new AttributeValue { N = pokemonNumber.ToString() } } }
            };

            var response = await _amazonDynamoDb.DeleteItemAsync(request);
        }

        public async Task<GetPokemonDto> GetPokemonByNumber(int pokemonId)
        {
            var request = new GetItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue> { { "Number", new AttributeValue { N = pokemonId.ToString() } } }
            };

            //var response = await _amazonDynamoDb.GetItemAsync(request);
            var response = await _amazonDynamoDb.GetItemAsync(request);

            if (!response.IsItemSet)
                return new GetPokemonDto();

            return response.Adapt<GetPokemonDto>();
        }

        public async Task<GetPokemonDto> GetPokemonByName(string pokemonName)
        {
            var request = new GetItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue> { { "Name", new AttributeValue { S = pokemonName.ToString() } } }
            };

            //var response = await _amazonDynamoDb.GetItemAsync(request);
            var response = await _amazonDynamoDb.GetItemAsync(request);

            if (!response.IsItemSet)
                return new GetPokemonDto();

            return response.Adapt<GetPokemonDto>();
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

        public async Task UpdatePokemon(UpdatePokemonDto pokemon)
        {
            throw new System.NotImplementedException();
        }
    }
}