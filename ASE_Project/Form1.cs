using System;
using System.Drawing;
using System.Windows.Controls;
using System.IO;
using System.Windows.Forms;
using CommandLine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows;
using System.Security.Policy;
using System.Collections.Generic;
using MessageBox = System.Windows.Forms.MessageBox;

namespace ASE_Project
{
    public partial class Form1 : Form
    {
        const int CanvasBitmapWidth = 580;
        const int CanvasBitmapHeight = 490;
        private bool draw = false;

        Bitmap canvasBitmap = new Bitmap(CanvasBitmapWidth, CanvasBitmapHeight);
        Canvas paintingCanvas;
        ShapeFactory commandFactory;
        Graphics g;
        Parser parser;

        public Form1()
        {
            InitializeComponent();
            paintingCanvas = new Canvas(Graphics.FromImage(canvasBitmap), this);
            parser = Parser.getParser();
            parser.setCanvas(paintingCanvas);
            updateCursorPositionLabel(Canvas.posX, Canvas.posY);
            updateFillStatusLabel(Canvas.fill);
            updatePenColourStatusLabel(Canvas.penColour);
            paintingCanvas.idicateCursor();
        }

        private void drawPanel_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            g.DrawImageUnscaled(canvasBitmap, 0, 0);
        }
        /// <summary>
        /// Sends instructions to analyse (parse) the command (if not empty) when runButton is pressed. CommandTextBox has priority
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void runButton_Click(object sender, EventArgs e)
        {
            draw = true;
            try
            {
                if (!string.IsNullOrEmpty(commandTextBox.Text) && commandLineBox.Text.ToLower().Equals("run"))
                {
                    parser.parseCommand(commandTextBox.Lines, draw);
                    Refresh();
                }
                else if (!string.IsNullOrEmpty(commandLineBox.Text))
                {
                    parser.parseCommand(commandLineBox.Lines, draw);
                    Refresh();
                }
                else if (!string.IsNullOrEmpty(commandTextBox.Text) && !commandLineBox.Text.ToLower().Equals("run"))
                {
                    draw = false;
                    parser.parseCommand(commandTextBox.Lines, draw);
                    Refresh();
                    throw new Exception("You need to enter 'Run' into the 'Simple Command Box' in order to successfully run the command");
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
                errorMessageForm.setErrorMessages(Dictionaries.errorMessages);
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
                draw = true;
                try
                {
                    if (commandLineBox.Text.ToLower().Equals("run"))
                    {
                        parser.parseCommand(commandTextBox.Lines, draw);
                        Refresh();
                    }
                    else
                    {
                        parser.parseCommand(commandLineBox.Lines, draw);
                        Refresh();
                    }
                }
                catch (Exception ex)
                {
                    Dictionaries.errorMessages.Add(ex.Message);

                    ErrorMessageForm errorMessageForm = new ErrorMessageForm();
                    errorMessageForm.setErrorMessages(Dictionaries.errorMessages);
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

        private void syntaxButton_Click(object sender, EventArgs e)
        {
            draw = false;
            try
            {
                if (!string.IsNullOrEmpty(commandTextBox.Text) && commandLineBox.Text.ToLower().Equals("run"))
                {
                    parser.parseCommand(commandTextBox.Lines, draw);
                    Refresh();
                }
                else if (!string.IsNullOrEmpty(commandTextBox.Text) && !commandLineBox.Text.ToLower().Equals("run"))
                {
                    parser.parseCommand(commandTextBox.Lines, draw);
                    Refresh();
                    throw new Exception("Error: The command syntax is correct BUT you need to enter 'Run' into 'Simple Command Box' in order to successfully run the command");
                }
                else if (!string.IsNullOrEmpty(commandLineBox.Text))
                {
                    parser.parseCommand(commandLineBox.Lines, draw);
                    Refresh();
                }
                else
                {
                    throw new Exception("Error: No command entered");
                }
                ErrorMessageForm errorMessageForm = new ErrorMessageForm();
                errorMessageForm.setLabelMessage("Command is correct");
                List<string> errorMessage = new List<string>();
                errorMessage.Add("No errors found. You can run this command");
                errorMessageForm.setErrorMessages(errorMessage);
                errorMessageForm.ShowDialog();

            }
            catch (Exception ex)
            {
                Dictionaries.errorMessages.Add(ex.Message);
                ErrorMessageForm errorMessageForm = new ErrorMessageForm();
                errorMessageForm.setErrorMessages(Dictionaries.errorMessages);
                errorMessageForm.ShowDialog();
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (saveProgramDialog() == true)
            {
                string directory = getDirectory();
                if (directory != "")
                {
                    saveToTXT(directory, commandTextBox.Lines);
                }
            };
        }

        public static bool saveProgramDialog()
        {
            const string message = "Would you like to save your drawing program before you leave?";
            const string caption = "Save Program";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                return true;
            else
                return false;
        }

        public string getDirectory()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                return saveFileDialog.FileName;
            }
            else
            {
                return "";
            }
        }

        public void saveToTXT(string directory, string[] saveText)
        {
            File.WriteAllLines(directory, saveText);
        }
    }
}

