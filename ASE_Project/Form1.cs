using System;
using System.Drawing;
using System.Windows.Controls;
using System.IO;
using System.Windows.Forms;
using CommandLine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
        Parser parser;

        public Form1()
        {
            InitializeComponent();
            paintingCanvas = new Canvas(Graphics.FromImage(canvasBitmap));
            commandFactory = new CommandFactory();
            parser = Parser.getParser();
            parser.setCanvas(paintingCanvas);
        }

        private void drawPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImageUnscaled(canvasBitmap, 0, 0);
        }
        private void runButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(commandTextBox.Text))
            {
                parser.parseCommand(commandTextBox.Lines);
                Refresh();
            }
            else if (!string.IsNullOrEmpty(commandLineBox.Text))
            {
                parser.parseCommand(commandLineBox.Lines);
                Refresh();
            }
            else
            {
                throw new Exception("No command entered");
            }
        }
        
        private void commandLineBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                parser.parseCommand(commandLineBox.Lines);
                Refresh();
            }
        }

        private void commandTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                parser.parseCommand(commandTextBox.Lines);
                Refresh();
            }

        }
    }
}
