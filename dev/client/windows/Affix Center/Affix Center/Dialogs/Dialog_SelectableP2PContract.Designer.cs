namespace Affix_Center.Pages
{
    partial class Dialog_SelectableP2PContract
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
            System.Drawing.StringFormat stringFormat3 = new System.Drawing.StringFormat();
            this.btnSelect = new Affix_Center.CustomControls.RoundedButton();
            this.pnlMain = new Affix_Center.CustomControls.RoundedPanel();
            this.txtMachineID = new System.Windows.Forms.TextBox();
            this.lblIDTitle = new System.Windows.Forms.Label();
            this.lblNameTitle = new System.Windows.Forms.Label();
            this.txtMachineStatus = new System.Windows.Forms.TextBox();
            this.txtMachineName = new System.Windows.Forms.TextBox();
            this.lblStatusTitle = new System.Windows.Forms.Label();
            this.btnDeny = new Affix_Center.CustomControls.RoundedButton();
            this.btnAccept = new Affix_Center.CustomControls.RoundedButton();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnSelect.BorderWidth = 2;
            this.btnSelect.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSelect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.btnSelect.Location = new System.Drawing.Point(241, 63);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(1);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Radius = 5;
            this.btnSelect.Size = new System.Drawing.Size(30, 30);
            stringFormat1.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat1.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat1.Trimming = System.Drawing.StringTrimming.Character;
            this.btnSelect.StringFormat = stringFormat1;
            this.btnSelect.TabIndex = 27;
            this.btnSelect.Text = "->";
            this.btnSelect.TextColor = System.Drawing.Color.White;
            this.btnSelect.UseVisualStyleBackColor = false;
            this.btnSelect.MouseEnter += new System.EventHandler(this.btnSelect_MouseEnter);
            this.btnSelect.MouseLeave += new System.EventHandler(this.btnSelect_MouseLeave);
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.pnlMain.BorderWidth = 2;
            this.pnlMain.Controls.Add(this.txtMachineID);
            this.pnlMain.Controls.Add(this.lblIDTitle);
            this.pnlMain.Controls.Add(this.lblNameTitle);
            this.pnlMain.Controls.Add(this.txtMachineStatus);
            this.pnlMain.Controls.Add(this.txtMachineName);
            this.pnlMain.Controls.Add(this.lblStatusTitle);
            this.pnlMain.Location = new System.Drawing.Point(4, 3);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Radius = 5;
            this.pnlMain.Size = new System.Drawing.Size(233, 100);
            this.pnlMain.TabIndex = 26;
            // 
            // txtMachineID
            // 
            this.txtMachineID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.txtMachineID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMachineID.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.txtMachineID.ForeColor = System.Drawing.Color.DodgerBlue;
            this.txtMachineID.Location = new System.Drawing.Point(80, 22);
            this.txtMachineID.Name = "txtMachineID";
            this.txtMachineID.ReadOnly = true;
            this.txtMachineID.Size = new System.Drawing.Size(132, 15);
            this.txtMachineID.TabIndex = 20;
            this.txtMachineID.TextChanged += new System.EventHandler(this.txtMachineID_TextChanged);
            // 
            // lblIDTitle
            // 
            this.lblIDTitle.AutoSize = true;
            this.lblIDTitle.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblIDTitle.ForeColor = System.Drawing.Color.White;
            this.lblIDTitle.Location = new System.Drawing.Point(21, 24);
            this.lblIDTitle.Name = "lblIDTitle";
            this.lblIDTitle.Size = new System.Drawing.Size(21, 13);
            this.lblIDTitle.TabIndex = 0;
            this.lblIDTitle.Text = "ID:";
            this.lblIDTitle.Click += new System.EventHandler(this.lblIDTitle_Click);
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.AutoSize = true;
            this.lblNameTitle.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblNameTitle.ForeColor = System.Drawing.Color.White;
            this.lblNameTitle.Location = new System.Drawing.Point(21, 43);
            this.lblNameTitle.Name = "lblNameTitle";
            this.lblNameTitle.Size = new System.Drawing.Size(39, 13);
            this.lblNameTitle.TabIndex = 1;
            this.lblNameTitle.Text = "Name:";
            this.lblNameTitle.Click += new System.EventHandler(this.lblNameTitle_Click);
            // 
            // txtMachineStatus
            // 
            this.txtMachineStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.txtMachineStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMachineStatus.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.txtMachineStatus.ForeColor = System.Drawing.Color.DodgerBlue;
            this.txtMachineStatus.Location = new System.Drawing.Point(80, 64);
            this.txtMachineStatus.Name = "txtMachineStatus";
            this.txtMachineStatus.ReadOnly = true;
            this.txtMachineStatus.Size = new System.Drawing.Size(132, 15);
            this.txtMachineStatus.TabIndex = 23;
            // 
            // txtMachineName
            // 
            this.txtMachineName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.txtMachineName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMachineName.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.txtMachineName.ForeColor = System.Drawing.Color.DodgerBlue;
            this.txtMachineName.Location = new System.Drawing.Point(80, 43);
            this.txtMachineName.Name = "txtMachineName";
            this.txtMachineName.ReadOnly = true;
            this.txtMachineName.Size = new System.Drawing.Size(132, 15);
            this.txtMachineName.TabIndex = 21;
            this.txtMachineName.TextChanged += new System.EventHandler(this.txtMachineName_TextChanged);
            // 
            // lblStatusTitle
            // 
            this.lblStatusTitle.AutoSize = true;
            this.lblStatusTitle.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblStatusTitle.ForeColor = System.Drawing.Color.White;
            this.lblStatusTitle.Location = new System.Drawing.Point(21, 64);
            this.lblStatusTitle.Name = "lblStatusTitle";
            this.lblStatusTitle.Size = new System.Drawing.Size(53, 13);
            this.lblStatusTitle.TabIndex = 22;
            this.lblStatusTitle.Text = "Pending:";
            this.lblStatusTitle.Click += new System.EventHandler(this.lblStatusTitle_Click);
            // 
            // btnDeny
            // 
            this.btnDeny.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeny.BackColor = System.Drawing.Color.Red;
            this.btnDeny.BorderWidth = 2;
            this.btnDeny.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDeny.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnDeny.Location = new System.Drawing.Point(140, 107);
            this.btnDeny.Margin = new System.Windows.Forms.Padding(1);
            this.btnDeny.Name = "btnDeny";
            this.btnDeny.Radius = 5;
            this.btnDeny.Size = new System.Drawing.Size(90, 37);
            stringFormat2.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat2.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat2.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat2.Trimming = System.Drawing.StringTrimming.Character;
            this.btnDeny.StringFormat = stringFormat2;
            this.btnDeny.TabIndex = 25;
            this.btnDeny.Text = "Deny";
            this.btnDeny.TextColor = System.Drawing.Color.White;
            this.btnDeny.UseVisualStyleBackColor = false;
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccept.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnAccept.BorderWidth = 2;
            this.btnAccept.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnAccept.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnAccept.Location = new System.Drawing.Point(48, 107);
            this.btnAccept.Margin = new System.Windows.Forms.Padding(1);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Radius = 5;
            this.btnAccept.Size = new System.Drawing.Size(90, 37);
            stringFormat3.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat3.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat3.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat3.Trimming = System.Drawing.StringTrimming.Character;
            this.btnAccept.StringFormat = stringFormat3;
            this.btnAccept.TabIndex = 24;
            this.btnAccept.Text = "Accept";
            this.btnAccept.TextColor = System.Drawing.Color.White;
            this.btnAccept.UseVisualStyleBackColor = false;
            // 
            // Dialog_SelectableP2PContract
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.btnDeny);
            this.Controls.Add(this.btnAccept);
            this.Name = "Dialog_SelectableP2PContract";
            this.Size = new System.Drawing.Size(279, 156);
            this.Load += new System.EventHandler(this.Dialog_SelectableMachine_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblIDTitle;
        private System.Windows.Forms.Label lblNameTitle;
        private System.Windows.Forms.TextBox txtMachineID;
        private System.Windows.Forms.TextBox txtMachineName;
        private System.Windows.Forms.Label lblStatusTitle;
        private System.Windows.Forms.TextBox txtMachineStatus;
        private CustomControls.RoundedButton btnAccept;
        private CustomControls.RoundedButton btnDeny;
        public CustomControls.RoundedPanel pnlMain;
        public CustomControls.RoundedButton btnSelect;
    }
}
