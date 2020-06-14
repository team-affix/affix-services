namespace Affix_Center.Forms
{
    partial class Form_AuthenticateAccount
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
            System.Drawing.StringFormat stringFormat1 = new System.Drawing.StringFormat();
            System.Drawing.StringFormat stringFormat2 = new System.Drawing.StringFormat();
            this.btnAuthenticateAccount = new Affix_Center.CustomControls.RoundedButton();
            this.btnRegisterAccount = new Affix_Center.CustomControls.RoundedButton();
            this.dsp = new UILayout.DisplayPanel();
            this.hdr = new Affix_Center.Dialog_Header();
            this.SuspendLayout();
            // 
            // btnAuthenticateAccount
            // 
            this.btnAuthenticateAccount.BorderWidth = 2;
            this.btnAuthenticateAccount.ForeColor = System.Drawing.Color.DimGray;
            this.btnAuthenticateAccount.Location = new System.Drawing.Point(12, 106);
            this.btnAuthenticateAccount.Name = "btnAuthenticateAccount";
            this.btnAuthenticateAccount.Radius = 5;
            this.btnAuthenticateAccount.Size = new System.Drawing.Size(149, 47);
            stringFormat1.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat1.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat1.Trimming = System.Drawing.StringTrimming.Character;
            this.btnAuthenticateAccount.StringFormat = stringFormat1;
            this.btnAuthenticateAccount.TabIndex = 7;
            this.btnAuthenticateAccount.Text = "Authenticate Account";
            this.btnAuthenticateAccount.TextColor = System.Drawing.Color.DimGray;
            this.btnAuthenticateAccount.UseVisualStyleBackColor = true;
            this.btnAuthenticateAccount.Click += new System.EventHandler(this.btnAuthenticateAccount_Click);
            this.btnAuthenticateAccount.MouseEnter += new System.EventHandler(this.btnRegisterAccount_MouseEnter);
            this.btnAuthenticateAccount.MouseLeave += new System.EventHandler(this.btnRegisterAccount_MouseLeave);
            // 
            // btnRegisterAccount
            // 
            this.btnRegisterAccount.BorderWidth = 2;
            this.btnRegisterAccount.ForeColor = System.Drawing.Color.DimGray;
            this.btnRegisterAccount.Location = new System.Drawing.Point(12, 159);
            this.btnRegisterAccount.Name = "btnRegisterAccount";
            this.btnRegisterAccount.Radius = 5;
            this.btnRegisterAccount.Size = new System.Drawing.Size(149, 47);
            stringFormat2.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat2.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat2.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat2.Trimming = System.Drawing.StringTrimming.Character;
            this.btnRegisterAccount.StringFormat = stringFormat2;
            this.btnRegisterAccount.TabIndex = 6;
            this.btnRegisterAccount.Text = "Register Account";
            this.btnRegisterAccount.TextColor = System.Drawing.Color.DimGray;
            this.btnRegisterAccount.UseVisualStyleBackColor = true;
            this.btnRegisterAccount.Click += new System.EventHandler(this.btnRegisterAccount_Click);
            this.btnRegisterAccount.MouseEnter += new System.EventHandler(this.btnRegisterAccount_MouseEnter);
            this.btnRegisterAccount.MouseLeave += new System.EventHandler(this.btnRegisterAccount_MouseLeave);
            // 
            // dsp
            // 
            this.dsp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dsp.Location = new System.Drawing.Point(170, 106);
            this.dsp.Name = "dsp";
            this.dsp.Size = new System.Drawing.Size(648, 616);
            this.dsp.TabIndex = 8;
            // 
            // hdr
            // 
            this.hdr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hdr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.hdr.Location = new System.Drawing.Point(-3, 0);
            this.hdr.Name = "hdr";
            this.hdr.Size = new System.Drawing.Size(836, 100);
            this.hdr.Subtitle = "Authenticate your Affix Services account, or create one";
            this.hdr.TabIndex = 0;
            this.hdr.Title = "Authenticate Account";
            // 
            // Form_AuthenticateAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(830, 734);
            this.Controls.Add(this.btnAuthenticateAccount);
            this.Controls.Add(this.btnRegisterAccount);
            this.Controls.Add(this.dsp);
            this.Controls.Add(this.hdr);
            this.Name = "Form_AuthenticateAccount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form_AuthenticateAccount_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Dialog_Header hdr;
        private CustomControls.RoundedButton btnRegisterAccount;
        private CustomControls.RoundedButton btnAuthenticateAccount;
        private UILayout.DisplayPanel dsp;
    }
}