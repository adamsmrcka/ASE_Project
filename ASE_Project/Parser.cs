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
        string[] args;
        int[] intArguments;
        String command;
        System.Drawing.Color colour;
        Canvas canvas;

        public static List<String> shapes = new List<String>() { "circle", "rectangle", "triangle", "drawto" };

        private static Parser parser = new Parser();
        private Parser()
        {
            commandFactory = CommandFactory.getShapeFactory();
        }

        public static Parser getParser() { return parser; }

        public void setCanvas(Canvas canvas) { this.canvas = canvas; }

        internal void parseCommand(string[] lines)
        {
            foreach (String line in lines)
            {
                command = String.Empty;
                trimmedCommand = String.Empty;

                trimmedCommand = line.Trim(' ').ToLower();
                parts = trimmedCommand.Split(' ');

                command = parts[0];

                if (isShape(command))
                {
                    args = parts.Skip(1).ToArray();
                    intArguments = Array.ConvertAll(args, int.Parse);
                    Shape s = (Shape)commandFactory.getShape(command);

                    canvas.drawShape(s, Canvas.penColour, Canvas.fill, Canvas.posX, Canvas.posY, intArguments);
                }
                else
                {
                    //User has input a predefined command that is not a shape. Handle accordingly.
                    switch (command)
                    {
                        case "clear":
                            canvas.clearCanvas();
                            break;
                        case "reset":
                            canvas.restoreCanvas();
                            break;
                        case "moveto":
                            args = parts.Skip(1).ToArray();
                            intArguments = Array.ConvertAll(args, int.Parse);
                            canvas.moveTo(intArguments);
                            break;
                        case "colour":
                            args = parts.Skip(1).ToArray();
                            colour = ColorTranslator.FromHtml(args[0]);
                            canvas.changeColor(colour);
                            break;
                        case "fill":
                            canvas.toggleFill();
                            break;
                    }
                }
            }
        }
        public bool isShape(String cmd)
        {
            return shapes.Contains(cmd);
        }
    }
}
