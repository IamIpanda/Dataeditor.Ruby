using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public class ProtoColoredListbox : ProtoListBox
    {
    
        public ProtoColoredListbox()
        {
            this.RightShift = 18;
        }
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);
            if (e.Index < 0 || Items.Count == 0) return;
            var color = GetColor(e.Index);
            if (color == default(Color)) return;
            e.Graphics.FillRectangle(
                ProtoListControlHelp.GetBrush(color),
                GetCircleRect(e));
            
        }
        protected virtual Rectangle GetCircleRect(DrawItemEventArgs e)
        {
            return new Rectangle(e.Bounds.X + 1, e.Bounds.Y + 1, ItemHeight - 2, ItemHeight - 2);
        }
        protected virtual Color GetColor(int index)
        {
            return default(Color);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
    }
    public class ProtoRtpList : ProtoColoredListbox
    {
        protected override Color GetColor(int index)
        {
            if (index < 0 || Items.Count == 0) return default(Color);
            object obj = Items[index];
            if (!(obj is Help.Rtp)) return base.GetColor(index);
            var rtp = (Help.Rtp)obj;
            return rtp.Color;
        }
    }
    public class ProtoPainterList : ProtoColoredListbox
    {
        public ProtoPainterList()
        {
            this.RightShift = 32;
        }
        protected Help.Palette palette;
        public Help.Palette Palette 
        {
            get { return palette; }
            set { palette = value; ReloadPalette(); }
        }
        protected override Color GetColor(int index)
        {
            if (palette == null) return default(Color);
            if (index < 0 || Items.Count == 0) return default(Color);
            Color? color = palette[index + 1];
            return color ?? default(Color);
        }
        public void ReloadPalette()
        {
            Items.Clear();
            for (int i = 1; i <= 25; i++)
                Items.Add("颜色 " + i.ToString());
        }
        protected override Rectangle GetCircleRect(DrawItemEventArgs e)
        {
            return new Rectangle(e.Bounds.X + 1, e.Bounds.Y + 1, 28, ItemHeight - 2);
        }
    }
}
