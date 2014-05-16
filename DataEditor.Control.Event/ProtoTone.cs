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
    public partial class ProtoTone : UserControl
    {
        public enum ToneMode { Color, Tone };
        protected ToneMode mode;
        public ToneMode Mode { get { return mode; } set { mode = value; OnModeChanged(); } }
        public ProtoTone()
        {
            InitializeComponent();
        }
        public void OnModeChanged()
        { 
            if (mode == ToneMode.Color)
            {
                trackBar1.Minimum = 0;
                trackBar2.Minimum = 0;
                trackBar3.Minimum = 0;
                label4.Text = "Alpha：";
            }
            else
            {
                trackBar1.Minimum = -255;
                trackBar2.Minimum = -255;
                trackBar3.Minimum = -255;
                label4.Text = "灰：";
            }
        }
        public int Value1 { get { return trackBar1.Value; } set { trackBar1.Value = value; } }
        public int Value2 { get { return trackBar2.Value; } set { trackBar2.Value = value; } }
        public int Value3 { get { return trackBar3.Value; } set { trackBar3.Value = value; } }
        public int Value4 { get { return trackBar4.Value; } set { trackBar4.Value = value; } }
    }
}
