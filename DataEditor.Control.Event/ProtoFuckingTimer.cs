using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public partial class ProtoFuckingTimer : UserControl
    {
        public ProtoFuckingTimer()
        {
            InitializeComponent();
        }
        public int Value1 { get { return (int)numericUpDown1.Value; } set { numericUpDown1.Value = value; } }
        public int Value2 { get { return (int)numericUpDown2.Value; } set { numericUpDown1.Value = value; } }
        public int MaxValue1
        {
            get { return (int)(numericUpDown1.Maximum); }
            set { numericUpDown1.Value = value; }
        }
        public int MaxValue2
        {
            get { return (int)(numericUpDown2.Maximum); }
            set { numericUpDown2.Maximum = value; }
        }
    }
}
