namespace Affix_Center.Classes
{
    partial class Page_ServerHome
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
            this.hdr = new Affix_Center.Dialog_Header();
            this.SuspendLayout();
            // 
            // hdr
            // 
            this.hdr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.hdr.Location = new System.Drawing.Point(0, 0);
            this.hdr.Name = "hdr";
            this.hdr.Size = new System.Drawing.Size(926, 100);
            this.hdr.Subtitle = "Configure and Maintain your Server";
            this.hdr.TabIndex = 0;
            this.hdr.Title = "Server Home";
            // 
            // Page_ServerHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Controls.Add(this.hdr);
            this.Name = "Page_ServerHome";
            this.Size = new System.Drawing.Size(1055, 481);
            this.Load += new System.EventHandler(this.Page_ServerHome_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Dialog_Header hdr;
    }
}
