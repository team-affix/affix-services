using Affix_Center.Classes;
using Affix_Center.CustomControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UILayout;

namespace Affix_Center.Forms
{
    public partial class Form_AuthenticateAccount : Form
    {
        public Page_AuthenticateAccount Page_AuthenticateAccount = new Page_AuthenticateAccount();
        public Page_RegisterAccount Page_RegisterAccount = new Page_RegisterAccount();

        public Form_AuthenticateAccount()
        {
            InitializeComponent();
            App.Vals.Action_ProgramColorsChanged += ProgramColorsChanged;
            dsp.Display(Page_AuthenticateAccount);
        }

        private void Form_AuthenticateAccount_Load(object sender, EventArgs e)
        {
            ProgramColorsChanged();
        }

        void ProgramColorsChanged()
        {
            List<RoundedButton> RoundedButtons = new List<RoundedButton>
            {
                btnAuthenticateAccount,
                btnRegisterAccount,
            };
            List<Dialog_Header> Headers = new List<Dialog_Header>
            {
                hdr,
            };
            List<DisplayPanel> DisplayPanels = new List<DisplayPanel>
            {
                dsp,
            };
            RoundedButtons.ForEach(r =>
            {
                r.ForeColor = App.Vals.ProgramColors_Active.DeselectedColor;
                r.TextColor = App.Vals.ProgramColors_Active.ParagraphColor;
                r.BackColor = App.Vals.ProgramColors_Active.BackColor;
            });
            Headers.ForEach(h =>
            {
                h.BackColor = App.Vals.ProgramColors_Active.BackColor;
                h.lblTitle.ForeColor = App.Vals.ProgramColors_Active.TitleColor;
                h.lblSubtitle.ForeColor = App.Vals.ProgramColors_Active.SubtitleColor;
                h.pnlGradient.ColorStart = App.Vals.ProgramColors_Active.HUDColor;
                h.pnlGradient.ColorEnd = App.Vals.ProgramColors_Active.BackColor;
            });
            DisplayPanels.ForEach(d =>
            {
                d.BackColor = App.Vals.ProgramColors_Active.BackColor;
            });
            BackColor = App.Vals.ProgramColors_Active.BackColor;
        }

        private void btnRegisterAccount_MouseEnter(object sender, EventArgs e)
        {
            RoundedButton b = (RoundedButton)sender;
            b.ForeColor = App.Vals.ProgramColors_Active.SelectedColor;
            b.TextColor = App.Vals.ProgramColors_Active.SelectedColor;
        }

        private void btnRegisterAccount_MouseLeave(object sender, EventArgs e)
        {
            RoundedButton b = (RoundedButton)sender;
            b.ForeColor = App.Vals.ProgramColors_Active.DeselectedColor;
            b.TextColor = App.Vals.ProgramColors_Active.ParagraphColor;
        }

        private void btnAuthenticateAccount_Click(object sender, EventArgs e)
        {
            dsp.Display(Page_AuthenticateAccount);
        }

        private void btnRegisterAccount_Click(object sender, EventArgs e)
        {
            dsp.Display(Page_RegisterAccount);
        }
    }
}
