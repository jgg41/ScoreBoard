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
        private Game _game3;
        private Game _game4;
        private Game _game5;

        [TestInitialize]
        public void Setup()
        {
            _service = new Service();

            _game1 = new Game {HomeTeam = "Mexico", AwayTeam = "Canada"};
            _game2 = new Game {HomeTeam = "Spain", AwayTeam = "Brazil"};
            _game3 = new Game { HomeTeam = "Germany", AwayTeam = "France" };
            _game4 = new Game { HomeTeam = "Uruguay", AwayTeam = "Italy" };
            _game5 = new Game { HomeTeam = "Argentina", AwayTeam = "Australia" };
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
            Action act = () => _service.UpdateGame(key, 1, 1);

            //Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Service_UpdateGameWithInvalidHomeTeamScore_ShouldThrowArgumentOutOfRangeException()
        {
            //Arrange
            const string key = "someKey";

            //Act
            Action act = () => _service.UpdateGame(key, -1, 1);

            //Assert
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void Service_UpdateGameWithInvalidAwayTeamScore_ShouldThrowArgumentOutOfRangeException()
        {
            //Arrange
            const string key = "someKey";

            //Act
            Action act = () => _service.UpdateGame(key, 1, -1);

            //Assert
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [TestMethod]
        public void Service_UpdateGameWithNonExistingGame_ShouldThrowException()
        {
            //Arrange
            const string key = "someKey";

            //Act
            Action act = () => _service.UpdateGame(key, 1, 1);

            //Assert
            act.Should().Throw<Exception>().WithMessage("The given key 'someKey' was not present in the dictionary.");
        }

        [TestMethod]
        public void Service_UpdateGame_ShouldUpdateScores()
        {
            //Arrange
            var homeTeamScore = 1;
            var awayTeamScore = 2;
            var key = $"{_game1.HomeTeam} - {_game1.AwayTeam}";
            _service.StartGame(_game1);

            //Act
            _service.UpdateGame(key, homeTeamScore, awayTeamScore);

            //Assert
            _service.Games.Count.Should().Be(1);
            _service.Games.Values.First().HomeTeam.Should().Be("Mexico");
            _service.Games.Values.First().AwayTeam.Should().Be("Canada");
            _service.Games.Values.First().HomeTeamScore.Should().Be(1);
            _service.Games.Values.First().AwayTeamScore.Should().Be(2);
            _service.Games.Values.First().TotalScore.Should().Be(3);
        }

        [TestMethod]
        public void Service_GetScoreBoard_ShouldReturnOrderedScores()
        {
            //Arrange
            StartPlaying();

            //Act
            var result =  _service.GetScoreBoard();

            //Assert
            result.Count().Should().Be(5);

            result.First().Value.HomeTeam.Should().Be("Uruguay");
            result.First().Value.AwayTeam.Should().Be("Italy");
            result.First().Value.HomeTeamScore.Should().Be(6);
            result.First().Value.AwayTeamScore.Should().Be(6);
            result.First().Value.TotalScore.Should().Be(12);

            result.ElementAt(1).Value.HomeTeam.Should().Be("Spain");
            result.ElementAt(1).Value.AwayTeam.Should().Be("Brazil");
            result.ElementAt(1).Value.HomeTeamScore.Should().Be(10);
            result.ElementAt(1).Value.AwayTeamScore.Should().Be(2);
            result.ElementAt(1).Value.TotalScore.Should().Be(12);

            result.ElementAt(2).Value.HomeTeam.Should().Be("Mexico");
            result.ElementAt(2).Value.AwayTeam.Should().Be("Canada");
            result.ElementAt(2).Value.HomeTeamScore.Should().Be(0);
            result.ElementAt(2).Value.AwayTeamScore.Should().Be(5);
            result.ElementAt(2).Value.TotalScore.Should().Be(5);

            result.ElementAt(3).Value.HomeTeam.Should().Be("Argentina");
            result.ElementAt(3).Value.AwayTeam.Should().Be("Australia");
            result.ElementAt(3).Value.HomeTeamScore.Should().Be(3);
            result.ElementAt(3).Value.AwayTeamScore.Should().Be(1);
            result.ElementAt(3).Value.TotalScore.Should().Be(4);

            result.Last().Value.HomeTeam.Should().Be("Germany");
            result.Last().Value.AwayTeam.Should().Be("France");
            result.Last().Value.HomeTeamScore.Should().Be(2);
            result.Last().Value.AwayTeamScore.Should().Be(2);
            result.Last().Value.TotalScore.Should().Be(4);
        }

        private void StartPlaying()
        {
            var key1 = $"{_game1.HomeTeam} - {_game1.AwayTeam}";
            var key2 = $"{_game2.HomeTeam} - {_game2.AwayTeam}";
            var key3 = $"{_game3.HomeTeam} - {_game3.AwayTeam}";
            var key4 = $"{_game4.HomeTeam} - {_game4.AwayTeam}";
            var key5 = $"{_game5.HomeTeam} - {_game5.AwayTeam}";

            _service.StartGame(_game1);
            _service.StartGame(_game2);
            _service.StartGame(_game3);
            _service.StartGame(_game4);
            _service.StartGame(_game5);

            _service.UpdateGame(key1, 0, 5);
            _service.UpdateGame(key2, 10, 2);
            _service.UpdateGame(key3, 2, 2);
            _service.UpdateGame(key4, 6, 6);
            _service.UpdateGame(key5, 3, 1);
        }
    }
}
