using ASE_Project;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;

namespace ASEUnitTest
{
    /// <summary>
    /// Test class testing functionality of Assignment 2 functionality
    /// </summary>
    [TestClass]
    public class Assignment2UnitTests
    {
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
        }

        [TestMethod]
        public void testVariables_ShouldDraw_number()
        {
            // Arrange
            string[] lines = { "test = 20" };

            // Act
            parser.parseCommand(lines, true);

            // Assert
            Dictionaries.variables.TryGetValue("test", out int varTest);
            Assert.AreEqual(20, varTest);
        }

        [TestMethod]
        public void testVariables_ShouldDraw_variable()
        {
            // Arrange
            string[] lines = { "test = 20", "test2 = test" };

            // Act
            parser.parseCommand(lines, true);

            // Assert
            Dictionaries.variables.TryGetValue("test", out int varTestBeg);
            Assert.AreEqual(varTestBeg, 20);
            Dictionaries.variables.TryGetValue("test2", out int varTestFinal);
            Assert.AreEqual(varTestFinal, varTestBeg);
        }

        [TestMethod]
        public void testVariables_ShouldDraw_update()
        {
            // Arrange
            string[] lines = { "test = 20", "test = 80" };

            // Act
            parser.parseCommand(lines, true);

            // Assert
            Dictionaries.variables.TryGetValue("test", out int varTestFinal);
            Assert.AreEqual(80, varTestFinal);
        }

        [TestMethod]
        public void testVariables_ShouldDraw_complex()
        {
            // Arrange
            string[] lines = { "test = 80", "test2 = test + 50" };

            // Act
            parser.parseCommand(lines, true);

            // Assert
            Dictionaries.variables.TryGetValue("test", out int varTestBeg);
            Assert.AreEqual(80, varTestBeg);
            Dictionaries.variables.TryGetValue("test2", out int varTestFinal);
            Assert.AreEqual(varTestBeg + 50, varTestFinal);
        }

        [TestMethod]
        public void testVariables_ShouldThrowException_complex()
        {
            // Arrange
            Dictionaries.variables.Clear();
            string[] lines = { "test2 = test + 50" };

            // Act
            parser.parseCommand(lines, true);

            // Assert
            Assert.IsTrue(parser.errors > 0);
        }

        [TestMethod]
        public void testVariables_SyntaxChecking_NoChange()
        {
            // Arrange
            Dictionaries.variables.Clear();
            string[] lines = { "test = 80" };

            // Act
            parser.parseCommand(lines, false);

            // Assert

            Assert.AreEqual(false, Dictionaries.variables.TryGetValue("test", out int varTestBeg));
        }

        [TestMethod]
        public void testLoops_ShouldDraw_Valid()
        {
            // Arrange
            string[] lines = { "test = 50", "while test < 80", "test = test + 5", "endloop"};

            // Act
            parser.parseCommand(lines, true);

            // Assert
            Dictionaries.variables.TryGetValue("test", out int varTestBeg);
            Assert.AreEqual(80, varTestBeg);
        }

        [TestMethod]
        public void testLoops_ShouldNotDraw_Invalid()
        {
            // Arrange
            string[] lines = { "test = 50", "while test < 40", "test = test + 5", "endloop" };

            // Act
            parser.parseCommand(lines, true);

            // Assert
            Dictionaries.variables.TryGetValue("test", out int varTestBeg);
            Assert.AreEqual(50, varTestBeg);
        }

        [TestMethod]
        public void testLoops_ShouldThrowException_InvalidCommand()
        {
            // Arrange
            string[] lines = { "test = 50", "while test", "test = test + 5", "endloop" };

            // Act
            parser.parseCommand(lines, true);

            // Assert
            Assert.IsTrue(parser.errors > 0);
        }

        [TestMethod]
        public void testLoops_ShouldThrowException_InvalidDeclaration()
        {
            // Arrange
            string[] lines = { "test = 50", "while test", "test = test + 5" };

            // Act
            parser.parseCommand(lines, true);

            // Assert
            Assert.IsTrue(parser.errors > 0);
        }

        [TestMethod]
        public void testLoops_SyntaxChecking_NoChange()
        {
            // Arrange
            Dictionaries.variables.Clear();
            string[] lines = { "test = 50", "while test < 40", "test = test + 5", "endloop" };

            // Act
            parser.parseCommand(lines, false);

            // Assert
            Assert.AreEqual(false, Dictionaries.variables.TryGetValue("test", out int varTestBeg));
        }

        [TestMethod]
        public void testIf_ShouldDraw_Valid()
        {
            // Arrange
            string[] lines = { "test = 50", "if test < 80", "test = test + 5", "endif" };

            // Act
            parser.parseCommand(lines, true);

            // Assert
            Dictionaries.variables.TryGetValue("test", out int varTestBeg);
            Assert.AreEqual(55, varTestBeg);
        }

        [TestMethod]
        public void testIf_ShouldDraw_Invalid()
        {
            // Arrange
            string[] lines = { "test = 50", "if test > 80", "test = test + 5", "endif" };

            // Act
            parser.parseCommand(lines, true);

            // Assert
            Dictionaries.variables.TryGetValue("test", out int varTestBeg);
            Assert.AreEqual(50, varTestBeg);
        }

        [TestMethod]
        public void testIfs_ShouldThrowException_InvalidCommand()
        {
            // Arrange
            string[] lines = { "test = 50", "if test", "test = test + 5", "endif" };

            // Act
            parser.parseCommand(lines, true);

            // Assert
            Assert.IsTrue(parser.errors > 0);
        }

        [TestMethod]
        public void testIfs_ShouldThrowException_InvalidDeclaration()
        {
            // Arrange
            string[] lines = { "test = 50", "if test", "test = test + 5" };

            // Act
            parser.parseCommand(lines, true);

            // Assert
            Assert.IsTrue(parser.errors > 0);
        }

        [TestMethod]
        public void testMethods_ShouldDraw_Valid()
        {
            // Arrange
            Dictionaries.methods.Clear();
            Dictionaries.methodLines.Clear();
            Dictionaries.variables.Clear();
            string[] lines = { "tests = 50", "method unittest (units)", "units = 150" , "endmethod", "method unittest (tests)" };

            // Act
            parser.parseCommand(lines, true);

            // Assert
            Dictionaries.variables.TryGetValue("tests", out int varTestBeg);
            Assert.AreEqual(150, varTestBeg);
        }

        [TestMethod]
        public void testMethods_ShouldThrowException_InvalidCommand()
        {
            // Arrange
            Dictionaries.methods.Clear();
            Dictionaries.methodLines.Clear();
            Dictionaries.variables.Clear();
            string[] lines = { "method unittest (units)", "circle units", "endmethod", "method unittest (tests)" };

            // Act
            parser.parseCommand(lines, true);

            // Assert
            Assert.IsTrue(parser.errors > 0);
        }

        [TestMethod]
        public void testMethods_ShouldThrowException_InvalidDeclaration()
        {
            // Arrange
            Dictionaries.methods.Clear();
            Dictionaries.methodLines.Clear();
            Dictionaries.variables.Clear();
            string[] lines = { "method unittest (units)", "circle units" };

            // Act & Assert
            Assert.ThrowsException<Exception>(() => parser.parseCommand(lines, true));
        }

        [TestMethod]
        public void testMethods_SyntaxChecking_Valid()
        {
            // Arrange
            Dictionaries.methods.Clear();
            Dictionaries.methodLines.Clear();
            Dictionaries.variables.Clear();
            string[] lines = { "tests = 50", "method unittest (units)", "units = 150", "endmethod", "method unittest (tests)" };

            // Act
            parser.parseCommand(lines, false);

            // Assert
            Assert.IsFalse(Dictionaries.variables.TryGetValue("tests", out _));
        }
    }
}
