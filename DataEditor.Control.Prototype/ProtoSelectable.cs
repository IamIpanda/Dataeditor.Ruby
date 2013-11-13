using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public partial class ProtoSelectable : UserControl, ProtoFrameControl
    {
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        public override string Text { get; set; }
        protected StringFormat Format;
        public ProtoSelectable ()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.Text = "TEST";
            this.Format = new StringFormat();
            this.Format.Alignment = StringAlignment.Center;
            this.Format.LineAlignment = StringAlignment.Center;
           
        }

        private void ProtoSelectable_Enter (object sender, EventArgs e)
        {
            Invalidate();
        }

        private void ProtoSelectable_Leave (object sender, EventArgs e)
        {
            Invalidate();
        }

        private void ProtoSelectable_Paint (object sender, PaintEventArgs e)
        {
            if ( this.Focused )
            {
                Brush back = ProtoListControlHelp.GetFocusBrush(e.ClipRectangle, BackColor);
                e.Graphics.FillRectangle(back, e.ClipRectangle);
                ProtoFrameControlHelp.DrawFocusRectangle(e.Graphics, e.ClipRectangle);
                e.Graphics.DrawString(Text, Font, new SolidBrush(Color.White), e.ClipRectangle, Format);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.White), e.ClipRectangle);
                e.Graphics.DrawString(Text, Font, new SolidBrush(this.ForeColor), e.ClipRectangle, Format);
            }
        }
    }
}
