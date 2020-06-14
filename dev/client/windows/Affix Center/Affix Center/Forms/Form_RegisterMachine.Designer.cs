namespace Affix_Center.Classes
{
    partial class Form_RegisterMachine
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.lblMachineName = new System.Windows.Forms.Label();
            this.btnRegisterMachine = new Affix_Center.CustomControls.RoundedButton();
            this.txtMachineName = new Affix_Center.Classes.Default_TextBox();
            this.hdr = new Affix_Center.Dialog_Header();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(93, 183);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(232, 21);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Thank you for your cooperation.";
            // 
            // lblDesc
            // 
            this.lblDesc.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDesc.AutoSize = true;
            this.lblDesc.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.lblDesc.ForeColor = System.Drawing.Color.DimGray;
            this.lblDesc.Location = new System.Drawing.Point(93, 162);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(410, 21);
            this.lblDesc.TabIndex = 2;
            this.lblDesc.Text = "For access to online services, you must register this machine.";
            // 
            // lblMachineName
            // 
            this.lblMachineName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblMachineName.AutoSize = true;
            this.lblMachineName.Font = new System.Drawing.Font("Segoe UI Light", 12F);
            this.lblMachineName.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblMachineName.Location = new System.Drawing.Point(323, 251);
            this.lblMachineName.Name = "lblMachineName";
            this.lblMachineName.Size = new System.Drawing.Size(115, 21);
            this.lblMachineName.TabIndex = 4;
            this.lblMachineName.Text = "Machine Name:";
            // 
            // btnRegisterMachine
            // 
            this.btnRegisterMachine.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRegisterMachine.BorderWidth = 2;
            this.btnRegisterMachine.ForeColor = System.Drawing.Color.DimGray;
            this.btnRegisterMachine.Location = new System.Drawing.Point(473, 341);
            this.btnRegisterMachine.Name = "btnRegisterMachine";
            this.btnRegisterMachine.Radius = 5;
            this.btnRegisterMachine.Size = new System.Drawing.Size(195, 42);
            stringFormat1.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat1.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat1.Trimming = System.Drawing.StringTrimming.Character;
            this.btnRegisterMachine.StringFormat = stringFormat1;
            this.btnRegisterMachine.TabIndex = 5;
            this.btnRegisterMachine.Text = "Register Machine";
            this.btnRegisterMachine.TextColor = System.Drawing.Color.DimGray;
            this.btnRegisterMachine.UseVisualStyleBackColor = true;
            this.btnRegisterMachine.Click += new System.EventHandler(this.btnRegisterMachine_Click);
            this.btnRegisterMachine.MouseEnter += new System.EventHandler(this.btnRegisterMachine_MouseEnter);
            this.btnRegisterMachine.MouseLeave += new System.EventHandler(this.btnRegisterMachine_MouseLeave);
            // 
            // txtMachineName
            // 
            this.txtMachineName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtMachineName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.txtMachineName.Location = new System.Drawing.Point(327, 275);
            this.txtMachineName.Name = "txtMachineName";
            this.txtMachineName.Size = new System.Drawing.Size(492, 59);
            this.txtMachineName.TabIndex = 1;
            // 
            // hdr
            // 
            this.hdr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hdr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.hdr.Location = new System.Drawing.Point(0, 0);
            this.hdr.Name = "hdr";
            this.hdr.Size = new System.Drawing.Size(1144, 100);
            this.hdr.Subtitle = "Register your machine to Affix Services.";
            this.hdr.TabIndex = 0;
            this.hdr.Title = "Machine Registry";
            // 
            // Form_RegisterMachine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(1142, 450);
            this.Controls.Add(this.btnRegisterMachine);
            this.Controls.Add(this.lblMachineName);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.txtMachineName);
            this.Controls.Add(this.hdr);
            this.Name = "Form_RegisterMachine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.Load += new System.EventHandler(this.Form_RegisterMachine_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Dialog_Header hdr;
        private Default_TextBox txtMachineName;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.Label lblMachineName;
        private CustomControls.RoundedButton btnRegisterMachine;
    }
}