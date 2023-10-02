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
            ((System.ComponentModel.ISupportInitialize)(this.drawPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(169, 508);
            this.runButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(93, 22);
            this.runButton.TabIndex = 0;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // syntaxButton
            // 
            this.syntaxButton.Location = new System.Drawing.Point(532, 465);
            this.syntaxButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.syntaxButton.Name = "syntaxButton";
            this.syntaxButton.Size = new System.Drawing.Size(93, 22);
            this.syntaxButton.TabIndex = 1;
            this.syntaxButton.Text = "Syntax";
            this.syntaxButton.UseVisualStyleBackColor = true;
            // 
            // commandTextBox
            // 
            this.commandTextBox.Location = new System.Drawing.Point(23, 37);
            this.commandTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.commandTextBox.Multiline = true;
            this.commandTextBox.Name = "commandTextBox";
            this.commandTextBox.Size = new System.Drawing.Size(405, 413);
            this.commandTextBox.TabIndex = 2;
            // 
            // commandLineBox
            // 
            this.commandLineBox.Location = new System.Drawing.Point(23, 484);
            this.commandLineBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.commandLineBox.Name = "commandLineBox";
            this.commandLineBox.Size = new System.Drawing.Size(405, 20);
            this.commandLineBox.TabIndex = 3;
            // 
            // complexCommandLabel
            // 
            this.complexCommandLabel.AutoSize = true;
            this.complexCommandLabel.Location = new System.Drawing.Point(166, 19);
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
            this.drawPanel.Location = new System.Drawing.Point(456, 13);
            this.drawPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.drawPanel.Name = "drawPanel";
            this.drawPanel.Size = new System.Drawing.Size(568, 489);
            this.drawPanel.TabIndex = 7;
            this.drawPanel.TabStop = false;
            this.drawPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.drawPanel_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1045, 534);
            this.Controls.Add(this.drawPanel);
            this.Controls.Add(this.simpleCommandLabel);
            this.Controls.Add(this.complexCommandLabel);
            this.Controls.Add(this.commandLineBox);
            this.Controls.Add(this.commandTextBox);
            this.Controls.Add(this.syntaxButton);
            this.Controls.Add(this.runButton);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
    }
}

