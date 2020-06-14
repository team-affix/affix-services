namespace Affix_Center.Classes
{
    partial class Page_HostPersonalServer
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
            this.dialog_Header1 = new Affix_Center.Dialog_Header();
            this.SuspendLayout();
            // 
            // dialog_Header1
            // 
            this.dialog_Header1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dialog_Header1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.dialog_Header1.Location = new System.Drawing.Point(0, 0);
            this.dialog_Header1.Name = "dialog_Header1";
            this.dialog_Header1.Size = new System.Drawing.Size(785, 100);
            this.dialog_Header1.Subtitle = "Host a personal server";
            this.dialog_Header1.TabIndex = 0;
            this.dialog_Header1.Title = "Host";
            // 
            // Page_HostPersonalServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.dialog_Header1);
            this.Name = "Page_HostPersonalServer";
            this.Size = new System.Drawing.Size(785, 667);
            this.ResumeLayout(false);

        }

        #endregion

        private Dialog_Header dialog_Header1;
    }
}
