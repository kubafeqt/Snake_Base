﻿namespace WinForms01
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            gamepanel = new Panel();
            SuspendLayout();
            // 
            // gamepanel
            // 
            gamepanel.Location = new Point(10, 10);
            gamepanel.Name = "gamepanel";
            gamepanel.Size = new Size(984, 589);
            gamepanel.TabIndex = 0;
            gamepanel.Paint += gamepanel_Paint;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1008, 610);
            Controls.Add(gamepanel);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            Resize += Form1_Resize;
            ResumeLayout(false);
        }

        #endregion

        private Panel gamepanel;
    }
}
