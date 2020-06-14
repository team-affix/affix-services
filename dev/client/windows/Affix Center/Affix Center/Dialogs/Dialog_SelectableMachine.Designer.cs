namespace Affix_Center.Pages
{
    partial class Dialog_SelectableMachine
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
            this.lblIDTitle = new System.Windows.Forms.Label();
            this.lblNameTitle = new System.Windows.Forms.Label();
            this.txtMachineID = new System.Windows.Forms.TextBox();
            this.txtMachineName = new System.Windows.Forms.TextBox();
            this.lblStatusTitle = new System.Windows.Forms.Label();
            this.txtMachineStatus = new System.Windows.Forms.TextBox();
            this.btnConnect = new Affix_Center.CustomControls.RoundedButton();
            this.SuspendLayout();
            // 
            // lblIDTitle
            // 
            this.lblIDTitle.AutoSize = true;
            this.lblIDTitle.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblIDTitle.ForeColor = System.Drawing.Color.White;
            this.lblIDTitle.Location = new System.Drawing.Point(12, 12);
            this.lblIDTitle.Name = "lblIDTitle";
            this.lblIDTitle.Size = new System.Drawing.Size(21, 13);
            this.lblIDTitle.TabIndex = 0;
            this.lblIDTitle.Text = "ID:";
            // 
            // lblNameTitle
            // 
            this.lblNameTitle.AutoSize = true;
            this.lblNameTitle.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblNameTitle.ForeColor = System.Drawing.Color.White;
            this.lblNameTitle.Location = new System.Drawing.Point(12, 31);
            this.lblNameTitle.Name = "lblNameTitle";
            this.lblNameTitle.Size = new System.Drawing.Size(39, 13);
            this.lblNameTitle.TabIndex = 1;
            this.lblNameTitle.Text = "Name:";
            // 
            // txtMachineID
            // 
            this.txtMachineID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.txtMachineID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMachineID.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.txtMachineID.ForeColor = System.Drawing.Color.DodgerBlue;
            this.txtMachineID.Location = new System.Drawing.Point(64, 10);
            this.txtMachineID.Name = "txtMachineID";
            this.txtMachineID.ReadOnly = true;
            this.txtMachineID.Size = new System.Drawing.Size(130, 15);
            this.txtMachineID.TabIndex = 20;
            // 
            // txtMachineName
            // 
            this.txtMachineName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.txtMachineName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMachineName.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.txtMachineName.ForeColor = System.Drawing.Color.DodgerBlue;
            this.txtMachineName.Location = new System.Drawing.Point(64, 31);
            this.txtMachineName.Name = "txtMachineName";
            this.txtMachineName.ReadOnly = true;
            this.txtMachineName.Size = new System.Drawing.Size(130, 15);
            this.txtMachineName.TabIndex = 21;
            // 
            // lblStatusTitle
            // 
            this.lblStatusTitle.AutoSize = true;
            this.lblStatusTitle.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblStatusTitle.ForeColor = System.Drawing.Color.White;
            this.lblStatusTitle.Location = new System.Drawing.Point(12, 52);
            this.lblStatusTitle.Name = "lblStatusTitle";
            this.lblStatusTitle.Size = new System.Drawing.Size(53, 13);
            this.lblStatusTitle.TabIndex = 22;
            this.lblStatusTitle.Text = "Pending:";
            // 
            // txtMachineStatus
            // 
            this.txtMachineStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.txtMachineStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMachineStatus.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.txtMachineStatus.ForeColor = System.Drawing.Color.DodgerBlue;
            this.txtMachineStatus.Location = new System.Drawing.Point(64, 52);
            this.txtMachineStatus.Name = "txtMachineStatus";
            this.txtMachineStatus.ReadOnly = true;
            this.txtMachineStatus.Size = new System.Drawing.Size(130, 15);
            this.txtMachineStatus.TabIndex = 23;
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnConnect.BorderWidth = 2;
            this.btnConnect.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnConnect.ForeColor = System.Drawing.Color.DimGray;
            this.btnConnect.Location = new System.Drawing.Point(11, 79);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Radius = 5;
            this.btnConnect.Size = new System.Drawing.Size(173, 28);
            stringFormat1.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat1.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat1.Trimming = System.Drawing.StringTrimming.Character;
            this.btnConnect.StringFormat = stringFormat1;
            this.btnConnect.TabIndex = 24;
            this.btnConnect.Text = "Connect";
            this.btnConnect.TextColor = System.Drawing.Color.White;
            this.btnConnect.UseVisualStyleBackColor = false;
            // 
            // Dialog_SelectableMachine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtMachineStatus);
            this.Controls.Add(this.lblStatusTitle);
            this.Controls.Add(this.txtMachineName);
            this.Controls.Add(this.txtMachineID);
            this.Controls.Add(this.lblNameTitle);
            this.Controls.Add(this.lblIDTitle);
            this.Name = "Dialog_SelectableMachine";
            this.Size = new System.Drawing.Size(194, 117);
            this.Load += new System.EventHandler(this.Dialog_SelectableMachine_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIDTitle;
        private System.Windows.Forms.Label lblNameTitle;
        private System.Windows.Forms.TextBox txtMachineID;
        private System.Windows.Forms.TextBox txtMachineName;
        private System.Windows.Forms.Label lblStatusTitle;
        private System.Windows.Forms.TextBox txtMachineStatus;
        private CustomControls.RoundedButton btnConnect;
    }
}
