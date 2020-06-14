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

namespace Affix_Center.Forms
{
    public partial class Dialog_Selection : UserControl
    {
        public List<Tuple<string, Action, Color>> Options = new List<Tuple<string, Action, Color>> { };
        public Dialog_Selection(string Title, string Description)
        {
            InitializeComponent();
            lblTitle.Text = Title;
            lblDescription.Text = Description;
        }

        private void Dialog_Selection_Load(object sender, EventArgs e)
        {
            App.Vals.Action_ProgramColorsChanged += ProgramColorsChanged;
            OptionsChanged();
            ProgramColorsChanged();
        }

        void ProgramColorsChanged()
        {
            BackColor = App.Vals.ProgramColors_Active.BackColor;
            pnlInfo.BackColor = App.Vals.ProgramColors_Active.HUDColor1;
            pnlInfo.ForeColor = App.Vals.ProgramColors_Active.BackColor;
            pnlOptions.BackColor = App.Vals.ProgramColors_Active.HUDColor1;
            pnlOptions.ForeColor = App.Vals.ProgramColors_Active.BackColor;
            lblTitle.ForeColor = App.Vals.ProgramColors_Active.TitleColor;
            lblDescription.ForeColor = App.Vals.ProgramColors_Active.ParagraphColor;
        }

        void OptionsChanged()
        {
            flpOptions.Controls.Clear();
            for (int i = 0; i < Options.Count; i++)
            {
                string Text = Options[i].Item1;
                Action Action = Options[i].Item2;
                Color Color = Options[i].Item3;
                RoundedButton button = new RoundedButton() { Text = Text, BackColor = Color, ForeColor = App.Vals.ProgramColors_Active.HUDColor1, Size = new Size(200, 40), BorderWidth = 2, Radius = 5 };
                button.MouseEnter += btn_MouseEnter;
                button.MouseLeave += btn_MouseLeave;
                button.Click += (sender, e) => { Action(); };
                flpOptions.Controls.Add(button);
            }
        }

        private void btn_MouseEnter(object sender, EventArgs e)
        {
            RoundedButton b = (RoundedButton)sender;
            b.ForeColor = App.Vals.ProgramColors_Active.SelectedColor;
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            RoundedButton b = (RoundedButton)sender;
            b.ForeColor = App.Vals.ProgramColors_Active.HUDColor3;
        }
    }
}
