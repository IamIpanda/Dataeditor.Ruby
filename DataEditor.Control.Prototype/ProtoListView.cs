using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public class ProtoListView : ListView ,ProtoListControl
    {
        private StringFormat Format = new StringFormat(StringFormatFlags.NoClip);
        public ProtoListView()
        {
            this.View = System.Windows.Forms.View.Details;
            this.FullRowSelect = true;
            this.MultiSelect = false;
            this.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.OwnerDraw = true;
            Format.LineAlignment = StringAlignment.Center;
        }
        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            if (e.ItemIndex < 0 || e.ItemIndex >= Items.Count)
                return;
            if (Items.Count == 0 && Focused)
            {
                ProtoListControlHelp.DrawFocusRectangle(e.Graphics, e.Bounds);
                return;
            }
            Rectangle Bounds = new Rectangle(e.Bounds.X, e.Bounds.Y, this.ClientSize.Width, e.Bounds.Height);
            // TODO：修正焦点条被截断的问题。
            // 描绘背景图形
            e.Graphics.FillRectangle(GetBackBrush(e), Bounds);
            if (GetFocused(e))
            {
                e.Graphics.FillRectangle(ProtoListControlHelp.GetFocusBrush(e.Bounds, BackColor), e.Bounds);
                ProtoListControlHelp.DrawFocusRectangle(e.Graphics, e.Bounds);
            }
            // 子类描绘调用
            foreach(ListViewItem.ListViewSubItem item in e.Item.SubItems)
                OnDrawSubItem(new DrawListViewSubItemEventArgs(
                    e.Graphics, item.Bounds, e.Item, item, e.ItemIndex, 0, null, e.State));
            // 补齐剩余部分
            if (e.ItemIndex == Items.Count - 1)
            {
                int cy = e.Bounds.Y + e.Bounds.Height, count = e.ItemIndex % ProtoListControlHelp.DefaultBackColors.Count;
                while (cy <= this.ClientRectangle.Height)
                {
                    count = (count + 1) % ProtoListControlHelp.DefaultBackColors.Count;
                    SolidBrush ExtraBackBrush = new SolidBrush(ProtoListControlHelp.
                        CheckEnabled(ProtoListControlHelp.DefaultBackColors[count], Enabled));
                    e.Graphics.FillRectangle(ExtraBackBrush, new Rectangle(e.Bounds.X, cy, this.ClientSize.Width, e.Bounds.Height));
                    cy += e.Bounds.Height;
                }
            }
        }
        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
            base.OnDrawColumnHeader(e);
        }
        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            // 此处描绘文字
            Brush ForeBrush = GetForeBrush(e);
            e.Graphics.DrawString(e.SubItem.Text, Font, ForeBrush, e.Bounds, Format);
        }
        protected bool GetFocused(DrawListViewItemEventArgs e)
        {
            return (e.State & ListViewItemStates.Selected) != 0;
        }
        protected bool GetFocused(DrawListViewSubItemEventArgs e)
        {
            return (e.ItemState & ListViewItemStates.Selected) != 0;
        }
        protected Brush GetForeBrush(DrawListViewItemEventArgs e)
        {
            Color c;
            if (GetFocused(e))
                c = ProtoListControlHelp.DefaultForeColorOnFocus;
            else
                c = ForeColor;
            return new SolidBrush(ProtoListControlHelp.CheckEnabled(c, Enabled));
        }
        protected Brush GetForeBrush(DrawListViewSubItemEventArgs e)
        {
            Color c;
            if (GetFocused(e))
                c = ProtoListControlHelp.DefaultForeColorOnFocus;
            else
                c = ForeColor;
            return new SolidBrush(ProtoListControlHelp.CheckEnabled(c, Enabled));
        }
        protected Brush GetBackBrush(DrawListViewItemEventArgs e)
        {
            int index = e.ItemIndex % ProtoListControlHelp.DefaultFocusColors.Count;
            Color c = ProtoListControlHelp.DefaultBackColors[index];
            return new SolidBrush(ProtoListControlHelp.CheckEnabled(c, Enabled));
        }
        protected Brush GetBackBrush(DrawListViewSubItemEventArgs e)
        {
            int index = e.ItemIndex % ProtoListControlHelp.DefaultFocusColors.Count;
            Color c = ProtoListControlHelp.DefaultBackColors[index];
            return new SolidBrush(ProtoListControlHelp.CheckEnabled(c, Enabled));
        }
    }
}
