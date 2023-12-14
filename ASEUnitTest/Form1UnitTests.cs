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
    /// <summary>
    /// Test class testing functionality of Forms1 Class
    /// </summary>
    [TestClass]
    public class Form1UnitTests
    {

        Form1 form1 = new Form1();

        /// <summary>
        /// Sets Objects used during tests
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
        }

        /// <summary>
        /// Test checking that the program is successfully saved and loaded to/from a text file
        /// </summary>
        [TestMethod]
        public void testProgramSave_ShouldSaveAndMatch()
        {
            // Arrange
            Random rnd = new Random();
            int[] variables = { rnd.Next(), rnd.Next() };
            string[] fileText = { "rectangle " + variables[0] + " " + variables[0], "circle " + variables[1] };
            string[] notMatchingText = { "rectangles " + variables[1] + "circles " + variables[0] };

            // Define a unique file name for the test
            string testFileName = "test_program_save.txt";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, testFileName);

            // Act
            form1.saveToTXT(filePath, fileText);
            string[] loadText = form1.loadFromTXT(filePath);

            // Assert - Check if the file was created & Read the content of the saved file
            Assert.IsTrue(File.Exists(filePath));
            string[] savedText = File.ReadAllLines(filePath);

            // Assert - Check if the saved text matches the original text
            CollectionAssert.AreEqual(fileText, savedText);
            CollectionAssert.AreEqual(fileText, loadText);
            CollectionAssert.AreNotEqual(notMatchingText, savedText);
            CollectionAssert.AreNotEqual(notMatchingText, loadText);
            // Clean up: delete the test file
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }

}
