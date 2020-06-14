using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Affix_Center
{
    public partial class NotificationDialog : UserControl
    {
        public NotificationDialog(string Title, string Desc, Control Icon)
        {
            InitializeComponent();
            this.lblTitle.Text = Title;
            this.lblDesc.Text = Desc;
            Icon.Size = pnlIcon.Size;
            pnlIcon.Controls.Add(Icon);
        }

        private void lblDesc_Click(object sender, EventArgs e)
        {

        }

        private void NotificationDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
