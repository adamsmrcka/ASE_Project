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
            string[] lines = { "Rectangle 10 20" };

            // Act
            parser.parseCommand(lines);

            // Assert
            Assert.AreEqual(Parser.s.ToString().ToLower().Split('.').Last(), "rectangle");
            Assert.AreEqual(Shape.colourShape, Color.Black);
            Assert.AreEqual(Shape.fillShape, false);
            Assert.AreEqual(Shape.xPos, 10);
            Assert.AreEqual(Shape.yPos, 10);
            Assert.AreEqual(ASE_Project.Rectangle.width, 10);
            Assert.AreEqual(ASE_Project.Rectangle.height, 20);
        }

        [TestMethod]
        public void TestShapes_ShouldDraw_FillRectangle()
        {
            Canvas.fill = true;
            // Arrange
            string[] lines = { "Rectangle 10 20" };

            // Act
            parser.parseCommand(lines);

            // Assert
            Assert.AreEqual(Parser.s.ToString().ToLower().Split('.').Last(), "rectangle");
            Assert.AreEqual(Shape.colourShape, Color.Black);
            Assert.AreEqual(Shape.fillShape, true);
            Assert.AreEqual(Shape.xPos, 10);
            Assert.AreEqual(Shape.yPos, 10);
            Assert.AreEqual(ASE_Project.Rectangle.width, 10);
            Assert.AreEqual(ASE_Project.Rectangle.height, 20);
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
            Assert.AreEqual(Shape.colourShape, Color.Black);
            Assert.AreEqual(Shape.fillShape, false);
            Assert.AreEqual(Shape.xPos, 10);
            Assert.AreEqual(Shape.yPos, 10);
            Assert.AreEqual(Circle.circleSize, 50);
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
            Assert.AreEqual(Shape.colourShape, Color.Black);
            Assert.AreEqual(Shape.fillShape, true);
            Assert.AreEqual(Shape.xPos, 10);
            Assert.AreEqual(Shape.yPos, 10);
            Assert.AreEqual(Parser.intArguments[0], 50);
        }

        [TestMethod]
        public void TestShapes_ShouldDraw_Triangle()
        {
            Canvas.fill = false;
            Point[] testPoints = new Point[3];
            testPoints[0] = new Point(10, 10);
            testPoints[1] = new Point(40, 50);
            testPoints[2] = new Point(120, 60);
            // Arrange
            string[] lines = { "Triangle 40 50 120 60" };

            // Act
            parser.parseCommand(lines);

            // Assert
            Assert.AreEqual(Parser.s.ToString().ToLower().Split('.').Last(), "triangle");
            Assert.AreEqual(Shape.colourShape, Color.Black);
            Assert.AreEqual(Shape.fillShape, false);
            Assert.AreEqual(Triangle.trianglePoints[0], testPoints[0]);
            Assert.AreEqual(Triangle.trianglePoints[1], testPoints[1]);
            Assert.AreEqual(Triangle.trianglePoints[2], testPoints[2]);
        }

        [TestMethod]
        public void TestShapes_ShouldDraw_FillTriangle()
        {
            Canvas.fill = true;
            Point[] testPoints = new Point[3];
            testPoints[0] = new Point(10, 10);
            testPoints[1] = new Point(40, 50);
            testPoints[2] = new Point(120, 60);
            // Arrange
            string[] lines = { "Triangle 40 50 120 60" };

            // Act
            parser.parseCommand(lines);

            // Assert
            Assert.AreEqual(Parser.s.ToString().ToLower().Split('.').Last(), "triangle");
            Assert.AreEqual(Shape.colourShape, Color.Black);
            Assert.AreEqual(Shape.fillShape, true);
            Assert.AreEqual(Triangle.trianglePoints[0], testPoints[0]);
            Assert.AreEqual(Triangle.trianglePoints[1], testPoints[1]);
            Assert.AreEqual(Triangle.trianglePoints[2], testPoints[2]);
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
            Assert.AreEqual(Shape.colourShape, Color.Black);
            Assert.AreEqual(Shape.fillShape, false);
            Assert.AreEqual(Shape.xPos, 10);
            Assert.AreEqual(Shape.yPos, 10);
            Assert.AreEqual(Line.toX, 40);
            Assert.AreEqual(Line.toY, 50);
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
            Assert.AreEqual(Shape.colourShape, Color.Black);
            Assert.AreEqual(Shape.fillShape, true);
            Assert.AreEqual(Shape.xPos, 10);
            Assert.AreEqual(Shape.yPos, 10);
            Assert.AreEqual(Line.toX, 40);
            Assert.AreEqual(Line.toY, 50);
        }
    }
}
