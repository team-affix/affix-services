namespace Affix_Center.Forms
{
    partial class Page_RegisterAccount
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
            System.Drawing.StringFormat stringFormat2 = new System.Drawing.StringFormat();
            this.lblAccountName = new System.Windows.Forms.Label();
            this.lblEmailAddress = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtName = new Affix_Center.Classes.Default_TextBox();
            this.btnRegister = new Affix_Center.CustomControls.RoundedButton();
            this.txtConfirmPassword = new Affix_Center.Classes.Default_TextBox();
            this.txtPassword = new Affix_Center.Classes.Default_TextBox();
            this.txtEmailAddress = new Affix_Center.Classes.Default_TextBox();
            this.btnAuthenticate = new Affix_Center.CustomControls.RoundedButton();
            this.SuspendLayout();
            // 
            // lblAccountName
            // 
            this.lblAccountName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblAccountName.AutoSize = true;
            this.lblAccountName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblAccountName.ForeColor = System.Drawing.Color.DimGray;
            this.lblAccountName.Location = new System.Drawing.Point(70, 180);
            this.lblAccountName.Name = "lblAccountName";
            this.lblAccountName.Size = new System.Drawing.Size(140, 21);
            this.lblAccountName.TabIndex = 0;
            this.lblAccountName.Text = "Name (Username):";
            // 
            // lblEmailAddress
            // 
            this.lblEmailAddress.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEmailAddress.AutoSize = true;
            this.lblEmailAddress.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblEmailAddress.ForeColor = System.Drawing.Color.DimGray;
            this.lblEmailAddress.Location = new System.Drawing.Point(70, 257);
            this.lblEmailAddress.Name = "lblEmailAddress";
            this.lblEmailAddress.Size = new System.Drawing.Size(111, 21);
            this.lblEmailAddress.TabIndex = 2;
            this.lblEmailAddress.Text = "Email Address:";
            // 
            // lblPassword
            // 
            this.lblPassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblPassword.ForeColor = System.Drawing.Color.DimGray;
            this.lblPassword.Location = new System.Drawing.Point(70, 334);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(79, 21);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Password:";
            this.lblPassword.Click += new System.EventHandler(this.lblPassword_Click);
            // 
            // lblConfirmPassword
            // 
            this.lblConfirmPassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblConfirmPassword.ForeColor = System.Drawing.Color.DimGray;
            this.lblConfirmPassword.Location = new System.Drawing.Point(70, 411);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.Size = new System.Drawing.Size(140, 21);
            this.lblConfirmPassword.TabIndex = 6;
            this.lblConfirmPassword.Text = "Confirm Password:";
            this.lblConfirmPassword.Click += new System.EventHandler(this.lblConfirmPassword_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Light", 14F);
            this.lblTitle.ForeColor = System.Drawing.Color.LightGray;
            this.lblTitle.Location = new System.Drawing.Point(69, 132);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(169, 25);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.Text = "Become an Affiliate:";
            // 
            // txtName
            // 
            this.txtName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.txtName.Location = new System.Drawing.Point(87, 204);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(422, 50);
            this.txtName.TabIndex = 0;
            this.txtName.Enter += new System.EventHandler(this.txt_Enter);
            this.txtName.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // btnRegister
            // 
            this.btnRegister.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRegister.BorderWidth = 2;
            this.btnRegister.ForeColor = System.Drawing.Color.DimGray;
            this.btnRegister.Location = new System.Drawing.Point(192, 539);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Radius = 5;
            this.btnRegister.Size = new System.Drawing.Size(195, 42);
            stringFormat1.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat1.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat1.Trimming = System.Drawing.StringTrimming.Character;
            this.btnRegister.StringFormat = stringFormat1;
            this.btnRegister.TabIndex = 4;
            this.btnRegister.Text = "Register";
            this.btnRegister.TextColor = System.Drawing.Color.DimGray;
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            this.btnRegister.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
            this.btnRegister.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtConfirmPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.txtConfirmPassword.Location = new System.Drawing.Point(87, 435);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.Size = new System.Drawing.Size(422, 50);
            this.txtConfirmPassword.TabIndex = 3;
            this.txtConfirmPassword.Load += new System.EventHandler(this.txtConfirmPassword_Load);
            this.txtConfirmPassword.Enter += new System.EventHandler(this.txt_Enter);
            this.txtConfirmPassword.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.txtPassword.Location = new System.Drawing.Point(87, 358);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(422, 50);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.Load += new System.EventHandler(this.txtPassword_Load);
            this.txtPassword.Enter += new System.EventHandler(this.txt_Enter);
            this.txtPassword.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // txtEmailAddress
            // 
            this.txtEmailAddress.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtEmailAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.txtEmailAddress.Location = new System.Drawing.Point(87, 281);
            this.txtEmailAddress.Name = "txtEmailAddress";
            this.txtEmailAddress.Size = new System.Drawing.Size(422, 50);
            this.txtEmailAddress.TabIndex = 1;
            this.txtEmailAddress.Enter += new System.EventHandler(this.txt_Enter);
            this.txtEmailAddress.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // btnAuthenticate
            // 
            this.btnAuthenticate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAuthenticate.BorderWidth = 2;
            this.btnAuthenticate.ForeColor = System.Drawing.Color.DimGray;
            this.btnAuthenticate.Location = new System.Drawing.Point(216, 605);
            this.btnAuthenticate.Name = "btnAuthenticate";
            this.btnAuthenticate.Radius = 5;
            this.btnAuthenticate.Size = new System.Drawing.Size(146, 31);
            stringFormat2.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat2.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat2.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat2.Trimming = System.Drawing.StringTrimming.Character;
            this.btnAuthenticate.StringFormat = stringFormat2;
            this.btnAuthenticate.TabIndex = 21;
            this.btnAuthenticate.Text = "Authenticate";
            this.btnAuthenticate.TextColor = System.Drawing.Color.DimGray;
            this.btnAuthenticate.UseVisualStyleBackColor = true;
            this.btnAuthenticate.Click += new System.EventHandler(this.btnAuthenticate_Click);
            this.btnAuthenticate.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
            this.btnAuthenticate.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            // 
            // Page_RegisterAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Controls.Add(this.btnAuthenticate);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.lblConfirmPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtEmailAddress);
            this.Controls.Add(this.lblEmailAddress);
            this.Controls.Add(this.lblAccountName);
            this.Name = "Page_RegisterAccount";
            this.Size = new System.Drawing.Size(578, 712);
            this.Load += new System.EventHandler(this.Page_AuthenticateAccount_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAccountName;
        private Classes.Default_TextBox txtEmailAddress;
        private System.Windows.Forms.Label lblEmailAddress;
        private Classes.Default_TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private Classes.Default_TextBox txtConfirmPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private CustomControls.RoundedButton btnRegister;
        private System.Windows.Forms.Label lblTitle;
        private Classes.Default_TextBox txtName;
        private CustomControls.RoundedButton btnAuthenticate;
    }
}
