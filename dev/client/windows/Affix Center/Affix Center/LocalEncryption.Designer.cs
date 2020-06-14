namespace Affix_Center
{
    partial class LocalEncryption
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocalEncryption));
            this.lblSettingsDesc = new System.Windows.Forms.Label();
            this.lblSettingsTitle = new System.Windows.Forms.Label();
            this.pnlEncrypt = new System.Windows.Forms.Panel();
            this.lblEncrypt = new System.Windows.Forms.Label();
            this.txt3FA = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.pnlLoginUsername = new System.Windows.Forms.Panel();
            this.txt3FAConfirm = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDisregard = new System.Windows.Forms.Label();
            this.pnlEncrypt.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSettingsDesc
            // 
            this.lblSettingsDesc.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSettingsDesc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSettingsDesc.ForeColor = System.Drawing.Color.White;
            this.lblSettingsDesc.Location = new System.Drawing.Point(206, 83);
            this.lblSettingsDesc.Name = "lblSettingsDesc";
            this.lblSettingsDesc.Size = new System.Drawing.Size(389, 43);
            this.lblSettingsDesc.TabIndex = 10;
            this.lblSettingsDesc.Text = "Encrypt this installation of Affix  Center with a local key that you decide upon." +
    "";
            this.lblSettingsDesc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSettingsTitle
            // 
            this.lblSettingsTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSettingsTitle.AutoSize = true;
            this.lblSettingsTitle.Font = new System.Drawing.Font("Segoe UI", 17F);
            this.lblSettingsTitle.ForeColor = System.Drawing.Color.Coral;
            this.lblSettingsTitle.Location = new System.Drawing.Point(205, 39);
            this.lblSettingsTitle.Name = "lblSettingsTitle";
            this.lblSettingsTitle.Size = new System.Drawing.Size(253, 31);
            this.lblSettingsTitle.TabIndex = 9;
            this.lblSettingsTitle.Text = "Encrypt this Installation";
            this.lblSettingsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlEncrypt
            // 
            this.pnlEncrypt.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlEncrypt.BackgroundImage = global::Affix_Center.Properties.Resources.Lockdown;
            this.pnlEncrypt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlEncrypt.Controls.Add(this.lblEncrypt);
            this.pnlEncrypt.Location = new System.Drawing.Point(268, 318);
            this.pnlEncrypt.Name = "pnlEncrypt";
            this.pnlEncrypt.Size = new System.Drawing.Size(265, 55);
            this.pnlEncrypt.TabIndex = 8;
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
            this.lblEncrypt.Text = "Encrypt my Local Data";
            this.lblEncrypt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEncrypt.Click += new System.EventHandler(this.lblEncrypt_Click);
            this.lblEncrypt.MouseEnter += new System.EventHandler(this.lblEncrypt_MouseEnter);
            this.lblEncrypt.MouseLeave += new System.EventHandler(this.lblEncrypt_MouseLeave);
            // 
            // txt3FA
            // 
            this.txt3FA.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txt3FA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txt3FA.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt3FA.Font = new System.Drawing.Font("Segoe UI Light", 14F);
            this.txt3FA.ForeColor = System.Drawing.Color.Coral;
            this.txt3FA.Location = new System.Drawing.Point(222, 167);
            this.txt3FA.Name = "txt3FA";
            this.txt3FA.PasswordChar = '-';
            this.txt3FA.Size = new System.Drawing.Size(462, 25);
            this.txt3FA.TabIndex = 17;
            this.txt3FA.TextChanged += new System.EventHandler(this.txt3FA_TextChanged);
            // 
            // lblUsername
            // 
            this.lblUsername.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.lblUsername.ForeColor = System.Drawing.Color.White;
            this.lblUsername.Location = new System.Drawing.Point(117, 167);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(99, 31);
            this.lblUsername.TabIndex = 19;
            this.lblUsername.Text = "LFA Code:";
            this.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlLoginUsername
            // 
            this.pnlLoginUsername.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlLoginUsername.BackgroundImage = global::Affix_Center.Properties.Resources.TextBoxWhite__1_;
            this.pnlLoginUsername.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlLoginUsername.Location = new System.Drawing.Point(222, 198);
            this.pnlLoginUsername.Name = "pnlLoginUsername";
            this.pnlLoginUsername.Size = new System.Drawing.Size(462, 12);
            this.pnlLoginUsername.TabIndex = 18;
            // 
            // txt3FAConfirm
            // 
            this.txt3FAConfirm.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txt3FAConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.txt3FAConfirm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt3FAConfirm.Font = new System.Drawing.Font("Segoe UI Light", 14F);
            this.txt3FAConfirm.ForeColor = System.Drawing.Color.Coral;
            this.txt3FAConfirm.Location = new System.Drawing.Point(222, 228);
            this.txt3FAConfirm.Name = "txt3FAConfirm";
            this.txt3FAConfirm.PasswordChar = '-';
            this.txt3FAConfirm.Size = new System.Drawing.Size(462, 25);
            this.txt3FAConfirm.TabIndex = 20;
            this.txt3FAConfirm.TextChanged += new System.EventHandler(this.txt3FAConfirm_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(117, 228);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 31);
            this.label1.TabIndex = 22;
            this.label1.Text = "Confirm LFA:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BackgroundImage = global::Affix_Center.Properties.Resources.TextBoxWhite__1_;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Location = new System.Drawing.Point(222, 259);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(462, 12);
            this.panel1.TabIndex = 21;
            // 
            // lblDisregard
            // 
            this.lblDisregard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDisregard.BackColor = System.Drawing.Color.Transparent;
            this.lblDisregard.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisregard.ForeColor = System.Drawing.Color.White;
            this.lblDisregard.Location = new System.Drawing.Point(320, 376);
            this.lblDisregard.Name = "lblDisregard";
            this.lblDisregard.Size = new System.Drawing.Size(160, 55);
            this.lblDisregard.TabIndex = 1;
            this.lblDisregard.Text = "Don\'t Display this Again";
            this.lblDisregard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblDisregard.Click += new System.EventHandler(this.lblDisregard_Click);
            this.lblDisregard.MouseEnter += new System.EventHandler(this.lblDisregard_MouseEnter);
            this.lblDisregard.MouseLeave += new System.EventHandler(this.lblDisregard_MouseLeave);
            // 
            // LocalEncryption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(800, 470);
            this.Controls.Add(this.lblDisregard);
            this.Controls.Add(this.txt3FAConfirm);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txt3FA);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.pnlLoginUsername);
            this.Controls.Add(this.lblSettingsDesc);
            this.Controls.Add(this.lblSettingsTitle);
            this.Controls.Add(this.pnlEncrypt);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LocalEncryption";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.LocalEncryption_Load);
            this.pnlEncrypt.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSettingsDesc;
        private System.Windows.Forms.Label lblSettingsTitle;
        private System.Windows.Forms.Panel pnlEncrypt;
        private System.Windows.Forms.Label lblEncrypt;
        private System.Windows.Forms.TextBox txt3FA;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Panel pnlLoginUsername;
        private System.Windows.Forms.TextBox txt3FAConfirm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDisregard;
    }
}