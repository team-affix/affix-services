namespace Affix_Center
{
    partial class FileReceive
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
            this.lblCloudWireTitle = new System.Windows.Forms.Label();
            this.lblCloudwireDesc = new System.Windows.Forms.Label();
            this.lstFilesAdded = new System.Windows.Forms.ListBox();
            this.pnlSend = new System.Windows.Forms.Panel();
            this.lblSend = new System.Windows.Forms.Label();
            this.pnlSend.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCloudWireTitle
            // 
            this.lblCloudWireTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCloudWireTitle.Font = new System.Drawing.Font("Roboto", 24F);
            this.lblCloudWireTitle.ForeColor = System.Drawing.Color.DimGray;
            this.lblCloudWireTitle.Location = new System.Drawing.Point(124, 46);
            this.lblCloudWireTitle.Name = "lblCloudWireTitle";
            this.lblCloudWireTitle.Size = new System.Drawing.Size(549, 39);
            this.lblCloudWireTitle.TabIndex = 21;
            this.lblCloudWireTitle.Text = "Receiving Files";
            this.lblCloudWireTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCloudwireDesc
            // 
            this.lblCloudwireDesc.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCloudwireDesc.Font = new System.Drawing.Font("Roboto", 14F);
            this.lblCloudwireDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(23)))), ((int)(((byte)(22)))));
            this.lblCloudwireDesc.Location = new System.Drawing.Point(127, 85);
            this.lblCloudwireDesc.Name = "lblCloudwireDesc";
            this.lblCloudwireDesc.Size = new System.Drawing.Size(549, 39);
            this.lblCloudwireDesc.TabIndex = 20;
            this.lblCloudwireDesc.Text = "Waiting for files from partner.";
            this.lblCloudwireDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lstFilesAdded
            // 
            this.lstFilesAdded.BackColor = System.Drawing.Color.White;
            this.lstFilesAdded.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstFilesAdded.Font = new System.Drawing.Font("Roboto", 12F);
            this.lstFilesAdded.FormattingEnabled = true;
            this.lstFilesAdded.ItemHeight = 19;
            this.lstFilesAdded.Location = new System.Drawing.Point(128, 127);
            this.lstFilesAdded.Name = "lstFilesAdded";
            this.lstFilesAdded.Size = new System.Drawing.Size(545, 228);
            this.lstFilesAdded.TabIndex = 22;
            this.lstFilesAdded.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstFilesAdded_MouseDown);
            // 
            // pnlSend
            // 
            this.pnlSend.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlSend.BackColor = System.Drawing.Color.Transparent;
            this.pnlSend.BackgroundImage = global::Affix_Center.Properties.Resources.Listen;
            this.pnlSend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlSend.Controls.Add(this.lblSend);
            this.pnlSend.Location = new System.Drawing.Point(335, 361);
            this.pnlSend.Name = "pnlSend";
            this.pnlSend.Size = new System.Drawing.Size(130, 50);
            this.pnlSend.TabIndex = 23;
            // 
            // lblSend
            // 
            this.lblSend.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSend.BackColor = System.Drawing.Color.Transparent;
            this.lblSend.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.lblSend.ForeColor = System.Drawing.Color.White;
            this.lblSend.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lblSend.Location = new System.Drawing.Point(0, -2);
            this.lblSend.Name = "lblSend";
            this.lblSend.Size = new System.Drawing.Size(130, 50);
            this.lblSend.TabIndex = 0;
            this.lblSend.Text = "Build Files";
            this.lblSend.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSend.Click += new System.EventHandler(this.lblSend_Click);
            this.lblSend.MouseEnter += new System.EventHandler(this.lblSend_MouseEnter);
            this.lblSend.MouseLeave += new System.EventHandler(this.lblSend_MouseLeave);
            // 
            // FileReceive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlSend);
            this.Controls.Add(this.lstFilesAdded);
            this.Controls.Add(this.lblCloudWireTitle);
            this.Controls.Add(this.lblCloudwireDesc);
            this.Name = "FileReceive";
            this.Text = "FileReceive";
            this.Load += new System.EventHandler(this.FileReceive_Load);
            this.pnlSend.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblCloudWireTitle;
        private System.Windows.Forms.Label lblCloudwireDesc;
        private System.Windows.Forms.ListBox lstFilesAdded;
        private System.Windows.Forms.Panel pnlSend;
        private System.Windows.Forms.Label lblSend;
    }
}