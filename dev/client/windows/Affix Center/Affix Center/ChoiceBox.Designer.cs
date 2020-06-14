namespace Affix_Center
{
    partial class ChoiceBox
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
            this.lblSample = new System.Windows.Forms.Label();
            this.pnlChoiceList = new System.Windows.Forms.Panel();
            this.scrScroll = new System.Windows.Forms.VScrollBar();
            this.SuspendLayout();
            // 
            // lblSample
            // 
            this.lblSample.AutoSize = true;
            this.lblSample.Font = new System.Drawing.Font("Roboto", 12F);
            this.lblSample.Location = new System.Drawing.Point(628, 46);
            this.lblSample.Name = "lblSample";
            this.lblSample.Size = new System.Drawing.Size(0, 20);
            this.lblSample.TabIndex = 1;
            // 
            // pnlChoiceList
            // 
            this.pnlChoiceList.Location = new System.Drawing.Point(12, 12);
            this.pnlChoiceList.Name = "pnlChoiceList";
            this.pnlChoiceList.Size = new System.Drawing.Size(660, 419);
            this.pnlChoiceList.TabIndex = 2;
            // 
            // scrScroll
            // 
            this.scrScroll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scrScroll.Enabled = false;
            this.scrScroll.Location = new System.Drawing.Point(675, 12);
            this.scrScroll.Name = "scrScroll";
            this.scrScroll.Size = new System.Drawing.Size(16, 419);
            this.scrScroll.TabIndex = 3;
            // 
            // ChoiceBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(700, 440);
            this.Controls.Add(this.lblSample);
            this.Controls.Add(this.scrScroll);
            this.Controls.Add(this.pnlChoiceList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ChoiceBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChoiceBox";
            this.Load += new System.EventHandler(this.ChoiceBox_Load);
            this.Shown += new System.EventHandler(this.ChoiceBox_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblSample;
        private System.Windows.Forms.Panel pnlChoiceList;
        private System.Windows.Forms.VScrollBar scrScroll;
    }
}