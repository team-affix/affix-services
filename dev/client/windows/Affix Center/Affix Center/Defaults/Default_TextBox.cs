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
    public partial class Default_TextBox : UserControl
    {
        public Default_TextBox()
        {
            InitializeComponent();
        }

        private void Default_TextBox_Click(object sender, EventArgs e)
        {
            textBox.Focus();
        }

        private void Default_TextBox_Paint(object sender, PaintEventArgs e)
        {
            textBox.BackColor = BackColor;
        }
    }
}
