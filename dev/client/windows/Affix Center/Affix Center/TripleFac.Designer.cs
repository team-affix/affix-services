namespace Affix_Center
{
    partial class TripleFac
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TripleFac));
            this.txt3FA = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.pnlLoginUsername = new System.Windows.Forms.Panel();
            this.lblSettingsDesc = new System.Windows.Forms.Label();
            this.lblSettingsTitle = new System.Windows.Forms.Label();
            this.pnlEncrypt = new System.Windows.Forms.Panel();
            this.lblEncrypt = new System.Windows.Forms.Label();
            this.pnlEncrypt.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt3FA
            // 
            this.txt3FA.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txt3FA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txt3FA.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt3FA.Font = new System.Drawing.Font("Segoe UI Light", 14F);
            this.txt3FA.ForeColor = System.Drawing.Color.Coral;
            this.txt3FA.Location = new System.Drawing.Point(222, 211);
            this.txt3FA.Name = "txt3FA";
            this.txt3FA.PasswordChar = '-';
            this.txt3FA.Size = new System.Drawing.Size(462, 25);
            this.txt3FA.TabIndex = 13;
            this.txt3FA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt3FA_KeyDown);
            // 
            // lblUsername
            // 
            this.lblUsername.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.lblUsername.ForeColor = System.Drawing.Color.White;
            this.lblUsername.Location = new System.Drawing.Point(117, 211);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(99, 31);
            this.lblUsername.TabIndex = 16;
            this.lblUsername.Text = "LFA Code:";
            this.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlLoginUsername
            // 
            this.pnlLoginUsername.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlLoginUsername.BackgroundImage = global::Affix_Center.Properties.Resources.TextBoxWhite__1_;
            this.pnlLoginUsername.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlLoginUsername.Location = new System.Drawing.Point(222, 242);
            this.pnlLoginUsername.Name = "pnlLoginUsername";
            this.pnlLoginUsername.Size = new System.Drawing.Size(462, 12);
            this.pnlLoginUsername.TabIndex = 15;
            // 
            // lblSettingsDesc
            // 
            this.lblSettingsDesc.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSettingsDesc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSettingsDesc.ForeColor = System.Drawing.Color.White;
            this.lblSettingsDesc.Location = new System.Drawing.Point(218, 150);
            this.lblSettingsDesc.Name = "lblSettingsDesc";
            this.lblSettingsDesc.Size = new System.Drawing.Size(333, 43);
            this.lblSettingsDesc.TabIndex = 21;
            this.lblSettingsDesc.Text = "Enter the local authentication code for this machine.";
            this.lblSettingsDesc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSettingsTitle
            // 
            this.lblSettingsTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSettingsTitle.AutoSize = true;
            this.lblSettingsTitle.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.lblSettingsTitle.ForeColor = System.Drawing.Color.Coral;
            this.lblSettingsTitle.Location = new System.Drawing.Point(216, 119);
            this.lblSettingsTitle.Name = "lblSettingsTitle";
            this.lblSettingsTitle.Size = new System.Drawing.Size(245, 31);
            this.lblSettingsTitle.TabIndex = 20;
            this.lblSettingsTitle.Text = "Access this Installation";
            this.lblSettingsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlEncrypt
            // 
            this.pnlEncrypt.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlEncrypt.BackgroundImage = global::Affix_Center.Properties.Resources.Lockdown;
            this.pnlEncrypt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlEncrypt.Controls.Add(this.lblEncrypt);
            this.pnlEncrypt.Location = new System.Drawing.Point(268, 277);
            this.pnlEncrypt.Name = "pnlEncrypt";
            this.pnlEncrypt.Size = new System.Drawing.Size(265, 55);
            this.pnlEncrypt.TabIndex = 22;
            // 
            // lblEncrypt
            // 
            this.lblEncrypt.BackColor = System.Drawing.Color.Transparent;
            this.lblEncrypt.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEncrypt.ForeColor = System.Drawing.Color.White;
            this.lblEncrypt.Location = new System.Drawing.Point(0, 0);
            this.lblEncrypt.Name = "lblEncrypt";
            this.lblEncrypt.Size = new System.Drawing.Size(265, 55);
            this.lblEncrypt.TabIndex = 0;
            this.lblEncrypt.Text = "Access my Local Data";
            this.lblEncrypt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEncrypt.Click += new System.EventHandler(this.lblSignInButton_Click);
            this.lblEncrypt.MouseEnter += new System.EventHandler(this.lblSignInButton_MouseEnter);
            this.lblEncrypt.MouseLeave += new System.EventHandler(this.lblSignInButton_MouseLeave);
            // 
            // TripleFac
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlEncrypt);
            this.Controls.Add(this.lblSettingsDesc);
            this.Controls.Add(this.lblSettingsTitle);
            this.Controls.Add(this.txt3FA);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.pnlLoginUsername);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TripleFac";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TripleFac_FormClosing);
            this.Load += new System.EventHandler(this.TripleFac_Load);
            this.pnlEncrypt.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txt3FA;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Panel pnlLoginUsername;
        private System.Windows.Forms.Label lblSettingsDesc;
        private System.Windows.Forms.Label lblSettingsTitle;
        private System.Windows.Forms.Panel pnlEncrypt;
        private System.Windows.Forms.Label lblEncrypt;
    }
}