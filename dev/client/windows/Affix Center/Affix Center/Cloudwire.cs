using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Affix_Center
{
    public partial class Cloudwire : Form
    {
        TcpClient client;
        public static List<string> filelocationlist = new List<string> { };
        public static List<object> filereceivelist = new List<object> { };
        public Cloudwire()
        {
            InitializeComponent();
            client = new TcpClient();
        }

        private void Cloudwire_Load(object sender, EventArgs e)
        {
            lblIPv4.Text = GetLocalIPAddress();
        }


        private void connectmachines(string ipv4)
        {
            try
            {
                IPEndPoint IP_End = new IPEndPoint(IPAddress.Parse(ipv4), 8070);
                try
                {
                    client.Connect(IP_End);
                    if (client.Connected)
                    {
                        System.Threading.Thread.Sleep(100);
                        Waitform.closeform = true;
                        shownotification(nullvoid, "Success", "DodgerBlue");
                    }
                }
                catch
                {
                    System.Threading.Thread.Sleep(100);
                    Waitform.closeform = true;
                    shownotification(nullvoid, "Unsuccessful", "Coral");
                }
            }
            catch
            {
                System.Threading.Thread.Sleep(100);
                Waitform.closeform = true;
                shownotification(nullvoid, "Invalid IPv4 Address", "Coral");
            }
            
        }

        private void sendbytes(byte[] sendbytes)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                //byte[] buffer = new byte[client.SendBufferSize];
                stream.Write(BitConverter.GetBytes(sendbytes.Length), 0, 4);
                //new Thread(() => { MessageBox.Show(sendbytes.Length.ToString()); }).Start();

                int sent = 0;
                int remaining = sendbytes.Length - sent;
                while (sent < sendbytes.Length)
                {
                    byte[] sendbuf = new byte[4];
                    if (remaining <= client.SendBufferSize)
                    {
                        int size = remaining;
                        sendbuf = SubArray(sendbytes, sent, size);
                    }
                    else
                    {
                        int size = client.SendBufferSize;
                        sendbuf = SubArray(sendbytes, sent, size);

                    }
                    stream.Write(sendbuf, 0, sendbuf.Length);
                    sent = sent + sendbuf.Length;
                    remaining = sendbytes.Length - sent;
                }
                System.Threading.Thread.Sleep(100);
                Waitform.closeform = true;
            }
            catch
            {
                System.Threading.Thread.Sleep(100);
                Waitform.closeform = true;
                shownotification(nullvoid, "Error Encountered", "Coral");
            }
        }



        private byte[] getbytes()
        {
            try
            {
                Waitform.waitformtitle = "Receiving Bytes; Receiving Size.";
                NetworkStream stream = client.GetStream();
                byte[] sizebuf = new byte[4];
                stream.Read(sizebuf, 0, sizebuf.Length);
                int size = BitConverter.ToInt32(sizebuf, 0);
                int totalsize = size;
                //Waitform.waitformdesc = "Progress is being made, captain!";
                if (size == 0)
                {
                    return null;
                }
                MemoryStream ms = new MemoryStream();
                while (size > 0)
                {
                    //new Thread(() => { MessageBox.Show(ms.ToArray().Length.ToString() + " " + totalsize.ToString()); }).Start();
                    byte[] buffer;
                    if (size < client.ReceiveBufferSize)
                    {
                        buffer = new byte[size];
                    }
                    else
                    {
                        buffer = new byte[client.ReceiveBufferSize];
                    }
                    int rec = stream.Read(buffer, 0, buffer.Length);
                    size -= rec;
                    ms.Write(buffer, 0, rec);
                    //MessageBox.Show((ms.ToArray().Length / totalsize).ToString());
                    Waitform.waitformtitle = "Receiving Bytes; " + (ms.ToArray().Length / totalsize).ToString() + "%";
                }
                return ms.ToArray();
            }
            catch
            {
                System.Threading.Thread.Sleep(100);
                Waitform.closeform = true;
                shownotification(nullvoid, "Error Encountered", "Coral");
                return null;
            }

        }

        byte[] SubArray(byte[] data, int index, int length)
        {
            byte[] result = new byte[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        private void nullvoid()
        {

        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        private void listenforclient()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 8070);
            try
            {
                listener.Start();
                client = listener.AcceptTcpClient();

                System.Threading.Thread.Sleep(100);
                Waitform.closeform = true;
                shownotification(nullvoid, "Connection Established", "DodgerBlue");
            }
            catch
            {
                System.Threading.Thread.Sleep(100);
                Waitform.closeform = true;
                shownotification(nullvoid, "Unable To Connect", "Coral");
            }
        }

        private void txtIPAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                new Thread(() => {
                    var f = new Waitform();
                    f.ShowDialog();
                    Waitform.exitclosesall = false;
                }).Start();
                connectmachines(txtIPAddress.Text);
                
                var d = new LocalFileTransfer();
                d.ShowDialog();

                new Thread(() => {
                    var f = new Waitform();
                    f.ShowDialog();
                    Waitform.exitclosesall = false;
                }).Start();

                try
                {
                    List<object> fileinfo = new List<object> { };
                    foreach (string s in filelocationlist)
                    {
                        byte[] filebytes = File.ReadAllBytes(s);
                        string[] filenamesplit = s.Split('\\');
                        string localfilename = filenamesplit.Last();
                        fileinfo.Add(new List<object> { localfilename, filebytes });
                    }

                    byte[] sendobjbytes = convertlisttobyte(fileinfo);

                    sendbytes(sendobjbytes);
                    System.Threading.Thread.Sleep(100);
                    Waitform.closeform = true;
                    shownotification(nullvoid, "File transfer completed", "DodgerBlue");
                }
                catch
                {
                    System.Threading.Thread.Sleep(100);
                    Waitform.closeform = true;
                    shownotification(nullvoid, "Error", "Coral");
                }



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

        private void lblListen_MouseEnter(object sender, EventArgs e)
        {
            pnlListen.BackgroundImage = Properties.Resources.ListenSelected;
        }

        private void lblListen_MouseLeave(object sender, EventArgs e)
        {
            pnlListen.BackgroundImage = Properties.Resources.Listen;
        }

        private void lblListen_Click(object sender, EventArgs e)
        {
            new Thread(() => {
                var f = new Waitform();
                f.ShowDialog();
                Waitform.exitclosesall = false;
            }).Start();
            listenforclient();

            
            System.Threading.Thread.Sleep(100);

            new Thread(() => {
                var g = new Waitform();
                g.ShowDialog();
                Waitform.exitclosesall = false;
            }).Start();
            
            try
            {
                byte[] sendobjbyte = getbytes();
                Waitform.closeform = true;
                List<object> fileinfo = convertbytetolist(sendobjbyte) as List<object>;

                filereceivelist = fileinfo;

                var choosefiles = new FileReceive();
                choosefiles.ShowDialog();
                foreach(List<object> file in filereceivelist)
                {
                    if (!Directory.Exists("dat\\person\\client\\files\\local"))
                    {
                        Directory.CreateDirectory("dat\\person\\client\\files\\local");
                    }
                    File.WriteAllBytes("dat\\person\\client\\files\\local\\" + (file[0] as string), (file[1] as byte[]));
                    Process.Start("dat\\person\\client\\files\\local");
                }

                System.Threading.Thread.Sleep(100);
                Waitform.closeform = true;
                shownotification(nullvoid, "File transfer completed", "DodgerBlue");
            }
            catch(Exception ex)
            {
                System.Threading.Thread.Sleep(100);
                Waitform.closeform = true;
                shownotification(nullvoid, "Error", "Coral");
                MessageBox.Show(ex.ToString());
            }


        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                new Thread(() => {
                    var f = new Waitform();
                    f.ShowDialog();
                    Waitform.exitclosesall = false;
                }).Start();
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
