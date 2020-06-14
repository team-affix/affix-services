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


namespace Affix_Center.Forms
{
    public partial class Dialog_LeftSideBar : UserControl
    {
        public Dialog_LeftSideBar()
        {
            InitializeComponent();
        }

        private void Dialog_LeftSideBar_MouseLeave(object sender, EventArgs e)
        {

        }

        private void btnHome_MouseEnter(object sender, EventArgs e)
        {
            App.Methods.void_DisplayHoverInfo(App.Vals.Form_Main.PointToClient(PointToScreen(new Point(btnHome.Right + 10, btnHome.Top))), "Home Page");
        }

        private void btnHome_MouseLeave(object sender, EventArgs e)
        {
            App.Methods.void_ClearHoverInfo();
        }

        private void btnSettings_MouseEnter(object sender, EventArgs e)
        {
            App.Methods.void_DisplayHoverInfo(App.Vals.Form_Main.PointToClient(PointToScreen(new Point(btnSettings.Right + 10, btnSettings.Top))), "Settings Page");
        }

        private void btnSettings_MouseLeave(object sender, EventArgs e)
        {
            App.Methods.void_ClearHoverInfo();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            App.Vals.Form_Main.dsp.Display(App.Vals.Page_ClientHome);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            App.Vals.Form_Main.dsp.Display(App.Vals.Page_Settings);
        }
    }
}
