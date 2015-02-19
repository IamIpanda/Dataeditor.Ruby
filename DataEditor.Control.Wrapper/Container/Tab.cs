using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper.Container
{
    public class Tab : WrapControlContainer<System.Windows.Forms.TabPage>
    {
        public System.Drawing.Size RememberSize { get; set; }
        public override string Flag { get { return "tab"; } }
        public override int start_x { get { return 2; } }
        public override int start_y { get { return 2; } }
        public override int end_x { get { return 2; } }
        public override int end_y { get { return 2; } }
        public override void SetSize(System.Drawing.Size size)
        {
            size = new System.Drawing.Size(size.Width + 4, size.Height + 40);
            RememberSize = size;
            base.SetSize(size);
        }
        protected override void SetEnabled() { }
        public override void Putt()
        {
            var state = Help.Taint.Instance[Value];
            var color = Help.Taint.DefaultColor(state);
            // Control.ForeColor = color;
        }
        public override void Reset()
        {
            base.Reset();
            Help.Log.log("正在初始化 " + Control.Text + " 标签页...");
        }
    }
}
