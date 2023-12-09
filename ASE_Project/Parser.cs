using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

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
        bool loopFlagFirst = false;
        bool ifFlag = false;
        bool ifFlagFirst = false;
        bool buildingMethodFlag = false;

        string methodName;
        string[] loopArgs;
        string[] ifArgs;

        List<String> methodLines = new List<String>();
        List<String> loopCommands = new List<String>();
        List<String> ifCommands = new List<String>();

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
            methodName = "";
            methodLines.Clear();
            loopCommands.Clear();
            ifCommands.Clear();

            errors = 0;
            if (lines.Length > 0)
            {
                ifFlag = false;
                loopFlag = false;
                loopFlagFirst = false;
                ifFlagFirst = false;
                buildingMethodFlag |= false;

                foreach (String line in lines)
                {
                    string trimmedCommand = String.Empty;
                    try
                    {
                        trimmedCommand = line.Trim(' ').ToLower();
                        if (trimmedCommand.Equals(String.Empty))
                        {
                            continue;
                        };

                        if (buildingMethodFlag)
                        {
                            buildingMethod(trimmedCommand, draw);
                        }
                        else if (ifFlag && !loopFlagFirst)
                        {
                            ifAnalyses(trimmedCommand, draw);
                        }
                        else if (loopFlag && !ifFlagFirst)
                        {
                            loopsAnalyses(trimmedCommand, draw);
                        }
                        else
                        {
                            analyses(trimmedCommand, draw);
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
                    throw new Exception($"Error: Loop command is not correctly ended");
                }
                if (ifFlag)
                {
                    throw new Exception($"Error: If command is not correctly ended");
                }
                if (buildingMethodFlag)
                {
                    throw new Exception($"Error: Building method command is not correctly ended");
                }
            }
            else
            {
                errors++;
                throw new Exception($"Error: No command entered.");
            }
            canvas.displaySavedVar();
        }

        private bool validateExpression(string equation)
        {
            try
            {
                DataTable table = new DataTable();
                var result = table.Compute(equation, "");
                return Convert.ToBoolean(result);
            }
            catch (Exception)
            {
                // An exception may occur for invalid expressions
                return false;
            }
        }

        private void analyses(string trimmedCommand, bool draw)
        {
            string[] parts = trimmedCommand.Split(' ');
            string command = parts[0];
            //If the command is to draw a Shape - detects the shape and sends instruction and parameters to prepare the drawing
            if (isShape(command))
            {
                if (Dictionaries.validArgsNumber.TryGetValue(command, out int expectedArgsCount))
                {
                    args = parts.Skip(1).ToArray();
                    intArguments = new int[args.Length];

                    if (args.Length == expectedArgsCount || command == "polygon" && args.Length >= expectedArgsCount && args.Length % 2 == 0)
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
                                if (Dictionaries.loopSymbols.Contains(args[1]) && Dictionaries.variables.TryGetValue(args[0], out int loopArgVarValue1) || int.TryParse(args[0], out int loopArgValue1) && Dictionaries.variables.TryGetValue(args[2], out int loopArgVarValue2) || int.TryParse(args[2], out int loopArgValue2))
                                {
                                    int e1, e2;
                                    try
                                    {
                                        e1 = int.Parse(args[0]);
                                    }
                                    catch (Exception)
                                    {
                                        e1 = Dictionaries.variables[args[0]];
                                    }
                                    try
                                    {
                                        e2 = int.Parse(args[2]);
                                    }
                                    catch (Exception)
                                    {
                                        e2 = Dictionaries.variables[args[2]];
                                    }
                                    loopArgs = new string[] { args[0], args[1], args[2] };
                                    loopFlag = true;
                                    if (ifFlag == false)
                                    {
                                        loopFlagFirst = true;
                                    }
                                }
                                else
                                {
                                    throw new Exception($"Error: Argumets: '{args[0]}', '{args[1]}' and '{args[2]}' are invalid arguments for command While loop");
                                }
                                break;

                            case "if":
                                if (Dictionaries.ifSymbols.Contains(args[1]) && Dictionaries.variables.TryGetValue(args[0], out int ifArgVarValue1) || int.TryParse(args[0], out int ifArgValue1) && Dictionaries.variables.TryGetValue(args[2], out int ifArgVarValue2) || int.TryParse(args[2], out int ifArgValue2))
                                {
                                    int e1, e2;
                                    try
                                    {
                                        e1 = int.Parse(args[0]);
                                    }
                                    catch (Exception)
                                    {
                                        e1 = Dictionaries.variables[args[0]];
                                    }
                                    try
                                    {
                                        e2 = int.Parse(args[2]);
                                    }
                                    catch (Exception)
                                    {
                                        e2 = Dictionaries.variables[args[2]];
                                    }

                                    ifFlag = true;
                                    ifArgs = new string[] { args[0], args[1], args[2] };
                                    if (loopFlag == false)
                                    {
                                        ifFlagFirst = true;
                                    }
                                }
                                else
                                {
                                    throw new Exception($"Error: Argumets: '{args[0]}', '{args[1]}' and '{args[2]}' are invalid arguments for command If");
                                }
                                break;

                            case "delete":
                                if (parts.Length == 2)
                                {
                                    if (Dictionaries.variables.TryGetValue(parts[1], out int VarValue1))
                                    {
                                        Dictionaries.variables.Remove(parts[1]);
                                    }
                                    else
                                    {
                                        throw new Exception($"Error: Variable '{parts[1]}' was not found");
                                    }
                                }
                                else
                                {
                                    throw new Exception($"Error: '{parts[1]}' is invalid delete argument");
                                }
                                break;

                            case "deletemethod":
                                if (parts[1] == "method")
                                {
                                    if (Dictionaries.methods.TryGetValue(parts[1], out string VarValue1))
                                    {
                                        Dictionaries.methods.Remove(parts[1]);
                                        Dictionaries.methodLines.Remove(parts[1]);
                                    }
                                    else
                                    {
                                        throw new Exception($"Error: Method '{parts[1]}' was not found");
                                    }
                                }
                                else
                                {
                                    throw new Exception($"Error: '{parts[1]}' is invalid delete argument");
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
                                if (Dictionaries.loopSymbols.Contains(args[1]) && Dictionaries.variables.TryGetValue(args[0], out int loopArgVarValue1) || int.TryParse(args[0], out int loopArgValue1) && Dictionaries.variables.TryGetValue(args[2], out int loopArgVarValue2) || int.TryParse(args[2], out int loopArgValue2))
                                {
                                    int e1, e2;
                                    try
                                    {
                                        e1 = int.Parse(args[0]);
                                    }
                                    catch (Exception)
                                    {
                                        e1 = Dictionaries.variables[args[0]];
                                    }
                                    try
                                    {
                                        e2 = int.Parse(args[2]);
                                    }
                                    catch (Exception)
                                    {
                                        e2 = Dictionaries.variables[args[2]];
                                    }

                                    loopArgs = new string[] { args[0], args[1], args[2] };
                                    loopFlag = true;
                                    if (ifFlag == false)
                                    {
                                        loopFlagFirst = true;
                                    }
                                }
                                else
                                {
                                    throw new Exception($"Error: Argumets: '{args[0]}', '{args[1]}' and '{args[2]}' are invalid arguments for command While loop");
                                }
                                break;

                            case "if":
                                if (Dictionaries.ifSymbols.Contains(args[1]) && Dictionaries.variables.TryGetValue(args[0], out int ifArgVarValue1) || int.TryParse(args[0], out int ifArgValue1) && Dictionaries.variables.TryGetValue(args[2], out int ifArgVarValue2) || int.TryParse(args[2], out int ifArgValue2))
                                {
                                    int e1, e2;
                                    try
                                    {
                                        e1 = int.Parse(args[0]);
                                    }
                                    catch (Exception)
                                    {
                                        e1 = Dictionaries.variables[args[0]];
                                    }
                                    try
                                    {
                                        e2 = int.Parse(args[2]);
                                    }
                                    catch (Exception)
                                    {
                                        e2 = Dictionaries.variables[args[2]];
                                    }

                                    ifFlag = true;
                                    ifArgs = new string[] { args[0], args[1], args[2] };
                                    if (loopFlag == false)
                                    {
                                        ifFlagFirst = true;
                                    }
                                }
                                else
                                {
                                    throw new Exception($"Error: Argumets: '{args[0]}', '{args[1]}' and '{args[2]}' are invalid arguments for command If");
                                }
                                break;

                            case "delete":
                                if (parts.Length == 2)
                                {
                                    if (Dictionaries.variables.TryGetValue(parts[1], out int VarValue1))
                                    {
                                    }
                                    else
                                    {
                                        throw new Exception($"Error: Variable '{parts[1]}' was not found");
                                    }
                                }
                                else
                                {
                                    throw new Exception($"Error: '{parts[1]}' is invalid delete argument");
                                }
                                break;

                            case "deletemethod":
                                if (parts[1] == "method")
                                {
                                    if (Dictionaries.methods.TryGetValue(parts[1], out string VarValue1))
                                    {
                                    }
                                    else
                                    {
                                        throw new Exception($"Error: Method '{parts[1]}' was not found");
                                    }
                                }
                                else
                                {
                                    throw new Exception($"Error: '{parts[1]}' is invalid delete argument");
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
            else if (command == "method")
            {
                if (IsValidMethodDeclaration(trimmedCommand))
                {

                    if (Dictionaries.methods.TryGetValue(parts[1], out string MethodVar))
                    {
                        string[] MethodVarValues = MethodVar.Split(' ');

                        if (MethodVar == "")
                        {
                            if (parts[2] == "()")
                            {
                                List<string> linesInMethod = Dictionaries.methodLines[parts[1]];
                                foreach (string lineInMethod in linesInMethod)
                                {
                                    analyses(lineInMethod, draw);
                                }
                            }
                            else
                            {
                                throw new Exception($"Error: No parameters for '{parts[1]}' method during creation were declared");
                            }
                        }
                        else if (parts.Length - 2 == MethodVarValues.Length)
                        {
                            string[] methodParameters;
                            List<string> parametersList = new List<string>();

                            for (int i = 2; i < parts.Length; i++)
                            {
                                // Trim characters and add to the List
                                parametersList.Add(parts[i].Trim('(', ')', ','));
                            }

                            // Convert the List to an array if needed
                            methodParameters = parametersList.ToArray();

                            string parUsedForCommand = methodParameters.FirstOrDefault(item => Dictionaries.commands.Contains(item));
                            string parUsedForShape = methodParameters.FirstOrDefault(item => Dictionaries.shapes.Contains(item));

                            if (parUsedForCommand != null || parUsedForShape != null)
                            {
                                throw new Exception($"Error: Method Failed!" + Environment.NewLine + $"'{parUsedForCommand ?? parUsedForShape}' is an invalid name for variable");
                            }
                            else
                            {
                                List<string> linesInMethod = Dictionaries.methodLines[parts[1]];
                                foreach (string lineInMethod in linesInMethod)
                                {
                                    string methodLine = lineInMethod;
                                    for (int i = 0; i < MethodVarValues.Length; i++)
                                    {
                                        // Replace variables in the line with values from parts array
                                        string variableToReplace = MethodVarValues[i];
                                        if (lineInMethod.Contains(variableToReplace))
                                        {
                                            methodLine = lineInMethod.Replace(variableToReplace, methodParameters[i]);
                                        }
                                    }
                                    try
                                    {
                                        analyses(methodLine, draw);
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new Exception($"Method Failed!" + Environment.NewLine + $"{ex.Message}");
                                    }
                                }
                            }
                        }
                        else
                        {
                            throw new Exception($"Error: Invalid number of parameters for method '{parts[1]}' were included. {parts.Length - 2} were included but {MethodVarValues.Length} were expected");
                        }
                    }
                    else
                    {
                        List<string> emptyList = new List<string>();
                        methodName = parts[1];
                        Dictionaries.methods.Add(methodName, "");
                        Dictionaries.methodLines.Add(methodName, emptyList);
                        string methodParameters = "";
                        for (int i = 2; i < parts.Length; i++)
                        {
                            methodParameters += parts[i].Trim('(', ')', ',') + " ";
                        }

                        string[] MethodVarValues = methodParameters.Split(' ');
                        string parUsedForCommand = MethodVarValues.FirstOrDefault(item => Dictionaries.commands.Contains(item));
                        string parUsedForShape = MethodVarValues.FirstOrDefault(item => Dictionaries.shapes.Contains(item));

                        if (parUsedForCommand != null || parUsedForShape != null)
                        {
                            Dictionaries.methods.Remove(methodName);
                            Dictionaries.methodLines.Remove(methodName);
                            throw new Exception($"Error: '{parUsedForCommand ?? parUsedForShape}' is an invalid name for variable");
                        }
                        else
                        {
                            methodLines.Clear();
                            Dictionaries.methods[methodName] = methodParameters.Trim();
                            buildingMethodFlag = true;
                        }
                    }
                }
                else
                {
                    throw new Exception($"Error: Method '{methodName}' has not been declared." + Environment.NewLine + "Expected format: 'method <methodName> (<optionalVariable>, <optionalVariable>, ...)'");
                }

            }
            // Check if it is declaring Variables
            else if (parts.Length == 3 && parts[1] == "=")
            {

                if (int.TryParse(parts[2], out int argValue))
                {
                    if (Dictionaries.variables.TryGetValue(command, out int oldVarValue))
                    {
                        if (draw)
                        {
                            // Update the existing variable
                            Dictionaries.variables[command] = int.Parse(parts[2]);
                        }
                    }
                    else
                    {
                        if (draw)
                        {
                            Dictionaries.variables.Add(parts[0], int.Parse(parts[2]));
                        }
                    }
                }
                else if (Dictionaries.variables.TryGetValue(parts[2], out int newVarValue))
                {
                    if (Dictionaries.variables.TryGetValue(command, out int oldVarValue))
                    {
                        if (draw)
                        {
                            // Update the existing variable
                            Dictionaries.variables[command] = newVarValue;
                        }
                    }
                    else
                    {
                        if (draw)
                        {
                            Dictionaries.variables.Add(parts[0], newVarValue);
                        }
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
                            if (draw)
                            {
                                // Update the existing variable
                                Dictionaries.variables[command] = int.Parse(resultString);
                            }
                        }
                        else
                        {
                            if (draw)
                            {
                                Dictionaries.variables.Add(parts[0], int.Parse(resultString));
                            }
                        }
                    }
                }
            }
            else if (command == "endmethod")
            {
                throw new Exception($"Error: '{command}' is an invalid command. The method has already been Declared, or the Declaration has not begun.");
            }
            else
            {
                throw new Exception($"Error: '{command}' is an Unknown command.");
            }
        }

        private void buildingMethod(string trimmedCommand, bool draw)
        {
            string[] parts = trimmedCommand.Split(' ');

            if (parts[0] != "endmethod")
            {
                methodLines.Add(trimmedCommand);
            }
            else if (parts[0] == "endmethod")
            {
                buildingMethodFlag = false;
                Dictionaries.methodLines[methodName] = new List<string>(methodLines);
            }
            if (!draw)
            {
                Dictionaries.methodLines.Remove(methodName);
                Dictionaries.methods.Remove(methodName);
            }

        }

        private void loopsAnalyses(string trimmedCommand, bool draw)
        {
            string[] parts = trimmedCommand.Split(' ');

            if (parts[0] != "endloop")
            {
                loopCommands.Add(trimmedCommand);
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
                    isLoopExCorrect = validateExpression(equation);

                    if (isLoopExCorrect)
                    {
                        foreach (String loopLine in loopCommands)
                        {
                            if (!ifFlag || ifFlagFirst)
                            {
                                string loopTrimmedCommand = loopLine.Trim(' ').ToLower();
                                analyses(loopTrimmedCommand, draw);
                            }
                            else
                            {
                                string loopTrimmedCommand = loopLine.Trim(' ').ToLower();
                                ifAnalyses(loopTrimmedCommand, draw);
                            }
                        }
                    }
                    numberOfCycles++;
                }
                while (isLoopExCorrect && numberOfCycles < 100);

                if (numberOfCycles > 100)
                {
                    loopFlag = false;
                    loopFlagFirst = false;
                    throw new Exception($"Error: Loop command has ended automatically after 100 cycles");
                }
                else
                {
                    loopFlag = false;
                    loopFlagFirst = false;
                }
            }
        }

        private void ifAnalyses(string trimmedCommand, bool draw)
        {
            string[] parts = trimmedCommand.Split(' ');

            if (parts[0] != "endif")
            {
                ifCommands.Add(trimmedCommand);
            }
            else if (parts[0] == "endif")
            {
                string e1 = "";
                string e2 = "";
                bool isIfExCorrect;
                bool e1Assigned = false;
                bool e2Assigned = false;

                try
                {
                    e1 = Dictionaries.variables[ifArgs[0]].ToString();
                    e1Assigned = true;
                }
                catch (Exception)
                {
                    if (!e1Assigned)
                    {
                        e1 = ifArgs[0];
                        e1Assigned = true;
                    }
                }
                try
                {
                    e2 = Dictionaries.variables[ifArgs[2]].ToString();
                    e2Assigned = true;
                }
                catch (Exception)
                {
                    if (!e2Assigned)
                    {
                        e2 = ifArgs[2];
                        e2Assigned = true;
                    }
                }
                string equation = e1 + " " + ifArgs[1] + " " + e2;
                isIfExCorrect = validateExpression(equation);

                if (isIfExCorrect)
                {
                    foreach (String ifLine in ifCommands)
                    {
                        if (!loopFlag || loopFlagFirst)
                        {
                            string ifTrimmedCommand = ifLine.Trim(' ').ToLower();
                            analyses(ifTrimmedCommand, draw);
                        }
                        else
                        {
                            string ifTrimmedCommand = ifLine.Trim(' ').ToLower();
                            loopsAnalyses(ifTrimmedCommand, draw);

                        }
                    }
                }
                ifFlag = false;
                ifFlagFirst = false;
            }
        }

        private bool IsValidMethodDeclaration(string methodDeclaration)
        {
            // Define a regular expression for the expected format
            string pattern = @"^\w+\s+\w+\s*\(\s*(\w+\s*(,\s*\w+\s*)*)?\s*\)$";

            // Use Regex.IsMatch to check if the input matches the pattern
            return Regex.IsMatch(methodDeclaration, pattern);
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

