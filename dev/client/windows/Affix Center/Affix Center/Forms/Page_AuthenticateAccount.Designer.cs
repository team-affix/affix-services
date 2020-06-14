namespace Affix_Center.Forms
{
    partial class Page_AuthenticateAccount
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
            System.Drawing.StringFormat stringFormat1 = new System.Drawing.StringFormat();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnAuthenticate = new Affix_Center.CustomControls.RoundedButton();
            this.txtPassword = new Affix_Center.Classes.Default_TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtEmailAddress = new Affix_Center.Classes.Default_TextBox();
            this.lblEmailAddress = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Light", 14F);
            this.lblTitle.ForeColor = System.Drawing.Color.LightGray;
            this.lblTitle.Location = new System.Drawing.Point(71, 122);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(174, 25);
            this.lblTitle.TabIndex = 19;
            this.lblTitle.Text = "Authenticate Access:";
            // 
            // btnAuthenticate
            // 
            this.btnAuthenticate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAuthenticate.BorderWidth = 2;
            this.btnAuthenticate.ForeColor = System.Drawing.Color.DimGray;
            this.btnAuthenticate.Location = new System.Drawing.Point(194, 331);
            this.btnAuthenticate.Name = "btnAuthenticate";
            this.btnAuthenticate.Radius = 5;
            this.btnAuthenticate.Size = new System.Drawing.Size(195, 42);
            stringFormat1.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat1.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat1.Trimming = System.Drawing.StringTrimming.Character;
            this.btnAuthenticate.StringFormat = stringFormat1;
            this.btnAuthenticate.TabIndex = 16;
            this.btnAuthenticate.Text = "Authenticate";
            this.btnAuthenticate.TextColor = System.Drawing.Color.DimGray;
            this.btnAuthenticate.UseVisualStyleBackColor = true;
            this.btnAuthenticate.Click += new System.EventHandler(this.btnAuthenticate_Click);
            this.btnAuthenticate.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
            this.btnAuthenticate.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.txtPassword.Location = new System.Drawing.Point(89, 261);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(422, 50);
            this.txtPassword.TabIndex = 13;
            this.txtPassword.Load += new System.EventHandler(this.txtPassword_Load);
            this.txtPassword.Enter += new System.EventHandler(this.txt_Enter);
            this.txtPassword.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // lblPassword
            // 
            this.lblPassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblPassword.ForeColor = System.Drawing.Color.DimGray;
            this.lblPassword.Location = new System.Drawing.Point(72, 237);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(79, 21);
            this.lblPassword.TabIndex = 17;
            this.lblPassword.Text = "Password:";
            // 
            // txtEmailAddress
            // 
            this.txtEmailAddress.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtEmailAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.txtEmailAddress.Location = new System.Drawing.Point(89, 184);
            this.txtEmailAddress.Name = "txtEmailAddress";
            this.txtEmailAddress.Size = new System.Drawing.Size(422, 50);
            this.txtEmailAddress.TabIndex = 12;
            this.txtEmailAddress.Enter += new System.EventHandler(this.txt_Enter);
            this.txtEmailAddress.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // lblEmailAddress
            // 
            this.lblEmailAddress.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEmailAddress.AutoSize = true;
            this.lblEmailAddress.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblEmailAddress.ForeColor = System.Drawing.Color.DimGray;
            this.lblEmailAddress.Location = new System.Drawing.Point(72, 160);
            this.lblEmailAddress.Name = "lblEmailAddress";
            this.lblEmailAddress.Size = new System.Drawing.Size(111, 21);
            this.lblEmailAddress.TabIndex = 14;
            this.lblEmailAddress.Text = "Email Address:";
            // 
            // Page_AuthenticateAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnAuthenticate);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtEmailAddress);
            this.Controls.Add(this.lblEmailAddress);
            this.Name = "Page_AuthenticateAccount";
            this.Size = new System.Drawing.Size(583, 670);
            this.Load += new System.EventHandler(this.Page_AuthenticateAccount_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private CustomControls.RoundedButton btnAuthenticate;
        private Classes.Default_TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private Classes.Default_TextBox txtEmailAddress;
        private System.Windows.Forms.Label lblEmailAddress;
    }
}
