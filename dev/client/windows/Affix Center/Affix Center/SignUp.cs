using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    public partial class SignUp : Form
    {
        public static bool signupsuccess { get; set; }
        public SignUp()
        {
            InitializeComponent();
            signupsuccess = false;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if(txtPassword.Text == txtConfirmPassword.Text)
            {
                txtPassword.ForeColor = Color.DodgerBlue;
                txtConfirmPassword.ForeColor = Color.DodgerBlue;
            }
            else
            {
                if (UserCredentials.color1 == "dark")
                {
                    txtPassword.ForeColor = Color.White;
                    txtConfirmPassword.ForeColor = Color.White;
                }
                else
                {
                    txtPassword.ForeColor = Color.Black;
                    txtConfirmPassword.ForeColor = Color.Black;
                }
            }
        }

        private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text == txtConfirmPassword.Text)
            {
                txtPassword.ForeColor = Color.DodgerBlue;
                txtConfirmPassword.ForeColor = Color.DodgerBlue;
            }
            else
            {

                if (UserCredentials.color1 == "dark")
                {
                    txtPassword.ForeColor = Color.White;
                    txtConfirmPassword.ForeColor = Color.White;
                }
                else
                {
                    txtPassword.ForeColor = Color.Black;
                    txtConfirmPassword.ForeColor = Color.Black;
                }
            }
        }

        private void txtTFA_TextChanged(object sender, EventArgs e)
        {
            if (txtTFA.Text == txtConfirmTFA.Text)
            {
                txtTFA.ForeColor = Color.DodgerBlue;
                txtConfirmTFA.ForeColor = Color.DodgerBlue;
            }
            else
            {
                if (UserCredentials.color1 == "dark")
                {
                    txtTFA.ForeColor = Color.White;
                    txtConfirmTFA.ForeColor = Color.White;
                }
                else
                {
                    txtTFA.ForeColor = Color.Black;
                    txtConfirmTFA.ForeColor = Color.Black;
                }
            }
        }

        private void txtConfirmTFA_TextChanged(object sender, EventArgs e)
        {
            if (txtTFA.Text == txtConfirmTFA.Text)
            {
                txtTFA.ForeColor = Color.DodgerBlue;
                txtConfirmTFA.ForeColor = Color.DodgerBlue;
            }
            else
            {
                if(UserCredentials.color1 == "dark")
                {
                    txtTFA.ForeColor = Color.White;
                    txtConfirmTFA.ForeColor = Color.White;
                }
                else
                {
                    txtTFA.ForeColor = Color.Black;
                    txtConfirmTFA.ForeColor = Color.Black;
                }
            }
        }

        private void lblSignUpButton_MouseEnter(object sender, EventArgs e)
        {
            pnlSignUpButton.BackgroundImage = Properties.Resources.SignInButton__1_;
        }

        private void lblSignUpButton_MouseLeave(object sender, EventArgs e)
        {
            pnlSignUpButton.BackgroundImage = Properties.Resources.SignInButton;
        }

        private void lblSignUpButton_Click(object sender, EventArgs e)
        {
            signupsuccess = false;
            this.Hide();
            var f = new Waitform();
            new Thread(() => {
                f.ShowDialog();
            }).Start();
            //signup();
            signupIFA2();
            if (signupsuccess)
            {
                this.Hide();
                var g = new UserVerificationInput();
                g.ShowDialog();
                f = new Waitform();
                new Thread(() => {
                    f.ShowDialog();
                }).Start();
                //verify();
                verifyIFA2();
            }
        }

        private void nullvoid()
        {

        }
        /*
        private void signup()
        {
            string pubkey = File.ReadAllText("dat\\source\\client\\keys\\puK.uri");

            if (UserCredentials.dhkey == null)
            {
                shownotification(nullvoid, "DHC Key not generated; unable to connect. ", Color.Coral.Name);
                Waitform.closeform = true;
                return;
            }
            string availablechars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_";
            string final = "";
            bool perfect = true;
            foreach (char c in txtUsername.Text)
            {
                if (availablechars.Contains(c))
                {
                    final = final + c;
                }
                else
                {
                    perfect = false;
                }
            }
            if (!perfect)
            {
                Waitform.closeform = true;
                this.Show();
                shownotification(nullvoid, "Your username contained special characters. We changed it to: '" + final + "'", "Red");
                return;
            }
            List<object> sendobj = new List<object>() { "signup", final, EncryptByteArray(Encoding.UTF8.GetBytes(txtPassword.Text), txtTFA.Text), txtEmail.Text, pubkey };
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
                    //MessageBox.Show((string)(returnobj[0]));
                    UserCredentials.credentials = EncryptByteArray(Encoding.UTF8.GetBytes(txtPassword.Text), txtTFA.Text);
                    UserCredentials.username = txtUsername.Text;
                    Waitform.closeform = true;
                    signupsuccess = true;
                }
                else
                {
                    MessageBox.Show((string)(returnobj[1]));
                    Waitform.closeform = true;
                    this.Show();
                }
            }
            catch
            {
                Waitform.closeform = true;
                this.Show();
            }
            
        }
        */



        private void signupIFA2()
        {
            string pubkey = UserCredentials.mypublickey;

            if (UserCredentials.dhkey == null)
            {
                shownotification(nullvoid, "DHC Key not generated; unable to connect. ", Color.Coral.Name);
                Waitform.closeform = true;
                return;
            }
            string availablechars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_";
            string final = "";
            bool perfect = true;
            foreach (char c in txtUsername.Text)
            {
                if (availablechars.Contains(c))
                {
                    final = final + c;
                }
                else
                {
                    perfect = false;
                }
            }
            if (!perfect)
            {
                Waitform.closeform = true;
                this.Show();
                shownotification(nullvoid, "Your username contained special characters. We changed it to: '" + final + "'", "Red");
                return;
            }

            //Generating Keys
            List<object> newkeys = Generatetempkeys();
            string tempPriKey = newkeys[0] as string;
            string tempPubKey = newkeys[1] as string;


            //Encrypting Private Key
            byte[] encTempPriKey = EncryptByteArray(Encoding.UTF8.GetBytes(tempPriKey), txtPassword.Text);
            byte[] fullEncTempPriKey = EncryptByteArray(encTempPriKey, txtTFA.Text);




            List<object> sendobj = new List<object>() { "signupIFA2", final, fullEncTempPriKey, EncryptByteArray(Encoding.UTF8.GetBytes(txtPassword.Text), txtTFA.Text), tempPubKey, txtEmail.Text, pubkey };
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
                    //MessageBox.Show((string)(returnobj[0]));
                    UserCredentials.verificationKey = returnobj[1] as string;
                    UserCredentials.username = txtUsername.Text;
                    Waitform.closeform = true;
                    signupsuccess = true;
                }
                else
                {
                    MessageBox.Show((string)(returnobj[1]));
                    Waitform.closeform = true;
                    this.Show();
                }
            }
            catch
            {
                Waitform.closeform = true;
                this.Show();
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






        /*
        private void verify()
        {
            string pubkey = File.ReadAllText("dat\\source\\client\\keys\\puK.uri");

            string availablechars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_";
            string final = "";
            bool perfect = true;
            foreach (char c in txtUsername.Text)
            {
                if (availablechars.Contains(c))
                {
                    final = final + c;
                }
                else
                {
                    perfect = false;
                }
            }
            if (!perfect)
            {
                Waitform.closeform = true;
                this.Show();
                return;
            }
            List<object> sendobj = new List<object>() { "verify", final, EncryptByteArray(Encoding.UTF8.GetBytes(txtPassword.Text), txtTFA.Text), txtEmail.Text, pubkey, UserCredentials.confirmationcode };
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
                    //MessageBox.Show((string)(returnobj[0]));
                    UserCredentials.credentials = EncryptByteArray(Encoding.UTF8.GetBytes(txtPassword.Text), txtTFA.Text);
                    UserCredentials.username = txtUsername.Text;
                    Waitform.closeform = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show((string)(returnobj[1]));
                    shownotification(null, "Affix Services response: incorrect credentials.", Color.Red.Name);
                    
                    Waitform.closeform = true;
                    this.Show();
                }
            }
            catch
            {
                Waitform.closeform = true;
                this.Show();
            }
            
        }
        */

        private void verifyIFA2()
        {
            string pubkey = UserCredentials.mypublickey;

            string availablechars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_";
            string final = "";
            bool perfect = true;
            foreach (char c in txtUsername.Text)
            {
                if (availablechars.Contains(c))
                {
                    final = final + c;
                }
                else
                {
                    perfect = false;
                }
            }
            if (!perfect)
            {
                Waitform.closeform = true;
                this.Show();
                return;
            }
            List<object> sendobj = new List<object>() { "verifyIFA2", final, UserCredentials.verificationKey, EncryptByteArray(Encoding.UTF8.GetBytes(txtPassword.Text), txtTFA.Text), txtEmail.Text, pubkey, UserCredentials.confirmationcode };
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
                    //MessageBox.Show((string)(returnobj[0]));
                    UserCredentials.username = txtUsername.Text;
                    UserCredentials.signedin = true;
                    Waitform.closeform = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show((string)(returnobj[1]));
                    shownotification(null, "Affix Services response: error encountered.", Color.Red.Name);

                    Waitform.closeform = true;
                    this.Show();
                }
            }
            catch
            {
                Waitform.closeform = true;
                this.Show();
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
            using (var rsa = new RSACryptoServiceProvider(4096))
            {
                rsa.PersistKeyInCsp = false;
                string pubKeyString = rsa.ToXmlString(false);
                File.WriteAllText(pubKeyFileName, pubKeyString);
                string priKeyString = rsa.ToXmlString(true);
                File.WriteAllText(priKeyFileName, priKeyString);
            }
        }


        static List<object> Generatetempkeys()
        {
            using (var rsa = new RSACryptoServiceProvider(4096))
            {
                rsa.PersistKeyInCsp = false;
                string pubKeyString = rsa.ToXmlString(false);
                string priKeyString = rsa.ToXmlString(true);
                return new List<object> { priKeyString, pubKeyString };
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

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
        }

        private void SignUp_Load(object sender, EventArgs e)
        {
        }

        private void SignUp_Shown(object sender, EventArgs e)
        {
            if (UserCredentials.color1 == "dark")
            {
                this.BackColor = Color.FromArgb(30, 30, 30);
                lblLoadingDesc.ForeColor = Color.White;
                lblSignInTitle.ForeColor = Color.LightGray;
                txtUsername.BackColor = Color.FromArgb(30, 30, 30);
                txtPassword.BackColor = Color.FromArgb(30, 30, 30);
                txtTFA.BackColor = Color.FromArgb(30, 30, 30);
                txtConfirmPassword.BackColor = Color.FromArgb(30, 30, 30);
                txtConfirmTFA.BackColor = Color.FromArgb(30, 30, 30);
                txtEmail.BackColor = Color.FromArgb(30, 30, 30);

                txtUsername.ForeColor = Color.White;
                txtPassword.ForeColor = Color.White;
                txtConfirmPassword.ForeColor = Color.White;
                txtTFA.ForeColor = Color.White;
                txtConfirmTFA.ForeColor = Color.White;
                txtEmail.ForeColor = Color.White;



                lblUsername.ForeColor = Color.White;
                lblPassword.ForeColor = Color.White;
                label1.ForeColor = Color.White;
                label2.ForeColor = Color.White;
                label3.ForeColor = Color.White;
                label4.ForeColor = Color.White;
                lblEmail.ForeColor = Color.White;
                lblTFA.ForeColor = Color.White;


            }
        }
    }
}
