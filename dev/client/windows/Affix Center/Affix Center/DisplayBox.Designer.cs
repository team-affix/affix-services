namespace Affix_Center
{
    partial class DisplayBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayBox));
            this.lblNull = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lstInfo = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lblNull
            // 
            this.lblNull.AutoSize = true;
            this.lblNull.Location = new System.Drawing.Point(400, -1);
            this.lblNull.Name = "lblNull";
            this.lblNull.Size = new System.Drawing.Size(0, 13);
            this.lblNull.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Light", 14F);
            this.lblTitle.ForeColor = System.Drawing.Color.Coral;
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(57, 25);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "label1";
            // 
            // lstInfo
            // 
            this.lstInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lstInfo.FormattingEnabled = true;
            this.lstInfo.ItemHeight = 17;
            this.lstInfo.Location = new System.Drawing.Point(17, 38);
            this.lstInfo.Name = "lstInfo";
            this.lstInfo.Size = new System.Drawing.Size(771, 391);
            this.lstInfo.TabIndex = 3;
            this.lstInfo.Click += new System.EventHandler(this.lstInfo_Click);
            // 
            // DisplayBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lstInfo);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblNull);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DisplayBox";
            this.Load += new System.EventHandler(this.DisplayBox_Load);
            this.Shown += new System.EventHandler(this.DisplayBox_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblNull;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ListBox lstInfo;
    }
}