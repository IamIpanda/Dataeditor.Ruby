using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public partial class ProtoTitleBox : UserControl
    {
        Brush BackBrush;
        Brush TextBrush;
        StringFormat Format;
        public ProtoTitleBox()
        {
            InitializeComponent();
            this.Text = "样例";
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            BackBrush = GetBackBrush();
            TextBrush = GetForeBrush();
            SetFormat();
        }
        [Browsable(true)]
        [EditorBrowsable]
        public new string Text { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.FillRectangle(BackBrush, e.ClipRectangle);
            e.Graphics.DrawString(Text, Font, TextBrush, e.ClipRectangle, Format);
        }
        protected virtual Brush GetBackBrush()
        {
            return new System.Drawing.Drawing2D.LinearGradientBrush(
                          this.ClientRectangle,
                          Color.FromArgb(2, 81, 161),
                          Color.FromArgb(77, 118, 198),
                          System.Drawing.Drawing2D.LinearGradientMode.Vertical
                          );
        }
        protected virtual Brush GetForeBrush()
        {
            return new SolidBrush(ForeColor);
        }
        protected virtual void SetFormat()
        {
            Format = new StringFormat();
            Format.Alignment = StringAlignment.Center;
            Format.LineAlignment = StringAlignment.Center;
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            BackBrush = GetBackBrush();
            base.OnSizeChanged(e);
        }
        protected override void OnForeColorChanged(EventArgs e)
        {
            TextBrush = GetForeBrush();
            base.OnForeColorChanged(e);
        }
    }
}
