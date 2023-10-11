using System;
using System.Drawing;
using System.Windows.Controls;
using System.IO;
using System.Windows.Forms;
using CommandLine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows;
using System.Security.Policy;

namespace ASE_Project
{
    public partial class Form1 : Form
    {
        const int CanvasBitmapWidth = 850;
        const int CanvasBitmapHeight = 750;

        Bitmap canvasBitmap = new Bitmap(CanvasBitmapWidth, CanvasBitmapHeight);
        Canvas paintingCanvas;
        ShapeFactory commandFactory;
        Graphics g;
        Parser parser;

        public Form1()
        {
            InitializeComponent();
            paintingCanvas = new Canvas(Graphics.FromImage(canvasBitmap), this);
            commandFactory = new ShapeFactory();
            parser = Parser.getParser();
            parser.setCanvas(paintingCanvas);
            updateCursorPositionLabel(Canvas.posX, Canvas.posY);
            updateFillStatusLabel(Canvas.fill);
            updatePenColourStatusLabel(Canvas.penColour);
            paintingCanvas.idicateCursor();
        }

        private void drawPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImageUnscaled(canvasBitmap, 0, 0);
        }
        /// <summary>
        /// Sends instructions to analyse (parse) the command (if not empty) when runButton is pressed. CommandTextBox has priority
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void runButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(commandTextBox.Text) && commandLineBox.Text.ToLower().Equals("run"))
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
                    throw new Exception("Error: No command entered");
                }
            }
            catch (Exception ex)
            {
                Dictionaries.errorMessages.Add(ex.Message);

                ErrorMessageForm errorMessageForm = new ErrorMessageForm();
                errorMessageForm.SetErrorMessages(Dictionaries.errorMessages);
                errorMessageForm.ShowDialog();
            }
        }
        /// <summary>
        /// Sends instructions to analyse the command when Enter is pressed when in CommandLineBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void commandLineBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (commandLineBox.Text.ToLower().Equals("run"))
                    {
                        parser.parseCommand(commandTextBox.Lines);
                        Refresh();
                    }
                    else
                    {
                        parser.parseCommand(commandLineBox.Lines);
                        Refresh();
                    }
                }
                catch (Exception ex)
                {
                    Dictionaries.errorMessages.Add(ex.Message);

                    ErrorMessageForm errorMessageForm = new ErrorMessageForm();
                    errorMessageForm.SetErrorMessages(Dictionaries.errorMessages);
                    errorMessageForm.ShowDialog();
                }
            }
        }

        private void commandTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            /*
            if (e.KeyCode == Keys.Enter)
            {
                if (commandLineBox.Text.ToLower().Equals("run"))
                {
                    parser.parseCommand(commandTextBox.Lines);
                    Refresh();
                }
            }*/
        }

        public void updateCursorPositionLabel(int posX, int posY)
        {
            cursorPositionLabel.Text = "Cursor Position: X = " + posX + ", Y = " + posY;
        }
        public void updateFillStatusLabel(bool fillStatus)
        {
            string fillTextStatus;
            if (!fillStatus)
            {
                fillTextStatus = "Off";
            }
            else
            {
                fillTextStatus = "On";
            }
            fillStatusLabel.Text = "Fill: " + fillTextStatus;
        }
        public void updatePenColourStatusLabel(Color colourStatus)
        {
            penColourStatusLabel.Text = "Colour: " + colourStatus.Name;
        }
    }
}
