using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Affix_Center
{
    public partial class Page_TermsAndConditions : UserControl
    {
        public Page_TermsAndConditions()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            App_Processor.Open_TermsAndConditions();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            App_Processor.CallBack_AcceptTermsAndConditions();
        }
    }
}
