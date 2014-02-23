using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class Check : WrapControlEditor<FuzzyData.FuzzyBool, System.Windows.Forms.CheckBox>
    {
        public override string Flag { get { return "check"; } }
        public override void Push()
        {
            value.Value = Control.Checked;
        }

        public override void Pull()
        {
            Control.Checked = value.Value;
        }

        public override bool ValueIsChanged()
        {
            return Control.Checked == value.Value;
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            Argument.OverrideArgument("label", 0);
        }
        public override void Reset()
        {
            base.Reset();
            Control.Text = argument.GetArgument<string>("text");
            Control.Size = Control.PreferredSize;
        }
    }
}
