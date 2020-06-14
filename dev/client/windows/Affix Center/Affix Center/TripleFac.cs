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
    public partial class TripleFac : Form
    {
        bool closingtocheck = false;
        public TripleFac()
        {
            InitializeComponent();
        }

        private void lblSignInButton_MouseEnter(object sender, EventArgs e)
        {
            pnlEncrypt.BackgroundImage = Properties.Resources.LockdownSelected;
        }

        private void lblSignInButton_MouseLeave(object sender, EventArgs e)
        {
            pnlEncrypt.BackgroundImage = Properties.Resources.Lockdown;
        }

        private void lblSignInButton_Click(object sender, EventArgs e)
        {
            UserCredentials.LFA = txt3FA.Text;
            closingtocheck = true;
            this.Close();
        }

        private void txt3FA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            { 
                UserCredentials.LFA = txt3FA.Text;
                closingtocheck = true;
                this.Close();
            }
        }

        private void TripleFac_Load(object sender, EventArgs e)
        {

        }

        private void TripleFac_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!closingtocheck)
            {
                Environment.Exit(1);
            }
        }
    }
}
