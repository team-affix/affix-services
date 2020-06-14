using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;

namespace Affix_Center.Classes
{
    public partial class Page_ServerHome : UserControl
    {
        bool hasLoaded;
        public Page_ServerHome()
        {
            InitializeComponent();
        }

        private void Page_ServerHome_Load(object sender, EventArgs e)
        {
            if (!hasLoaded) 
            {
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
            List<Dialog_Header> Headers = new List<Dialog_Header>
            {
                hdr,
            };
            Headers.ForEach(h =>
            {
                h.BackColor = App.Vals.ProgramColors_Active.BackColor;
                h.lblTitle.ForeColor = App.Vals.ProgramColors_Active.TitleColor;
                h.lblSubtitle.ForeColor = App.Vals.ProgramColors_Active.SubtitleColor;
                h.pnlGradient.ColorStart = App.Vals.ProgramColors_Active.HUDColor1;
                h.pnlGradient.ColorEnd = App.Vals.ProgramColors_Active.BackColor;
            });
        }
    }
}
