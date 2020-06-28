using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
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
        public void DeletePokemon(int pokemonId)
        {
            throw new System.NotImplementedException();
        }

        public GetPokemonDto GetPokemonByID(int pokemonId)
        {
            throw new System.NotImplementedException();
        }

        public GetPokemonDto GetPokemonByName(string pokemonName)
        {
            throw new System.NotImplementedException();
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

        public void UpdatePokemon(UpdatePokemonDto pokemon)
        {
            throw new System.NotImplementedException();
        }
    }
}