using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AffixServices
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Main.StartUp();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnToggleServer_Click(object sender, EventArgs e)
        {
            if (!MainServer.Vals.bool_ServerActive)
            {
                MainServer.Methods.void_StartMainServer();
                btnToggleServer.Text = "Shut Down Server";
            }
            else
            {
                MainServer.Methods.void_StopMainServer();
                btnToggleServer.Text = "Start Up Server";
            }
        }
    }
}
