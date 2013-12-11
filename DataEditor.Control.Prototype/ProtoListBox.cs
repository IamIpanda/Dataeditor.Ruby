using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DataEditor.Control.Prototype
{
    public class ProtoListBox : ListBox, ProtoListControl
    {
        protected int RightShift { get; set; }
        public bool DisappearRectLosingFocus { get; set; }

        protected ColorHashCollection BackColorCollection = new ColorHashCollection();
        protected ColorHashCollection ForeColorCollection = new ColorHashCollection();
        public ColorHashCollection BackColors { get { return BackColorCollection; } }
        public ColorHashCollection ForeColors { get { return ForeColorCollection; } }

        public ProtoListBox()
        {
            this.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.RightShift = 0;
            this.DisappearRectLosingFocus = true;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            if (Items.Count == 0)
                return;
            if (Items.Count == 0 && Focused)
            {
                DrawFocusRectangle(e.Graphics, e.Bounds);
                return;
            }
            // 对背景进行描绘
            Brush BackBrush = GetFocused(e.State) ? GetFocusBrush(e.Bounds) : GetBackColor(e);
            e.Graphics.FillRectangle(BackBrush, e.Bounds);
            if (GetFocused(e.State))
                DrawFocusRectangle(e.Graphics, e.Bounds);
            // 对前台文字进行描绘
            Brush ForeBrush = GetForeColor(e);
            e.Graphics.DrawString(Items[e.Index].ToString(), Font, ForeBrush,
                new Rectangle(e.Bounds.X + RightShift, e.Bounds.Y, e.Bounds.Width - RightShift, e.Bounds.Height));
            // 补齐余项
            if (e.Index == Items.Count - 1)
            {
                int cy = e.Bounds.Y + e.Bounds.Height, count = e.Index % ProtoListControlHelp.DefaultBackColors.Count;
                while (cy <= this.ClientRectangle.Height)
                {
                    count = (count + 1) % ProtoListControlHelp.DefaultBackColors.Count;
                    SolidBrush ExtraBackBrush = new SolidBrush(CheckEnabled(ProtoListControlHelp.DefaultBackColors[count]));
                    e.Graphics.FillRectangle(ExtraBackBrush, new Rectangle(e.Bounds.X, cy, e.Bounds.Width, e.Bounds.Height));
                    cy += ItemHeight;
                }
            }
        }

        protected bool GetFocused(DrawItemState state)
        {
            if (DisappearRectLosingFocus)
                return (state & DrawItemState.Focus) != 0;
            return (state & DrawItemState.Selected) != 0;
        }
        protected Brush GetBackColor(DrawItemEventArgs e)
        {
            int index = e.Index;
            Color color = default(Color);
            BackColors.TryGetValue(index, out color);
            if (color != default(Color))
                return new SolidBrush(CheckEnabled(color));
            index %= ProtoListControlHelp.DefaultBackColors.Count;
            return new SolidBrush(CheckEnabled(ProtoListControlHelp.DefaultBackColors[index]));
        }
        protected Brush GetForeColor(DrawItemEventArgs e)
        {
            DrawItemState state = e.State;
            if (GetFocused(state))
                return new SolidBrush(CheckEnabled(ProtoListControlHelp.DefaultForeColorOnFocus));
            //Color color = ForeColors[e.Index];
            Color color = default(Color);
            ForeColors.TryGetValue(e.Index, out color);
            if (color != default(Color))
                return new SolidBrush(CheckEnabled(color));
            else return new SolidBrush(CheckEnabled(ForeColor));
        }
        protected Brush GetFocusBrush(Rectangle? rect = null)
        {
            return ProtoListControlHelp.GetFocusBrush(rect, BackColor);
        }
        protected void DrawFocusRectangle(Graphics graphics, Rectangle rect)
        {
            ProtoListControlHelp.DrawFocusRectangle(graphics, rect);
        }
        protected Color CheckEnabled(Color c)
        {
            return ProtoListControlHelp.CheckEnabled(c, Enabled);
        }

    }
    public class ColorHashCollection : Dictionary<int, Color> { }
}