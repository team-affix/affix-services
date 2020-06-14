using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Affix_Center
{
    public partial class FileReceive : Form
    {
        Timer t = new Timer();
        public FileReceive()
        {
            InitializeComponent();
        }
        
        private void lblSend_MouseEnter(object sender, EventArgs e)
        {
            pnlSend.BackgroundImage = Properties.Resources.ListenSelected;
        }

        private void lblSend_MouseLeave(object sender, EventArgs e)
        {
            pnlSend.BackgroundImage = Properties.Resources.Listen;
        }

        private void lblSend_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lstFilesAdded_MouseDown(object sender, MouseEventArgs e)
        {
            if (lstFilesAdded.SelectedIndex > -1)
            {
                if (e.Button == MouseButtons.Right)
                {
                    int selected = lstFilesAdded.SelectedIndex;
                    lstFilesAdded.Items.RemoveAt(selected);
                    Cloudwire.filereceivelist.RemoveAt(selected);
                }
            }
        }

        private void FileReceive_Load(object sender, EventArgs e)
        {
            foreach(List<object> file in Cloudwire.filereceivelist)
            {
                lstFilesAdded.Items.Add(file[0] as string);
            }
        }
    }
}
