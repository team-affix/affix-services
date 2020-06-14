namespace Affix_Center
{
    partial class PhysicalTokenRegistration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PhysicalTokenRegistration));
            this.lblRegisteringTokens = new System.Windows.Forms.Label();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // lblRegisteringTokens
            // 
            this.lblRegisteringTokens.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblRegisteringTokens.AutoSize = true;
            this.lblRegisteringTokens.Font = new System.Drawing.Font("Segoe UI Light", 15F);
            this.lblRegisteringTokens.Location = new System.Drawing.Point(313, 78);
            this.lblRegisteringTokens.Name = "lblRegisteringTokens";
            this.lblRegisteringTokens.Size = new System.Drawing.Size(308, 28);
            this.lblRegisteringTokens.TabIndex = 1;
            this.lblRegisteringTokens.Text = "Insert Token Disks for this affiliation.";
            // 
            // pnlStatus
            // 
            this.pnlStatus.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlStatus.BackColor = System.Drawing.Color.Transparent;
            this.pnlStatus.BackgroundImage = global::Affix_Center.Properties.Resources.CheckingPhysicalTokens;
            this.pnlStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlStatus.Location = new System.Drawing.Point(317, 127);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(300, 300);
            this.pnlStatus.TabIndex = 0;
            // 
            // PhysicalTokenRegistration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 504);
            this.Controls.Add(this.lblRegisteringTokens);
            this.Controls.Add(this.pnlStatus);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PhysicalTokenRegistration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.PhysicalTokenRegistration_Load);
            this.Shown += new System.EventHandler(this.PhysicalTokenRegistration_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.Label lblRegisteringTokens;
    }
}