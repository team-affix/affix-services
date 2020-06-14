namespace Affix_Center
{
    partial class Form_Main
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
            System.Drawing.StringFormat stringFormat1 = new System.Drawing.StringFormat();
            System.Drawing.StringFormat stringFormat2 = new System.Drawing.StringFormat();
            this.pnlNotifications = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlTeamLogo = new System.Windows.Forms.Panel();
            this.dsp = new UILayout.DisplayPanel();
            this.pnlLoadingItems = new Affix_Center.CustomControls.RoundedPanel();
            this.lstConsole = new System.Windows.Forms.CheckedListBox();
            this.btnShowConsole = new Affix_Center.CustomControls.RoundedButton();
            this.pnlConsoleOptions = new Affix_Center.CustomControls.RoundedPanel();
            this.btnDismiss = new Affix_Center.CustomControls.RoundedButton();
            this.pnlConsole = new Affix_Center.CustomControls.RoundedPanel();
            this.lstOptions = new Affix_Center.Pages.Dialog_List();
            this.pnlConsoleOptions.SuspendLayout();
            this.pnlConsole.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlNotifications
            // 
            this.pnlNotifications.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlNotifications.AutoSize = true;
            this.pnlNotifications.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.pnlNotifications.Location = new System.Drawing.Point(1393, 949);
            this.pnlNotifications.MaximumSize = new System.Drawing.Size(600, 825);
            this.pnlNotifications.Name = "pnlNotifications";
            this.pnlNotifications.Size = new System.Drawing.Size(0, 0);
            this.pnlNotifications.TabIndex = 0;
            // 
            // pnlTeamLogo
            // 
            this.pnlTeamLogo.BackgroundImage = global::Affix_Center.Properties.Resources.AffixLogoWhite;
            this.pnlTeamLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlTeamLogo.Location = new System.Drawing.Point(12, 95);
            this.pnlTeamLogo.Name = "pnlTeamLogo";
            this.pnlTeamLogo.Size = new System.Drawing.Size(100, 100);
            this.pnlTeamLogo.TabIndex = 13;
            // 
            // dsp
            // 
            this.dsp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dsp.Location = new System.Drawing.Point(118, 95);
            this.dsp.Name = "dsp";
            this.dsp.Size = new System.Drawing.Size(1179, 795);
            this.dsp.TabIndex = 12;
            // 
            // pnlLoadingItems
            // 
            this.pnlLoadingItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.pnlLoadingItems.BorderWidth = 2;
            this.pnlLoadingItems.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.pnlLoadingItems.Location = new System.Drawing.Point(10, 60);
            this.pnlLoadingItems.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.pnlLoadingItems.Name = "pnlLoadingItems";
            this.pnlLoadingItems.Radius = 5;
            this.pnlLoadingItems.Size = new System.Drawing.Size(433, 75);
            this.pnlLoadingItems.TabIndex = 17;
            // 
            // lstConsole
            // 
            this.lstConsole.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstConsole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lstConsole.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstConsole.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lstConsole.ForeColor = System.Drawing.Color.White;
            this.lstConsole.FormattingEnabled = true;
            this.lstConsole.Location = new System.Drawing.Point(9, 151);
            this.lstConsole.Margin = new System.Windows.Forms.Padding(10);
            this.lstConsole.Name = "lstConsole";
            this.lstConsole.Size = new System.Drawing.Size(1356, 260);
            this.lstConsole.TabIndex = 0;
            // 
            // btnShowConsole
            // 
            this.btnShowConsole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnShowConsole.BorderWidth = 2;
            this.btnShowConsole.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnShowConsole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnShowConsole.Location = new System.Drawing.Point(9, 9);
            this.btnShowConsole.Name = "btnShowConsole";
            this.btnShowConsole.Radius = 5;
            this.btnShowConsole.Size = new System.Drawing.Size(103, 34);
            stringFormat1.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat1.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat1.Trimming = System.Drawing.StringTrimming.Character;
            this.btnShowConsole.StringFormat = stringFormat1;
            this.btnShowConsole.TabIndex = 1;
            this.btnShowConsole.Text = "▲ Show Console";
            this.btnShowConsole.TextColor = System.Drawing.Color.White;
            this.btnShowConsole.UseVisualStyleBackColor = false;
            this.btnShowConsole.Click += new System.EventHandler(this.btnShowConsole_Click);
            this.btnShowConsole.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
            this.btnShowConsole.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            // 
            // pnlConsoleOptions
            // 
            this.pnlConsoleOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.pnlConsoleOptions.BorderWidth = 2;
            this.pnlConsoleOptions.Controls.Add(this.btnDismiss);
            this.pnlConsoleOptions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.pnlConsoleOptions.Location = new System.Drawing.Point(826, 60);
            this.pnlConsoleOptions.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.pnlConsoleOptions.Name = "pnlConsoleOptions";
            this.pnlConsoleOptions.Radius = 5;
            this.pnlConsoleOptions.Size = new System.Drawing.Size(433, 75);
            this.pnlConsoleOptions.TabIndex = 18;
            // 
            // btnDismiss
            // 
            this.btnDismiss.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnDismiss.BorderWidth = 2;
            this.btnDismiss.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnDismiss.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnDismiss.Location = new System.Drawing.Point(23, 20);
            this.btnDismiss.Name = "btnDismiss";
            this.btnDismiss.Radius = 5;
            this.btnDismiss.Size = new System.Drawing.Size(156, 34);
            stringFormat2.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat2.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat2.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat2.Trimming = System.Drawing.StringTrimming.Character;
            this.btnDismiss.StringFormat = stringFormat2;
            this.btnDismiss.TabIndex = 18;
            this.btnDismiss.Text = "Dismiss";
            this.btnDismiss.TextColor = System.Drawing.Color.White;
            this.btnDismiss.UseVisualStyleBackColor = false;
            this.btnDismiss.Click += new System.EventHandler(this.btnDismiss_Click);
            this.btnDismiss.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
            this.btnDismiss.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            // 
            // pnlConsole
            // 
            this.pnlConsole.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlConsole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.pnlConsole.BorderWidth = 2;
            this.pnlConsole.Controls.Add(this.pnlConsoleOptions);
            this.pnlConsole.Controls.Add(this.btnShowConsole);
            this.pnlConsole.Controls.Add(this.lstConsole);
            this.pnlConsole.Controls.Add(this.pnlLoadingItems);
            this.pnlConsole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.pnlConsole.Location = new System.Drawing.Point(12, 896);
            this.pnlConsole.Name = "pnlConsole";
            this.pnlConsole.Radius = 5;
            this.pnlConsole.Size = new System.Drawing.Size(1375, 53);
            this.pnlConsole.TabIndex = 6;
            // 
            // lstOptions
            // 
            this.lstOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstOptions.AutoScroll = true;
            this.lstOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.lstOptions.Location = new System.Drawing.Point(12, 6);
            this.lstOptions.Name = "lstOptions";
            this.lstOptions.Size = new System.Drawing.Size(1285, 83);
            this.lstOptions.TabIndex = 14;
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(1405, 961);
            this.Controls.Add(this.lstOptions);
            this.Controls.Add(this.pnlConsole);
            this.Controls.Add(this.pnlTeamLogo);
            this.Controls.Add(this.dsp);
            this.Controls.Add(this.pnlNotifications);
            this.MinimumSize = new System.Drawing.Size(1421, 1000);
            this.Name = "Form_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.SizeChanged += new System.EventHandler(this.Main_SizeChanged);
            this.pnlConsoleOptions.ResumeLayout(false);
            this.pnlConsole.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.FlowLayoutPanel pnlNotifications;
        private System.Windows.Forms.Panel pnlTeamLogo;
        public UILayout.DisplayPanel dsp;
        private CustomControls.RoundedPanel pnlLoadingItems;
        public System.Windows.Forms.CheckedListBox lstConsole;
        private CustomControls.RoundedButton btnShowConsole;
        private CustomControls.RoundedPanel pnlConsoleOptions;
        private CustomControls.RoundedButton btnDismiss;
        private CustomControls.RoundedPanel pnlConsole;
        private Pages.Dialog_List lstOptions;
    }
}

