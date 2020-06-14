namespace Affix_Center.Forms
{
    partial class Dialog_LeftSideBar
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
            this.pnlPreviousPages = new Affix_Center.CustomControls.RoundedPanel();
            this.flpPreviousPages = new System.Windows.Forms.FlowLayoutPanel();
            this.lblAccountName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlActiveAccount = new Affix_Center.CustomControls.RoundedPanel();
            this.pnlActiveAccountOption0 = new Affix_Center.CustomControls.RoundedPanel();
            this.btnAccounts = new Affix_Center.CustomControls.RoundedButton();
            this.pnlAccountName = new Affix_Center.CustomControls.RoundedPanel();
            this.roundedButton1 = new Affix_Center.CustomControls.RoundedButton();
            this.pnlPreviousPages.SuspendLayout();
            this.pnlActiveAccount.SuspendLayout();
            this.pnlActiveAccountOption0.SuspendLayout();
            this.pnlAccountName.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPreviousPages
            // 
            this.pnlPreviousPages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPreviousPages.AutoSize = true;
            this.pnlPreviousPages.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlPreviousPages.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.pnlPreviousPages.BorderWidth = 2;
            this.pnlPreviousPages.Controls.Add(this.flpPreviousPages);
            this.pnlPreviousPages.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.pnlPreviousPages.Location = new System.Drawing.Point(-209, 213);
            this.pnlPreviousPages.Margin = new System.Windows.Forms.Padding(30);
            this.pnlPreviousPages.Name = "pnlPreviousPages";
            this.pnlPreviousPages.Radius = 5;
            this.pnlPreviousPages.Size = new System.Drawing.Size(202, 66);
            this.pnlPreviousPages.TabIndex = 1;
            // 
            // flpPreviousPages
            // 
            this.flpPreviousPages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpPreviousPages.AutoSize = true;
            this.flpPreviousPages.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpPreviousPages.Location = new System.Drawing.Point(10, 10);
            this.flpPreviousPages.Margin = new System.Windows.Forms.Padding(10);
            this.flpPreviousPages.Name = "flpPreviousPages";
            this.flpPreviousPages.Size = new System.Drawing.Size(182, 46);
            this.flpPreviousPages.TabIndex = 0;
            this.flpPreviousPages.Paint += new System.Windows.Forms.PaintEventHandler(this.flpPreviousPages_Paint);
            // 
            // lblAccountName
            // 
            this.lblAccountName.AutoSize = true;
            this.lblAccountName.Font = new System.Drawing.Font("Segoe UI Light", 16F);
            this.lblAccountName.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblAccountName.Location = new System.Drawing.Point(9, 29);
            this.lblAccountName.Margin = new System.Windows.Forms.Padding(10);
            this.lblAccountName.Name = "lblAccountName";
            this.lblAccountName.Size = new System.Drawing.Size(152, 30);
            this.lblAccountName.TabIndex = 2;
            this.lblAccountName.Text = "Account Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "You\'re signed in as:";
            // 
            // pnlActiveAccount
            // 
            this.pnlActiveAccount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlActiveAccount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.pnlActiveAccount.BorderWidth = 2;
            this.pnlActiveAccount.Controls.Add(this.pnlActiveAccountOption0);
            this.pnlActiveAccount.Controls.Add(this.pnlAccountName);
            this.pnlActiveAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.pnlActiveAccount.Location = new System.Drawing.Point(-209, 30);
            this.pnlActiveAccount.Name = "pnlActiveAccount";
            this.pnlActiveAccount.Radius = 5;
            this.pnlActiveAccount.Size = new System.Drawing.Size(202, 150);
            this.pnlActiveAccount.TabIndex = 4;
            // 
            // pnlActiveAccountOption0
            // 
            this.pnlActiveAccountOption0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.pnlActiveAccountOption0.BorderWidth = 2;
            this.pnlActiveAccountOption0.Controls.Add(this.btnAccounts);
            this.pnlActiveAccountOption0.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.pnlActiveAccountOption0.Location = new System.Drawing.Point(10, 84);
            this.pnlActiveAccountOption0.Name = "pnlActiveAccountOption0";
            this.pnlActiveAccountOption0.Radius = 5;
            this.pnlActiveAccountOption0.Size = new System.Drawing.Size(182, 54);
            this.pnlActiveAccountOption0.TabIndex = 6;
            // 
            // btnAccounts
            // 
            this.btnAccounts.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAccounts.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnAccounts.BorderWidth = 2;
            this.btnAccounts.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnAccounts.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btnAccounts.Location = new System.Drawing.Point(17, 9);
            this.btnAccounts.Margin = new System.Windows.Forms.Padding(10, 10, 3, 3);
            this.btnAccounts.Name = "btnAccounts";
            this.btnAccounts.Radius = 5;
            this.btnAccounts.Size = new System.Drawing.Size(149, 37);
            stringFormat1.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat1.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat1.Trimming = System.Drawing.StringTrimming.Character;
            this.btnAccounts.StringFormat = stringFormat1;
            this.btnAccounts.TabIndex = 2;
            this.btnAccounts.Text = "Log Out";
            this.btnAccounts.TextColor = System.Drawing.Color.White;
            this.btnAccounts.UseVisualStyleBackColor = false;
            // 
            // pnlAccountName
            // 
            this.pnlAccountName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.pnlAccountName.BorderWidth = 2;
            this.pnlAccountName.Controls.Add(this.lblAccountName);
            this.pnlAccountName.Controls.Add(this.label1);
            this.pnlAccountName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.pnlAccountName.Location = new System.Drawing.Point(10, 12);
            this.pnlAccountName.Name = "pnlAccountName";
            this.pnlAccountName.Radius = 5;
            this.pnlAccountName.Size = new System.Drawing.Size(182, 67);
            this.pnlAccountName.TabIndex = 5;
            // 
            // roundedButton1
            // 
            this.roundedButton1.BorderWidth = 0;
            this.roundedButton1.Location = new System.Drawing.Point(15, 23);
            this.roundedButton1.Name = "roundedButton1";
            this.roundedButton1.Radius = 0;
            this.roundedButton1.Size = new System.Drawing.Size(48, 48);
            stringFormat2.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat2.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat2.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat2.Trimming = System.Drawing.StringTrimming.Character;
            this.roundedButton1.StringFormat = stringFormat2;
            this.roundedButton1.TabIndex = 5;
            this.roundedButton1.Text = "roundedButton1";
            this.roundedButton1.TextColor = System.Drawing.Color.White;
            this.roundedButton1.UseVisualStyleBackColor = true;
            // 
            // Dialog_LeftSideBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Controls.Add(this.roundedButton1);
            this.Controls.Add(this.pnlActiveAccount);
            this.Controls.Add(this.pnlPreviousPages);
            this.Name = "Dialog_LeftSideBar";
            this.Size = new System.Drawing.Size(76, 694);
            this.pnlPreviousPages.ResumeLayout(false);
            this.pnlPreviousPages.PerformLayout();
            this.pnlActiveAccount.ResumeLayout(false);
            this.pnlActiveAccountOption0.ResumeLayout(false);
            this.pnlAccountName.ResumeLayout(false);
            this.pnlAccountName.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public CustomControls.RoundedPanel pnlPreviousPages;
        public System.Windows.Forms.FlowLayoutPanel flpPreviousPages;
        private System.Windows.Forms.Label lblAccountName;
        private System.Windows.Forms.Label label1;
        private CustomControls.RoundedPanel pnlActiveAccount;
        private CustomControls.RoundedPanel pnlActiveAccountOption0;
        private CustomControls.RoundedPanel pnlAccountName;
        private CustomControls.RoundedButton btnAccounts;
        private CustomControls.RoundedButton roundedButton1;
    }
}
