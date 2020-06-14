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
    public partial class Dialog_Header : UserControl
    {
        public string Title { get { return _Title; } set { _Title = value; this.lblTitle.Text = value; } }
        public string Subtitle { get { return _Subtitle; } set { _Subtitle = value; this.lblSubtitle.Text = value; } }

        string _Title;
        string _Subtitle;

        public Dialog_Header()
        {
            InitializeComponent();
        }
        public Dialog_Header(string Title, string Subtitle)
        {
            InitializeComponent();
            this.lblTitle.Text = Title;
            this.lblSubtitle.Text = Subtitle;
        }
    }
}
