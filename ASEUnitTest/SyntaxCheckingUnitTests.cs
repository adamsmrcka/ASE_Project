using ASE_Project;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASEUnitTest
{
    /// <summary>
    /// Test class testing functionality of Syntax Checking
    /// </summary>
    [TestClass]
    public class SyntaxCheckingUnitTests
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
        /// Valid Non-shape command Test - no changes, no errors test
        /// </summary>
        [TestMethod]
        public void testSyntaxChecking_ShouldParseValidNonShapeCommand_NoEffects()
        {
            // Arrange
            Canvas.fill = false;
            string[] lines = { "fiLL oN" };

            // Act
            parser.parseCommand(lines, false);

            // Assert
            Assert.IsTrue(parser.errors == 0);
            Assert.AreEqual(false, Canvas.fill);
            Canvas.fill = false;
        }

        /// <summary>
        /// Invalid Non-Shape command argument Test - errors found
        /// </summary>
        [TestMethod]
        public void testSyntaxChecking_ShouldThrowException_WhenIncorrectStringArgumentType()
        {
            // Arrange
            string[] lines = { "fiLL 20" };

            // Act
            parser.parseCommand(lines, false);

            //Assert
            Assert.IsTrue(parser.errors > 0);
        }

        /// <summary>
        /// Valid shape command Test - no changes, no errors
        /// </summary>
        [TestMethod]
        public void testSyntaxChecking_ShouldParseValidShapeCommand_NoEffects()
        {
            Parser.s = (Shape)commandFactory.getShape("triangle");
            // Arrange
            string[] lines = { "rectangle 10 20" };

            // Act
            parser.parseCommand(lines, false);

            // Assert
            Assert.IsTrue(parser.errors == 0);
            Assert.AreEqual("triangle", Parser.s.ToString().ToLower().Split('.').Last());
        }

        /// <summary>
        /// Invalid Shape command argument Test - errors found
        /// </summary>
        [TestMethod]
        public void testSyntaxChecking_ShouldThrowException_WhenIncorrectIntArgumentType()
        {
            // Arrange
            string[] lines = { "rectangle rectangle rectangle" }; // Rectangle expects ints

            // Act
            parser.parseCommand(lines, false);

            //Assert
            Assert.IsTrue(parser.errors > 0);
        }

        /// <summary>
        /// Invalid command Test - multiple commands - no changes; errors found
        /// </summary>
        [TestMethod]
        public void testSyntaxChecking_ShouldThrowException_WhenUnknownCommand_MultipleCommands_NoChange()
        {
            // Arrange
            Canvas.fill = false;
            string[] lines = { "fill on", "unknowncommand" };


            // Act
            parser.parseCommand(lines, false);

            //Assert
            Assert.IsTrue(parser.errors > 0);
            Assert.AreEqual(false, Canvas.fill);
            Canvas.fill = false;
        }

        /// <summary>
        /// Valid Shape and Non-Shape command Test - multiple commands - no changes, no errors
        /// </summary>
        [TestMethod]
        public void testSyntaxChecking_ShouldParseValidShapeandNonShapeCommand_MultipleCommands_NoChange()
        {
            // Arrange
            Parser.s = (Shape)commandFactory.getShape("triangle");
            Canvas.fill = false;
            string[] lines = { "fill on", "rectangle 10 20" };

            // Act
            parser.parseCommand(lines, false);

            // Assert
            Assert.IsTrue(parser.errors == 0);
            Assert.AreEqual("triangle", Parser.s.ToString().ToLower().Split('.').Last());
            Assert.AreEqual(false, Canvas.fill);
        }
    }
}
