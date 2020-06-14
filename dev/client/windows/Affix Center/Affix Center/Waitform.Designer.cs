namespace Affix_Center
{
    partial class Waitform
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Waitform));
            this.lblLoadingDesc = new System.Windows.Forms.Label();
            this.lblLoadingTitle = new System.Windows.Forms.Label();
            this.pnlLoadingBar = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // lblLoadingDesc
            // 
            this.lblLoadingDesc.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLoadingDesc.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoadingDesc.Location = new System.Drawing.Point(51, 196);
            this.lblLoadingDesc.Name = "lblLoadingDesc";
            this.lblLoadingDesc.Size = new System.Drawing.Size(449, 58);
            this.lblLoadingDesc.TabIndex = 3;
            this.lblLoadingDesc.Text = "We\'re going as fast as we can. ";
            this.lblLoadingDesc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLoadingTitle
            // 
            this.lblLoadingTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLoadingTitle.Font = new System.Drawing.Font("Segoe UI Light", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoadingTitle.Location = new System.Drawing.Point(134, 136);
            this.lblLoadingTitle.Name = "lblLoadingTitle";
            this.lblLoadingTitle.Size = new System.Drawing.Size(282, 60);
            this.lblLoadingTitle.TabIndex = 2;
            this.lblLoadingTitle.Text = "Loading; please wait.";
            this.lblLoadingTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlLoadingBar
            // 
            this.pnlLoadingBar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlLoadingBar.BackColor = System.Drawing.Color.DodgerBlue;
            this.pnlLoadingBar.Location = new System.Drawing.Point(178, 294);
            this.pnlLoadingBar.Name = "pnlLoadingBar";
            this.pnlLoadingBar.Size = new System.Drawing.Size(200, 10);
            this.pnlLoadingBar.TabIndex = 4;
            // 
            // Waitform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(556, 461);
            this.Controls.Add(this.pnlLoadingBar);
            this.Controls.Add(this.lblLoadingDesc);
            this.Controls.Add(this.lblLoadingTitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Waitform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Waitform_FormClosing);
            this.Load += new System.EventHandler(this.Waitform_Load);
            this.Shown += new System.EventHandler(this.Waitform_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblLoadingDesc;
        private System.Windows.Forms.Label lblLoadingTitle;
        private System.Windows.Forms.Panel pnlLoadingBar;
    }
}