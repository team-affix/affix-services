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
    public partial class LearnMoreGeneral : Form
    {
        Graphics g;
        public LearnMoreGeneral()
        {
            InitializeComponent();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pnlSecurity_MouseEnter(object sender, EventArgs e)
        {
        }

        private void pnlSecurity_MouseLeave(object sender, EventArgs e)
        {
        }

        private void pnlTeamInformation_MouseEnter(object sender, EventArgs e)
        {
        }

        private void pnlTeamInformation_MouseLeave(object sender, EventArgs e)
        {
        }

        private void pnlSecurity_Enter(object sender, EventArgs e)
        {
        }

        private void pnlLearnMoreGeneral_MouseEnter(object sender, EventArgs e)
        {
        }

        private void pnlSecurity_Click(object sender, EventArgs e)
        {

        }

        private void displaybox(string title, string desc)
        {
        }

        private void LearnMoreGeneral_Load(object sender, EventArgs e)
        {
            g = pnlLearnMoreGeneral.CreateGraphics();
        }

        private void LearnMoreGeneral_Shown(object sender, EventArgs e)
        {
            displaybox("Welcome", "welcome to affix");
        }
    }
}
