using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Affix_Center
{
    public partial class Default_TextField : UserControl
    {
        public override string Text { get { return textBox1.Text; } set { textBox1.Text = value; } }
        public char PasswordChar { get { return textBox1.PasswordChar; } set { textBox1.PasswordChar = value; } }

        public Default_TextField()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            roundedPanel1.ForeColor = App_Vals.ProgramColors_Active.SelectedColor;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            roundedPanel1.ForeColor = App_Vals.ProgramColors_Active.UnselectedColor;
        }
    }
}
