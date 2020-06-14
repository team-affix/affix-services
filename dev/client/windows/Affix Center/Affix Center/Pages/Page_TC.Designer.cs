﻿namespace Affix_Center.Forms
{
    partial class Page_TC
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
            this.btnDeny = new Affix_Center.CustomControls.RoundedButton();
            this.btnAccept = new Affix_Center.CustomControls.RoundedButton();
            this.hdr = new Affix_Center.Dialog_Header();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnDeny
            // 
            this.btnDeny.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDeny.BorderWidth = 2;
            this.btnDeny.Font = new System.Drawing.Font("Segoe UI Light", 10F);
            this.btnDeny.ForeColor = System.Drawing.Color.DimGray;
            this.btnDeny.Location = new System.Drawing.Point(605, 292);
            this.btnDeny.Name = "btnDeny";
            this.btnDeny.Radius = 5;
            this.btnDeny.Size = new System.Drawing.Size(159, 46);
            stringFormat1.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat1.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat1.Trimming = System.Drawing.StringTrimming.Character;
            this.btnDeny.StringFormat = stringFormat1;
            this.btnDeny.TabIndex = 9;
            this.btnDeny.Text = "Deny";
            this.btnDeny.TextColor = System.Drawing.Color.DimGray;
            this.btnDeny.UseVisualStyleBackColor = true;
            this.btnDeny.Click += new System.EventHandler(this.btnDeny_Click);
            this.btnDeny.MouseEnter += new System.EventHandler(this.btnAccept_MouseEnter);
            this.btnDeny.MouseLeave += new System.EventHandler(this.btnAccept_MouseLeave);
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAccept.BorderWidth = 2;
            this.btnAccept.Font = new System.Drawing.Font("Segoe UI Light", 10F);
            this.btnAccept.ForeColor = System.Drawing.Color.DimGray;
            this.btnAccept.Location = new System.Drawing.Point(383, 292);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Radius = 5;
            this.btnAccept.Size = new System.Drawing.Size(159, 46);
            stringFormat2.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat2.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat2.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat2.Trimming = System.Drawing.StringTrimming.Character;
            this.btnAccept.StringFormat = stringFormat2;
            this.btnAccept.TabIndex = 8;
            this.btnAccept.Text = "Accept";
            this.btnAccept.TextColor = System.Drawing.Color.DimGray;
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            this.btnAccept.MouseEnter += new System.EventHandler(this.btnAccept_MouseEnter);
            this.btnAccept.MouseLeave += new System.EventHandler(this.btnAccept_MouseLeave);
            // 
            // hdr
            // 
            this.hdr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hdr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.hdr.Location = new System.Drawing.Point(0, 0);
            this.hdr.Name = "hdr";
            this.hdr.Size = new System.Drawing.Size(1146, 100);
            this.hdr.Subtitle = "Terms and Conditions for Team Affix Software.";
            this.hdr.TabIndex = 7;
            this.hdr.Title = "Terms and Conditions";
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(98, 214);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(232, 21);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "Thank you for your cooperation.";
            // 
            // lblDesc
            // 
            this.lblDesc.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDesc.AutoSize = true;
            this.lblDesc.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.lblDesc.ForeColor = System.Drawing.Color.DimGray;
            this.lblDesc.Location = new System.Drawing.Point(98, 193);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(950, 21);
            this.lblDesc.TabIndex = 5;
            this.lblDesc.Text = "For access to online services and any usage of this Affix Software Client, you fi" +
    "rst must read fully and agree to the Terms and Conditions of Use.";
            // 
            // Page_TC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Controls.Add(this.btnDeny);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.hdr);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblDesc);
            this.Name = "Page_TC";
            this.Size = new System.Drawing.Size(1146, 460);
            this.Load += new System.EventHandler(this.Page_TC_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomControls.RoundedButton btnDeny;
        private CustomControls.RoundedButton btnAccept;
        private Dialog_Header hdr;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDesc;
    }
}
