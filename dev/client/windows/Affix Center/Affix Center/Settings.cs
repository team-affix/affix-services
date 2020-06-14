using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Affix_Center
{
    public partial class Settings : Form
    {
        List<Action> listColorChangeActions = new List<Action>();
        List<string[]> listcoloroptions = new List<string[]>();
        List<System.Windows.Forms.Timer> listtimers2 = new List<System.Windows.Forms.Timer>();
        List<System.Windows.Forms.Timer> listtimers = new List<System.Windows.Forms.Timer>();
        List<string[]> listslideoptions = new List<string[]>();
        List<Action> listslidefinishaction = new List<Action>();
        bool windowmodesliding { get; set; }
        public static TabPage activepage { get; set; }
        public Settings()
        {
            InitializeComponent();
            activepage = tbpBlank;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lblSettingsTitle_Click(object sender, EventArgs e)
        {

        }

        private void Settings_Load(object sender, EventArgs e)
        {
            setcolors();
            if (UserCredentials.signedin)
            {
                lblUsername.Text = UserCredentials.username;
                lblPassword.Text = "Password";
                lblTFA.Text = "TFA";
                lblPhysicalSecurityDESC.Text = "Tokens for: " + UserCredentials.username;
            }
        }

        private void setcolors()
        {
            if(UserCredentials.color1 == "dark")
            {
                this.BackColor = Color.FromArgb(30, 30, 30);
                lblSecurity.ForeColor = Color.LightGray;
                lblCustomization.ForeColor = Color.LightGray;
                lblSettingsDesc.ForeColor = Color.White;
                tbpAffiliationSecurity.BackColor = Color.FromArgb(30, 30, 30);
                tbpSecurity.BackColor = Color.FromArgb(30, 30, 30);
                tbpCustomization.BackColor = Color.FromArgb(30, 30, 30);
                tbpChatSecurity.BackColor = Color.FromArgb(30, 30, 30);
                tbpBlank.BackColor = Color.FromArgb(30, 30, 30);
                lblChangeThemes.ForeColor = Color.LightGray;
                lblChangeWindowMode.ForeColor = Color.LightGray;
                lblCustomization.ForeColor = Color.White;
                lblCustomizationTitle.ForeColor = Color.LightGray;
                lblSecurityAffiliationPreferences.ForeColor = Color.DimGray;
                lblSecurityChatPreferences.ForeColor = Color.DimGray;
                lblSecurityLocalFactorAuthentication.ForeColor = Color.DimGray;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Settings_Scroll(object sender, ScrollEventArgs e)
        {
            MessageBox.Show("");
        }

        private void lblSecurity_MouseEnter(object sender, EventArgs e)
        {
            pnlSecurty.BackgroundImage = Properties.Resources.Lock_Picture_Selected;
            lblSecurity.ForeColor = Color.MediumSpringGreen;
        }

        private void lblSecurity_MouseLeave(object sender, EventArgs e)
        {
            pnlSecurty.BackgroundImage = Properties.Resources.Lock_Picture;
            if (UserCredentials.color1 == "light")
            {
                lblSecurity.ForeColor = Color.FromArgb(24, 23, 22);
            }
            else
            {
                lblSecurity.ForeColor = Color.LightGray;
            }
        }

        private void pnlLeftPage_MouseEnter(object sender, EventArgs e)
        {
            pnlLeftPage.BackgroundImage = Properties.Resources.Left_Page_Selected;
        }

        private void pnlLeftPage_MouseLeave(object sender, EventArgs e)
        {
            pnlLeftPage.BackgroundImage = Properties.Resources.Left_Page;
        }

        private void pnlRightPage_Click(object sender, EventArgs e)
        {

        }

        private void pnlRightPage_MouseEnter(object sender, EventArgs e)
        {
            pnlRightPage.BackgroundImage = Properties.Resources.Right_Page_Selected;
        }

        private void pnlRightPage_MouseLeave(object sender, EventArgs e)
        {
            pnlRightPage.BackgroundImage = Properties.Resources.Right_Page;
        }
        
        private void lblSecurityAffiliationPreferences_MouseEnter(object sender, EventArgs e)
        {
            lblSecurityAffiliationPreferences.ForeColor = Color.MediumSpringGreen;
        }

        private void lblSecurityAffiliationPreferences_MouseLeave(object sender, EventArgs e)
        {
            if(UserCredentials.color1 == "dark")
            {
                lblSecurityAffiliationPreferences.ForeColor = Color.DimGray;
            }
            else
            {
                lblSecurityAffiliationPreferences.ForeColor = Color.FromArgb(24, 23, 22);
            }
        }

        private void lblSecurityChatPreferences_MouseEnter(object sender, EventArgs e)
        {

            lblSecurityChatPreferences.ForeColor = Color.MediumSpringGreen;
        }

        private void lblSecurityChatPreferences_MouseLeave(object sender, EventArgs e)
        {
            if(UserCredentials.color1 == "dark")
            {
                lblSecurityChatPreferences.ForeColor = Color.DimGray;
            }
            else
            {
                lblSecurityChatPreferences.ForeColor = Color.FromArgb(24, 23, 22);
            }
        }

        private void Settings_Shown(object sender, EventArgs e)
        {
            tbcSettings.SelectedTab = tbpBlank;
        }

        private void lblSecurity_Click(object sender, EventArgs e)
        {
            activepage = tbpSecurity;
            tbcSettings.SelectedTab = tbpSecurity;
        }

        private void tbcSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbcSettings.SelectedTab = activepage;
        }

        private void lblSecurityAffiliationPreferences_Click(object sender, EventArgs e)
        {
            activepage = tbpAffiliationSecurity;
            tbcSettings.SelectedTab = tbpAffiliationSecurity;
            if (UserCredentials.verificationKey == null)
            {
                this.Hide();
                var s = new SignIn();
                s.ShowDialog();
                this.Show();
            }
        }
        
        private byte[] EncryptByteArray(byte[] unEncryptedBytes, string key)
        {
            try
            {
                //enc
                byte[] dectext = unEncryptedBytes;
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripDes.CreateEncryptor();
                        byte[] results = transform.TransformFinalBlock(dectext, 0, dectext.Length);
                        return results;
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        private byte[] DecryptByteArray(byte[] EncryptedBytes, string key)
        {
            try
            {
                byte[] enctext = EncryptedBytes;
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripDes.CreateDecryptor();
                        byte[] results = transform.TransformFinalBlock(enctext, 0, enctext.Length);
                        return results;
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        private void lblCustomization_MouseEnter(object sender, EventArgs e)
        {
            pnlCustomization.BackgroundImage = Properties.Resources.CustomizationButtonWhiteClientSelected__1_;
            lblCustomization.ForeColor = Color.MediumSpringGreen;
        }

        private void lblCustomization_MouseLeave(object sender, EventArgs e)
        {
            pnlCustomization.BackgroundImage = Properties.Resources.CustomizationButtonWhiteClient__2_;
            if (UserCredentials.color1 == "light")
            {
                lblCustomization.ForeColor = Color.FromArgb(24, 23, 22);
            }
            else
            {
                lblCustomization.ForeColor = Color.LightGray;
            }
        }

        private void lblCustomization_Click(object sender, EventArgs e)
        {
            activepage = tbpCustomization;
            tbcSettings.SelectedTab = tbpCustomization;
        }


        private void scrCustomization_ValueChanged(object sender, EventArgs e)
        {
            /*
            int interval = (int)(pnlCustomizationScroll.Height / scrCustomization.Maximum);
            pnlCustomizationScroll.Top = -(scrCustomization.Value * interval);
            */
        }

        private void pnlFullscreen_Click(object sender, EventArgs e)
        {
            if (!windowmodesliding)
            {
                windowmodesliding = true;
                slidecontrol(pnlWindowModeSelectionBar, true, 55, 1, 1, new string[] { "none", "0", "250", "154", "10" }, -1, endpanelslide);
                UserCredentials.startform.FormBorderStyle = FormBorderStyle.None;
                UserCredentials.startform.WindowState = FormWindowState.Maximized;
            }
        }

        private void pnlWindowedMode_Click(object sender, EventArgs e)
        {
            if (!windowmodesliding)
            {
                windowmodesliding = true;
                slidecontrol(pnlWindowModeSelectionBar, true, 192, 1, 1, new string[] { "none", "0", "250", "154", "10" }, -1, endpanelslide);
                UserCredentials.startform.FormBorderStyle = FormBorderStyle.Sizable;
                UserCredentials.startform.WindowState = FormWindowState.Normal;
            }
        }

        private void pnlAffixBorder_Click(object sender, EventArgs e)
        {
            if (!windowmodesliding)
            {
                windowmodesliding = true;

                slidecontrol(pnlWindowModeSelectionBar, true, 329, 1, 1, new string[] { "none", "0", "250", "154", "10" }, -1, endpanelslide);
                UserCredentials.startform.FormBorderStyle = FormBorderStyle.None;
                UserCredentials.startform.WindowState = FormWindowState.Normal;
            }
        }
        private void endpanelslide()
        {
            windowmodesliding = false;
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

        private void pnlDarkTheme_Click(object sender, EventArgs e)
        {
            string theme = "dark";
            File.WriteAllBytes("dat\\person\\client\\colors.uri", convertlisttobyte(theme));
            if (UserCredentials.filesEncrypted)
            {
                EncryptFile("dat\\person\\client\\colors.uri", "dat\\person\\client\\colors.uri", UserCredentials.LFA);
            }
            UserCredentials.color1 = theme;
        }

        private void pnlWhiteTheme_Click(object sender, EventArgs e)
        {
            string theme = "light";
            File.WriteAllBytes("dat\\person\\client\\colors.uri", convertlisttobyte(theme));
            if (UserCredentials.filesEncrypted)
            {
                EncryptFile("dat\\person\\client\\colors.uri", "dat\\person\\client\\colors.uri", UserCredentials.LFA);
            }
            UserCredentials.color1 = theme;
        }

        private bool EncryptFile(string inputFile, string outputFile, string key)
        {
            try
            {
                byte[] encBytes = EncryptByteArray(File.ReadAllBytes(inputFile), key);
                File.WriteAllBytes(outputFile, encBytes);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        private byte[] convertlisttobyte(object list)
        {
            var binFormatter = new BinaryFormatter();
            var mStream = new MemoryStream();
            binFormatter.Serialize(mStream, list);

            return mStream.ToArray();
        }

        private object convertbytetolist(byte[] bytelist)
        {
            var mStream = new MemoryStream();
            var binFormatter = new BinaryFormatter();
            // Where 'objectBytes' is your byte array.
            mStream.Write(bytelist, 0, bytelist.Length);
            mStream.Position = 0;
            return binFormatter.Deserialize(mStream) as object;
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            lblSecurityLocalFactorAuthentication.ForeColor = Color.MediumSpringGreen;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            if (UserCredentials.color1 == "dark")
            {
                lblSecurityLocalFactorAuthentication.ForeColor = Color.DimGray;
            }
            else
            {
                lblSecurityLocalFactorAuthentication.ForeColor = Color.FromArgb(24, 23, 22);
            }
        }

        private void lblPhysicalSecurityDESC_Click(object sender, EventArgs e)
        {
            var f = new AddPhysicalToken();
            f.ShowDialog();
        }

        private void lblPhysicalSecurityDESC_MouseEnter(object sender, EventArgs e)
        {
            lblPhysicalSecurity.ForeColor = Color.MediumSpringGreen;
            lblPhysicalSecurityDESC.ForeColor = Color.MediumSpringGreen;
        }

        private void lblPhysicalSecurityDESC_MouseLeave(object sender, EventArgs e)
        {
            if (UserCredentials.color1 == "dark")
            {
                lblPhysicalSecurity.ForeColor = Color.DimGray;
                lblPhysicalSecurityDESC.ForeColor = Color.DimGray;
            }
            else
            {
                lblPhysicalSecurity.ForeColor = Color.DimGray;
                lblPhysicalSecurityDESC.ForeColor = Color.DimGray;
            }
        }
    }
}
