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
        public class Animation
        {
            public Bitmap[] Frames { get; private set; }
            public Animation(Bitmap[] Frames)
            {
                this.Frames = Frames;
            }
        }

        public class GradientPanel : Panel
        {
            public float GradientAngle { get; set; }
            public Color ColorStart { get; set; }
            public Color ColorEnd { get; set; }

            protected override void OnPaint(PaintEventArgs e)
            {
                DoubleBuffered = true;
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
                Graphics g = e.Graphics;
                g.FillRectangle(new SolidBrush(Parent.BackColor), ClientRectangle);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.FillRoundedRectangle(new SolidBrush(this.BackColor), BorderWidth, BorderWidth, this.Width - BorderWidth * 2, this.Height - BorderWidth * 2, Radius);
                g.DrawRoundedRectangle(new Pen(this.ForeColor, BorderWidth), BorderWidth, BorderWidth, this.Width - BorderWidth * 2, this.Height - BorderWidth * 2, Radius);
            }
            protected override void OnResize(EventArgs eventargs)
            {
                base.OnResize(eventargs);
                Invalidate();
            }
        }

        public class RoundedButton : Button
        {
            public int BorderWidth { get; set; }
            public int Radius { get; set; }
            public Color TextColor
            {
                get { return _TextColor; }
                set { _TextColor = value; }
            }
            Color _TextColor = Color.Black;
            public StringFormat StringFormat
            {
                get { return _StringFormat; }
                set { _StringFormat = value; }
            }
            StringFormat _StringFormat = new StringFormat()
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };

            public RoundedButton()
            {
                DoubleBuffered = true;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                g.FillRectangle(new SolidBrush(Parent.BackColor), ClientRectangle);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.FillRoundedRectangle(new SolidBrush(this.BackColor), BorderWidth, BorderWidth, this.Width - BorderWidth * 2, this.Height - BorderWidth * 2, Radius);
                g.DrawRoundedRectangle(new Pen(this.ForeColor, BorderWidth), BorderWidth, BorderWidth, this.Width - BorderWidth * 2, this.Height - BorderWidth * 2, Radius);
                g.DrawString(Text, Font, new SolidBrush(TextColor), ClientRectangle, StringFormat);
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
            public static void DrawGear(this Graphics gr, Brush axle_brush,
                Brush gear_brush, Pen gear_pen, PointF center,
                float radius, float tooth_length, int num_teeth,
                float axle_radius, bool start_with_tooth)
            {
                float dtheta = (float)(Math.PI / num_teeth);
                // dtheta in degrees.
                float dtheta_degrees = (float)(dtheta * 180 / Math.PI);

                const float chamfer = 2;
                float tooth_width = radius * dtheta - chamfer;
                float alpha = tooth_width / (radius + tooth_length);
                float alpha_degrees = (float)(alpha * 180 / Math.PI);
                float phi = (dtheta - alpha) / 2;

                // Set theta for the beginning of the first tooth.
                float theta;
                if (start_with_tooth) theta = dtheta / 2;
                else theta = -dtheta / 2;

                // Make rectangles to represent
                // the gear's inner and outer arcs.
                RectangleF inner_rect = new RectangleF(
                    center.X - radius, center.Y - radius,
                    2 * radius, 2 * radius);
                RectangleF outer_rect = new RectangleF(
                    center.X - radius - tooth_length,
                    center.Y - radius - tooth_length,
                    2 * (radius + tooth_length), 2 * (radius + tooth_length));

                // Make a path representing the gear.
                GraphicsPath path = new GraphicsPath();
                for (int i = 0; i < num_teeth; i++)
                {
                    // Move across the gap between teeth.
                    float degrees = (float)(theta * 180 / Math.PI);
                    path.AddArc(inner_rect, degrees, dtheta_degrees);
                    theta += dtheta;

                    // Move across the tooth's outer edge.
                    degrees = (float)((theta + phi) * 180 / Math.PI);
                    path.AddArc(outer_rect, degrees, alpha_degrees);
                    theta += dtheta;
                }

                path.CloseFigure();

                // Draw the gear.
                gr.FillPath(gear_brush, path);
                gr.DrawPath(gear_pen, path);
                gr.FillEllipse(axle_brush,
                    center.X - axle_radius, center.Y - axle_radius,
                    2 * axle_radius, 2 * axle_radius);
            }
        }
    }
}
