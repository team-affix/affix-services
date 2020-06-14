using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Affix_Center
{
    namespace CustomControls
    {
        public class GradientPanel : Panel
        {
            public float GradientAngle { get; set; }
            public Color ColorStart { get; set; }
            public Color ColorEnd { get; set; }

            protected override void OnPaint(PaintEventArgs e)
            {
                if(this.ClientRectangle.Width > 0 && this.ClientRectangle.Height > 0)
                {
                    LinearGradientBrush lgb = new LinearGradientBrush(this.ClientRectangle, this.ColorStart, this.ColorEnd, GradientAngle);
                    Graphics g = e.Graphics;
                    g.FillRectangle(lgb, this.ClientRectangle);
                }
                base.OnPaint(e);
            }
        }

        public class RoundedPanel : Panel
        {
            public int BorderWidth { get; set; }
            public int Radius { get; set; }
            protected override void OnPaint(PaintEventArgs e)
            {
                SolidBrush brush = new SolidBrush(this.BackColor);
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.FillRoundedRectangle(brush, BorderWidth, BorderWidth, this.Width - BorderWidth * 2, this.Height - BorderWidth * 2, Radius);
                g.DrawRoundedRectangle(new Pen(this.ForeColor, BorderWidth), BorderWidth, BorderWidth, this.Width - BorderWidth * 2, this.Height - BorderWidth * 2, Radius);
            }
        }

        public class RoundedButton : Button
        {
            private int radius = 20;
            [DefaultValue(20)]
            public int Radius
            {
                get { return radius; }
                set
                {
                    radius = value;
                    this.RecreateRegion();
                }
            }
            private GraphicsPath GetRoundRectagle(Rectangle bounds, int radius)
            {
                GraphicsPath path = new GraphicsPath();
                path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
                path.AddArc(bounds.X + bounds.Width - radius, bounds.Y, radius, radius, 270, 90);
                path.AddArc(bounds.X + bounds.Width - radius, bounds.Y + bounds.Height - radius,
                            radius, radius, 0, 90);
                path.AddArc(bounds.X, bounds.Y + bounds.Height - radius, radius, radius, 90, 90);
                path.CloseAllFigures();
                return path;
            }
            private void RecreateRegion()
            {
                var bounds = ClientRectangle;
                bounds.Width--; bounds.Height--;
                using (var path = GetRoundRectagle(bounds, this.Radius))
                    this.Region = new Region(path);
                this.Invalidate();
            }
            protected override void OnSizeChanged(EventArgs e)
            {
                base.OnSizeChanged(e);
                this.RecreateRegion();
            }

            protected override void OnPaint(PaintEventArgs pevent)
            {
                pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                base.OnPaint(pevent);
            }
        }

        public class RoundedTextBox : TextBox
        {
            private int radius = 20;
            [DefaultValue(20)]
            public int Radius
            {
                get { return radius; }
                set
                {
                    radius = value;
                    this.RecreateRegion();
                }
            }
            private GraphicsPath GetRoundRectagle(Rectangle bounds, int radius)
            {
                GraphicsPath path = new GraphicsPath();
                path.AddArc(bounds.X, bounds.Y, radius, radius, 180, 90);
                path.AddArc(bounds.X + bounds.Width - radius, bounds.Y, radius, radius, 270, 90);
                path.AddArc(bounds.X + bounds.Width - radius, bounds.Y + bounds.Height - radius,
                            radius, radius, 0, 90);
                path.AddArc(bounds.X, bounds.Y + bounds.Height - radius, radius, radius, 90, 90);
                path.CloseAllFigures();
                return path;
            }
            private void RecreateRegion()
            {
                var bounds = ClientRectangle;
                bounds.Width--; bounds.Height--;
                using (var path = GetRoundRectagle(bounds, this.Radius))
                    this.Region = new Region(path);
                this.Invalidate();
            }
            protected override void OnSizeChanged(EventArgs e)
            {
                base.OnSizeChanged(e);
                this.RecreateRegion();
            }

            protected override void OnPaint(PaintEventArgs pevent)
            {
                pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                base.OnPaint(pevent);
            }
        }

        public static class GraphicsExtension
        {
            private static GraphicsPath GenerateRoundedRectangle(
                this Graphics graphics,
                RectangleF rectangle,
                float radius)
            {
                float diameter;
                GraphicsPath path = new GraphicsPath();
                if (radius <= 0.0F)
                {
                    path.AddRectangle(rectangle);
                    path.CloseFigure();
                    return path;
                }
                else
                {
                    if (radius >= (Math.Min(rectangle.Width, rectangle.Height)) / 2.0)
                        return graphics.GenerateCapsule(rectangle);
                    diameter = radius * 2.0F;
                    SizeF sizeF = new SizeF(diameter, diameter);
                    RectangleF arc = new RectangleF(rectangle.Location, sizeF);
                    path.AddArc(arc, 180, 90);
                    arc.X = rectangle.Right - diameter;
                    path.AddArc(arc, 270, 90);
                    arc.Y = rectangle.Bottom - diameter;
                    path.AddArc(arc, 0, 90);
                    arc.X = rectangle.Left;
                    path.AddArc(arc, 90, 90);
                    path.CloseFigure();
                }
                return path;
            }
            private static GraphicsPath GenerateCapsule(
                this Graphics graphics,
                RectangleF baseRect)
            {
                float diameter;
                RectangleF arc;
                GraphicsPath path = new GraphicsPath();
                try
                {
                    if (baseRect.Width > baseRect.Height)
                    {
                        diameter = baseRect.Height;
                        SizeF sizeF = new SizeF(diameter, diameter);
                        arc = new RectangleF(baseRect.Location, sizeF);
                        path.AddArc(arc, 90, 180);
                        arc.X = baseRect.Right - diameter;
                        path.AddArc(arc, 270, 180);
                    }
                    else if (baseRect.Width < baseRect.Height)
                    {
                        diameter = baseRect.Width;
                        SizeF sizeF = new SizeF(diameter, diameter);
                        arc = new RectangleF(baseRect.Location, sizeF);
                        path.AddArc(arc, 180, 180);
                        arc.Y = baseRect.Bottom - diameter;
                        path.AddArc(arc, 0, 180);
                    }
                    else path.AddEllipse(baseRect);
                }
                catch { path.AddEllipse(baseRect); }
                finally { path.CloseFigure(); }
                return path;
            }

            /// <summary>
            /// Draws a rounded rectangle specified by a pair of coordinates, a width, a height and the radius 
            /// for the arcs that make the rounded edges.
            /// </summary>
            /// <param name="brush">System.Drawing.Pen that determines the color, width and style of the rectangle.</param>
            /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to draw.</param>
            /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to draw.</param>
            /// <param name="width">Width of the rectangle to draw.</param>
            /// <param name="height">Height of the rectangle to draw.</param>
            /// <param name="radius">The radius of the arc used for the rounded edges.</param>

            public static void DrawRoundedRectangle(
                this Graphics graphics,
                Pen pen,
                float x,
                float y,
                float width,
                float height,
                float radius)
            {
                RectangleF rectangle = new RectangleF(x, y, width, height);
                GraphicsPath path = graphics.GenerateRoundedRectangle(rectangle, radius);
                SmoothingMode old = graphics.SmoothingMode;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.DrawPath(pen, path);
                graphics.SmoothingMode = old;
            }

            /// <summary>
            /// Draws a rounded rectangle specified by a pair of coordinates, a width, a height and the radius 
            /// for the arcs that make the rounded edges.
            /// </summary>
            /// <param name="brush">System.Drawing.Pen that determines the color, width and style of the rectangle.</param>
            /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to draw.</param>
            /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to draw.</param>
            /// <param name="width">Width of the rectangle to draw.</param>
            /// <param name="height">Height of the rectangle to draw.</param>
            /// <param name="radius">The radius of the arc used for the rounded edges.</param>

            public static void DrawRoundedRectangle(
                this Graphics graphics,
                Pen pen,
                int x,
                int y,
                int width,
                int height,
                int radius)
            {
                graphics.DrawRoundedRectangle(
                    pen,
                    Convert.ToSingle(x),
                    Convert.ToSingle(y),
                    Convert.ToSingle(width),
                    Convert.ToSingle(height),
                    Convert.ToSingle(radius));
            }

            /// <summary>
            /// Fills the interior of a rounded rectangle specified by a pair of coordinates, a width, a height
            /// and the radius for the arcs that make the rounded edges.
            /// </summary>
            /// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
            /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to fill.</param>
            /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to fill.</param>
            /// <param name="width">Width of the rectangle to fill.</param>
            /// <param name="height">Height of the rectangle to fill.</param>
            /// <param name="radius">The radius of the arc used for the rounded edges.</param>

            public static void FillRoundedRectangle(
                this Graphics graphics,
                Brush brush,
                float x,
                float y,
                float width,
                float height,
                float radius)
            {
                RectangleF rectangle = new RectangleF(x, y, width, height);
                GraphicsPath path = graphics.GenerateRoundedRectangle(rectangle, radius);
                SmoothingMode old = graphics.SmoothingMode;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.FillPath(brush, path);
                graphics.SmoothingMode = old;
            }

            /// <summary>
            /// Fills the interior of a rounded rectangle specified by a pair of coordinates, a width, a height
            /// and the radius for the arcs that make the rounded edges.
            /// </summary>
            /// <param name="brush">System.Drawing.Brush that determines the characteristics of the fill.</param>
            /// <param name="x">The x-coordinate of the upper-left corner of the rectangle to fill.</param>
            /// <param name="y">The y-coordinate of the upper-left corner of the rectangle to fill.</param>
            /// <param name="width">Width of the rectangle to fill.</param>
            /// <param name="height">Height of the rectangle to fill.</param>
            /// <param name="radius">The radius of the arc used for the rounded edges.</param>

            public static void FillRoundedRectangle(
                this Graphics graphics,
                Brush brush,
                int x,
                int y,
                int width,
                int height,
                int radius)
            {
                graphics.FillRoundedRectangle(
                    brush,
                    Convert.ToSingle(x),
                    Convert.ToSingle(y),
                    Convert.ToSingle(width),
                    Convert.ToSingle(height),
                    Convert.ToSingle(radius));
            }
        }
    }
}
