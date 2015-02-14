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
    public partial class ProtoColorSpectrum : UserControl, DataEditor.Control.Prototype.ProtoFrameControl
    {
        public Help.Filter.ToneFilter Fileter { get; set; }
        public new Color? BackColor { get; set; }
        public ProtoColorSpectrum()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.Selectable, false);
            this.Fileter = new Help.Filter.ToneFilter(0, 0, 0, 0);
            this.BackColor = null;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if(this.BackColor != null)
            {
                e.Graphics.FillRectangle(new SolidBrush(this.BackColor.Value), e.ClipRectangle);
                return;
            }
            ProtoFrameControlHelp.DrawBorder(e.Graphics, e.ClipRectangle, Color.Black);
            Bitmap bm = new Bitmap(e.ClipRectangle.Width - 2, e.ClipRectangle.Height - 2);
            float brigntness_step = 1F / bm.Width;
            float hue_step = 360F / bm.Height;
            for (int i = 0; i < bm.Height; i++)
                for (int j = 0; j < bm.Width; j++)
                {
                    Help.Filter.HSLtoRGBConverter.ConvertToRGB(hue_step * i, 1F, (brigntness_step * j));
                    this.Fileter.ChangeTone((int)Help.Filter.HSLtoRGBConverter.temp_result_r,
                        (int)Help.Filter.HSLtoRGBConverter.temp_result_g,
                        (int)Help.Filter.HSLtoRGBConverter.temp_result_b);
                    bm.SetPixel(j, i, Color.FromArgb(Fileter.temp_result_r, Fileter.temp_result_g,Fileter.temp_result_b));
                }
            e.Graphics.DrawImage(bm, 1F, 1F);
        }
    }
}
