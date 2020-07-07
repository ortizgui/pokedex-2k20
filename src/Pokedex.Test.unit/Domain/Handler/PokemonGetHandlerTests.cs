using System;
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

        [Fact]
        public async void Should_Find_Return_Is_Valid()
        {
            _mockPokemonRepository = new Mock<IPokemonRepository>();

            _mockPokemonRepository.Setup(p => p.GetPokemon(It.IsAny<EnumPokemonSelectOptions>(),
                It.IsAny<string>())).Returns(Task.FromResult(new GetPokemonRepositoryDto()));

            var _handler = new PokemonGetHandler(_mockPokemonRepository.Object);

            //Act
            var response = await _handler.Handle(new PokemonGetCommand(), new CancellationToken());

            //Assert
            _mockPokemonRepository.VerifyAll();
        }
        
        [Fact]
        public async void Should_Return_Try_Again_Later()
        {
            //Arrange
            _mockPokemonRepository = new Mock<IPokemonRepository>();

            _mockPokemonRepository.Setup(p => p.GetPokemon(It.IsAny<EnumPokemonSelectOptions>(),
                It.IsAny<string>())).Throws(new Exception());

            var _handler = new PokemonGetHandler(_mockPokemonRepository.Object);

            //Act
            var response = await _handler.Handle(new PokemonGetCommand(), new CancellationToken());

            //Assert
            Assert.Equal("Please try again later.", response.Message);
        }
    }
}