namespace Affix_Center.Classes
{
    partial class LeftMenu_AccountUnauthenticated
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
            this.roundedPanel1 = new Affix_Center.CustomControls.RoundedPanel();
            this.roundedPanel2 = new Affix_Center.CustomControls.RoundedPanel();
            this.SuspendLayout();
            // 
            // roundedPanel1
            // 
            this.roundedPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.roundedPanel1.BorderWidth = 4;
            this.roundedPanel1.ForeColor = System.Drawing.Color.White;
            this.roundedPanel1.Location = new System.Drawing.Point(108, 42);
            this.roundedPanel1.Name = "roundedPanel1";
            this.roundedPanel1.Radius = 5;
            this.roundedPanel1.Size = new System.Drawing.Size(50, 50);
            this.roundedPanel1.TabIndex = 0;
            // 
            // roundedPanel2
            // 
            this.roundedPanel2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.roundedPanel2.BorderWidth = 4;
            this.roundedPanel2.ForeColor = System.Drawing.Color.White;
            this.roundedPanel2.Location = new System.Drawing.Point(108, 98);
            this.roundedPanel2.Name = "roundedPanel2";
            this.roundedPanel2.Radius = 5;
            this.roundedPanel2.Size = new System.Drawing.Size(50, 50);
            this.roundedPanel2.TabIndex = 1;
            // 
            // LeftMenu_AccountUnauthenticated
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Controls.Add(this.roundedPanel2);
            this.Controls.Add(this.roundedPanel1);
            this.Name = "LeftMenu_AccountUnauthenticated";
            this.Size = new System.Drawing.Size(266, 693);
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControls.RoundedPanel roundedPanel1;
        private CustomControls.RoundedPanel roundedPanel2;
    }
}
