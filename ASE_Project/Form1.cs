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
                Canvas.DefaultPosX = 100;
                Canvas.DefaultPosY = 100;
            }
            else if (Command.Equals("circle") == true)
            {
                Shape s = (Shape)commandFactory.GetShape(Command);
                s.Set(Color.Red, 50, 50, 100);
                s.Draw(g);
            }
                commandLineBox.Text = "Done";
            Refresh();
        }

            private void drawPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImageUnscaled(canvasBitmap, 0, 0);
        }
    }
}
