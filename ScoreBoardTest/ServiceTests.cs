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
        private Game _game1;
        private Game _game2;

        [TestInitialize]
        public void Setup()
        {
            _service = new Service();

            _game1 = new Game {HomeTeam = "Mexico", AwayTeam = "Canada"};
            _game2 = new Game {HomeTeam = "Spain", AwayTeam = "Brazil"};
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
        public void Service_StartGameWithEmptyHomeTeam_ShouldThrowArgumentNullException()
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
        public void Service_StartGameWithEmptyAwayTeam_ShouldThrowArgumentNullException()
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
            var game11 = new Game { HomeTeam = "Mexico", AwayTeam = "Canada" };

            //Act
            _service.Games.Add($"{_game1.HomeTeam} - {_game1.AwayTeam}", _game1);
            Action act = () => _service.StartGame(game11);

            //Assert
            act.Should().Throw<Exception>().WithMessage("Game already started!");
            _service.Games.Count.Should().Be(1);
            _service.Games.Values.First().HomeTeam.Should().Be("Mexico");
            _service.Games.Values.First().AwayTeam.Should().Be("Canada");
        }

        [TestMethod]
        public void Service_StartGame_ShouldAddGame()
        {
            //Arrange
            _service.StartGame(_game1);

            //Act
            _service.StartGame(_game2);

            //Assert
            _service.Games.Count.Should().Be(2);
            _service.Games.Values.First().HomeTeam.Should().Be("Mexico");
            _service.Games.Values.First().AwayTeam.Should().Be("Canada");
            _service.Games.Values.First().HomeTeamScore.Should().Be(0);
            _service.Games.Values.First().AwayTeamScore.Should().Be(0);
            _service.Games.Values.First().TotalScore.Should().Be(0);
            _service.Games.Values.Last().HomeTeam.Should().Be("Spain");
            _service.Games.Values.Last().AwayTeam.Should().Be("Brazil");
            _service.Games.Values.Last().HomeTeamScore.Should().Be(0);
            _service.Games.Values.Last().AwayTeamScore.Should().Be(0);
            _service.Games.Values.Last().TotalScore.Should().Be(0);
        }

        [TestMethod]
        public void Service_FinishGameWithNonExistingGame_ShouldThrowException()
        {
            //Arrange
            const string key = "HomeTeam - AwayTeam";

            //Act
            Action act = () => _service.FinishGame(key);

            //Assert
            act.Should().Throw<Exception>().WithMessage("Game does not exist!");
        }

        [TestMethod]
        public void Service_FinishGameWithEmptyKey_ShouldThrowArgumentNullException()
        {
            //Arrange
            var key = string.Empty;

            //Act
            Action act = () => _service.FinishGame(key);

            //Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Service_FinishGame_ShouldRemoveGame()
        {
            //Arrange
            var key = $"{_game1.HomeTeam} - {_game1.AwayTeam}";
            _service.StartGame(_game1);
            _service.StartGame(_game2);

            //Act
            _service.FinishGame(key);

            //Assert
            _service.Games.Count.Should().Be(1);
            _service.Games.Values.First().HomeTeam.Should().Be("Spain");
            _service.Games.Values.First().AwayTeam.Should().Be("Brazil");
            _service.Games.Values.First().HomeTeamScore.Should().Be(0);
            _service.Games.Values.First().AwayTeamScore.Should().Be(0);
            _service.Games.Values.First().TotalScore.Should().Be(0);
        }

        [TestMethod]
        public void Service_UpdateGameWithEmptyKey_ShouldThrowArgumentNullException()
        {
            //Arrange
            var key = string.Empty;

            //Act
            Action act = () => _service.UpdateGame(key);

            //Assert
            act.Should().Throw<ArgumentNullException>();
        }

    }
}
