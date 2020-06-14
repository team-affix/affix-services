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
    public partial class LocalEncryption : Form
    {
        List<Action> listColorChangeActions = new List<Action>();
        List<string[]> listcoloroptions = new List<string[]>();
        List<System.Windows.Forms.Timer> listtimers2 = new List<System.Windows.Forms.Timer>();
        List<System.Windows.Forms.Timer> listtimers = new List<System.Windows.Forms.Timer>();
        public static bool faderunning { get; set; }
        public static int loadint { get; set; }
        System.Windows.Forms.Timer changecolortimer = new System.Windows.Forms.Timer();
        bool matching = false;
        public LocalEncryption()
        {
            InitializeComponent();
            changecolortimer.Interval = 1;
            changecolortimer.Tick += new EventHandler(loadtimer_tick);
        }

        private void lblEncrypt_MouseEnter(object sender, EventArgs e)
        {
            pnlEncrypt.BackgroundImage = Properties.Resources.LockdownSelected;
        }

        private void lblEncrypt_MouseLeave(object sender, EventArgs e)
        {
            pnlEncrypt.BackgroundImage = Properties.Resources.Lockdown;
        }

        private void lblEncrypt_Click(object sender, EventArgs e)
        {
            if (!(txt3FA.Text == txt3FAConfirm.Text))
            {
                shownotification(nullvoid, "Password and Confirmation do not match. ", Color.Coral.Name);
                return;
            }
            else
            {
                if (txt3FA.Text.Length > 0)
                {
                    UserCredentials.LFA = txt3FA.Text;
                    Waitform.waitformtitle = "Encrypting";
                    Waitform.waitformdesc = "Encrypting your data; please wait.";
                    new Thread(() => { new Waitform().ShowDialog(); }).Start();
                    encryptdata();
                    Waitform.closeform = true;
                    List<object> recalloptions = getrecall("LFA");
                    setrecall("LFA", new List<object> { UserCredentials.filesEncrypted, recalloptions[1] });
                    this.Close();
                }
            }
        }

        private void encryptdata()
        {
            if(
            EncryptFile("dat\\person\\client\\colors.uri", "dat\\person\\client\\colors.uri", UserCredentials.LFA) == true ==
            EncryptFile("dat\\source\\client\\apps\\installed.uri", "dat\\source\\client\\apps\\installed.uri", UserCredentials.LFA) == true ==
            EncryptFile("dat\\source\\client\\credentials\\credentials.uri", "dat\\source\\client\\credentials\\credentials.uri", UserCredentials.LFA) == true ==
            EncryptFile("dat\\source\\client\\keys\\piK.uri", "dat\\source\\client\\keys\\piK.uri", UserCredentials.LFA) == true ==
            EncryptFile("dat\\source\\client\\keys\\puK.uri", "dat\\source\\client\\keys\\puK.uri", UserCredentials.LFA) == true ==
            EncryptFile("dat\\source\\client\\lfa\\lfa.uri", "dat\\source\\client\\lfa\\lfa.uri", UserCredentials.LFA) == true
            )
            {
                shownotification(nullvoid, "Successfully encrypted files. ", Color.DodgerBlue.Name);
            }
            else
            {
                shownotification(nullvoid, "Unsuccessful encryption. ", Color.Coral.Name);
            }
        }
        private void nullvoid() { }

        private void shownotification(Action action, string description, string colorname)
        {
            string name = action.Method.Name;
            if (Form.ActiveForm == this)
            {
                if (!Notification.notificationshown)
                {
                    Notification.actionList = new List<Action>();
                    Notification.actionList.Clear();
                    Notification.actionList.Add(action);
                    Notification.notificationList = new List<string[]>();
                    Notification.notificationList.Clear();
                    Notification.notificationList.Add(new string[] { name, description, colorname });
                    new Thread(() =>
                    {
                        var g = new Notification();
                        g.ShowDialog();
                    }).Start();
                    System.Threading.Thread.Sleep(50);
                    this.Invoke((MethodInvoker)(() => { this.Activate(); }));
                }
                else
                {
                    try
                    {
                        Notification.actionList.Add(action);
                        Notification.notificationList.Add(new string[] { name, description, colorname });
                    }
                    catch
                    {
                        Notification.actionList = new List<Action>();
                        Notification.actionList.Clear();
                        Notification.actionList.Add(action);
                        Notification.notificationList = new List<string[]>();
                        Notification.notificationList.Clear();
                        Notification.notificationList.Add(new string[] { name, description, colorname });
                    }
                }
            }
            else
            {

                if (!Notification.notificationshown)
                {
                    Notification.actionList = new List<Action>();
                    Notification.actionList.Clear();
                    Notification.actionList.Add(action);
                    Notification.notificationList = new List<string[]>();
                    Notification.notificationList.Clear();
                    Notification.notificationList.Add(new string[] { name, description, colorname });
                    new Thread(() =>
                    {
                        var g = new Notification();
                        g.ShowDialog();
                    }).Start();
                }
                else
                {
                    try
                    {
                        Notification.actionList.Add(action);
                        Notification.notificationList.Add(new string[] { name, description, colorname });
                    }
                    catch
                    {
                        Notification.actionList = new List<Action>();
                        Notification.actionList.Clear();
                        Notification.actionList.Add(action);
                        Notification.notificationList = new List<string[]>();
                        Notification.notificationList.Clear();
                        Notification.notificationList.Add(new string[] { name, description, colorname });
                    }
                }
            }
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

        private byte[] ReadEncryptedFile(string inputFile, string key)
        {
            try
            {
                byte[] encBytes = File.ReadAllBytes(inputFile);
                return DecryptByteArray(encBytes, key);
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                return null;
            }
            catch
            {
                return null;
            }

        }

        private bool DecryptFile(string inputFile, string outputFile, string key)
        {
            try
            {
                byte[] encBytes = File.ReadAllBytes(inputFile);
                byte[] decBytes = DecryptByteArray(encBytes, key);
                File.WriteAllBytes(outputFile, decBytes);
                return true;
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                return false;
            }
            catch
            {
                return false;
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



        private void txt3FA_TextChanged(object sender, EventArgs e)
        {
            if(txt3FA.Text == txt3FAConfirm.Text)
            {
                if (!matching)
                {
                    matching = true;
                    changecolortimer.Start();
                }
            }
            else
            {
                if (matching)
                {
                    matching = false;
                    changecolortimer.Stop();
                }
            }
        }

        private void txt3FAConfirm_TextChanged(object sender, EventArgs e)
        {
            if (txt3FA.Text == txt3FAConfirm.Text)
            {
                if (!matching)
                {
                    matching = true;
                    changecolortimer.Start();
                }
            }
            else
            {
                if (matching)
                {
                    matching = false;
                    changecolortimer.Stop();
                }
            }
        }

        private void LocalEncryption_Load(object sender, EventArgs e)
        {

        }





        private void loadtimer_tick(object sender, EventArgs e)
        {
            if (!faderunning)
            {
                if (loadint == 0)
                {
                    faderunning = true;
                    fadecolor(new string[] { "0", "250", "154", txt3FA.Name, "10", "false" }, 1, endfade);
                    fadecolor(new string[] { "0", "250", "154", txt3FAConfirm.Name, "10", "false" }, 1, endfade);
                    loadint++;
                    return;
                }
                if (loadint == 1)
                {
                    faderunning = true;
                    fadecolor(new string[] { Color.Coral.R.ToString(), Color.Coral.G.ToString(), Color.Coral.B.ToString(), txt3FA.Name, "10", "false" }, 1, endfade);
                    fadecolor(new string[] { Color.Coral.R.ToString(), Color.Coral.G.ToString(), Color.Coral.B.ToString(), txt3FAConfirm.Name, "10", "false" }, 1, endfade);
                    loadint = 0;
                    return;
                }
            }
        }

        private void endfade()
        {
            faderunning = false;
            if (changecolortimer.Enabled == false)
            {
                txt3FA.ForeColor = Color.Coral;
                txt3FAConfirm.ForeColor = Color.Coral;
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

        private void lblDisregard_MouseEnter(object sender, EventArgs e)
        {
            lblDisregard.ForeColor = Color.Coral;
        }

        private void lblDisregard_MouseLeave(object sender, EventArgs e)
        {
            lblDisregard.ForeColor = Color.White;
        }

        private void lblDisregard_Click(object sender, EventArgs e)
        {
            setrecall("LFA", new List<object> { UserCredentials.filesEncrypted, true });
            this.Close();
        }

        private void setrecall(string nameofrecall, List<object> thingstoremember)
        {
            if (UserCredentials.filesEncrypted)
            {
                List<object> recalllist = convertbytetolist(ReadEncryptedFile("dat\\person\\client\\recall.uri", UserCredentials.LFA)) as List<object>;
                foreach(List<object> recall in recalllist)
                {
                    if(recall[0] as string == nameofrecall)
                    {
                        recall[1] = thingstoremember;
                        return;
                    }
                }
                recalllist.Add(new List<object> { nameofrecall, thingstoremember });
                File.WriteAllBytes("dat\\person\\client\\recall.uri", convertlisttobyte(recalllist));
                EncryptFile("dat\\person\\client\\recall.uri", "dat\\person\\client\\recall.uri", UserCredentials.LFA);
            }
            else
            {
                List<object> recalllist = convertbytetolist(File.ReadAllBytes("dat\\person\\client\\recall.uri")) as List<object>;
                foreach (List<object> recall in recalllist)
                {
                    if (recall[0] as string == nameofrecall)
                    {
                        recall[1] = thingstoremember;
                        return;
                    }
                }
                recalllist.Add(new List<object> { nameofrecall, thingstoremember });
                File.WriteAllBytes("dat\\person\\client\\recall.uri", convertlisttobyte(recalllist));
            }
        }

        private List<object> getrecall(string nameofrecall)
        {
            if (UserCredentials.filesEncrypted)
            {
                List<object> recalllist = convertbytetolist(ReadEncryptedFile("dat\\person\\client\\recall.uri", UserCredentials.LFA)) as List<object>;
                foreach (List<object> recall in recalllist)
                {
                    if (recall[0] as string == nameofrecall)
                    {
                        return recall[1] as List<object>;
                    }
                }
                return null;
            }
            else
            {
                List<object> recalllist = convertbytetolist(File.ReadAllBytes("dat\\person\\client\\recall.uri")) as List<object>;
                foreach (List<object> recall in recalllist)
                {
                    if (recall[0] as string == nameofrecall)
                    {
                        return recall[1] as List<object>;
                    }
                }
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
