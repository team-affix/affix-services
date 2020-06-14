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
    public partial class LocalFileTransfer : Form
    {
        public static List<string> filenamelist = new List<string> { };
        public LocalFileTransfer()
        {
            InitializeComponent();
            
        }
        


        private void pnlDragAndDrop_DragDrop(object sender, DragEventArgs e)
        {
            List<string[]> filesadded = new List<string[]> { };
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string str in s)
            {
                filesadded.Add(new string[] { str });
            }
            lstFilesAdded.Items.Clear();
            foreach (string[] file in filesadded)
            {
                lstFilesAdded.Items.Add(file[0]);
                filenamelist.Add(file[0]);
            }
            lblSend.Text = "Continue" + " (" + filesadded.Count.ToString() + " files)";
        }

        private void pnlDragAndDrop_DragEnter(object sender, DragEventArgs e)
        {
            pnlDragAndDrop.BackColor = Color.White;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void pnlDragAndDrop_MouseLeave(object sender, EventArgs e)
        {

        }

        private void pnlSend_MouseEnter(object sender, EventArgs e)
        {
            pnlSend.BackgroundImage = Properties.Resources.ListenSelected;
        }

        private void pnlSend_MouseLeave(object sender, EventArgs e)
        {
            pnlSend.BackgroundImage = Properties.Resources.Listen;
        }

        private void lblSend_Click(object sender, EventArgs e)
        {
            Cloudwire.filelocationlist = filenamelist;
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
                    filenamelist.RemoveAt(selected);
                    lblSend.Text = "Continue" + " (" + filenamelist.Count.ToString() + " files)";
                }
            }
        }
    }
}
