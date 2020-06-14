namespace Affix_Center
{
    partial class Lockdown_Account
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lockdown_Account));
            this.lblLockDown = new System.Windows.Forms.Label();
            this.pnlLockDown = new System.Windows.Forms.Panel();
            this.lblSettingsDesc = new System.Windows.Forms.Label();
            this.lblSettingsTitle = new System.Windows.Forms.Label();
            this.pnlLockDown.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblLockDown
            // 
            this.lblLockDown.BackColor = System.Drawing.Color.Transparent;
            this.lblLockDown.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLockDown.ForeColor = System.Drawing.Color.White;
            this.lblLockDown.Location = new System.Drawing.Point(0, 0);
            this.lblLockDown.Name = "lblLockDown";
            this.lblLockDown.Size = new System.Drawing.Size(265, 55);
            this.lblLockDown.TabIndex = 0;
            this.lblLockDown.Text = "Initiate Lockdown";
            this.lblLockDown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLockDown.MouseEnter += new System.EventHandler(this.lblLockDown_MouseEnter);
            this.lblLockDown.MouseLeave += new System.EventHandler(this.lblLockDown_MouseLeave);
            // 
            // pnlLockDown
            // 
            this.pnlLockDown.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlLockDown.BackgroundImage = global::Affix_Center.Properties.Resources.Lockdown;
            this.pnlLockDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlLockDown.Controls.Add(this.lblLockDown);
            this.pnlLockDown.Location = new System.Drawing.Point(98, 178);
            this.pnlLockDown.Name = "pnlLockDown";
            this.pnlLockDown.Size = new System.Drawing.Size(265, 55);
            this.pnlLockDown.TabIndex = 1;
            // 
            // lblSettingsDesc
            // 
            this.lblSettingsDesc.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSettingsDesc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSettingsDesc.ForeColor = System.Drawing.Color.White;
            this.lblSettingsDesc.Location = new System.Drawing.Point(37, 71);
            this.lblSettingsDesc.Name = "lblSettingsDesc";
            this.lblSettingsDesc.Size = new System.Drawing.Size(389, 77);
            this.lblSettingsDesc.TabIndex = 7;
            this.lblSettingsDesc.Text = "Warning: upon use of the Lockdown Procedure, you will not have rights to accessin" +
    "g your account from anywhere until you decide to unlock it using your Third-Laye" +
    "r encryption key.";
            this.lblSettingsDesc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSettingsTitle
            // 
            this.lblSettingsTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSettingsTitle.AutoSize = true;
            this.lblSettingsTitle.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.lblSettingsTitle.ForeColor = System.Drawing.Color.Coral;
            this.lblSettingsTitle.Location = new System.Drawing.Point(35, 40);
            this.lblSettingsTitle.Name = "lblSettingsTitle";
            this.lblSettingsTitle.Size = new System.Drawing.Size(281, 31);
            this.lblSettingsTitle.TabIndex = 6;
            this.lblSettingsTitle.Text = "Lock Down your Affiliation";
            this.lblSettingsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Lockdown_Account
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(460, 272);
            this.Controls.Add(this.lblSettingsDesc);
            this.Controls.Add(this.lblSettingsTitle);
            this.Controls.Add(this.pnlLockDown);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Lockdown_Account";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.pnlLockDown.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLockDown;
        private System.Windows.Forms.Panel pnlLockDown;
        private System.Windows.Forms.Label lblSettingsDesc;
        private System.Windows.Forms.Label lblSettingsTitle;
    }
}