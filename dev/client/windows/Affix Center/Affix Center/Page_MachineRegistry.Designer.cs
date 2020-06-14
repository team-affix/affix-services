namespace Affix_Center
{
    partial class Page_MachineRegistry
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
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblAccountName = new System.Windows.Forms.Label();
            this.btnRegisterMachine = new Affix_Center.CustomControls.RoundedButton();
            this.pnlMachineNameHolder = new Affix_Center.CustomControls.RoundedPanel();
            this.txtMachineName = new System.Windows.Forms.TextBox();
            this.pnlMachineNameHolder.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.DimGray;
            this.lblSubtitle.Location = new System.Drawing.Point(39, 33);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(132, 21);
            this.lblSubtitle.TabIndex = 5;
            this.lblSubtitle.Text = "Name this device.";
            // 
            // lblAccountName
            // 
            this.lblAccountName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblAccountName.AutoSize = true;
            this.lblAccountName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblAccountName.ForeColor = System.Drawing.Color.White;
            this.lblAccountName.Location = new System.Drawing.Point(131, 248);
            this.lblAccountName.Name = "lblAccountName";
            this.lblAccountName.Size = new System.Drawing.Size(118, 21);
            this.lblAccountName.TabIndex = 14;
            this.lblAccountName.Text = "Machine Name:";
            // 
            // btnRegisterMachine
            // 
            this.btnRegisterMachine.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRegisterMachine.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnRegisterMachine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegisterMachine.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRegisterMachine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnRegisterMachine.Location = new System.Drawing.Point(266, 344);
            this.btnRegisterMachine.Name = "btnRegisterMachine";
            this.btnRegisterMachine.Size = new System.Drawing.Size(218, 42);
            this.btnRegisterMachine.TabIndex = 16;
            this.btnRegisterMachine.Text = "Register Machine";
            this.btnRegisterMachine.UseVisualStyleBackColor = false;
            // 
            // pnlMachineNameHolder
            // 
            this.pnlMachineNameHolder.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlMachineNameHolder.BorderWidth = 3;
            this.pnlMachineNameHolder.Controls.Add(this.txtMachineName);
            this.pnlMachineNameHolder.ForeColor = System.Drawing.Color.DimGray;
            this.pnlMachineNameHolder.Location = new System.Drawing.Point(135, 287);
            this.pnlMachineNameHolder.Name = "pnlMachineNameHolder";
            this.pnlMachineNameHolder.Radius = 20;
            this.pnlMachineNameHolder.Size = new System.Drawing.Size(485, 36);
            this.pnlMachineNameHolder.TabIndex = 15;
            // 
            // txtMachineName
            // 
            this.txtMachineName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMachineName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.txtMachineName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMachineName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtMachineName.ForeColor = System.Drawing.Color.Yellow;
            this.txtMachineName.Location = new System.Drawing.Point(19, 7);
            this.txtMachineName.Name = "txtMachineName";
            this.txtMachineName.Size = new System.Drawing.Size(447, 22);
            this.txtMachineName.TabIndex = 0;
            // 
            // Page_MachineRegistry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Controls.Add(this.btnRegisterMachine);
            this.Controls.Add(this.pnlMachineNameHolder);
            this.Controls.Add(this.lblAccountName);
            this.Controls.Add(this.lblSubtitle);
            this.Name = "Page_MachineRegistry";
            this.Size = new System.Drawing.Size(751, 635);
            this.pnlMachineNameHolder.ResumeLayout(false);
            this.pnlMachineNameHolder.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.TextBox txtMachineName;
        public CustomControls.RoundedButton btnRegisterMachine;
        public System.Windows.Forms.Label lblSubtitle;
        public CustomControls.RoundedPanel pnlMachineNameHolder;
        public System.Windows.Forms.Label lblAccountName;
    }
}
