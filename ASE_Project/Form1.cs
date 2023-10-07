using System;
using System.Drawing;
using System.Windows.Controls;
using System.IO;
using System.Windows.Forms;
using CommandLine;

namespace ASE_Project
{
    public partial class Form1 : Form
    {

        const int CanvasBitmapWidth = 850;
        const int CanvasBitmapHeight = 750;

        Bitmap canvasBitmap = new Bitmap(CanvasBitmapWidth, CanvasBitmapHeight);
        Canvas paintingCanvas;
        CommandFactory commandFactory;
        Graphics g;

        public Form1()
        {
            InitializeComponent();
            paintingCanvas = new Canvas(Graphics.FromImage(canvasBitmap));
            g = paintingCanvas.GetGraphics();
            commandFactory = new CommandFactory();
        }
        private void runButton_Click(object sender, EventArgs e)
        {
            String Command = commandLineBox.Text.Trim().ToLower();
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
        }

            private void drawPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImageUnscaled(canvasBitmap, 0, 0);
        }
    }
}
