namespace Affix_Center
{
    partial class Input
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
            this.pnlSignInPicture = new System.Windows.Forms.Panel();
            this.lblLoadingDesc = new System.Windows.Forms.Label();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.pnlLoginUsername = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlSignInPicture
            // 
            this.pnlSignInPicture.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlSignInPicture.BackgroundImage = global::Affix_Center.Properties.Resources.SignInPicture;
            this.pnlSignInPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlSignInPicture.Location = new System.Drawing.Point(108, 146);
            this.pnlSignInPicture.Name = "pnlSignInPicture";
            this.pnlSignInPicture.Size = new System.Drawing.Size(75, 75);
            this.pnlSignInPicture.TabIndex = 15;
            // 
            // lblLoadingDesc
            // 
            this.lblLoadingDesc.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLoadingDesc.Font = new System.Drawing.Font("Roboto", 14F);
            this.lblLoadingDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(23)))), ((int)(((byte)(22)))));
            this.lblLoadingDesc.Location = new System.Drawing.Point(104, 104);
            this.lblLoadingDesc.Name = "lblLoadingDesc";
            this.lblLoadingDesc.Size = new System.Drawing.Size(656, 39);
            this.lblLoadingDesc.TabIndex = 14;
            this.lblLoadingDesc.Text = "DefaultText";
            this.lblLoadingDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtInput
            // 
            this.txtInput.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInput.Font = new System.Drawing.Font("Segoe UI Light", 14F);
            this.txtInput.Location = new System.Drawing.Point(298, 171);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(462, 25);
            this.txtInput.TabIndex = 16;
            // 
            // lblUsername
            // 
            this.lblUsername.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.lblUsername.Location = new System.Drawing.Point(193, 171);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(99, 31);
            this.lblUsername.TabIndex = 18;
            this.lblUsername.Text = "DefaultText";
            this.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlLoginUsername
            // 
            this.pnlLoginUsername.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlLoginUsername.BackgroundImage = global::Affix_Center.Properties.Resources.TextBoxWhite__1_;
            this.pnlLoginUsername.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlLoginUsername.Location = new System.Drawing.Point(298, 202);
            this.pnlLoginUsername.Name = "pnlLoginUsername";
            this.pnlLoginUsername.Size = new System.Drawing.Size(462, 12);
            this.pnlLoginUsername.TabIndex = 17;
            // 
            // Input
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(865, 324);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.pnlLoginUsername);
            this.Controls.Add(this.pnlSignInPicture);
            this.Controls.Add(this.lblLoadingDesc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Input";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlSignInPicture;
        private System.Windows.Forms.Label lblLoadingDesc;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Panel pnlLoginUsername;
    }
}