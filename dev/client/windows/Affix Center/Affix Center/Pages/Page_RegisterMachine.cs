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

namespace Affix_Center.Forms
{
    public partial class Page_RegisterMachine : UserControl
    {
        public Page_RegisterMachine()
        {
            InitializeComponent();
            App.Vals.Action_ProgramColorsChanged += ProgramColorsChanged;
            txtMachineName.textBox.GotFocus += (x, y) =>
            {
                txtMachineName.textBox.ForeColor = App.Vals.ProgramColors_Active.SelectedColor;
                txtMachineName.panel.ForeColor = App.Vals.ProgramColors_Active.SelectedColor;
            };
            txtMachineName.textBox.LostFocus += (x, y) =>
            {
                txtMachineName.textBox.ForeColor = App.Vals.ProgramColors_Active.ParagraphColor;
                txtMachineName.panel.ForeColor = App.Vals.ProgramColors_Active.DeselectedColor;
            };
            txtMachineName.textBox.KeyDown += txtMachineName_KeyDown;
        }

        private void Page_RegisterMachine_Load(object sender, EventArgs e)
        {
            ProgramColorsChanged();
        }

        void ProgramColorsChanged()
        {
            BackColor = App.Vals.ProgramColors_Active.BackColor;
            List<RoundedButton> RoundedButtons = new List<RoundedButton>
            {
                btnRegisterMachine,
            };
            List<Dialog_Header> Headers = new List<Dialog_Header>
            {
                hdr,
            };
            List<Default_TextBox> DefaultTextBoxes = new List<Default_TextBox>
            {
                txtMachineName,
            };
            List<Label> Titles = new List<Label>
            {
                lblTitle,
            };
            List<Label> Subtitles = new List<Label>
            {
                lblMachineName,
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
        }

        void txtMachineName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                Perform();
            }
        }

        private void btnRegisterMachine_MouseEnter(object sender, EventArgs e)
        {
            btnRegisterMachine.ForeColor = App.Vals.ProgramColors_Active.SelectedColor;
            btnRegisterMachine.TextColor = App.Vals.ProgramColors_Active.SelectedColor;
        }

        private void btnRegisterMachine_MouseLeave(object sender, EventArgs e)
        {
            btnRegisterMachine.ForeColor = App.Vals.ProgramColors_Active.DeselectedColor;
            btnRegisterMachine.TextColor = App.Vals.ProgramColors_Active.ParagraphColor;
        }

        private void btnRegisterMachine_Click(object sender, EventArgs e)
        {
            Perform();
        }

        void Perform()
        {
            btnRegisterMachine.Enabled = false;
            txtMachineName.Enabled = false;
            Action RM = () => { App.Methods.void_RegisterMachine(txtMachineName.textBox.Text, Fail, Succeed); };
            RM.Invoke();
        }

        void Fail(bool[] flags)
        {
            Invoke((MethodInvoker)(() =>
            {
                btnRegisterMachine.Enabled = true;
                txtMachineName.Enabled = true;
            }));
        }

        void Succeed()
        {
            Invoke((MethodInvoker)(() =>
            {
                btnRegisterMachine.Enabled = true;
                txtMachineName.Enabled = true;
            }));
        }
    }
}
