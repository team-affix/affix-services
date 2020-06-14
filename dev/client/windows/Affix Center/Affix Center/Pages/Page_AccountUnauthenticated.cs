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
using UILayout;

namespace Affix_Center.Forms
{
    public partial class Page_AccountUnauthenticated : UserControl
    {
        System.Windows.Forms.Timer Timer_AnimateSlide = new System.Windows.Forms.Timer();
        public Page_AccountUnauthenticated()
        {
            InitializeComponent();
            App.Vals.Action_ProgramColorsChanged += ProgramColorsChanged;
        }

        private void Page_AccountUnauthenticated_Load(object sender, EventArgs e)
        {
            ProgramColorsChanged();
            ShowAuthenticateAccount();
        }

        void ProgramColorsChanged()
        {
            List<RoundedButton> RoundedButtons = new List<RoundedButton>
            {

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
                h.pnlGradient.ColorStart = App.Vals.ProgramColors_Active.HUDColor1;
                h.pnlGradient.ColorEnd = App.Vals.ProgramColors_Active.BackColor;
            });
            DisplayPanels.ForEach(d =>
            {
                d.BackColor = App.Vals.ProgramColors_Active.BackColor;
            });
            BackColor = App.Vals.ProgramColors_Active.BackColor;
        }

        public void ShowAuthenticateAccount()
        {
            Timer_AnimateSlide = App.Methods.Timer_MakeSlideAnimation(dsp, new Point(-dsp.Width, dsp.Top), 15, ()=> 
            {
                dsp.Left = this.Width;
                dsp.Display(App.Vals.Page_AuthenticateAccount);
                Timer_AnimateSlide = App.Methods.Timer_MakeSlideAnimation(dsp, new Point(3, dsp.Top), 5, () => 
                { });
                Timer_AnimateSlide.Start();
            });
            Timer_AnimateSlide.Start();
        }

        public void ShowRegisterAccount()
        {
            Timer_AnimateSlide = App.Methods.Timer_MakeSlideAnimation(dsp, new Point(-dsp.Width, dsp.Top), 15, () =>
            {
                dsp.Left = this.Width;
                dsp.Display(App.Vals.Page_RegisterAccount);
                Timer_AnimateSlide = App.Methods.Timer_MakeSlideAnimation(dsp, new Point(3, dsp.Top), 5, () =>
                { });
                Timer_AnimateSlide.Start();
            });
            Timer_AnimateSlide.Start();
        }
    }
}
