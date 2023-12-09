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
            this.saveButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.declaredVarTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DeleteVar_Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.drawPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(34, 785);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(140, 34);
            this.runButton.TabIndex = 2;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // syntaxButton
            // 
            this.syntaxButton.Location = new System.Drawing.Point(189, 785);
            this.syntaxButton.Name = "syntaxButton";
            this.syntaxButton.Size = new System.Drawing.Size(140, 34);
            this.syntaxButton.TabIndex = 3;
            this.syntaxButton.Text = "Syntax";
            this.syntaxButton.UseVisualStyleBackColor = true;
            this.syntaxButton.Click += new System.EventHandler(this.syntaxButton_Click);
            // 
            // commandTextBox
            // 
            this.commandTextBox.Location = new System.Drawing.Point(34, 51);
            this.commandTextBox.Multiline = true;
            this.commandTextBox.Name = "commandTextBox";
            this.commandTextBox.Size = new System.Drawing.Size(606, 649);
            this.commandTextBox.TabIndex = 0;
            // 
            // commandLineBox
            // 
            this.commandLineBox.Location = new System.Drawing.Point(34, 745);
            this.commandLineBox.Name = "commandLineBox";
            this.commandLineBox.Size = new System.Drawing.Size(606, 26);
            this.commandLineBox.TabIndex = 1;
            this.commandLineBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.commandLineBox_KeyDown);
            // 
            // complexCommandLabel
            // 
            this.complexCommandLabel.AutoSize = true;
            this.complexCommandLabel.Location = new System.Drawing.Point(250, 14);
            this.complexCommandLabel.Name = "complexCommandLabel";
            this.complexCommandLabel.Size = new System.Drawing.Size(177, 20);
            this.complexCommandLabel.TabIndex = 5;
            this.complexCommandLabel.Text = "Enter Complex Program";
            // 
            // simpleCommandLabel
            // 
            this.simpleCommandLabel.AutoSize = true;
            this.simpleCommandLabel.Location = new System.Drawing.Point(249, 715);
            this.simpleCommandLabel.Name = "simpleCommandLabel";
            this.simpleCommandLabel.Size = new System.Drawing.Size(177, 20);
            this.simpleCommandLabel.TabIndex = 6;
            this.simpleCommandLabel.Text = "Enter Simple Command";
            // 
            // drawPanel
            // 
            this.drawPanel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.drawPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.drawPanel.Location = new System.Drawing.Point(684, 9);
            this.drawPanel.Name = "drawPanel";
            this.drawPanel.Size = new System.Drawing.Size(868, 752);
            this.drawPanel.TabIndex = 7;
            this.drawPanel.TabStop = false;
            this.drawPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.drawPanel_Paint);
            // 
            // cursorPositionLabel
            // 
            this.cursorPositionLabel.AutoSize = true;
            this.cursorPositionLabel.Location = new System.Drawing.Point(1314, 772);
            this.cursorPositionLabel.Name = "cursorPositionLabel";
            this.cursorPositionLabel.Size = new System.Drawing.Size(220, 20);
            this.cursorPositionLabel.TabIndex = 8;
            this.cursorPositionLabel.Text = "Cursor Position: X = 10 Y = 10";
            // 
            // penColourStatusLabel
            // 
            this.penColourStatusLabel.AutoSize = true;
            this.penColourStatusLabel.Location = new System.Drawing.Point(680, 772);
            this.penColourStatusLabel.Name = "penColourStatusLabel";
            this.penColourStatusLabel.Size = new System.Drawing.Size(102, 20);
            this.penColourStatusLabel.TabIndex = 9;
            this.penColourStatusLabel.Text = "Colour: Black";
            // 
            // fillStatusLabel
            // 
            this.fillStatusLabel.AutoSize = true;
            this.fillStatusLabel.Location = new System.Drawing.Point(680, 792);
            this.fillStatusLabel.Name = "fillStatusLabel";
            this.fillStatusLabel.Size = new System.Drawing.Size(58, 20);
            this.fillStatusLabel.TabIndex = 10;
            this.fillStatusLabel.Text = "Fill: Off";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(345, 785);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(140, 34);
            this.saveButton.TabIndex = 11;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(500, 785);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(140, 34);
            this.loadButton.TabIndex = 12;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // declaredVarTextBox1
            // 
            this.declaredVarTextBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.declaredVarTextBox1.Location = new System.Drawing.Point(1574, 52);
            this.declaredVarTextBox1.Name = "declaredVarTextBox1";
            this.declaredVarTextBox1.ReadOnly = true;
            this.declaredVarTextBox1.Size = new System.Drawing.Size(438, 709);
            this.declaredVarTextBox1.TabIndex = 13;
            this.declaredVarTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1666, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "Declared variables/methods";
            // 
            // DeleteVar_Button
            // 
            this.DeleteVar_Button.Location = new System.Drawing.Point(1702, 785);
            this.DeleteVar_Button.Name = "DeleteVar_Button";
            this.DeleteVar_Button.Size = new System.Drawing.Size(182, 34);
            this.DeleteVar_Button.TabIndex = 15;
            this.DeleteVar_Button.Text = "Delete All Variables";
            this.DeleteVar_Button.UseVisualStyleBackColor = true;
            this.DeleteVar_Button.Click += new System.EventHandler(this.DeleteVar_Button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(2028, 824);
            this.Controls.Add(this.DeleteVar_Button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.declaredVarTextBox1);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.saveButton);
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
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
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
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.RichTextBox declaredVarTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button DeleteVar_Button;
    }
}

