using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper.Container
{
    public class Metro : Control.WrapControlContainer<Control.Prototype.ProtoMetroContainer>
    {
        public override string Flag { get { return "metro"; } }
        public override System.Windows.Forms.Control.ControlCollection Controls
        {
            get
            {
                return Control.PanelCollection;
            }
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.OverrideArgument("text", "", Help.Parameter.ArgumentType.Option);
        }
        public override void SetSize(System.Drawing.Size size)
        {
            //base.SetSize(size);
            base.SetSize(new System.Drawing.Size(size.Width, size.Height + (Control.Text == "" ? 0 : 12)));
        }
    }
}
