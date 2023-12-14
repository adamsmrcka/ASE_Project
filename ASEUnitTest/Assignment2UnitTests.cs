using ASE_Project;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            Assert.AreEqual(varTestBeg, 80);
            Dictionaries.variables.TryGetValue("test2", out int varTestFinal);
            Assert.AreEqual(varTestBeg + 50, varTestFinal);
        }
    }
}
