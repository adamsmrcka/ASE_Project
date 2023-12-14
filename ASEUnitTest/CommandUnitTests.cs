using ASE_Project;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Windows.Forms;

namespace ASEUnitTest
{
    /// <summary>
    /// Test class testing functionality of Commands
    /// </summary>
    [TestClass]
    public class CommandUnitTests
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

        /// <summary>
        /// Fill on command should be parsed
        /// </summary>
        [TestMethod]
        public void testCommands_ShouldDraw_FillOn()
        {
            Canvas.fill = false;
            // Arrange
            string[] lines = { "fill on" };

            // Act
            parser.parseCommand(lines, true);

            // Assert
            Assert.AreEqual(true, Canvas.fill);
            Canvas.fill = false;
        }

        /// <summary>
        /// Fill off command should be parsed
        /// </summary>
        [TestMethod]
        public void testCommands_ShouldDraw_FillOff()
        {
            Canvas.fill = true;
            // Arrange
            string[] lines = { "fill off" };

            // Act
            parser.parseCommand(lines, true);

            // Assert
            Assert.AreEqual(false, Canvas.fill);
            Canvas.fill = false;
        }

        /// <summary>
        /// Colour red hex command should be parsed
        /// </summary>
        [TestMethod]
        public void testCommands_ShouldDraw_ColourRedHex()
        {
            Canvas.penColour = Color.Black;
            // Arrange
            string[] lines = { "pen #FF0000" };

            // Act
            parser.parseCommand(lines, true);

            // Assert
            Assert.AreEqual(ColorTranslator.FromHtml("#FF0000"), Canvas.penColour);
            Canvas.penColour = Color.Black;
        }

        /// <summary>
        /// Colour red string command should be parsed
        /// </summary>
        [TestMethod]
        public void testCommands_ShouldDraw_ColourRedString()
        {
            Canvas.penColour = Color.Black;
            // Arrange
            string[] lines = { "pen red" };

            // Act
            parser.parseCommand(lines, true);

            // Assert
            Assert.AreEqual(Color.Red, Canvas.penColour);
            Canvas.penColour = Color.Black;
        }

        /// <summary>
        /// invalid command should throw error message
        /// </summary>
        [TestMethod]
        public void testCommands_ShouldThrowException_InvalidColour()
        {
            // Arrange
            string[] lines = { "pen incorrectColour" };

            // Act
            parser.parseCommand(lines, true);

            //Assert
            Assert.IsTrue(parser.errors > 0);
        }

        /// <summary>
        /// Move to command should be parsed
        /// </summary>
        [TestMethod]
        public void testCommands_ShouldDraw_MoveTo()
        {
            // Arrange
            string[] lines = { "moveto 50 50" };

            // Act
            parser.parseCommand(lines, true);

            // Assert
            Assert.AreEqual(50, Canvas.posX);
            Assert.AreEqual(50, Canvas.posY);

            Canvas.posX = Canvas.defaultPosX;
            Canvas.posY = Canvas.defaultPosY;
        }

        /// <summary>
        /// Reset command should be parsed
        /// </summary>
        [TestMethod]
        public void testCommands_ShouldDraw_Reset()
        {
            Canvas.posX = 50;
            Canvas.posY = 50;
            Canvas.fill = true;
            Canvas.penColour = Color.Red;
            // Arrange
            string[] lines = { "reset" };

            // Act
            parser.parseCommand(lines, true);

            // Assert
            Assert.AreEqual(Canvas.defaultPosX, Canvas.posX);
            Assert.AreEqual(Canvas.defaultPosY, Canvas.posY);
            Assert.AreEqual(Color.Black, Canvas.penColour);
            Assert.AreEqual(false, Canvas.fill);
        }

        /// <summary>
        /// Clear command should be parsed
        /// </summary>
        [TestMethod]
        public void testCommands_ShouldDraw_Clear()
        {
            // Arrange
            // Create a Graphics object and a Canvas
            Bitmap bitmap = new Bitmap(800, 600);
            Graphics graphics = Graphics.FromImage(bitmap);
            Canvas canvas = new Canvas(graphics, null);

            // Draw something on the canvas
            canvas.getGraphics().FillRectangle(new SolidBrush(Color.Black), 10, 10, 100, 100);

            // Act
            canvas.clearCanvas();

            // Assert
            // After clearing, the canvas should be empty (white background)
            Color pixelColor = bitmap.GetPixel(10, 10); // Assuming defaultPosX and defaultPosY
            Assert.AreEqual(ColorTranslator.FromHtml("#FFFFFF"), pixelColor);
        }
    }
}

