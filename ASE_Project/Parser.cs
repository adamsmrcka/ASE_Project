using ASE_Project;
using CommandLine;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;

namespace ASE_Project
{
    internal class Parser
    {
        CommandFactory commandFactory;
        String trimmedCommand;
        String[] parts;
        string[] arg;
        int[] intArguments;
        String command;
        System.Drawing.Color colour;
        Canvas canvas;

        public static List<String> shapes = new List<String>() { "circle", "rectangle", "triangle", "drawto" };

        private static Parser parser = new Parser();
        private Parser()
        {
            commandFactory = CommandFactory.GetShapeFactory();
        }

        public static Parser GetParser() { return parser; }

        public void SetCanvas(Canvas canvas) { this.canvas = canvas; }

        internal void parseCommand(string[] lines)
        {
            foreach (String line in lines)
            {
                command = String.Empty;
                trimmedCommand = String.Empty;

                trimmedCommand = line.Trim(' ').ToLower();
                parts = line.Split(' ');

                command = parts[0];

                if (IsShape(command))
                {
                    arg = parts.Skip(1).ToArray();
                    intArguments = Array.ConvertAll(arg, int.Parse);
                    Shape s = (Shape)commandFactory.GetShape(command);

                    canvas.DrawShape(s, Canvas.penColour, Canvas.fill, Canvas.posX, Canvas.posY, intArguments);
                }
                else
                {
                    //User has input a predefined command that is not a shape. Handle accordingly.
                    switch (command)
                    {
                        case "clear":
                            canvas.ClearCanvas();
                            break;
                        case "reset":
                            canvas.RestoreCanvas();
                            break;
                        case "moveto":
                            arg = parts.Skip(1).ToArray();
                            intArguments = Array.ConvertAll(arg, int.Parse);
                            canvas.MoveTo(intArguments);
                            break;
                        case "colour":
                            arg = parts.Skip(1).ToArray();
                            colour = ColorTranslator.FromHtml(arg[0]);
                            canvas.ChangeColor(colour);
                            break;
                        case "fill":
                            canvas.ToggleFill();
                            break;
                    }
                }
            }
        }
        public bool IsShape(String cmd)
        {
            return shapes.Contains(cmd);
        }
    }
}
