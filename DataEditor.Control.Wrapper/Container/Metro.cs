using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper.Container
{
    public class Metro : Control.WrapControlContainer<Control.Prototype.ProtoMetroContainer>
    {
        bool IsFix = false;
        public override string Flag { get { return "metro"; } }
        public override System.Windows.Forms.Control.ControlCollection Controls
        { get { return Control.PanelCollection; } }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.OverrideArgument("text", "", Help.Parameter.ArgumentType.Option);
        }
        public override void SetSize(System.Drawing.Size size)
        {
            base.SetSize(new System.Drawing.Size(size.Width, size.Height + (Control.Text == "" ? 0 : 12)));
        }
        public override int end_x { get { return IsFix ? 0 : 3; } }
        public override int end_y { get { return IsFix ? 0 : 3; } }
        public override int start_x { get { return IsFix ? 0 : 3; } }
        public override int start_y { get { return IsFix ? 0 : 3; } }
        public override void Bind()
        {
            base.Bind();
            Control.TextChanged += Control_TextChanged;
        }

        void Control_TextChanged(object sender, EventArgs e)
        {
            IsFix = Control.Text == "";
        }
    }
}
