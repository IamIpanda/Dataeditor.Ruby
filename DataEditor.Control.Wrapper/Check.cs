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
             return !(Control.Checked == value.Value);
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.OverrideArgument("label", 0);
            argument.OverrideArgument("height", 20);
            argument.SetArgument("max_width", -1, Help.Parameter.ArgumentType.Option);
            argument.SetArgument("lock_size", true, Help.Parameter.ArgumentType.Option);
        }
        public override void Reset()
        {
            base.Reset();
            Control.Text = argument.GetArgument<string>("text");
            int max_width = argument.GetArgument<int>("max_width");
            int prefer_width = Control.PreferredSize.Width;
            bool lock_size = argument.GetArgument<bool>("lock_size");
            if (max_width < 0) lock_size = false;
            if (lock_size) prefer_width = max_width;
            Control.Width = prefer_width;
        }
        public override void Putt()
        {
            var state = DataEditor.Help.Taint.Instance[Value];
            var color = DataEditor.Help.Taint.DefaultColor(state);
            Control.ForeColor = System.Windows.Forms.CheckBox.DefaultForeColor;
            if (state != Contract.TaintState.UnTainted)
                Control.ForeColor = color;
        }
    }
}
