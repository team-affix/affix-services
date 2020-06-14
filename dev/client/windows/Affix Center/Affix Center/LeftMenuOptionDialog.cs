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

namespace Affix_Center
{
    public partial class LeftMenuOptionDialog : UserControl
    {
        public string Title { get { return _Title; } set { _Title = value; this.lblTitle.Text = value; } }
        string _Title;

        int Index = 0;
        Timer Timer_MouseEnter = new Timer() { Interval = 1 };
        Timer Timer_MouseLeave = new Timer() { Interval = 1 };

        public LeftMenuOptionDialog()
        {
            InitializeComponent();
            Timer_MouseEnter.Tick += Animate_AnimationMouseEnter;
            Timer_MouseLeave.Tick += Animate_AnimationMouseLeave;
            Timer_MouseEnter.Tick += (x, y) => { if (Index >= 30) { Timer_MouseEnter.Stop(); Index = 30; } };
            Timer_MouseLeave.Tick += (x, y) => { if (Index <= 0) { Timer_MouseLeave.Stop(); Index = 0; } };
            this.DoubleBuffered = true;
        }

        private void LeftMenuOptionDialog_MouseEnter(object sender, EventArgs e)
        {
            Timer_MouseLeave.Stop();
            Timer_MouseEnter.Start();
        }

        private void LeftMenuOptionDialog_MouseLeave(object sender, EventArgs e)
        {
            Timer_MouseEnter.Stop();
            Timer_MouseLeave.Start();
        }

        private void Animate_AnimationMouseEnter(object sender, EventArgs e)
        {
            this.Invalidate();
            Index++;
        }

        private void Animate_AnimationMouseLeave(object sender, EventArgs e)
        {
            this.Invalidate();
            Index--;
        }

        private void LeftMenuOptionDialog_Paint(object sender, PaintEventArgs e)
        {
            int Radius = (int)(10 * Math.Tanh(0.2 * Index));
            e.Graphics.FillRoundedRectangle(new SolidBrush(Color.FromArgb((int)(10 * Math.Tanh(0.1 * Index) + 20), (int)(10 * Math.Tanh(0.1 * Index) + 20), (int)(10 * Math.Tanh(0.1 * Index) + 20))), Radius, Radius, Width - Radius * 2, Height - Radius * 2, Radius);
            e.Graphics.DrawRoundedRectangle(new Pen(Color.FromArgb((int)(50 * Math.Tanh(0.1 * Index) + 20), (int)(235 * Math.Tanh(0.1 * Index) + 20), (int)(120 * Math.Tanh(0.1 * Index)) + 20), 2), Radius, Radius, this.Width - Radius * 2, this.Height - Radius * 2, Radius);
            lblTitle.ForeColor = Color.FromArgb((int)(50 * Math.Tanh(0.1 * Index) + 20), (int)(235 * Math.Tanh(0.1 * Index) + 20), (int)(120 * Math.Tanh(0.1 * Index)) + 20);
        }
    }
}
