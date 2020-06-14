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
    public partial class Page_MachineRegistry : UserControl
    {
        public Page_MachineRegistry()
        {
            InitializeComponent();
        }

        private void btnRegisterMachine_Click(object sender, EventArgs e)
        {
            Transceiver_Processor.Register_Machine();
        }
    }
}
