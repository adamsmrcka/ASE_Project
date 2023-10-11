using ASE_Project;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASEUnitTest
{
    [TestClass]
    public class ShapeUnitTests
    {
        private Parser parser;
        private Canvas canvas;
        PictureBox pictureBox = new PictureBox();

        Form1 form1 = new Form1();

        [TestInitialize]
        public void Initialize()
        {
            parser = Parser.getParser();
            Canvas canvas = new Canvas(pictureBox.CreateGraphics(), form1); // Create a dummy canvas for testing
            parser.setCanvas(canvas);
        }

        [TestMethod]
        public void TestShapes_ShouldDraw_Rectangle()
        {
            Canvas.fill = false;
            // Arrange
            string[] lines = { "rectangle 10 20" };

            // Act
            parser.parseCommand(lines);

            // Assert
            Assert.AreEqual(Parser.s.ToString().ToLower().Split('.').Last(), "rectangle");
            Assert.AreEqual(Canvas.penColour, Color.Black);
            Assert.AreEqual(Canvas.fill, false);
            Assert.AreEqual(Canvas.posX, 10);
            Assert.AreEqual(Canvas.posY, 10);
            Assert.AreEqual(Parser.intArguments[0], 10);
            Assert.AreEqual(Parser.intArguments[1], 20);
        }

        [TestMethod]
        public void TestShapes_ShouldDraw_FillRectangle()
        {
            Canvas.fill = true;
            // Arrange
            string[] lines = { "rectangle 10 20" };

            // Act
            parser.parseCommand(lines);

            // Assert
            Assert.AreEqual(Parser.s.ToString().ToLower().Split('.').Last(), "rectangle");
            Assert.AreEqual(Canvas.penColour, Color.Black);
            Assert.AreEqual(Canvas.fill, true);
            Assert.AreEqual(Canvas.posX, 10);
            Assert.AreEqual(Canvas.posY, 10);
            Assert.AreEqual(Parser.intArguments[0], 10);
            Assert.AreEqual(Parser.intArguments[1], 20);
        }

        [TestMethod]
        public void TestShapes_ShouldDraw_Circle()
        {
            Canvas.fill = false;
            // Arrange
            string[] lines = { "Circle 50" };

            // Act
            parser.parseCommand(lines);

            // Assert
            Assert.AreEqual(Parser.s.ToString().ToLower().Split('.').Last(), "circle");
            Assert.AreEqual(Canvas.penColour, Color.Black);
            Assert.AreEqual(Canvas.fill, false);
            Assert.AreEqual(Canvas.posX, 10);
            Assert.AreEqual(Canvas.posY, 10);
            Assert.AreEqual(Parser.intArguments[0], 50);
        }

        [TestMethod]
        public void TestShapes_ShouldDraw_FillCircle()
        {
            Canvas.fill = true;
            // Arrange
            string[] lines = { "Circle 50" };

            // Act
            parser.parseCommand(lines);

            // Assert
            Assert.AreEqual(Parser.s.ToString().ToLower().Split('.').Last(), "circle");
            Assert.AreEqual(Canvas.penColour, Color.Black);
            Assert.AreEqual(Canvas.fill, true);
            Assert.AreEqual(Canvas.posX, 10);
            Assert.AreEqual(Canvas.posY, 10);
            Assert.AreEqual(Parser.intArguments[0], 50);
        }

        [TestMethod]
        public void TestShapes_ShouldDraw_Triangle()
        {
            Canvas.fill = false;
            // Arrange
            string[] lines = { "Triangle 40 50 120 60" };

            // Act
            parser.parseCommand(lines);

            // Assert
            Assert.AreEqual(Parser.s.ToString().ToLower().Split('.').Last(), "triangle");
            Assert.AreEqual(Canvas.penColour, Color.Black);
            Assert.AreEqual(Canvas.fill, false);
            Assert.AreEqual(Canvas.posX, 10);
            Assert.AreEqual(Canvas.posY, 10);
            Assert.AreEqual(Parser.intArguments[0], 40);
            Assert.AreEqual(Parser.intArguments[1], 50);
            Assert.AreEqual(Parser.intArguments[2], 120);
            Assert.AreEqual(Parser.intArguments[3], 60);
        }

        [TestMethod]
        public void TestShapes_ShouldDraw_FillTriangle()
        {
            Canvas.fill = true;
            // Arrange
            string[] lines = { "Triangle 40 50 120 60" };

            // Act
            parser.parseCommand(lines);

            // Assert
            Assert.AreEqual(Parser.s.ToString().ToLower().Split('.').Last(), "triangle");
            Assert.AreEqual(Canvas.penColour, Color.Black);
            Assert.AreEqual(Canvas.fill, true);
            Assert.AreEqual(Canvas.posX, 10);
            Assert.AreEqual(Canvas.posY, 10);
            Assert.AreEqual(Parser.intArguments[0], 40);
            Assert.AreEqual(Parser.intArguments[1], 50);
            Assert.AreEqual(Parser.intArguments[2], 120);
            Assert.AreEqual(Parser.intArguments[3], 60);
        }

        [TestMethod]
        public void TestShapes_ShouldDraw_DrawTo()
        {
            Canvas.fill = false;
            // Arrange
            string[] lines = { "Drawto 40 50" };

            // Act
            parser.parseCommand(lines);

            // Assert
            Assert.AreEqual(Parser.s.ToString().ToLower().Split('.').Last(), "line");
            Assert.AreEqual(Canvas.penColour, Color.Black);
            Assert.AreEqual(Canvas.fill, false);
            Assert.AreEqual(Canvas.posX, 10);
            Assert.AreEqual(Canvas.posY, 10);
            Assert.AreEqual(Parser.intArguments[0], 40);
            Assert.AreEqual(Parser.intArguments[1], 50);
        }

        [TestMethod]
        public void TestShapes_ShouldDraw_FillDrawTo()
        {
            Canvas.fill = true;
            // Arrange
            string[] lines = { "Drawto 40 50" };

            // Act
            parser.parseCommand(lines);

            // Assert
            Assert.AreEqual(Parser.s.ToString().ToLower().Split('.').Last(), "line");
            Assert.AreEqual(Canvas.penColour, Color.Black);
            Assert.AreEqual(Canvas.fill, true);
            Assert.AreEqual(Canvas.posX, 10);
            Assert.AreEqual(Canvas.posY, 10);
            Assert.AreEqual(Parser.intArguments[0], 40);
            Assert.AreEqual(Parser.intArguments[1], 50);
        }
    }
}
