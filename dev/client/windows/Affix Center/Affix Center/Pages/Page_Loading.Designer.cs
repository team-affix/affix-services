namespace Affix_Center.Forms
{
    partial class Page_Loading
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
            this.pnlLoading = new System.Windows.Forms.Panel();
            this.hdr = new Affix_Center.Dialog_Header();
            this.SuspendLayout();
            // 
            // pnlLoading
            // 
            this.pnlLoading.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlLoading.Location = new System.Drawing.Point(332, 124);
            this.pnlLoading.Name = "pnlLoading";
            this.pnlLoading.Size = new System.Drawing.Size(200, 200);
            this.pnlLoading.TabIndex = 3;
            // 
            // hdr
            // 
            this.hdr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hdr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.hdr.Location = new System.Drawing.Point(0, 0);
            this.hdr.Name = "hdr";
            this.hdr.Size = new System.Drawing.Size(865, 100);
            this.hdr.Subtitle = "We\'re loading things up; please wait.";
            this.hdr.TabIndex = 2;
            this.hdr.Title = "Loading";
            // 
            // Page_Loading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Controls.Add(this.pnlLoading);
            this.Controls.Add(this.hdr);
            this.Name = "Page_Loading";
            this.Size = new System.Drawing.Size(865, 449);
            this.Load += new System.EventHandler(this.Page_Loading_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLoading;
        private Dialog_Header hdr;
    }
}
