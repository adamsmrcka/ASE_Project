﻿namespace ASE_Project
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
            this.drawPanel = new System.Windows.Forms.PictureBox();
            this.complexCommandLabel = new System.Windows.Forms.Label();
            this.simpleCommandLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.drawPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(268, 776);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(139, 34);
            this.runButton.TabIndex = 0;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // syntaxButton
            // 
            this.syntaxButton.Location = new System.Drawing.Point(798, 716);
            this.syntaxButton.Name = "syntaxButton";
            this.syntaxButton.Size = new System.Drawing.Size(139, 34);
            this.syntaxButton.TabIndex = 1;
            this.syntaxButton.Text = "Syntax";
            this.syntaxButton.UseVisualStyleBackColor = true;
            // 
            // commandTextBox
            // 
            this.commandTextBox.Location = new System.Drawing.Point(34, 57);
            this.commandTextBox.Multiline = true;
            this.commandTextBox.Name = "commandTextBox";
            this.commandTextBox.Size = new System.Drawing.Size(606, 598);
            this.commandTextBox.TabIndex = 2;
            // 
            // commandLineBox
            // 
            this.commandLineBox.Location = new System.Drawing.Point(34, 744);
            this.commandLineBox.Name = "commandLineBox";
            this.commandLineBox.Size = new System.Drawing.Size(606, 26);
            this.commandLineBox.TabIndex = 3;
            // 
            // drawPanel
            // 
            this.drawPanel.BackColor = System.Drawing.SystemColors.Window;
            this.drawPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drawPanel.Cursor = System.Windows.Forms.Cursors.No;
            this.drawPanel.Location = new System.Drawing.Point(664, 29);
            this.drawPanel.Name = "drawPanel";
            this.drawPanel.Size = new System.Drawing.Size(875, 626);
            this.drawPanel.TabIndex = 4;
            this.drawPanel.TabStop = false;
            // 
            // complexCommandLabel
            // 
            this.complexCommandLabel.AutoSize = true;
            this.complexCommandLabel.Location = new System.Drawing.Point(249, 29);
            this.complexCommandLabel.Name = "complexCommandLabel";
            this.complexCommandLabel.Size = new System.Drawing.Size(190, 20);
            this.complexCommandLabel.TabIndex = 5;
            this.complexCommandLabel.Text = "Enter Complex Command";
            // 
            // simpleCommandLabel
            // 
            this.simpleCommandLabel.AutoSize = true;
            this.simpleCommandLabel.Location = new System.Drawing.Point(249, 716);
            this.simpleCommandLabel.Name = "simpleCommandLabel";
            this.simpleCommandLabel.Size = new System.Drawing.Size(177, 20);
            this.simpleCommandLabel.TabIndex = 6;
            this.simpleCommandLabel.Text = "Enter Simple Command";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1568, 822);
            this.Controls.Add(this.simpleCommandLabel);
            this.Controls.Add(this.complexCommandLabel);
            this.Controls.Add(this.drawPanel);
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
        private System.Windows.Forms.PictureBox drawPanel;
        private System.Windows.Forms.Label complexCommandLabel;
        private System.Windows.Forms.Label simpleCommandLabel;
    }
}

