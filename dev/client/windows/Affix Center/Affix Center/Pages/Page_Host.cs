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

namespace Affix_Center.Pages
{
    public partial class Page_Host : UserControl
    {
        bool hasLoaded;
        public Page_Host()
        {
            InitializeComponent();
        }

        private void Page_Host_Load(object sender, EventArgs e)
        {
            if (!hasLoaded)
            {
                hasLoaded = true;
                StartUp();
            }
        }
        
        void StartUp()
        {
            App.Vals.Action_ProgramColorsChanged += ProgramColorsChanged;
            ProgramColorsChanged();
        }

        void ProgramColorsChanged()
        {
            BackColor = App.Vals.ProgramColors_Active.BackColor;
            List<RoundedButton> RoundedButtons = new List<RoundedButton>
            {
                btnToggleServer,
                btnToggleMessaging,
                btnToggleFileStorage,
            };
            List<Dialog_Header> Headers = new List<Dialog_Header>
            {
                hdr,
            };
            List<TextBox> TextBoxes = new List<TextBox>
            {
                txtNumberMembers,
                txtNumberConnected,
                txtIPv4,
                txtNatType,
                txtCompatibility,
            };
            List<Label> Titles = new List<Label>
            {
                lblNumberMembers,
                lblNumberConnected,
                lblIPv4,
                lblNatType,
                lblCompatibility,
            };
            List<RoundedPanel> HUD1 = new List<RoundedPanel>
            {
                pnlConnections,
                pnlInfo,
            };
            List<RoundedPanel> HUD2 = new List<RoundedPanel>
            {
                pnlConnectionInfo,
                pnlServerInfo0,
                pnlServerInfo1,
                pnlServerInfo2,
            };
            List<RoundedPanel> HUD3 = new List<RoundedPanel>
            {
                pnlToggleServerLoading,
                pnlToggleMessagingLoading,
                pnlToggleFileStorageLoading,

            };
            RoundedButtons.ForEach(r =>
            {
                r.ForeColor = App.Vals.ProgramColors_Active.HUDColor3;
                r.TextColor = App.Vals.ProgramColors_Active.TitleColor;
                r.BackColor = App.Vals.ProgramColors_Active.HUDColor3;
            });
            Headers.ForEach(h =>
            {
                h.BackColor = App.Vals.ProgramColors_Active.BackColor;
                h.lblTitle.ForeColor = App.Vals.ProgramColors_Active.TitleColor;
                h.lblSubtitle.ForeColor = App.Vals.ProgramColors_Active.SubtitleColor;
                h.pnlGradient.ColorStart = App.Vals.ProgramColors_Active.HUDColor1;
                h.pnlGradient.ColorEnd = App.Vals.ProgramColors_Active.BackColor;
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
            HUD3.ForEach(p =>
            {
                p.BackColor = App.Vals.ProgramColors_Active.HUDColor3;
                p.ForeColor = App.Vals.ProgramColors_Active.HUDColor3;
            });
        }

        private void pnlServerInfo1_Paint(object sender, PaintEventArgs e)
        {

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

        private void btnToggleServer_Click(object sender, EventArgs e)
        {

        }

        private void btnToggleMessaging_Click(object sender, EventArgs e)
        {

        }

        private void btnToggleFileStorage_Click(object sender, EventArgs e)
        {

        }
    }
}
