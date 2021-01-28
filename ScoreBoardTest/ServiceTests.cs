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
    }
}
