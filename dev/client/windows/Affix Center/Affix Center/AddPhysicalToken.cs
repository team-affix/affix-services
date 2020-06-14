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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Affix_Center
{
    public partial class AddPhysicalToken : Form
    {
        List<System.Windows.Forms.Timer> listtimers2 = new List<System.Windows.Forms.Timer>();
        List<System.Windows.Forms.Timer> listtimers = new List<System.Windows.Forms.Timer>();
        List<string[]> listslideoptions = new List<string[]>();
        List<Action> listslidefinishaction = new List<Action>();
        System.Windows.Forms.Timer checktimer = new System.Windows.Forms.Timer();

        DriveInfo[] drives;
        

        List<object> drivelist = new List<object> { };
        List<Control> driveobjlist = new List<Control> { };
        Panel pnldrives = new Panel();


        public AddPhysicalToken()
        {
            InitializeComponent();
            checktimer.Interval = 100;
            checktimer.Tick += new EventHandler(loaddrives);
        }

        private void AddPhysicalToken_Load(object sender, EventArgs e)
        {
            setcolors();
            pnldrives.Size = new Size(pnlDrivesHolder.Width, 0);
            pnldrives.BackColor = Color.Transparent;
            pnldrives.Location = new Point(0, 0);
            pnlDrivesHolder.Controls.Add(pnldrives);
            checktimer.Start();
        }

        private void setcolors()
        {
            if(UserCredentials.color1 == "dark")
            {
                this.BackColor = Color.FromArgb(24, 23, 22);
                pnldrives.BackColor = Color.Transparent;
                pnlDrivesHolder.BackColor = Color.Transparent;
                lblAddPhysicalTokensTitle.ForeColor = Color.DimGray;
                lblAddPhysicalTokensDESC.ForeColor = Color.DimGray;
            }
        }

        private void loaddrives(object sender, EventArgs e)
        {


            if(drives == null)
            {
                drives = DriveInfo.GetDrives();
                //MessageBox.Show("");
                cleardriveobjects();
                foreach (DriveInfo drive in drives)
                {
                    if(File.Exists(drive.Name.Substring(0, 2) + "\\atk\\afx.tok"))
                    {
                        try
                        {
                            if (!UserCredentials.filesEncrypted)
                            {
                                    try
                                    {
                                        List<object> token = (convertbytetolist(File.ReadAllBytes(drive.Name + "\\atk\\afx.tok")) as List<object>);
                                        addtodrivelist(drive.Name, true, true);
                                    }
                                    catch
                                    {
                                        addtodrivelist(drive.Name, true, false);
                                    }
                            }
                            else
                            {
                                    try
                                    {
                                        List<object> token = (convertbytetolist(DecryptByteArray(File.ReadAllBytes(drive.Name + "\\atk\\afx.tok"), UserCredentials.LFA)) as List<object>);
                                        addtodrivelist(drive.Name, true, true);
                                    }
                                    catch
                                    {
                                        addtodrivelist(drive.Name, true, false);
                                    }
                            }
                        }
                        catch
                        {
                            
                        }
                    }
                    else
                    {
                        addtodrivelist(drive.Name, false, false);
                    }
                }
            }



            
            DriveInfo[] newdrives = DriveInfo.GetDrives();
            if (!areequalsequences(newdrives, drives))
            {
                cleardriveobjects();
                foreach (DriveInfo drive in newdrives)
                {
                    if (File.Exists(drive.Name.Substring(0, 2) + "\\atk\\afx.tok"))
                    {
                        try
                        {
                            if (!UserCredentials.filesEncrypted)
                            {
                                try
                                {
                                    List<object> token = (convertbytetolist(File.ReadAllBytes(drive.Name + "\\atk\\afx.tok")) as List<object>);
                                    addtodrivelist(drive.Name, true, true);
                                }
                                catch
                                {
                                    addtodrivelist(drive.Name, true, false);
                                }
                            }
                            else
                            {
                                try
                                {
                                    List<object> token = (convertbytetolist(DecryptByteArray(File.ReadAllBytes(drive.Name + "\\atk\\afx.tok"), UserCredentials.LFA)) as List<object>);
                                    addtodrivelist(drive.Name, true, true);
                                }
                                catch
                                {
                                    addtodrivelist(drive.Name, true, false);
                                }
                            }
                        }
                        catch { }
                    }
                    else
                    {
                        addtodrivelist(drive.Name, false, false);
                    }
                }
                drives = newdrives;
                //MessageBox.Show(newdrives.SequenceEqual(drives).ToString());
            }
        }

        private bool areequalsequences(DriveInfo[] o1, DriveInfo[] o2)
        {
            int index = -1;
            foreach(DriveInfo o in o1)
            {
                index++;
                try
                {
                    if (!(o2[index].Name == o.Name))
                    {
                        //MessageBox.Show(o.Name + " " + o2[index].Name);
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
            index = -1;
            foreach (DriveInfo o in o2)
            {
                index++;
                try
                {
                    if (!(o1[index].Name == o.Name))
                    {
                        //MessageBox.Show(o.Name + " " + o2[index].Name);
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }
        
        private void addtodrivelist(string driveletter, bool hastoken, bool usable)
        {
            int heightforeach = 50;
            Panel pnldrive = new Panel();
            pnldrive.BackColor = Color.Transparent;
            pnldrive.Size = new Size(pnldrives.Width, heightforeach);
            pnldrive.Location = new Point(0, heightforeach * drivelist.Count);

            pnldrives.Controls.Add(pnldrive);

            Label lbldrivename = new Label();
            lbldrivename.Font = new Font("Segoe UI Light", 12);
            lbldrivename.BackColor = Color.Transparent;
            lbldrivename.ForeColor = Color.LightGray;
            lbldrivename.AutoSize = true;
            lbldrivename.Location = new Point(0 , pnldrive.Height / 2 - lbldrivename.Height / 2);
            lbldrivename.Text = driveletter;
            lbldrivename.MouseEnter += new EventHandler(lbldrivename_mouseenter);
            pnldrive.Controls.Add(lbldrivename);

            Panel pnlicon = new Panel();
            pnlicon.BackColor = Color.Transparent;
            pnlicon.Size = new Size(heightforeach - 20, heightforeach - 20);
            if (hastoken)
            {
                if (usable)
                {
                    pnlicon.BackgroundImage = Properties.Resources.RemovePhysicalToken;
                }
                else
                {
                    pnlicon.BackgroundImage = Properties.Resources.UnreadablePhysicalToken;
                }
            }
            else
            {
                pnlicon.BackgroundImage = Properties.Resources.AddPhysicalToken;
            }
            pnlicon.BackgroundImageLayout = ImageLayout.Zoom;
            pnlicon.Location = new Point(pnldrive.Width - heightforeach, pnldrive.Height / 2 - pnlicon.Height / 2);
            pnldrive.Controls.Add(pnlicon);
            pnlicon.Click += new EventHandler(driveclick);

            driveobjlist.Add(pnlicon);
            driveobjlist.Add(pnldrive);
            driveobjlist.Add(lbldrivename);
            drivelist.Add(new List<object> { driveletter });

            pnldrives.Height = pnldrive.Bottom;

        }

        private void driveclick(object sender, EventArgs e)
        {

        }

        private void cleardriveobjects()
        {
            foreach(Control c in driveobjlist)
            {
                c.Dispose();
            }
            drivelist = new List<object> { };
        }


        private void lbldrivename_mouseenter(object sender, EventArgs e)
        {
            stopslide("pnlSelector");
            Panel pnldrive = new Panel();
            slidecontrol(pnlSelector, false, pnlDrivesHolder.Top + (sender as Label).Parent.Top, 1, 1, new string[] { "none", "130", "255", "255", "10" }, -1, nullvoid);
        }

        private void nullvoid() { }

        private void stopslide(string slidename)
        {
            foreach (string[] slideoption in listslideoptions)
            {
                if (slideoption[0] == slidename)
                {
                    slideoption[0] = " ";
                }
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

        
    }
}
