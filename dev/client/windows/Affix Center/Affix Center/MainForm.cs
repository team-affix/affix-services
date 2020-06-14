using Affix_Center.CustomControls;
using Aurora.Generalization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UILayout;
using Security;
using AffixServices.Communication;

namespace Affix_Center
{
    public partial class MainForm : Form
    {
        public ControlLayout Layout_Main;
        public ControlLayout Layout_Notification;


        public List<List<PageController>> PageControllers;


        public Label Title_StartUp;
        public Label Title_SetUp;
        public Label Title_Welcome;


        public PageHeader Header_TermsAndConditions;
        public PageHeader Header_AffixCenter;


        public Panel ProgramIcon_TeamAffix;
        public Label ProgramIcon_ErrorEncountered;


        public Page_TermsAndConditions Page_TermsAndConditions;
        public Page_AffixCenterDetails Page_AffixCenterDetails;
        public Page_AffixServicesDetails Page_AffixServicesDetails;
        public Page_AccountRegistry Page_AccountRegistry;
        public Page_MachineRegistry Page_MachineRegistry;
        public Page_AuthenticateAccount Page_AuthenticateAccount;


        public Control LeftMenu_UserUnauthenticated;


        public LeftMenuOptionDialog LeftMenuOptionDialog_RegisterAccount;
        public LeftMenuOptionDialog LeftMenuOptionDialog_AuthenticateAccount;


        public List<AvailablePage> Pages_Available = new List<AvailablePage> { };
        public List<AvailablePage> Pages_Available_Previous = new List<AvailablePage> { };


        public NotificationDialog Notification_MachineRegistryRequired;
        public NotificationDialog Notification_AccountRegistryRequired;
        public NotificationDialog Notification_TCRequired;
        public NotificationDialog Notification_ClientOffline;


        public Sync<List<AvailablePage>> Sync_WaitForAvailablePages;


        public bool WaitingForRegistry;


        public Control CenterPage_Active;
        public Control Header_Active;
        public Control LeftMenu_Active;
        public Control ProgramIcon_Active;


        public MainForm()
        {
            InitializeComponent();
        }
        private void Main_Load(object sender, EventArgs e)
        {

        }
        private void Main_Shown(object sender, EventArgs e)
        {
            this.Init_MainForm();
        }
        private void Main_SizeChanged(object sender, EventArgs e)
        {
            this.Update_MainLayout();
            this.Update_NotificationLayout();
            this.Update_PageControllers();
        }
    }

    public static class Main_Processor
    {
        public static void Init_MainForm(this MainForm mainForm)
        {
            mainForm.Init_Layouts();
            mainForm.Init_PageControllers();
            mainForm.Init_Notifications();
            mainForm.Init_Pages();
            mainForm.Init_Updates();
        }


        public static void Init_Updates(this MainForm mainForm)
        {
            App_Vals.Timer_Update.Tick += (x, y) => { mainForm.Update_Display(); };
        }


        public static void Init_Pages(this MainForm mainForm)
        {
            mainForm.Init_TitlePages();
            mainForm.Init_HeaderPages();
            mainForm.Init_ProgramIconPages();
            mainForm.Init_CenterScreenPages();
            mainForm.Init_LeftMenuPages();
            Init_ActivePages(mainForm);
        }
        private static void Init_ActivePages(MainForm mainForm)
        {
            mainForm.CenterPage_Active = mainForm.Page_AffixCenterDetails;
            mainForm.Header_Active = mainForm.Header_AffixCenter;
            mainForm.LeftMenu_Active = mainForm.LeftMenu_UserUnauthenticated;
            mainForm.ProgramIcon_Active = mainForm.ProgramIcon_TeamAffix;
        }


        public static void Init_CenterScreenPages(this MainForm mainForm)
        {
            Init_PageTermsAndConditions(mainForm);
            Init_PageAffixCenterDetails(mainForm);
            Init_PageAffixServicesDetails(mainForm);
            Init_PageAccountRegistry(mainForm);
            Init_PageMachineRegistry(mainForm);
            Init_PageAuthenticateAccount(mainForm);
        }
        private static void Init_PageTermsAndConditions(MainForm mainForm)
        {
            mainForm.Page_TermsAndConditions = new Page_TermsAndConditions();
            mainForm.Page_TermsAndConditions.btnAccept.Click += (x, y) => { App_Vals.TC_Accepted = true; };
        }
        private static void Init_PageAffixCenterDetails(MainForm mainForm)
        {
            mainForm.Page_AffixCenterDetails = new Page_AffixCenterDetails();
        }
        private static void Init_PageAffixServicesDetails(MainForm mainForm)
        {
            mainForm.Page_AffixServicesDetails = new Page_AffixServicesDetails();
        }
        private static void Init_PageAccountRegistry(MainForm mainForm)
        {
            mainForm.Page_AccountRegistry = new Page_AccountRegistry();
            mainForm.Page_AccountRegistry.btnRegisterAccount.Click += (x, y) => 
            {
                mainForm.Register_Account();
            };
        }
        private static void Init_PageMachineRegistry(MainForm mainForm)
        {
            mainForm.Page_MachineRegistry = new Page_MachineRegistry();
            mainForm.Page_MachineRegistry.btnRegisterMachine.Click += (x, y) => 
            {
                mainForm.Register_Machine();
            };
        }
        private static void Init_PageAuthenticateAccount(MainForm mainForm)
        {
            mainForm.Page_AuthenticateAccount = new Page_AuthenticateAccount();
        }


        public static void Init_TitlePages(this MainForm mainForm)
        {
            Init_TitleStartUp(mainForm);
            Init_TitleSetUp(mainForm);
            Init_TitleTermsAndConditions(mainForm);
        }
        private static void Init_TitleStartUp(MainForm mainForm)
        {
            mainForm.Title_StartUp = new Label() { Text = "Starting up; please wait.", ForeColor = Color.DimGray, Font = new Font("Segoe UI Light", 20), TextAlign = ContentAlignment.MiddleLeft };
        }
        private static void Init_TitleSetUp(MainForm mainForm)
        {
            mainForm.Title_SetUp = new Label() { Text = "Setup", ForeColor = Color.DimGray, Font = new Font("Segoe UI", 20), TextAlign = ContentAlignment.MiddleLeft };
        }
        private static void Init_TitleTermsAndConditions(MainForm mainForm)
        {
            mainForm.Title_Welcome = new Label() { Text = "Welcome", ForeColor = Color.CadetBlue, Font = new Font("Segoe UI", 20), TextAlign = ContentAlignment.MiddleLeft };
        }


        public static void Init_HeaderPages(this MainForm mainForm)
        {
            Init_HeaderTermsAndConditions(mainForm);
            Init_HeaderAffixCenter(mainForm);
        }
        private static void Init_HeaderTermsAndConditions(MainForm mainForm)
        {
            mainForm.Header_TermsAndConditions = new PageHeader("Terms and Conditions", "Team Affix Online Services");
        }
        private static void Init_HeaderAffixCenter(MainForm mainForm)
        {
            mainForm.Header_AffixCenter = new PageHeader("Affix Center", "Team Affix Online Services' Affix Center Client");
        }


        public static void Init_LeftMenuPages(this MainForm mainForm)
        {
            Init_LeftMenuUserUnauthenticated(mainForm);
        }
        private static void Init_LeftMenuUserUnauthenticated(MainForm mainForm)
        {
            mainForm.LeftMenu_UserUnauthenticated = new LeftMenu_UserUnauthenticated();
        }


        public static void Init_ProgramIconPages(this MainForm mainForm)
        {
            Init_ProgramIconTeamAffix(mainForm);
            Init_ProgramIconErrorEncountered(mainForm);
        }
        private static void Init_ProgramIconTeamAffix(this MainForm mainForm)
        {
            mainForm.ProgramIcon_TeamAffix = new Panel() { BackColor = Color.Transparent, BackgroundImage = Properties.Resources.AffixLogoWhite, BackgroundImageLayout = ImageLayout.Zoom };
            mainForm.ProgramIcon_TeamAffix.Click += (x, y) => { mainForm.Display_PageAffixServicesDetails(); };
        }
        private static void Init_ProgramIconErrorEncountered(this MainForm mainForm)
        {
            mainForm.ProgramIcon_ErrorEncountered = new Label() { BackColor = Color.Transparent, ForeColor = Color.Yellow, Font = new Font("Segoe UI", 20), Text = "!", TextAlign = ContentAlignment.MiddleRight };
        }


        public static void Init_Notifications(this MainForm mainForm)
        {
            Init_NotificationMachineRegistryRequired(mainForm);
            Init_NotificationAccountRegistryRequired(mainForm);
            Init_NotificationTCRequired(mainForm);
            Init_NotificationClientOffline(mainForm);
        }
        private static void Init_NotificationMachineRegistryRequired(MainForm mainForm)
        {
            mainForm.Notification_MachineRegistryRequired = new NotificationDialog("Machine Registry", "You will be unable to use Affix Online Services until\nyou register this machine.", new Panel());
        }
        private static void Init_NotificationAccountRegistryRequired(MainForm mainForm)
        {
            mainForm.Notification_AccountRegistryRequired = new NotificationDialog("Account Registry", "You will be unable to use Affix Online Services until\nyou sign into your account.", new Panel());
        }
        private static void Init_NotificationTCRequired(MainForm mainForm)
        {
            mainForm.Notification_TCRequired = new NotificationDialog("Terms and Conditions", "This Affix Client will be offline until \nTerms and Conditions are accepted.", new Panel());
        }
        private static void Init_NotificationClientOffline(MainForm mainForm)
        {
            mainForm.Notification_ClientOffline = new NotificationDialog("Affix Center is Offline", "Affix Online Services will be unavailable until\nthis client connects to Affix Services", new LoadingDialog(Color.White));
        }


        public static void Init_Layouts(this MainForm mainForm)
        {
            mainForm.Update_MainLayout();
            mainForm.Update_NotificationLayout();
        }
        public static void Update_MainLayout(this MainForm mainForm)
        {
            mainForm.Layout_Main = new ControlLayout(mainForm.Size, new List<int> { 20, 120, 220, mainForm.Width - 220, mainForm.Width - 120, mainForm.Width - 20 }, new List<int> { 20, 70, 170, mainForm.Height - 80, mainForm.Height });
        }
        public static void Update_NotificationLayout(this MainForm mainForm)
        {
            mainForm.Layout_Notification = new ControlLayout(new Size(300, 60), new List<int> { 0, 50, 300 }, new List<int> { 0, 50, 60 });
        }


        public static void Init_PageControllers(this MainForm mainForm)
        {
            mainForm.PageControllers = new List<List<PageController>> { };
            var GS = mainForm.Layout_Main.GetGridSpaces();
            for (int i = 0; i < GS.Count; i++)
            {
                List<PageController> Row = new List<PageController> { };
                for (int j = 0; j < GS[i].Count; j++)
                {
                    PageController PageController = new PageController() { BackColor = Color.Transparent, Bounds = GS[i][j] };
                    mainForm.Invoke((MethodInvoker)(() => { mainForm.Controls.Add(PageController); }));
                    Row.Add(PageController);
                }

                mainForm.PageControllers.Add(Row);
            }
        }


        public static void Update_PageControllers(this MainForm mainForm)
        {
            var GS = mainForm.Layout_Main.GetGridSpaces();
            for (int i = 0; i < mainForm.PageControllers.Count; i++)
            {
                for (int j = 0; j < mainForm.PageControllers[i].Count; j++)
                {
                    PageController PageController = mainForm.PageControllers[i][j];
                    PageController.Bounds = GS[i][j];
                }
            }
        }


        public static void Update_Display(this MainForm mainForm)
        {
            Update_Pages(mainForm);
            Update_Notifications(mainForm);
        }
        public static void Update_Pages(this MainForm mainForm)
        {

            if (!App_Processor.Get_MachineInfoLocalExists() || !App_Vals.TC_Accepted)
            {
                if (!mainForm.WaitingForRegistry)
                {
                    mainForm.WaitingForRegistry = true;
                    EventHandler WaitForRegistry = null;
                    WaitForRegistry = (x, y) =>
                    {
                        if (App_Processor.Get_MachineInfoLocalExists() && App_Vals.TC_Accepted)
                        {
                            App_Vals.Timer_Update.Tick -= WaitForRegistry;
                            mainForm.Invoke((MethodInvoker)(() =>
                            {
                                mainForm.Display_ActivePages();
                            }));
                            mainForm.WaitingForRegistry = false;
                        }
                    };
                    App_Vals.Timer_Update.Tick += WaitForRegistry;
                }
            }

            if (!mainForm.PageControllers[1][1].Controls.Contains(mainForm.ProgramIcon_TeamAffix)) // Display Team Affix Icon
            {
                mainForm.PageControllers[1][1].ActivatePage(mainForm.ProgramIcon_TeamAffix);
            }

            if (!App_Vals.TC_Accepted) // T&C has not been accepted
            {
                if (!mainForm.PageControllers[2][2].Controls.Contains(mainForm.Page_TermsAndConditions)) // Display T&C Page
                {
                    mainForm.Display_PageTermsAndConditions();
                }
                return;
            }

            if (!App_Processor.Get_MachineInfoLocalExists() && App_Vals.TC_Accepted) // Machine not registered, but T&C have been accepted
            {
                if (!mainForm.PageControllers[2][2].Controls.Contains(mainForm.Page_MachineRegistry)) // Display Machine Registry Page
                {
                    mainForm.Display_PageMachineRegistry();
                }
                return;
            }

            if (!mainForm.PageControllers[1][2].Controls.Contains(mainForm.Header_Active) 
                || !mainForm.PageControllers[1][1].Controls.Contains(mainForm.ProgramIcon_Active) 
                || !mainForm.PageControllers[2][1].Controls.Contains(mainForm.LeftMenu_Active) 
                || !mainForm.PageControllers[2][2].Controls.Contains(mainForm.CenterPage_Active))
            {
                mainForm.Display_ActivePages();
            }
        }
        public static void Update_Notifications(this MainForm mainForm)
        {
            if (!App_Vals.TC_Accepted) // T&C has not been accepted
            {
                if (!mainForm.pnlNotifications.Controls.Contains(mainForm.Notification_TCRequired))
                {
                    mainForm.Display_NotificationTCRequired();
                    mainForm.Page_TermsAndConditions.btnAccept.Click += (x, y) => { mainForm.pnlNotifications.Controls.Remove(mainForm.Notification_TCRequired); };
                }
            }

            if (!App_Processor.Get_AccountInfoLocalExists())
            {
                if (!mainForm.pnlNotifications.Controls.Contains(mainForm.Notification_AccountRegistryRequired))
                {
                    mainForm.Display_NotificationAccountRegistryRequired(); 
                }
            }

            if (App_Vals.Transceiver == null || !App_Vals.Transceiver.Socket.Connected)
            {
                if (!mainForm.pnlNotifications.Controls.Contains(mainForm.Notification_ClientOffline))
                {
                    mainForm.Display_NotificationClientOffline();
                    EventHandler CheckForOnlineTick = null;
                    CheckForOnlineTick = (x, y) =>
                    {
                        if(App_Vals.Transceiver != null && App_Vals.Transceiver.Socket.Connected)
                        {
                            mainForm.pnlNotifications.Controls.Remove(mainForm.Notification_ClientOffline);
                        }
                    };
                    App_Vals.Timer_Update.Tick += CheckForOnlineTick;
                }
            }
        }


        public static void Display_PageTermsAndConditions(this MainForm mainForm)
        {
            mainForm.PageControllers[1][2].ActivatePage(new PageHeader("Terms and Conditions", "Team Affix Online Services' Terms and Conditions"));
            mainForm.PageControllers[2][2].ActivatePage(mainForm.Page_TermsAndConditions);
        }
        public static void Display_PageAffixCenterDetails(this MainForm mainForm)
        {
            mainForm.PageControllers[1][2].ActivatePage(mainForm.Header_AffixCenter);
            mainForm.PageControllers[2][2].ActivatePage(mainForm.Page_AffixCenterDetails);
        }
        public static void Display_PageAffixServicesDetails(this MainForm mainForm)
        {
            mainForm.PageControllers[1][2].ActivatePage(new PageHeader("Affix Center", "Team Affix Online Services' Affix Center Client"));
            mainForm.PageControllers[2][2].ActivatePage(mainForm.Page_AffixServicesDetails);
        }
        public static void Display_PageAccountRegistry(this MainForm mainForm)
        {
            mainForm.PageControllers[1][2].ActivatePage(new PageHeader("Affiliation Registry", "Create an affiliation with Affix Services"));
            mainForm.PageControllers[2][2].ActivatePage(mainForm.Page_AccountRegistry);
        }
        public static void Display_PageMachineRegistry(this MainForm mainForm)
        {
            mainForm.PageControllers[1][2].ActivatePage(new PageHeader("Machine Registry", "Set up your machine to be recognized by Affix Services"));
            mainForm.PageControllers[2][2].ActivatePage(mainForm.Page_MachineRegistry);
        }
        public static void Display_PageAuthenticateAccount(this MainForm mainForm)
        {
            mainForm.PageControllers[1][2].ActivatePage(new PageHeader("Log In", "Authenticate your Affix Services affiliation"));
            mainForm.PageControllers[2][2].ActivatePage(mainForm.Page_AuthenticateAccount);
        }


        public static void Display_NotificationMachineRegistryRequired(this MainForm mainForm)
        {
            mainForm.pnlNotifications.Controls.Add(mainForm.Notification_MachineRegistryRequired);
        }
        public static void Display_NotificationAccountRegistryRequired(this MainForm mainForm)
        {
            mainForm.pnlNotifications.Controls.Add(mainForm.Notification_AccountRegistryRequired);
        }
        public static void Display_NotificationTCRequired(this MainForm mainForm)
        {
            mainForm.pnlNotifications.Controls.Add(mainForm.Notification_TCRequired);
        }
        public static void Display_NotificationClientOffline(this MainForm mainForm)
        {
            mainForm.pnlNotifications.Controls.Add(mainForm.Notification_ClientOffline);
        }


        public static void Display_ActivePages(this MainForm mainForm)
        {
            mainForm.PageControllers[1][1].ActivatePage(mainForm.ProgramIcon_Active);
            mainForm.PageControllers[1][2].ActivatePage(mainForm.Header_Active);
            mainForm.PageControllers[2][2].ActivatePage(mainForm.CenterPage_Active);
            mainForm.PageControllers[2][1].ActivatePage(mainForm.LeftMenu_Active);
        }


        public static void Register_Machine(this MainForm mainForm)
        {
            if(App_Vals.Transceiver == null || !App_Vals.Transceiver.Socket.Connected)
            {
                mainForm.Display_NotificationClientOffline();
                return;
            }
            mainForm.Page_MachineRegistry.Enabled = false;
            string Name = mainForm.Page_MachineRegistry.txtMachineName.Text;

            App_Vals.MachineInfo_Local = new MachineInfo();
            App_Vals.MachineInfo_Local.Name = Name;
            Tuple<string, string> RSAKeys = Crypt.GenerateRSAKeys(4096);
            App_Vals.MachineInfo_Local.PublicKey = RSAKeys.Item1;
            App_Vals.MachineInfo_Local.PrivateKey = RSAKeys.Item2;

            Discourse d = App_Vals.Transceiver.OpenNewDiscourse((TC, DC, TM) => { Finish_RegisterMachine(mainForm, TC, DC, TM); });
            Transmission t = new Transmission(d.Info.ID);
            t.Add("function_name", "register_machine");
            t.Add("machine_name", Name);
            t.Add("machine_public_key", App_Vals.MachineInfo_Local.PublicKey);
            App_Vals.Transceiver.SendTransmission(t, AS_Vals.AS_PublicKey);
        }
        private static void Finish_RegisterMachine(MainForm mainForm, Transceiver transceiver, Discourse discourse, Transmission transmission)
        {
            mainForm.Invoke((MethodInvoker)(() =>
            {
                string FunctionName = (string)transmission["function_name"];
                if (FunctionName == "function_success")
                {
                    string MachineID = (string)transmission["machine_id"];
                    App_Vals.MachineInfo_Local.ID = MachineID;
                }
                else
                {
                    bool[] ErrorFlags = (bool[])transmission["function_flags"];
                    string ErrorDesc = "";
                    ErrorDesc += ErrorFlags[0] ? "Machine name was not supplied\n" : "";
                    ErrorDesc += ErrorFlags[1] ? "Machine public key was not supplied\n" : "";
                    NotificationDialog n = new NotificationDialog("Error Registering Machine", ErrorDesc, new Panel() { BackColor = Color.Yellow });
                    mainForm.pnlNotifications.Controls.Add(n);
                }
            }));
        }


        public static void Register_Account(this MainForm mainForm)
        {
            if (App_Vals.Transceiver == null || !App_Vals.Transceiver.Socket.Connected)
            {
                mainForm.Display_NotificationClientOffline();
                return;
            }
            mainForm.Page_MachineRegistry.Enabled = false;
            
        }
        private static void Finish_RegisterAccount(MainForm mainForm, Transceiver transceiver, Discourse discourse, Transmission transmission)
        {
        }
    }

    public class AvailablePage
    {
        public Control Page;
        public Control PageOption;
        public AvailablePage(Control Page)
        {
            this.Page = Page;
        }
    }
}

namespace UILayout
{
    public class ControlLayout
    {
        public Size Size;
        public List<int> HorizontalGridLines;
        public List<int> VerticalGridLines;

        public ControlLayout(Size size, List<int> verticalGridLines, List<int> horizontalGridLines)
        {
            this.Size = size;
            this.HorizontalGridLines = horizontalGridLines;
            this.VerticalGridLines = verticalGridLines;
        }

        public List<List<Rectangle>> GetGridSpaces()
        {
            List<List<Rectangle>> Result = new List<List<Rectangle>> { };
            for (int i = 0; i + 1 < HorizontalGridLines.Count; i++) // Y
            {
                List<Rectangle> Row = new List<Rectangle> { };
                for (int j = 0; j + 1 < VerticalGridLines.Count; j++) // X
                {
                     Row.Add(new Rectangle(VerticalGridLines[j], HorizontalGridLines[i], VerticalGridLines[j + 1] - VerticalGridLines[j], HorizontalGridLines[i + 1] - HorizontalGridLines[i]));
                }
                Result.Add(Row);
            }
            return Result;
        }

        public void VisualizeGridSpaces(Control parent)
        {
            Graphics g = parent.CreateGraphics();
            for (int i = 0; i < VerticalGridLines.Count; i++)
            {
                g.DrawLine(new Pen(Color.Red, 4), VerticalGridLines[i], 0, VerticalGridLines[i], parent.Height);
                g.DrawString(i.ToString(), new Font("Segoe UI", 12), Brushes.Yellow, new Point(VerticalGridLines[i], 0));
            }
            for (int i = 0; i < HorizontalGridLines.Count; i++)
            {
                g.DrawLine(new Pen(Color.Red, 4), 0, HorizontalGridLines[i], parent.Width, HorizontalGridLines[i]);
                g.DrawString(i.ToString(), new Font("Segoe UI", 12), Brushes.Yellow, new Point(0, HorizontalGridLines[i]));
            }
        }
    }

    public class PageController : Panel
    {
        public void ActivatePage(Control page) 
        {
            DeactivatePage();
            page.Bounds = GetUsableBounds();
            page.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            Controls.Add(page);
        }

        public void DeactivatePage()
        {
            if (Controls.Count > 0)
            {
                Controls.Clear();
            }
        }

        public Rectangle GetUsableBounds()
        {
            return new Rectangle(0, 0, Width, Height);
        }
    }
}
