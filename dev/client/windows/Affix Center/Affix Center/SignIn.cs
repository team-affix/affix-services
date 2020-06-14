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
using System.Net;
using System.Net.Sockets;

namespace Affix_Center
{
    public partial class SignIn : Form
    {
        public static bool complete { get; set; }
        public static bool signinsuccess { get; set; }
        public SignIn()
        {
            InitializeComponent();
            complete = false;
        }

        private void SignIn_Load(object sender, EventArgs e)
        {
            List<object> keys = Generatekeyswithoutwrite();
            string senderpubkey = keys[0] as string;
            string senderprikey = keys[1] as string;
            List<object> keys1 = Generatekeyswithoutwrite();
            string receiverpubkey = keys1[0] as string;
            string receiverprikey = keys1[1] as string;
            byte[] messageenc = Encrypt(receiverpubkey, Encoding.UTF8.GetBytes("hello"));
            byte[] signature = Generatevalidsignature(senderprikey, messageenc);
            MessageBox.Show(Checksignature(senderpubkey, signature, messageenc).ToString());

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
        private void pnlSignInButton_Click(object sender, EventArgs e)
        {
            var f = new Waitform();
            new Thread(() => {
                f.ShowDialog();
            }).Start();
            //signin();
            signinwithIFA2();
        }

        private void lblSignInButton_MouseEnter(object sender, EventArgs e)
        {
            pnlSignInButton.BackgroundImage = Properties.Resources.SignInButton__1_;
        }

        private void lblSignInButton_MouseLeave(object sender, EventArgs e)
        {
            pnlSignInButton.BackgroundImage = Properties.Resources.SignInButton;
        }




        private void DHPartialKeyGen()
        {

        }

        private void nullvoid() { }

        /*
        private void signin()
        {
            string pubkey = File.ReadAllText("dat\\source\\client\\keys\\puK.uri");
            if(UserCredentials.dhkey == null)
            {
                shownotification(nullvoid, "DHC Key not generated; unable to connect. ", Color.Coral.Name);
                Waitform.closeform = true;
                return;
            }
            List<object> sendobj = new List<object>() { "signin", txtUsername.Text, EncryptByteArray(Encoding.UTF8.GetBytes(txtPassword.Text), txtTFA.Text), pubkey};
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
                    shownotification(nullvoid, "Affix Services response: Welcome, " + txtUsername.Text, Color.Cyan.Name);
                    UserCredentials.credentials = EncryptByteArray(Encoding.UTF8.GetBytes(txtPassword.Text), txtTFA.Text);
                    UserCredentials.username = txtUsername.Text;
                    Waitform.closeform = true;
                    List<object> fullcredentials = new List<object> { UserCredentials.username, UserCredentials.credentials };
                    File.WriteAllBytes("dat\\source\\client\\credentials\\credentials.uri", convertlisttobyte(fullcredentials));
                    this.Close();
                }
                else
                {
                    shownotification(nullvoid, "Affix Services response: Incorrect credentials.", Color.Coral.Name);
                    Waitform.closeform = true;
                }
            }
            catch
            {
                Waitform.closeform = true;
            }

        
            
        }
            */

        private void signinwithIFA2()
        {
            string pubkey = UserCredentials.mypublickey;
            if (UserCredentials.dhkey == null)
            {
                shownotification(nullvoid, "DHC Key not generated; unable to connect. ", Color.Coral.Name);
                Waitform.closeform = true;
                return;
            }
            List<object> sendobj = new List<object>() { "signinIFA2", txtUsername.Text, pubkey };
            byte[] response = null;
            while (response == null)
            {
                response = sendtoserver(EncryptMessage(sendobj), 1000);
            }
            try
            {
                List<object> returnobj = DecryptMessage(response);
                if ((string)(returnobj[0]) == "success")
                {/*
                    try
                    {
                        
                        //MessageBox.Show("");
                        byte[] encPermPriKey = returnobj[1] as byte[];
                        byte[] encVerificationMessage = returnobj[2] as byte[];
                        byte[] encVerifiedConnectionKey = returnobj[3] as byte[];

                        byte[] tfaremovedEncPermPriKey = DecryptByteArray(encPermPriKey, txtTFA.Text);
                        byte[] permPriKeyByte = DecryptByteArray(tfaremovedEncPermPriKey, txtPassword.Text);

                        string permPriKey = Encoding.UTF8.GetString(permPriKeyByte);

                        byte[] verificationMessageByte = Decrypt(permPriKey, encVerificationMessage);
                        string verificationMessage = Encoding.UTF8.GetString(verificationMessageByte);

                        byte[] verifiedConnectionKeyByte = DecryptByteArray(encVerifiedConnectionKey, verificationMessage);
                        string verifiedConnectionKey = Encoding.UTF8.GetString(verifiedConnectionKeyByte);

                        UserCredentials.username = txtUsername.Text;
                        UserCredentials.verificationKey = verifiedConnectionKey;

                        List<object> fullcredentials = new List<object> { UserCredentials.username, verifiedConnectionKey };
                        if (UserCredentials.filesEncrypted)
                        {
                            File.WriteAllBytes("dat\\source\\client\\credentials\\credentials.uri", convertlisttobyte(fullcredentials));
                            EncryptFile("dat\\source\\client\\credentials\\credentials.uri", "dat\\source\\client\\credentials\\credentials.uri", UserCredentials.LFA);
                        }
                        else
                        {
                            File.WriteAllBytes("dat\\source\\client\\credentials\\credentials.uri", convertlisttobyte(fullcredentials));
                        }

                        shownotification(nullvoid, "Affix Services response: Welcome, " + txtUsername.Text, Color.Cyan.Name);
                        UserCredentials.signedin = true;
                        UserCredentials.credentialsimported = true;
                        Waitform.closeform = true;
                        this.Close();
                        //MessageBox.Show(verifiedConnectionKey);

                    }
                    catch
                    {
                        shownotification(nullvoid, "Affix Services response: Incorrect credentials.", Color.Coral.Name);
                        Waitform.closeform = true;
                    }
                    */

                    try
                    {

                        UserCredentials.creds = new List<object> { txtPassword.Text, txtTFA.Text };
                        byte[] encPermPriKey = returnobj[1] as byte[];
                        byte[] encVerificationMessage = returnobj[2] as byte[];
                        byte[] encVerifiedConnectionKey = returnobj[3] as byte[];
                        List<object> tokenlist = returnobj[4] as List<object>;

                        UserCredentials.IFA2details = new List<object> { tokenlist, encPermPriKey, encVerificationMessage, encVerifiedConnectionKey, txtUsername.Text };
                        if (tokenlist.Count == 0)
                        {

                            byte[] tfaremovedEncPermPriKey = DecryptByteArray(encPermPriKey, txtTFA.Text);
                            byte[] permPriKeyByte = DecryptByteArray(tfaremovedEncPermPriKey, txtPassword.Text);

                            string permPriKey = Encoding.UTF8.GetString(permPriKeyByte);

                            byte[] verificationMessageByte = Decrypt(permPriKey, encVerificationMessage);
                            string verificationMessage = Encoding.UTF8.GetString(verificationMessageByte);

                            byte[] verifiedConnectionKeyByte = DecryptByteArray(encVerifiedConnectionKey, verificationMessage);
                            string verifiedConnectionKey = Encoding.UTF8.GetString(verifiedConnectionKeyByte);

                            UserCredentials.verificationKey = verifiedConnectionKey;
                            UserCredentials.username = txtUsername.Text;
                            
                            List<object> fullcredentials = new List<object> { UserCredentials.username, verifiedConnectionKey };
                            if (UserCredentials.filesEncrypted)
                            {
                                File.WriteAllBytes("dat\\source\\client\\credentials\\credentials.uri", convertlisttobyte(fullcredentials));
                                EncryptFile("dat\\source\\client\\credentials\\credentials.uri", "dat\\source\\client\\credentials\\credentials.uri", UserCredentials.LFA);
                            }
                            else
                            {
                                File.WriteAllBytes("dat\\source\\client\\credentials\\credentials.uri", convertlisttobyte(fullcredentials));
                            }

                            UserCredentials.credentialsimported = true;

                            UserCredentials.signedin = true;
                            shownotification(nullvoid, "Affix Services response: Welcome, " + UserCredentials.username, Color.Cyan.Name);
                            Waitform.closeform = true;
                            this.Close();
                            return;
                            //MessageBox.Show(verifiedConnectionKey);
                        }
                        else
                        {
                            Waitform.closeform = true;
                            System.Threading.Thread.Sleep(100);
                            var f = new PhysicalTokenRegistration();
                            f.ShowDialog();
                            if (UserCredentials.signedin)
                            {
                                this.Close();
                            }
                            else
                            {
                                txtUsername.Text = "";
                                txtPassword.Text = "";
                                txtTFA.Text = "";
                            }
                            return;
                        }

                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        shownotification(nullvoid, "Affix Services response: Incorrect credentials.", Color.Coral.Name);
                        Waitform.closeform = true;
                        return;
                    }
                }
                else
                {
                    shownotification(nullvoid, "Affix Services response: Failure to retrieve IFA-2 Private Key.", Color.Coral.Name);
                    Waitform.closeform = true;
                }
            }
            catch
            {
                Waitform.closeform = true;
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


        private void addtoken(List<object> tokenlist)
        {
            string pubkey = UserCredentials.mypublickey;
            if (UserCredentials.dhkey == null)
            {
                shownotification(nullvoid, "DHC Key not generated; unable to connect. ", Color.Coral.Name);
                Waitform.closeform = true;
                return;
            }
            List<object> sendobj = new List<object>() { "addtoken", txtUsername.Text, UserCredentials.verificationKey, tokenlist };
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
                    MessageBox.Show("success");
                }
                else
                {
                    shownotification(nullvoid, "Affix Services response: Failure to retrieve IFA-2 Private Key.", Color.Coral.Name);
                    Waitform.closeform = true;
                }
            }
            catch
            {
                Waitform.closeform = true;
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
                catch (Exception ex)
                {
                    //MessageBox.Show("unable to receive");
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
                        size -= rec;
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

        static void Generatekeys(string pubKeyFileName, string priKeyFileName)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
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
            using (var rsa = new RSACryptoServiceProvider(2048))
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
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(privateKey);
                decrypted = rsa.Decrypt(input, true);
            }
            return decrypted;
        }

        static bool Checksignature(string sendersPublicKey, byte[] signature, byte[] signedData)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(sendersPublicKey);
                SHA256Managed hash = new SHA256Managed();
                byte[] hashedData;
                hashedData = hash.ComputeHash(signedData);
                return rsa.VerifyHash(hashedData, CryptoConfig.MapNameToOID("SHA256"), signature);
            }
        }

        static byte[] Generatevalidsignature(string privateKey, byte[] enc)
        {
            byte[] endsignature;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(privateKey);
                
                SHA256Managed hashmethod = new SHA256Managed();

                byte[] hasheddata = hashmethod.ComputeHash(enc);

                endsignature = rsa.SignHash(hasheddata, CryptoConfig.MapNameToOID("SHA256"));
            }
            return endsignature;
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
                    this.Activate();
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

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
        }

        private void pnlCreateAffiliation_MouseLeave(object sender, EventArgs e)
        {
        }

        private void pnlCreateAffiliation_Click(object sender, EventArgs e)
        {
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            pnlCreateAffiliation.BackgroundImage = Properties.Resources.CreateaffiliationButtonSelected;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            pnlCreateAffiliation.BackgroundImage = Properties.Resources.CreateaffiliationButton__1_;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            AffixCenter.showsignup = true;
            this.Close();
        }

        private void lblSignInTitle_Click(object sender, EventArgs e)
        {

        }

        private void SignIn_Shown(object sender, EventArgs e)
        {
            if(UserCredentials.color1 == "dark")
            {
                this.BackColor = Color.FromArgb(30,30,30);
                txtUsername.BackColor = Color.FromArgb(30, 30, 30);
                txtPassword.BackColor = Color.FromArgb(30, 30, 30);
                txtTFA.BackColor = Color.FromArgb(30, 30, 30);
                lblSignInTitle.ForeColor = Color.LightGray;
                lblLoadingDesc.ForeColor = Color.White;
                lblUsername.ForeColor = Color.White;
                lblPassword.ForeColor = Color.White;
                lblTFA.ForeColor = Color.White;
                pnlProgramIcon.BackgroundImage = Properties.Resources.AffixLogoWhite;
                pnlProgramIcon.BackColor = Color.Transparent;
                lblAffixServicesDesc.ForeColor = Color.LightGray;
                txtUsername.ForeColor = Color.White;
                txtPassword.ForeColor = Color.White;
                txtTFA.ForeColor = Color.DodgerBlue;

            }
            else
            {

            }
        }
    }
}
