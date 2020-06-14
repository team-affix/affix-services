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
using Aurora.Generalization;

namespace Affix_Center.Pages
{
    public partial class Page_MachineSettings : UserControl
    {
        public bool hasLoaded;
        public Page_MachineSettings()
        {
            InitializeComponent();
        }

        private void Page_MachineSettings_Load(object sender, EventArgs e)
        {
            if (!hasLoaded)
            {
                hasLoaded = true;
                App.Vals.Action_ProgramColorsChanged += ProgramColorsChanged;
                App.Vals.Action_MachineActiveChanged += () =>
                {
                    Invoke((MethodInvoker)(() =>
                    {
                        if(App.Vals.Machine_Active.string_IdentificationName != App.Vals.Machine_ActivePrevious.string_IdentificationName)
                        {
                            MachineNameChanged();
                        }
                        if (App.Vals.Machine_Active.string_IdentificationID != App.Vals.Machine_ActivePrevious.string_IdentificationID)
                        {
                            MachineIDChanged();
                        }
                        if (!App.Vals.Machine_Active.NotificationMethod_ConfigurationSuspiciousActivity.Equals(App.Vals.Machine_ActivePrevious.NotificationMethod_ConfigurationSuspiciousActivity))
                        {
                            SuspiciousActivityChanged();
                        }
                        if (App.Vals.Machine_Active.bool_ConfigurationUseAssociation2FA != App.Vals.Machine_ActivePrevious.bool_ConfigurationUseAssociation2FA)
                        {
                            UseAssociation2FAChanged();
                        }
                    }));
                };
            }
            ProgramColorsChanged();
            MachineNameChanged();
            MachineIDChanged();
        }

        void ProgramColorsChanged()
        {
            BackColor = App.Vals.ProgramColors_Active.BackColor;
            List<RoundedButton> RoundedButtons = new List<RoundedButton>
            {
                btnChangeMachineName,
                btnViewAdminAccounts,
                btnChangeSuspiciousActivityResponse,
                btnChangeAssociation2FA,
            };
            List<TextBox> TextBoxes = new List<TextBox>
            {
                txtMachineName,
                txtMachineID,
                txtAdminAccounts,
                txtSuspiciousActivity,
                txtAssociation2FA,
            };
            List<Label> Titles = new List<Label>
            {
                lblMachineInformation,
                lblMachineSecurity,
                lblMachineName,
                lblMachineID,
                lblAdminAccounts,
                lblSuspiciousActivityResponse,
                lblAssociation2FA,
            };
            List<RoundedPanel> HUD1 = new List<RoundedPanel>
            {
                pnlMachineInformation,
                pnlMachineSecurity,
            };
            List<RoundedPanel> HUD2 = new List<RoundedPanel>
            {
                pnlMachineInfo0,
                pnlMachineSecurity0,
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

        void MachineNameChanged()
        {
            txtMachineName.Text = App.Vals.Machine_Active.string_IdentificationName;
        }

        void MachineIDChanged()
        {
            txtMachineID.Text = App.Vals.Machine_Active.string_IdentificationID;
        }

        void SuspiciousActivityChanged()
        {
            txtSuspiciousActivity.Text = "";
            txtSuspiciousActivity.Text += App.Vals.Machine_Active.NotificationMethod_ConfigurationSuspiciousActivity.Email ? "Email" : "";
            txtSuspiciousActivity.Text += App.Vals.Machine_Active.NotificationMethod_ConfigurationSuspiciousActivity.AffixCenter ? ", Affix Center" : "";
        }

        void UseAssociation2FAChanged()
        {
            txtAssociation2FA.Text = App.Vals.Machine_Active.bool_ConfigurationUseAssociation2FA.ToString();
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
