using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Affix_Center.Classes;
using Affix_Center.CustomControls;

namespace Affix_Center.Pages
{
    public partial class Page_AddMachine : UserControl
    {
        System.Windows.Forms.Timer Timer_Animate = new Timer();
        public Page_AddMachine()
        {
            InitializeComponent();
            App.Vals.Action_ProgramColorsChanged += ProgramColorsChanged;

            txtMachineID.textBox.KeyDown += (sender, e) => { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; Perform(); } };
        }

        private void Page_AddMachine_Load(object sender, EventArgs e)
        {
            ProgramColorsChanged();
        }

        void ProgramColorsChanged()
        {
            BackColor = App.Vals.ProgramColors_Active.BackColor;
            List<RoundedButton> RoundedButtons = new List<RoundedButton>
            {
                btnAddMachine,
            };
            List<Dialog_Header> Headers = new List<Dialog_Header>
            {
                hdr,
            };
            List<Default_TextBox> DefaultTextBoxes = new List<Default_TextBox>
            {
                txtMachineID,
            };
            List<Label> Titles = new List<Label>
            {

            };
            List<Label> Subtitles = new List<Label>
            {
                lblMachineID,
            };
            List<Label> Paragraphs = new List<Label>
            {
                lblDesc,
            };
            RoundedButtons.ForEach(r =>
            {
                r.ForeColor = App.Vals.ProgramColors_Active.DeselectedColor;
                r.TextColor = App.Vals.ProgramColors_Active.ParagraphColor;
                r.BackColor = App.Vals.ProgramColors_Active.BackColor;
            });
            Headers.ForEach(h =>
            {
                h.BackColor = App.Vals.ProgramColors_Active.BackColor;
                h.lblTitle.ForeColor = App.Vals.ProgramColors_Active.TitleColor;
                h.lblSubtitle.ForeColor = App.Vals.ProgramColors_Active.SubtitleColor;
                h.pnlGradient.ColorStart = App.Vals.ProgramColors_Active.HUDColor;
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
        }

        private void txt_Enter(object sender, EventArgs e)
        {
            Default_TextBox d = (Default_TextBox)sender;
            d.textBox.ForeColor = App.Vals.ProgramColors_Active.SelectedColor;
            d.panel.ForeColor = App.Vals.ProgramColors_Active.SelectedColor;
        }

        private void txt_Leave(object sender, EventArgs e)
        {
            Default_TextBox d = (Default_TextBox)sender;
            d.textBox.ForeColor = App.Vals.ProgramColors_Active.ParagraphColor;
            d.panel.ForeColor = App.Vals.ProgramColors_Active.DeselectedColor;
        }

        private void btn_MouseEnter(object sender, EventArgs e)
        {
            RoundedButton b = (RoundedButton)sender;
            b.ForeColor = App.Vals.ProgramColors_Active.SelectedColor;
            b.TextColor = App.Vals.ProgramColors_Active.SelectedColor;
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            RoundedButton b = (RoundedButton)sender;
            b.ForeColor = App.Vals.ProgramColors_Active.DeselectedColor;
            b.TextColor = App.Vals.ProgramColors_Active.ParagraphColor;
        }

        private void btnAddMachine_Click(object sender, EventArgs e)
        {
            Perform();
        }

        void Perform()
        {
            Enabled = false;
            lblError.Text = "";
            Timer_Animate = App.Methods.Timer_MakeLoadingAnimation(pnlLoading);
            Timer_Animate.Start();

            App.Methods.void_AddMachine(txtMachineID.textBox.Text, flags =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    Enabled = true;
                    Timer_Animate.Stop();
                    pnlLoading.CreateGraphics().Clear(pnlLoading.BackColor);
                    lblError.Text += flags[0] ? "Machine ID was incorrect." : "";
                    lblError.Text += flags[1] ? "This Machine ID has already been added to your account." : "";
                }));
            }, () =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    Enabled = true;
                    Timer_Animate.Stop();
                    pnlLoading.CreateGraphics().Clear(pnlLoading.BackColor);
                }));
            });
        }
    }
}
