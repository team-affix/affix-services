using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Affix_Center.Forms
{
    public partial class Form_Loading : Form
    {
        public System.Windows.Forms.Timer Timer_Animation;
        public Form_Loading()
        {
            InitializeComponent();
            App.Vals.Action_ProgramColorsChanged += ProgramColorsChanged;
        }

        private void Form_Loading_Load(object sender, EventArgs e)
        {
            ProgramColorsChanged();
            Timer_Animation = App.Methods.Timer_MakeLoadingAnimation(pnlLoading);
            Timer_Animation.Start();
        }

        void ProgramColorsChanged()
        {
            BackColor = App.Vals.ProgramColors_Active.BackColor;
            List<Dialog_Header> Headers = new List<Dialog_Header>
            {
                hdr,
            };
            Headers.ForEach(h =>
            {
                h.BackColor = App.Vals.ProgramColors_Active.BackColor;
                h.lblTitle.ForeColor = App.Vals.ProgramColors_Active.TitleColor;
                h.lblSubtitle.ForeColor = App.Vals.ProgramColors_Active.SubtitleColor;
                h.pnlGradient.ColorStart = App.Vals.ProgramColors_Active.HUDColor;
                h.pnlGradient.ColorEnd = App.Vals.ProgramColors_Active.BackColor;
            });
        }
    }
}
