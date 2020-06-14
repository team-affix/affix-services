namespace Affix_Center
{
    partial class Page_TermsAndConditions
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
            this.btnOpen = new Affix_Center.CustomControls.RoundedButton();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnAccept = new Affix_Center.CustomControls.RoundedButton();
            this.dialog_Header1 = new Affix_Center.Dialog_Header();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnOpen.BackColor = System.Drawing.Color.Yellow;
            this.btnOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpen.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnOpen.Location = new System.Drawing.Point(236, 354);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(304, 43);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "Open Terms and Conditions";
            this.btnOpen.UseVisualStyleBackColor = false;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.Yellow;
            this.lblSubtitle.Location = new System.Drawing.Point(232, 269);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(353, 21);
            this.lblSubtitle.TabIndex = 3;
            this.lblSubtitle.Text = "Team Affix Online Services\' Terms and Conditions:";
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Light", 10F);
            this.lblTitle.ForeColor = System.Drawing.Color.DimGray;
            this.lblTitle.Location = new System.Drawing.Point(232, 250);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(689, 19);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "We\'re sorry, but you cannot access any online services with this Affix Client unt" +
    "il you accept our Terms and Conditions.";
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAccept.BackColor = System.Drawing.Color.Yellow;
            this.btnAccept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccept.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAccept.Location = new System.Drawing.Point(546, 354);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(304, 43);
            this.btnAccept.TabIndex = 4;
            this.btnAccept.Text = "I have read and I accept the terms and conditions.";
            this.btnAccept.UseVisualStyleBackColor = false;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // dialog_Header1
            // 
            this.dialog_Header1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dialog_Header1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.dialog_Header1.Location = new System.Drawing.Point(0, 0);
            this.dialog_Header1.Name = "dialog_Header1";
            this.dialog_Header1.Size = new System.Drawing.Size(1153, 100);
            this.dialog_Header1.Subtitle = "Affix Software";
            this.dialog_Header1.TabIndex = 5;
            this.dialog_Header1.Title = "Terms and Conditions";
            // 
            // Page_TermsAndConditions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.dialog_Header1);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnOpen);
            this.Name = "Page_TermsAndConditions";
            this.Size = new System.Drawing.Size(1153, 647);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public CustomControls.RoundedButton btnOpen;
        public System.Windows.Forms.Label lblSubtitle;
        public System.Windows.Forms.Label lblTitle;
        public CustomControls.RoundedButton btnAccept;
        private Dialog_Header dialog_Header1;
    }
}
