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
    public partial class PageHeader : UserControl
    {
        public PageHeader(string Title, string Subtitle)
        {
            InitializeComponent();
            this.lblTitle.Text = Title;
            this.lblSubtitle.Text = Subtitle;
        }
    }
}
