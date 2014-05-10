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
        static public int SignSpace = 12;
        static public Brush ForeBrush = new SolidBrush(Color.Black);
        static public Brush ForeBrushOnFocus = new SolidBrush(Color.White);
        protected override System.Drawing.Brush GetForeColor(System.Windows.Forms.DrawItemEventArgs e)
        {
            DrawItemState state = e.State;
            if ((GetFocused(state) && !UsingNull) || UsingFocus)
                return new SolidBrush(CheckEnabled(ProtoListControlHelp.DefaultForeColorOnFocus));
            return new SolidBrush(ItemColor);
        }
        protected override Brush GetFocusBrush(DrawItemEventArgs e)
        {
            if (!UsingNull) return base.GetFocusBrush(e);
            else return base.GetBackColor(e);
        }
        protected override Brush GetBackColor(DrawItemEventArgs e)
        {
            if (UsingFocus) return base.GetFocusBrush(e);
            else return base.GetBackColor(e);
        }
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            UsingFocus = false;
            UsingNull = false;
            Indent = 0;
            AddOnString = "";
            ItemColor = Color.Black;
            if (e.Index >= 0 && ItemGoingDraw != null) ItemGoingDraw(e.Index);
            RightShift = (UsingNull ? (int)
                (e.Graphics.MeasureString(AddOnString, this.Font).Width) : 4)
                + Indent * 10 + SignSpace;
            base.OnDrawItem(e);
            if (!UsingNull)
            {
                Point pos = new Point(e.Bounds.Location.X + Indent * 10, e.Bounds.Location.Y);
                e.Graphics.DrawString(Event.EventCommand.FocusSign, this.Font, 
                    (base.GetFocused(e.State) || UsingFocus) ? ForeBrushOnFocus : ForeBrush, pos);
            }
        }
        public ProtoEventList()
        {

        }
        public delegate void EventWithIndex(int index);
        public EventWithIndex ItemGoingDraw;
        public bool UsingFocus = false;
        public bool UsingNull = false;
        public int Indent = 0;
        public string AddOnString = "";
        public Color ItemColor = Color.Black;
    }
}
