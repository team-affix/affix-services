namespace Affix_Center
{
    partial class LearnMoreGeneral
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LearnMoreGeneral));
            this.pnlLearnMoreGeneral = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlLearnMoreGeneral
            // 
            this.pnlLearnMoreGeneral.AutoScroll = true;
            this.pnlLearnMoreGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLearnMoreGeneral.Location = new System.Drawing.Point(0, 0);
            this.pnlLearnMoreGeneral.Name = "pnlLearnMoreGeneral";
            this.pnlLearnMoreGeneral.Size = new System.Drawing.Size(1099, 657);
            this.pnlLearnMoreGeneral.TabIndex = 0;
            this.pnlLearnMoreGeneral.MouseEnter += new System.EventHandler(this.pnlLearnMoreGeneral_MouseEnter);
            // 
            // LearnMoreGeneral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(23)))), ((int)(((byte)(22)))));
            this.ClientSize = new System.Drawing.Size(1099, 657);
            this.Controls.Add(this.pnlLearnMoreGeneral);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LearnMoreGeneral";
            this.Load += new System.EventHandler(this.LearnMoreGeneral_Load);
            this.Shown += new System.EventHandler(this.LearnMoreGeneral_Shown);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlLearnMoreGeneral;
    }
}