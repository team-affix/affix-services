using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aurora.Generalization;
using AffixServices.Communication;
using AffixServices.Data;
using Aurora.Sequencing;
using System.Drawing;
using System.Security.Cryptography;
using System.Net;
using System.Net.Sockets;
using System.Drawing.Drawing2D;
using Security;
using System.Threading;
using System.Diagnostics;
using Affix_Center.Classes;
using Affix_Center.Forms;
using Affix_Center.Pages;
using Aurora.Networking;
using Affix_Center.CustomControls;

namespace Affix_Center
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            App.Vals.void_Init();
            App.Methods.void_Start();
        }
    }

    public static class App
    {
        public static class Vals
        {
            public static void void_Init()
            {
                #region Files
                string[] ExpectedDirectories = new string[]
                {
                    "dat\\",
                };
                for(int i = 0; i < ExpectedDirectories.Length; i++)
                {
                    Directory.CreateDirectory(ExpectedDirectories[i]);
                }
                Dictionary<string, Action> FilesNotFound = new Dictionary<string, Action>
                {
                    [string_FileNameListAccount] = () => { Methods.void_ExportFile(new List<Account> { }, string_FileNameListAccount); },
                    [string_FileNameMachineLocal] = () => { Methods.void_ExportFile(new Machine { }, string_FileNameMachineLocal); },
                    [string_FileNameMachineSecurityLocal] = () => { Methods.void_ExportFile(new MachineSecurity(), string_FileNameMachineSecurityLocal); },
                    [string_FileNameProgramColors] = () => { Methods.void_ExportFile(ProgramColors_Default, string_FileNameProgramColors); },
                    [string_FileNameTC] = () => { Methods.void_ExportFile(false, string_FileNameTC); },
                };
                for(int i = 0; i < FilesNotFound.Count; i++)
                {
                    if (!File.Exists(FilesNotFound.ElementAt(i).Key))
                    {
                        FilesNotFound.ElementAt(i).Value();
                    }
                }
                #endregion
                #region Import
                List_Account = Methods.T_ImportFile<List<Account>>(string_FileNameListAccount);
                Machine_Active = Methods.T_ImportFile<Machine>(string_FileNameMachineLocal);
                MachineSecurity_Active = Methods.T_ImportFile<MachineSecurity>(string_FileNameMachineSecurityLocal);
                ProgramColors_Active = Methods.T_ImportFile<ProgramColors>(string_FileNameProgramColors);
                bool_TCAccepted = Methods.T_ImportFile<bool>(string_FileNameTC);
                int_TCAcceptedPreviousHash = int_TCAcceptedCurrentHash;
                int_ProgramColorsActivePreviousHash = int_ProgramColorsActiveCurrentHash;
                int_MachineActivePreviousHash = int_MachineActiveCurrentHash;
                List_AccountPrevious = List_Account.Clone();
                int_AccountAuthenticatedPreviousHash = int_AccountAuthenticatedCurrentHash;
                int_MachineAuthenticatedPreviousHash = int_MachineAuthenticatedHash;
                #endregion
                #region Timer
                Action_TimerUpdateCallBack += () => { int_TimerUpdateTickValue++; };
                Action_TimerUpdateCallBack += Methods.void_RefreshPendingItems;
                Action_TimerUpdateCallBack += Methods.void_RefreshCallBackConditions;
                #endregion
                #region Actions
                Action_TCAcceptedChanged += () => { Methods.void_ExportFile(bool_TCAccepted, string_FileNameTC); };
                Action_MachineActiveChanged += () => { Methods.void_ExportFile(Machine_Active, string_FileNameMachineLocal); };
                Action_MachineSecurityActiveChanged += () => { Methods.void_ExportFile(MachineSecurity_Active, string_FileNameMachineSecurityLocal); };

                Action CreateRSAKeys = null;
                CreateRSAKeys = () =>
                {
                    Action_RSAKeysCreationPending -= CreateRSAKeys;
                    Methods.void_CreateRSAKeys();
                };
                Action_RSAKeysCreationPending += CreateRSAKeys;

                Action ConnectTransceiver = null;
                ConnectTransceiver = () =>
                {
                    if (bool_TCAccepted)
                    {
                        Action_TransceiverConnectedPending -= ConnectTransceiver;
                        Methods.void_InitTransceiver();
                    }
                };
                Action_TransceiverConnectedPending += ConnectTransceiver;

                Action SetUpSeedKeyPair = null;
                SetUpSeedKeyPair = () =>
                {
                    if(bool_TCAccepted && bool_TransceiverConnected && bool_RSAKeysCreated)
                    {
                        Action_TimerUpdateCallBack -= SetUpSeedKeyPair;
                        Methods.void_GetSeedKeyPairSeed(flags => 
                        {

                        }, Seed =>
                        {
                            Vals.TransceiverSecurity.SeedKeyPair = new SeedKeyPair(Seed, x =>
                            {
                                byte[] NewVal = (Vals.TransceiverSecurity.SeedKeyPair.Seed + x).ToByte();
                                return Crypt.EncryptECB(NewVal, NewVal);
                            });
                        });
                    }
                };
                Action_TimerUpdateCallBack += SetUpSeedKeyPair;

                Action AuthenticateMachine = null;
                AuthenticateMachine = () =>
                {
                    if (bool_TCAccepted && bool_TransceiverConnected && bool_RSAKeysCreated && bool_SeedKeyPairIsSetUp && bool_MachineRegistered)
                    {
                        Action_TimerUpdateCallBack -= AuthenticateMachine;
                        Methods.void_GetMachineAccessToken(flags => 
                        { 
                        
                        }, encryptedAccessToken =>
                        {
                            try
                            {
                                string accessToken = Crypt.DecryptRSA(Vals.MachineSecurity_Active.PrivateKey, encryptedAccessToken).To<string>();
                                Methods.void_AuthenticateMachine(accessToken, flags =>
                                { }, () =>
                                { });
                            }
                            catch (NullReferenceException)
                            {

                            }
                        });
                    }
                };
                Action_TimerUpdateCallBack += AuthenticateMachine;

                Action StreamGetMachine = () =>
                {
                    if (bool_MachineAuthenticated)
                    {
                        Methods.void_StreamGetMachine(flags =>
                        {

                        }, () =>
                        {

                        }, Machine =>
                        {
                            Machine_Active = Machine;
                            bool_MachineRetrieved = true;
                        });
                    }
                    else
                    {
                        bool_MachineRetrieved = false;
                    }
                };
                Action_MachineAuthenticatedChanged += StreamGetMachine;

                Action StreamGetAccount = () =>
                {
                    if (bool_AccountAuthenticated)
                    {
                        Methods.void_StreamGetAccount(flags =>
                        {

                        }, () =>
                        {

                        }, Account =>
                        {
                            Account_Active = Account;
                            bool_AccountRetrieved = true;
                        });
                    }
                    else
                    {
                        bool_AccountRetrieved = false;
                    }
                };
                Action_AccountAuthenticatedChanged += StreamGetAccount;

                Action ProgramColorsChanged = () =>
                {
                    RoundedPanel_HoverInfo.BackColor = App.Vals.ProgramColors_Active.HUDColor1;
                    RoundedPanel_HoverInfo.ForeColor = App.Vals.ProgramColors_Active.HUDColor1;
                    Label_HoverInfo.ForeColor = App.Vals.ProgramColors_Active.TitleColor;
                };
                Action_ProgramColorsChanged += ProgramColorsChanged;

                #endregion
            }

            public static bool bool_TCAccepted = false;
            public static bool bool_AppStartingUp = false;
            public static bool bool_CreatingRSAKeys = false;
            public static bool bool_RSAKeysCreated { get { return MachineSecurity_Active.PrivateKey != null && MachineSecurity_Active.PublicKey != null; } }
            public static bool bool_RegisteringMachine = false;
            public static bool bool_RegisteringAccount = false;
            public static bool bool_MachineRegistered { get { return Machine_Active.string_IdentificationID != null; } }
            public static bool bool_MachineAuthenticated = false;
            public static bool bool_AccountAuthenticated = false;
            public static bool bool_TransceiverConnected { get { return Transceiver != null && Transceiver.Socket != null && Transceiver.Socket.Connected; } }
            public static bool bool_SeedKeyPairIsSetUp { get { return TransceiverSecurity.SeedKeyPair.Seed != null; } }
            public static bool bool_MachineRetrieved = false;
            public static bool bool_AccountRetrieved = false;
            public static bool bool_ProgramColorsActiveChanged { get { return int_ProgramColorsActivePreviousHash != int_ProgramColorsActiveCurrentHash; } }
            public static bool bool_TCAcceptedChanged { get { return int_TCAcceptedPreviousHash != int_TCAcceptedCurrentHash; } }
            public static bool bool_MachineActiveChanged { get { return int_MachineActivePreviousHash != int_MachineActiveCurrentHash; } }
            public static bool bool_AccountActiveChanged { get { return int_AccountActivePreviousHash != int_AccountActiveCurrentHash; } }
            public static bool bool_ListAccountChanged { get { return List_Account.ToByte().SequenceEqual(List_AccountPrevious.ToByte()); } }
            public static bool bool_MachineAuthenticatedChanged { get { return int_MachineAuthenticatedPreviousHash != int_MachineAuthenticatedHash; } }
            public static bool bool_AccountAuthenticatedChanged { get { return int_AccountAuthenticatedPreviousHash != int_AccountAuthenticatedCurrentHash; } }
            public static bool bool_MachineSecurityActiveChanged { get { return !MachineSecurity_ActivePrevious.ToByte().SequenceEqual(MachineSecurity_Active.ToByte()); } }
            public static bool bool_AvailablePagesChanged { get { return !List_AvailablePages.ToByte().SequenceEqual(List_AvailablePagesPrevious.ToByte()); } }
            public static System.Threading.Timer Timer_Update = new System.Threading.Timer(Methods.void_TimerUpdateCallBack, null, 0, 100);
            public static string string_FileNameListAccount = "dat\\accounts";
            public static string string_FileNameMachineLocal = "dat\\machine";
            public static string string_FileNameMachineSecurityLocal = "dat\\machineSecurity";
            public static string string_FileNameProgramColors = "dat\\programColors";
            public static string string_FileNameTC = "dat\\tc";
            public static string string_AffixServicesPublicKey = "<RSAKeyValue><Modulus>4igTaAPOT3l6JrsWHVN5hpOEsKnFYVUKHwJYwcQOCC980QXLW9ogdst0u04LjftZrT7uOx0g82UimO2qXSwVvBOa5A8mptOewj9H3M58qYp34ZBtaX/9gZt9jBX/5g9B96DV3s2Kd8C3AHtKjvaOg2VOK0qad/RhXQEQJM/1mC78Dx9miFNVVcXfIhSvzggIyvGarXusNtTgKdR2VmI7FMP2N8LisNYBGnBmI00daGdZn8SDO7NiA7CeM4/nmj5MFIcox3l0larnkXhYE0eM550TrZ8phbZvaMOn0nfjT4YVkhfd6ECz+TtVxxVZNHswxiCiTtCQ3qotj19BqxUK8lGBxqlLTmMI6nNUUGN8EwrsVSwm7hVkmsfKnnFKZ+U6GbAmPgyDe/4t+gz9emG/7TxAGr/ln/P/hYRNChuLTUA4JpuK6LFw+57qxvSb75EOo5Q6Zl2ORxHWQ+gPrnkRPk8rQL46RLweUeeE8Wnli7yFxAqiaIHhZbWBWGiwci2xcSjfWRrBcRe9W6j1hxCDS9Jf1sn6sIhnwlDr2CEBPI73ikZAV88yLSUUWRSaPWIIpSI+spNd6NWN2arR7dFvaoAv+3kXY7eM/mt4kGscuSlvnjwxa5pLXlEmkcCOizy6bNs6vblqw1ddCgwhrJzkMf/iA6XIldhMU7+u7enOGYk=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            public const string string_HomePageKey = "Home";
            public const string string_SettingsPageKey = "Settings";
            public const string string_HostPageKey = "Host";
            public const string string_MachineSettingsPageKey = "Machine Settings";
            public const string string_AccountSettingsPageKey = "Account Settings";
            public const string string_AccountToMachineP2PContracts = "This Account-> Machine P2P Contracts";
            public const string string_AccountToAccountP2PContracts = "This Account-> Account P2P Contracts";
            public const string string_MachineToAccountP2PContracts = "This Machine-> Account P2P Contracts";
            public const string string_AddAccountToMachineP2PContract = "Add";
            public const string string_AddAccountToAccountP2PContract = "Add";
            public const string string_AddMachineToAccountP2PContract = "Add";
            public const string string_AcceptContract = "Accept";
            public const string string_DenyContract = "Deny";
            public const string string_Disassociate = "Disassociate";
            public static int int_TimerUpdateTickValue = 0;
            public static int int_ProgramColorsActivePreviousHash;
            public static int int_ProgramColorsActiveCurrentHash { get { return ProgramColors_Active.GetHashCode(); } }
            public static int int_TCAcceptedPreviousHash;
            public static int int_TCAcceptedCurrentHash { get { return bool_TCAccepted.GetHashCode(); } }
            public static int int_MachineActivePreviousHash;
            public static int int_MachineActiveCurrentHash { get { return Machine_Active.GetHashCode(); } }
            public static int int_AccountActivePreviousHash;
            public static int int_AccountActiveCurrentHash { get { return Account_Active.GetHashCode(); } }
            public static int int_AccountAuthenticatedPreviousHash;
            public static int int_AccountAuthenticatedCurrentHash { get { return bool_AccountAuthenticated.GetHashCode(); } }
            public static int int_MachineAuthenticatedPreviousHash;
            public static int int_MachineAuthenticatedHash { get { return bool_MachineAuthenticated.GetHashCode(); } }
            public static List<string> List_AvailablePagesPrevious = new List<string> { };
            public static List<string> List_AvailablePages = new List<string> { };
            public static List<Account> List_Account = new List<Account> { };
            public static List<Account> List_AccountPrevious = new List<Account> { };
            public static List<Account> List_AccountRemote = new List<Account> { };
            public static Account Account_Active = new Account { };
            public static Account Account_ActivePrevious = new Account { };
            public static Machine Machine_Active = new Machine { };
            public static Machine Machine_ActivePrevious = new Machine { };
            public static MachineSecurity MachineSecurity_Active = new MachineSecurity { };
            public static MachineSecurity MachineSecurity_ActivePrevious = new MachineSecurity { };
            public static ProgramColors ProgramColors_Active = new ProgramColors { };
            public static ProgramColors ProgramColors_Dark = new ProgramColors(Color.FromArgb(20, 20, 20), Color.FromArgb(40, 40, 40), Color.FromArgb(50,50,50), Color.FromArgb(60, 60, 60), Color.White, Color.DodgerBlue, Color.DimGray, Color.DodgerBlue, Color.DimGray);
            public static ProgramColors ProgramColors_Bright = new ProgramColors(Color.LightGray, Color.FromArgb(220,220,220), Color.FromArgb(200,200,200), Color.FromArgb(190,190,190), Color.FromArgb(100, 100, 100), Color.DeepSkyBlue, Color.DimGray, Color.DeepSkyBlue, Color.DimGray);
            public static ProgramColors ProgramColors_Default { get { return ProgramColors_Dark; } }
            public static Form_Main Form_Main = new Form_Main();
            public static Page_TC Page_TC = new Page_TC();
            public static Page_RegisterMachine Page_RegisterMachine = new Page_RegisterMachine();
            public static Page_Loading Page_Loading = new Page_Loading();
            public static Page_AccountUnauthenticated Page_AccountUnauthenticated = new Page_AccountUnauthenticated();
            public static Page_AuthenticateAccount Page_AuthenticateAccount = new Page_AuthenticateAccount();
            public static Page_RegisterAccount Page_RegisterAccount = new Page_RegisterAccount();
            public static Page_ClientHome Page_ClientHome = new Page_ClientHome();
            public static Page_AddAccountToMachineP2PContract Page_AddAccountToMachineP2PContract = new Page_AddAccountToMachineP2PContract();
            public static Page_AddAccountToAccountP2PContract Page_AddAccountToAccountP2PContract = new Page_AddAccountToAccountP2PContract();
            public static Page_Settings Page_Settings = new Page_Settings();
            public static Page_MachineSettings Page_MachineSettings = new Page_MachineSettings();
            public static Page_AccountSettings Page_AccountSettings = new Page_AccountSettings();
            public static Page_Host Page_Host = new Page_Host();
            public static Dialog_LeftSideBar Dialog_LeftSideBar = new Dialog_LeftSideBar();
            public static RoundedPanel RoundedPanel_HoverInfo = new RoundedPanel() { Size = new Size(150, 40), BorderWidth = 2, Radius = 5 };
            public static Label Label_HoverInfo = new Label() { Font = new Font("Segoe UI", 10), AutoSize = false, Size = new Size(125, 30), TextAlign = ContentAlignment.MiddleCenter };
            public static Transceiver Transceiver = new Transceiver(null, TransceiverCallBacks);
            public static TransceiverCallBacks TransceiverCallBacks = new TransceiverCallBacks(Methods.void_CallBackNewDiscourseCreated);
            public static TransceiverSecurity TransceiverSecurity = new TransceiverSecurity(new SeedKeyPair(null, x => { return null; }), Methods.byte_EncryptTransmission, Methods.Transmission_DecryptTransmission);
            public static IPEndPoint IPEndPoint_AffixServices = new IPEndPoint(IPAddress.Parse("24.10.33.136"), 8090);
            public static IPEndPoint IPEndPoint_AffixReturner0 = new IPEndPoint(IPAddress.Parse("24.10.33.136"), 8010);
            public static IPEndPoint IPEndPoint_AffixReturner1 = new IPEndPoint(IPAddress.Parse("76.105.35.171"), 8010);
            public static Random Random = new Random(DateTime.Now.Hour + DateTime.Now.Day - DateTime.Now.Month * 2);
            //Recurrent
            public static Action Action_TimerUpdateCallBack = () => { };
            public static Action Action_TCPending = () => { };
            public static Action Action_TCNotPending = () => { };
            public static Action Action_MachineRegistryPending = () => { };
            public static Action Action_MachineRegistryNotPending = () => { };
            public static Action Action_TransceiverConnectedPending = () => { };
            public static Action Action_TransceiverConnectedNotPending = () => { };
            public static Action Action_RSAKeysCreationPending = () => { };
            public static Action Action_RSAKeysCreationNotPending = () => { };
            public static Action Action_MachineAuthenticationPending = () => { };
            public static Action Action_MachineAuthenticationNotPending = () => { };
            public static Action Action_AccountAuthenticationPending = () => { };
            public static Action Action_AccountAuthenticationNotPending = () => { };
            public static Action Action_SeedKeyPairIsSetUpPending = () => { };
            public static Action Action_SeedKeyPairIsSetUpNotPending = () => { };
            //SingleUse
            public static Action Action_ProgramColorsChanged = () => { };
            public static Action Action_TCAcceptedChanged = () => { };
            public static Action Action_MachineActiveChanged = () => { };
            public static Action Action_AccountActiveChanged = () => { };
            public static Action Action_ListAccountChanged = () => { };
            public static Action Action_MachineAuthenticatedChanged = () => { };
            public static Action Action_MachineSecurityActiveChanged = () => { };
            public static Action Action_AccountAuthenticatedChanged = () => { };
            public static Action Action_AvailablePagesChanged = () => { };
        }

        public static class Methods
        {
            public static void void_Start()
            {
                int msTimeout = 100;
                Thread t = new Thread(() => { Vals.Form_Main.ShowDialog(); while (true) { Thread.Sleep(msTimeout); } });
                t.Start();
                while (!Vals.Form_Main.IsHandleCreated) { Thread.Sleep(msTimeout); }
                if (!Vals.bool_TCAccepted)
                {
                    Vals.Form_Main.Invoke((MethodInvoker)(() => { Vals.Form_Main.dsp.Display(Vals.Page_TC); }));
                }
                while (!Vals.bool_TCAccepted) { Thread.Sleep(msTimeout); }
                if (!Vals.bool_RSAKeysCreated || !Vals.bool_TransceiverConnected || !Vals.bool_SeedKeyPairIsSetUp)
                {
                    Vals.Form_Main.Invoke((MethodInvoker)(() => { Vals.Form_Main.dsp.Display(Vals.Page_Loading); }));
                }
                while (!Vals.bool_RSAKeysCreated || !Vals.bool_TransceiverConnected || !Vals.bool_SeedKeyPairIsSetUp) { Thread.Sleep(msTimeout); }
                if (!Vals.bool_MachineRegistered)
                {
                    Vals.Form_Main.Invoke((MethodInvoker)(() => { Vals.Form_Main.dsp.Display(Vals.Page_RegisterMachine); }));
                }
                while (!Vals.bool_MachineRegistered) { Thread.Sleep(msTimeout); }
                if (!Vals.bool_MachineAuthenticated)
                {
                    Vals.Form_Main.Invoke((MethodInvoker)(() => { Vals.Form_Main.dsp.Display(Vals.Page_Loading); }));
                }
                while (!Vals.bool_MachineAuthenticated) { Thread.Sleep(msTimeout); }
                if (!Vals.bool_AccountAuthenticated)
                {
                    Vals.Form_Main.Invoke((MethodInvoker)(() => { Vals.Form_Main.dsp.Display(Vals.Page_AccountUnauthenticated); }));
                }
                while (!Vals.bool_AccountAuthenticated) { Thread.Sleep(msTimeout); }
                if (!Vals.bool_MachineRetrieved || !Vals.bool_AccountRetrieved)
                {
                    Vals.Form_Main.Invoke((MethodInvoker)(() => { Vals.Form_Main.dsp.Display(Vals.Page_Loading); }));
                }
                while (!Vals.bool_MachineRetrieved || !Vals.bool_AccountRetrieved) { Thread.Sleep(msTimeout); }
                Vals.List_AvailablePages.Add(Vals.string_HomePageKey);
                Vals.List_AvailablePages.Add(Vals.string_SettingsPageKey);
                Vals.List_AvailablePages.Add(Vals.string_HostPageKey);
                Vals.Form_Main.Invoke((MethodInvoker)(() => { Vals.Form_Main.dsp.Display(Vals.Page_ClientHome); }));
            }

            public static void void_TimerUpdateCallBack(object state) 
            {
                Vals.Action_TimerUpdateCallBack();
            }
            public static void void_RefreshPendingItems()
            {
                if (!Vals.bool_TCAccepted)
                {
                    Vals.Action_TCPending();
                }
                else
                {
                    Vals.Action_TCNotPending();
                }

                if (!Vals.bool_MachineRegistered)
                {
                    Vals.Action_MachineRegistryPending();
                }
                else
                {
                    Vals.Action_MachineRegistryNotPending();
                }

                if (!Vals.bool_TransceiverConnected)
                {
                    Vals.Action_TransceiverConnectedPending();
                }
                else
                {
                    Vals.Action_TransceiverConnectedNotPending();
                }

                if (!Vals.bool_RSAKeysCreated)
                {
                    Vals.Action_RSAKeysCreationPending();
                }
                else
                {
                    Vals.Action_RSAKeysCreationNotPending();
                }

                if (!Vals.bool_MachineAuthenticated)
                {
                    Vals.Action_MachineAuthenticationPending();
                }
                else
                {
                    Vals.Action_MachineAuthenticationNotPending();
                }

                if (!Vals.bool_AccountAuthenticated)
                {
                    Vals.Action_AccountAuthenticationPending();
                }
                else
                {
                    Vals.Action_AccountAuthenticationNotPending();
                }

                if (!Vals.bool_SeedKeyPairIsSetUp)
                {
                    Vals.Action_SeedKeyPairIsSetUpPending();
                }
                else
                {
                    Vals.Action_SeedKeyPairIsSetUpNotPending();
                }
            }
            public static void void_RefreshCallBackConditions()
            {
                if (Vals.bool_ProgramColorsActiveChanged)
                {
                    Vals.int_ProgramColorsActivePreviousHash = Vals.int_ProgramColorsActiveCurrentHash;
                    Vals.Action_ProgramColorsChanged();
                }
                if (Vals.bool_TCAcceptedChanged)
                {
                    Vals.int_TCAcceptedPreviousHash = Vals.int_TCAcceptedCurrentHash;
                    Vals.Action_TCAcceptedChanged();
                }
                if (Vals.bool_ListAccountChanged)
                {
                    Vals.List_AccountPrevious = Vals.List_Account.Clone();
                    Vals.Action_ListAccountChanged();
                }
                if (Vals.bool_MachineAuthenticatedChanged)
                {
                    Vals.int_MachineAuthenticatedPreviousHash = Vals.int_MachineAuthenticatedHash;
                    Vals.Action_MachineAuthenticatedChanged();
                }
                if (Vals.bool_AccountAuthenticatedChanged)
                {
                    Vals.int_AccountAuthenticatedPreviousHash = Vals.int_AccountAuthenticatedCurrentHash;
                    Vals.Action_AccountAuthenticatedChanged();
                }
                if (Vals.bool_MachineSecurityActiveChanged)
                {
                    Vals.MachineSecurity_ActivePrevious = Vals.MachineSecurity_Active.Clone();
                    Vals.Action_MachineSecurityActiveChanged();
                }
                if (Vals.bool_MachineActiveChanged)
                {
                    Vals.int_MachineActivePreviousHash = Vals.int_MachineActiveCurrentHash;
                    Vals.Action_MachineActiveChanged();
                    Vals.Machine_ActivePrevious = Vals.Machine_Active.Clone();
                }
                if (Vals.bool_AccountActiveChanged)
                {
                    Vals.int_AccountActivePreviousHash = Vals.int_AccountActiveCurrentHash;
                    Vals.Action_AccountActiveChanged();
                    Vals.Account_ActivePrevious = Vals.Account_Active.Clone();
                }
                if (Vals.bool_AvailablePagesChanged)
                {
                    Vals.List_AvailablePagesPrevious = Vals.List_AvailablePages.Clone();
                    Vals.Action_AvailablePagesChanged();
                }
            }


            public static void void_ExportFile(object o, string fileName)
            {
                byte[] Data = o.ToByte();
                File.WriteAllBytes(fileName, Data);
            }
            public static T T_ImportFile<T>(string fileName)
            {
                byte[] Data = File.ReadAllBytes(fileName);
                return Data.To<T>();
            }


            public static System.Windows.Forms.Timer Timer_MakeLoadingAnimation(Control control, int penWidth)
            {
                Graphics g = control.CreateGraphics();
                g.SmoothingMode = SmoothingMode.AntiAlias;
                int PenWidth = penWidth;
                int index = 0;

                Pen ErasePen = new Pen(control.BackColor, PenWidth + 4);
                
                System.Windows.Forms.Timer Result = new System.Windows.Forms.Timer { Interval = 10 };
                Result.Tick += (sender, e) =>
                {
                    g.DrawArc(ErasePen, new Rectangle(PenWidth, PenWidth, control.Width - PenWidth * 2, control.Height - PenWidth * 2), index % 360 - (90), 15);
                    g.DrawArc(new Pen(Color.FromArgb(index % 50, index % 254, index % 255), PenWidth), new Rectangle(PenWidth, PenWidth, control.Width - PenWidth * 2, control.Height - PenWidth * 2), index % 360, 10);
                    index += 10;
                };
                return Result;
            }
            public static System.Windows.Forms.Timer Timer_MakeSurroundLoadingAnimation(Control control, int speed)
            {
                Graphics g = control.CreateGraphics();
                g.SmoothingMode = SmoothingMode.AntiAlias;
                Size previousControlSize = control.Size;

                int Index = 0;
                int PenWidth = 4;

                Pen ErasePen = new Pen(control.BackColor, PenWidth + 4);
                Pen DrawPen;
                Func<int> GetPerimeter = () => { return 2 * control.Width + 2 * control.Height; };
                Func<Color> GetDrawColor = () => { return Color.FromArgb(Index % 50, Index % 1, Index % 100); };

                System.Windows.Forms.Timer Result = new System.Windows.Forms.Timer { Interval = 1 };
                Result.Tick += (sender, e) =>
                {
                    if(control.Size != previousControlSize)
                    {
                        control.Invalidate();
                    }
                    int perimeter = GetPerimeter();
                    DrawPen = new Pen(GetDrawColor(), PenWidth);
                    if (Index % perimeter < control.Height)
                    {
                        int Distance = Index % perimeter;
                        //g.DrawLine(ErasePen, new Point(0, Distance - speed), new Point(0, Distance));
                        g.DrawLine(DrawPen, new Point(0, Distance), new Point(0, Distance + speed));
                    }
                    else if(Index % perimeter < control.Height + control.Width)
                    {
                        int Distance = Index % perimeter - (control.Height);
                        //g.DrawLine(ErasePen, new Point(Distance - speed, control.Height - PenWidth), new Point(Distance, control.Height - PenWidth));
                        g.DrawLine(DrawPen, new Point(Distance, control.Height - PenWidth), new Point(Distance + speed, control.Height - PenWidth));
                    }
                    else if(Index % perimeter < 2 * control.Height + control.Width)
                    {
                        int Distance = Index % perimeter - (control.Height + control.Width);
                        //g.DrawLine(ErasePen, new Point(control.Width - PenWidth, control.Height - (Distance - speed)), new Point(control.Width - PenWidth, control.Height - (Distance)));
                        g.DrawLine(DrawPen, new Point(control.Width - PenWidth, control.Height - (Distance)), new Point(control.Width - PenWidth, control.Height - (Distance + speed)));
                    }
                    else if(Index % perimeter < perimeter)
                    {
                        int Distance = Index % perimeter - (2 * control.Height + control.Width);
                        //g.DrawLine(ErasePen, new Point(control.Width - (Distance - speed), 0), new Point(control.Width - (Distance), 0));
                        g.DrawLine(DrawPen, new Point(control.Width - (Distance), 0), new Point(control.Width - (Distance + speed), 0));
                    }
                    Index += speed;
                };
                return Result;
            }
            public static System.Windows.Forms.Timer Timer_MakeSlideAnimation(Control control, Point endLocation, int speed, Action CallBack)
            {
                int index = 0;
                Point originalLocation = control.Location;
                int originalXDistance = endLocation.X - control.Left;
                int originalYDistance = endLocation.Y - control.Top;
                System.Windows.Forms.Timer Result = new System.Windows.Forms.Timer { Interval = 1 };
                Result.Tick += (sender, e) =>
                {
                    int x = (int)(.5 * originalXDistance * Math.Tanh(index * 0.04 - 4) + originalLocation.X + .5 * originalXDistance);
                    int y = (int)(.5 * originalYDistance * Math.Tanh(index * 0.04 - 4) + originalLocation.Y + .5 * originalYDistance);
                    control.Left = x;
                    control.Top = y;
                    index += speed;

                    if(Math.Abs(control.Left - endLocation.X) <= 1 && Math.Abs(control.Top - endLocation.Y) <= 1)
                    {
                        control.Left = endLocation.X;
                        control.Top = endLocation.Y;
                    }

                    if (control.Left == endLocation.X && control.Top == endLocation.Y)
                    { Result.Stop(); CallBack(); }
                };
                return Result;
            }


            public static void void_BtnMouseEnter(object sender, EventArgs e)
            {
                RoundedButton b = (RoundedButton)sender;
                b.ForeColor = App.Vals.ProgramColors_Active.SelectedColor;
            }
            public static void void_BtnMouseLeave(object sender, EventArgs e)
            {
                RoundedButton b = (RoundedButton)sender;
                b.ForeColor = App.Vals.ProgramColors_Active.HUDColor3;
            }
            

            public static void void_WriteToConsole(string text, bool isChecked)
            {
                App.Vals.Form_Main.Invoke((MethodInvoker)(() =>
                {
                    App.Vals.Form_Main.lstConsole.Items.Add(text, isChecked);
                }));
            }


            public static void void_DisplayHoverInfo(Point location, string info)
            {
                App.Vals.RoundedPanel_HoverInfo.Location = location;
                App.Vals.RoundedPanel_HoverInfo.BackColor = App.Vals.ProgramColors_Active.HUDColor1;
                App.Vals.RoundedPanel_HoverInfo.ForeColor = App.Vals.ProgramColors_Active.HUDColor1;
                App.Vals.Label_HoverInfo.ForeColor = App.Vals.ProgramColors_Active.TitleColor;
                App.Vals.Label_HoverInfo.Location = new Point(App.Vals.RoundedPanel_HoverInfo.Width / 2 - App.Vals.Label_HoverInfo.Width / 2,
                    App.Vals.RoundedPanel_HoverInfo.Height / 2 - App.Vals.Label_HoverInfo.Height / 2);

                App.Vals.RoundedPanel_HoverInfo.Controls.Add(App.Vals.Label_HoverInfo);
                App.Vals.Label_HoverInfo.Text = info;
                App.Vals.Form_Main.Invoke((MethodInvoker)(() =>
                {
                    App.Vals.Form_Main.Controls.Add(App.Vals.RoundedPanel_HoverInfo);
                    App.Vals.RoundedPanel_HoverInfo.BringToFront();
                }));
            }
            public static void void_ClearHoverInfo()
            {
                App.Vals.Form_Main.Controls.Remove(App.Vals.RoundedPanel_HoverInfo);
            }


            public static void void_CreateRSAKeys()
            {
                Tuple<string, string> Keys = Crypt.GenerateRSAKeys(4096);
                Vals.MachineSecurity_Active.PublicKey = Keys.Item1;
                Vals.MachineSecurity_Active.PrivateKey = Keys.Item2;
            }


            public static Transmission Transmission_DecryptTransmission(Transceiver Transceiver, byte[] Data)
            {
                if (Data.Length == 0)
                {
                    return null;
                }
                string InPrivateKey = Vals.MachineSecurity_Active.PrivateKey;
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

                byte[] ValidKey = Transceiver.Security.SeedKeyPair.GetKey(Result.SKPString);
                if (Vals.bool_SeedKeyPairIsSetUp)
                {
                    if ((ValidKey == null && Result.SKPKey != null) || (Result.SKPKey == null && ValidKey != null) || (Result.SKPKey != null && ValidKey != null && !Result.SKPKey.SequenceEqual(ValidKey)))
                    {
                        return null;
                    }
                }
                Transceiver.Security.UsedSKPStrings.Add(Result.SKPString);

                return Result;
            }
            public static byte[] byte_EncryptTransmission(Transceiver Transceiver, Transmission Transmission, string OutPublicKey)
            {
                byte[] Result = null;

                Transmission.SKPString = Crypt.RandomString_AlphaNumeric(30, Vals.Random);
                Transmission.SKPKey = Transceiver.Security.SeedKeyPair.GetKey(Transmission.SKPString);
                Transceiver.Security.UsedSKPStrings.Add(Transmission.SKPString);

                Action Encrypt = null;
                Action Serialize = () =>
                {
                    Result = Transmission.ToByte();
                };
                Action AddMainLayer = () =>
                {
                    string MessageKey = Crypt.RandomString_AlphaNumeric(100, Vals.Random);
                    byte[] EncryptedMessageKey = Crypt.EncryptRSA(OutPublicKey, MessageKey.ToByte());
                    byte[] EncryptedMessage = Crypt.EncryptECB(Result, MessageKey.ToByte());
                    List<object> FinalExport = new List<object> { EncryptedMessageKey, EncryptedMessage };
                    Result = FinalExport.ToByte();
                };

                Encrypt += Serialize;
                Encrypt += AddMainLayer;
                Encrypt.Invoke();

                return Result;
            }


            public static void void_CallBackNewDiscourseCreated(Transceiver transceiver, Discourse discourse)
            {
                discourse.HandleTransmission = void_ProcessTransmission;
            }


            public static void void_GetSeedKeyPairSeed(Action<bool[]> Fail, Action<string> Succeed)
            {
                Discourse d = Vals.Transceiver.OpenNewDiscourse(Crypt.RandomString_AlphaNumeric(20, Vals.Random), (TC, DC, TM) => { void_CallBackGetSeedKeyPairSeed(TC, DC, TM, Fail, Succeed); });
                Transmission t = new Transmission(d.Info.ID);
                t.Add("function_name", "request_get_seed_key_pair_seed");
                t.Add("machine_public_key", Vals.MachineSecurity_Active.PublicKey);
                Vals.Transceiver.SendTransmission(t, Vals.string_AffixServicesPublicKey);
            }
            public static void void_RegisterMachine(string name, Action<bool[]> Fail, Action Succeed)
            {
                Machine MachineTemp = new Machine { string_IdentificationName = name, string_AuthenticationPublicKey = Vals.MachineSecurity_Active.PublicKey };
                //name, null, Vals.MachineSecurity_Active.PublicKey, Vals.Machine_Active.PrivateKey
                Discourse d = Vals.Transceiver.OpenNewDiscourse(Crypt.RandomString_AlphaNumeric(20, Vals.Random), (TC, DC, TM) => { void_CallBackRegisterMachine(TC, DC, TM, MachineTemp, Fail, Succeed); });
                Transmission t = new Transmission(d.Info.ID);
                t.Add("function_name", "request_register_machine");
                t.Add("machine_name", name);
                t.Add("machine_public_key", MachineTemp.string_AuthenticationPublicKey);
                Vals.Transceiver.SendTransmission(t, Vals.string_AffixServicesPublicKey);
            }
            public static void void_GetMachineAccessToken(Action<bool[]> Fail, Action<byte[]> Succeed)
            {
                Discourse d = Vals.Transceiver.OpenNewDiscourse(Crypt.RandomString_AlphaNumeric(20, Vals.Random), (TC, DC, TM) => { void_CallBackGetMachineAccessToken(TC, DC, TM, Fail, Succeed); });
                Transmission t = new Transmission(d.Info.ID);
                t.Add("function_name", "request_get_machine_access_token");
                t.Add("machine_id", Vals.Machine_Active.string_IdentificationID);
                Vals.Transceiver.SendTransmission(t, Vals.string_AffixServicesPublicKey);
            }
            public static void void_AuthenticateMachine(string accessToken, Action<bool[]> Fail, Action Succeed)
            {
                Discourse d = Vals.Transceiver.OpenNewDiscourse(Crypt.RandomString_AlphaNumeric(20, Vals.Random), (TC, DC, TM) => { void_CallBackAuthenticateMachine(TC, DC, TM, Fail, Succeed); });
                Transmission t = new Transmission(d.Info.ID);
                t.Add("function_name", "request_authenticate_machine");
                t.Add("machine_access_token", accessToken);
                Vals.Transceiver.SendTransmission(t, Vals.string_AffixServicesPublicKey);
            }
            public static void void_StreamGetMachine(Action<bool[]> Fail, Action Succeed, Action<Machine> StreamIn)
            {
                Discourse d = Vals.Transceiver.OpenNewDiscourse(Crypt.RandomString_AlphaNumeric(20, Vals.Random), (TC, DC, TM) => { void_CallBackStreamGetMachine(TC, DC, TM, Fail, Succeed, StreamIn); });
                Transmission t = new Transmission(d.Info.ID);
                t.Add("function_name", "request_streamget_machine");
                Vals.Transceiver.SendTransmission(t, Vals.string_AffixServicesPublicKey);
            }
            public static void void_RegisterAccount(string name, string emailAddress, string password, Action<bool[]> Fail, Action Succeed)
            {
                Tuple<string, string> RSAKeys = Crypt.GenerateRSAKeys(4096);
                string IFAPublicKey = RSAKeys.Item1;
                string IFAPrivateKey = RSAKeys.Item2;

                Tuple<string, string> RSABackupKeys = Crypt.GenerateRSAKeys(4096);
                string IFABackupPublicKey = RSAKeys.Item1;
                string IFABackupPrivateKey = RSAKeys.Item2;

                byte[] EncryptedIFAPrivateKey = Crypt.EncryptECB(IFAPrivateKey.ToByte(), password.ToByte());
                string RandomBackupKey = Crypt.RandomString_AlphaNumeric(48, Vals.Random);
                byte[] EncryptedIFABackupPrivateKey = Crypt.EncryptECB(IFABackupPrivateKey.ToByte(), RandomBackupKey.ToByte());
                Account AccountTemp = new Account { string_IdentificationName = name, string_IdentificationEmailAddress = emailAddress };
                Discourse d = Vals.Transceiver.OpenNewDiscourse(Crypt.RandomString_AlphaNumeric(20, Vals.Random), (TC, DC, TM) => { void_CallBackRegisterAccount(TC, DC, TM, AccountTemp, Fail, Succeed); });
                Transmission t = new Transmission(d.Info.ID);
                t.Add("function_name", "request_register_account");
                t.Add("account_name", name);
                t.Add("email_address", emailAddress);
                t.Add("ifa_public_key", IFAPublicKey);
                t.Add("ifa_private_key", EncryptedIFAPrivateKey);
                t.Add("ifa_backup_public_key", IFABackupPublicKey);
                t.Add("ifa_backup_private_key", EncryptedIFABackupPrivateKey);
                Vals.Transceiver.SendTransmission(t, Vals.string_AffixServicesPublicKey);
            }
            public static void void_GetAccountAccessToken(Action<bool[]> Fail, Action Succeed, Action<byte[], byte[]> RetrievedAccessToken)
            {
                Discourse d = Vals.Transceiver.OpenNewDiscourse(Crypt.RandomString_AlphaNumeric(20, Vals.Random), (TC, DC, TM) => { void_CallBackGetAccountAccessToken(TC, DC, TM, Fail, Succeed, RetrievedAccessToken); });
                Transmission t = new Transmission(d.Info.ID);
                t.Add("function_name", "request_get_account_access_token");
                t.Add("account_email_address", Vals.Account_Active.string_IdentificationEmailAddress);
                Vals.Transceiver.SendTransmission(t, Vals.string_AffixServicesPublicKey);
            }
            public static void void_AuthenticateAccount(string accessToken, Action<bool[]> Fail, Action Succeed)
            {
                Discourse d = Vals.Transceiver.OpenNewDiscourse(Crypt.RandomString_AlphaNumeric(20, Vals.Random), (TC, DC, TM) => { void_CallBackAuthenticateAccount(TC, DC, TM, Fail, Succeed); });
                Transmission t = new Transmission(d.Info.ID);
                t.Add("function_name", "request_authenticate_account");
                t.Add("account_access_token", accessToken);
                Vals.Transceiver.SendTransmission(t, Vals.string_AffixServicesPublicKey);
            }
            public static void void_StreamGetAccount(Action<bool[]> Fail, Action Succeed, Action<Account> StreamIn)
            {
                Discourse d = Vals.Transceiver.OpenNewDiscourse(Crypt.RandomString_AlphaNumeric(20, Vals.Random), (TC, DC, TM) => { void_CallBackStreamGetAccount(TC, DC, TM, Fail, Succeed, StreamIn); });
                Transmission t = new Transmission(d.Info.ID);
                t.Add("function_name", "request_streamget_account");
                Vals.Transceiver.SendTransmission(t, Vals.string_AffixServicesPublicKey);
            }
            public static void void_AddAccountToAccountP2PContract(string AccountName, Action<bool[]> Fail, Action Succeed)
            {
                Discourse d = Vals.Transceiver.OpenNewDiscourse(Crypt.RandomString_AlphaNumeric(20, Vals.Random), (TC, DC, TM) => { void_CallBackAddAccountToAccountP2PContract(TC, DC, TM, Fail, Succeed); });
                Transmission t = new Transmission(d.Info.ID);
                t.Add("function_name", "request_add_account_to_account");
                t.Add("account_name", AccountName);
                Vals.Transceiver.SendTransmission(t, Vals.string_AffixServicesPublicKey);
            }
            public static void void_AddMachineToAccountP2PContract(string AccountName, Action<bool[]> Fail, Action Succeed)
            {
                Discourse d = Vals.Transceiver.OpenNewDiscourse(Crypt.RandomString_AlphaNumeric(20, Vals.Random), (TC, DC, TM) => { void_CallBackAddMachineToAccountP2PContract(TC, DC, TM, Fail, Succeed); });
                Transmission t = new Transmission(d.Info.ID);
                t.Add("function_name", "request_add_account_to_machine");
                t.Add("account_name", AccountName);
                Vals.Transceiver.SendTransmission(t, Vals.string_AffixServicesPublicKey);
            }
            public static void void_RemoveAccountFromMachine(string AccountName, Action<bool[]> Fail, Action Succeed)
            {
                Discourse d = Vals.Transceiver.OpenNewDiscourse(Crypt.RandomString_AlphaNumeric(20, Vals.Random), (TC, DC, TM) => { void_CallBackRemoveAccountFromMachine(TC, DC, TM, Fail, Succeed); });
                Transmission t = new Transmission(d.Info.ID);
                t.Add("function_name", "request_remove_account_from_machine");
                t.Add("account_name", AccountName);
                Vals.Transceiver.SendTransmission(t, Vals.string_AffixServicesPublicKey);
            }
            public static void void_AddAccountToMachineP2PContract(string MachineID, Action<bool[]> Fail, Action Succeed)
            {
                Discourse d = Vals.Transceiver.OpenNewDiscourse(Crypt.RandomString_AlphaNumeric(20, Vals.Random), (TC, DC, TM) => { void_CallBackAddAccountToMachineP2PContract(TC, DC, TM, Fail, Succeed); });
                Transmission t = new Transmission(d.Info.ID);
                t.Add("function_name", "request_add_machine_to_account");
                t.Add("machine_id", MachineID);
                Vals.Transceiver.SendTransmission(t, Vals.string_AffixServicesPublicKey);
            }
            public static void void_RemoveMachineFromAccount(string MachineID, Action<bool[]> Fail, Action Succeed)
            {
                Discourse d = Vals.Transceiver.OpenNewDiscourse(Crypt.RandomString_AlphaNumeric(20, Vals.Random), (TC, DC, TM) => { void_CallBackRemoveMachineFromAccount(TC, DC, TM, Fail, Succeed); });
                Transmission t = new Transmission(d.Info.ID);
                t.Add("function_name", "request_remove_machine_from_account");
                t.Add("machine_id", MachineID);
                Vals.Transceiver.SendTransmission(t, Vals.string_AffixServicesPublicKey);
            }
            public static void void_ConnectToMachine(string RemoteMachineID, IPEndPoint IPEndPoint_External, IPEndPoint IPEndPoint_Internal, Action<bool[]> Fail, Action<string> Succeed)
            {
                Discourse d = Vals.Transceiver.OpenNewDiscourse(Crypt.RandomString_AlphaNumeric(20, Vals.Random), (TC, DC, TM) => { void_CallBackConnectToMachine(TC, DC, TM, Fail, Succeed); });
                Transmission t = new Transmission(d.Info.ID);
                t.Add("function_name", "request_connect_to_machine");
                t.Add("remote_machine_id", RemoteMachineID);
                t.Add("ipendpoint_external", IPEndPoint_External);
                t.Add("ipendpoint_internal", IPEndPoint_Internal);
                Vals.Transceiver.SendTransmission(t, Vals.string_AffixServicesPublicKey);
            }
            public static void void_ConnectToAccount(string RemoteAccountID, IPEndPoint IPEndPoint_External, IPEndPoint IPEndPoint_Internal, Action<bool[]> Fail, Action<string> Succeed)
            {
                Discourse d = Vals.Transceiver.OpenNewDiscourse(Crypt.RandomString_AlphaNumeric(20, Vals.Random), (TC, DC, TM) => { void_CallBackConnectToAccount(TC, DC, TM, Fail, Succeed); });
                Transmission t = new Transmission(d.Info.ID);
                t.Add("function_name", "request_connect_to_account");
                t.Add("remote_account_id", RemoteAccountID);
                t.Add("ipendpoint_external", IPEndPoint_External);
                t.Add("ipendpoint_internal", IPEndPoint_Internal);
                Vals.Transceiver.SendTransmission(t, Vals.string_AffixServicesPublicKey);
            }


            private static void void_CallBackGetSeedKeyPairSeed(Transceiver transceiver, Discourse discourse, Transmission transmission, Action<bool[]> Fail, Action<string> Succeed)
            {
                bool[] FunctionFlags = (bool[])transmission["function_flags"];
                if (!FunctionFlags.Contains(true))
                {
                    string Seed = (string)transmission["seed_key_pair_seed"];
                    Succeed(Seed);
                }
                else
                {
                    Fail(FunctionFlags);
                }
            }
            private static void void_CallBackRegisterMachine(Transceiver transceiver, Discourse discourse, Transmission transmission, Machine MachineTemp, Action<bool[]> Fail, Action Succeed)
            {
                discourse.Info.isOpen = false;

                bool[] FunctionFlags = (bool[])transmission["function_flags"];
                if (!FunctionFlags.Contains(true))
                {
                    string MachineID = (string)transmission["machine_id"];
                    MachineTemp.string_IdentificationID = MachineID;
                    Vals.Machine_Active = MachineTemp;
                    Succeed();
                }
                else
                {
                    bool[] ErrorFlags = (bool[])transmission["function_flags"];
                    Fail(ErrorFlags);
                }
            }
            private static void void_CallBackGetMachineAccessToken(Transceiver transceiver, Discourse discourse, Transmission transmission, Action<bool[]> Fail, Action<byte[]> Succeed)
            {
                discourse.Info.isOpen = false;

                bool[] FunctionFlags = (bool[])transmission["function_flags"];
                if (!FunctionFlags.Contains(true))
                {
                    byte[] EncryptedMachineAccessToken = (byte[])transmission["machine_access_token"];
                    Succeed(EncryptedMachineAccessToken);
                }
                else
                {
                    bool[] ErrorFlags = (bool[])transmission["function_flags"];
                    Fail(ErrorFlags);
                }
            }
            private static void void_CallBackAuthenticateMachine(Transceiver transceiver, Discourse discourse, Transmission transmission, Action<bool[]> Fail, Action Succeed)
            {
                discourse.Info.isOpen = false;

                bool[] FunctionFlags = (bool[])transmission["function_flags"];
                if (!FunctionFlags.Contains(true))
                {
                    Vals.bool_MachineAuthenticated = true;
                    Succeed();
                }
                else
                {
                    bool[] ErrorFlags = (bool[])transmission["function_flags"];
                    Fail(ErrorFlags);
                }
            }
            private static void void_CallBackStreamGetMachine(Transceiver transceiver, Discourse discourse, Transmission transmission, Action<bool[]> Fail, Action Succeed, Action<Machine> StreamIn)
            {
                bool[] FunctionFlags = (bool[])transmission["function_flags"];
                if (!FunctionFlags.Contains(true))
                {
                    discourse.HandleTransmission = (TC, DC, TM) =>
                    {
                        Machine machine = (Machine)TM["machine"];
                        StreamIn(machine);
                    };
                    Succeed();
                }
                else
                {
                    bool[] ErrorFlags = (bool[])transmission["function_flags"];
                    Fail(ErrorFlags);
                    discourse.Close();
                }
            }
            private static void void_CallBackRegisterAccount(Transceiver transceiver, Discourse discourse, Transmission transmission, Account AccountTemp, Action<bool[]> Fail, Action Succeed)
            {
                discourse.Info.isOpen = false;

                bool[] FunctionFlags = (bool[])transmission["function_flags"];
                if (!FunctionFlags.Contains(true))
                {
                    string AccountID = (string)transmission["account_id"];
                    AccountTemp.string_IdentificationID = AccountID;
                    Vals.Account_Active = AccountTemp;
                    Succeed();
                }
                else
                {
                    bool[] ErrorFlags = (bool[])transmission["function_flags"];
                    Fail(ErrorFlags);
                }
            }
            private static void void_CallBackGetAccountAccessToken(Transceiver transceiver, Discourse discourse, Transmission transmission, Action<bool[]> Fail, Action Succeed, Action<byte[], byte[]> RetrievedAccessToken)
            {
                bool[] FunctionFlags = (bool[])transmission["function_flags"];
                if (!FunctionFlags.Contains(true))
                {
                    discourse.HandleTransmission = (TC, DC, TM) =>
                    {
                        byte[] EncryptedAccountAccessToken = (byte[])TM["account_access_token"];
                        byte[] EncryptedIFAPrivateKey = (byte[])TM["account_ifa_private_key"];
                        RetrievedAccessToken(EncryptedAccountAccessToken, EncryptedIFAPrivateKey);
                        discourse.Info.isOpen = false;
                    };
                    Succeed();
                }
                else
                {
                    discourse.Info.isOpen = false;
                    bool[] ErrorFlags = (bool[])transmission["function_flags"];
                    string ErrorDesc = "";
                    ErrorDesc += ErrorFlags[0] ? "An account on this client is already authenticated\n" : "";
                    Dialog_Notification n = new Dialog_Notification("Error Authenticating Account", ErrorDesc, new Panel() { BackColor = Color.Red });
                    Fail(ErrorFlags);
                }
            }
            private static void void_CallBackAuthenticateAccount(Transceiver transceiver, Discourse discourse, Transmission transmission, Action<bool[]> Fail, Action Succeed)
            {
                discourse.Info.isOpen = false;

                bool[] FunctionFlags = (bool[])transmission["function_flags"];
                if (!FunctionFlags.Contains(true))
                {
                    Vals.bool_AccountAuthenticated = true;
                    Succeed();
                }
                else
                {
                    bool[] ErrorFlags = (bool[])transmission["function_flags"];
                    Fail(ErrorFlags);
                }
            }
            private static void void_CallBackStreamGetAccount(Transceiver transceiver, Discourse discourse, Transmission transmission, Action<bool[]> Fail, Action Succeed, Action<Account> StreamIn)
            {
                bool[] FunctionFlags = (bool[])transmission["function_flags"];
                if (!FunctionFlags.Contains(true))
                {
                    discourse.HandleTransmission = (TC, DC, TM) =>
                    {
                        Account account = (Account)TM["account"];
                        StreamIn(account);
                    };
                    Succeed();
                }
                else
                {
                    bool[] ErrorFlags = (bool[])transmission["function_flags"];
                    Fail(ErrorFlags);
                    discourse.Close();
                }
            }
            private static void void_CallBackAddAccountToAccountP2PContract(Transceiver transceiver, Discourse discourse, Transmission transmission, Action<bool[]> Fail, Action Succeed)
            {
                discourse.Info.isOpen = false;

                bool[] FunctionFlags = (bool[])transmission["function_flags"];
                if (!FunctionFlags.Contains(true))
                {
                    Succeed();
                }
                else
                {
                    bool[] ErrorFlags = (bool[])transmission["function_flags"];
                    Fail(ErrorFlags);
                }
            }
            private static void void_CallBackAddMachineToAccountP2PContract(Transceiver transceiver, Discourse discourse, Transmission transmission, Action<bool[]> Fail, Action Succeed)
            {
                discourse.Info.isOpen = false;

                bool[] FunctionFlags = (bool[])transmission["function_flags"];
                if (!FunctionFlags.Contains(true))
                {
                    Succeed();
                }
                else
                {
                    bool[] ErrorFlags = (bool[])transmission["function_flags"];
                    Fail(ErrorFlags);
                }
            }
            private static void void_CallBackRemoveAccountFromMachine(Transceiver transceiver, Discourse discourse, Transmission transmission, Action<bool[]> Fail, Action Succeed)
            {
                discourse.Info.isOpen = false;

                bool[] FunctionFlags = (bool[])transmission["function_flags"];
                if (!FunctionFlags.Contains(true))
                {
                    Succeed();
                }
                else
                {
                    bool[] ErrorFlags = (bool[])transmission["function_flags"];
                    Fail(ErrorFlags);
                }
            }
            private static void void_CallBackAddAccountToMachineP2PContract(Transceiver transceiver, Discourse discourse, Transmission transmission, Action<bool[]> Fail, Action Succeed)
            {
                discourse.Info.isOpen = false;

                bool[] FunctionFlags = (bool[])transmission["function_flags"];
                if (!FunctionFlags.Contains(true))
                {
                    Succeed();
                }
                else
                {
                    bool[] ErrorFlags = (bool[])transmission["function_flags"];
                    Fail(ErrorFlags);
                }
            }
            private static void void_CallBackRemoveMachineFromAccount(Transceiver transceiver, Discourse discourse, Transmission transmission, Action<bool[]> Fail, Action Succeed)
            {
                discourse.Info.isOpen = false;

                bool[] FunctionFlags = (bool[])transmission["function_flags"];
                if (!FunctionFlags.Contains(true))
                {
                    string SuccessMessage = "The Account to Machine P2P Contract was successfully disassociated.";
                    void_WriteToConsole(SuccessMessage, false);
                    Succeed();
                }
                else
                {
                    bool[] ErrorFlags = (bool[])transmission["function_flags"];
                    string ErrorMessage = "";
                    ErrorMessage += ErrorFlags[0] ? "The remote machine does not exist;" : "";
                    ErrorMessage += ErrorFlags[1] ? "The remote machine was not associated with the account;" : "";
                    void_WriteToConsole(ErrorMessage, false);
                    Fail(ErrorFlags);
                }
            }
            private static void void_CallBackConnectToMachine(Transceiver transceiver, Discourse discourse, Transmission transmission, Action<bool[]> Fail, Action<string> Succeed)
            {
                discourse.Info.isOpen = false;

                bool[] FunctionFlags = (bool[])transmission["function_flags"];
                if (!FunctionFlags.Contains(true))
                {
                    string ContactToken = (string)transmission["contact_token"];
                    Succeed(ContactToken);
                }
                else
                {
                    bool[] ErrorFlags = (bool[])transmission["function_flags"];
                    Fail(ErrorFlags);
                }
            }
            private static void void_CallBackConnectToAccount(Transceiver transceiver, Discourse discourse, Transmission transmission, Action<bool[]> Fail, Action<string> Succeed)
            {
                discourse.Info.isOpen = false;

                bool[] FunctionFlags = (bool[])transmission["function_flags"];
                if (!FunctionFlags.Contains(true))
                {
                    string ContactToken = (string)transmission["contact_token"];
                    Succeed(ContactToken);
                }
                else
                {
                    bool[] ErrorFlags = (bool[])transmission["function_flags"];
                    Fail(ErrorFlags);
                }
            }


            public static void void_InitTransceiver()
            {
                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IAsyncResult result = s.BeginConnect(Vals.IPEndPoint_AffixServices, void_CallBackTransceiverSocketConnected, s);
                Action WaitForResult = () =>
                {
                    bool success = result.AsyncWaitHandle.WaitOne(5000, true);
                    if (s.Connected)
                    {
                        s.EndConnect(result);
                    }
                    else
                    {
                        s.Close();
                        void_InitTransceiver();
                    }
                };
                WaitForResult.BeginInvoke(x => { }, null);
            }
            private static void void_CallBackTransceiverSocketConnected(IAsyncResult result)
            {
                Socket s = (Socket)result.AsyncState;
                Vals.Transceiver = new Transceiver(s, Vals.TransceiverCallBacks);
                Vals.Transceiver.Security = Vals.TransceiverSecurity;
                Vals.Transceiver.Open();

                if (s.Connected)
                {
                    Action OnDisconnect = null;
                    OnDisconnect = () =>
                    {
                        Vals.Action_TransceiverConnectedPending -= OnDisconnect;
                        void_InitTransceiver();
                    };
                    Vals.Action_TransceiverConnectedPending += OnDisconnect;
                }
            }


            public static void void_ProcessTransmission(Transceiver Transceiver, Discourse Discourse, Transmission Transmission)
            {
                string FunctionName = (string)Transmission["function_name"];
            }
        }
    }

    [Serializable]
    public class MachineSecurity
    {
        public string PublicKey;
        public string PrivateKey;
        public MachineSecurity()
        {

        }
        public MachineSecurity(string publicKey, string privateKey)
        {
            this.PublicKey = publicKey;
            this.PrivateKey = privateKey;
        }
    }
    [Serializable]
    public struct ProgramColors
    {
        public Color BackColor;
        public Color HUDColor1;
        public Color HUDColor2;
        public Color HUDColor3;
        public Color TitleColor;
        public Color SubtitleColor;
        public Color ParagraphColor;
        public Color SelectedColor;
        public Color DeselectedColor;
        public ProgramColors(Color BackColor, Color HUDColor1, Color HUDColor2, Color HUDColor3, Color TitleColor, Color SubtitleColor, Color ParagraphColor, Color SelectedColor, Color DeselectedColor)
        {
            this.BackColor = BackColor;
            this.HUDColor1 = HUDColor1;
            this.HUDColor2 = HUDColor2;
            this.HUDColor3 = HUDColor3;
            this.TitleColor = TitleColor;
            this.SubtitleColor = SubtitleColor;
            this.ParagraphColor = ParagraphColor;
            this.SelectedColor = SelectedColor;
            this.DeselectedColor = DeselectedColor;
        }
    }
    public class P2PConnection
    {
        public P2P P2P;
        public NatTraversal NatTraversal;
        public Transceiver Transceiver;

        public bool bool_MachineAuthenticated;
        public bool bool_AccountAuthenticated;

        public P2PConnection(P2P P2P)
        {
            this.P2P = P2P;
            this.NatTraversal = new NatTraversal();
            this.Transceiver = new Transceiver(null, new TransceiverCallBacks(void_CallBackNewDiscourseCreated));
            this.Transceiver.Security = new TransceiverSecurity(new SeedKeyPair(null, x => { return null; }), App.Methods.byte_EncryptTransmission, App.Methods.Transmission_DecryptTransmission);
        }

        public void void_CallBackNewDiscourseCreated(Transceiver transceiver, Discourse discourse)
        {

        }
    }
}