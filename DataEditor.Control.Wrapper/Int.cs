using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class Int : DataEditor.Control.WrapControlEditor<FuzzyData.FuzzyFixnum, System.Windows.Forms.NumericUpDown>
    {
        public override string Flag { get { return "int"; } }
        public override void Push()
        {
            value.Value = Convert.ToInt64(Control.Value);
        }

        public override void Pull()
        {
            if (value.Value < Control.Minimum) value.Value = Convert.ToInt64(Control.Minimum);
            if (value.Value > Control.Maximum) value.Value = Convert.ToInt64(Control.Maximum);
            Control.Value = value.Value;
        }

        public override bool ValueIsChanged()
        {
            return value.Value != Control.Value;
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("minvalue", 0, Help.Parameter.ArgumentType.Option);
            argument.SetArgument("maxvalue", int.MaxValue, Help.Parameter.ArgumentType.Option);
            argument.SetArgument("digit", 0, Help.Parameter.ArgumentType.Option);
            argument.SetArgument("increment", 1D, Help.Parameter.ArgumentType.Option);
        }
        public override void Reset()
        {
            base.Reset();
            if (Binding == null) return;
            Control.Maximum = argument.GetArgument<int>("MAXVALUE");
            Control.Minimum = argument.GetArgument<int>("MINVALUE");
            Control.Increment = (decimal)argument.GetArgument<double>("INCREMENT");
        }
    }
}
