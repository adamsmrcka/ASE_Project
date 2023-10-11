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
    public class Parser
    {
        ShapeFactory commandFactory;
        String trimmedCommand;
        String[] parts;
        public string[] args;
        public static int[] intArguments;
        String command;
        System.Drawing.Color colour;
        Canvas canvas;
        public static Shape s;

        private static Parser parser = new Parser();
        private Parser()
        {
            commandFactory = ShapeFactory.getShapeFactory();
        }

        public static Parser getParser() { return parser; }

        public void setCanvas(Canvas canvas) { this.canvas = canvas; }
        /// <summary>
        /// Analyses the imput per line and handles instruction according to the imput - differentiates between commands and parameters and divides them accordingly
        /// </summary>
        /// <param name="lines">Array of commands and parameters from commandLineBox or commandTextBox - divided by lines</param>
        public void parseCommand(string[] lines)
        {
            if (lines.Length > 0)
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
                            if (Dictionaries.validArgsNumber.TryGetValue(command, out int expectedArgsCount))
                            {
                                args = parts.Skip(1).ToArray();
                                intArguments = new int[args.Length];

                                if (args.Length == expectedArgsCount)
                                {
                                    bool argumentsAreInts = true;

                                    for (int i = 0; i < args.Length; i++)
                                    {
                                        if (!int.TryParse(args[i], out int argValue))
                                        {
                                            argumentsAreInts = false;
                                            throw new Exception($"Error: Argument {i + 1} for '{command}' is not a valid integer: '{args[i]}'");
                                        }
                                        intArguments[i] = argValue;
                                    }

                                    if (argumentsAreInts)
                                    {
                                        s = (Shape)commandFactory.getShape(command);

                                        canvas.drawShape(s, Canvas.penColour, Canvas.fill, Canvas.posX, Canvas.posY, intArguments);
                                    }
                                }
                                else
                                {
                                    // Handle the error as needed, e.g., show an error message or log it.
                                    throw new Exception($"Error: '{command}' command expects {expectedArgsCount} argument(s), but {intArguments.Length} were provided.");
                                }
                            }
                        }
                        else
                        {
                            // User has input a predefined command that is not a shape. Handle accordingly.
                            if (Dictionaries.validArgsNumber.TryGetValue(command, out int expectedArgsCount))
                            {
                                args = parts.Skip(1).ToArray();
                                if (args.Length == expectedArgsCount)
                                {
                                    switch (command)
                                    {
                                        case "clear":
                                            canvas.clearCanvas();
                                            break;

                                        case "reset":
                                            canvas.restoreCanvas();
                                            break;

                                        case "moveto":
                                            intArguments = Array.ConvertAll(args, int.Parse);
                                            canvas.moveTo(intArguments);
                                            break;

                                        case "pen":
                                            try
                                            {
                                                colour = ColorTranslator.FromHtml(args[0]);
                                            }
                                            catch 
                                            {
                                                throw new Exception($"Error: Argument '{args[0]}' for '{command}' command is not a valid colour.");
                                            }
                                            canvas.changeColor(colour);
                                            break;

                                        case "fill":
                                            canvas.toggleFill(args[0]);
                                            break;

                                        default:
                                            throw new Exception($"Error: Unknown command '{command}'");
                                            // Handle the error as needed.
                                    }
                                }
                                else
                                {
                                    throw new Exception($"Error: '{command}' command expects {expectedArgsCount} argument(s), but {args.Length} were provided.");
                                    // Handle the error as needed.
                                }
                            }
                            else
                            {
                                throw new Exception($"Error: Unknown command '{command}'");
                                // Handle the error as needed.
                            }
                        }
                    }
                    else
                    {
                        throw new Exception($"Error: No command entered.");
                        // Handle the error as needed.
                    }
                }
            }
            else
            {
                throw new Exception($"Error: No command entered.");
                // Handle the error as needed.
            }
        }
        /// <summary>
        /// Boolean checking whether the command is a Shape
        /// </summary>
        /// <param name="cmd">User entered Command (string)</param>
        /// <returns></returns>
        public bool isShape(String cmd)
        {
            return Dictionaries.shapes.Contains(cmd);
        }
    }
}
