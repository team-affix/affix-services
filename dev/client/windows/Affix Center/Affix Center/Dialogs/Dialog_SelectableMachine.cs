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

namespace Affix_Center.Pages
{
    public partial class Dialog_SelectableMachine : UserControl
    {
        public Dialog_SelectableMachine(string MachineID, string MachineName, string Status)
        {
            InitializeComponent();
            txtMachineID.Text = MachineID;
            txtMachineName.Text = MachineName;
            txtMachineStatus.Text = Status;
        }

        private void Dialog_SelectableMachine_Load(object sender, EventArgs e)
        {
            ProgramColorsChanged();
            App.Vals.Action_ProgramColorsChanged += ProgramColorsChanged;
        }

        void ProgramColorsChanged()
        {
            BackColor = App.Vals.ProgramColors_Active.HUDColor;
            List<RoundedButton> RoundedButtons = new List<RoundedButton>
            {
                btnConnect,
            };
            List<Label> Titles = new List<Label>
            {
                lblIDTitle,
                lblNameTitle,
                lblStatusTitle,
            };
            List<TextBox> TextBoxes = new List<TextBox>
            {
                txtMachineID,
                txtMachineName,
                txtMachineStatus,
            };
            RoundedButtons.ForEach(r =>
            {
                r.ForeColor = App.Vals.ProgramColors_Active.DeselectedColor;
                r.TextColor = App.Vals.ProgramColors_Active.ParagraphColor;
                r.BackColor = App.Vals.ProgramColors_Active.BackColor;
            });
            Titles.ForEach(l =>
            {
                l.ForeColor = App.Vals.ProgramColors_Active.TitleColor;
            });
            TextBoxes.ForEach(t =>
            {
                t.BackColor = App.Vals.ProgramColors_Active.HUDColor;
                t.ForeColor = App.Vals.ProgramColors_Active.SubtitleColor;
            });
        }
    }
}
