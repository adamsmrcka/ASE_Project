using ASE_Project;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ASEUnitTest
{
    /// <summary>
    /// Test class testing functionality of Parser Class
    /// </summary>
    [TestClass]
    public class ParserUnitTests
    {
        ShapeFactory commandFactory;
        private Parser parser;
        private Canvas canvas;
        PictureBox pictureBox = new PictureBox();

        Form1 form1 = new Form1();

        /// <summary>
        /// Sets Objects used during tests
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            parser = Parser.getParser();
            canvas = new Canvas(pictureBox.CreateGraphics(), form1); // Create a dummy canvas for testing
            parser.setCanvas(canvas);
            commandFactory = ShapeFactory.getShapeFactory();
        }

        /// <summary>
        /// Command should parse when second line is empty - multiple commands
        /// </summary>
        [TestMethod]
        public void testParseCommand_ShouldParse_WhenEmptyString_MultipleCommands()
        {
            // Arrange
            string[] lines = { "fill on", "" };
            Canvas.fill = false;

            // Act
            parser.parseCommand(lines, true);

            //Assert
            Assert.IsTrue(parser.errors == 0);
            Assert.AreEqual(true, Canvas.fill);
            Canvas.fill = false;
        }

        /// <summary>
        /// Negative test unknown command
        /// </summary>
        [TestMethod]
        public void testParseCommand_ShouldThrowException_WhenUnknownCommand()
        {
            // Arrange
            string[] lines = { "unknowncommand" };

            // Act
            parser.parseCommand(lines, true);

            //Assert
            Assert.IsTrue(parser.errors > 0);
        }

        /// <summary>
        /// Negative test unknown command - multiple commands
        /// </summary>
        [TestMethod]
        public void testParseCommand_ShouldThrowException_WhenUnknownCommand_MultipleCommands()
        {
            // Arrange
            string[] lines = { "fill on", "unknowncommand" };
            Canvas.fill = false;

            // Act
            parser.parseCommand(lines, true);

            //Assert
            Assert.IsTrue(parser.errors > 0);
            Assert.AreEqual(true, Canvas.fill);
            Canvas.fill = false;
        }

        /// <summary>
        /// Negative test incorect number of parameters
        /// </summary>
        [TestMethod]
        public void testParseCommand_ShouldThrowException_WhenIncorrectArgumentCount()
        {
            // Arrange
            string[] lines = { "rectangle 1 2 3" }; // Rectangle expects 2 arguments

            // Act
            parser.parseCommand(lines, true);

            //Assert
            Assert.IsTrue(parser.errors > 0);
        }

        /// <summary>
        /// Negative test incorect number of parameters - multiple commands
        /// </summary>
        [TestMethod]
        public void testParseCommand_ShouldThrowException_WhenIncorrectArgumentCount_MultipleCommands()
        {
            // Arrange
            string[] lines = { "fill on", "rectangle 1 2 3" };
            Canvas.fill = false;

            // Act
            parser.parseCommand(lines, true);

            //Assert
            Assert.IsTrue(parser.errors > 0);
            Assert.AreEqual(true, Canvas.fill);
            Canvas.fill = false;
        }

        /// <summary>
        /// Negative test incorect parameters types (Int)
        /// </summary>
        [TestMethod]
        public void testParseCommand_ShouldThrowException_WhenIncorrectIntArgumentType()
        {
            // Arrange
            string[] lines = { "rectangle rectangle rectangle" }; // Rectangle expects ints

            // Act
            parser.parseCommand(lines, true);

            //Assert
            Assert.IsTrue(parser.errors > 0);
        }

        /// <summary>
        /// Negative test incorect parameters types (Int) - multiple commands
        /// </summary>
        [TestMethod]
        public void testParseCommand_ShouldThrowException_WhenIncorrectIntArgumentType_MultipleCommands()
        {
            // Arrange
            string[] lines = { "fill on", "rectangle rectangle rectangle" }; // Rectangle expects ints
            Canvas.fill = false;

            // Act
            parser.parseCommand(lines, true);

            //Assert
            Assert.IsTrue(parser.errors > 0);
            Assert.AreEqual(true, Canvas.fill);
            Canvas.fill = false;
        }

        /// <summary>
        /// Negative test incorect parameters types (String)
        /// </summary>
        [TestMethod]
        public void testParseCommand_ShouldThrowException_WhenIncorrectStringArgumentType()
        {
            // Arrange
            string[] lines = { "fill 20" }; // fill expects string

            // Act
            parser.parseCommand(lines, true);

            //Assert
            Assert.IsTrue(parser.errors > 0);
        }

        /// <summary>
        /// Negative test incorect parameters types (String) - multiple commands
        /// </summary>
        [TestMethod]
        public void testParseCommand_ShouldThrowException_WhenIncorrectStringArgumentType_MultipleCommands()
        {
            // Arrange
            string[] lines = { "fill on", "fill 20" }; // fill expects string

            // Act
            parser.parseCommand(lines, true);

            //Assert
            Assert.IsTrue(parser.errors > 0);
        }

        /// <summary>
        /// Command should parse when valid shape is entered
        /// </summary>
        [TestMethod]
        public void testParseCommand_ShouldParseValidShapeCommand()
        {
            // Arrange
            string[] lines = { "rectangle 10 20" };

            // Act
            parser.parseCommand(lines, true);

            // Assert
            Assert.AreEqual("rectangle", Parser.s.ToString().ToLower().Split('.').Last());
        }

        /// <summary>
        /// Command should parse when valid shape is entered - multiple commands
        /// </summary>
        [TestMethod]
        public void testParseCommand_ShouldParseValidShapeCommand_MultipleCommands()
        {
            // Arrange
            string[] lines = { "fill on", "rectangle 10 20" };
            Canvas.fill = false;

            // Act
            parser.parseCommand(lines, true);

            // Assert
            Assert.AreEqual("rectangle", Parser.s.ToString().ToLower().Split('.').Last());
            Assert.AreEqual(true, Canvas.fill);
            Canvas.fill = false;
        }

        /// <summary>
        /// Command should parse when valid shape is entered - Random capital letters
        /// </summary>
        [TestMethod]
        public void testParseCommand_ShouldParseValidShapeCommand_Uppercase()
        {
            // Arrange
            string[] lines = { "RecTangle 10 20" };

            // Act
            parser.parseCommand(lines, true);

            // Assert
            Assert.AreEqual("rectangle", Parser.s.ToString().ToLower().Split('.').Last());
        }

        /// <summary>
        /// Command should parse when valid non-shape command is entered
        /// </summary>
        [TestMethod]
        public void testParseCommand_ShouldParseValidNonShapeCommand()
        {
            // Arrange
            string[] lines = { "fill on" };
            Canvas.fill = false;

            // Act
            parser.parseCommand(lines, true);

            // Assert
            Assert.AreEqual(true, Canvas.fill);
            Canvas.fill = false;
        }

        /// <summary>
        /// Command should parse when valid non-shape command is entered - multiple commands
        /// </summary>
        [TestMethod]
        public void testParseCommand_ShouldParseValidNonShapeCommand_MultipleCommands()
        {
            // Arrange
            string[] lines = { "rectangle 10 20", "fill on" };
            Canvas.fill = false;

            // Act
            parser.parseCommand(lines, true);

            // Assert
            Assert.AreEqual("rectangle", Parser.s.ToString().ToLower().Split('.').Last());
            Assert.AreEqual(true, Canvas.fill);
            Canvas.fill = false;
        }

        /// <summary>
        /// Command should parse when valid non-shape command is entered - random capital letters
        /// </summary>
        [TestMethod]
        public void testParseCommand_ShouldParseValidNonShapeCommand_Uppercase()
        {
            // Arrange
            string[] lines = { "fiLL oN" };
            Canvas.fill = false;

            // Act
            parser.parseCommand(lines, true );

            // Assert
            Assert.AreEqual(true, Canvas.fill);
            Canvas.fill = false;
        }

        /// <summary>
        /// When a valid shape is entered, a True boolean is returned
        /// </summary>
        [TestMethod]
        public void testIsShape_ShouldReturnTrue_WhenCommandIsShape()
        {
            // Arrange
            string shapeCommand = "circle";

            // Act
            bool isShape = parser.isShape(shapeCommand);

            // Assert
            Assert.IsTrue(isShape);
        }

        /// <summary>
        /// When non-shape command is entered, a False boolean is returned
        /// </summary>
        [TestMethod]
        public void testIsShape_ShouldReturnFalse_WhenCommandIsNotShape()
        {
            // Arrange
            string nonShapeCommand = "pen";

            // Act
            bool isShape = parser.isShape(nonShapeCommand);

            // Assert
            Assert.IsFalse(isShape);
        }
        /// <summary>
        /// When a No shape is entered, a False boolean is returned
        /// </summary>
        [TestMethod]
        public void testIsShape_ShouldReturnFalse_WhenCommandIsEmpty()
        {
            // Arrange
            string emptyCommand = "";

            // Act
            bool isShape = parser.isShape(emptyCommand);

            // Assert
            Assert.IsFalse(isShape);
        }

        /// <summary>
        /// When Invalid shape command is entered, a False boolean is returned
        /// </summary>
        [TestMethod]
        public void testIsShape_ShouldReturnFalse_WhenCommandIsUnknown()
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