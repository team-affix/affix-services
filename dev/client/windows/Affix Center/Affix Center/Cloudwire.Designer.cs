namespace Affix_Center
{
    partial class Cloudwire
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cloudwire));
            this.lblCloudWireTitle = new System.Windows.Forms.Label();
            this.txtIPAddress = new System.Windows.Forms.TextBox();
            this.lblCloudwireDesc = new System.Windows.Forms.Label();
            this.lblIPAddress = new System.Windows.Forms.Label();
            this.lblIPv4 = new System.Windows.Forms.Label();
            this.pnlIPAddress = new System.Windows.Forms.Panel();
            this.lblListen = new System.Windows.Forms.Label();
            this.pnlListen = new System.Windows.Forms.Panel();
            this.pnlListen.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCloudWireTitle
            // 
            this.lblCloudWireTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCloudWireTitle.Font = new System.Drawing.Font("Roboto", 24F);
            this.lblCloudWireTitle.ForeColor = System.Drawing.Color.DimGray;
            this.lblCloudWireTitle.Location = new System.Drawing.Point(183, 131);
            this.lblCloudWireTitle.Name = "lblCloudWireTitle";
            this.lblCloudWireTitle.Size = new System.Drawing.Size(549, 39);
            this.lblCloudWireTitle.TabIndex = 17;
            this.lblCloudWireTitle.Text = "Local File Transfer";
            this.lblCloudWireTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtIPAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtIPAddress.Font = new System.Drawing.Font("Segoe UI Light", 14F);
            this.txtIPAddress.Location = new System.Drawing.Point(324, 287);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(462, 25);
            this.txtIPAddress.TabIndex = 13;
            this.txtIPAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIPAddress_KeyDown);
            // 
            // lblCloudwireDesc
            // 
            this.lblCloudwireDesc.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCloudwireDesc.Font = new System.Drawing.Font("Roboto", 14F);
            this.lblCloudwireDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(23)))), ((int)(((byte)(22)))));
            this.lblCloudwireDesc.Location = new System.Drawing.Point(186, 170);
            this.lblCloudwireDesc.Name = "lblCloudwireDesc";
            this.lblCloudwireDesc.Size = new System.Drawing.Size(549, 39);
            this.lblCloudwireDesc.TabIndex = 16;
            this.lblCloudwireDesc.Text = "Transfer files between computers under your router.";
            this.lblCloudwireDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIPAddress
            // 
            this.lblIPAddress.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblIPAddress.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.lblIPAddress.Location = new System.Drawing.Point(133, 287);
            this.lblIPAddress.Name = "lblIPAddress";
            this.lblIPAddress.Size = new System.Drawing.Size(185, 31);
            this.lblIPAddress.TabIndex = 15;
            this.lblIPAddress.Text = "IPv4 Address of Recipient:";
            this.lblIPAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblIPv4
            // 
            this.lblIPv4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblIPv4.Font = new System.Drawing.Font("Roboto", 24F);
            this.lblIPv4.Location = new System.Drawing.Point(186, 209);
            this.lblIPv4.Name = "lblIPv4";
            this.lblIPv4.Size = new System.Drawing.Size(542, 75);
            this.lblIPv4.TabIndex = 18;
            this.lblIPv4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlIPAddress
            // 
            this.pnlIPAddress.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlIPAddress.BackgroundImage = global::Affix_Center.Properties.Resources.TextBoxWhite__1_;
            this.pnlIPAddress.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlIPAddress.Location = new System.Drawing.Point(324, 318);
            this.pnlIPAddress.Name = "pnlIPAddress";
            this.pnlIPAddress.Size = new System.Drawing.Size(462, 12);
            this.pnlIPAddress.TabIndex = 14;
            // 
            // lblListen
            // 
            this.lblListen.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblListen.BackColor = System.Drawing.Color.Transparent;
            this.lblListen.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.lblListen.ForeColor = System.Drawing.Color.White;
            this.lblListen.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lblListen.Location = new System.Drawing.Point(0, -2);
            this.lblListen.Name = "lblListen";
            this.lblListen.Size = new System.Drawing.Size(130, 50);
            this.lblListen.TabIndex = 0;
            this.lblListen.Text = "Listen";
            this.lblListen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblListen.Click += new System.EventHandler(this.lblListen_Click);
            this.lblListen.MouseEnter += new System.EventHandler(this.lblListen_MouseEnter);
            this.lblListen.MouseLeave += new System.EventHandler(this.lblListen_MouseLeave);
            // 
            // pnlListen
            // 
            this.pnlListen.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlListen.BackColor = System.Drawing.Color.Transparent;
            this.pnlListen.BackgroundImage = global::Affix_Center.Properties.Resources.Listen;
            this.pnlListen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlListen.Controls.Add(this.lblListen);
            this.pnlListen.Location = new System.Drawing.Point(190, 345);
            this.pnlListen.Name = "pnlListen";
            this.pnlListen.Size = new System.Drawing.Size(130, 50);
            this.pnlListen.TabIndex = 19;
            // 
            // Cloudwire
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(919, 521);
            this.Controls.Add(this.pnlListen);
            this.Controls.Add(this.lblIPv4);
            this.Controls.Add(this.lblCloudWireTitle);
            this.Controls.Add(this.txtIPAddress);
            this.Controls.Add(this.lblCloudwireDesc);
            this.Controls.Add(this.lblIPAddress);
            this.Controls.Add(this.pnlIPAddress);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Cloudwire";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Cloudwire_Load);
            this.pnlListen.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCloudWireTitle;
        private System.Windows.Forms.TextBox txtIPAddress;
        private System.Windows.Forms.Label lblCloudwireDesc;
        private System.Windows.Forms.Label lblIPAddress;
        private System.Windows.Forms.Panel pnlIPAddress;
        private System.Windows.Forms.Label lblIPv4;
        private System.Windows.Forms.Label lblListen;
        private System.Windows.Forms.Panel pnlListen;
    }
}