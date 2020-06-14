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
using AffixServices.Data;

namespace Affix_Center.Pages
{
    public partial class Page_AccountSettings : UserControl
    {
        bool hasLoaded;
        public Page_AccountSettings()
        {
            InitializeComponent();
        }

        private void Page_AccountSettings_Load(object sender, EventArgs e)
        {
            if (!hasLoaded)
            {
                hasLoaded = true;
                App.Vals.Action_ProgramColorsChanged += ProgramColorsChanged;
                App.Vals.Action_AccountActiveChanged += () =>
                {
                    Invoke((MethodInvoker)(() =>
                    {
                        if (App.Vals.Account_Active.string_IdentificationName != App.Vals.Account_ActivePrevious.string_IdentificationName)
                        {
                            AccountNameChanged();
                        }
                        if (App.Vals.Account_Active.string_IdentificationEmailAddress != App.Vals.Account_ActivePrevious.string_IdentificationEmailAddress)
                        {
                            AccountEmailAddressChanged();
                        }
                        if (App.Vals.Account_Active.string_IdentificationID != App.Vals.Account_ActivePrevious.string_IdentificationID)
                        {
                            AccountIDChanged();
                        }
                        if (!App.Vals.Account_Active.NotificationMethod_ConfigurationSuspiciousActivity.Equals(App.Vals.Account_ActivePrevious.NotificationMethod_ConfigurationSuspiciousActivity))
                        {
                            SuspiciousActivityChanged();
                        }
                        if(App.Vals.Account_Active.bool_ConfigurationUseAssociation2FA != App.Vals.Account_ActivePrevious.bool_ConfigurationUseAssociation2FA)
                        {
                            UseAssociation2FAChanged();
                        }
                    }));
                };
            }
            ProgramColorsChanged();
            AccountNameChanged();
            AccountEmailAddressChanged();
            AccountIDChanged();
            SuspiciousActivityChanged();
            UseAssociation2FAChanged();
        }

        void ProgramColorsChanged()
        {
            BackColor = App.Vals.ProgramColors_Active.BackColor;
            List<RoundedButton> RoundedButtons = new List<RoundedButton>
            {
                btnChangeAccountName,
                btnChangeAccountPassword,
                btnChangeAccountEmailAddress,
                btnChangeSuspiciousActivityResponse,
                btnChangeAssociation2FA,
                btnVerifyBackupCode,
                btnGenerateBackupCode,
            };
            List<TextBox> TextBoxes = new List<TextBox>
            {
                txtAccountName,
                txtAccountEmailAddress,
                txtAccountID,
                txtSuspiciousActivity,
                txtAssociation2FA,
            };
            List<Label> Titles = new List<Label>
            {
                lblAccountInformation,
                lblAccountSecurity,
                lblAccountRecovery,
                lblAccountName,
                lblAccountPassword,
                lblAccountEmailAddress,
                lblAccountID,
                lblSuspiciousActivityResponse,
                lblAssociation2FA,
            };
            List<RoundedPanel> HUD1 = new List<RoundedPanel>
            {
                pnlMachineInformation,
                pnlAccountSecurity,
                pnlAccountRecovery,
            };
            List<RoundedPanel> HUD2 = new List<RoundedPanel>
            {
                pnlAccountInfo0,
                pnlAccountSecurity0,
                pnlAccountRecovery0,
            };
            RoundedButtons.ForEach(r =>
            {
                r.ForeColor = App.Vals.ProgramColors_Active.HUDColor3;
                r.TextColor = App.Vals.ProgramColors_Active.TitleColor;
                r.BackColor = App.Vals.ProgramColors_Active.HUDColor3;
            });
            TextBoxes.ForEach(t =>
            {
                t.BackColor = App.Vals.ProgramColors_Active.HUDColor2;
                t.ForeColor = App.Vals.ProgramColors_Active.SubtitleColor;
            });
            Titles.ForEach(l =>
            {
                l.ForeColor = App.Vals.ProgramColors_Active.TitleColor;
            });
            HUD1.ForEach(p =>
            {
                p.BackColor = App.Vals.ProgramColors_Active.HUDColor1;
                p.ForeColor = App.Vals.ProgramColors_Active.HUDColor1;
            });
            HUD2.ForEach(p =>
            {
                p.BackColor = App.Vals.ProgramColors_Active.HUDColor2;
                p.ForeColor = App.Vals.ProgramColors_Active.HUDColor2;
            });
        }

        void AccountNameChanged()
        {
            txtAccountName.Text = App.Vals.Account_Active.string_IdentificationName;
        }

        void AccountEmailAddressChanged()
        {
            txtAccountEmailAddress.Text = App.Vals.Account_Active.string_IdentificationEmailAddress;
        }

        void AccountIDChanged()
        {
            txtAccountID.Text = App.Vals.Account_Active.string_IdentificationID;
        }

        void SuspiciousActivityChanged()
        {
            txtSuspiciousActivity.Text = "";
            txtSuspiciousActivity.Text += App.Vals.Account_Active.NotificationMethod_ConfigurationSuspiciousActivity.Email ? "Email" : "";
            txtSuspiciousActivity.Text += App.Vals.Account_Active.NotificationMethod_ConfigurationSuspiciousActivity.AffixCenter ? ", Affix Center" : "";
        }

        void UseAssociation2FAChanged()
        {
            txtAssociation2FA.Text = App.Vals.Account_Active.bool_ConfigurationUseAssociation2FA.ToString();
        }

        private void btn_MouseEnter(object sender, EventArgs e)
        {
            RoundedButton b = (RoundedButton)sender;
            b.ForeColor = App.Vals.ProgramColors_Active.SelectedColor;
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            RoundedButton b = (RoundedButton)sender;
            b.ForeColor = App.Vals.ProgramColors_Active.HUDColor3;
        }
    }
}
