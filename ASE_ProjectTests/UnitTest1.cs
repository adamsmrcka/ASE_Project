using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;

namespace ASE_Project.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private Parser parser;

        [TestInitialize]
        public void Initialize()
        {
            parser = Parser.getParser();
        }

        [TestMethod]
        public void TestParseCommand_ShouldThrowException_WhenNoCommandEntered()
        {
            // Arrange
            string[] lines = { "" }; // Empty command

            // Act & Assert
            Assert.ThrowsException<Exception>(() => parser.parseCommand(lines));
        }

        [TestMethod]
        public void TestParseCommand_ShouldThrowException_WhenUnknownCommand()
        {
            // Arrange
            string[] lines = { "unknowncommand" };

            // Act & Assert
            Assert.ThrowsException<Exception>(() => parser.parseCommand(lines));
        }

        [TestMethod]
        public void TestParseCommand_ShouldThrowException_WhenIncorrectArgumentCount()
        {
            // Arrange
            string[] lines = { "rectangle 1 2 3" }; // Rectangle expects 2 arguments

            // Act & Assert
            Assert.ThrowsException<Exception>(() => parser.parseCommand(lines));
        }

        [TestMethod]
        public void TestParseCommand_ShouldParseValidShapeCommand()
        {
            // Arrange
            string[] lines = { "rectangle 10 20 30 40" };

            // Act
            parser.parseCommand(lines);

            // Assert - You can add assertions here based on expected canvas state
            Assert.AreEqual(Canvas.penColour, Color.Black);
            Assert.AreEqual(Canvas.fill, false);
            // Add more assertions as needed.
        }

        [TestMethod]
        public void TestParseCommand_ShouldParseValidNonShapeCommand()
        {
            // Arrange
            string[] lines = { "pen #FF0000" };

            // Act
            parser.parseCommand(lines);

            // Assert - You can add assertions here based on expected canvas state
            Assert.AreEqual(Canvas.penColour, Color.Red);
            // Add more assertions as needed.
        }
    }
}
