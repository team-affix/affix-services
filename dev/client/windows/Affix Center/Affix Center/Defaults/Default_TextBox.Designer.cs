namespace Affix_Center.Classes
{
    partial class Default_TextBox
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel = new Affix_Center.CustomControls.RoundedPanel();
            this.textBox = new System.Windows.Forms.TextBox();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.BorderWidth = 2;
            this.panel.Controls.Add(this.textBox);
            this.panel.ForeColor = System.Drawing.Color.White;
            this.panel.Location = new System.Drawing.Point(3, 4);
            this.panel.Name = "panel";
            this.panel.Radius = 5;
            this.panel.Size = new System.Drawing.Size(486, 39);
            this.panel.TabIndex = 2;
            this.panel.Click += new System.EventHandler(this.Default_TextBox_Click);
            // 
            // textBox
            // 
            this.textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBox.ForeColor = System.Drawing.Color.DimGray;
            this.textBox.Location = new System.Drawing.Point(19, 9);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(448, 22);
            this.textBox.TabIndex = 0;
            this.textBox.Click += new System.EventHandler(this.Default_TextBox_Click);
            // 
            // Default_TextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Controls.Add(this.panel);
            this.Name = "Default_TextBox";
            this.Size = new System.Drawing.Size(492, 50);
            this.Click += new System.EventHandler(this.Default_TextBox_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Default_TextBox_Paint);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TextBox textBox;
        public CustomControls.RoundedPanel panel;
    }
}
