using ASE_Project;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASEUnitTest
{
    [TestClass]
    public class Form1UnitTests
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
        public void testProgramSave_ShouldSaveAndMatch()
        {
            // Arrange
            Random rnd = new Random();
            int[] variables = { rnd.Next(), rnd.Next() };
            string[] fileText = { "rectangle " + variables[0], "circle " + variables[1] };

            // Define a unique file name for the test
            string testFileName = "test_program_save.txt";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, testFileName);

            try
            {
                // Act
                form1.saveToTXT(filePath, fileText);

                // Assert - Check if the file was created & Read the content of the saved file
                Assert.IsTrue(File.Exists(filePath)); 
                string[] savedText = File.ReadAllLines(filePath);

                // Assert - Check if the saved text matches the original text
                CollectionAssert.AreEqual(fileText, savedText);
            }
            finally
            {
                // Clean up: delete the test file
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }
    }

}
