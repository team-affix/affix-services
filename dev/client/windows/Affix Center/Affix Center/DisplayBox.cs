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

namespace Affix_Center
{
    public partial class DisplayBox : Form
    {
        public static List<object> displayinfo = new List<object> { };
        public static string displaytitle { get; set; }
        public DisplayBox()
        {
            InitializeComponent();
        }

        private void DisplayBox_Load(object sender, EventArgs e)
        {
            lblTitle.Text = displaytitle;
            foreach(List<object> info in displayinfo)
            {
                string desc = info[0] as string;
                bool clickable = info[1] as bool? ?? default(bool);
                lstInfo.Items.Add(desc);
            }
        }

        private void DisplayBox_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = lblNull;
        }

        private void lstInfo_Click(object sender, EventArgs e)
        {
            if(!(lstInfo.SelectedIndex == -1))
            {
                List<object> info = displayinfo[lstInfo.SelectedIndex] as List<object>;
                lstInfo.SelectedIndex = -1;
                if (info[1] as bool? ?? default(bool) == true)
                {
                    new Thread(() =>
                    {
                        var f = new Waitform();
                        f.ShowDialog();
                    }).Start();
                    (info[2] as Action)();
                    
                    Waitform.closeform = true;
                }
            }
        }
    }
}
