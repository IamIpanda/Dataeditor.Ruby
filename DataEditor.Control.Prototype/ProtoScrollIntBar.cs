using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public partial class ProtoScrollIntBar : UserControl
    {
        public ProtoScrollIntBar()
        {
            InitializeComponent();
        }

        private void ProtoScrollIntBar_SizeChanged(object sender, EventArgs e)
        {
            numericUpDown1.Location = new Point(0,
                (this.Height - numericUpDown1.Height) / 2);
        }
        public int MinValue
        {
            get { return trackBar1.Minimum; }
            set
            {
                numericUpDown1.Minimum = value;
                trackBar1.Minimum = value;
            }
        }
        public int MaxValue
        {
            get { return trackBar1.Maximum; }
            set 
            {
                numericUpDown1.Maximum = value;
                trackBar1.Maximum = value;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            trackBar1.Value = (int)numericUpDown1.Value;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            numericUpDown1.Value = trackBar1.Value;
        }

        public int value
        {
            get { return trackBar1.Value; }
            set 
            {
                trackBar1.Value = value;
                numericUpDown1.Value = value;
            }
        }
    }
}
