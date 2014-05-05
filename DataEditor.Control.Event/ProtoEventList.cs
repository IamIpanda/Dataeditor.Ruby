using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public class ProtoEventList : Prototype.ProtoListBox
    {
        public delegate Color GetEventItemColorDelegate(int index);
        public delegate bool EnableItem(int index);
        public GetEventItemColorDelegate GetEventItemColor { get; set; }
        public EnableItem ItemEnabled { get; set; }
        protected override System.Drawing.Brush GetForeColor(System.Windows.Forms.DrawItemEventArgs e)
        {
            if (GetEventItemColor == null) return base.GetForeColor(e);
            DrawItemState state = e.State;
            if (GetFocused(state) && (ItemEnabled == null || (ItemEnabled(e.Index))))
                return new SolidBrush(CheckEnabled(ProtoListControlHelp.DefaultForeColorOnFocus));
            return new System.Drawing.SolidBrush(GetEventItemColor(e.Index));
        }
        protected override Brush GetFocusBrush(DrawItemEventArgs e)
        {
            if (ItemEnabled == null) return base.GetFocusBrush(e);
            if (ItemEnabled(e.Index))
                return base.GetFocusBrush(e);
            else return base.GetBackColor(e);
        }
        public ProtoEventList()
        {

        }
    }
}
