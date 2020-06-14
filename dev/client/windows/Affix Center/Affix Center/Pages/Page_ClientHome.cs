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
using Affix_Center.Pages;
using AffixServices.Data;
using Aurora.Generalization;
using Affix_Center.Forms;

namespace Affix_Center.Classes
{
    public partial class Page_ClientHome : UserControl
    {
        bool hasLoaded;
        public Page_ClientHome()
        {
            InitializeComponent();
        }

        private void Page_Home_Load(object sender, EventArgs e)
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

            };
            List<Dialog_Header> Headers = new List<Dialog_Header>
            {
                hdr,
            };
            List<Default_TextBox> DefaultTextBoxes = new List<Default_TextBox>
            {

            };
            List<Label> Titles = new List<Label>
            {

            };
            List<Label> Subtitles = new List<Label>
            {

            };
            List<Label> Paragraphs = new List<Label>
            {

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
        }
    }
}
