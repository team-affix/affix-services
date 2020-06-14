using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Affix_Center.CustomControls;

namespace Affix_Center.Classes
{
    public partial class Form_TC : Form
    {
        public Form_TC()
        {
            InitializeComponent();
        }

        private void Form_TC_Load(object sender, EventArgs e)
        {
            ProgramColorsChanged();
            App.Vals.Action_ProgramColorsChanged += ProgramColorsChanged;
        }

        void ProgramColorsChanged()
        {
            BackColor = App.Vals.ProgramColors_Active.BackColor;
            List<RoundedButton> RoundedButtons = new List<RoundedButton>
            {
                btnAccept,
                btnDeny,
            };
            List<Dialog_Header> Headers = new List<Dialog_Header>
            {
                hdr,
            };
            List<Label> Titles = new List<Label>
            {
                lblTitle,
            };
            List<Label> Paragraphs = new List<Label>
            {
                lblDesc,
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
            Titles.ForEach(l =>
            {
                l.ForeColor = App.Vals.ProgramColors_Active.TitleColor;
            });
            Paragraphs.ForEach(l =>
            {
                l.ForeColor = App.Vals.ProgramColors_Active.ParagraphColor;
            });
        }

        private void btnAccept_MouseEnter(object sender, EventArgs e)
        {
            RoundedButton r = (RoundedButton)sender;
            r.ForeColor = App.Vals.ProgramColors_Active.SelectedColor;
            r.TextColor = App.Vals.ProgramColors_Active.SelectedColor;
        }

        private void btnAccept_MouseLeave(object sender, EventArgs e)
        {
            RoundedButton r = (RoundedButton)sender;
            r.ForeColor = App.Vals.ProgramColors_Active.DeselectedColor;
            r.TextColor = App.Vals.ProgramColors_Active.ParagraphColor;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            App.Vals.bool_TCAccepted = true;
            Close();
        }

        private void btnDeny_Click(object sender, EventArgs e)
        {
            App.Vals.bool_TCAccepted = false;
            Close();
        }
    }
}
