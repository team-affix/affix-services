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
    public partial class UserVerificationInput : Form
    {
        List<Action> listColorChangeActions = new List<Action>();
        List<string[]> listcoloroptions = new List<string[]>();
        List<string[]> listslideoptions = new List<string[]>();
        List<Action> listslidefinishaction = new List<Action>();
        List<System.Windows.Forms.Timer> listtimers2 = new List<System.Windows.Forms.Timer>();
        List<System.Windows.Forms.Timer> listtimers = new List<System.Windows.Forms.Timer>();
        public static bool faderunning { get; set; }
        public static int loadint { get; set; }
        Timer changecolortimer = new Timer();
        public UserVerificationInput()
        {
            InitializeComponent();
            changecolortimer.Interval = 1;
            changecolortimer.Tick += new EventHandler(loadtimer_tick);
            loadint = 0;
        }

        private void txtInput1_TextChanged(object sender, EventArgs e)
        {
            if (!((sender as TextBox).Text == ""))
            {
                pnlVerify.Hide();
                changecolortimer.Stop();
                this.ActiveControl = txtInput2;
            }

        }

        private void txtInput2_TextChanged(object sender, EventArgs e)
        {
            if (!((sender as TextBox).Text == ""))
            {
                pnlVerify.Hide();
                changecolortimer.Stop();
                this.ActiveControl = txtInput3;
            }
        }

        private void txtInput3_TextChanged(object sender, EventArgs e)
        {
            if (!((sender as TextBox).Text == ""))
            {
                pnlVerify.Hide();
                changecolortimer.Stop();
                this.ActiveControl = txtInput4;
            }
        }

        private void txtInput4_TextChanged(object sender, EventArgs e)
        {
            if (!((sender as TextBox).Text == ""))
            {
                pnlVerify.Hide();
                changecolortimer.Stop();
                this.ActiveControl = txtInput5;
            }
        }

        private void txtInput5_TextChanged(object sender, EventArgs e)
        {
            if (!((sender as TextBox).Text == ""))
            {
                pnlVerify.Hide();
                changecolortimer.Stop();
                this.ActiveControl = txtInput6;
            }
        }

        private void txtInput6_TextChanged(object sender, EventArgs e)
        {
            if (!((sender as TextBox).Text == ""))
            {
                pnlVerify.Hide();
                changecolortimer.Stop();
                this.ActiveControl = txtInput7;
            }
        }

        private void txtInput7_TextChanged(object sender, EventArgs e)
        {
            if (!((sender as TextBox).Text == ""))
            {
                pnlVerify.Hide();
                changecolortimer.Stop();
                this.ActiveControl = txtInput8;
            }
        }

        private void txtInput8_TextChanged(object sender, EventArgs e)
        {
            if (!((sender as TextBox).Text == ""))
            {
                changecolortimer.Start();
                pnlVerify.Show();
            }
        }

        private void UserVerificationInput_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtInput1;
            setcolors();
        }

        private void setcolors()
        {
            if(UserCredentials.color1 == "dark")
            {
                this.BackColor = Color.FromArgb(30, 30, 30);
                lblVerifyDesc.ForeColor = Color.White;
                lblVerifyTitle.ForeColor = Color.LightGray;
                txtInput1.ForeColor = Color.LightGray;
                txtInput2.ForeColor = Color.LightGray;
                txtInput3.ForeColor = Color.LightGray;
                txtInput4.ForeColor = Color.LightGray;
                txtInput5.ForeColor = Color.LightGray;
                txtInput6.ForeColor = Color.LightGray;
                txtInput7.ForeColor = Color.LightGray;
                txtInput8.ForeColor = Color.LightGray;
                txtInput1.BackColor = this.BackColor;
                txtInput2.BackColor = this.BackColor;
                txtInput3.BackColor = this.BackColor;
                txtInput4.BackColor = this.BackColor;
                txtInput5.BackColor = this.BackColor;
                txtInput6.BackColor = this.BackColor;
                txtInput7.BackColor = this.BackColor;
                txtInput8.BackColor = this.BackColor;
            }
        }

        private void txtInput3_Enter(object sender, EventArgs e)
        {

        }

        private void txtInput1_Enter_1(object sender, EventArgs e)
        {
            (sender as TextBox).Text = "";
            if (listslideoptions.Count > 0) { listslideoptions[listslideoptions.Count - 1][0] = "sdgfsdfsdf"; }
            int slidex = -666;
            if((sender as TextBox) == txtInput1)
            {
                (sender as TextBox).Text = "";
                pnlVerify.Hide();
                changecolortimer.Stop();
                slidex = (84 * 1) - 672;
            }
            if ((sender as TextBox) == txtInput2)
            {
                (sender as TextBox).Text = "";
                pnlVerify.Hide();
                changecolortimer.Stop();
                slidex = (84 * 2) - 672;
            }
            if ((sender as TextBox) == txtInput3)
            {
                (sender as TextBox).Text = "";
                pnlVerify.Hide();
                changecolortimer.Stop();
                slidex = (84 * 3) - 672;
            }
            if ((sender as TextBox) == txtInput4)
            {
                (sender as TextBox).Text = "";
                pnlVerify.Hide();
                changecolortimer.Stop();
                slidex = (84 * 4) - 672;
            }
            if ((sender as TextBox) == txtInput5)
            {
                (sender as TextBox).Text = "";
                pnlVerify.Hide();
                changecolortimer.Stop();
                slidex = (84 * 5) - 672;
            }
            if ((sender as TextBox) == txtInput6)
            {
                (sender as TextBox).Text = "";
                pnlVerify.Hide();
                changecolortimer.Stop();
                slidex = (84 * 6) - 672;
            }
            if ((sender as TextBox) == txtInput7)
            {
                (sender as TextBox).Text = "";
                pnlVerify.Hide();
                changecolortimer.Stop();
                slidex = (84 * 7) - 672;
            }
            if ((sender as TextBox) == txtInput8)
            {
                (sender as TextBox).Text = "";
                pnlVerify.Hide();
                slidex = (84 * 8) - 672;
            }
            slidecontrol(pnlSelected, true, slidex, 10, 1, new string[] { "none", "", "", "", "" }, -1, null);
        }
        
        long LongRandom(long min, long max, Random rand)
        {
            long result = rand.Next((Int32)(min >> 32), (Int32)(max >> 32));
            result = (result << 32);
            result = result | (long)rand.Next((Int32)min, (Int32)max);
            return result;
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
                    thistimer.Enabled = false;
                    thistimer.Stop();
                    thistimer.Dispose();
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
                        if (a != null) { a(); }
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
                        if (a != null) { a(); }
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
                        if (a != null) { a(); }
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

        private void loadtimer_tick(object sender, EventArgs e)
        {
            if (!faderunning)
            {
                if (loadint == 0)
                {
                    faderunning = true;
                    fadecolor(new string[] { "0", "250", "154", pnlSelected.Name, "10", "true" }, 1, endfade);
                    loadint++;
                    return;
                }
                if (loadint == 1)
                {
                    faderunning = true;
                    fadecolor(new string[] { "240", "240", "240", pnlSelected.Name, "10", "true" }, 1, endfade);
                    loadint = 0;
                    return;
                }
            }
        }

        private void endfade()
        {
            faderunning = false;
            if(changecolortimer.Enabled == false)
            {
                pnlSelected.BackColor = Color.FromArgb(0, 250, 154);
            }
        }




        private void fadecolor(string[] coloroptions, int timeout, Action a)
        {
            listcoloroptions.Add(coloroptions);
            listColorChangeActions.Add(a);
            System.Windows.Forms.Timer cTimer = new System.Windows.Forms.Timer();
            cTimer = new System.Windows.Forms.Timer();
            cTimer.Interval = timeout;
            cTimer.Tick += new EventHandler(changecolor);
            cTimer.Tag = "timer" + listtimers.Count.ToString();
            listtimers.Add(cTimer);
            cTimer.Start();
        }

        private void changecolor(object sender, EventArgs e)
        {
            Control temppanel;
            System.Windows.Forms.Timer thistimer = sender as System.Windows.Forms.Timer;
            int currentfade = listtimers.IndexOf(thistimer);
            Control[] d = this.Controls.Find(listcoloroptions[currentfade][3], true);
            bool backcolor;
            if (listcoloroptions[currentfade][5] == "true")
            {
                backcolor = true;
            }
            else
            {
                backcolor = false;
            }
            if (d.Length == 1)
            {
                temppanel = d[0] as Control;
            }
            else
            {
                thistimer.Stop();
                return;
            }
            Action a = listColorChangeActions[currentfade];
            int r2 = int.Parse(listcoloroptions[currentfade][0]);
            int g2 = int.Parse(listcoloroptions[currentfade][1]);
            int b2 = int.Parse(listcoloroptions[currentfade][2]);
            if (backcolor)
            {
                int prevr2 = temppanel.BackColor.R;
                int prevg2 = temppanel.BackColor.G;
                int prevb2 = temppanel.BackColor.B;

                int fadespeed = int.Parse(listcoloroptions[currentfade][4]);

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
                    goto skipend;
                end:;
                    if (!(a == null))
                    {
                        a();
                    }
                    thistimer.Stop();
                    return;
                skipend:;

                }
                catch (Exception ex)
                {
                    if (!(a == null))
                    {
                        a();
                    }
                    thistimer.Stop();
                    return;
                }
            }
            else
            {
                int prevr2 = temppanel.ForeColor.R;
                int prevg2 = temppanel.ForeColor.G;
                int prevb2 = temppanel.ForeColor.B;

                int fadespeed = int.Parse(listcoloroptions[currentfade][4]);

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
                                temppanel.ForeColor = Color.FromArgb(prevr2, prevg2, b2);
                            }));
                            prevb2 = b2;
                        }
                        temppanel.Invoke((MethodInvoker)(() =>
                        {
                            temppanel.ForeColor = Color.FromArgb(prevr2, prevg2, prevb2);
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
                                temppanel.ForeColor = Color.FromArgb(prevr2, prevg2, b2);
                            }));
                            prevb2 = b2;
                        }
                        temppanel.Invoke((MethodInvoker)(() =>
                        {
                            temppanel.ForeColor = Color.FromArgb(prevr2, prevg2, prevb2);
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
                                temppanel.ForeColor = Color.FromArgb(prevr2, g2, prevb2);
                            }));
                            prevg2 = g2;
                        }
                        temppanel.Invoke((MethodInvoker)(() =>
                        {
                            temppanel.ForeColor = Color.FromArgb(prevr2, prevg2, prevb2);
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
                                temppanel.ForeColor = Color.FromArgb(prevr2, g2, prevb2);
                            }));
                            prevg2 = g2;
                        }
                        temppanel.Invoke((MethodInvoker)(() =>
                        {
                            temppanel.ForeColor = Color.FromArgb(prevr2, prevg2, prevb2);
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
                                temppanel.ForeColor = Color.FromArgb(r2, prevg2, prevb2);
                            }));
                            prevr2 = r2;
                        }
                        temppanel.Invoke((MethodInvoker)(() =>
                        {
                            temppanel.ForeColor = Color.FromArgb(prevr2, prevg2, prevb2);
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
                                temppanel.ForeColor = Color.FromArgb(r2, prevg2, prevb2);
                            }));
                            prevr2 = r2;
                        }
                        temppanel.Invoke((MethodInvoker)(() =>
                        {
                            temppanel.ForeColor = Color.FromArgb(prevr2, prevg2, prevb2);
                        }));
                    }
                    goto skipend;
                end:;
                    if (!(a == null))
                    {
                        a();
                    }
                    thistimer.Stop();
                    return;
                skipend:;

                }
                catch (Exception ex)
                {
                    if (!(a == null))
                    {
                        a();
                    }
                    thistimer.Stop();
                    return;
                }
            }
        }
        
        private void pnlVerify_MouseEnter(object sender, EventArgs e)
        {
            pnlVerify.BackgroundImage = Properties.Resources.VerifySelected;
        }

        private void pnlVerify_MouseLeave(object sender, EventArgs e)
        {
            pnlVerify.BackgroundImage = Properties.Resources.VerifySelected__2_;
        }

        private void txtInput8_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Back)
            {
                if((sender as TextBox).Text == "")
                {
                    this.ActiveControl = txtInput7;
                }
                pnlVerify.Hide();
                changecolortimer.Stop();
            }
        }

        private void txtInput7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                if ((sender as TextBox).Text == "")
                {
                    this.ActiveControl = txtInput6;
                }
                pnlVerify.Hide();
                changecolortimer.Stop();
            }
        }

        private void txtInput6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                if ((sender as TextBox).Text == "")
                {
                    this.ActiveControl = txtInput5;
                }
                pnlVerify.Hide();
                changecolortimer.Stop();
            }
        }

        private void txtInput5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                if ((sender as TextBox).Text == "")
                {
                    this.ActiveControl = txtInput4;
                }
                pnlVerify.Hide();
                changecolortimer.Stop();
            }
        }

        private void txtInput4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                if ((sender as TextBox).Text == "")
                {
                    this.ActiveControl = txtInput3;
                }
                pnlVerify.Hide();
                changecolortimer.Stop();
            }
        }

        private void txtInput3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                if ((sender as TextBox).Text == "")
                {
                    this.ActiveControl = txtInput2;
                }
                pnlVerify.Hide();
                changecolortimer.Stop();
            }
        }

        private void txtInput2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                if ((sender as TextBox).Text == "")
                {
                    this.ActiveControl = txtInput1;
                }
                pnlVerify.Hide();
                changecolortimer.Stop();
            }
        }

        private void pnlVerify_Click(object sender, EventArgs e)
        {
            UserCredentials.confirmationcode = txtInput1.Text + txtInput2.Text + txtInput3.Text + txtInput4.Text + txtInput5.Text + txtInput6.Text + txtInput7.Text + txtInput8.Text;
            changecolortimer.Stop();
            this.Close();
        }
    }
}
