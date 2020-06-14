namespace Affix_Center
{
    partial class AddPhysicalToken
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddPhysicalToken));
            this.lblAddPhysicalTokensDESC = new System.Windows.Forms.Label();
            this.lblAddPhysicalTokensTitle = new System.Windows.Forms.Label();
            this.pnlDrivesHolder = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pnlSelector = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // lblAddPhysicalTokensDESC
            // 
            this.lblAddPhysicalTokensDESC.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblAddPhysicalTokensDESC.AutoSize = true;
            this.lblAddPhysicalTokensDESC.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.lblAddPhysicalTokensDESC.Location = new System.Drawing.Point(30, 89);
            this.lblAddPhysicalTokensDESC.Name = "lblAddPhysicalTokensDESC";
            this.lblAddPhysicalTokensDESC.Size = new System.Drawing.Size(336, 42);
            this.lblAddPhysicalTokensDESC.TabIndex = 7;
            this.lblAddPhysicalTokensDESC.Text = "Add a drive on your computer which will act as a \r\ntoken for all access to your a" +
    "ccount in the future.";
            // 
            // lblAddPhysicalTokensTitle
            // 
            this.lblAddPhysicalTokensTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblAddPhysicalTokensTitle.AutoSize = true;
            this.lblAddPhysicalTokensTitle.Font = new System.Drawing.Font("Roboto", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddPhysicalTokensTitle.ForeColor = System.Drawing.Color.LightGray;
            this.lblAddPhysicalTokensTitle.Location = new System.Drawing.Point(27, 50);
            this.lblAddPhysicalTokensTitle.Name = "lblAddPhysicalTokensTitle";
            this.lblAddPhysicalTokensTitle.Size = new System.Drawing.Size(309, 38);
            this.lblAddPhysicalTokensTitle.TabIndex = 6;
            this.lblAddPhysicalTokensTitle.Text = "Edit Physical Tokens";
            // 
            // pnlDrivesHolder
            // 
            this.pnlDrivesHolder.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlDrivesHolder.Location = new System.Drawing.Point(34, 147);
            this.pnlDrivesHolder.Name = "pnlDrivesHolder";
            this.pnlDrivesHolder.Size = new System.Drawing.Size(732, 254);
            this.pnlDrivesHolder.TabIndex = 10;
            // 
            // panel5
            // 
            this.panel5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel5.BackgroundImage = global::Affix_Center.Properties.Resources.AddTokens;
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel5.Location = new System.Drawing.Point(670, 50);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(96, 91);
            this.panel5.TabIndex = 11;
            // 
            // pnlSelector
            // 
            this.pnlSelector.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlSelector.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.pnlSelector.Location = new System.Drawing.Point(12, 147);
            this.pnlSelector.Name = "pnlSelector";
            this.pnlSelector.Size = new System.Drawing.Size(16, 50);
            this.pnlSelector.TabIndex = 12;
            // 
            // AddPhysicalToken
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlSelector);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.pnlDrivesHolder);
            this.Controls.Add(this.lblAddPhysicalTokensDESC);
            this.Controls.Add(this.lblAddPhysicalTokensTitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddPhysicalToken";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.AddPhysicalToken_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAddPhysicalTokensDESC;
        private System.Windows.Forms.Label lblAddPhysicalTokensTitle;
        private System.Windows.Forms.Panel pnlDrivesHolder;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel pnlSelector;
    }
}