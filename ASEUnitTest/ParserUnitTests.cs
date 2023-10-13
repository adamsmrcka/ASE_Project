using ASE_Project;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ASEUnitTest
{
    [TestClass]
    public class ParserUnitTests
    {
        private Parser parser;
        private Canvas canvas;
        PictureBox pictureBox = new PictureBox();

        Form1 form1 = new Form1();

        [TestInitialize]
        public void Initialize()
        {
            parser = Parser.getParser();
            canvas = new Canvas(pictureBox.CreateGraphics(), form1); // Create a dummy canvas for testing
            parser.setCanvas(canvas);
        }

        [TestMethod]
        public void TestParseCommand_ShouldThrowException_WhenNoCommandEntered()
        {
            // Arrange
            string[] lines = { "" }; // Empty command

            // Act & Assert
            Assert.ThrowsException<Exception>(() => parser.parseCommand(lines));
        }

        public void TestParseCommand_ShouldThrowException_WhenNoCommandEntered_MultipleCommands()
        {
            // Arrange
            string[] lines = { "fill on", "" };

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
        public void TestParseCommand_ShouldThrowException_WhenUnknownCommand_MultipleCommands()
        {
            // Arrange
            string[] lines = { "fill on", "unknowncommand" };

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
        public void TestParseCommand_ShouldThrowException_WhenIncorrectArgumentCount_MultipleCommands()
        {
            // Arrange
            string[] lines = { "fill on", "rectangle 1 2 3" };

            // Act & Assert
            Assert.ThrowsException<Exception>(() => parser.parseCommand(lines));
        }

        [TestMethod]
        public void TestParseCommand_ShouldThrowException_WhenIncorrectIntArgumentType()
        {
            // Arrange
            string[] lines = { "rectangle rectangle rectangle" }; // Rectangle expects ints

            // Act & Assert
            Assert.ThrowsException<Exception>(() => parser.parseCommand(lines));
        }

        [TestMethod]
        public void TestParseCommand_ShouldThrowException_WhenIncorrectIntArgumentType_MultipleCommands()
        {
            // Arrange
            string[] lines = { "fill on", "rectangle rectangle rectangle" };

            // Act & Assert
            Assert.ThrowsException<Exception>(() => parser.parseCommand(lines));
        }

        [TestMethod]
        public void TestParseCommand_ShouldThrowException_WhenIncorrectStringArgumentType()
        {
            // Arrange
            string[] lines = { "fill 20" }; // Rectangle expects ints

            // Act & Assert
            Assert.ThrowsException<Exception>(() => parser.parseCommand(lines));
        }

        [TestMethod]
        public void TestParseCommand_ShouldThrowException_WhenIncorrectStringArgumentType_MultipleCommands()
        {
            // Arrange
            string[] lines = { "fill on", "fill 20" };

            // Act & Assert
            Assert.ThrowsException<Exception>(() => parser.parseCommand(lines));
        }

        [TestMethod]
        public void TestParseCommand_ShouldParseValidShapeCommand()
        {
            // Arrange
            string[] lines = { "rectangle 10 20" };

            // Act
            parser.parseCommand(lines);

            // Assert
            Assert.AreEqual("rectangle", Parser.s.ToString().ToLower().Split('.').Last());
        }

        [TestMethod]
        public void TestParseCommand_ShouldParseValidShapeCommand_MultipleCommands()
        {
            // Arrange
            string[] lines = { "fill on", "rectangle 10 20" };

            // Act
            parser.parseCommand(lines);

            // Assert
            Assert.AreEqual("rectangle", Parser.s.ToString().ToLower().Split('.').Last());
            Assert.AreEqual(true, Canvas.fill);
        }

        [TestMethod]
        public void TestParseCommand_ShouldParseValidShapeCommand_Uppercase()
        {
            // Arrange
            string[] lines = { "RecTangle 10 20" };

            // Act
            parser.parseCommand(lines);

            // Assert
            Assert.AreEqual("rectangle", Parser.s.ToString().ToLower().Split('.').Last());
        }

        [TestMethod]
        public void TestParseCommand_ShouldParseValidNonShapeCommand()
        {
            // Arrange
            string[] lines = { "fill on" };

            // Act
            parser.parseCommand(lines);

            // Assert
            Assert.AreEqual(true, Canvas.fill);
        }

        [TestMethod]
        public void TestParseCommand_ShouldParseValidNonShapeCommand_MultipleCommands()
        {
            // Arrange
            string[] lines = { "rectangle 10 20", "fill on" };

            // Act
            parser.parseCommand(lines);

            // Assert
            Assert.AreEqual(true, Canvas.fill);
            Assert.AreEqual("rectangle", Parser.s.ToString().ToLower().Split('.').Last());
        }

        [TestMethod]
        public void TestParseCommand_ShouldParseValidNonShapeCommand_Uppercase()
        {
            // Arrange
            string[] lines = { "fiLL oN" };

            // Act
            parser.parseCommand(lines);

            // Assert
            Assert.AreEqual(true, Canvas.fill);
        }

        [TestMethod]
        public void TestIsShape_ShouldReturnTrue_WhenCommandIsShape()
        {
            // Arrange
            string shapeCommand = "circle";

            // Act
            bool isShape = parser.isShape(shapeCommand);

            // Assert
            Assert.IsTrue(isShape);
        }

        [TestMethod]
        public void TestIsShape_ShouldReturnFalse_WhenCommandIsNotShape()
        {
            // Arrange
            string nonShapeCommand = "pen";

            // Act
            bool isShape = parser.isShape(nonShapeCommand);

            // Assert
            Assert.IsFalse(isShape);
        }

        [TestMethod]
        public void TestIsShape_ShouldReturnFalse_WhenCommandIsEmpty()
        {
            // Arrange
            string emptyCommand = "";

            // Act
            bool isShape = parser.isShape(emptyCommand);

            // Assert
            Assert.IsFalse(isShape);
        }

        [TestMethod]
        public void TestIsShape_ShouldReturnFalse_WhenCommandIsUnknown()
        {
            // Arrange
            string unknownCommand = "unknown";

            // Act
            bool isShape = parser.isShape(unknownCommand);

            // Assert
            Assert.IsFalse(isShape);
        }
    }
}