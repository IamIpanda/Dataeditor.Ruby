﻿using System;
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
            if (e.Index >= 0)
            {
                if (Items.Count == 0 || e.Index >= Items.Count)
                    return;
                if (Items.Count == 0 && Focused)
                {
                    DrawFocusRectangle(e.Graphics, e.Bounds);
                    return;
                }
                // 对背景进行描绘
                Brush BackBrush = GetFocused(e.State) ?  GetFocusBrush(e) : GetBackColor(e);
                e.Graphics.FillRectangle(BackBrush, e.Bounds);
                if (GetFocused(e.State))
                    DrawFocusRectangle(e.Graphics, e.Bounds);
                // 对前台文字进行描绘
                Brush ForeBrush = GetForeColor(e);
                e.Graphics.DrawString(Items[e.Index].ToString(), Font, ForeBrush,
                    new Rectangle(e.Bounds.X + RightShift, e.Bounds.Y, e.Bounds.Width - RightShift, e.Bounds.Height));
            }
            // 补齐余项
            if (e.Index == Items.Count - 1)
            {
                int cy = e.Bounds.Y + e.Bounds.Height, count = e.Index % ProtoListControlHelp.DefaultBackColors.Count;
                while (cy <= this.ClientRectangle.Height)
                {
                    count = (count + 1) % ProtoListControlHelp.DefaultBackColors.Count;
                    SolidBrush ExtraBackBrush = ProtoListControlHelp.GetBrush(CheckEnabled(ProtoListControlHelp.DefaultBackColors[count]));
                    e.Graphics.FillRectangle(ExtraBackBrush, new Rectangle(e.Bounds.X, cy, e.Bounds.Width, e.Bounds.Height));
                    cy += ItemHeight;
                }
            }
        }

        protected virtual bool GetFocused(DrawItemState state)
        {
            if (DisappearRectLosingFocus)
                return (state & DrawItemState.Focus) != 0;
            return (state & DrawItemState.Selected) != 0;
        }
        protected virtual Brush GetBackColor(DrawItemEventArgs e)
        {
            int index = e.Index;
            Color color = default(Color);
            BackColors.TryGetValue(index, out color);
            if (color != default(Color))
                return ProtoListControlHelp.GetBrush(CheckEnabled(color));
            index %= ProtoListControlHelp.DefaultBackColors.Count;
            return ProtoListControlHelp.GetBrush(CheckEnabled(ProtoListControlHelp.DefaultBackColors[index]));
        }
        protected virtual Brush GetForeColor(DrawItemEventArgs e)
        {
            DrawItemState state = e.State;
            if (GetFocused(state))
                return ProtoListControlHelp.GetBrush(CheckEnabled(ProtoListControlHelp.DefaultForeColorOnFocus));
            //Color color = ForeColors[e.Index];
            Color color = default(Color);
            ForeColors.TryGetValue(e.Index, out color);
            if (color != default(Color))
                return ProtoListControlHelp.GetBrush(CheckEnabled(color));
            else return ProtoListControlHelp.GetBrush(CheckEnabled(ForeColor));
        }
        protected virtual Brush GetFocusBrush(DrawItemEventArgs e)
        {
            return ProtoListControlHelp.GetFocusBrush(e.Bounds, BackColor);
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