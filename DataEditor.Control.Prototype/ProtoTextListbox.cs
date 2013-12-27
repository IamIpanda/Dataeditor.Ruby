using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DataEditor.Control.Prototype
{
    public class ProtoTextListBox : ProtoListBox
    {
        public List<string> Texts { get; set; }
        public List<Color> TextColor { get; set; }
        public Color DefaultColor { get; set; }
        protected StringFormat format = new StringFormat();
        protected Font font;
        public ProtoTextListBox()
        {
            ItemHeight = 16;
            base.RightShift = ItemHeight + 1;
            DefaultColor = Color.Gray;
            Texts = new List<string>();
            TextColor = new List<Color>();
            font = new Font(Font, FontStyle.Bold);
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Far ;
        }
        protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
        {
            base.OnDrawItem(e);
            int y = e.Bounds.Y;
            var rect = new Rectangle(1, y + 1, ItemHeight - 2, ItemHeight - 2);
            System.Windows.Forms.ControlPaint.DrawCheckBox(e.Graphics, rect, System.Windows.Forms.ButtonState.Flat);
            if (Texts != null && Texts.Count > e.Index && e.Index > 0)
            {
                // 目标字段
                string target = Texts[e.Index];
                // 颜色
                Color fore = DefaultColor;
                if (TextColor != null && TextColor.Count > e.Index) fore = TextColor[e.Index];
                var brush = new SolidBrush(fore);
                e.Graphics.DrawString(target, font, brush, rect, format);
            }
        }
    }
    public class ProtoCircleTextListBox : ProtoTextListBox
    {
        List<string> target_text = new List<string>();
        public List<string> TargetText
        {
            get { return target_text; }
            set { target_text = value; Invalidate(); }
        }
        public List<int> Value { get; set; }
        public List<Color> TargetColor { get; set; }

        public ProtoCircleTextListBox()
        {
            this.MouseClick += ProtoCircleTextListBox_MouseClick;
            TargetColor = new List<Color>();
            Value = new List<int>();
        }

        void ProtoCircleTextListBox_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.X > ItemHeight) return;
            if (TargetText.Count == 0) return;
            int index = this.IndexFromPoint(e.Location);
            if (index >= Items.Count) return;
            while (Value.Count <= index) Value.Add(-1);
            Value[index] = (Value[index] + 1) % TargetText.Count;
            this.SelectedIndex = index;
            Invalidate();
        }
        protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
        {
            if (Value != null && TargetText != null && TargetColor != null)
            {
                while (Value.Count <= e.Index) Value.Add(-1);
                int target_value = target_value = Value[e.Index];
                if (target_value >= 0)
                {
                    while (Texts.Count <= e.Index) Texts.Add("");
                    while (TextColor.Count <= e.Index) TextColor.Add(DefaultColor);
                    Texts[e.Index] = TargetText[target_value];
                    if (TargetColor.Count > target_value) TextColor[e.Index] = TargetColor[target_value];
                }
            }
            base.OnDrawItem(e);
        }
        
    }
}
