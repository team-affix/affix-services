using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace Affix_Center
{
    public partial class PhysicalTokenRegistration : Form
    {
        System.Windows.Forms.Timer checktimer = new System.Windows.Forms.Timer();
        public PhysicalTokenRegistration()
        {
            InitializeComponent();
            checktimer.Interval = 50;
            checktimer.Tick += new EventHandler(checkfortokens);
        }

        private void PhysicalTokenRegistration_Load(object sender, EventArgs e)
        {
            setcolors();
            checktimer.Start();
        }

        private void setcolors()
        {
            if(UserCredentials.color1 == "dark")
            {
                this.BackColor = Color.FromArgb(30, 30, 30);
                lblRegisteringTokens.ForeColor = Color.White;
                pnlStatus.BackgroundImage = Properties.Resources.RegisteringTokensDark1;
            }
        }

        private void checkfortokens(object sender, EventArgs e)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            List<object> physicaltokens = new List<object> { };

            foreach(DriveInfo drive in drives)
            {
                if(File.Exists(drive.Name.Substring(0, 2) + "\\atk\\afx.tok"))
                {
                    try
                    {
                        if (!UserCredentials.filesEncrypted)
                        {
                            physicaltokens.Add(convertbytetolist(File.ReadAllBytes(drive.Name + "\\atk\\afx.tok")) as List<object>);
                        }
                        else
                        {
                            physicaltokens.Add(convertbytetolist(DecryptByteArray(File.ReadAllBytes(drive.Name + "\\atk\\afx.tok"), UserCredentials.LFA)) as List<object>);
                            //MessageBox.Show("");
                        }
                    }
                    catch { }
                }
            }

            List<object> finalphysicaltokens = new List<object> { };
            foreach (List<object> onlinetoken in UserCredentials.IFA2details[0] as List<object>)
            {
                foreach (List<object> token in physicaltokens)
                {
                    if (onlinetoken[0] as string == token[0] as string)
                    {
                        finalphysicaltokens.Add(new List<object> { token, onlinetoken });
                        goto continuewithoutreturn;
                    }
                }
                return;
                continuewithoutreturn:;
            }
            try
            {
                byte[] encPermPriKey = UserCredentials.IFA2details[1] as byte[];
                byte[] encVerificationMessage = UserCredentials.IFA2details[2] as byte[];
                byte[] encVerifiedConnectionKey = UserCredentials.IFA2details[3] as byte[];
                

                finalphysicaltokens.Reverse();

                List<object> finalverificationmessagelist = new List<object> { };
                foreach (List<object> token in finalphysicaltokens)
                {
                    try
                    {
                        byte[] decryptedverificationmessage = Decrypt((token[0] as List<object>)[2] as string, (token[1] as List<object>)[1] as byte[]);
                        string finalverificationmessage = Encoding.UTF8.GetString(decryptedverificationmessage);
                        finalverificationmessagelist.Add(finalverificationmessage);
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }
                
                foreach(string verificationmessage in finalverificationmessagelist)
                {
                    //MessageBox.Show(verificationmessage);
                    encVerifiedConnectionKey = DecryptByteArray(encVerifiedConnectionKey, verificationmessage);
                }
                
                byte[] tfaremovedEncPermPriKey = DecryptByteArray(encPermPriKey, UserCredentials.creds[1] as string);
                byte[] permPriKeyByte = DecryptByteArray(tfaremovedEncPermPriKey, UserCredentials.creds[0] as string);
                string permPriKey = Encoding.UTF8.GetString(permPriKeyByte);
               
                byte[] byteVerificationMessage = Decrypt(permPriKey, encVerificationMessage);
                
                string verificationMessage = Encoding.UTF8.GetString(byteVerificationMessage);


                
                byte[] verifiedConnectionKeyByte = DecryptByteArray(encVerifiedConnectionKey, verificationMessage);
                string verifiedConnectionKey = Encoding.UTF8.GetString(verifiedConnectionKeyByte);

                UserCredentials.verificationKey = verifiedConnectionKey;
                UserCredentials.username = UserCredentials.IFA2details[4] as string;
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
                checktimer.Stop();
                pnlStatus.BackgroundImage = Properties.Resources.SuccessfulTokenRegister;
                new Thread(() =>
                {
                    System.Threading.Thread.Sleep(500);
                    shownotification(nullvoid, "Affix Services response: Welcome, " + UserCredentials.username, Color.Cyan.Name);
                    this.Invoke((MethodInvoker)(() => { this.Dispose(); }));
                }).Start();

            }
            catch(Exception ex)
            {
                checktimer.Stop();
                shownotification(nullvoid, "Affix Services response: Physical tokens read, incorrect credentials." + UserCredentials.username, Color.Coral.Name);
                this.Invoke((MethodInvoker)(() => { this.Dispose(); }));
                return;
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

        private void PhysicalTokenRegistration_Shown(object sender, EventArgs e)
        {
            this.TopMost = true;
        }
    }
}
