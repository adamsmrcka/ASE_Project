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
        public ErrorMessageForm()
        {
            InitializeComponent();
        }
        public void SetErrorMessages(List<string> errorMessages)
        {
            string errorMessageText = string.Join(Environment.NewLine, errorMessages);

            // Set the text of the TextBox to display the error messages
            richTextBox1.Text = errorMessageText;
        }
        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
            Dictionaries.errorMessages.Clear();
        }
    }
}
