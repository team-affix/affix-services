using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aurora.Generalization;

namespace Affix_Center.Pages
{
    public partial class Dialog_List : UserControl
    {
        public List<(object, Control)> Options = new List<(object, Control)> { };
        public Func<object, Control> ConstructOptionControl;
        public Dialog_List()
        {
            InitializeComponent();
        }

        public bool ContainsKey(object key)
        {
            return Options.Exists(x => x.Item1 == key);
        }

        public void Add(object key)
        {
            Control c = ConstructOptionControl(key);
            Options.Add((key, c));
            flp.Controls.Add(c);
        }

        public void Insert(int index, object key)
        {
            Control c = ConstructOptionControl(key);
            Options.Add((key, c));
            flp.Controls.Add(c);
            flp.Controls.SetChildIndex(c, index);
        }

        public void SetChildIndex(int index, object key)
        {
            flp.Controls.SetChildIndex(Options.Find(x => x.Item1 == key).Item2, index);
        }

        public void SetChildIndex(int index, Control control)
        {
            flp.Controls.SetChildIndex(control, index);
        }

        public void Remove(object key)
        {
            flp.Controls.Remove(Options.Find(x => x.Item1 == key).Item2);
            Options.RemoveAt(x => x.Item1 == key);
        }

        public void RemoveAt(int index)
        {
            Control control = flp.Controls[index];
            flp.Controls.Remove(control);
            Options.RemoveAt(x => x.Item2.Equals(control));
        }

        public void Clear()
        {
            Options.Clear();
            flp.Controls.Clear();
        }
    }
}
