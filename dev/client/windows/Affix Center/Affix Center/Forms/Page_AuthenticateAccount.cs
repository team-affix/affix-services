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
using Aurora.Generalization;
using Security;

namespace Affix_Center.Forms
{
    public partial class Page_AuthenticateAccount : UserControl
    {
        public Page_AuthenticateAccount()
        {
            InitializeComponent();

            txtEmailAddress.textBox.KeyDown += (sender, e) => { if (e.KeyCode == Keys.Enter) { txtPassword.textBox.Focus(); } };
            txtPassword.textBox.KeyDown += (sender, e) => { if (e.KeyCode == Keys.Enter) { Perform(); } };

            App.Vals.Action_ProgramColorsChanged += ProgramColorsChanged;
        }

        private void Page_AuthenticateAccount_Load(object sender, EventArgs e)
        {
            ProgramColorsChanged();
            txtEmailAddress.textBox.Focus();
        }

        void ProgramColorsChanged()
        {
            List<RoundedButton> RoundedButtons = new List<RoundedButton>
            {
                btnAuthenticate,
            };
            List<Default_TextBox> DefaultTextBoxes = new List<Default_TextBox>
            {
                txtEmailAddress,
                txtPassword,
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
                lblEmailAddress,
                lblPassword,
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

        private void txtPassword_Load(object sender, EventArgs e)
        {
            txtPassword.textBox.PasswordChar = '-';
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

        private void btnAuthenticate_Click(object sender, EventArgs e)
        {
            Perform();
        }

        void Perform()
        {
            Enabled = false;
            byte[] IFADecryptionKey = txtPassword.textBox.Text.ToByte();
            Action<bool[]> AuthenticateAccountFail = flags =>
            {
                Invoke((MethodInvoker)(() => { Enabled = true; }));
            };
            Action AuthenticateAccountSucceed = () =>
            {
                Invoke((MethodInvoker)(() => { Enabled = true; }));
            };
            Action<bool[]> GetAccessTokenFail = flags =>
            {
                Invoke((MethodInvoker)(() => { Enabled = true; }));
            };
            Action<byte[], byte[]> GetAccessTokenSucceed = (encryptedIFAPrivateKey, encryptedAccessToken) =>
            {
                try
                {
                    string IFAPrivateKey = Crypt.DecryptECB(encryptedIFAPrivateKey, IFADecryptionKey).To<string>();
                    string accessToken = Crypt.DecryptRSA(IFAPrivateKey, encryptedAccessToken).To<string>();
                    App.Methods.void_AuthenticateAccount(accessToken, AuthenticateAccountFail, AuthenticateAccountSucceed);
                }
                catch (NullReferenceException)
                {
                    Invoke((MethodInvoker)(() => { Enabled = true; }));
                }
            };
            App.Vals.AccountInfo_ActiveLocal.Email = txtEmailAddress.textBox.Text;
            App.Methods.void_GetAccountAccessToken(GetAccessTokenFail, GetAccessTokenSucceed);
        }
    }
}
