namespace Affix_Center.Pages
{
    partial class Dialog_List
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
            this.pnl = new Affix_Center.CustomControls.RoundedPanel();
            this.flp = new System.Windows.Forms.FlowLayoutPanel();
            this.pnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl
            // 
            this.pnl.AutoSize = true;
            this.pnl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pnl.BorderWidth = 2;
            this.pnl.Controls.Add(this.flp);
            this.pnl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.pnl.Location = new System.Drawing.Point(3, 3);
            this.pnl.Name = "pnl";
            this.pnl.Radius = 5;
            this.pnl.Size = new System.Drawing.Size(20, 20);
            this.pnl.TabIndex = 0;
            // 
            // flp
            // 
            this.flp.AutoSize = true;
            this.flp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flp.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flp.Location = new System.Drawing.Point(10, 10);
            this.flp.Margin = new System.Windows.Forms.Padding(10);
            this.flp.Name = "flp";
            this.flp.Size = new System.Drawing.Size(0, 0);
            this.flp.TabIndex = 0;
            // 
            // Dialog_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Controls.Add(this.pnl);
            this.Name = "Dialog_List";
            this.Size = new System.Drawing.Size(129, 171);
            this.pnl.ResumeLayout(false);
            this.pnl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public CustomControls.RoundedPanel pnl;
        public System.Windows.Forms.FlowLayoutPanel flp;
    }
}
