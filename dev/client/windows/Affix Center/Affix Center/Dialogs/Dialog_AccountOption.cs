using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Affix_Center.Pages
{
    public partial class Dialog_AccountOption : UserControl
    {
        public Dialog_AccountOption(string AccountName)
        {
            InitializeComponent();
            lblUsername.Text = AccountName;
        }

        private void Dialog_AccountOption_Load(object sender, EventArgs e)
        {
            ProgramColorsChanged();
        }

        void ProgramColorsChanged()
        {
            lblUsername.ForeColor = App.Vals.ProgramColors_Active.TitleColor;
            BackColor = App.Vals.ProgramColors_Active.HUDColor1;
        }
    }
}
