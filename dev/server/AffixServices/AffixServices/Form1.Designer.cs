namespace AffixServices
{
    partial class Form1
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
            this.tbc = new System.Windows.Forms.TabControl();
            this.tbpMain = new System.Windows.Forms.TabPage();
            this.tbpProcessing = new System.Windows.Forms.TabPage();
            this.btnToggleServer = new System.Windows.Forms.Button();
            this.tbc.SuspendLayout();
            this.tbpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbc
            // 
            this.tbc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbc.Controls.Add(this.tbpMain);
            this.tbc.Controls.Add(this.tbpProcessing);
            this.tbc.Location = new System.Drawing.Point(13, 13);
            this.tbc.Name = "tbc";
            this.tbc.SelectedIndex = 0;
            this.tbc.Size = new System.Drawing.Size(775, 425);
            this.tbc.TabIndex = 0;
            // 
            // tbpMain
            // 
            this.tbpMain.Controls.Add(this.btnToggleServer);
            this.tbpMain.Location = new System.Drawing.Point(4, 22);
            this.tbpMain.Name = "tbpMain";
            this.tbpMain.Padding = new System.Windows.Forms.Padding(3);
            this.tbpMain.Size = new System.Drawing.Size(767, 399);
            this.tbpMain.TabIndex = 0;
            this.tbpMain.Text = "Main";
            this.tbpMain.UseVisualStyleBackColor = true;
            // 
            // tbpProcessing
            // 
            this.tbpProcessing.Location = new System.Drawing.Point(4, 22);
            this.tbpProcessing.Name = "tbpProcessing";
            this.tbpProcessing.Padding = new System.Windows.Forms.Padding(3);
            this.tbpProcessing.Size = new System.Drawing.Size(767, 399);
            this.tbpProcessing.TabIndex = 1;
            this.tbpProcessing.Text = "Processing";
            this.tbpProcessing.UseVisualStyleBackColor = true;
            // 
            // btnToggleServer
            // 
            this.btnToggleServer.Location = new System.Drawing.Point(25, 28);
            this.btnToggleServer.Name = "btnToggleServer";
            this.btnToggleServer.Size = new System.Drawing.Size(157, 41);
            this.btnToggleServer.TabIndex = 0;
            this.btnToggleServer.Text = "Shut Down Server";
            this.btnToggleServer.UseVisualStyleBackColor = true;
            this.btnToggleServer.Click += new System.EventHandler(this.btnToggleServer_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tbc);
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tbc.ResumeLayout(false);
            this.tbpMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbc;
        private System.Windows.Forms.TabPage tbpMain;
        private System.Windows.Forms.TabPage tbpProcessing;
        private System.Windows.Forms.Button btnToggleServer;
    }
}

