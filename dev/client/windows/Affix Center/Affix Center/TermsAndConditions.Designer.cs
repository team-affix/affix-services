namespace Affix_Center
{
    partial class TermsAndConditions
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
            this.btnAccept = new Affix_Center.CustomControls.RoundedButton();
            this.roundedButton1 = new Affix_Center.CustomControls.RoundedButton();
            this.SuspendLayout();
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAccept.BorderColor = System.Drawing.Color.Transparent;
            this.btnAccept.ButtonColor = System.Drawing.Color.DodgerBlue;
            this.btnAccept.FlatAppearance.BorderSize = 0;
            this.btnAccept.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnAccept.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnAccept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccept.Location = new System.Drawing.Point(48, 291);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.OnHoverBorderColor = System.Drawing.Color.Transparent;
            this.btnAccept.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnAccept.OnHoverTextColor = System.Drawing.Color.Gray;
            this.btnAccept.Size = new System.Drawing.Size(162, 46);
            this.btnAccept.TabIndex = 2;
            this.btnAccept.Text = "Accept";
            this.btnAccept.TextColor = System.Drawing.Color.White;
            this.btnAccept.UseVisualStyleBackColor = true;
            // 
            // roundedButton1
            // 
            this.roundedButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.roundedButton1.BorderColor = System.Drawing.Color.DodgerBlue;
            this.roundedButton1.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.roundedButton1.FlatAppearance.BorderSize = 0;
            this.roundedButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.roundedButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.roundedButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.roundedButton1.Location = new System.Drawing.Point(216, 291);
            this.roundedButton1.Name = "roundedButton1";
            this.roundedButton1.OnHoverBorderColor = System.Drawing.Color.Transparent;
            this.roundedButton1.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.roundedButton1.OnHoverTextColor = System.Drawing.Color.Gray;
            this.roundedButton1.Size = new System.Drawing.Size(162, 46);
            this.roundedButton1.TabIndex = 3;
            this.roundedButton1.Text = "Deny";
            this.roundedButton1.TextColor = System.Drawing.Color.White;
            this.roundedButton1.UseVisualStyleBackColor = true;
            // 
            // TermsAndConditions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.Controls.Add(this.roundedButton1);
            this.Controls.Add(this.btnAccept);
            this.Name = "TermsAndConditions";
            this.Size = new System.Drawing.Size(832, 358);
            this.ResumeLayout(false);

        }

        #endregion
        private CustomControls.RoundedButton btnAccept;
        private CustomControls.RoundedButton roundedButton1;
    }
}
