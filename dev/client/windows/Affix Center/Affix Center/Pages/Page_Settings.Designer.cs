namespace Affix_Center.Forms
{
    partial class Page_Settings
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
            this.dsp = new UILayout.DisplayPanel();
            this.lstOptions = new Affix_Center.Pages.Dialog_List();
            this.SuspendLayout();
            // 
            // hdr
            // 
            this.hdr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hdr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.hdr.Location = new System.Drawing.Point(0, 0);
            this.hdr.Name = "hdr";
            this.hdr.Size = new System.Drawing.Size(1147, 100);
            this.hdr.Subtitle = "Settings for Affix Center, your Affiliation, and your Machine";
            this.hdr.TabIndex = 0;
            this.hdr.Title = "Settings";
            // 
            // dsp
            // 
            this.dsp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dsp.Location = new System.Drawing.Point(233, 106);
            this.dsp.Name = "dsp";
            this.dsp.Size = new System.Drawing.Size(911, 664);
            this.dsp.TabIndex = 13;
            // 
            // lstOptions
            // 
            this.lstOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstOptions.AutoScroll = true;
            this.lstOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.lstOptions.Location = new System.Drawing.Point(0, 106);
            this.lstOptions.Name = "lstOptions";
            this.lstOptions.Size = new System.Drawing.Size(227, 605);
            this.lstOptions.TabIndex = 0;
            // 
            // Page_Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Controls.Add(this.lstOptions);
            this.Controls.Add(this.dsp);
            this.Controls.Add(this.hdr);
            this.Name = "Page_Settings";
            this.Size = new System.Drawing.Size(1147, 773);
            this.Load += new System.EventHandler(this.Page_Settings_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Dialog_Header hdr;
        private UILayout.DisplayPanel dsp;
        private Pages.Dialog_List lstOptions;
    }
}
