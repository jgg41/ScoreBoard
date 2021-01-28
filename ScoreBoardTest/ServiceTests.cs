using System;
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
    }
}
