using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Affix_Center
{
    public partial class AffixCenter : Form
    {
        //bool
        public static bool sendingmessage { get; set; }
        public static bool gettingchat { get; set; }
        public static bool connectonstartup { get; set; }
        public static bool faderunning { get; set; }
        public static bool installedcorrectly { get; set; }
        public static bool firsttimestartup { get; set; }
        public static bool pnlOpenopen { get; set; }
        public static bool pnlsliding { get; set; }
        public static bool displayingmessages { get; set; }
        public static bool showsignup { get; set; }
        //graphics
        Graphics drawgraphics { get; set; }
        //int
        int loadcount = 0;
        int numbermessagessending = 0;
        public static int loadint { get; set; }
        //long
        public static long q { get; set; }
        public static long p { get; set; }
        public static long i { get; set; }
        //string
        //byte[]
        public static byte[] creds;
        //list
        List<object> currentchat = new List<object>();
        List<object> prevchat = new List<object>();
        List<object> messageobjectlist = new List<object>();
        List<object> waitingtosend = new List<object>();

        List<object> messagepanellist = new List<object>();
        List<object> chatlist = new List<object>();
        List<object> chatobjectlist = new List<object>();
        List<Action> listColorChangeActions = new List<Action>();
        List<string[]> listcoloroptions = new List<string[]>();
        List<object> installationinfo = new List<object>();
        List<System.Windows.Forms.Timer> listtimers2 = new List<System.Windows.Forms.Timer>();
        List<System.Windows.Forms.Timer> listtimers = new List<System.Windows.Forms.Timer>();
        public static List<Control> hiddencontrolsList = new List<Control>();
        List<string[]> listslideoptions = new List<string[]>();
        List<Action> listslidefinishaction = new List<Action>();
        List<Label> listloadinglabels = new List<Label>();
        List<Panel> listloadingpanels = new List<Panel>();
        List<List<string>> listloading = new List<List<string>>();
        List<List<string>> prevlistloading = new List<List<string>>();
        List<object> Applist = new List<object> { };
        //textbox
        //panel
        Panel chatpanel { get; set; }
        //bounds
        int destinedwidth = 816;
        int destinedheight = 489;
        //tabpage
        public static TabPage tabpage { get; set; }
        //timer
        System.Windows.Forms.Timer refreshchattimer { get; set; }
        System.Windows.Forms.Timer updatechatdimensions { get; set; }
        System.Windows.Forms.Timer notificationclicktimer { get; set; }
        System.Windows.Forms.Timer checkloadtimer { get; set; }
        System.Windows.Forms.Timer loadtimer { get; set; }
        TextBox t = new TextBox();
        //formborderstyle
        FormWindowState prevfws = FormWindowState.Normal;
        //tcpclient

        //socket
        List<object> TotalPersons = new List<object> { };
        

        public AffixCenter()
        {
            InitializeComponent();
            TotalPersons.Add(new List<object> { "psv://teamaffix/testserver", new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp), new IPEndPoint(IPAddress.Parse("1.1.1.1"), 1), false, false });



            updatechatdimensions = new System.Windows.Forms.Timer();
            updatechatdimensions.Interval = 1000;
            updatechatdimensions.Tick += new EventHandler(updatechatdimensions_tick); 
            /*
            UserCredentials.peerusername = "testusername";
            currentchat.Add(new List<object> { "testusername", "this is a very long test send with absolutely no purpose in reading whatsoever other than to get minimal entertainment and satisfaction throughout life. This message is purely as long as it is for the intent to determine the character height.", "15 October 2018, 20:48" });
            currentchat.Add(new List<object> { "testusername", "testing messages", "15 October 2018, 20:48" });
            currentchat.Add(new List<object> { "testusername", "this is a test", "18 October 2018, 20:48" });
            currentchat.Add(new List<object> { "testusername", "HELLO", "18 October 2018, 20:48" });
            */
            refreshchattimer = new System.Windows.Forms.Timer();
            refreshchattimer.Interval = 500;
            refreshchattimer.Tick += new EventHandler(refreshchattimer_tick);
            gettingchat = false;
            sendingmessage = false;
            connectonstartup = true;
            notificationclicktimer = new System.Windows.Forms.Timer();
            notificationclicktimer.Tick += new EventHandler(notificationclicktimer_tick);
            notificationclicktimer.Interval = 100;
            notificationclicktimer.Start();
            loadint = 0;
            faderunning = false;
            loadtimer = new System.Windows.Forms.Timer();
            loadtimer.Tick += new EventHandler(loadtimer_tick);
            loadtimer.Interval = 500;
            checkloadtimer = new System.Windows.Forms.Timer();
            checkloadtimer.Tick += new EventHandler(checkloadtimer_tick);
            checkloadtimer.Interval = 100;
            pnlOpenopen = false;
            firsttimestartup = false;
            pnlsliding = false;
            installedcorrectly = true;
            tabpage = tbpLoading;
            listloading.Add(new List<string> { "Generating connection", "0" });
            /*
            chatlist.Add(new List<object> { "testusername", 1920 });
            chatlist.Add(new List<object> { "testusername2", 11230 });
            chatlist.Add(new List<object> { "testusername3", 38473892 });
            */
        }


        //AffixCenter::

        private void notificationclicktimer_tick(object sender, EventArgs e)
        {
            if (Notification.notificationclickedname != null)
            {
                if (Notification.notificationclickedname != "")
                {
                    Notification.notificationclickedname = "";
                    Notification.notificationclickedaction();
                    Notification.notificationclickedaction = null;
                }
            }
        }
        


        private void AffixCenter_Load(object sender, EventArgs e)
        {
            UserCredentials.startform = this;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            pnlOpen.Height = Screen.FromControl(this).Bounds.Height - 40;
            pnlOpen.Width = Screen.FromControl(this).Bounds.Width;
            pnlOpen.Top = this.Height - 40;
            pnlOpen.Left = 0;
            //File.WriteAllBytes("dat\\source\\client\\apps\\installed.uri", convertlisttobyte(new List<object> { new List<object> { "Morse - Private Messenger", Properties.Resources.Morse_Client_Logo_Fill, "Using this client, your needs for security will be met." + System.Environment.NewLine + "With this highly encrypted direct end-to-end personal intercom service, there is nothing to worry about." } }));
            checkfiles();
            setcolors();
            //string p = randomStringNumeric(10);
            //string q = randomStringNumeric(10);
            //string i = randomStringNumeric(5);
            //gendhpart12(p, q, i);
            /*
            tabpage = tbpChats;
            tbcMain.SelectedTab = tbpChats;
            */
            //testserverCommunication();
        }
        private void setcolors()
        {
            if (UserCredentials.color1 == "dark")
            {
                lblLoadingDesc.ForeColor = Color.White;
                tbpLoading.BackColor = Color.FromArgb(24, 23, 22);
                lblLearnMore.ForeColor = Color.FromArgb(24, 23, 22);
                pnlMain.BackColor = Color.FromArgb(30, 30, 30);
                pnlClosedMenu.BackColor = Color.FromArgb(30, 30, 30);
                pnlOpen.BackColor = Color.FromArgb(30, 30, 30);
                pnlOpenLoading.BackColor = Color.FromArgb(24, 23, 22);
                pnlOpenLoading.BackgroundImage = Properties.Resources.openloadingLight;
                pnlSettings.BackgroundImage = Properties.Resources.btnSettingsWhite;
                lblLoadingTitle.ForeColor = Color.LightGray;
                lblLearnMore.ForeColor = Color.White;

                tbpHome.BackColor = Color.FromArgb(24, 23, 22);
                lblContinueToChats.BackColor = Color.Transparent;
                lblHomeTitle.ForeColor = Color.LightGray;
                lblHomeDesc.ForeColor = Color.White;
            }
            else
            {
                UserCredentials.color1 = "light";
                pnlLearnMore.BackgroundImage = Properties.Resources.btnLearnMore__2_;

            }
        }
        

        private void AffixCenter_Shown(object sender, EventArgs e)
        {
            var f1 = new Lockdown_Account();
            f1.ShowDialog();
            loadtimer.Start();
            if (firsttimestartup)
            {
                shownotification(login, "Log into Affix Services.", "DodgerBlue");
                shownotification(taketour, "Take a tour of Affix Center!", "Yellow");
            }
            if (!installedcorrectly)
            {
                shownotification(showinstallationerrors, "Installation errors recognized: (" + installationinfo.Count.ToString() + ").", "Coral");
            }
            this.ActiveControl = pnlMain;
            if (connectonstartup)
            {
                var f = new Thread(() => generateconnection());
                f.Start();
            }
        }

        
        //AffixServicesLoading:: (LoadingPage)













        private void btnLoadingLearnMore_Click(object sender, EventArgs e)
        {
            this.ActiveControl = pnlMain;
            var g = new LearnMore();
            g.ShowDialog();
        }
        //AffixServices::

        private void login()
        {

        }


        private void signup()
        {

        }

        //ProgramInformation::

        private void taketour()
        {

        }

        //Installation::
        private void checkfiles()
        {
            if (!Directory.Exists("dat\\source\\client\\wav\\"))
            {
                Directory.CreateDirectory("dat\\source\\client\\wav\\");
            }
            if (!Directory.Exists("dat\\source\\client\\lfa"))
            {
                Directory.CreateDirectory("dat\\source\\client\\lfa");
            }
            if (!File.Exists("dat\\source\\client\\lfa\\lfa.uri"))
            {
                File.WriteAllText("dat\\source\\client\\lfa\\lfa.uri", "plaintext");
            }
            if(!(File.ReadAllText("dat\\source\\client\\lfa\\lfa.uri") == "plaintext"))
            {
                this.Hide();
                UserCredentials.filesEncrypted = true;
            retry:;
                var f = new TripleFac();
                f.ShowDialog();
                try
                {
                    if(ReadEncryptedFile("dat\\source\\client\\lfa\\lfa.uri", UserCredentials.LFA) == null)
                    {
                        goto retry;
                    }
                }
                catch
                {
                    goto retry;
                }
            }
            if (!Directory.Exists("dat\\source\\client\\keys\\"))
            {
                Directory.CreateDirectory("dat\\source\\client\\keys\\");
                
            }
            if (!File.Exists("dat\\source\\client\\keys\\puK.uri") || !File.Exists("dat\\source\\client\\keys\\piK.uri"))
            {
                //MessageBox.Show("");
                lblLoadDesc.Invoke((MethodInvoker)(() => { lblLoadDesc.Text = "Removing keys."; }));
                try { File.Delete("dat\\source\\client\\keys\\puK.uri"); } catch { }
                try { File.Delete("dat\\source\\client\\keys\\piK.uri"); } catch { }
                lblLoadDesc.Invoke((MethodInvoker)(() => { lblLoadDesc.Text = "Please wait; generating keys."; }));
                if (!UserCredentials.filesEncrypted)
                {
                    Generatekeys("dat\\source\\client\\keys\\puK.uri", "dat\\source\\client\\keys\\piK.uri");
                }
                else
                {
                    Generatekeys("dat\\source\\client\\keys\\puK.uri", "dat\\source\\client\\keys\\piK.uri");
                    EncryptFile("dat\\source\\client\\keys\\puK.uri", "dat\\source\\client\\keys\\puK.uri", UserCredentials.LFA);
                    EncryptFile("dat\\source\\client\\keys\\piK.uri", "dat\\source\\client\\keys\\piK.uri", UserCredentials.LFA);
                }
            }
            if (!UserCredentials.filesEncrypted)
            {
                UserCredentials.mypublickey = File.ReadAllText("dat\\source\\client\\keys\\puK.uri");
                UserCredentials.myprivatekey = File.ReadAllText("dat\\source\\client\\keys\\piK.uri");
            }
            else
            {
                UserCredentials.mypublickey = Encoding.UTF8.GetString(ReadEncryptedFile("dat\\source\\client\\keys\\puK.uri", UserCredentials.LFA));
                UserCredentials.myprivatekey = Encoding.UTF8.GetString(ReadEncryptedFile("dat\\source\\client\\keys\\piK.uri", UserCredentials.LFA));
                //File.WriteAllText("dat\\temp.txt", UserCredentials.mypublickey + Environment.NewLine + UserCredentials.myprivatekey);
            }
            if (!Directory.Exists("dat\\source\\client\\credentials\\"))
            {
                Directory.CreateDirectory("dat\\source\\client\\credentials\\");
            }
            if (File.Exists("dat\\source\\client\\credentials\\credentials.uri"))
            {
                if (!UserCredentials.filesEncrypted)
                {
                    try
                    {
                        List<object> creds = convertbytetolist(File.ReadAllBytes("dat\\source\\client\\credentials\\credentials.uri")) as List<object>;
                        UserCredentials.credentialsimported = true;

                        UserCredentials.username = creds[0] as string;
                        UserCredentials.verificationKey = creds[1] as string;
                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    try
                    {
                        List<object> creds = convertbytetolist(ReadEncryptedFile("dat\\source\\client\\credentials\\credentials.uri", UserCredentials.LFA)) as List<object>;
                        UserCredentials.credentialsimported = true;

                        UserCredentials.username = creds[0] as string;
                        UserCredentials.verificationKey = creds[1] as string;
                        //MessageBox.Show(UserCredentials.verificationKey);
                        //MessageBox.Show((UserCredentials.credentials[0]).ToString());
                    }
                    catch
                    {

                    }
                }
            }
            
            if (!Directory.Exists("dat\\source\\client\\apps\\"))
            {
                Directory.CreateDirectory("dat\\source\\client\\apps\\");
            }
            if (!UserCredentials.filesEncrypted)
            {
                Applist = convertbytetolist(File.ReadAllBytes("dat\\source\\client\\apps\\installed.uri")) as List<object>;
            }
            else
            {
                Applist = convertbytetolist(ReadEncryptedFile("dat\\source\\client\\apps\\installed.uri", UserCredentials.LFA)) as List<object>;
            }
            if (!File.Exists("dat\\source\\client\\apps\\affixsoftware.uri"))
            {
                installedcorrectly = false;
                Action a = getavailableapps;
                installationinfo.Add(new List<object> { "Affix Software Application information not locally available. (Click to fix)", true, a });
            }
            if (!Directory.Exists("dat\\source\\client\\py\\"))
            {
                installedcorrectly = false;
                installationinfo.Add(new List<string> { "Python scripts uninstalled. (Click to install scripts)'" });
                Directory.CreateDirectory("dat\\source\\client\\py\\");
            }
            if (!Directory.Exists("dat\\source\\server\\"))
            {
                Directory.CreateDirectory("dat\\source\\server\\"); 
            }
            if (!Directory.Exists("dat\\source\\intercom\\"))
            {
                Directory.CreateDirectory("dat\\source\\intercom\\");
            }
            if (!Directory.Exists("dat\\person\\client\\"))
            {
                Directory.CreateDirectory("dat\\person\\client\\");
            }
            if (!Directory.Exists("dat\\person\\server\\"))
            {
                Directory.CreateDirectory("dat\\person\\server\\");
            }

            if (!File.Exists("dat\\person\\client\\colors.uri"))
            {
                string theme = "dark";
                if (!UserCredentials.filesEncrypted)
                {
                    File.WriteAllBytes("dat\\person\\client\\colors.uri", convertlisttobyte(theme));
                }
                else
                {
                    File.WriteAllBytes("dat\\person\\client\\colors.uri", convertlisttobyte(theme));
                    EncryptFile("dat\\person\\client\\colors.uri", "dat\\person\\client\\colors.uri", UserCredentials.LFA);
                }
            }

            if (!File.Exists("dat\\person\\client\\recall.uri"))
            {
                if (UserCredentials.filesEncrypted)
                {
                    File.WriteAllBytes("dat\\person\\client\\recall.uri", convertlisttobyte(new List<object> { }));
                    EncryptFile("dat\\person\\client\\recall.uri", "dat\\person\\client\\recall.uri", UserCredentials.LFA);
                }
                else
                {
                    File.WriteAllBytes("dat\\person\\client\\recall.uri", convertlisttobyte(new List<object> { }));
                }
            }

            if (!UserCredentials.filesEncrypted)
            {
                byte[] theme2 = File.ReadAllBytes("dat\\person\\client\\colors.uri");
                string lc = convertbytetolist(theme2) as string;
                UserCredentials.color1 = lc;
            }
            else
            {
                byte[] theme2 = ReadEncryptedFile("dat\\person\\client\\colors.uri", UserCredentials.LFA);
                string lc = convertbytetolist(theme2) as string;
                UserCredentials.color1 = lc;
            }

            if (!File.Exists("dat\\source\\client\\former.dll"))
            {
                firsttimestartup = true;
                File.WriteAllText("dat\\source\\client\\former.dll", "");
            }
            if (!File.Exists("dat\\source\\client\\wav\\unn.wav"))
            {
                installedcorrectly = false;
                installationinfo.Add(new List<string> { "File not found: 'dat\\source\\client\\wav\\unn.wav'" });
            }
        }


        private void getavailableapps()
        {
        }


        private bool? signin(string username, byte[] creds)
        {
            new Thread(() => {
                var f = new Waitform();
                f.ShowDialog();
            }).Start();
            
            string pubkey = UserCredentials.mypublickey;
            if (UserCredentials.dhkey == null)
            {
                shownotification(nullvoid, "DHC Key not generated; unable to connect. ", Color.Coral.Name);
                Waitform.closeform = true;  
                return null;
            }
            List<object> sendobj = new List<object>() { "signin", username, creds, pubkey };
            byte[] response = null;
            while (response == null)
            {
                response = sendtoserver(EncryptMessage(sendobj), 1000);
            }
            try
            {
                List<object> returnobj = DecryptMessage(response);
                if ((string)(returnobj[0]) == "success")
                {
                    shownotification(nullvoid, "Affix Services response: Welcome, " + username, Color.Cyan.Name);
                    Waitform.closeform = true;
                    return true;
                }
                else
                {
                    shownotification(nullvoid, "Affix Services response: Incorrect credentials.", Color.Coral.Name);
                    Waitform.closeform = true;
                    return false;
                }
            }
            catch
            {
                Waitform.closeform = true;
                return null;
            }


            /*
            List<byte[]> creds = new List<byte[]>();
            creds.Add(Encoding.UTF8.GetBytes(txtUsername.Text));
            creds.Add(EncryptByteArray(Encoding.UTF8.GetBytes(txtPassword.Text), txtTFA.Text));
            byte[] encmessage = EncryptMessage(new List<object> { "signin", creds });
            MessageBox.Show("");
            byte[] response = sendtoserver(encmessage, 3000);
            DecryptMessage(response);
            */
        }



        private bool? signinwithIFA2(string username, List<object> creds)
        {
            string pubkey = UserCredentials.mypublickey;
            if (UserCredentials.dhkey == null)
            {
                shownotification(nullvoid, "DHC Key not generated; unable to connect. ", Color.Coral.Name);
                Waitform.closeform = true;
                return false;
            }
            List<object> sendobj = new List<object>() { "signinIFA2", username, pubkey };
            byte[] response = null;
            while (response == null)
            {
                response = sendtoserver(EncryptMessage(sendobj), 1000);
            }
            try
            {
                List<object> returnobj = DecryptMessage(response);
                if ((string)(returnobj[0]) == "success")
                {
                    try
                    {
                        
                        byte[] encPermPriKey = returnobj[1] as byte[];
                        byte[] encVerificationMessage = returnobj[2] as byte[];
                        byte[] encVerifiedConnectionKey = returnobj[3] as byte[];
                        List<object> tokenlist = returnobj[4] as List<object>;

                        UserCredentials.IFA2details = new List<object> { tokenlist, encPermPriKey, encVerificationMessage, encVerifiedConnectionKey };
                        if (tokenlist.Count == 0)
                        {

                            byte[] tfaremovedEncPermPriKey = DecryptByteArray(encPermPriKey, creds[1] as string);
                            byte[] permPriKeyByte = DecryptByteArray(tfaremovedEncPermPriKey, creds[0] as string);

                            string permPriKey = Encoding.UTF8.GetString(permPriKeyByte);

                            byte[] verificationMessageByte = Decrypt(permPriKey, encVerificationMessage);
                            string verificationMessage = Encoding.UTF8.GetString(verificationMessageByte);

                            byte[] verifiedConnectionKeyByte = DecryptByteArray(encVerifiedConnectionKey, verificationMessage);
                            string verifiedConnectionKey = Encoding.UTF8.GetString(verifiedConnectionKeyByte);

                            UserCredentials.verificationKey = verifiedConnectionKey;
                            shownotification(nullvoid, "Affix Services response: Welcome, " + UserCredentials.username, Color.Cyan.Name);
                            Waitform.closeform = true;
                            return true;
                            //MessageBox.Show(verifiedConnectionKey);
                        }
                        else
                        {
                            var f = new PhysicalTokenRegistration();
                            f.ShowDialog();
                            return true;
                        }

                    }
                    catch
                    {
                        shownotification(nullvoid, "Affix Services response: Incorrect credentials.", Color.Coral.Name);
                        Waitform.closeform = true;
                        return false;
                    }
                }
                else
                {
                    shownotification(nullvoid, "Affix Services response: Failure to verify IFA-2 Token.", Color.Coral.Name);
                    Waitform.closeform = true;
                    return false;
                }
            }
            catch
            {
                Waitform.closeform = true;
                return false;
            }


            /*
            List<byte[]> creds = new List<byte[]>();
            creds.Add(Encoding.UTF8.GetBytes(txtUsername.Text));
            creds.Add(EncryptByteArray(Encoding.UTF8.GetBytes(txtPassword.Text), txtTFA.Text));
            byte[] encmessage = EncryptMessage(new List<object> { "signin", creds });
            MessageBox.Show("");
            byte[] response = sendtoserver(encmessage, 3000);
            DecryptMessage(response);
            */
        }
        
        private bool? checkIFA2(string username)
        {
            string pubkey = UserCredentials.mypublickey;
            if (UserCredentials.dhkey == null)
            {
                shownotification(nullvoid, "DHC Key not generated; unable to connect. ", Color.Coral.Name);
                Waitform.closeform = true;
                return false;
            }
            List<object> sendobj = new List<object>() { "checkIFA2", username, UserCredentials.verificationKey, pubkey };
            byte[] response = null;
            while (response == null)
            {
                response = sendtoserver(EncryptMessage(sendobj), 1000);
            }
            try
            {
                List<object> returnobj = DecryptMessage(response);
                if ((string)(returnobj[0]) == "success")
                {
                    Waitform.closeform = true;
                    return true;
                }
                else
                {
                    shownotification(nullvoid, "Affix Services response: Failure to verify IFA-2 Token.", Color.Coral.Name);
                    Waitform.closeform = true;
                    return false;
                }
            }
            catch
            {
                Waitform.closeform = true;
                return false;
            }


            /*
            List<byte[]> creds = new List<byte[]>();
            creds.Add(Encoding.UTF8.GetBytes(txtUsername.Text));
            creds.Add(EncryptByteArray(Encoding.UTF8.GetBytes(txtPassword.Text), txtTFA.Text));
            byte[] encmessage = EncryptMessage(new List<object> { "signin", creds });
            MessageBox.Show("");
            byte[] response = sendtoserver(encmessage, 3000);
            DecryptMessage(response);
            */
        }







        private void showinstallationerrors()
        {
            DisplayBox.displayinfo = installationinfo;
            DisplayBox.displaytitle = "Installation errors";
            new Thread(() => {
                var f = new DisplayBox();
                f.ShowDialog();
            }).Start();
        }


        //Menu::
        private void panel1_Click(object sender, EventArgs e)
        {
            if (!pnlsliding)
            {
                if (!pnlOpenopen)
                {
                    pnlOpen.Top = 0;
                    pnlOpenopen = true;
                }
                else
                {
                    pnlOpen.Top = this.Height - 40;
                    pnlOpenopen = false;
                }
            }
        }

        private void endpnlOpenSlide()
        {
            pnlsliding = false;
            /*
            t = new TextBox();
            t.Left = 12;
            t.Width = 300;
            t.Font = lblMenuSampleText.Font;
            t.BackColor = pnlOpen.BackColor;
            t.BorderStyle = BorderStyle.None;
            t.Height = 30;
            t.Top = 10;
            this.Controls.Add(t);
            t.Parent = pnlOpen;
            t.KeyDown += new KeyEventHandler(tkeydown);
            t.ForeColor = Color.FromArgb(10, 10, 20);
            this.ActiveControl = t;
            */
            pnlSettings.Show();
            pnlCloudwire.Show();

        }
        private void endpnlOpenCloseSlide()
        {
            pnlsliding = false;
            this.ActiveControl = pnlMain;
            this.Controls.Remove(t);
            t.Dispose();
        }


        private void tkeydown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                if (!pnlsliding)
                {
                    pnlOpen.Top = pnlMain.Top;

                }
            }
        }

        private void resetfocus()
        {
            System.Threading.Thread.Sleep(10);
            this.Invoke((MethodInvoker)(() =>
            {
                this.ActiveControl = pnlMain;
            }));
        }


        private void pnlMain_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.KeyCode == Keys.Tab)
            {
                Thread f = new Thread(() => resetfocus());
                f.Start();
            }

            if (e.KeyCode == Keys.Space)
            {
                if (!pnlOpenopen)
                {
                    pnlOpen.Top = 0;
                    pnlOpenopen = true;
                }
                else
                {
                    pnlOpen.Top = pnlMain.Top;
                    pnlOpenopen = false;
                }
            }
        }

































        //syncs
        //controls

        private void pnlMain_Click(object sender, EventArgs e)
        {
            this.ActiveControl = pnlMain;
        }

        
        private void pnlLoad_Click(object sender, EventArgs e)
        {
        }

        private void pnlLoad_MouseEnter(object sender, EventArgs e)
        {
            pnlOpenLoading.Left = (this.Width - pnlOpenLoading.Width) - 30;
            if (!checkloadtimer.Enabled)
            {
                checkloadtimer.Start();
            }
        }



        private void pnlOpenLoading_MouseLeave(object sender, EventArgs e)
        {
            disposeopenload();
        }



        private void pnlSettings_MouseEnter(object sender, EventArgs e)
        {
            pnlSettings.BackgroundImage = Properties.Resources.btnSettingsHover1;
        }


        private void pnlSettings_MouseLeave(object sender, EventArgs e)
        {
            if (!(UserCredentials.color1 == "dark"))
            {
                pnlSettings.BackgroundImage = Properties.Resources.btnSettings;
            }
            else
            {
                pnlSettings.BackgroundImage = Properties.Resources.btnSettingsWhite;
            }
        }




        //code





        private void checkloadtimer_tick(object sender, EventArgs e)
        {
            //MessageBox.Show("");
            if(true)//listloading != prevlistloading)
            {
                foreach(Label p in listloadinglabels)
                {
                    p.Dispose();
                }
                foreach(Panel pan in listloadingpanels)
                {
                    pan.Dispose();
                }
                listloadinglabels.Clear();
                listloadingpanels.Clear();
                int ylocation = 10;
                int loopthrough = 0;
            restartloop:;
                foreach (List<string> ls in listloading)
                {
                    if(ls[1] == "100")
                    {
                        listloading.RemoveAt(listloading.IndexOf(ls));
                        goto restartloop;
                    }
                }

                prevlistloading = listloading;

                try
                {
                    foreach (List<string> ls in listloading)
                    {
                        ylocation = (loopthrough * 50) + 10;
                        Label l = new Label();
                        l.Font = lblLoadingDesc.Font;
                        l.AutoSize = true;
                        l.Text = ls[0];
                        l.Left = 10;
                        l.Top = ylocation;
                        l.ForeColor = Color.White;
                        l.BackColor = Color.Transparent;
                        Label l1 = new Label();
                        l1.Font = lblPercentageFont.Font;
                        l1.AutoSize = true;
                        l1.Text = ls[1] + "% complete.";
                        l1.Left = 10;
                        l1.Top = l.Top + 25;
                        l1.ForeColor = Color.MediumSpringGreen;
                        l1.BackColor = Color.Transparent;

                        Panel p1 = new Panel();
                        p1.Name = "pnlLoad" + loopthrough.ToString();
                        p1.Width = 395;
                        p1.Height = 2;
                        p1.Top = l1.Top + 18;
                        p1.Left = (-p1.Width);
                        p1.BackColor = Color.MediumSpringGreen;



                        listloadinglabels.Add(l);
                        listloadinglabels.Add(l1);
                        listloadingpanels.Add(p1);

                        this.Controls.Add(l);
                        l.Parent = pnlOpenLoading;
                        this.Controls.Add(l1);
                        l1.Parent = pnlOpenLoading;
                        this.Controls.Add(p1);
                        p1.Parent = pnlOpenLoading;
                        loopthrough++;


                    }
                }
                catch
                {

                }
                foreach(Panel p2 in listloadingpanels)
                {
                    string percentage = listloading[listloadingpanels.IndexOf(p2)][1];
                    //MessageBox.Show(((-pnlOpenLoading.Left) + (int)((decimal.Parse(percentage) / 100) * pnlOpenLoading.Width)).ToString());
                    //slidecontrol(p2, true, ((-p2.Width) + (int)((decimal.Parse(percentage) / 100) * pnlOpenLoading.Width)), 1, 1, new string[] { "none", "", "", "", "" }, -1, null);
                    p2.Left = ((-p2.Width) + (int)((decimal.Parse(percentage) / 100) * pnlOpenLoading.Width));
                }
            }
        }

        private void disposeopenload()
        {
            foreach (Label p in listloadinglabels)
            {
                p.Dispose();
            }
            foreach (Panel pan in listloadingpanels)
            {
                pan.Dispose();
            }
            listloadinglabels.Clear();
            listloadingpanels.Clear();

            pnlOpenLoading.Left = this.Width;
            checkloadtimer.Stop();
            prevlistloading = null;
        }





        private void loadtimer_tick(object sender, EventArgs e)
        {
            if (!faderunning)
            {
                if (listloading.Count > 0)
                {

                
                    if (loadint == 0)
                    {
                        faderunning = true;
                        fadecolor(new string[] { "0", "250", "154", pnlLoad.Name, "10", "true" }, 1, endfade);
                        loadint++;
                        return;
                    }
                    if (loadint == 1)
                    {
                        faderunning = true;
                        if (!(UserCredentials.color1 == "dark"))
                        {
                            fadecolor(new string[] { "240", "240", "240", pnlLoad.Name, "10", "true" }, 1, endfade);
                        }
                        else
                        {
                            fadecolor(new string[] { "30", "30", "30", pnlLoad.Name, "10", "true" }, 1, endfade);
                        }
                        loadint = 0;
                        return;
                    }

                }
                else
                {
                    faderunning = true;
                    if (!(UserCredentials.color1 == "dark"))
                    {
                        fadecolor(new string[] { "240", "240", "240", pnlLoad.Name, "10", "true" }, 1, endfade);
                    }
                    else
                    {
                        fadecolor(new string[] { "30", "30", "30", pnlLoad.Name, "10", "true" }, 1, endfade);
                    }
                    loadint = 0;
                    return;
                }
            }

        }

        private void endfade()
        {
            faderunning = false;
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
                        if(a != null) {a();}
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
                        if(a != null) {a();}
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
                        if(a != null) {a();}
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








        private void hideall()
        {
            foreach (Control c in this.Controls)
            {
                foreach (Control cc in c.Controls)
                {
                    cc.Visible = false;
                }
                c.Visible = false;
            }
        }

        private void AffixCenter_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

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
                    this.Invoke((MethodInvoker)(() => { this.Activate();  }));
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
        private void generateconnection()
        {
            lblLoadDesc.Invoke((MethodInvoker)(() => { lblLoadDesc.Text = "Please wait; checking for keys.";  }));
            lblLoadDesc.Invoke((MethodInvoker)(() => { lblLoadDesc.Text = "Please wait; retreiving keys."; }));
            string pubkey = UserCredentials.mypublickey;
            string prikey = UserCredentials.myprivatekey;

            lblLoadDesc.Invoke((MethodInvoker)(() => { lblLoadDesc.Text = "Please wait; generating DH."; }));
            long dh1 = genDHpart1();
            lblLoadDesc.Invoke((MethodInvoker)(() => { lblLoadDesc.Text = "Please wait; generating connection to Affix Services."; }));
            //string randomkey = randomString(60);
            List<object> sendobject = new List<object>() { "new bind", dh1.ToString(), p.ToString(), q.ToString(), pubkey };
            foreach (List<string> ls in listloading)
            {
                if (ls[0] == "Generating connection")
                {
                    ls[1] = "50";
                }
            }
            int trycount = 0;
        retry:;
            byte[] response = null;
            while(response == null)
            {
                trycount++;
                response = sendtoserver(EncryptMessageWithoutDH(sendobject), 1000);
            }
            lblLoadDesc.Invoke((MethodInvoker)(() => { lblLoadDesc.Text = "Response received."; }));
            foreach (List<string> ls in listloading)
            {
                if (ls[0] == "Generating connection")
                {
                    ls[1] = "75";
                }
            }
            try
            {
                List<object> returnobj = DecryptMessageWithoutDH(response);
                if ((returnobj[0] as string) == "success")
                {
                    string s = returnobj[1] as string;
                    long inversekey = long.Parse(s);
                    lblLoadDesc.Invoke((MethodInvoker)(() => { lblLoadDesc.Text = "Please wait; finalizing DH."; }));
                    long dhkey1 = genDHpart2(inversekey);
                    UserCredentials.dhkey = dhkey1.ToString();
                    UserCredentials.IDKey = returnobj[2] as string;
                    lblLoadDesc.Invoke((MethodInvoker)(() => { lblLoadDesc.Text = "Connection generated in " + trycount.ToString() + " attempts."; }));
                }
                try
                {
                    foreach (List<string> ls in listloading)
                    {
                        if (ls[0] == "Generating connection")
                        {
                            listloading.RemoveAt(listloading.IndexOf(ls));
                        }
                    }
                }
                catch
                {

                }
                System.Threading.Thread.Sleep(1000);
            tabpage = tbpHome;
            tbcMain.Invoke((MethodInvoker)(() => { tbcMain.SelectedTab = tbpHome; }));
            }
            catch
            {
                foreach (List<string> ls in listloading)
                {
                    if (ls[0] == "Generating connection")
                    {
                        ls[1] = "50";
                    }
                }
                //lblLoadDesc.Invoke((MethodInvoker)(() => { lblLoadDesc.Text = "Bad response. Retrying..."; }));
                goto retry;
            }
        }

        private byte[] EncryptMessage(List<object> listobj)
        {
            //string pubKey = File.ReadAllText("dat\\source\\client\\keys\\puK.uri");

            //MessageBox.Show(Encoding.UTF8.GetString(UserCredentials.credentials));
            //EncryptFull==============================================================
            var sendObject = listobj;

            byte[] sendObjectByte = convertlisttobyte(sendObject);

            string randomkey = randomString(40);

            byte[] dhencrypted = EncryptByteArray(sendObjectByte, UserCredentials.dhkey.ToString());

            List<object> IDKeyandDHencrypted = new List<object>() { UserCredentials.IDKey, dhencrypted, UserCredentials.authkey };

            byte[] ByteIDKeyandDHencrypted = convertlisttobyte(IDKeyandDHencrypted);

            byte[] encrypted = EncryptByteArray(ByteIDKeyandDHencrypted, randomkey);

            string pubKey = UserCredentials.serverpublickey;

            byte[] encryptedKey = Encrypt(pubKey, Encoding.UTF8.GetBytes(randomkey));

            List<byte[]> finalSendObject = new List<byte[]> { encryptedKey, encrypted };

            byte[] finalSendObjectByte = convertlisttobyte(finalSendObject);
            return finalSendObjectByte;
        }


        private List<object> DecryptMessage(byte[] finalSendObjectByte)
        {
            string priKey = UserCredentials.myprivatekey;

            List<byte[]> finalSendObject2 = convertbytetolist(finalSendObjectByte) as List<byte[]>;

            byte[] encryptedKey2 = finalSendObject2[0] as byte[];

            byte[] encrypted2 = finalSendObject2[1] as byte[];

            byte[] randomKey = Decrypt(priKey, encryptedKey2);

            string randomKey2 = Encoding.UTF8.GetString(randomKey);

            byte[] ByteIDKeyandDHencrypted = DecryptByteArray(encrypted2, randomKey2);

            List<object> IDKeyandDHencrypted = convertbytetolist(ByteIDKeyandDHencrypted) as List<object>;

            string IDKey = IDKeyandDHencrypted[0] as string;

            byte[] processListByte = IDKeyandDHencrypted[1] as byte[];

            byte[] processListDHByte = DecryptByteArray(processListByte, UserCredentials.dhkey.ToString());

            List<object> processList = convertbytetolist(processListDHByte) as List<object>;
            return processList;
        }


        private byte[] EncryptMessageWithoutDH(List<object> listobj)
        {
            //string pubKey = File.ReadAllText("dat\\source\\client\\keys\\puK.uri");

            //MessageBox.Show(Encoding.UTF8.GetString(UserCredentials.credentials));
            //EncryptFull==============================================================
            var sendObject = listobj;

            byte[] sendObjectByte = convertlisttobyte(sendObject);

            string randomkey = randomString(40);
            
            List<object> IDKeyandDHencrypted = new List<object>() { UserCredentials.IDKey, sendObjectByte, UserCredentials.authkey };

            byte[] ByteIDKeyandDHencrypted = convertlisttobyte(IDKeyandDHencrypted);

            byte[] encrypted = EncryptByteArray(ByteIDKeyandDHencrypted, randomkey);

            string pubKey = UserCredentials.serverpublickey;

            byte[] encryptedKey = Encrypt(pubKey, Encoding.UTF8.GetBytes(randomkey));

            List<byte[]> finalSendObject = new List<byte[]> { encryptedKey, encrypted };

            byte[] finalSendObjectByte = convertlisttobyte(finalSendObject);
            return finalSendObjectByte;
        }



        private List<object> DecryptMessageWithoutDH(byte[] finalSendObjectByte)
        {
            string priKey = UserCredentials.myprivatekey;

            List<byte[]> finalSendObject2 = convertbytetolist(finalSendObjectByte) as List<byte[]>;

            byte[] encryptedKey2 = finalSendObject2[0] as byte[];

            byte[] encrypted2 = finalSendObject2[1] as byte[];

            byte[] randomKey = Decrypt(priKey, encryptedKey2);

            string randomKey2 = Encoding.UTF8.GetString(randomKey);

            byte[] ByteIDKeyandDHencrypted = DecryptByteArray(encrypted2, randomKey2);

            List<object> IDKeyandDHencrypted = convertbytetolist(ByteIDKeyandDHencrypted) as List<object>;

            string IDKey = IDKeyandDHencrypted[0] as string;

            byte[] processListByte = IDKeyandDHencrypted[1] as byte[];

            return convertbytetolist(processListByte) as List<object>;
        }








        static byte[] sendtoserver(byte[] data, int waitretry)
        {
            // System.Threading.Thread.Sleep(4000);
            byte[] data2 = new byte[1024];
            Socket sck;
            Socket newSocket;
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //sck.Close();
        retry2:;
            try
            {
                // if (!finishedloading)
                // {
                //      bool lfloading = LoadingForm.stoploading;
                //      //MessageBox.Show(lfloading.ToString());
                //      if(lfloading)
                //      {
                ////           goto end;
                //       }
                //  }
                sck.SendTimeout = waitretry;
                sck.ReceiveTimeout = waitretry;
                try
                {
                        sck.Connect(new IPEndPoint(IPAddress.Parse("76.105.35.171"), 8090));
                }
                catch(Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                    return null;
                }
                sck.Send(BitConverter.GetBytes(data.Length), 0, 4, 0);
                sck.Send(data);
                // sck.Listen(0);
                //newSocket = sck.Accept();
                newSocket = sck;
                while (true)
                {
                    byte[] sizeBuf = new byte[4];
                    newSocket.Receive(sizeBuf, 0, sizeBuf.Length, 0);
                    int size = BitConverter.ToInt32(sizeBuf, 0);
                    if(size == 0)
                    {
                        return null;
                    }
                    //new Thread(() => { MessageBox.Show(size.ToString()); }).Start();
                    MemoryStream ms = new MemoryStream();
                    while (size > 0)
                    {
                        byte[] buffer;
                        if (size < newSocket.ReceiveBufferSize)
                        {
                            buffer = new byte[size];
                        }
                        else
                        {
                            buffer = new byte[newSocket.ReceiveBufferSize];
                        }
                        int rec = newSocket.Receive(buffer, 0, buffer.Length, 0);
                        //MessageBox.Show(size.ToString());
                        size -= rec;
                        // MessageBox.Show(size.ToString());
                        // MessageBox.Show(rec.ToString());
                        //MessageBox.Show(buffer.Length.ToString());


                        //buffer.Length
                        ms.Write(buffer, 0, rec);
                    }
                    data2 = ms.ToArray();
                    goto end;
                }
            }
            catch { goto retry2; }
        end:;
            return data2;
        }



        static byte[] bindandsend(byte[] data, int waitretry, int porttobind)
        {
            // System.Threading.Thread.Sleep(4000);
            byte[] data2 = new byte[1024];
            Socket sck;
            Socket newSocket;
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint listeningep = new IPEndPoint(IPAddress.Any, porttobind);
            try
            {
                sck.Bind(listeningep);
            }
            catch
            {
                return null;
            }
        //sck.Close();
        retry2:;
            try
            {
                // if (!finishedloading)
                // {
                //      bool lfloading = LoadingForm.stoploading;
                //      //MessageBox.Show(lfloading.ToString());
                //      if(lfloading)
                //      {
                ////           goto end;
                //       }
                //  }
                sck.SendTimeout = waitretry;
                sck.ReceiveTimeout = waitretry;
                try
                {
                    sck.Connect(new IPEndPoint(IPAddress.Parse("67.174.235.214"), 8090));
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                    return null;
                }
                sck.Send(BitConverter.GetBytes(data.Length), 0, 4, 0);
                sck.Send(data);
                // sck.Listen(0);
                //newSocket = sck.Accept();
                newSocket = sck;
                while (true)
                {
                    byte[] sizeBuf = new byte[4];
                    newSocket.Receive(sizeBuf, 0, sizeBuf.Length, 0);
                    int size = BitConverter.ToInt32(sizeBuf, 0);
                    if (size == 0)
                    {
                        return null;
                    }
                    //new Thread(() => { MessageBox.Show(size.ToString()); }).Start();
                    MemoryStream ms = new MemoryStream();
                    while (size > 0)
                    {
                        byte[] buffer;
                        if (size < newSocket.ReceiveBufferSize)
                        {
                            buffer = new byte[size];
                        }
                        else
                        {
                            buffer = new byte[newSocket.ReceiveBufferSize];
                        }
                        int rec = newSocket.Receive(buffer, 0, buffer.Length, 0);
                        //MessageBox.Show(size.ToString());
                        size -= rec;
                        // MessageBox.Show(size.ToString());
                        // MessageBox.Show(rec.ToString());
                        //MessageBox.Show(buffer.Length.ToString());


                        //buffer.Length
                        ms.Write(buffer, 0, rec);
                    }
                    data2 = ms.ToArray();
                    goto end;
                }
            }
            catch { goto retry2; }
        end:;
            return data2;
        }




        private long genDHpart1()
        {
            p = LongRandom(120000, 120000000, new Random());
            q = LongRandom(120000001, 1200000000, new Random());
            i = LongRandom(90000, 900000, new Random());
            long I = (p ^ i) % q;
            return I;
        }

        private long genDHpart2(long inversekey)
        {
            return ((inversekey ^ i) % q);
        }

        long LongRandom(long min, long max, Random rand)
        {
            long result = rand.Next((Int32)(min >> 32), (Int32)(max >> 32));
            result = (result << 32);
            result = result | (long)rand.Next((Int32)min, (Int32)max);
            return result;
        }

        private void testserverCommunication()
        {
            //EncryptFull=============================================================
            var sendObject = new List<object>();
            sendObject.Add(new List<string> { });
            List<string> g = sendObject[0] as List<string>;
            g.Add("this is a test");

            byte[] sendObjectByte = convertlisttobyte(sendObject);

            string randomkey = randomString(40);

            byte[] encrypted = EncryptByteArray(sendObjectByte, randomkey);

            Generatekeys("pubKey.txt", "priKey.txt");
            string pubKey = File.ReadAllText("pubKey.txt");
            string priKey = File.ReadAllText("priKey.txt");

            byte[] encryptedKey = Encrypt(pubKey, Encoding.UTF8.GetBytes(randomkey));

            List<byte[]> finalSendObject = new List<byte[]> { encryptedKey, encrypted };

            byte[] finalSendObjectByte = convertlisttobyte(finalSendObject);

            List<byte[]> finalSendObject2 = convertbytetolist(finalSendObjectByte) as List<byte[]>;

            byte[] encryptedKey2 = finalSendObject2[0] as byte[];

            byte[] encrypted2 = finalSendObject2[1] as byte[];

            string randomKey2 = Encoding.UTF8.GetString(Decrypt(priKey, encryptedKey2));

            byte[] processListByte = DecryptByteArray(encrypted2, randomKey2);

            List<object> processList = convertbytetolist(processListByte) as List<object>;

            foreach (string s in processList[0] as List<string>)
            {
                MessageBox.Show(s);
            }
        }




        private string randomString(int randomlength)
        {
        retry:;
            try
            {
                string randomString = "";
                char[] letters = "qwertyuiopasdfghjklzxcvbnm0123456789!@#$%^&*()=-+_".ToCharArray();
                Random randomcode = new Random();
                for (int i = 0; i < randomlength; i++)
                {
                    randomString += letters[randomcode.Next(0, 49)].ToString();
                }
                return randomString;
            }
            catch
            {
                goto retry;
            }
        }


        private string randomStringNumeric(int randomlength)
        {
        retry:;
            try
            {
                string randomString = "";
                char[] letters = "0123456789".ToCharArray();
                Random randomcode = new Random();
                for (int i = 0; i < randomlength; i++)
                {
                    randomString += letters[randomcode.Next(0, 9)].ToString();
                }
                return randomString;
            }
            catch
            {
                goto retry;
            }
        }


        static void Generatekeys(string pubKeyFileName, string priKeyFileName)
        {
            using (var rsa = new RSACryptoServiceProvider(4096))
            {
                rsa.PersistKeyInCsp = false;
                string pubKeyString = rsa.ToXmlString(false);
                File.WriteAllText(pubKeyFileName, pubKeyString);
                string priKeyString = rsa.ToXmlString(true);
                File.WriteAllText(priKeyFileName, priKeyString);
            }
        }

        static byte[] Encrypt(string publicKey, byte[] input)
        {
            //byte[] input1 = Encoding.UTF8.GetBytes(input);
            byte[] encrypted;
            using (var rsa = new RSACryptoServiceProvider(4096))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(publicKey);
                encrypted = rsa.Encrypt(input, true);
            }
            return encrypted;
        }

        static byte[] Decrypt(string privateKey, byte[] input)
        {
            byte[] decrypted;
            using (var rsa = new RSACryptoServiceProvider(4096))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(privateKey);
                decrypted = rsa.Decrypt(input, true);
            }
            return decrypted;
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

        private void postpone(Control maincontrol)
        {
            foreach (Control c in maincontrol.Controls)
            {
                if (c.Visible)
                {
                    hiddencontrolsList.Add(c);
                }
                foreach (Control cc in c.Controls)
                {
                    if (cc.Visible)
                    {
                        hiddencontrolsList.Add(c);
                    }
                    cc.Visible = false;
                }
                c.Visible = false;
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

        private void resume()
        {
            foreach (Control c in hiddencontrolsList)
            {
                c.Visible = false;
            }
            hiddencontrolsList.Clear();
        }

        private void AffixCenter_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
        }

        private void tbcMain_Enter(object sender, EventArgs e)
        {
        }

        private void tbcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbcMain.SelectedTab = tabpage;
            this.ActiveControl = pnlMain;
            if(tabpage == tbpHome)
            {
                if (File.Exists("dat\\source\\client\\credentials\\credentials.uri"))
                {
                    lblContinueToChats.Text = "Continue";
                    pnlLogOut.Show();
                }
            }
            if (tabpage == tbpApplications)
            {
                changechatscolor();
                loadapps();
            }
            if (tabpage == tbpChat)
            {
                refreshchattimer.Start();
            }
            if (tabpage == tbpHolePunch)
            {
                new Thread(() => holepunch()).Start();
            }
        }
        


        private void loadapps()
        {
            if (!File.Exists("dat\\source\\client\\apps\\installed.uri"))
            {
                File.WriteAllBytes("dat\\source\\client\\apps\\installed.uri", convertlisttobyte(new List<object> { }));
            }

            int bottom = 0;
            foreach (List<object> app in Applist)
            {
                Panel apppanel = new Panel();
                apppanel.BackColor = Color.Transparent;
                apppanel.Location = new Point(0, bottom + 20);
                this.Controls.Add(apppanel);
                apppanel.Parent = pnlApplications;
                Panel appimage = new Panel();
                appimage.BackgroundImageLayout = ImageLayout.Zoom;
                appimage.Size = new Size(150, 150);
                appimage.Location = new Point(0, 0);
                appimage.BackgroundImage = app[1] as Image;
                appimage.BackColor = Color.Transparent;
                this.Controls.Add(appimage);
                appimage.Parent = apppanel;
                Label apptitle = new Label();
                apptitle.AutoSize = true;
                apptitle.Text = app[0] as string;
                apptitle.Location = new Point(appimage.Right, 0);
                if (UserCredentials.color1 == "dark")
                {
                    apptitle.ForeColor = Color.LightGray;
                }
                else
                {
                    apptitle.ForeColor = Color.DimGray;
                }
                apptitle.Font = new Font("Roboto", 20);
                apptitle.BackColor = Color.Transparent;
                this.Controls.Add(apptitle);
                apptitle.Parent = apppanel;
                Label appdesc = new Label();
                appdesc.AutoSize = true;
                appdesc.BackColor = Color.Transparent;
                appdesc.Text = app[2] as string;
                if(UserCredentials.color1 == "dark")
                {
                    appdesc.ForeColor = Color.White;
                }
                else
                {
                    appdesc.ForeColor = Color.Black;
                }
                appdesc.Location = new Point(apptitle.Left, apptitle.Bottom);
                appdesc.Font = new Font("Segoe UI", 14);
                this.Controls.Add(appdesc);
                appdesc.Parent = apppanel;


                if(apptitle.Right > appdesc.Right)
                {
                    apppanel.Width = apptitle.Right;
                }
                if (appdesc.Right > apptitle.Right)
                {
                    apppanel.Width = appdesc.Right;
                }
                if(appimage.Bottom > appdesc.Bottom)
                {
                    apppanel.Height = appimage.Bottom + 60;
                }
                if (appdesc.Bottom > appimage.Bottom)
                {
                    apppanel.Height = appdesc.Bottom + 60;
                }
                Button appinstall = new Button();
                appinstall.FlatStyle = FlatStyle.Flat;
                if (UserCredentials.color1 == "dark")
                {
                    appinstall.BackColor = Color.FromArgb(255, 255, 255);
                }
                else
                {
                    appinstall.BackColor = Color.FromArgb(195, 195, 195);
                }
                appinstall.ForeColor = tbpApplications.BackColor;
                appinstall.Size = new Size(200, 40);
                appinstall.Location = new Point(apppanel.Width - appinstall.Width, apppanel.Height - appinstall.Height);
                appinstall.Font = new Font("Roboto", 12);
                appinstall.Text = "Install";
                this.Controls.Add(appinstall);
                appinstall.Parent = apppanel;
                apppanel.Left = (pnlApplications.Width / 2) - (apppanel.Width / 2);
                bottom = apppanel.Bottom;
            }
        }

        private void changechatscolor()
        {
            if(UserCredentials.color1 == "dark")
            {
                lblApplications.ForeColor = Color.LightGray;
                tbpApplications.BackColor = Color.FromArgb(24, 23, 22);
            }
            else
            {

            }
        }





        private void drawanimation(object sender, EventArgs e)
        {
        }
        private Matrix RotateAroundPoint(float angle, Point center)
        {
            // Translate the point to the origin.
            Matrix result = new Matrix();
            result.RotateAt(angle, center);
            return result;
        }


        private void pnlSettings_Click(object sender, EventArgs e)
        { 
            var f = new Settings();
            f.ShowDialog();
        }

        private void tbpLoading_Click(object sender, EventArgs e)
        {

        }

        private void lblContinueToChats_MouseEnter(object sender, EventArgs e)
        {
            pnlContinueToChats.BackgroundImage = Properties.Resources.Continue_to_chats__1_;
        }

        private void lblContinueToChats_MouseLeave(object sender, EventArgs e)
        {
            pnlContinueToChats.BackgroundImage = Properties.Resources.Continue_to_chats;
        }

        private void lblContinueToChats_Click(object sender, EventArgs e)
        {
            /*
            if (!(UserCredentials.credentials == null))
            {
                if (!UserCredentials.signedin)
                {
                    if(signin(UserCredentials.username, UserCredentials.credentials) ?? default(bool) == true)
                    {
                        UserCredentials.signedin = true;
                        tabpage = tbpApplications;
                        tbcMain.SelectedTab = tbpApplications;
                    }
                }
                else
                {
                    tabpage = tbpApplications;
                    tbcMain.SelectedTab = tbpApplications;
                }
            }
            else
            {
                var d = new SignIn();
                d.ShowDialog();
                if(UserCredentials.credentials == null && showsignup)
                {
                    var d1 = new SignUp();
                    d1.ShowDialog();
                    showsignup = false;
                }
            }
            */

            if ((UserCredentials.credentialsimported))
            {
                if (!UserCredentials.signedin)
                {
                    new Thread(() =>
                    {
                        var f = new Waitform();
                        f.ShowDialog();
                    }).Start();

                    if (checkIFA2(UserCredentials.username) ?? default(bool) == true)
                    {
                        UserCredentials.signedin = true;
                        tabpage = tbpApplications;
                        tbcMain.SelectedTab = tbpApplications;
                        List<object> lfagr = getrecall("LFA");
                        //MessageBox.Show((lfagr[1]).ToString());
                        if ((lfagr == null || (lfagr[1] as bool? == false)) && !(lfagr[0] as bool? == false))
                        {
                            new LocalEncryption().ShowDialog();
                        }
                    }
                }
                else
                {
                    tabpage = tbpApplications;
                    tbcMain.SelectedTab = tbpApplications;
                }
            }
            else
            {
                var d = new SignIn();
                d.ShowDialog();
                if (UserCredentials.verificationKey == null && showsignup)
                {
                    var d1 = new SignUp();
                    d1.ShowDialog();
                    showsignup = false;
                }
                if (UserCredentials.signedin)
                {
                    pnlLogOut.Show();
                    lblContinueToChats.Text = "Continue";

                    /*
                    string ranstring = randomString(10);
                    List<object> keys = Generatekeyswithoutwrite();
                    File.WriteAllBytes("E:\\atk\\afx.tok", EncryptByteArray(convertlisttobyte(new List<object> { ranstring, keys[0] as string, keys[1] as string }), UserCredentials.LFA));
                    addtoken(new List<object> { new List<object> { ranstring, keys[0] as string } });
                    */
                    

                }
            }
        }


        private void addtoken(List<object> tokenlist)
        {
            string pubkey = UserCredentials.mypublickey;
            if (UserCredentials.dhkey == null)
            {
                shownotification(nullvoid, "DHC Key not generated; unable to connect. ", Color.Coral.Name);
                Waitform.closeform = true;
                return;
            }
            List<object> sendobj = new List<object>() { "addtoken", UserCredentials.username, UserCredentials.verificationKey, tokenlist, UserCredentials.mypublickey };
            byte[] response = null;
            while (response == null)
            {
                MessageBox.Show("starting");
                response = sendtoserver(EncryptMessage(sendobj), 1000);
            }
            try
            {
                List<object> returnobj = DecryptMessage(response);
                if ((string)(returnobj[0]) == "success")
                {
                    MessageBox.Show("success");
                }
                else
                {
                    shownotification(nullvoid, "Affix Services response: Failure to verify IFA-2 Token.", Color.Coral.Name);
                    Waitform.closeform = true;
                }
            }
            catch
            {
                Waitform.closeform = true;
            }
        }


        private void setrecall(string nameofrecall, List<object> thingstoremember)
        {
            if (UserCredentials.filesEncrypted)
            {
                List<object> recalllist = convertbytetolist(ReadEncryptedFile("dat\\person\\client\\recall.uri", UserCredentials.LFA)) as List<object>;
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

        //Chats
        private void loadchats()
        {
            
            listloading.Add(new List<string> { "Loading chats", "25" });
            string pubkey = File.ReadAllText("dat\\source\\client\\keys\\puK.uri");
            List<object> sendobj = new List<object>() { "getuserchats", UserCredentials.username, /*UserCredentials.credentials*/ pubkey };
            foreach (List<string> ls in listloading)
            {
                if (ls[0] == "Loading chats")
                {
                    ls[1] = "50";
                }
            }

            byte[] response = null;
            while (response == null) 
            {
                response = sendtoserver(EncryptMessage(sendobj), 10000);
            }
            foreach (List<string> ls in listloading)
            {
                if (ls[0] == "Loading chats")
                {
                    ls[1] = "75";
                }
            }
            
            try
            {
                List<object> returnobj = DecryptMessage(response);
                if ((string)(returnobj[0]) == "success")
                {
                    foreach (List<string> ls in listloading)
                    {
                        if (ls[0] == "Loading chats")
                        {
                            listloading.RemoveAt(listloading.IndexOf(ls));
                            goto continue1;
                        }
                    }
                continue1:;
                    chatlist = returnobj[1] as List<object>;
                
                    foreach (List<Control> lc in chatobjectlist)
                    {
                        foreach (Control c in lc)
                    {
                        c.Invoke((MethodInvoker)(() => { c.Dispose(); }));
                        }
                    }
                    chatobjectlist.Clear();
                    int multiplier = 40;
                    int yloc = 0;
                    foreach (List<object> lo in chatlist)
                    {
                        Panel pnlSeparator = new Panel();
                        pnlSeparator.Height = 1;
                        pnlSeparator.Width = 600;
                        if(UserCredentials.color1 == "dark")
                        {
                            pnlSeparator.BackColor = Color.FromArgb(255, 255, 255);
                        }
                        else
                        {
                            pnlSeparator.BackColor = Color.FromArgb(24, 23, 22);
                        }
                        pnlSeparator.Left = (int)(this.Width / 2) - (pnlSeparator.Width / 2);
                        pnlSeparator.Top = (this.Height / 2) + ((chatlist.IndexOf(lo)) * multiplier);

                        this.Invoke((MethodInvoker)(() => { this.Controls.Add(pnlSeparator); }));
                        pnlSeparator.Invoke((MethodInvoker)(() => { pnlSeparator.Parent = tbpApplications; }));

                        Label lblPeerUsername = new Label();
                        lblPeerUsername.Height = 39;
                        lblPeerUsername.TextAlign = ContentAlignment.MiddleCenter;
                        lblPeerUsername.Width = 300;
                        lblPeerUsername.AutoSize = false;
                        lblPeerUsername.Font = lblContinueToChats.Font;
                        if (UserCredentials.color1 == "dark")
                        {
                            lblPeerUsername.ForeColor= Color.FromArgb(255, 255, 255);
                        }
                        else
                        {
                            lblPeerUsername.ForeColor = Color.FromArgb(24, 23, 22);
                        }
                        lblPeerUsername.Top = pnlSeparator.Top - 40;
                        lblPeerUsername.Text = (string)lo[0];
                        lblPeerUsername.MouseEnter += new EventHandler(lblPeerUsername_MouseEnter);
                        lblPeerUsername.MouseLeave += new EventHandler(lblPeerUsername_MouseLeave);
                        lblPeerUsername.Click += new EventHandler(lblPeerUsername_Click);


                        Label lblPeerNumberMessages = new Label();
                        lblPeerNumberMessages.Height = 39;
                        lblPeerNumberMessages.Width = 300;
                        lblPeerNumberMessages.TextAlign = ContentAlignment.MiddleCenter;
                        lblPeerNumberMessages.AutoSize = false;
                        lblPeerNumberMessages.Font = lblContinueToChats.Font;
                        if (UserCredentials.color1 == "dark")
                        {
                            lblPeerNumberMessages.ForeColor = Color.FromArgb(255, 255, 255);
                        }
                        else
                        {
                            lblPeerNumberMessages.ForeColor = Color.FromArgb(24, 23, 22);
                        }
                        lblPeerNumberMessages.Top = pnlSeparator.Top - 40;
                        lblPeerNumberMessages.Text = (int)lo[1] + " total messages";

                        lblPeerUsername.Left = (this.Width / 2) - (lblPeerUsername.Width);
                        lblPeerNumberMessages.Left = this.Width / 2;


                        this.Invoke((MethodInvoker)(() => { this.Controls.Add(lblPeerNumberMessages); }));
                        lblPeerNumberMessages.Invoke((MethodInvoker)(() => { lblPeerNumberMessages.Parent = tbpApplications; }));

                        this.Invoke((MethodInvoker)(() => { this.Controls.Add(lblPeerUsername); }));
                        lblPeerUsername.Invoke((MethodInvoker)(() => { lblPeerUsername.Parent = tbpApplications; }));
                        chatobjectlist.Add(new List<Control> { pnlSeparator, lblPeerUsername, lblPeerNumberMessages });

                        lblPeerNumberMessages.Anchor = AnchorStyles.None;
                        lblPeerUsername.Anchor = AnchorStyles.None;
                        pnlSeparator.Anchor = AnchorStyles.None;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void lblPeerUsername_MouseEnter(object sender, EventArgs e)
        {
            int thissetofcontrols = -1;
            foreach(List<Control> lc in chatobjectlist)
            {
                if (lc.IndexOf((sender as Label)) != -1)
                {
                    thissetofcontrols = chatobjectlist.IndexOf(lc);
                }
            }
            (chatobjectlist[thissetofcontrols] as List<Control>)[0].BackColor = Color.MediumSpringGreen;
            (chatobjectlist[thissetofcontrols] as List<Control>)[1].ForeColor = Color.MediumSpringGreen;
            (chatobjectlist[thissetofcontrols] as List<Control>)[2].ForeColor = Color.MediumSpringGreen;
        }
        

        private void lblPeerUsername_MouseLeave(object sender, EventArgs e)
        {
            int thissetofcontrols = -1;
            foreach (List<Control> lc in chatobjectlist)
            {
                if (lc.IndexOf((sender as Label)) != -1)
                {
                    thissetofcontrols = chatobjectlist.IndexOf(lc);
                }
            }
            if(UserCredentials.color1 == "dark")
            {
                (chatobjectlist[thissetofcontrols] as List<Control>)[0].BackColor = Color.FromArgb(255, 255, 255);
                (chatobjectlist[thissetofcontrols] as List<Control>)[1].ForeColor = Color.FromArgb(255, 255, 255);
                (chatobjectlist[thissetofcontrols] as List<Control>)[2].ForeColor = Color.FromArgb(255, 255, 255);
            }
            else
            {
                (chatobjectlist[thissetofcontrols] as List<Control>)[0].BackColor = Color.FromArgb(24, 23, 22);
                (chatobjectlist[thissetofcontrols] as List<Control>)[1].ForeColor = Color.FromArgb(24, 23, 22);
                (chatobjectlist[thissetofcontrols] as List<Control>)[2].ForeColor = Color.FromArgb(24, 23, 22);
            }

        }
        private void lblPeerUsername_Click(object sender, EventArgs e)
        {
            Label l = sender as Label;
            UserCredentials.peerusername = l.Text;
            tabpage = tbpChat;
            tbcMain.SelectedTab = tbpChat;
        }

        private void pnlAddChat_MouseEnter(object sender, EventArgs e)
        {
        }

        private void pnlAddChat_MouseLeave(object sender, EventArgs e)
        {
        }

        private void pnlAddChat_Click(object sender, EventArgs e)
        {
            tabpage = tbpAddFriend;
            tbcMain.SelectedTab = tbpAddFriend;
        }

        private void lblAddFriendCommence_MouseEnter(object sender, EventArgs e)
        {
            pnlAddFriendCommence.BackgroundImage = Properties.Resources.btnLearnMore__5_;
        }

        private void lblAddFriendCommence_MouseLeave(object sender, EventArgs e)
        {
            pnlAddFriendCommence.BackgroundImage = Properties.Resources.btnLearnMore__2_;
        }

        private void lblAddFriendCommence_Click(object sender, EventArgs e)
        {
            var t = new Thread(() => addfriend());
            t.Start();
            listloading.Add(new List<string> { "Adding friend", "10" });
        }

        private void nullvoid() { }

        private void addfriend()
        {
            string newe2e = txtNewE2ECode.Text;
            string pubkey = File.ReadAllText("dat\\source\\client\\keys\\puK.uri");
            if (UserCredentials.dhkey == null)
            {
                shownotification(nullvoid, "DHC Key not generated; unable to connect. ", Color.Coral.Name);
                Waitform.closeform = true;
                return;
            }
            List<object> sendobj = new List<object>() { "createuserchat", UserCredentials.username, /*UserCredentials.credentials, */pubkey, txtPeerUsername.Text };
            byte[] response = null;
            while (response == null)
            {
                response = sendtoserver(EncryptMessage(sendobj), 1000);
            }
            try
            {
                foreach (List<string> ls in listloading)
                {
                    if (ls[0] == "Adding friend")
                    {
                        listloading.RemoveAt(listloading.IndexOf(ls));
                    }
                }
            }
            catch { }
            try
            {
                List<object> returnobj = DecryptMessage(response);
                if ((returnobj[0] as string) == "success")
                {
                    try
                    {
                        shownotification(nullvoid, "Affix Services response: Friend request pending.", Color.Coral.Name);
                    }
                    catch
                    {

                    }
                }
                else
                {
                    shownotification(nullvoid, "Affix Services response: " + (string)(returnobj[0]) + (string)(returnobj[1]), Color.Coral.Name);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                shownotification(nullvoid, "Affix Services response: error", Color.Coral.Name);
            }


            /*
            List<byte[]> creds = new List<byte[]>();
            creds.Add(Encoding.UTF8.GetBytes(txtUsername.Text));
            creds.Add(EncryptByteArray(Encoding.UTF8.GetBytes(txtPassword.Text), txtTFA.Text));
            byte[] encmessage = EncryptMessage(new List<object> { "signin", creds });
            MessageBox.Show("");
            byte[] response = sendtoserver(encmessage, 3000);
            DecryptMessage(response);
            */







        }
        private void getchat()
        {
            string pubkey = File.ReadAllText("dat\\source\\client\\keys\\puK.uri");
            if (UserCredentials.dhkey == null)

            {
                shownotification(nullvoid, "DHC Key not generated; unable to connect. ", Color.Coral.Name);
                gettingchat = false;
                return;
            }
            List<object> sendobj = new List<object>() { "getchat", UserCredentials.username, /*UserCredentials.credentials,*/ pubkey, UserCredentials.peerusername };
            byte[] response = null;
            while (response == null)
            {
                response = sendtoserver(EncryptMessage(sendobj), 1000);
            }
            try
            {
                //MessageBox.Show(response.Count().ToString());
                List<object> returnobj = DecryptMessage(response);
                if ((returnobj[0] as string) == "success")
                {
                    List<object> cc = returnobj[1] as List<object>;
                    List<object> decryptedchat = new List<object> { };
                    try
                    {
                        foreach (List<object> message1 in cc)
                        {
                            List<object> decryptedmessage = convertbytetolist(DecryptByteArray((message1[0] as byte[]), "test")) as List<object>;
                            decryptedmessage.Insert(0, (message1[1] as string));
                            decryptedmessage.Insert(3, (message1[2] as string));
                            decryptedchat.Add(decryptedmessage);
                        }
                        currentchat = decryptedchat;
                        gettingchat = false;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        gettingchat = false;
                    }
                }
                else
                {
                    shownotification(nullvoid, "Affix Services response: " + (string)(returnobj[0]) + " " + (string)(returnobj[1]), Color.Coral.Name);
                    gettingchat = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                gettingchat = false;
                //shownotification(nullvoid, "Affix Services response: error", Color.Coral.Name);
            }
        }

        private void displaychat()
        {
            displayingmessages = true;
        retry:;
            try
            {
                foreach (Control c in messageobjectlist)
                {
                    c.Dispose();
                }
                messageobjectlist.Clear();
                messagepanellist.Clear();
                chatpanel = new Panel();
                chatpanel.AutoScroll = true;
                chatpanel.Width = this.Width;
                chatpanel.Height = this.Height - 123;
                chatpanel.Left = 0;
                chatpanel.Top = 25;
                chatpanel.BackColor = Color.Transparent;
                chatpanel.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
                this.Controls.Add(chatpanel);
                chatpanel.Parent = tbpChat;
                messageobjectlist.Add(chatpanel);
                int yloc = 0;
                int numbermessages = 0;
                foreach (List<object> message in currentchat)
                {
                    int prevwidth = 0;

                    Panel pnlSeparator = new Panel();
                    pnlSeparator.Width = 1000;
                    pnlSeparator.Height = 1;
                    pnlSeparator.BackColor = Color.LightGray;
                    pnlSeparator.Top = yloc;
                    pnlSeparator.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
                    this.Controls.Add(pnlSeparator);
                    pnlSeparator.Parent = chatpanel;

                    Label messagelabel = new Label();
                    messagelabel.Text = (message[1] as string);
                    messagelabel.AutoSize = true;
                    messagelabel.Top = 0;
                    messagelabel.ForeColor = Color.Black;
                    messagelabel.Font = lblSmallFont.Font;
                    messagelabel.TextAlign = ContentAlignment.TopLeft;
                    messagelabel.Left = 10;
                    this.Controls.Add(messagelabel);
                    prevwidth = messagelabel.Width;
                    messagelabel.AutoSize = false;
                    decimal scale1 = (prevwidth / this.Width);
                    int scale = 0;
                    if (scale1.ToString().Contains("."))
                    {
                        scale = int.Parse(scale1.ToString().Split('.')[0]) + 1;
                    }
                    else
                    {
                        scale = int.Parse(scale1.ToString()) + 1;
                    }
                    messagelabel.Height = scale * 40;
                    if (messagelabel.Height == 0)
                    {
                        messagelabel.Height = 28;
                    }
                    messagelabel.Width = this.Width - 20;
                    messagelabel.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
                    messageobjectlist.Add(messagelabel);

                    Panel pnlMessage = new Panel();
                    pnlMessage.Width = this.Width;
                    pnlMessage.Height = messagelabel.Height + 40;
                    pnlMessage.BackColor = Color.Transparent;
                    pnlMessage.Top = pnlSeparator.Bottom;
                    pnlMessage.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
                    this.Controls.Add(pnlMessage);
                    pnlMessage.Parent = chatpanel;

                    messagepanellist.Add(pnlMessage);

                    Label timelabel = new Label();
                    timelabel.Text = (message[2] as string);
                    timelabel.Font = lblSmallFont.Font;
                    timelabel.ForeColor = Color.DarkGray;
                    timelabel.AutoSize = true;
                    timelabel.Top = messagelabel.Bottom;
                    timelabel.Left = messagelabel.Left;
                    this.Controls.Add(timelabel);
                    messageobjectlist.Add(timelabel);
                    timelabel.Parent = pnlMessage;

                    prevchat.Add((message[3] as string));

                    //chatpanel.Height = chatpanel.Height + pnlMessage.Height;
                    messagelabel.Parent = pnlMessage;
                    messageobjectlist.Add(pnlMessage);
                    yloc = pnlMessage.Bottom;
                }
                ScrollToBottom(chatpanel);
                displayingmessages = false;
                //chatpanel.Top = -(chatpanel.Height) + (this.Height - 103); //- (scrChatVertical.Value * interval);
            }
            catch
            {
                displayingmessages = false;
            }
            displayingmessages = false;
        }

        public void ScrollToBottom(Panel p)
        {
            using (Control c = new Control() { Parent = p, Dock = DockStyle.Bottom })
            {
                p.ScrollControlIntoView(c);
                c.Parent = null;
            }
        }

        private void displaynewmessages()
        {
            displayingmessages = true;
            if(!(messagepanellist.Count > 0))
            {
                displaychat();
                displayingmessages = false;
                return;
            }
            try
            {
                Panel lastpanel = messagepanellist[messagepanellist.Count - 1] as Panel;
                int yloc = lastpanel.Bottom;
                foreach (List<object> message in currentchat)
                {
                    bool newmessage = true;
                    foreach (string messageid in prevchat)
                    {
                        if (messageid == (message[3] as string))
                        {
                            newmessage = false;
                        }
                    }
                    if (newmessage)
                    {
                        int prevwidth = 0;

                        Panel pnlSeparator = new Panel();
                        pnlSeparator.Width = 1000;
                        pnlSeparator.Height = 1;
                        pnlSeparator.BackColor = Color.LightGray;
                        pnlSeparator.Top = yloc;
                        pnlSeparator.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
                        this.Controls.Add(pnlSeparator);
                        pnlSeparator.Parent = chatpanel;

                        Label messagelabel = new Label();
                        messagelabel.Text = (message[1] as string);
                        messagelabel.AutoSize = true;
                        messagelabel.Top = 0;
                        messagelabel.ForeColor = Color.Black;
                        messagelabel.Font = lblSmallFont.Font;
                        messagelabel.TextAlign = ContentAlignment.TopLeft;
                        messagelabel.Left = 10;
                        this.Controls.Add(messagelabel);
                        prevwidth = messagelabel.Width;
                        messagelabel.AutoSize = false;
                        decimal scale1 = (prevwidth / this.Width);
                        int scale = 0;
                        if (scale1.ToString().Contains("."))
                        {
                            scale = int.Parse(scale1.ToString().Split('.')[0]) + 1;
                        }
                        else
                        {
                            scale = int.Parse(scale1.ToString()) + 1;
                        }
                        messagelabel.Height = scale * 40;
                        if (messagelabel.Height == 0)
                        {
                            messagelabel.Height = 28;
                        }
                        messagelabel.Width = this.Width - 20;
                        messagelabel.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
                        messageobjectlist.Add(messagelabel);

                        Panel pnlMessage = new Panel();
                        pnlMessage.Width = this.Width;
                        pnlMessage.Height = messagelabel.Height + 40;
                        pnlMessage.BackColor = Color.Transparent;
                        pnlMessage.Top = pnlSeparator.Bottom;
                        pnlMessage.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
                        this.Controls.Add(pnlMessage);
                        pnlMessage.Parent = chatpanel;

                        messagepanellist.Add(pnlMessage);

                        Label timelabel = new Label();
                        timelabel.Text = (message[2] as string);
                        timelabel.Font = lblSmallFont.Font;
                        timelabel.ForeColor = Color.DarkGray;
                        timelabel.AutoSize = true;
                        timelabel.Top = messagelabel.Bottom;
                        timelabel.Left = messagelabel.Left;
                        this.Controls.Add(timelabel);
                        messageobjectlist.Add(timelabel);
                        timelabel.Parent = pnlMessage;
                        prevchat.Add((message[3] as string));

                        //chatpanel.Height = chatpanel.Height + pnlMessage.Height;
                        messagelabel.Parent = pnlMessage;
                        messageobjectlist.Add(pnlMessage);
                        yloc = pnlMessage.Bottom;
                    }
                }
                displayingmessages = false;
                ScrollToBottom(chatpanel);
                //chatpanel.Top = -(chatpanel.Height) + (this.Height - 103); //- (scrChatVertical.Value * interval);


            }
            catch
            {
                displayingmessages = false;
            }
            displayingmessages = false;
        }




        private void AffixCenter_ResizeEnd(object sender, EventArgs e)
        {
            displaychat();
        }

        private void AffixCenter_StyleChanged(object sender, EventArgs e)
        {
        }

        private void AffixCenter_SizeChanged(object sender, EventArgs e)
        {
            if (!(prevfws == this.WindowState))
            {
                displaychat();
                prevfws = WindowState;
            }
        }

        private void updatechatdimensions_tick(object sender, EventArgs e)
        {
            //MessageBox.Show("");
            //displaychat();
        }

        private void AffixCenter_ResizeBegin(object sender, EventArgs e)
        {
        }

        private void pnlBackToChats_MouseEnter(object sender, EventArgs e)
        {
            pnlBackToChats.BackgroundImage = Properties.Resources.Left_Page_Selected;
        }

        private void pnlBackToChats_MouseLeave(object sender, EventArgs e)
        {
            pnlBackToChats.BackgroundImage = Properties.Resources.Left_Page;
        }

        private void pnlBackToChats_Click(object sender, EventArgs e)
        {
            tabpage = tbpApplications;
            tbcMain.SelectedTab = tbpApplications;
        }

        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                waitingtosend.Add(txtMessage.Text);
                txtMessage.Text = "";
                if (!sendingmessage)
                {
                    gettingchat = true;
                    sendingmessage = true;
                    var f = new Thread(() => sendmessage());
                    f.Start();
                }
            }
        }

        private void sendmessage()
        {
            string pubkey = File.ReadAllText("dat\\source\\client\\keys\\puK.uri");
        startsend:;
            if (UserCredentials.dhkey == null)
            {
                shownotification(nullvoid, "DHC Key not generated; unable to connect. ", Color.Coral.Name);
                sendingmessage = false;
                gettingchat = false;
                return;
            }
            List<object> messagelist = new List<object> { };
            try
            {
                foreach (string messagetext in waitingtosend)
                {
                    List<object> message = new List<object> { messagetext, "testtime" };
                    byte[] encmessagebyte = EncryptByteArray(convertlisttobyte(message), "test");
                    messagelist.Add(encmessagebyte);
                }
            }
            catch
            {
                goto startsend;
            }
            List<object> sendobj = new List<object>() { "sendmessage", UserCredentials.username, /*UserCredentials.credentials, */pubkey, UserCredentials.peerusername, messagelist };
            byte[] response = null;
            while (response == null)
            {
                response = sendtoserver(EncryptMessage(sendobj), 10000);
            }
            try
            {
                //MessageBox.Show(response.Count().ToString());
                List<object> returnobj = DecryptMessage(response);
                if ((returnobj[0] as string) == "success")
                {
                    waitingtosend.RemoveRange(0, messagelist.Count);
                    List<object> cc = returnobj[1] as List<object>;
                    List<object> decryptedchat = new List<object> { };
                    try
                    {
                        foreach (List<object> message1 in cc)
                        {
                            List<object> decryptedmessage = convertbytetolist(DecryptByteArray((message1[0] as byte[]), "test")) as List<object>;
                            decryptedmessage.Insert(0, (message1[1] as string));
                            decryptedmessage.Insert(3, (message1[2] as string));
                            decryptedchat.Add(decryptedmessage);
                        }
                        currentchat = decryptedchat;
                            displaynewmessages();
                        
                        //displaynewmessages();
                        if (waitingtosend.Count > 0)
                        {
                            goto startsend;
                        }
                        sendingmessage = false;
                        gettingchat = false;
                    }
                    catch (Exception ex)
                    {
                        sendingmessage = false;
                        gettingchat = false;
                        MessageBox.Show(ex.ToString());
                    }
                }
                else
                {
                    shownotification(nullvoid, "Affix Services response: " + (string)(returnobj[0]) + " " + (string)(returnobj[1]), Color.Coral.Name);
                    sendingmessage = false;
                    gettingchat = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //MessageBox.Show(ex.ToString());
                //shownotification(nullvoid, "Affix Services response: error", Color.Coral.Name);
                sendingmessage = false;
                gettingchat = false;
            }
        }
            
        private void scrChatVertical_ValueChanged(object sender, EventArgs e)
        {
        }

        private void refreshchattimer_tick(object sender, EventArgs e)
        {
            if (!gettingchat)
            {
                gettingchat = true;
                var f = new Thread(() => { getchat();
                });
                f.Start();
                //MessageBox.Show(waitingtosend.Count().ToString());
            }
            displaynewmessages();
        }

        private void pnlCloudwire_MouseEnter(object sender, EventArgs e)
        {
            pnlCloudwire.BackgroundImage = Properties.Resources.CloudwireSelected;
        }

        private void pnlCloudwire_MouseLeave(object sender, EventArgs e)
        {
            pnlCloudwire.BackgroundImage = Properties.Resources.Cloudwire;
        }

        private void pnlCloudwire_Click(object sender, EventArgs e)
        {
            var f = new Cloudwire();
            f.ShowDialog();
        }




















        //HolePunch
        private void btnHolePunch_Click(object sender, EventArgs e)
        {
            tabpage = tbpHolePunch;
            tbcMain.SelectedTab = tbpHolePunch;
        }





        private void holepunch()
        {
            //logmykeys(new List<object> { new List<object> { "psv://teamaffix/testserver", "<RSAKeyValue><Modulus>sgmXQsmWU/xkLwGwOsGbsaGYlZB+Kb7LixnKb4/Rh5jxwxtj4xo2BozxR8yfzFxax5uYzFmjGYIGEuGqIfEY3G02j6JDe2cW1YOBcizOywLIn8j9swpKxU7IpaYi1LqC9CLlp/wadjC4k44tVbPMdIBn0ZADzjhjyCiK5RPIpPRwH+aCXhs1IRWNE/a40v1ulr1kocvB7PD6iydTdBOM81FdS7JdOTN2seyuYDFWyQwrNqDUh/q6OFFUrQFZp336GPVOZYq+kDu2ohSb5aEXLqmZnsJifIkvao3BNbw4/NYcdFhRUMlgvp5OwFZ9w6O7ix+3VKXSZDIK7C5hElE+1rgbLaLdGizh3lE834Y/zyKXbQY3gFzglKhxky7ApXbTU+a3r67rTGo/AQwDsoUu/tDkzl8NI27xEQv4HoRnmCkIAE/JBwsITcCdtZKskCessdg4QB7GsL2YB+MawY9YchNMtGV6plI7JsSO/agVa9pJiHRmwJi6CiUQsf4AYAff06S7OFZTWwbKP1aDc56QN/dyCPFRQ1MyjByHSypXEtlAeBpLMlM53Gl58I35qTG+okUcMT/j+4X7TF+FW7N04NZOeo8bMFJrlJXyvPY5J2eFQjKuTTBnVKDyObbbhnPQnGMU1lEh2O8UXQR0kjetFtXUypEzJPX2VUvGNOumdpk=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>" } });
            foreach (List<object> peer in TotalPersons)
            {
                if (UserCredentials.protocolmode == "udp")
                {
                    Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    IPEndPoint RemoteEndPoint = new IPEndPoint(IPAddress.Parse("1.1.1.1"), 10000);
                    byte[] buffer = new byte[2];
                    client.SendTo(buffer, buffer.Length, SocketFlags.None, RemoteEndPoint);
                    peer[1] = client;
                }
                else
                {
                    Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    retry:;
                    try
                    {
                        string ranport = randomStringNumeric(5);
                        IPEndPoint LocalIPEP = new IPEndPoint(IPAddress.Any, int.Parse(ranport));
                        client.Bind((EndPoint)LocalIPEP);
                        peer[1] = client;
                    }
                    catch
                    {
                        goto retry;
                    }
                }
            }
            List<object> keys = Generatekeyswithoutwrite();
            string temppubkey = keys[0] as string;
            string tempprikey = keys[1] as string;
            //MessageBox.Show(temppubkey);
            logmykeys(new List<object> { new List<object> { "psv://teamaffix/testserver", temppubkey } });
            //MessageBox.Show("");
            List<object> peerkeys = getpeerkeys(new List<object> { "psv://teamaffix/testserver" });
            foreach(List<object> peer in peerkeys)
            {
                //MessageBox.Show(peer[1] as string);
            }
            logmyips();
            int loopnumber = 0;
            while (true)
            {
                System.Threading.Thread.Sleep(4000);
                if (loopnumber == 10)
                {
                    logmyips();
                    loopnumber = 0;
                }
                List<object> peersinfo = getpeerip();
                foreach (List<object> peer in peersinfo) 
                {
                    //MessageBox.Show(peer[1] as string);
                    string[] peerinfo = (peer[1] as string).Split(':');
                    string peerip = peerinfo[0];
                    if (peerip == "192.168.1.1")
                    {
                        //MessageBox.Show("");
                        //peerip = "67.174.235.214";
                    }
                    int peerport = int.Parse(peerinfo[1]);
                    lblPeerInfo.Invoke((MethodInvoker)(() => { lblPeerInfo.Text = peerip + " " + peerport.ToString(); }));
                    //generateholepunchlistener(peerip, peerport, myport);
                    IPEndPoint RemoteEndPoint = new IPEndPoint(IPAddress.Parse(peerip), peerport);
                    foreach (List<object> peeruser in TotalPersons)
                    {
                        if (peeruser[0] as string == peer[0] as string)
                        {
                            if (peeruser[4] as bool? == false && (peerip != (peeruser[2] as IPEndPoint).Address.ToString() || peerport != (peeruser[2] as IPEndPoint).Port))
                            {
                                opentopeer(peeruser[1] as Socket, peer[0] as string, peerip, peerport);
                            }
                            sendtopeer("psv://teamaffix/testserver", EncryptP2P(new List<object> { "signin" }, (peerkeys[0] as List<object>)[1] as string));
                        }
                    }
                }
                loopnumber++;
            }
        }

        private void sendtopeer(string peeruser, byte[] data)
        {
            foreach(List<object> person in TotalPersons)
            {
                if(person[0] as string == peeruser)
                {
                    //MessageBox.Show("");
                    Socket client = (person[1]) as Socket;
                    IPEndPoint RemoteEndPoint = (person[2]) as IPEndPoint;
                    client.Send(BitConverter.GetBytes(data.Length), 0, 4, 0);
                    client.Send(data);
                }
            }
        }


        private byte[] SubArray(byte[] data, int index, int length)
        {
            byte[] result = new byte[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        private int logmyip()
        {
            long randomnumber2 = LongRandom(4000, 10000, new Random());
            string pubkey = UserCredentials.mypublickey;
            if (UserCredentials.dhkey == null)
            {
                shownotification(nullvoid, "DHC Key not generated; unable to connect. ", Color.Coral.Name);
                return 0;
            }
            List<object> sendobj = new List<object>() { "holepunchlog", UserCredentials.username, /* UserCredentials.credentials, */ pubkey };
            byte[] response = null;
            while (response == null)
            {
                response = bindandsend(EncryptMessage(sendobj), 1000, int.Parse(randomnumber2.ToString()));
            }
            try
            {
                List<object> returnobj = DecryptMessage(response);
                if ((string)(returnobj[0]) == "success")
                {
                    return int.Parse(randomnumber2.ToString());
                }
                else
                {
                    shownotification(nullvoid, "Affix Services response: error", Color.Coral.Name);
                    return 0;
                }
            }
            catch
            {
                return 0;
            }


        }


        




        private string getpeerip1()
        {
            string pubkey = UserCredentials.mypublickey;
            if (UserCredentials.dhkey == null)
            {
                shownotification(nullvoid, "DHC Key not generated; unable to connect. ", Color.Coral.Name);
                return null;
            }
            List<object> sendobj = new List<object>() { "holepunchget", UserCredentials.username, /* UserCredentials.credentials, */ pubkey, txtHolePunchPeer.Text };
            byte[] response = null;
            while (response == null)
            {
                response = sendtoserver(EncryptMessage(sendobj), 1000);
            }
            try
            {
                List<object> returnobj = DecryptMessage(response);
                if ((string)(returnobj[0]) == "success")
                {
                    return (string)(returnobj[1]);
                }
                else
                {
                    shownotification(nullvoid, "Affix Services response: error", Color.Coral.Name);
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        private void generateholepunchlistener(string peerip, int peerport, int myport)
        {
            IPEndPoint RemoteEndPoint = new IPEndPoint(IPAddress.Parse(peerip), peerport);
            IPEndPoint listeningep = new IPEndPoint(IPAddress.Any, myport);
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            client.Bind(listeningep);
            string welcome = "Hello";
            byte[] data = Encoding.ASCII.GetBytes(welcome);
            client.SendTo(data, data.Length, SocketFlags.None, RemoteEndPoint);
            client.ReceiveTimeout = 10000;
            EndPoint remep = (EndPoint)RemoteEndPoint;
            byte[] buffer = new byte[client.ReceiveBufferSize];
            client.Connect((EndPoint)RemoteEndPoint);
        retry:;
            try
            {
            client.SendTo(data, data.Length, SocketFlags.None, RemoteEndPoint);
                while (true)
                {
                    client.SendTo(data, data.Length, SocketFlags.None, RemoteEndPoint);
                    int rec = client.ReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref remep);
                    new Thread(() => { MessageBox.Show(Encoding.ASCII.GetString(buffer)); }).Start();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                goto retry;
            }
        }


        private void logmyips()
        {
            string pubkey = UserCredentials.mypublickey;
            //List<object> credentials = UserCredentials.credentials;
                List<object> listendpoints = new List<object> { };
                foreach (List<object> peer in TotalPersons)
                {
                    Socket client = peer[1] as Socket;
                    listendpoints.Add(new List<object> { peer[0] as string, client.LocalEndPoint.ToString().Split(':')[1] });
                }
                List<object> sendobj = new List<object>() { "holepunchlogwithIFA2", UserCredentials.username, UserCredentials.verificationKey, pubkey, listendpoints };
                byte[] response = null;
                while (response == null)
                {
                    response = sendtoserver(EncryptMessage(sendobj), 1000);
                }
                try
                {
                    List<object> returnobj = DecryptMessage(response);
                    if ((string)(returnobj[0]) == "success")
                    {
                        //MessageBox.Show("success");
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                catch
                {
                    return;
                }
            

        }



        private byte[] EncryptP2P(List<object> sendobj, string pubkey)
        {
            string randomkey = randomString(60);

            byte[] sendobjbyte = convertlisttobyte(sendobj);

            byte[] encsendobjbyte = EncryptByteArray(sendobjbyte, randomkey);

            byte[] encrandomkey = Encrypt(pubkey, Encoding.UTF8.GetBytes(randomkey));

            List<object> fullsendobj = new List<object> { encsendobjbyte, encrandomkey };

            byte[] fullsendobjbyte = convertlisttobyte(fullsendobj);

            return fullsendobjbyte;
        }

        private List<object> DecryptP2P(byte[] EncryptedBytes, string prikey)
        {
            List<object> fullsendobj = convertbytetolist(EncryptedBytes) as List<object>;

            byte[] encrandomkey = fullsendobj[1] as byte[];

            byte[] encsendobjbyte = fullsendobj[0] as byte[];

            string randomkey = Encoding.UTF8.GetString(Decrypt(prikey, encrandomkey));

            byte[] sendobjbyte = DecryptByteArray(encsendobjbyte, randomkey);

            List<object> sendobj = convertbytetolist(sendobjbyte) as List<object>;

            return sendobj;
        }

        static List<object> Generatekeyswithoutwrite()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                string pubKeyString = rsa.ToXmlString(false);
                string priKeyString = rsa.ToXmlString(true);
                return new List<object> { pubKeyString, priKeyString };
            }
        }




        private void logmykeys(List<object> userandkeytielist)
        {
            string pubkey = UserCredentials.mypublickey;
            List<object> sendobj = new List<object>() { "holepunchkeylog", UserCredentials.username, UserCredentials.verificationKey, pubkey, userandkeytielist };
            byte[] response = null;

            while (response == null)
            {
                response = sendtoserver(EncryptMessage(sendobj), 1000);
            }
            try
            {
                List<object> returnobj = DecryptMessage(response);
                if ((string)(returnobj[0]) == "success")
                {
                    return;
                }
                else
                {
                    return;
                }
            }
            catch
            {
                return;
            }
        }


        private List<object> getpeerkeys(List<object> userlist)
        {
            string pubkey = UserCredentials.mypublickey;
            List<object> sendobj = new List<object>() { "holepunchkeyget", UserCredentials.username, UserCredentials.verificationKey, pubkey, userlist };
            byte[] response = null;
            while (response == null)
            {
                response = sendtoserver(EncryptMessage(sendobj), 1000);
            }
            try
            {
                List<object> returnobj = DecryptMessage(response);
                if ((string)(returnobj[0]) == "success")
                {
                    return returnobj[1] as List<object>;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }


        private List<object> getpeerip()
        {
            string pubkey = UserCredentials.mypublickey;
            //List<object> credentials = UserCredentials.credentials;
            List<object> disconnectedusernames = new List<object> { };
            foreach (List<object> person in TotalPersons)
            {
                if (person[4] as bool? == false)
                {
                    disconnectedusernames.Add(person[0] as string);
                }
            }
            List<object> sendobj = new List<object>() { "holepunchgetwithIFA2", UserCredentials.username, UserCredentials.verificationKey, pubkey, disconnectedusernames };
            byte[] response = null;
            while (response == null)
            {
                response = sendtoserver(EncryptMessage(sendobj), 1000);
            }
            try
            {
                List<object> returnobj = DecryptMessage(response);
                if ((string)(returnobj[0]) == "success")
                {
                    return (returnobj[1]) as List<object>;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }



        private void opentopeer(Socket client, string peerusername, string peerip, int peerport)
        {
            IPEndPoint RemoteEndPoint = new IPEndPoint(IPAddress.Parse(peerip), peerport);
            //IPEndPoint listeningep = new IPEndPoint(IPAddress.Any, myport);
            string welcome = "hi";
            byte[] data = Encoding.ASCII.GetBytes(welcome);
            if (UserCredentials.protocolmode == "udp")
            {
                client.SendTo(data, data.Length, SocketFlags.None, RemoteEndPoint);
                client.ReceiveTimeout = 1000;
                EndPoint remep = (EndPoint)RemoteEndPoint;
                byte[] buffer = new byte[client.ReceiveBufferSize];
                client.Connect((EndPoint)RemoteEndPoint);
                int indexofperson = -1;
                foreach (List<object> person in TotalPersons)
                {
                    if (person[0] as string == peerusername)
                    {
                        indexofperson = TotalPersons.IndexOf(person);
                    }
                }
                if (indexofperson == -1)
                {
                    return;
                }
                List<object> thisperson = TotalPersons[indexofperson] as List<object>;
                thisperson[2] = RemoteEndPoint;
                thisperson[3] = true;
                try
                {
                    WaitForData(client, buffer);
                }
                catch (SocketException se)
                {
                    //Socket has been closed  
                    //Close/dispose of socket
                }
            }
            else
            {
                client.ReceiveTimeout = 20000;
                //MessageBox.Show("starting");
                try
                {
                    client.Connect(RemoteEndPoint);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                MessageBox.Show("success!");
                
                int indexofperson = -1;
                foreach (List<object> person in TotalPersons)
                {
                    if (person[0] as string == peerusername)
                    {
                        indexofperson = TotalPersons.IndexOf(person);
                    }
                }
                if (indexofperson == -1)
                {
                    return;
                }
                List<object> thisperson = TotalPersons[indexofperson] as List<object>;
                thisperson[2] = RemoteEndPoint;
                thisperson[3] = true;
                try
                {
                    //WaitForData(client);
                }
                catch (SocketException se)
                {
                    //Socket has been closed  
                    //Close/dispose of socket
                }
            }
        }

        void WaitForData(Socket client, byte[] buffer)
        {
            try
            {
                client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReadDataCallback), client);
            }
            catch (SocketException se)
            {
                //Socket has been closed  
                //Close/dispose of socket
            }
        }



        public void ReadDataCallback(IAsyncResult ar)
        {
            Socket client = (Socket)ar.AsyncState;
            try
            {
                // Read data from the client socket.
                int iRx = client.EndReceive(ar);


                MessageBox.Show("");


                WaitForData(client, new byte[client.ReceiveBufferSize]);
            }
            catch (ObjectDisposedException)
            {
                //Socket has been closed  
                //Close/dispose of socket
            }
            catch (SocketException)
            {
                //Socket exception
                //Close/dispose of socket
            }


        }


        private void lblChatsTitle_Click(object sender, EventArgs e)
            {

            }

        private void tbpHome_Click(object sender, EventArgs e)
        {

        }

        private void lblLogOut_MouseEnter(object sender, EventArgs e)
        {
            pnlLogOut.BackgroundImage = Properties.Resources.Continue_to_chats__1_;
        }

        private void lblLogOut_MouseLeave(object sender, EventArgs e)
        {
            pnlLogOut.BackgroundImage = Properties.Resources.Continue_to_chats;
        }

        private void lblLogOut_Click(object sender, EventArgs e)
        {
            if (File.Exists("dat\\source\\client\\credentials\\credentials.uri"))
            {
                File.Delete("dat\\source\\client\\credentials\\credentials.uri");
            }
            lblContinueToChats.Text = "Sign in";
            pnlLogOut.Hide();
            UserCredentials.credentialsimported = false;
            UserCredentials.verificationKey = null;
        }

        private void tbpApplications_Click(object sender, EventArgs e)
        {

        }
    }
}
