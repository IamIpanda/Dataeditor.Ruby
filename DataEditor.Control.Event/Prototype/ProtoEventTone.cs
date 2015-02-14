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
    public partial class ProtoEventTone : UserControl
    {
        public enum ToneMode { Color, Tone };
        protected ToneMode mode;
        public ToneMode Mode { get { return mode; } set { mode = value; OnModeChanged(); } }
        public ProtoEventTone()
        {
            InitializeComponent();
            this.mode = ToneMode.Tone;
        }
        public void OnModeChanged()
        { 
            if (mode == ToneMode.Color)
            {
                tb_red.Minimum = 0;
                tb_green.Minimum = 0;
                tb_blue.Minimum = 0;
                label4.Text = "Alpha：";
                nud_red.Minimum = nud_green.Minimum = nud_blue.Minimum = 0;
            }
            else
            {
                tb_red.Minimum = -255;
                tb_green.Minimum = -255;
                tb_blue.Minimum = -255;
                nud_red.Minimum = nud_green.Minimum = nud_blue.Minimum = -255;
                label4.Text = "灰：";
                protoColorSpectrum1.BackColor = null;
            }
        }
        public int Value1 { get { return tb_red.Value; } set { tb_red.Value = value; } }
        public int Value2 { get { return tb_green.Value; } set { tb_green.Value = value; } }
        public int Value3 { get { return tb_blue.Value; } set { tb_blue.Value = value; } }
        public int Value4 { get { return tb_gray.Value; } set { tb_gray.Value = value; } }

        private void tb_scroll(object sender, EventArgs e)
        {
            nud_red.Value = tb_red.Value;
            nud_green.Value = tb_green.Value;
            nud_blue.Value = tb_blue.Value;
            nud_gray.Value = tb_gray.Value;
            SetColor();
        }

        private void nud_ValueChanged(object sender, EventArgs e)
        {
            tb_red.Value = (int)nud_red.Value;
            tb_green.Value = (int)nud_green.Value;
            tb_blue.Value = (int)nud_blue.Value;
            tb_gray.Value = (int)nud_gray.Value;
            SetColor();
        }

        void SetColor()
        {
            if (mode == ToneMode.Color)
                protoColorSpectrum1.BackColor = Color.FromArgb((int)nud_gray.Value, (int)nud_red.Value, (int)nud_green.Value, (int)nud_blue.Value);
            else
            {
                protoColorSpectrum1.Fileter.ToneRed = (int)nud_red.Value;
                protoColorSpectrum1.Fileter.ToneGreen = (int)nud_green.Value;
                protoColorSpectrum1.Fileter.ToneBlue = (int)nud_blue.Value;
                protoColorSpectrum1.Fileter.ToneGray = (int)nud_gray.Value;
            }
            protoColorSpectrum1.Invalidate();
        }
    }
}
