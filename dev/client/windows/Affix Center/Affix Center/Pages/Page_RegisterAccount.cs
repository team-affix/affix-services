using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Affix_Center.CustomControls;
using Affix_Center.Classes;

namespace Affix_Center.Forms
{
    public partial class Page_RegisterAccount : UserControl
    {
        public Page_RegisterAccount()
        {
            InitializeComponent();
            App.Vals.Action_ProgramColorsChanged += ProgramColorsChanged;

            txtName.textBox.KeyDown += (sender, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; txtEmailAddress.textBox.Focus(); } };
            txtEmailAddress.textBox.KeyDown += (sender, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; txtPassword.textBox.Focus(); } };
            txtPassword.textBox.KeyDown += (sender, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; txtConfirmPassword.textBox.Focus(); } };
            txtConfirmPassword.textBox.KeyDown += (sender, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; Perform(); } };
        }

        private void Page_AuthenticateAccount_Load(object sender, EventArgs e)
        {
            ProgramColorsChanged();
            txtName.textBox.Focus();
        }

        void ProgramColorsChanged()
        {
            List<RoundedButton> RoundedButtons = new List<RoundedButton>
            {
                btnRegister,
            };
            List<Default_TextBox> DefaultTextBoxes = new List<Default_TextBox>
            {
                txtName,
                txtEmailAddress,
                txtPassword,
                txtConfirmPassword,
            };
            List<Label> Titles = new List<Label>
            {
                lblTitle,
            };
            List<Label> Subtitles = new List<Label>
            {

            };
            List<Label> Paragraphs = new List<Label>
            {
                lblAccountName,
                lblEmailAddress,
                lblPassword,
                lblConfirmPassword,
            };
            RoundedButtons.ForEach(r =>
            {
                r.ForeColor = App.Vals.ProgramColors_Active.DeselectedColor;
                r.TextColor = App.Vals.ProgramColors_Active.ParagraphColor;
                r.BackColor = App.Vals.ProgramColors_Active.BackColor;
            });
            DefaultTextBoxes.ForEach(t =>
            {
                t.BackColor = App.Vals.ProgramColors_Active.BackColor;
                t.textBox.BackColor = App.Vals.ProgramColors_Active.BackColor;
                t.textBox.ForeColor = App.Vals.ProgramColors_Active.ParagraphColor;
                t.panel.ForeColor = App.Vals.ProgramColors_Active.DeselectedColor;
            });
            Titles.ForEach(l =>
            {
                l.ForeColor = App.Vals.ProgramColors_Active.TitleColor;
            });
            Subtitles.ForEach(l =>
            {
                l.ForeColor = App.Vals.ProgramColors_Active.SubtitleColor;
            });
            Paragraphs.ForEach(l =>
            {
                l.ForeColor = App.Vals.ProgramColors_Active.ParagraphColor;
            });
            BackColor = App.Vals.ProgramColors_Active.BackColor;
        }

        private void txt_Enter(object sender, EventArgs e)
        {
            Default_TextBox d = (Default_TextBox)sender;
            d.textBox.ForeColor = App.Vals.ProgramColors_Active.SelectedColor;
            d.panel.ForeColor = App.Vals.ProgramColors_Active.SelectedColor;
        }

        private void txt_Leave(object sender, EventArgs e)
        {
            Default_TextBox d = (Default_TextBox)sender;
            d.textBox.ForeColor = App.Vals.ProgramColors_Active.ParagraphColor;
            d.panel.ForeColor = App.Vals.ProgramColors_Active.DeselectedColor;
        }

        private void btn_MouseEnter(object sender, EventArgs e)
        {
            RoundedButton b = (RoundedButton)sender;
            b.ForeColor = App.Vals.ProgramColors_Active.SelectedColor;
            b.TextColor = App.Vals.ProgramColors_Active.SelectedColor;

        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            RoundedButton b = (RoundedButton)sender;
            b.ForeColor = App.Vals.ProgramColors_Active.DeselectedColor;
            b.TextColor = App.Vals.ProgramColors_Active.ParagraphColor;
        }

        private void lblConfirmPassword_Click(object sender, EventArgs e)
        {

        }

        private void lblPassword_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_Load(object sender, EventArgs e)
        {
            txtPassword.textBox.PasswordChar = '-';
        }

        private void txtConfirmPassword_Load(object sender, EventArgs e)
        {
            txtConfirmPassword.textBox.PasswordChar = '-';
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Perform();
        }

        private void btnAuthenticate_Click(object sender, EventArgs e)
        {
            App.Vals.Page_AccountUnauthenticated.ShowAuthenticateAccount();
        }

        private void Perform()
        {
            Enabled = false;
            Action<bool[]> RegisterAccountFail = flags =>
            {
                Invoke((MethodInvoker)(() => { Enabled = true; }));
            };
            Action RegisterAccountSucceed = () =>
            {
                Invoke((MethodInvoker)(() => { Enabled = true; }));
            };
            App.Methods.void_RegisterAccount(txtName.textBox.Text, txtEmailAddress.textBox.Text, txtPassword.textBox.Text, RegisterAccountFail, RegisterAccountSucceed);
        }
    }
}
