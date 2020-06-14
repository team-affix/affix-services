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
    public partial class LoadingDialog : UserControl
    {

        int SweepAngle = 100;
        int StrideLength = 10;
        int PenWidth = 2;

        Rectangle GraphicsRectangle;
        Graphics Graphics;
        Timer UpdateGraphicsTimer = new Timer();
        Pen DrawPen;
        Pen ClearPen;

        int Index;

        public LoadingDialog(Color Color)
        {
            InitializeComponent();
            Graphics = this.CreateGraphics();
            Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            DrawPen = new Pen(Color, PenWidth);
            ClearPen = new Pen(this.BackColor, PenWidth + 4);
            UpdateGraphicsTimer.Interval = 1;
            UpdateGraphicsTimer.Tick += UpdateGraphicsTimer_Tick;
        }

        private void LoadingDialog_Load(object sender, EventArgs e)
        {
            UpdateGraphicsTimer.Start();
            GraphicsRectangle = new Rectangle(PenWidth, PenWidth, this.Width - PenWidth * 2, this.Height - PenWidth * 2);
        }

        private void UpdateGraphicsTimer_Tick(object sender, EventArgs e)
        {
            if (!IsDisposed)
            {
                int PrevStartAngle = GetStartAngle(Index - 1);
                int NewStartAngle = GetStartAngle(Index);

                Graphics.DrawArc(ClearPen, GraphicsRectangle, PrevStartAngle - DrawPen.Width - 2, StrideLength + DrawPen.Width + 2);
                Graphics.DrawArc(DrawPen, GraphicsRectangle, NewStartAngle, SweepAngle);

                Index++;
            }
        }

        private int GetStartAngle(int index)
        {
            return index * StrideLength;
        }

        private void LoadingDialog_BackColorChanged(object sender, EventArgs e)
        {
            ClearPen = new Pen(this.BackColor, PenWidth + 4);
        }

        private void LoadingDialog_SizeChanged(object sender, EventArgs e)
        {
            GraphicsRectangle = new Rectangle(PenWidth, PenWidth, this.Width - PenWidth * 2, this.Height - PenWidth * 2);
            Graphics = this.CreateGraphics();
        }
    }
}
