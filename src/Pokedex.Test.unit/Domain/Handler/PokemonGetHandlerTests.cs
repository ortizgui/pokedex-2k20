using System.Threading;
using System.Threading.Tasks;
using Moq;
using Pokedex.Domain.Commands;
using Pokedex.Domain.Dtos.Pokemon;
using Pokedex.Domain.Dtos.Repository;
using Pokedex.Domain.Entities;
using Pokedex.Domain.Handler;
using Pokedex.Domain.Repositories;
using Xunit;

namespace Pokedex.Test.unit.Domain.Handler
{
    public class PokemonGetHandlerTests
    {
        private Mock<IPokemonRepository> _mockPokemonRepository;
        private PokemonGetHandler _handler;

        [Fact]
        public async void GetPokemon_FindOne_ReturnIsTrue()
        {
            //Arrange
            var handleExpectedResponse = new ServiceResponse<GetPokemonDto>
            {
                Data = new GetPokemonDto
                {
                    Name = "bulbasaur",
                    Number = 1,
                    Order = 1,
                    Height = 7,
                    Weight = 69
                },
                Success = true,
                Message = null
            };

            var getPokemonRepositoryDto = new GetPokemonRepositoryDto
            {
                Name = "bulbasaur",
                Number = 1,
                Order = 1,
                Height = 7,
                Weight = 69
            };

            _mockPokemonRepository = new Mock<IPokemonRepository>();

            _mockPokemonRepository.Setup(p => p.GetPokemon(It.IsAny<EnumPokemonSelectOptions>(),
                It.IsAny<string>())).Returns(Task.FromResult(getPokemonRepositoryDto));

            _handler = new PokemonGetHandler(_mockPokemonRepository.Object);

            //Act
            var response = await _handler.Handle(It.IsAny<PokemonGetCommand>(), CancellationToken.None);

            //Assert
            Assert.True(handleExpectedResponse.Equals(response));
        }
    }
}