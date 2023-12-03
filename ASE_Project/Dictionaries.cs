using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ASE_Project
{
    /// <summary>
    /// Class with lists of supported commands, their arguments and recorded error messages
    /// </summary>
    internal class Dictionaries
    {
        /// <summary>
        /// List of supported Shapes
        /// </summary>
        public static List<String> shapes = new List<String>() { "circle", "rectangle", "triangle", "drawto" };

        /// <summary>
        /// List of supported Non-Shape commands
        /// </summary>
        public static List<String> commands = new List<String>() { "moveto", "reset", "clear", "pen", "fill", "while", "endloop" };

        /// <summary>
        /// Dictionary of Commands and Shapes and their respective required number of variables
        /// </summary>
        public static Dictionary<String, int> validArgsNumber = new Dictionary<String, int>() {
            { "circle",  1 },
            { "rectangle", 2},
            { "triangle", 4 },
            { "drawto", 2 },
            { "moveto", 2 },
            { "fill", 1},
            { "pen", 1 },
            { "clear", 0},
            { "reset", 0},
            { "while", 3}
        };

        /// <summary>
        /// List storing all error messages
        /// </summary>
        public static List<String> errorMessages = new List<string>();

        public static List<String> calcualtions = new List<String>() { "+", "-", "*", "/" };

        public static List<String> loopSymbols = new List<String>() { ">", "<"};

        public static Dictionary<String, int> variables = new Dictionary<String, int>() { };
    }
}
