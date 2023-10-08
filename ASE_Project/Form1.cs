﻿using System;
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
        Parser parser;

        public Form1()
        {
            InitializeComponent();
            paintingCanvas = new Canvas(Graphics.FromImage(canvasBitmap));
            commandFactory = new CommandFactory();
            parser = Parser.GetParser();
            parser.SetCanvas(paintingCanvas);
        }
        private void runButton_Click(object sender, EventArgs e)
        {
            parser.parseCommand(commandLineBox.Lines);
            Refresh();
        }

        private void drawPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImageUnscaled(canvasBitmap, 0, 0);
        }
    }
}
