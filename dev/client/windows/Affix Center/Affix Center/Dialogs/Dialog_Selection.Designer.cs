namespace Affix_Center.Forms
{
    partial class Dialog_Selection
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
            this.pnlInfo = new Affix_Center.CustomControls.RoundedPanel();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlOptions = new Affix_Center.CustomControls.RoundedPanel();
            this.flpOptions = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlInfo.SuspendLayout();
            this.pnlOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlInfo
            // 
            this.pnlInfo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.pnlInfo.BorderWidth = 0;
            this.pnlInfo.Controls.Add(this.lblDescription);
            this.pnlInfo.Controls.Add(this.lblTitle);
            this.pnlInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.pnlInfo.Location = new System.Drawing.Point(57, 83);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Radius = 10;
            this.pnlInfo.Size = new System.Drawing.Size(400, 106);
            this.pnlInfo.TabIndex = 13;
            // 
            // lblDescription
            // 
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDescription.ForeColor = System.Drawing.Color.White;
            this.lblDescription.Location = new System.Drawing.Point(10, 46);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(10, 3, 10, 10);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(380, 50);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "lblDescription";
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Light", 16F);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(10, 10);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(10, 10, 10, 3);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(380, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "lblTitle";
            // 
            // pnlOptions
            // 
            this.pnlOptions.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlOptions.AutoSize = true;
            this.pnlOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.pnlOptions.BorderWidth = 0;
            this.pnlOptions.Controls.Add(this.flpOptions);
            this.pnlOptions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.pnlOptions.Location = new System.Drawing.Point(57, 207);
            this.pnlOptions.Margin = new System.Windows.Forms.Padding(15);
            this.pnlOptions.Name = "pnlOptions";
            this.pnlOptions.Radius = 10;
            this.pnlOptions.Size = new System.Drawing.Size(400, 82);
            this.pnlOptions.TabIndex = 12;
            // 
            // flpOptions
            // 
            this.flpOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpOptions.AutoSize = true;
            this.flpOptions.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpOptions.Location = new System.Drawing.Point(97, 13);
            this.flpOptions.Margin = new System.Windows.Forms.Padding(15);
            this.flpOptions.Name = "flpOptions";
            this.flpOptions.Size = new System.Drawing.Size(206, 54);
            this.flpOptions.TabIndex = 0;
            // 
            // Dialog_Selection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Controls.Add(this.pnlInfo);
            this.Controls.Add(this.pnlOptions);
            this.Name = "Dialog_Selection";
            this.Size = new System.Drawing.Size(514, 373);
            this.Load += new System.EventHandler(this.Dialog_Selection_Load);
            this.pnlInfo.ResumeLayout(false);
            this.pnlOptions.ResumeLayout(false);
            this.pnlOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private CustomControls.RoundedPanel pnlInfo;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblTitle;
        private CustomControls.RoundedPanel pnlOptions;
        private System.Windows.Forms.FlowLayoutPanel flpOptions;
    }
}
