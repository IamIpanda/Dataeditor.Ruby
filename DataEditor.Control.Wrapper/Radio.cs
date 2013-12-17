using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class Radio : Control.WrapControlValueContainer<FuzzyData.FuzzyFixnum, Prototype.ProtoRadioContainer>
    {
        static Dictionary<string, List<Radio>> Radios = new Dictionary<string, List<Radio>>();
        static void OnRadiosChanged(object sender, EventArgs e)
        {

        }
        protected int radio_key = -1;
        public override void SetSize(System.Drawing.Size size)
        {
            Control.Size = new System.Drawing.Size(size.Width + Control.RadioWidth + Control.Margin.Horizontal, size.Height);
        }

        public override System.Windows.Forms.Control.ControlCollection Controls { get { return Control.PanelCollection; } }

        public override void Push() 
        {
            if (Control.Radio.Checked) value.Value = radio_key;
        }
        public override void Pull()
        {
            base.Pull();
            if (value.Value == radio_key) Control.Radio.Checked = true;
            else Control.Radio.Checked = false;
        }

        public override bool CheckValue()
        {
            return false;
        }
        public override void Bind()
        {
            base.Bind();
            Control.Radio.CheckedChanged += OnRadiosChanged;
        }
        public override void Reset()
        {
            base.Reset();
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.Defaults.Add("KEY", 0);
            argument.Defaults.Add("GROUP", "UNGROUPED");
        }
    }
}
