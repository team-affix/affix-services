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
    public partial class Page_AffixServicesDetails : UserControl
    {
        public Page_AffixServicesDetails()
        {
            InitializeComponent();
        }

        private void Page_AffixServicesDetails_Load(object sender, EventArgs e)
        {

        }

        private void btnOpenTermsAndConditions_Click(object sender, EventArgs e)
        {
            App_Processor.Open_TermsAndConditions();
        }

        private void btnOpenTermsOfManagement_Click(object sender, EventArgs e)
        {
            App_Processor.Open_TermsOfManagement();
        }

        private void btnOpenSSFTest_Click(object sender, EventArgs e)
        {
            App_Processor.Open_SSFTest();
        }
    }
}
