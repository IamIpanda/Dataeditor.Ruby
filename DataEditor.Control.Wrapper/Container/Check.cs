using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper.Container
{
    public class Check : Control.WrapControlContainer<Prototype.ProtoCheckContainer>
    {
        public override string Flag { get { return "check_container"; } }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.OverrideArgument("label", 0);
            argument.SetArgument("ison", null);
            argument.SetArgument("deny", null);
        }
        public override void SetSize(System.Drawing.Size size)
        {
            if (size.Height < 18) size.Height = 18;
            Control.Size = new System.Drawing.Size(
                size.Width + Control.RadioWidth + Control.Margin.Horizontal, size.Height);
        }
        public override void Reset()
        {
            base.Reset();
            Control.Text = argument.GetArgument<string>("text");;
        }
        public override void Bind()
        {
            base.Bind();
            Control.Radio.CheckedChanged += Radio_CheckedChanged;
        }
        public override void Pull()
        {
            var check = argument.GetArgument<Contract.Runable>("ison");
            if (check != null)
            {
                var ans = check.call(value);
                Control.Radio.Checked = (bool)ans;
            }
            base.Pull();
        }
        void Radio_CheckedChanged(object sender, EventArgs e)
        {
            var state = Control.Radio.Checked;
            if (state) { }
            else
            {
                var proc = argument.GetArgument<Contract.Runable>("deny");
                if (proc != null) proc.call(value);
            }
        }

        public override System.Windows.Forms.Control.ControlCollection Controls
        { get { return Control.PanelCollection; } }
    }
}
