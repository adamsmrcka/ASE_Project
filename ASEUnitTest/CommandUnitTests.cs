﻿using ASE_Project;
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
    public class CommandUnitTests
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
        public void TestCommands_ShouldDraw_FillOn()
        {
            Canvas.fill = false;
            // Arrange
            string[] lines = { "fill on" };

            // Act
            parser.parseCommand(lines);

            // Assert
            Assert.AreEqual(true, Canvas.fill);
            Canvas.fill = false;
        }

        [TestMethod]
        public void TestCommands_ShouldDraw_FillOff()
        {
            Canvas.fill = true;
            // Arrange
            string[] lines = { "fill off" };

            // Act
            parser.parseCommand(lines);

            // Assert
            Assert.AreEqual(false, Canvas.fill);
            Canvas.fill = false;
        }

        [TestMethod]
        public void TestCommands_ShouldDraw_ColourRedHex()
        {
            Canvas.penColour = Color.Black;
            // Arrange
            string[] lines = { "pen #FF0000" };

            // Act
            parser.parseCommand(lines);

            // Assert
            Assert.AreEqual(ColorTranslator.FromHtml("#FF0000"), Canvas.penColour);
            Canvas.penColour = Color.Black;
        }

        [TestMethod]
        public void TestCommands_ShouldDraw_ColourRedString()
        {
            Canvas.penColour = Color.Black;
            // Arrange
            string[] lines = { "pen red" };

            // Act
            parser.parseCommand(lines);

            // Assert
            Assert.AreEqual(Color.Red, Canvas.penColour);
            Canvas.penColour = Color.Black;
        }

        [TestMethod]
        public void TestCommands_ShouldThrowException_InvalidColour()
        {
            // Arrange
            string[] lines = { "pen incorrectColour" };

            // Act & Assert
            Assert.ThrowsException<Exception>(() => parser.parseCommand(lines));
        }

        [TestMethod]
        public void TestCommands_ShouldDraw_MoveTo()
        {
            // Arrange
            string[] lines = { "moveto 50 50" };

            // Act
            parser.parseCommand(lines);

            // Assert
            Assert.AreEqual(50, Canvas.posX);
            Assert.AreEqual(50, Canvas.posY);

            Canvas.posX = Canvas.defaultPosX;
            Canvas.posY = Canvas.defaultPosY;
        }
    }
}
