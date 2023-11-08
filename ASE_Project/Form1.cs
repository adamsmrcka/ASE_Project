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

        public Bitmap canvasBitmap = new Bitmap(CanvasBitmapWidth, CanvasBitmapHeight);
        Canvas paintingCanvas;
        Graphics g;
        Parser parser;

        /// <summary>
        /// Constructor. Initializes component. Gets Parser instances. Sets Graphics context for Canvas. Initializes GUI Label elements 
        /// </summary>
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
        /// <summary>
        /// Paints the contents of the Bitmap onto the Canvas
        /// </summary>
        /// <param name="sender">Object that sent to event</param>
        /// <param name="e">Event args</param>
        private void drawPanel_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            g.DrawImageUnscaled(canvasBitmap, 0, 0);
        }
        /// <summary>
        /// Sends instructions to analyse (parse) the command (if not empty) when runButton is pressed. CommandTextBox has priority.
        /// Catches error messages and displays dialog
        /// </summary>
        /// <param name="sender">Object that sent to event</param>
        /// <param name="e">Event args</param>
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
            if (parser.errors != 0)
            {
                ErrorMessageForm errorMessageForm = new ErrorMessageForm();
                errorMessageForm.setErrorMessages(Dictionaries.errorMessages);
                errorMessageForm.ShowDialog();
            }
        }
        /// <summary>
        /// Sends instructions to analyse the command when Enter is pressed when in CommandLineBox
        /// If CommandLineBox is "run" the commandTextBox is analysed
        /// Else the CommandLineBox is run.
        /// Catches error messages and displays dialog.
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
                if (parser.errors != 0)
                {
                    ErrorMessageForm errorMessageForm = new ErrorMessageForm();
                    errorMessageForm.setErrorMessages(Dictionaries.errorMessages);
                    errorMessageForm.ShowDialog();
                }
            }
        }
        /// <summary>
        /// Updates the cursor position label on the form
        /// </summary>
        /// <param name="posX">The x position of the cursor</param>
        /// <param name="posY">The y position of the cursor</param>
        public void updateCursorPositionLabel(int posX, int posY)
        {
            cursorPositionLabel.Text = "Cursor Position: X = " + posX + ", Y = " + posY;
        }

        /// <summary>
        /// Updates the fill status label on the form
        /// </summary>
        /// <param name="fillStatus">Boolean of the fill status entered by the user</param>
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
        /// <summary>
        /// Updates the pen colour label on the form
        /// </summary>
        /// <param name="colourStatus">Current pen colour used</param>
        public void updatePenColourStatusLabel(Color colourStatus)
        {
            penColourStatusLabel.Text = "Colour: " + colourStatus.Name;
        }

        /// <summary>
        /// On Syntax button click commands syntax is validated and all errors are displayed
        /// </summary>
        /// <param name="sender">Object that sent to event</param>
        /// <param name="e">Event args</param>
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
                    throw new Exception("Error: You need to enter 'Run' into 'Simple Command Box' in order to successfully run the command");
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
                if (parser.errors == 0)
                {
                    ErrorMessageForm errorMessageForm = new ErrorMessageForm();
                    errorMessageForm.setLabelMessage("Command is correct");
                    List<string> errorMessage = new List<string>();
                    errorMessage.Add("No errors found. You can run this command");
                    errorMessageForm.setErrorMessages(errorMessage);
                    errorMessageForm.ShowDialog();
                }
                else
                {
                    ErrorMessageForm errorMessageForm = new ErrorMessageForm();
                    errorMessageForm.setErrorMessages(Dictionaries.errorMessages);
                    errorMessageForm.ShowDialog();
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
        /// On closure of the app the app will prompt a user to save their code and acts accordingly
        /// </summary>
        /// <param name="e">Event args</param>
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
        /// <summary>
        /// Prompts user via message box to save users program code
        /// </summary>
        /// <returns>Bool with users response</returns>
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
        /// <summary>
        /// A save dialog appears for a user to select directory and file name for the file
        /// </summary>
        /// <returns>File destination</returns>
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

        /// <summary>
        /// Saves all lines stored in the command text box to a file
        /// </summary>
        /// <param name="directory"> The user selected location(directory for the file)</param>
        /// <param name="saveText"> The text being saved</param>
        public void saveToTXT(string directory, string[] saveText)
        {
            File.WriteAllLines(directory, saveText);
        }
    }
}

