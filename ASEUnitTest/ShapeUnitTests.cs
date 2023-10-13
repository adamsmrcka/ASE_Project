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
            canvas = new Canvas(pictureBox.CreateGraphics(), form1); // Create a dummy canvas for testing
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
            Assert.AreEqual("rectangle", Parser.s.ToString().ToLower().Split('.').Last());
            Assert.AreEqual(Color.Black, Shape.colourShape);
            Assert.AreEqual(false, Shape.fillShape);
            Assert.AreEqual(10, Shape.xPos);
            Assert.AreEqual(10, Shape.yPos);
            Assert.AreEqual(10, ASE_Project.Rectangle.width);
            Assert.AreEqual(20, ASE_Project.Rectangle.height);
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
            Assert.AreEqual("rectangle", Parser.s.ToString().ToLower().Split('.').Last());
            Assert.AreEqual(Color.Black, Shape.colourShape);
            Assert.AreEqual(true, Shape.fillShape);
            Assert.AreEqual(10, Shape.xPos);
            Assert.AreEqual(10, Shape.yPos);
            Assert.AreEqual(10, ASE_Project.Rectangle.width);
            Assert.AreEqual(20, ASE_Project.Rectangle.height);
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
            Assert.AreEqual("circle", Parser.s.ToString().ToLower().Split('.').Last());
            Assert.AreEqual(Color.Black, Shape.colourShape);
            Assert.AreEqual(false, Shape.fillShape);
            Assert.AreEqual(10, Shape.xPos);
            Assert.AreEqual(10, Shape.yPos);
            Assert.AreEqual(50, Circle.circleSize);
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
            Assert.AreEqual("circle", Parser.s.ToString().ToLower().Split('.').Last());
            Assert.AreEqual(Color.Black, Shape.colourShape);
            Assert.AreEqual(true, Shape.fillShape);
            Assert.AreEqual(10, Shape.xPos);
            Assert.AreEqual(10, Shape.yPos);
            Assert.AreEqual(50, Parser.intArguments[0]);
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
            Assert.AreEqual("triangle", Parser.s.ToString().ToLower().Split('.').Last());
            Assert.AreEqual(Color.Black, Shape.colourShape);
            Assert.AreEqual(false, Shape.fillShape);
            Assert.AreEqual(testPoints[0], Triangle.trianglePoints[0]);
            Assert.AreEqual(testPoints[1], Triangle.trianglePoints[1]);
            Assert.AreEqual(testPoints[2], Triangle.trianglePoints[2]);
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
            Assert.AreEqual("triangle", Parser.s.ToString().ToLower().Split('.').Last());
            Assert.AreEqual(Color.Black, Shape.colourShape);
            Assert.AreEqual(true, Shape.fillShape);
            Assert.AreEqual(testPoints[0], Triangle.trianglePoints[0]);
            Assert.AreEqual(testPoints[1], Triangle.trianglePoints[1]);
            Assert.AreEqual(testPoints[2], Triangle.trianglePoints[2]);
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
            Assert.AreEqual("line", Parser.s.ToString().ToLower().Split('.').Last());
            Assert.AreEqual(Color.Black, Shape.colourShape);
            Assert.AreEqual(false, Shape.fillShape);
            Assert.AreEqual(10, Shape.xPos);
            Assert.AreEqual(10, Shape.yPos);
            Assert.AreEqual(40, Line.toX);
            Assert.AreEqual(50, Line.toY);
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
            Assert.AreEqual("line", Parser.s.ToString().ToLower().Split('.').Last());
            Assert.AreEqual(Color.Black, Shape.colourShape);
            Assert.AreEqual(true, Shape.fillShape);
            Assert.AreEqual(10, Shape.xPos);
            Assert.AreEqual(10, Shape.yPos);
            Assert.AreEqual(40, Line.toX);
            Assert.AreEqual(50, Line.toY);
        }
    }
}
