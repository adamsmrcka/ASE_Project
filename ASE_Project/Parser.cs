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
using System.Xml.Linq;

namespace ASE_Project
{
    internal class Parser
    {
        CommandFactory commandFactory;
        String trimmedCommand;
        String[] parts;
        string[] arg;
        int[] arguments;
        String command;
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
                arg = parts.Skip(1).ToArray();
                arguments = Array.ConvertAll(arg, int.Parse);

                if (IsShape(command))
                {
                    Shape s = (Shape)commandFactory.GetShape(command);

                    canvas.DrawShape(s, Color.Red, Canvas.posX, Canvas.posY, arguments);
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
                            canvas.MoveTo(arguments);
                            break;
                    }
                }
            }
        }
        public bool IsShape(String cmd)
        {
            return shapes.Contains(cmd);
        }

        /* String Command = commandLineBox.Text.Trim().ToLower();
             if (Command.Equals("line") == true)
             {
                 paintingCanvas.DrawLine(1000, 1000);
             }
             else if (Command.Equals("move to") == true)
             {
                 Canvas.posX = 100;
                 Canvas.posY = 100;
             }
             else if (Command.Equals("circle") == true)
 {
     Shape s = (Shape)commandFactory.GetShape(Command);
     s.Set(Color.Red, Canvas.posX, Canvas.posY, 100);
     s.Draw(g);
 }
 else if (Command.Equals("triangle") == true)
 {
     Shape s = (Shape)commandFactory.GetShape(Command);
     s.SetPolygon(Color.Red, new Point[] { new Point(Canvas.posX, Canvas.posY), new Point(100, 200), new Point(200, 200) });
     s.Draw(g);
 }
 else if (Command.Equals("rectangle") == true)
 {
     Shape s = (Shape)commandFactory.GetShape(Command);
     s.Set(Color.Red, Canvas.posX, Canvas.posY, 100, 200);
     s.Draw(g);
 }
 else if (Command.Equals("clear") == true)
 {
     paintingCanvas.ClearCanvas();
 }
 else if (Command.Equals("reset") == true)
 {
     paintingCanvas.RestoreCanvas();
 }
 else
 {
     commandLineBox.Text = "Error";
 }
 Refresh();
     */
    }
}
