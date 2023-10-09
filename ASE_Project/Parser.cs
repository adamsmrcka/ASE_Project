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
        /// <summary>
        /// Analyses the imput per line and handles instruction according to the imput - differentiates between commands and parameters and divides them accordingly
        /// </summary>
        /// <param name="lines">Array of commands and parameters from commandLineBox or commandTextBox - divided by lines</param>
        internal void parseCommand(string[] lines)
        {
            foreach (String line in lines)
            {
                command = String.Empty;
                trimmedCommand = String.Empty;

                trimmedCommand = line.Trim(' ').ToLower();
                parts = trimmedCommand.Split(' ');

                if (!string.IsNullOrEmpty(parts[0]))
                {
                    command = parts[0];
                    //If the command is to draw a Shape - detects the shape and sends instruction and parameters to prepare the drawing
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
                            default:
                                throw new Exception("Shape does not exist or unknown command");
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Boolean checking whether the command is a Shape
        /// </summary>
        /// <param name="cmd">User entered Command (string)</param>
        /// <returns></returns>
        public bool isShape(String cmd)
        {
            return shapes.Contains(cmd);
        }
    }
}
