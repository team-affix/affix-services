using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Affix_Center
{
    public partial class Lockdown_Account : Form
    {
        public Lockdown_Account()
        {
            InitializeComponent();
        }

        private void lblLockDown_MouseEnter(object sender, EventArgs e)
        {
            pnlLockDown.BackgroundImage = Properties.Resources.LockdownSelected;
        }

        private void lblLockDown_MouseLeave(object sender, EventArgs e)
        {
            pnlLockDown.BackgroundImage = Properties.Resources.Lockdown;
        }
    }
}
