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
using System.Linq;
using System.Collections;
using FontStyle = System.Drawing.FontStyle;

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
            displaySavedVar();
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
                if (!string.IsNullOrEmpty(commandTextBox.Text))
                {
                    parser.parseCommand(commandTextBox.Lines, draw);
                    Refresh();
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
                if (!string.IsNullOrEmpty(commandTextBox.Text))
                {
                    parser.parseCommand(commandTextBox.Lines, draw);
                    Refresh();
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
        /// On Save button click user is promted to save the written program
        /// </summary>
        /// <param name="sender">Object that sent to event</param>
        /// <param name="e">Event args</param>
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(commandTextBox.Text))
            {
                string directory = getSaveDirectory();
                if (directory != "")
                {
                    saveToTXT(directory, commandTextBox.Lines);
                }
            }
            else
            {
                const string message = "The command text box is empty - Enter program to be saved";
                const string caption = "Unable to save Program";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// On closure of the app the app will prompt a user to save their code and acts accordingly
        /// </summary>
        /// <param name="e">Event args</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!string.IsNullOrEmpty(commandTextBox.Text))
            {
                if (saveProgramDialog() == true)
                {
                    string directory = getSaveDirectory();
                    if (directory != "")
                    {
                        saveToTXT(directory, commandTextBox.Lines);
                    }
                }
            }
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
        public string getSaveDirectory()
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

        /// <summary>
        /// On Load button click user is promted to choose a file to be loaded to the GUI
        /// </summary>
        /// <param name="sender">Object that sent to event</param>
        /// <param name="e">Event args</param>
        private void loadButton_Click(object sender, EventArgs e)
        {
            string directory = getLoadDirectory();
            if (directory != "")
            {
                string[] textFromFile = loadFromTXT(directory);
                commandTextBox.Text = "";
                foreach (string command in textFromFile)
                {
                    commandTextBox.AppendText(command + Environment.NewLine);
                }
            }
        }

        /// <summary>
        /// An Open File dialog appears for a user to select the file to be loaded
        /// </summary>
        /// <returns></returns>
        public string getLoadDirectory()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileName;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Reads all lines stored in the choosen file
        /// </summary>
        /// <param name="directory">Directory of the file being read</param>
        /// <returns>Array of strings saved in the file</returns>
        public string[] loadFromTXT(string directory)
        {
            return File.ReadAllLines(directory).ToArray();
        }

        private void DeleteVar_Button_Click(object sender, EventArgs e)
        {
            restoreVar();
        }

        public void restoreVar()
        {
            Dictionaries.methodLines.Clear();
            Dictionaries.methods.Clear();
            Dictionaries.variables.Clear();
            displaySavedVar();
        }

        public void displaySavedVar()
        {
            declaredVarTextBox1.Clear();
            declaredVarTextBox1.SelectionFont = new Font(declaredVarTextBox1.Font, FontStyle.Bold);
            declaredVarTextBox1.AppendText("Variables:\r\n");
            int v = 0;

            foreach (var kvp in Dictionaries.variables)
            {
                v++;
                declaredVarTextBox1.AppendText($"{v}) {kvp.Key} = {kvp.Value}\r\n");
            }

            declaredVarTextBox1.SelectionFont = new Font(declaredVarTextBox1.Font, FontStyle.Bold);
            declaredVarTextBox1.AppendText("\r\nMethods:\r\n");

            int m = 0;
            foreach (var kvp in Dictionaries.methods)
            {
                m++;
                declaredVarTextBox1.AppendText($"{m}) {kvp.Key} - {kvp.Value}\r\n");

                int l = 0;
                // Display method lines
                foreach (var line in Dictionaries.methodLines[kvp.Key])
                {
                    l++;
                    declaredVarTextBox1.SelectionFont = new Font(declaredVarTextBox1.Font, FontStyle.Italic);
                    declaredVarTextBox1.AppendText($"Line {l})     {line}\r\n");
                }
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(1380, 575);
        }
    }
}

