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
    public partial class ChoiceBox : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public static Form pform { get; set; }
        public static int widthloopnumber { get; set; }
        public static int choicewidth { get; set; }
        List<string[]> listslideoptions = new List<string[]>();
        List<Action> listslidefinishaction = new List<Action>();
        public static List<Control> hiddencontrolsList = new List<Control>();
        List<System.Windows.Forms.Timer> listtimers2 = new List<System.Windows.Forms.Timer>();
        List<System.Windows.Forms.Timer> listtimers = new List<System.Windows.Forms.Timer>();
        public static bool choiceselected { get; set; }
        public static Action choiceclickedaction { get; set; }
        public static List<List<object>> choicelist = new List<List<object>>();
        List<Panel> listPanels = new List<Panel>();
        List<Label> listLabels = new List<Label>();
        public ChoiceBox()
        {
            InitializeComponent();
            choiceselected = false;
            widthloopnumber = 0;
        }

        private void ChoiceBox_Load(object sender, EventArgs e)
        {
        }

        private void test()
        {

        }
        private void loadchoices()
        {
            int yMultiplier = 1;
            int yLocation = 0;
            int panelheight = 60;
            foreach (List<object> choice in choicelist)
            {
                //this.Height = (yMultiplier * 60) + 34;
                //this.Top = Screen.FromControl(this).Bounds.Height - this.Height;
                Panel p = new Panel();
                p.Name = "pnl" + choicelist.IndexOf(choice).ToString();
                p.Width = this.Width;
                p.Height = panelheight;
                p.Left = -this.Width + 40;
                p.Click += new EventHandler(choiceclicked);
                p.Top = yLocation;
                p.BackColor = Color.FromName(choice[2] as string);
                listPanels.Add(p);

                Label l = new Label();
                l.Name = "label" + choice[0] as string;
                l.Text = choice[1] as string;
                l.Font = lblSample.Font;
                l.ForeColor = Color.FromName(choice[2] as string);
                l.Left = 50;
                l.TextAlign = ContentAlignment.MiddleLeft;
                l.Height = 60;
                l.Click += new EventHandler(labelchoiceclicked);
                l.Width = 400;
                l.Top = yLocation;
                //l.AutoSize = true;
                listLabels.Add(l);
                this.Controls.Add(p);
                this.Controls.Add(l);
                p.Parent = pnlChoiceList;
                l.Parent = pnlChoiceList;
                p.Show();
                l.Show();
                yMultiplier++;
                yLocation = yLocation + (panelheight + 5);
            }
            pnlChoiceList.Height = yLocation + 12;
            this.Height = pnlChoiceList.Height + pnlChoiceList.Top;
            Rectangle scrn = Screen.FromControl(this).Bounds;
            this.Top = scrn.Height / 2 - (this.Height / 2);
            this.Left = scrn.Width / 2 - (this.Width / 2);
        }


        private void choiceclicked(object sender, EventArgs e)
        {
            foreach (Label l in listLabels)
            {
                l.Hide();
            }
            Panel g = sender as Panel;
            List<object> choicechosen = choicelist[listPanels.IndexOf(sender as Panel)];
            choiceselected = true;
            choiceclickedaction = choicechosen[0] as Action;
            foreach (Panel b in listPanels)
            {
                if (!(g == b))
                {
                    b.Hide();
                }
            }
            g.Parent = this;
            g.Height = this.Height;
            g.Top = 0;
            g.BringToFront();
            slidecontrol(g, true, 0, 10, (int)(this.Width / 100), new string[] { "yes", "10", "10", "20", "20" }, -1, outputandclose);
        }

        private void labelchoiceclicked(object sender, EventArgs e)
        {
            foreach (Label l in listLabels)
            {
                l.Hide();
            }
            Label label = sender as Label;
            List<object> choicechosen = choicelist[listLabels.IndexOf(sender as Label)];
            choiceselected = true;
            choiceclickedaction = choicechosen[0] as Action;
            Panel g = listPanels[listLabels.IndexOf(sender as Label)];
            foreach (Panel b in listPanels)
            {
                if (!(g == b))
                {
                    b.Hide();
                }
            }
            g.Parent = this;
            g.Height = this.Height;
            g.Top = 0;
            g.BringToFront();
            slidecontrol(g, true, 0, 10, (int)(this.Width / 100), new string[] { "yes", "10", "10", "20", "20" }, -1, outputandclose);
        }
        private void outputandclose()
        {
            closeform();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void closeform()
        {
            System.Windows.Forms.Timer ct = new System.Windows.Forms.Timer();
            ct.Interval = 1;
            ct.Tick += new EventHandler(changeformwidth);
            ct.Start();
        }
        private void changeformwidth(object sender, EventArgs e)
        {
            if (widthloopnumber <= this.Width)
            {
                this.Width = this.Width - widthloopnumber;
                widthloopnumber = widthloopnumber + 3;
            }
            else
            {
                this.Close();
            }
        }














        private void slidecontrol(Control c, bool horizontal, int finalpoint, int interval, int delay, string[] coloroptions, int reuseindex, Action a)
        {
            System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
            int totaldistance = 0;
            if (horizontal)
            {
                if (c.Left > finalpoint)
                {
                    totaldistance = c.Left - finalpoint;
                }
                else
                {
                    totaldistance = finalpoint - c.Left;
                }
            }
            if (!horizontal)
            {
                if (c.Top > finalpoint)
                {
                    totaldistance = c.Top - finalpoint;
                }
                else
                {
                    totaldistance = finalpoint - c.Top;
                }
            }
            if (reuseindex >= 0)
            {
                if (listtimers2.Count > reuseindex)
                {
                    t = listtimers2[reuseindex];
                    listslideoptions.RemoveAt(reuseindex);
                    listslideoptions.Insert(reuseindex, new string[] { c.Name, horizontal.ToString(), c.Left.ToString(), c.Top.ToString(), finalpoint.ToString(), totaldistance.ToString(), interval.ToString(), coloroptions[0], coloroptions[1], coloroptions[2], coloroptions[3], coloroptions[4], "1" });
                }
                else
                {
                    t = new System.Windows.Forms.Timer();
                    t.Tag = "timer" + listtimers.Count.ToString();
                    listtimers2.Add(t);
                    listslideoptions.Add(new string[] { c.Name, horizontal.ToString(), c.Left.ToString(), c.Top.ToString(), finalpoint.ToString(), totaldistance.ToString(), interval.ToString(), coloroptions[0], coloroptions[1], coloroptions[2], coloroptions[3], coloroptions[4], "1" });
                }
            }
            if (reuseindex == -1)
            {
                t = new System.Windows.Forms.Timer();
                t.Tag = "timer" + listtimers.Count.ToString();
                listtimers2.Add(t);
                listslideoptions.Add(new string[] { c.Name, horizontal.ToString(), c.Left.ToString(), c.Top.ToString(), finalpoint.ToString(), totaldistance.ToString(), interval.ToString(), coloroptions[0], coloroptions[1], coloroptions[2], coloroptions[3], coloroptions[4], "1" });
            }
            listslidefinishaction.Add(a);
            t.Interval = delay;
            t.Tick += new EventHandler(changepos);
            t.Start();
        }


        private void changepos(object sender, EventArgs e)
        {
            try
            {
                //c.ToString(), horizontal.ToString(), c.Left.ToString(), c.Top.ToString(), finalpoint.ToString(), totaldistance.ToString(), interval.ToString(), coloroptions[0], coloroptions[1], coloroptions[2], coloroptions[3], coloroptions[4], speed  
                Control temppanel;
                System.Windows.Forms.Timer thistimer = sender as System.Windows.Forms.Timer;
                int currentfade = listtimers2.IndexOf(thistimer);
                Control[] d = this.Controls.Find(listslideoptions[currentfade][0], true);
                if (d.Length == 1)
                {
                    temppanel = d[0] as Control;
                }
                else
                {
                    thistimer.Stop();
                    return;
                }
                int speed = int.Parse(listslideoptions[currentfade][12]);
                System.IO.StreamWriter objWriter2;
                objWriter2 = new System.IO.StreamWriter("temp.txt", true);
                objWriter2.WriteLine(listslideoptions[currentfade][12]);
                objWriter2.Close();
                int adder = int.Parse(listslideoptions[currentfade][6]);
                int totaldistance = int.Parse(listslideoptions[currentfade][5]);
                int prevr2 = temppanel.BackColor.R;
                int prevg2 = temppanel.BackColor.G;
                int prevb2 = temppanel.BackColor.B;
                Action a = listslidefinishaction[currentfade];
                bool hor;
                int finalpoint;
                int startpoint;
                finalpoint = int.Parse(listslideoptions[currentfade][4]);
                if (listslideoptions[currentfade][1] == "True")
                {
                    startpoint = int.Parse(listslideoptions[currentfade][2]);
                    //  MessageBox.Show(startpoint.ToString());
                    hor = true;
                }
                else
                {
                    startpoint = int.Parse(listslideoptions[currentfade][3]);
                    hor = false;
                }

                if (!(listslideoptions[currentfade][7] == "none"))
                {
                    int r2 = int.Parse(listslideoptions[currentfade][8]);
                    int g2 = int.Parse(listslideoptions[currentfade][9]);
                    int b2 = int.Parse(listslideoptions[currentfade][10]);
                    int fadespeed = int.Parse(listslideoptions[currentfade][11]);

                    try
                    {
                        if (r2 == prevr2)
                        {
                            if (g2 == prevg2)
                            {
                                if (b2 == prevb2)
                                {
                                    goto end;
                                }
                            }
                        }

                        if (prevb2 < b2)
                        {
                            prevb2 = prevb2 + fadespeed;
                            if (prevb2 >= 255)
                            {
                                prevb2 = 255;
                                b2 = 255;
                            }
                            if ((b2 - prevb2) < fadespeed)
                            {
                                temppanel.Invoke((MethodInvoker)(() =>
                                {
                                    temppanel.BackColor = Color.FromArgb(prevr2, prevg2, b2);
                                }));
                                prevb2 = b2;
                            }
                            temppanel.Invoke((MethodInvoker)(() =>
                            {
                                temppanel.BackColor = Color.FromArgb(prevr2, prevg2, prevb2);
                            }));
                        }
                        if (prevb2 > b2)
                        {
                            prevb2 = prevb2 - fadespeed;
                            if (prevb2 <= 0)
                            {
                                prevb2 = 0;
                                b2 = 0;
                            }
                            if ((prevb2 - b2) < fadespeed)
                            {
                                temppanel.Invoke((MethodInvoker)(() =>
                                {
                                    temppanel.BackColor = Color.FromArgb(prevr2, prevg2, b2);
                                }));
                                prevb2 = b2;
                            }
                            temppanel.Invoke((MethodInvoker)(() =>
                            {
                                temppanel.BackColor = Color.FromArgb(prevr2, prevg2, prevb2);
                            }));
                        }
                        if (prevg2 < g2)
                        {
                            prevg2 = prevg2 + fadespeed;
                            if (prevb2 >= 255)
                            {
                                prevb2 = 255;
                                b2 = 255;
                            }
                            if ((g2 - prevg2) < fadespeed)
                            {
                                temppanel.Invoke((MethodInvoker)(() =>
                                {
                                    temppanel.BackColor = Color.FromArgb(prevr2, g2, prevb2);
                                }));
                                prevg2 = g2;
                            }
                            temppanel.Invoke((MethodInvoker)(() =>
                            {
                                temppanel.BackColor = Color.FromArgb(prevr2, prevg2, prevb2);
                            }));
                        }
                        if (prevg2 > g2)
                        {
                            prevg2 = prevg2 - fadespeed;
                            if (prevb2 <= 0)
                            {
                                prevb2 = 0;
                                b2 = 0;
                            }
                            if ((prevg2 - g2) < fadespeed)
                            {
                                temppanel.Invoke((MethodInvoker)(() =>
                                {
                                    temppanel.BackColor = Color.FromArgb(prevr2, g2, prevb2);
                                }));
                                prevg2 = g2;
                            }
                            temppanel.Invoke((MethodInvoker)(() =>
                            {
                                temppanel.BackColor = Color.FromArgb(prevr2, prevg2, prevb2);
                            }));
                        }
                        if (prevr2 < r2)
                        {
                            prevr2 = prevr2 + fadespeed;
                            if (prevb2 >= 255)
                            {
                                prevb2 = 255;
                                b2 = 255;
                            }
                            if ((r2 - prevr2) < fadespeed)
                            {
                                temppanel.Invoke((MethodInvoker)(() =>
                                {
                                    temppanel.BackColor = Color.FromArgb(r2, prevg2, prevb2);
                                }));
                                prevr2 = r2;
                            }
                            temppanel.Invoke((MethodInvoker)(() =>
                            {
                                temppanel.BackColor = Color.FromArgb(prevr2, prevg2, prevb2);
                            }));
                        }
                        if (prevr2 > r2)
                        {
                            prevr2 = prevr2 - fadespeed;
                            if (prevb2 <= 0)
                            {
                                prevb2 = 0;
                                b2 = 0;
                            }
                            if ((prevr2 - r2) < fadespeed)
                            {
                                temppanel.Invoke((MethodInvoker)(() =>
                                {
                                    temppanel.BackColor = Color.FromArgb(r2, prevg2, prevb2);
                                }));
                                prevr2 = r2;
                            }
                            temppanel.Invoke((MethodInvoker)(() =>
                            {
                                temppanel.BackColor = Color.FromArgb(prevr2, prevg2, prevb2);
                            }));
                        }

                    }
                    catch (Exception ex)
                    {
                        a();
                        thistimer.Stop();
                        return;
                    }
                }
            end:;
                // totaldistance = int.Parse(listslideoptions[currentfade][]);
                // MessageBox.Show(speed.ToString());
                if (hor)
                {
                    if (temppanel.Left != finalpoint)
                    {
                        if (temppanel.Left < finalpoint)
                        {
                            if (speed <= 0)
                            {
                                speed = 1;
                                string[] temp = listslideoptions[currentfade];
                                listslideoptions.RemoveAt(currentfade);
                                temp[12] = speed.ToString();
                                listslideoptions.Insert(currentfade, temp);
                            }
                            if (finalpoint - temppanel.Left < speed)
                            {
                                speed = 1;
                                string[] temp = listslideoptions[currentfade];
                                listslideoptions.RemoveAt(currentfade);
                                temp[12] = speed.ToString();
                                listslideoptions.Insert(currentfade, temp);
                            }
                            temppanel.Invoke((MethodInvoker)(() => { temppanel.Left = temppanel.Left + speed; }));
                            int distance_travelled = temppanel.Left - startpoint;
                            //MessageBox.Show(distance_travelled.ToString());
                            if (distance_travelled < (totaldistance / 2))
                            {
                                speed += adder;
                                string[] temp = listslideoptions[currentfade];
                                listslideoptions.RemoveAt(currentfade);
                                temp[12] = speed.ToString();
                                listslideoptions.Insert(currentfade, temp);
                            }
                            else
                            {
                                speed -= adder;
                                string[] temp = listslideoptions[currentfade];
                                listslideoptions.RemoveAt(currentfade);
                                temp[12] = speed.ToString();
                                listslideoptions.Insert(currentfade, temp);
                            }
                        }

                        if (temppanel.Left > finalpoint)
                        {
                            if (speed <= 0)
                            {
                                speed = 1;
                                string[] temp = listslideoptions[currentfade];
                                listslideoptions.RemoveAt(currentfade);
                                temp[12] = speed.ToString();
                                listslideoptions.Insert(currentfade, temp);
                            }
                            if (temppanel.Left - finalpoint < speed)
                            {
                                speed = 1;
                                string[] temp = listslideoptions[currentfade];
                                listslideoptions.RemoveAt(currentfade);
                                temp[12] = speed.ToString();
                                listslideoptions.Insert(currentfade, temp);
                            }
                            temppanel.Invoke((MethodInvoker)(() => { temppanel.Left = temppanel.Left - speed; }));
                            int distance_travelled = startpoint - temppanel.Left;
                            if (distance_travelled < (totaldistance / 2))
                            {
                                speed += adder;
                                string[] temp = listslideoptions[currentfade];
                                listslideoptions.RemoveAt(currentfade);
                                temp[12] = speed.ToString();
                                listslideoptions.Insert(currentfade, temp);
                            }
                            else
                            {
                                speed -= adder;
                                string[] temp = listslideoptions[currentfade];
                                listslideoptions.RemoveAt(currentfade);
                                temp[12] = speed.ToString();
                                listslideoptions.Insert(currentfade, temp);
                            }
                        }

                    }
                    else
                    {
                        a();
                        thistimer.Stop();
                        return;
                    }
                }
                else
                {
                    string[] temp = listslideoptions[currentfade];
                    listslideoptions.RemoveAt(currentfade);
                    temp[12] = speed.ToString();
                    listslideoptions.Insert(currentfade, temp);
                    if (totaldistance < 0)
                    {
                        totaldistance = temppanel.Top - finalpoint;
                    }
                    if (temppanel.Top != finalpoint)
                    {
                        if (temppanel.Top < finalpoint)
                        {
                            if (speed <= 0)
                            {
                                speed = 1;
                                string[] temps = listslideoptions[currentfade];
                                listslideoptions.RemoveAt(currentfade);
                                temps[12] = speed.ToString();
                                listslideoptions.Insert(currentfade, temps);
                            }
                            if (finalpoint - temppanel.Top < speed)
                            {
                                speed = 1;
                                string[] temps = listslideoptions[currentfade];
                                listslideoptions.RemoveAt(currentfade);
                                temp[12] = speed.ToString();
                                listslideoptions.Insert(currentfade, temp);
                            }
                            temppanel.Invoke((MethodInvoker)(() => { temppanel.Top = temppanel.Top + speed; }));
                            int distance_travelled = startpoint - temppanel.Top;
                            if (distance_travelled < (totaldistance / 2))
                            {
                                speed += adder;
                                string[] temps = listslideoptions[currentfade];
                                listslideoptions.RemoveAt(currentfade);
                                temps[12] = speed.ToString();
                                listslideoptions.Insert(currentfade, temps);
                            }
                            else
                            {
                                speed -= adder;
                                string[] temps = listslideoptions[currentfade];
                                listslideoptions.RemoveAt(currentfade);
                                temps[12] = speed.ToString();
                                listslideoptions.Insert(currentfade, temps);
                            }
                        }

                        if (temppanel.Top > finalpoint)
                        {
                            if (speed <= 0)
                            {
                                speed = 1;
                                string[] temps = listslideoptions[currentfade];
                                listslideoptions.RemoveAt(currentfade);
                                temps[12] = speed.ToString();
                                listslideoptions.Insert(currentfade, temps);
                            }
                            if (temppanel.Top - finalpoint < speed)
                            {
                                speed = 1;
                                string[] temps = listslideoptions[currentfade];
                                listslideoptions.RemoveAt(currentfade);
                                temp[12] = speed.ToString();
                                listslideoptions.Insert(currentfade, temp);
                            }
                            temppanel.Invoke((MethodInvoker)(() => { temppanel.Top = temppanel.Top - speed; }));
                            int distance_travelled = temppanel.Top - startpoint;
                            if (distance_travelled < (totaldistance / 2))
                            {
                                speed += adder;
                                string[] temps = listslideoptions[currentfade];
                                listslideoptions.RemoveAt(currentfade);
                                temps[12] = speed.ToString();
                                listslideoptions.Insert(currentfade, temps);
                            }
                            else
                            {
                                speed -= adder;
                                string[] temps = listslideoptions[currentfade];
                                listslideoptions.RemoveAt(currentfade);
                                temps[12] = speed.ToString();
                                listslideoptions.Insert(currentfade, temps);
                            }
                        }

                    }
                    else
                    {
                        //MessageBox.Show("");
                        a();
                        thistimer.Stop();
                        return;
                    }
                }
            }
            catch
            {
                //thistimer.Stop();
                return;
            }
        }

        private void ChoiceBox_Shown(object sender, EventArgs e)
        {
            this.Width = choicewidth;
            locktoform();
            loadchoices();
        }

        private void locktoform()
        {
        }
        
    }
}
