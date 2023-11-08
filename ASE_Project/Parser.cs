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
    /// <summary>
    /// Parses a String[] one line at a time, identifies commands and variables, and executes them if they are correct.
    /// </summary>
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
        public int errors;
        Exception caughtException = null;

        private static Parser parser = new Parser();

        /// <summary>
        /// Constructor, gets ShapeFactory instances.
        /// </summary>
        private Parser()
        {
            commandFactory = ShapeFactory.getShapeFactory();
        }

        /// <summary>
        /// Returns the parser instance.
        /// </summary>
        /// <returns>Parser</returns>
        public static Parser getParser() { return parser; }

        /// <summary>
        /// Sets the Canvas object reference
        /// </summary>
        /// <param name="canvas">Canvas Object</param>
        public void setCanvas(Canvas canvas) { this.canvas = canvas; }

        /// <summary>
        /// Analyses the imput per line and handles instruction according to the imput - differentiates between commands and parameters and divides them accordingly
        /// </summary>
        /// <param name="lines">Array of commands and parameters from commandLineBox or commandTextBox - divided by lines</param>
        /// <param name="draw">boolean swithing between drawing and syntax analyses</param>
        public void parseCommand(string[] lines, bool draw)
        {
            errors = 0;
            if (lines.Length > 0)
            {
                foreach (String line in lines)
                {
                    command = String.Empty;
                    trimmedCommand = String.Empty;
                    try
                    {
                        trimmedCommand = line.Trim(' ').ToLower();
                        if (trimmedCommand.Equals(String.Empty))
                        {
                            continue;
                        };
                        parts = trimmedCommand.Split(' ');
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
                                        if (draw == true)
                                        {
                                            s = (Shape)commandFactory.getShape(command);
                                            canvas.drawShape(s, Canvas.penColour, Canvas.fill, Canvas.posX, Canvas.posY, intArguments);
                                        }
                                    }
                                }
                                else
                                {
                                    throw new Exception($"Error: '{command}' command expects {expectedArgsCount} argument(s), but {intArguments.Length} were provided.");
                                }
                            }
                        }
                        else
                        {
                            // User has input a predefined command that is not a shape. Checks number of arguments and handle accordingly.
                            if (Dictionaries.validArgsNumber.TryGetValue(command, out int expectedArgsCount))
                            {
                                args = parts.Skip(1).ToArray();
                                if (args.Length == expectedArgsCount && draw == true)
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
                                            if (args[0] == "on" || args[0] == "off")
                                            {
                                                canvas.toggleFill(args[0]);
                                            }
                                            else
                                            {
                                                throw new Exception($"Error: Incorect Fill parameter '{args[0]}', Expeted On or Off");
                                            }
                                            break;

                                        default:
                                            throw new Exception($"Error: Unknown command '{command}'");
                                    }
                                }
                                else if (args.Length == expectedArgsCount && draw == false)
                                {
                                    switch (command)
                                    {
                                        case "clear":
                                            break;

                                        case "reset":
                                            break;

                                        case "moveto":
                                            intArguments = Array.ConvertAll(args, int.Parse);
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
                                            break;

                                        case "fill":
                                            if (args[0] == "on" || args[0] == "off")
                                            {
                                            }
                                            else
                                            {
                                                throw new Exception($"Error: Incorect Fill parameter '{args[0]}', Expeted On or Off");
                                            }
                                            break;

                                        default:
                                            throw new Exception($"Error: Unknown command '{command}'");
                                    }
                                }
                                else
                                {
                                    throw new Exception($"Error: '{command}' command expects {expectedArgsCount} argument(s), but {args.Length} were provided.");
                                }
                            }
                            else
                            {
                                throw new Exception($"Error: Unknown command '{command}'");
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        errors++;
                        Dictionaries.errorMessages.Add(ex.Message);
                        caughtException = ex;
                    }
                }
            }
            else
            {
                errors++;
                throw new Exception($"Error: No command entered.");
            }
        }
        /// <summary>
        /// Boolean checking whether a command is a Shape
        /// </summary>
        /// <param name="cmd">User entered Command (string)</param>
        /// <returns>True or False</returns>
        public bool isShape(String cmd)
        {
            return Dictionaries.shapes.Contains(cmd);
        }
    }
}
