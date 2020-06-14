using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Affix_Center.CustomControls;
using Affix_Center.Classes;
using static System.Windows.Forms.Control;

namespace Affix_Center.Forms
{
    public partial class Page_Settings : UserControl
    {
        public bool hasLoaded;
        public Page_Settings()
        {
            InitializeComponent();
        }

        private void Page_Settings_Load(object sender, EventArgs e)
        {
            if (!hasLoaded)
            {
                hasLoaded = true;
                StartUp();
            }
        }

        private void btn_MouseEnter(object sender, EventArgs e)
        {
            RoundedButton r = (RoundedButton)sender;
            r.ForeColor = App.Vals.ProgramColors_Active.SelectedColor;
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            RoundedButton r = (RoundedButton)sender;
            r.ForeColor = App.Vals.ProgramColors_Active.HUDColor2;
        }

        private void btn_Click(string key)
        {
            switch (key)
            {
                case App.Vals.string_MachineSettingsPageKey:
                    dsp.Display(App.Vals.Page_MachineSettings);
                    break;
                case App.Vals.string_AccountSettingsPageKey:
                    dsp.Display(App.Vals.Page_AccountSettings);
                    break;
            }
        }

        void StartUp()
        {
            Func<object, Control> ConstructOptionControl = key =>
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
            lstOptions.ConstructOptionControl = ConstructOptionControl;

            App.Vals.Action_ProgramColorsChanged += ProgramColorsChanged;
            App.Vals.Action_MachineAuthenticatedChanged += MachineAuthenticatedChanged;
            App.Vals.Action_AccountAuthenticatedChanged += AccountAuthenticatedChanged;
            ProgramColorsChanged();
            MachineAuthenticatedChanged();
            AccountAuthenticatedChanged();
        }

        void ProgramColorsChanged()
        {
            Invoke((MethodInvoker)(() =>
            {
                BackColor = App.Vals.ProgramColors_Active.BackColor;
                lstOptions.BackColor = App.Vals.ProgramColors_Active.BackColor;
                lstOptions.pnl.BackColor = App.Vals.ProgramColors_Active.HUDColor1;
                lstOptions.pnl.ForeColor = App.Vals.ProgramColors_Active.HUDColor1;
                List<Dialog_Header> Headers = new List<Dialog_Header>
                {
                    hdr,
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
                lstOptions.flp.Controls.ForEach(c =>
                {
                    RoundedButton r = (RoundedButton)c;
                    r.ForeColor = App.Vals.ProgramColors_Active.DeselectedColor;
                    r.TextColor = App.Vals.ProgramColors_Active.ParagraphColor;
                    r.BackColor = App.Vals.ProgramColors_Active.BackColor;
                });
                Headers.ForEach(h =>
                {
                    h.BackColor = App.Vals.ProgramColors_Active.BackColor;
                    h.lblTitle.ForeColor = App.Vals.ProgramColors_Active.TitleColor;
                    h.lblSubtitle.ForeColor = App.Vals.ProgramColors_Active.SubtitleColor;
                    h.pnlGradient.ColorStart = App.Vals.ProgramColors_Active.HUDColor1;
                    h.pnlGradient.ColorEnd = App.Vals.ProgramColors_Active.BackColor;
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
            }));
        }

        void MachineAuthenticatedChanged()
        {
            Invoke((MethodInvoker)(() =>
            {
                if (App.Vals.bool_MachineAuthenticated && !lstOptions.ContainsKey(App.Vals.string_MachineSettingsPageKey))
                {
                    lstOptions.Add(App.Vals.string_MachineSettingsPageKey);
                }
                else if (!App.Vals.bool_MachineAuthenticated && lstOptions.ContainsKey(App.Vals.string_MachineSettingsPageKey))
                {
                    lstOptions.Remove(App.Vals.string_MachineSettingsPageKey);
                }
            }));
        }

        void AccountAuthenticatedChanged()
        {
            Invoke((MethodInvoker)(() =>
            {
                if (App.Vals.bool_AccountAuthenticated && !lstOptions.ContainsKey(App.Vals.string_AccountSettingsPageKey))
                {
                    lstOptions.Add(App.Vals.string_AccountSettingsPageKey);
                }
                else if (!App.Vals.bool_AccountAuthenticated && lstOptions.ContainsKey(App.Vals.string_AccountSettingsPageKey))
                {
                    lstOptions.Remove(App.Vals.string_AccountSettingsPageKey);
                }
            }));
        }
    }

    public static class ControlCollectionExtensions
    {
        public static void ForEach(this ControlCollection controlCollection, Action<Control> action)
        {
            for(int i = 0; i < controlCollection.Count; i++)
            {
                action(controlCollection[i]);
            }
        }
    }
}
