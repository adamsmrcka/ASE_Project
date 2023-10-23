namespace ASE_Project
{
    partial class Form1
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            this.runButton = new System.Windows.Forms.Button();
            this.syntaxButton = new System.Windows.Forms.Button();
            this.commandTextBox = new System.Windows.Forms.TextBox();
            this.commandLineBox = new System.Windows.Forms.TextBox();
            this.complexCommandLabel = new System.Windows.Forms.Label();
            this.simpleCommandLabel = new System.Windows.Forms.Label();
            this.drawPanel = new System.Windows.Forms.PictureBox();
            this.cursorPositionLabel = new System.Windows.Forms.Label();
            this.penColourStatusLabel = new System.Windows.Forms.Label();
            this.fillStatusLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.drawPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(113, 508);
            this.runButton.Margin = new System.Windows.Forms.Padding(2);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(93, 22);
            this.runButton.TabIndex = 2;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // syntaxButton
            // 
            this.syntaxButton.Location = new System.Drawing.Point(242, 508);
            this.syntaxButton.Margin = new System.Windows.Forms.Padding(2);
            this.syntaxButton.Name = "syntaxButton";
            this.syntaxButton.Size = new System.Drawing.Size(93, 22);
            this.syntaxButton.TabIndex = 3;
            this.syntaxButton.Text = "Syntax";
            this.syntaxButton.UseVisualStyleBackColor = true;
            this.syntaxButton.Click += new System.EventHandler(this.syntaxButton_Click);
            // 
            // commandTextBox
            // 
            this.commandTextBox.Location = new System.Drawing.Point(23, 33);
            this.commandTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.commandTextBox.Multiline = true;
            this.commandTextBox.Name = "commandTextBox";
            this.commandTextBox.Size = new System.Drawing.Size(405, 423);
            this.commandTextBox.TabIndex = 0;
            // 
            // commandLineBox
            // 
            this.commandLineBox.Location = new System.Drawing.Point(23, 484);
            this.commandLineBox.Margin = new System.Windows.Forms.Padding(2);
            this.commandLineBox.Name = "commandLineBox";
            this.commandLineBox.Size = new System.Drawing.Size(405, 20);
            this.commandLineBox.TabIndex = 1;
            this.commandLineBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.commandLineBox_KeyDown);
            // 
            // complexCommandLabel
            // 
            this.complexCommandLabel.AutoSize = true;
            this.complexCommandLabel.Location = new System.Drawing.Point(167, 9);
            this.complexCommandLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.complexCommandLabel.Name = "complexCommandLabel";
            this.complexCommandLabel.Size = new System.Drawing.Size(117, 13);
            this.complexCommandLabel.TabIndex = 5;
            this.complexCommandLabel.Text = "Enter Complex Program";
            // 
            // simpleCommandLabel
            // 
            this.simpleCommandLabel.AutoSize = true;
            this.simpleCommandLabel.Location = new System.Drawing.Point(166, 465);
            this.simpleCommandLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.simpleCommandLabel.Name = "simpleCommandLabel";
            this.simpleCommandLabel.Size = new System.Drawing.Size(116, 13);
            this.simpleCommandLabel.TabIndex = 6;
            this.simpleCommandLabel.Text = "Enter Simple Command";
            // 
            // drawPanel
            // 
            this.drawPanel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.drawPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.drawPanel.Location = new System.Drawing.Point(456, 6);
            this.drawPanel.Margin = new System.Windows.Forms.Padding(2);
            this.drawPanel.Name = "drawPanel";
            this.drawPanel.Size = new System.Drawing.Size(580, 490);
            this.drawPanel.TabIndex = 7;
            this.drawPanel.TabStop = false;
            this.drawPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.drawPanel_Paint);
            // 
            // cursorPositionLabel
            // 
            this.cursorPositionLabel.AutoSize = true;
            this.cursorPositionLabel.Location = new System.Drawing.Point(876, 502);
            this.cursorPositionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.cursorPositionLabel.Name = "cursorPositionLabel";
            this.cursorPositionLabel.Size = new System.Drawing.Size(148, 13);
            this.cursorPositionLabel.TabIndex = 8;
            this.cursorPositionLabel.Text = "Cursor Position: X = 10 Y = 10";
            // 
            // penColourStatusLabel
            // 
            this.penColourStatusLabel.AutoSize = true;
            this.penColourStatusLabel.Location = new System.Drawing.Point(453, 502);
            this.penColourStatusLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.penColourStatusLabel.Name = "penColourStatusLabel";
            this.penColourStatusLabel.Size = new System.Drawing.Size(70, 13);
            this.penColourStatusLabel.TabIndex = 9;
            this.penColourStatusLabel.Text = "Colour: Black";
            // 
            // fillStatusLabel
            // 
            this.fillStatusLabel.AutoSize = true;
            this.fillStatusLabel.Location = new System.Drawing.Point(453, 515);
            this.fillStatusLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.fillStatusLabel.Name = "fillStatusLabel";
            this.fillStatusLabel.Size = new System.Drawing.Size(39, 13);
            this.fillStatusLabel.TabIndex = 10;
            this.fillStatusLabel.Text = "Fill: Off";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1045, 534);
            this.Controls.Add(this.fillStatusLabel);
            this.Controls.Add(this.penColourStatusLabel);
            this.Controls.Add(this.cursorPositionLabel);
            this.Controls.Add(this.drawPanel);
            this.Controls.Add(this.simpleCommandLabel);
            this.Controls.Add(this.complexCommandLabel);
            this.Controls.Add(this.commandLineBox);
            this.Controls.Add(this.commandTextBox);
            this.Controls.Add(this.syntaxButton);
            this.Controls.Add(this.runButton);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.drawPanel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.Button syntaxButton;
        private System.Windows.Forms.TextBox commandTextBox;
        private System.Windows.Forms.TextBox commandLineBox;
        private System.Windows.Forms.Label complexCommandLabel;
        private System.Windows.Forms.Label simpleCommandLabel;
        private System.Windows.Forms.PictureBox drawPanel;
        private System.Windows.Forms.Label cursorPositionLabel;
        private System.Windows.Forms.Label penColourStatusLabel;
        private System.Windows.Forms.Label fillStatusLabel;
    }
}

