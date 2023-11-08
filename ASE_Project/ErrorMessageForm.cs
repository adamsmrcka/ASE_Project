using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASE_Project
{
    public partial class ErrorMessageForm : Form
    {
        /// <summary>
        /// Initilize component
        /// </summary>
        public ErrorMessageForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Changes the title of the error message form
        /// </summary>
        /// <param name="label">The title of the error message form </param>
        public void setLabelMessage(string label)
        {
            string labelText = label;

            // Set the text of the TextBox to change the title of the error messages
            label1.Text = labelText;
        }
        /// <summary>
        /// Changes the text of the error message form
        /// </summary>
        /// <param name="errorMessages">List of Error messages</param>
        public void setErrorMessages(List<string> errorMessages)
        {
            string errorMessageText = string.Join(Environment.NewLine, errorMessages);

            // Set the text of the TextBox to display the error messages
            richTextBox1.Text = errorMessageText;
        }
        /// <summary>
        /// Closes the error message dialog, clears all error messages
        /// </summary>
        /// <param name="sender">Object that sent to event</param>
        /// <param name="e">Event args</param>
        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
            Dictionaries.errorMessages.Clear();
        }
    }
}
