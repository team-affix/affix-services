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

namespace Affix_Center.Classes
{
    public partial class LeftMenu_AccountAuthenticated : UserControl
    {
        public LeftMenu_AccountAuthenticated()
        {
            InitializeComponent();
        }

        private void LeftMenu_AccountAuthenticated_Load(object sender, EventArgs e)
        {
            App.Vals.Action_ProgramColorsChanged += ProgramColorsChanged;
            ProgramColorsChanged();
        }

        void ProgramColorsChanged()
        {
            BackColor = App.Vals.ProgramColors_Active.BackColor;
            pnlMain.BackColor = App.Vals.ProgramColors_Active.HUDColor1;
            pnlMain.ForeColor = App.Vals.ProgramColors_Active.HUDColor1;
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
            b.TextColor = App.Vals.ProgramColors_Active.TitleColor;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            App.Vals.Form_Main.dsp.Display(App.Vals.Page_Home);
        }
    }
}
