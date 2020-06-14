using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aurora.Generalization;
using AffixServices.Communication;
using Aurora.Sequencing;
using System.Drawing;
using System.Security.Cryptography;
using System.Net;
using System.Net.Sockets;
using System.Drawing.Drawing2D;
using Security;

namespace Affix_Center
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            App_Processor.Init_App();
            App_Processor.Start_MainForm();
        }
    }

    public static class App_Vals
    {
        public static string FileName_AccountInfos_Local;
        public static string FileName_AccountInfo_Local;
        public static string FileName_MachineInfo_Local;
        public static string FileName_ProgramColors;
        public static string FileName_TC;


        public static bool App_StartingUp;


        public static System.Windows.Forms.Timer Timer_Update;


        public static MainForm Form_MainForm;
        public static List<Form> Form_ActiveForms;


        public static List<AccountInfo> AccountInfos_Local;
        public static AccountInfo AccountInfo_Local;
        public static AccountInfo AccountInfo_Remote;
        public static MachineInfo MachineInfo_Local;
        public static MachineInfo MachineInfo_Remote;
        public static bool TC_Accepted;


        public static ProgramColors ProgramColors;


        public static Sync<List<AccountInfo>> Sync_AccountInfos_Local;
        public static Sync<AccountInfo> Sync_AccountInfo_Local;
        public static Sync<MachineInfo> Sync_MachineInfo_Local;
        public static Sync<ProgramColors> Sync_ProgramColors;
        public static Sync<bool> Sync_TCAccepted;


        public static Transceiver Transceiver;
        public static TransceiverCallBacks TransceiverCallBacks;
        public static TransceiverSecurity TransceiverSecurity;


        public static Random Random = new Random(DateTime.Now.Hour + DateTime.Now.Day - DateTime.Now.Month * 2);
    }
    public static class AS_Vals
    {
        public static IPEndPoint AS_RemoteEndPoint;


        public static string AS_PublicKey;
    }
    public static class App_Processor
    {
        public static void Export_File(object o, string fileName)
        {
            byte[] Data = o.ToByte();
            File.WriteAllBytes(fileName, Data);
        }
        public static T Import_File<T>(string fileName)
        {
            byte[] Data = File.ReadAllBytes(fileName);
            return Data.To<T>();
        }


        public static void Init_App()
        {
            App_Vals.App_StartingUp = true;
            Configure_App();
            Init_TimerUpdate();
            Init_Syncs();
            Init_FileNames();
            Init_Storage();
            Init_AppValues();
            Init_ASValues();
            Open_Syncs();
            Configure_Updates();
        }
        private static void Init_TimerUpdate()
        {
            App_Vals.Timer_Update = new Timer();
            App_Vals.Timer_Update.Interval = 100;
            App_Vals.Timer_Update.Start();
        }
        private static void Init_Syncs()
        {
            App_Vals.Sync_AccountInfos_Local = new Sync<List<AccountInfo>>(() =>
            { return App_Vals.AccountInfos_Local; }, () =>
            { return App_Processor.Import_File<List<AccountInfo>>(App_Vals.FileName_AccountInfos_Local); }, (Local, Storage) =>
            { App_Processor.Export_File(Local, App_Vals.FileName_AccountInfos_Local); });


            App_Vals.Sync_AccountInfo_Local = new Sync<AccountInfo>(() =>
            { return App_Vals.AccountInfo_Local; }, () =>
            { return App_Processor.Import_File<AccountInfo>(App_Vals.FileName_AccountInfo_Local); }, (Local, Storage) =>
            { App_Processor.Export_File(Local, App_Vals.FileName_AccountInfo_Local); });


            App_Vals.Sync_MachineInfo_Local = new Sync<MachineInfo>(() =>
            { return App_Vals.MachineInfo_Local; }, () =>
            { return App_Processor.Import_File<MachineInfo>(App_Vals.FileName_MachineInfo_Local); }, (Local, Storage) =>
            { App_Processor.Export_File(Local, App_Vals.FileName_MachineInfo_Local); });


            App_Vals.Sync_ProgramColors = new Sync<ProgramColors>(() =>
            { return App_Vals.ProgramColors; }, () =>
            { return App_Processor.Import_File<ProgramColors>(App_Vals.FileName_ProgramColors); }, (Local, Storage) =>
            { App_Processor.Export_File(Local, App_Vals.FileName_ProgramColors); });

            
            App_Vals.Sync_TCAccepted = new Sync<bool>(() =>
            { return App_Vals.TC_Accepted; }, () =>
            { return App_Processor.Import_File<bool>(App_Vals.FileName_TC); }, (Local, Storage) =>
            { App_Processor.Export_File(Local, App_Vals.FileName_TC); });
        }
        private static void Init_FileNames()
        {
            App_Vals.FileName_AccountInfos_Local = "dat\\accounts";
            App_Vals.FileName_AccountInfo_Local = "dat\\account";
            App_Vals.FileName_MachineInfo_Local = "dat\\machine";
            App_Vals.FileName_ProgramColors = "dat\\programColors";
            App_Vals.FileName_TC = "dat\\tc";
        }
        private static void Init_Storage()
        {
            if (!Directory.Exists("dat"))
            {
                Directory.CreateDirectory("dat");
            }
            if (!File.Exists(App_Vals.FileName_ProgramColors))
            {
                App_Processor.Export_File(new ProgramColors(), App_Vals.FileName_ProgramColors);
            }
            if (!File.Exists(App_Vals.FileName_AccountInfos_Local))
            {
                App_Processor.Export_File(new List<AccountInfo> { }, App_Vals.FileName_AccountInfos_Local);
            }
            if (!File.Exists(App_Vals.FileName_AccountInfo_Local))
            {
                App_Processor.Export_File(new AccountInfo(), App_Vals.FileName_AccountInfo_Local);
            }
            if (!File.Exists(App_Vals.FileName_MachineInfo_Local))
            {
                App_Processor.Export_File(new MachineInfo(), App_Vals.FileName_MachineInfo_Local);
            }
            if (!File.Exists(App_Vals.FileName_TC))
            {
                App_Processor.Export_File(false, App_Vals.FileName_TC);
            }
        }
        private static void Init_AppValues()
        {
            App_Vals.ProgramColors = App_Vals.Sync_ProgramColors.GetSource2();
            App_Vals.AccountInfos_Local = App_Vals.Sync_AccountInfos_Local.GetSource2();
            App_Vals.AccountInfo_Local = App_Vals.Sync_AccountInfo_Local.GetSource2();
            App_Vals.MachineInfo_Local = App_Vals.Sync_MachineInfo_Local.GetSource2();
            App_Vals.Form_ActiveForms = new List<Form> { };
            App_Vals.TC_Accepted = App_Processor.Import_File<bool>(App_Vals.FileName_TC);
        }
        private static void Init_ASValues()
        {
            AS_Vals.AS_RemoteEndPoint = new IPEndPoint(IPAddress.Parse("24.10.33.136"), 8090);
            AS_Vals.AS_PublicKey = "<RSAKeyValue><Modulus>4igTaAPOT3l6JrsWHVN5hpOEsKnFYVUKHwJYwcQOCC980QXLW9ogdst0u04LjftZrT7uOx0g82UimO2qXSwVvBOa5A8mptOewj9H3M58qYp34ZBtaX/9gZt9jBX/5g9B96DV3s2Kd8C3AHtKjvaOg2VOK0qad/RhXQEQJM/1mC78Dx9miFNVVcXfIhSvzggIyvGarXusNtTgKdR2VmI7FMP2N8LisNYBGnBmI00daGdZn8SDO7NiA7CeM4/nmj5MFIcox3l0larnkXhYE0eM550TrZ8phbZvaMOn0nfjT4YVkhfd6ECz+TtVxxVZNHswxiCiTtCQ3qotj19BqxUK8lGBxqlLTmMI6nNUUGN8EwrsVSwm7hVkmsfKnnFKZ+U6GbAmPgyDe/4t+gz9emG/7TxAGr/ln/P/hYRNChuLTUA4JpuK6LFw+57qxvSb75EOo5Q6Zl2ORxHWQ+gPrnkRPk8rQL46RLweUeeE8Wnli7yFxAqiaIHhZbWBWGiwci2xcSjfWRrBcRe9W6j1hxCDS9Jf1sn6sIhnwlDr2CEBPI73ikZAV88yLSUUWRSaPWIIpSI+spNd6NWN2arR7dFvaoAv+3kXY7eM/mt4kGscuSlvnjwxa5pLXlEmkcCOizy6bNs6vblqw1ddCgwhrJzkMf/iA6XIldhMU7+u7enOGYk=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        }
        private static void Configure_App()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }
        private static void Open_Syncs()
        {
            App_Vals.Sync_AccountInfo_Local.Open(1000);
            App_Vals.Sync_AccountInfos_Local.Open(1000);
            App_Vals.Sync_MachineInfo_Local.Open(1000);
            App_Vals.Sync_ProgramColors.Open(1000);
            App_Vals.Sync_TCAccepted.Open(100);
        }
        private static void Configure_Updates()
        {
            EventHandler WaitForTCAccepted_ConnectToAffixServices = null;
            WaitForTCAccepted_ConnectToAffixServices = (x, y) =>
            {
                if (App_Vals.TC_Accepted)
                {
                    App_Vals.Timer_Update.Tick -= WaitForTCAccepted_ConnectToAffixServices;
                    Transceiver_Processor.Init_Transceiver();
                }
            };
            App_Vals.Timer_Update.Tick += WaitForTCAccepted_ConnectToAffixServices;
        }


        public static void Start_MainForm()
        {
            Display_MainForm();
        }
        private static void Display_MainForm()
        {
            App_Vals.Form_MainForm = new MainForm();
            App_Vals.Form_ActiveForms.Add(App_Vals.Form_MainForm);
            App_Vals.Form_MainForm.ShowDialog();
        }


        public static bool Get_AccountInfoLocalExists()
        {
            return !App_Vals.AccountInfo_Local.Equals(new AccountInfo());
        }
        public static bool Get_MachineInfoLocalExists()
        {
            return !App_Vals.MachineInfo_Local.Equals(new MachineInfo());
        }
        public static bool Get_AccountInfoRemoteExists()
        {
            return !App_Vals.AccountInfo_Remote.Equals(new AccountInfo());
        }
        public static bool Get_AccountInfosExists()
        {
            return !App_Vals.AccountInfos_Local.Equals(new List<AccountInfo>());
        }


    }
    public static class Transceiver_Processor
    {
        public static void Init_Transceiver()
        {
            Init_TransceiverCallBacks();
            Init_TransceiverSecurity();
            Init_TransceiverSocket();
        }
        private static void Init_TransceiverCallBacks()
        {
            App_Vals.TransceiverCallBacks = new TransceiverCallBacks(CallBack_NewDiscourseCreated);
        }
        private static void Init_TransceiverSecurity()
        {
            App_Vals.TransceiverSecurity = new TransceiverSecurity
            (
                new SeedKeyPair(null, 0, x => { }),
                (TransmissionEnvelope) =>
                {
                    Transmission Transmission = TransmissionEnvelope.Item1;
                    string OutPublicKey = TransmissionEnvelope.Item2;
                    byte[] Result = null;

                    Action Encrypt = null;
                    Action Serialize = () =>
                    {
                        Result = Transmission.ToByte();
                    };
                    Action AddMainLayer = () =>
                    {
                        string MessageKey = Crypt.RandomString_AlphaNumeric(100, App_Vals.Random);
                        byte[] EncryptedMessageKey = Crypt.EncryptRSA(OutPublicKey, MessageKey.ToByte());
                        byte[] EncryptedMessage = Crypt.EncryptECB(Result, MessageKey.ToByte());
                        List<object> FinalExport = new List<object> { EncryptedMessageKey, EncryptedMessage };
                        Result = FinalExport.ToByte();
                    };

                    Encrypt += Serialize;
                    Encrypt += AddMainLayer;
                    Encrypt.Invoke();

                    return Result;
                },
                (Data) =>
                {
                    if(Data.Length == 0)
                    {
                        return null;
                    }
                    string InPrivateKey = App_Vals.MachineInfo_Local.PrivateKey;
                    Transmission Result = null;

                    Action Decrypt = null;
                    Action RemoveMainLayer = () =>
                    {
                        List<object> FinalExport = Data.To<List<object>>();
                        byte[] EncryptedMessageKey = (byte[])FinalExport[0];
                        byte[] EncryptedMessage = (byte[])FinalExport[1];
                        string MessageKey = Crypt.DecryptRSA(InPrivateKey, EncryptedMessageKey).To<string>();
                        Data = Crypt.DecryptECB(EncryptedMessage, MessageKey.ToByte());
                    };
                    Action Deserialize = () =>
                    {
                        Result = Data.To<Transmission>();
                    };

                    Decrypt += RemoveMainLayer;
                    Decrypt += Deserialize;
                    Decrypt.Invoke();

                    return Result;
                }
            );
        }


        public static void Init_TransceiverSocket()
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IAsyncResult result = s.BeginConnect(AS_Vals.AS_RemoteEndPoint, CallBack_TransceiverSocketConnected, s);
            Action WaitForResult = () =>
            {
                bool success = result.AsyncWaitHandle.WaitOne(10000, true);
                if (s.Connected)
                {
                    s.EndConnect(result);
                }
                else
                {
                    s.Close();
                    Init_TransceiverSocket();
                }
            };
            WaitForResult.BeginInvoke(x => { }, null);
        }
        private static void CallBack_TransceiverSocketConnected(IAsyncResult result)
        {
            Socket s = (Socket)result.AsyncState;
            App_Vals.Transceiver = new Transceiver(s, App_Vals.TransceiverCallBacks);
            App_Vals.Transceiver.Security = App_Vals.TransceiverSecurity;
            App_Vals.Transceiver.Open();

            if (s.Connected)
            {
                EventHandler WaitForDisconnect = null;
                WaitForDisconnect = (x, y) =>
                {
                    if (!App_Vals.Transceiver.Socket.Connected)
                    {
                        App_Vals.Timer_Update.Tick -= WaitForDisconnect;
                        Init_TransceiverSocket();
                    }
                };
                App_Vals.Timer_Update.Tick += WaitForDisconnect;
            }
        }


        public static void CallBack_NewDiscourseCreated(Transceiver transceiver, Discourse discourse)
        {
            discourse.HandleTransmission = (TC, DC, TM) =>
            {
                InboundProcessor[(string)TM["function_name"]](Tuple.Create(TC, DC, TM));
            };
        }


        public static Processor<string, Tuple<Transceiver, Discourse, Transmission>> InboundProcessor = new Processor<string, Tuple<Transceiver, Discourse, Transmission>>()
        {

        };
    }

    [Serializable]
    public struct AccountInfo
    {
        public string ID;
        public string Email;
        public string Name;
        public string PermanentAccessToken;
        public ProgramColors ProgramColors;
        public AccountInfo(string Email, string Name, string ID, string PermanentAccessToken, ProgramColors ProgramColors)
        {
            this.Email = Email;
            this.Name = Name;
            this.ID = ID;
            this.PermanentAccessToken = PermanentAccessToken;
            this.ProgramColors = ProgramColors;
        }
    }
    [Serializable]
    public struct MachineInfo
    {
        public string Name;
        public string ID;
        public string PublicKey;
        public string PrivateKey;
        public MachineInfo(string Name, string ID, string PublicKey, string PrivateKey)
        {
            this.Name = Name;
            this.ID = ID;
            this.PublicKey = PublicKey;
            this.PrivateKey = PrivateKey;
        }
    }
    [Serializable]
    public class ProgramColors
    {
        public Color BackColor;
        public Color HUDColor;
        public Color TitleColor;
        public Color SubtitleColor;
        public Color ParagraphColor;
        public Color SelectedColor;
    }
}