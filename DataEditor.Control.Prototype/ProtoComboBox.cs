using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DataEditor.Control.Prototype
{
    public class ProtoComboBox : ComboBox, ProtoListControl
    {
        public ProtoComboBox()
        {
            this.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ItemHeight = 12;
        }
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= Items.Count)
                return;
            if (Items.Count == 0 && Focused)
            {
                ProtoListControlHelp.DrawFocusRectangle(e.Graphics, e.Bounds);
                return;
            }
            // 描绘背景颜色
            e.Graphics.FillRectangle(GetBackBrush(e), e.Bounds);
            if (GetFocused(e))
                ProtoListControlHelp.DrawFocusRectangle(e.Graphics, e.Bounds);
            // 描绘文字
            e.Graphics.DrawString(Items[e.Index].ToString(), Font, GetForeBrush(e), e.Bounds);
        }
        protected bool GetFocused(DrawItemEventArgs e)
        {
            return (e.State & DrawItemState.Selected) != 0;
        }
        protected Brush GetBackBrush(DrawItemEventArgs e)
        {
            if(!(this.DroppedDown || Focused))
                return new SolidBrush(this.BackColor);
            if (GetFocused(e))
                return ProtoListControlHelp.GetFocusBrush(e.Bounds, BackColor);
            int index = e.Index % ProtoListControlHelp.DefaultFocusColors.Count;
            Color c = ProtoListControlHelp.DefaultBackColors[index];
            return new SolidBrush(ProtoListControlHelp.CheckEnabled(c, Enabled));
        }
        protected Brush GetForeBrush(DrawItemEventArgs e)
        {
            if (GetFocused(e))
                return new SolidBrush(ProtoListControlHelp.DefaultForeColorOnFocus);
            return new SolidBrush(ProtoListControlHelp.CheckEnabled(ForeColor, Enabled));
        }
    }
}
