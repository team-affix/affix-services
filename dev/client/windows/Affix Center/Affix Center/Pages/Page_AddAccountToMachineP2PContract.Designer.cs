namespace Affix_Center.Pages
{
    partial class Page_AddAccountToMachineP2PContract
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
            System.Drawing.StringFormat stringFormat2 = new System.Drawing.StringFormat();
            this.lblMachineID = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.btnAddMachine = new Affix_Center.CustomControls.RoundedButton();
            this.txtMachineID = new Affix_Center.Classes.Default_TextBox();
            this.hdr = new Affix_Center.Dialog_Header();
            this.pnlLoading = new System.Windows.Forms.Panel();
            this.lblError = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblMachineID
            // 
            this.lblMachineID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMachineID.AutoSize = true;
            this.lblMachineID.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.lblMachineID.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblMachineID.Location = new System.Drawing.Point(153, 315);
            this.lblMachineID.Name = "lblMachineID";
            this.lblMachineID.Size = new System.Drawing.Size(90, 21);
            this.lblMachineID.TabIndex = 15;
            this.lblMachineID.Text = "Machine ID:";
            // 
            // lblDesc
            // 
            this.lblDesc.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDesc.AutoSize = true;
            this.lblDesc.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.lblDesc.ForeColor = System.Drawing.Color.DimGray;
            this.lblDesc.Location = new System.Drawing.Point(106, 257);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(590, 21);
            this.lblDesc.TabIndex = 13;
            this.lblDesc.Text = "In order to connect to the Machine, the owner will have to invite your account to" +
    " use it.";
            // 
            // btnAddMachine
            // 
            this.btnAddMachine.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAddMachine.BorderWidth = 2;
            this.btnAddMachine.ForeColor = System.Drawing.Color.DimGray;
            this.btnAddMachine.Location = new System.Drawing.Point(303, 405);
            this.btnAddMachine.Name = "btnAddMachine";
            this.btnAddMachine.Radius = 5;
            this.btnAddMachine.Size = new System.Drawing.Size(195, 42);
            stringFormat2.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat2.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat2.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat2.Trimming = System.Drawing.StringTrimming.Character;
            this.btnAddMachine.StringFormat = stringFormat2;
            this.btnAddMachine.TabIndex = 16;
            this.btnAddMachine.Text = "Add Machine";
            this.btnAddMachine.TextColor = System.Drawing.Color.DimGray;
            this.btnAddMachine.UseVisualStyleBackColor = true;
            this.btnAddMachine.Click += new System.EventHandler(this.btnAddMachine_Click);
            this.btnAddMachine.MouseEnter += new System.EventHandler(this.btn_MouseEnter);
            this.btnAddMachine.MouseLeave += new System.EventHandler(this.btn_MouseLeave);
            // 
            // txtMachineID
            // 
            this.txtMachineID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtMachineID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.txtMachineID.Location = new System.Drawing.Point(157, 339);
            this.txtMachineID.Name = "txtMachineID";
            this.txtMachineID.Size = new System.Drawing.Size(492, 59);
            this.txtMachineID.TabIndex = 12;
            this.txtMachineID.Enter += new System.EventHandler(this.txt_Enter);
            this.txtMachineID.Leave += new System.EventHandler(this.txt_Leave);
            // 
            // hdr
            // 
            this.hdr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hdr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.hdr.Location = new System.Drawing.Point(0, 0);
            this.hdr.Name = "hdr";
            this.hdr.Size = new System.Drawing.Size(802, 100);
            this.hdr.Subtitle = "Provide the ID of a machine you\'d like to connect to.";
            this.hdr.TabIndex = 0;
            this.hdr.Title = "Add a Machine";
            // 
            // pnlLoading
            // 
            this.pnlLoading.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlLoading.Location = new System.Drawing.Point(351, 453);
            this.pnlLoading.Name = "pnlLoading";
            this.pnlLoading.Size = new System.Drawing.Size(100, 100);
            this.pnlLoading.TabIndex = 17;
            // 
            // lblError
            // 
            this.lblError.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblError.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.lblError.ForeColor = System.Drawing.Color.Coral;
            this.lblError.Location = new System.Drawing.Point(106, 579);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(591, 53);
            this.lblError.TabIndex = 18;
            this.lblError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Page_AddMachine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.pnlLoading);
            this.Controls.Add(this.btnAddMachine);
            this.Controls.Add(this.lblMachineID);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.txtMachineID);
            this.Controls.Add(this.hdr);
            this.Name = "Page_AddMachine";
            this.Size = new System.Drawing.Size(802, 665);
            this.Load += new System.EventHandler(this.Page_AddMachine_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Dialog_Header hdr;
        private CustomControls.RoundedButton btnAddMachine;
        private System.Windows.Forms.Label lblMachineID;
        private System.Windows.Forms.Label lblDesc;
        private Classes.Default_TextBox txtMachineID;
        private System.Windows.Forms.Panel pnlLoading;
        private System.Windows.Forms.Label lblError;
    }
}
