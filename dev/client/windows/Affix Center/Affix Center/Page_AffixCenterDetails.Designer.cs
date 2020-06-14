namespace Affix_Center
{
    partial class Page_AffixCenterDetails
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblService1Name = new System.Windows.Forms.Label();
            this.lblService2Name = new System.Windows.Forms.Label();
            this.lblService1Desc = new System.Windows.Forms.Label();
            this.lblService2Desc = new System.Windows.Forms.Label();
            this.lblWelcomeMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Light", 16F);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(40, 40);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(244, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Welcome to Affix Center!";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.Yellow;
            this.lblSubtitle.Location = new System.Drawing.Point(41, 70);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(402, 21);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Some of the services you have access to are listed below.";
            // 
            // lblService1Name
            // 
            this.lblService1Name.AutoSize = true;
            this.lblService1Name.Font = new System.Drawing.Font("Segoe UI Light", 16F);
            this.lblService1Name.ForeColor = System.Drawing.Color.White;
            this.lblService1Name.Location = new System.Drawing.Point(40, 143);
            this.lblService1Name.Name = "lblService1Name";
            this.lblService1Name.Size = new System.Drawing.Size(310, 30);
            this.lblService1Name.TabIndex = 2;
            this.lblService1Name.Text = "Connecting to a Personal Server";
            // 
            // lblService2Name
            // 
            this.lblService2Name.AutoSize = true;
            this.lblService2Name.Font = new System.Drawing.Font("Segoe UI Light", 16F);
            this.lblService2Name.ForeColor = System.Drawing.Color.White;
            this.lblService2Name.Location = new System.Drawing.Point(40, 284);
            this.lblService2Name.Name = "lblService2Name";
            this.lblService2Name.Size = new System.Drawing.Size(330, 30);
            this.lblService2Name.TabIndex = 3;
            this.lblService2Name.Text = "Hosting Your Own Personal Server";
            // 
            // lblService1Desc
            // 
            this.lblService1Desc.AutoSize = true;
            this.lblService1Desc.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblService1Desc.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblService1Desc.Location = new System.Drawing.Point(41, 173);
            this.lblService1Desc.Name = "lblService1Desc";
            this.lblService1Desc.Size = new System.Drawing.Size(136, 63);
            this.lblService1Desc.TabIndex = 4;
            this.lblService1Desc.Text = "Instant Messaging\r\nFile Storage\r\nFile Sync";
            // 
            // lblService2Desc
            // 
            this.lblService2Desc.AutoSize = true;
            this.lblService2Desc.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblService2Desc.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblService2Desc.Location = new System.Drawing.Point(41, 314);
            this.lblService2Desc.Name = "lblService2Desc";
            this.lblService2Desc.Size = new System.Drawing.Size(208, 42);
            this.lblService2Desc.TabIndex = 5;
            this.lblService2Desc.Text = "Editing Permissions\r\nInviting / Disinviting Persons";
            // 
            // lblWelcomeMessage
            // 
            this.lblWelcomeMessage.AutoSize = true;
            this.lblWelcomeMessage.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblWelcomeMessage.ForeColor = System.Drawing.Color.DimGray;
            this.lblWelcomeMessage.Location = new System.Drawing.Point(41, 425);
            this.lblWelcomeMessage.Name = "lblWelcomeMessage";
            this.lblWelcomeMessage.Size = new System.Drawing.Size(302, 21);
            this.lblWelcomeMessage.TabIndex = 6;
            this.lblWelcomeMessage.Text = "Team Affix Online Services welcomes you. ";
            // 
            // Page_AffixCenterDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Controls.Add(this.lblWelcomeMessage);
            this.Controls.Add(this.lblService2Desc);
            this.Controls.Add(this.lblService1Desc);
            this.Controls.Add(this.lblService2Name);
            this.Controls.Add(this.lblService1Name);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.Name = "Page_AffixCenterDetails";
            this.Size = new System.Drawing.Size(1184, 780);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblTitle;
        public System.Windows.Forms.Label lblSubtitle;
        public System.Windows.Forms.Label lblService1Name;
        public System.Windows.Forms.Label lblService2Name;
        public System.Windows.Forms.Label lblService1Desc;
        public System.Windows.Forms.Label lblService2Desc;
        public System.Windows.Forms.Label lblWelcomeMessage;
    }
}
