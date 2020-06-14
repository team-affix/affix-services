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
using Affix_Center.Forms;
using Affix_Center.Classes;

namespace Affix_Center
{
    public partial class Form_Main : Form
    {
        public bool hasLoaded;
        public bool ConsoleActive;

        public Form_Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (!hasLoaded)
            {
                hasLoaded = true;
                StartUp();
            }
        }

        void StartUp()
        {
            lstOptions.flp.FlowDirection = FlowDirection.LeftToRight;
            lstOptions.ConstructOptionControl = key =>
            {
                RoundedButton OptionButton = new RoundedButton
                {
                    BackColor = App.Vals.ProgramColors_Active.HUDColor2,
                    ForeColor = App.Vals.ProgramColors_Active.HUDColor2,
                    TextColor = App.Vals.ProgramColors_Active.TitleColor,
                    Text = (string)key,
                    BorderWidth = 2,
                    Radius = 5,
                    Font = new Font("Segoe UI", 10),
                    Size = new Size(150, 40),
                };
                OptionButton.MouseEnter += btn_MouseEnter;
                OptionButton.MouseLeave += btn_MouseLeave;
                OptionButton.Click += (Sender, E) => { btn_Click((string)key); };
                return OptionButton;
            };

            App.Vals.Action_ProgramColorsChanged += () => { Invoke((MethodInvoker)ProgramColorsChanged); };
            App.Vals.Action_AvailablePagesChanged += () => { Invoke((MethodInvoker)AvailablePagesChanged); }; ;
            ProgramColorsChanged();
        }

        void ProgramColorsChanged()
        {
            BackColor = App.Vals.ProgramColors_Active.BackColor;
            lstOptions.BackColor = App.Vals.ProgramColors_Active.BackColor;
            lstOptions.pnl.BackColor = App.Vals.ProgramColors_Active.HUDColor1;
            lstOptions.pnl.ForeColor = App.Vals.ProgramColors_Active.HUDColor1;
            btnDismiss.BackColor = App.Vals.ProgramColors_Active.SelectedColor;
            List<RoundedButton> RoundedButtonsHUD2 = new List<RoundedButton>
            {
                btnShowConsole,
            };
            List<RoundedButton> RoundedButtonsHUD3 = new List<RoundedButton>
            {
                btnDismiss,
            };
            List<Default_TextBox> DefaultTextBoxes = new List<Default_TextBox>
            {

            };
            List<Label> Titles = new List<Label>
            {

            };
            List<Label> Subtitles = new List<Label>
            {

            };
            List<Label> Paragraphs = new List<Label>
            {

            };
            List<Panel> Panels = new List<Panel>
            {
                pnlTeamLogo,
                pnlNotifications
            };
            List<CheckedListBox> CheckedListBoxes = new List<CheckedListBox>
            {
                lstConsole,
            };
            List<RoundedPanel> RoundedPanelsHUD1 = new List<RoundedPanel>
            {
                pnlConsole,
                //pnlSideBar,
            };
            List<RoundedPanel> RoundedPanelsHUD2 = new List<RoundedPanel>
            {
                pnlConsoleOptions,
                pnlLoadingItems,
            };
            List<RoundedPanel> RoundedPanelsHUD3 = new List<RoundedPanel>
            {

            };
            RoundedButtonsHUD2.ForEach(r =>
            {
                r.ForeColor = App.Vals.ProgramColors_Active.HUDColor2;
                r.TextColor = App.Vals.ProgramColors_Active.TitleColor;
                r.BackColor = App.Vals.ProgramColors_Active.HUDColor2;
            });
            RoundedButtonsHUD3.ForEach(r =>
            {
                r.ForeColor = App.Vals.ProgramColors_Active.HUDColor3;
                r.TextColor = App.Vals.ProgramColors_Active.TitleColor;
                r.BackColor = App.Vals.ProgramColors_Active.HUDColor3;
            });
            DefaultTextBoxes.ForEach(t =>
            {
                t.BackColor = App.Vals.ProgramColors_Active.BackColor;
                t.textBox.BackColor = App.Vals.ProgramColors_Active.BackColor;
                t.textBox.ForeColor = App.Vals.ProgramColors_Active.ParagraphColor;
                t.panel.ForeColor = App.Vals.ProgramColors_Active.DeselectedColor;
            });
            Titles.ForEach(l =>
            {
                l.ForeColor = App.Vals.ProgramColors_Active.TitleColor;
            });
            Subtitles.ForEach(l =>
            {
                l.ForeColor = App.Vals.ProgramColors_Active.SubtitleColor;
            });
            Paragraphs.ForEach(l =>
            {
                l.ForeColor = App.Vals.ProgramColors_Active.ParagraphColor;
            });
            Panels.ForEach(p =>
            {
                p.BackColor = App.Vals.ProgramColors_Active.BackColor;
            });
            CheckedListBoxes.ForEach(c =>
            {
                c.BackColor = App.Vals.ProgramColors_Active.HUDColor2;
                c.ForeColor = App.Vals.ProgramColors_Active.TitleColor;
            });
            RoundedPanelsHUD1.ForEach(r =>
            {
                r.BackColor = App.Vals.ProgramColors_Active.HUDColor1;
                r.ForeColor = App.Vals.ProgramColors_Active.HUDColor1;
            });
            RoundedPanelsHUD2.ForEach(r =>
            {
                r.BackColor = App.Vals.ProgramColors_Active.HUDColor2;
                r.ForeColor = App.Vals.ProgramColors_Active.HUDColor2;
            });
            RoundedPanelsHUD3.ForEach(r =>
            {
                r.BackColor = App.Vals.ProgramColors_Active.HUDColor3;
                r.ForeColor = App.Vals.ProgramColors_Active.HUDColor3;
            });
        }

        void AvailablePagesChanged()
        {
            if (App.Vals.List_AvailablePages.Contains(App.Vals.string_HomePageKey) && !lstOptions.ContainsKey(App.Vals.string_HomePageKey))
            {
                lstOptions.Add(App.Vals.string_HomePageKey);
            }
            if (!App.Vals.List_AvailablePages.Contains(App.Vals.string_HomePageKey) && lstOptions.ContainsKey(App.Vals.string_HomePageKey))
            {
                lstOptions.Remove(App.Vals.string_HomePageKey);
            }
            if (App.Vals.List_AvailablePages.Contains(App.Vals.string_SettingsPageKey) && !lstOptions.ContainsKey(App.Vals.string_SettingsPageKey))
            {
                lstOptions.Add(App.Vals.string_SettingsPageKey);
            }
            if (!App.Vals.List_AvailablePages.Contains(App.Vals.string_SettingsPageKey) && lstOptions.ContainsKey(App.Vals.string_SettingsPageKey))
            {
                lstOptions.Remove(App.Vals.string_SettingsPageKey);
            }
            if (App.Vals.List_AvailablePages.Contains(App.Vals.string_HostPageKey) && !lstOptions.ContainsKey(App.Vals.string_HostPageKey))
            {
                lstOptions.Add(App.Vals.string_HostPageKey);
            }
            if (!App.Vals.List_AvailablePages.Contains(App.Vals.string_HostPageKey) && lstOptions.ContainsKey(App.Vals.string_HostPageKey))
            {
                lstOptions.Remove(App.Vals.string_HostPageKey);
            }
        }

        private void Main_Shown(object sender, EventArgs e)
        {

        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {

        }

        private void btn_MouseEnter(object sender, EventArgs e)
        {
            RoundedButton b = (RoundedButton)sender;
            b.ForeColor = App.Vals.ProgramColors_Active.SelectedColor;
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            RoundedButton b = (RoundedButton)sender;
            b.ForeColor = App.Vals.ProgramColors_Active.HUDColor2;
        }

        private void btn_Click(string key)
        {
            switch (key)
            {
                case App.Vals.string_HomePageKey:
                    dsp.Display(App.Vals.Page_ClientHome);
                    break;
                case App.Vals.string_SettingsPageKey:
                    dsp.Display(App.Vals.Page_Settings);
                    break;
                case App.Vals.string_HostPageKey:
                    dsp.Display(App.Vals.Page_Host);
                    break;
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            App.Vals.Form_Main.dsp.Display(App.Vals.Page_Settings);
        }

        private void btnShowConsole_Click(object sender, EventArgs e)
        {
            ToggleConsoleActive();
        }

        void ToggleConsoleActive()
        {
            if (!ConsoleActive)
            {
                pnlConsole.Top = Height - 490;
                pnlConsole.Height = 439;
                btnShowConsole.Text = "▼ Hide Console";
                ConsoleActive = true;
            }
            else
            {
                pnlConsole.Top = Height - 104;
                pnlConsole.Height = 53;
                btnShowConsole.Text = "▲ Show Console";
                ConsoleActive = false;
            }
        }

        private void btnDismiss_Click(object sender, EventArgs e)
        {
            for(int i = lstConsole.CheckedItems.Count - 1; i >= 0; i--)
            {
                lstConsole.Items.Remove(lstConsole.CheckedItems[i]);
            }
        }

        private void Form_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
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
    
    public class DisplayPanel : Panel
    {
        public Control ActiveControl;

        public DisplayPanel()
        {
            DoubleBuffered = true;
        }

        public void Display(Control control) 
        {
            Clear();
            ActiveControl = control;
            control.Bounds = GetUsableBounds();
            control.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            Controls.Add(control);
        }

        public void Clear()
        {
            if (Controls.Count > 0)
            {
                Controls.Clear();
            }
            ActiveControl = null;
        }

        public Rectangle GetUsableBounds()
        {
            return new Rectangle(0, 0, Width, Height);
        }
    }
    
}
