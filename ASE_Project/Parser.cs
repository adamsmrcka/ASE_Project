﻿using ASE_Project;
using CommandLine;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Xml.Linq;

namespace ASE_Project
{
    /// <summary>
    /// Parses a String[] one line at a time, identifies commands and variables, and executes them if they are correct.
    /// </summary>
    public class Parser
    {
        ShapeFactory commandFactory;
        public string[] args;
        public static int[] intArguments;
        System.Drawing.Color colour;
        Canvas canvas;
        public static Shape s;
        public int errors;
        Exception caughtException = null;
        bool loopFlag = false;
        string[] loopArgs;
        List<String> loopCommands = new List<String>();

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
                    string command = String.Empty;
                    string trimmedCommand = String.Empty;
                    try
                    {
                        trimmedCommand = line.Trim(' ').ToLower();
                        if (trimmedCommand.Equals(String.Empty))
                        {
                            continue;
                        };
                        string[] parts = trimmedCommand.Split(' ');

                        if (loopFlag == true)
                        {
                            if (parts[0] != "endloop")
                            {
                                loopCommands.Add(line);
                            }
                            else if (parts[0] == "endloop")
                            {
                                string e1 = "";
                                string e2 = "";
                                int numberOfCycles = 0;
                                bool isLoopExCorrect;
                                bool e1Assigned = false;
                                bool e2Assigned = false;
                                do
                                {
                                    string[] loopParts;
                                    try
                                    {
                                        e1 = Dictionaries.variables[loopArgs[0]].ToString();
                                        e1Assigned = true;
                                    }
                                    catch (Exception)
                                    {
                                        if (!e1Assigned)
                                        {
                                            e1 = loopArgs[0];
                                            e1Assigned = true;
                                        }
                                    }
                                    try
                                    {
                                        e2 = Dictionaries.variables[loopArgs[2]].ToString();
                                        e2Assigned = true;
                                    }
                                    catch (Exception)
                                    {
                                        if (!e2Assigned)
                                        {
                                            e2 = loopArgs[2];
                                            e2Assigned = true;
                                        }
                                    }

                                    string equation = e1 + " " + loopArgs[1] + " " + e2;
                                    isLoopExCorrect = validateLoopExpression(equation);

                                    if (isLoopExCorrect)
                                    {
                                        foreach (String loopLine in loopCommands)
                                        {
                                            string loopTrimmedCommand = loopLine.Trim(' ').ToLower();
                                            loopParts = loopTrimmedCommand.Split(' ');
                                            analyses(loopParts, draw);
                                        }
                                    }
                                    numberOfCycles++;
                                }
                                while (isLoopExCorrect && numberOfCycles < 100);

                                if (numberOfCycles > 100)
                                {
                                    loopFlag = true;
                                    throw new Exception($"Error: Loop command has ended automatically after 100 cycles");
                                }
                                else
                                {
                                    loopFlag = false;
                                }
                            }
                        }
                        else
                        {
                            analyses(parts, draw);
                        }
                    }
                    catch (Exception ex)
                    {
                        errors++;
                        Dictionaries.errorMessages.Add(ex.Message);
                        caughtException = ex;
                    }
                }
                if (loopFlag)
                {
                    errors++;
                    throw new Exception($"Error: Loop command is not correctly ended");
                }
            }
            else
            {
                errors++;
                throw new Exception($"Error: No command entered.");
            }

        }

        private bool validateLoopExpression(string equation)
        {
            try
            {
                DataTable table = new DataTable();
                var result = table.Compute(equation, "");
                return Convert.ToBoolean(result);
            }
            catch (Exception e)
            {
                // An exception may occur for invalid expressions
                return false;
            }
        }

        public void analyses(string[] parts, bool draw)
        {

            string command = parts[0];
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
                            if (Dictionaries.variables.TryGetValue(args[i], out int varValue))
                            {
                                intArguments[i] = varValue;
                            }
                            else if (int.TryParse(args[i], out int argValue))
                            {
                                intArguments[i] = argValue;
                            }
                            else
                            {
                                argumentsAreInts = false;
                                throw new Exception($"Error: Argument {i + 1} for '{command}' is not a valid integer: '{args[i]}'");
                            }
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
            // Checking non-shape Commands
            else if (Dictionaries.commands.Contains(command))
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
                            case "while":
                                if (Dictionaries.loopSymbols.Contains(args[1]) && Dictionaries.variables.TryGetValue(args[0], out int argVarValue1) || int.TryParse(args[0], out int argValue1) && Dictionaries.variables.TryGetValue(args[2], out int argVarValue2) || int.TryParse(args[2], out int argValue2))
                                {
                                    int e1, e2;
                                    bool isLoopExCorrect;
                                    try
                                    {
                                        e1 = int.Parse(args[0]);
                                    }
                                    catch (Exception e)
                                    {
                                        e1 = Dictionaries.variables[args[0]];
                                    }
                                    try
                                    {
                                        e2 = int.Parse(args[2]);
                                    }
                                    catch (Exception e)
                                    {
                                        e2 = Dictionaries.variables[args[2]];
                                    }

                                    string equation = e1.ToString() + " " + args[1] + " " + e2.ToString();
                                    isLoopExCorrect = validateLoopExpression(equation);

                                    if (isLoopExCorrect)
                                    {
                                        loopFlag = true;
                                        loopArgs = new string[] { args[0], args[1], args[2] };
                                    }
                                }
                                else
                                {
                                    throw new Exception($"Error: Argumets: '{args[0]}', '{args[1]}' and '{args[2]}' are invalid arguments for command While loop");
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

                            case "while":
                                if (Dictionaries.loopSymbols.Contains(args[1]) && Dictionaries.variables.TryGetValue(args[0], out int argVarValue1) || Dictionaries.variables.TryGetValue(args[2], out int argVarValue2) && int.TryParse(args[0], out int argValue1) || int.TryParse(args[2], out int argValue2))
                                {
                                    loopFlag = true;
                                }
                                else
                                {
                                    throw new Exception($"Error: Argumets: '{args[0]}', '{args[1]}' and '{args[2]}' are invalid arguments for command While loop");
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
            // Check if it is declaring Variables
            else if (parts.Length == 3 && parts[1] == "=")
            {

                if (int.TryParse(parts[2], out int argValue))
                {
                    if (Dictionaries.variables.TryGetValue(command, out int oldVarValue))
                    {
                        // Update the existing variable
                        Dictionaries.variables[command] = int.Parse(parts[2]);
                    }
                    else
                    {
                        Dictionaries.variables.Add(parts[0], int.Parse(parts[2]));
                    }
                }
                else if (Dictionaries.variables.TryGetValue(parts[2], out int newVarValue))
                {
                    if (Dictionaries.variables.TryGetValue(command, out int oldVarValue))
                    {
                        // Update the existing variable
                        Dictionaries.variables[command] = newVarValue;
                    }
                    else
                    {
                        Dictionaries.variables.Add(parts[0], newVarValue);
                    }
                }
                else
                {
                    throw new Exception($"Error: Argument '{parts[2]}' for Variable '{command}' is not a valid parameter.");
                }
            }

            // Check for a complex Variable declaration
            else if (parts.Length > 4 && parts[1] == "=")
            {
                string calculation = "";
                DataTable table = new DataTable();
                bool errors = false;

                for (int i = 2; i < parts.Length; i = i + 2)
                {
                    if (Dictionaries.variables.TryGetValue(parts[i], out int newVarValue))
                    {
                        parts[i] = newVarValue.ToString();

                    }
                    else if (!int.TryParse(parts[i], out int argValue))
                    {
                        errors = true;
                        throw new Exception($"Error: Argument '{parts[i]}' for Variable '{command}' is not a valid parameter.");

                    }
                }
                for (int i = 3; i < parts.Length; i = i + 2)
                {
                    if (!Dictionaries.calcualtions.Contains(parts[i]))
                    {
                        errors = true;
                        throw new Exception($"Error: Argument '{parts[i]}' for Variable '{command}' is not a valid equation parameter.");
                    }
                }
                if (errors == false)
                {
                    foreach (string cmd in parts.Skip(2))
                    {
                        calculation += cmd;
                    }
                    var result = table.Compute(calculation, null);
                    if (result != null)
                    {
                        string resultString = result.ToString();
                        if (Dictionaries.variables.TryGetValue(command, out int oldVarValue))
                        {
                            // Update the existing variable
                            Dictionaries.variables[command] = int.Parse(resultString);
                        }
                        else
                        {
                            Dictionaries.variables.Add(parts[0], int.Parse(resultString));
                        }
                    }
                }
            }
            else
            {
                throw new Exception($"Error: '{command}' is an Unknown command.");
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

