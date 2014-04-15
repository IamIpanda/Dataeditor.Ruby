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
        public GetEventItemColorDelegate GetEventItemColor { get; set; }
        protected override System.Drawing.Brush GetForeColor(System.Windows.Forms.DrawItemEventArgs e)
        {
            if (GetEventItemColor == null) return base.GetForeColor(e);
            DrawItemState state = e.State;
            if (GetFocused(state))
                return new SolidBrush(CheckEnabled(ProtoListControlHelp.DefaultForeColorOnFocus));
            return new System.Drawing.SolidBrush(GetEventItemColor(e.Index));
        }
    }
}
