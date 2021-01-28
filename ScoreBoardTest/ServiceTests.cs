using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScoreBoard;

namespace ScoreBoardTest
{
    [TestClass]
    public class ServiceTests
    {
        private Service _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new Service();
        }

        [TestMethod]
        public void Service_StartGameWithNullGame_ShouldThrowArgumentNullException()
        {
            //Arrange

            //Act
            Action act = () => _service.StartGame(null);

            //Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Service_StartGameWithEmptyHomeTeam_ShouldArgumentNullException()
        {
            //Arrange
            var game = new Game
            {
                HomeTeam = string.Empty,
                AwayTeam = "Canada"
            };

            //Act
            Action act = () => _service.StartGame(game);

            //Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Service_StartGameWithEmptyAwayTeam_ShouldArgumentNullException()
        {
            //Arrange
            var game = new Game
            {
                HomeTeam = "Mexico",
                AwayTeam = string.Empty
            };

            //Act
            Action act = () => _service.StartGame(game);

            //Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Service_StartGameWithExistingGame_ShouldThrowException()
        {
            //Arrange
            var game1 = new Game
            {
                HomeTeam = "Mexico",
                AwayTeam = "Canada"
            };

            var game2 = new Game
            {
                HomeTeam = "Mexico",
                AwayTeam = "Canada"
            };

            //Act
            _service.Games.Add($"{game1.HomeTeam} - {game1.AwayTeam}", game1);
            Action act = () => _service.StartGame(game2);

            //Assert
            act.Should().Throw<Exception>().WithMessage("Game already started!");
            _service.Games.Count.Should().Be(1);
            _service.Games.Values.First().HomeTeam.Should().Be("Mexico");
            _service.Games.Values.First().AwayTeam.Should().Be("Canada");
        }
    }
}
