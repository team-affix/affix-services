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
using AffixServices.Data;

namespace Affix_Center.Pages
{
    public partial class Dialog_SelectableP2PContract : UserControl
    {
        public Contract Contract;

        public Dialog_SelectableP2PContract(Contract contract)
        {
            this.Contract = contract;
            InitializeComponent();
            txtMachineID.Text = contract.ID;
            txtMachineName.Text = contract.Name;
            txtMachineStatus.Text = (!contract.Accepted).ToString();
            if(contract.Initiated || contract.Accepted)
            {
                Height = 102;
            }
        }

        private void Dialog_SelectableMachine_Load(object sender, EventArgs e)
        {
            ProgramColorsChanged();
            App.Vals.Action_ProgramColorsChanged += ProgramColorsChanged;
        }

        void ProgramColorsChanged()
        {
            BackColor = App.Vals.ProgramColors_Active.HUDColor1;
            pnlMain.BackColor = App.Vals.ProgramColors_Active.HUDColor2;
            pnlMain.ForeColor = App.Vals.ProgramColors_Active.HUDColor2;
            List<RoundedButton> RoundedButtons = new List<RoundedButton>
            {
                btnSelect,
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
                r.ForeColor = App.Vals.ProgramColors_Active.HUDColor2;
                r.TextColor = App.Vals.ProgramColors_Active.TitleColor;
                r.BackColor = App.Vals.ProgramColors_Active.HUDColor2;
            });
            Titles.ForEach(l =>
            {
                l.ForeColor = App.Vals.ProgramColors_Active.TitleColor;
            });
            TextBoxes.ForEach(t =>
            {
                t.BackColor = App.Vals.ProgramColors_Active.HUDColor2;
                t.ForeColor = App.Vals.ProgramColors_Active.SubtitleColor;
            });
        }

        private void lblStatusTitle_Click(object sender, EventArgs e)
        {

        }

        private void lblNameTitle_Click(object sender, EventArgs e)
        {

        }

        private void lblIDTitle_Click(object sender, EventArgs e)
        {

        }

        private void txtMachineID_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMachineName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSelect_MouseEnter(object sender, EventArgs e)
        {
            btnSelect.ForeColor = App.Vals.ProgramColors_Active.SelectedColor;
        }

        private void btnSelect_MouseLeave(object sender, EventArgs e)
        {
            btnSelect.ForeColor = App.Vals.ProgramColors_Active.HUDColor2;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {

        }
    }
}
