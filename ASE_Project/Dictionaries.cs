using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ASE_Project
{
    internal class Dictionaries
    {
        // List of supported Commands and Shapes
        public static List<String> shapes = new List<String>() { "circle", "rectangle", "triangle", "drawto" };
        public static List<String> commands = new List<String>() { "moveto", "reset", "clear", "pen", "fill" };

        // Dictionary of Commands and Shapes and their respective required number of variables
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
        };

        // List storing all error messages
        public static List<string> errorMessages = new List<string>();
    }
}
