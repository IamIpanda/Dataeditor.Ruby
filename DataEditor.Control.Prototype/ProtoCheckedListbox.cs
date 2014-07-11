using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Prototype
{
    public class ProtoCheckedListbox : ProtoListBox
    {
        public ProtoCheckedListbox()
        {
            this.ItemHeight = 14;
            this.RightShift = ItemHeight;
            this.CheckedIndecies = new List<int>();
            base.MouseClick += ProtoCheckedListbox_MouseClick;
        }

        void ProtoCheckedListbox_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int index = e.Y / ItemHeight;
            if (index < 0 || index >= this.Items.Count) return;
            if (e.Button != System.Windows.Forms.MouseButtons.Left) return;
            if (e.X > ItemHeight) return;
            bool state = CheckedIndecies.Contains(index);
            if (ItemCheck != null)
                ItemCheck(this, new System.Windows.Forms.ItemCheckEventArgs(index,
                state ? System.Windows.Forms.CheckState.Unchecked : System.Windows.Forms.CheckState.Checked,
                state ? System.Windows.Forms.CheckState.Checked : System.Windows.Forms.CheckState.Unchecked));
            if (state) CheckedIndecies.Remove(index); else CheckedIndecies.Add(index);
            this.Invalidate(this.GetItemRectangle(index));
        }
        protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
        {
            base.OnDrawItem(e);
            int y = ItemHeight * e.Index - this.AutoScrollOffset.X;
            System.Windows.Forms.ControlPaint.DrawCheckBox(e.Graphics, 0, y, ItemHeight, base.ItemHeight, GetState(e.Index));
        }
        public System.Windows.Forms.ButtonState GetState(int index)
        {
            System.Windows.Forms.ButtonState state = System.Windows.Forms.ButtonState.Flat;
            if (!(this.Enabled))
                state |= System.Windows.Forms.ButtonState.Inactive;
            if (CheckedIndecies.Contains(index)) state |= System.Windows.Forms.ButtonState.Checked;
            return state;
        }
        public List<int> CheckedIndecies { get; set; } 
        public void SetItemChecked(int index, bool value)
        {
            if (value)
                if (!(CheckedIndecies.Contains(index)))
                    CheckedIndecies.Add(index);
            if (!value)
                if (CheckedIndecies.Contains(index))
                    CheckedIndecies.Remove(index);
        }
        public bool GetItemChecked(int index)
        {
            return CheckedIndecies.Contains(index);
        }
        public event System.Windows.Forms.ItemCheckEventHandler ItemCheck;
    }
}
