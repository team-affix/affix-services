namespace Affix_Center.Pages
{
    partial class Dialog_P2PMute
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
            this.pnlLoading = new Affix_Center.CustomControls.RoundedPanel();
            this.btn = new Affix_Center.CustomControls.RoundedButton();
            this.SuspendLayout();
            // 
            // pnlLoading
            // 
            this.pnlLoading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.pnlLoading.BorderWidth = 2;
            this.pnlLoading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.pnlLoading.Location = new System.Drawing.Point(8, 3);
            this.pnlLoading.Margin = new System.Windows.Forms.Padding(5);
            this.pnlLoading.Name = "pnlLoading";
            this.pnlLoading.Radius = 5;
            this.pnlLoading.Size = new System.Drawing.Size(37, 37);
            this.pnlLoading.TabIndex = 11;
            // 
            // btn
            // 
            this.btn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn.BorderWidth = 2;
            this.btn.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btn.Location = new System.Drawing.Point(51, 3);
            this.btn.Margin = new System.Windows.Forms.Padding(1);
            this.btn.Name = "btn";
            this.btn.Radius = 5;
            this.btn.Size = new System.Drawing.Size(186, 37);
            stringFormat1.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat1.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat1.Trimming = System.Drawing.StringTrimming.Character;
            this.btn.StringFormat = stringFormat1;
            this.btn.TabIndex = 10;
            this.btn.Text = "Mute";
            this.btn.TextColor = System.Drawing.Color.White;
            this.btn.UseVisualStyleBackColor = false;
            // 
            // Dialog_P2PMute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.Controls.Add(this.pnlLoading);
            this.Controls.Add(this.btn);
            this.Name = "Dialog_P2PMute";
            this.Size = new System.Drawing.Size(245, 43);
            this.ResumeLayout(false);

        }

        #endregion

        public CustomControls.RoundedPanel pnlLoading;
        public CustomControls.RoundedButton btn;
    }
}
