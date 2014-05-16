using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class Float : DataEditor.Control.WrapControlEditor<FuzzyData.FuzzyFloat, System.Windows.Forms.NumericUpDown>
    {
        public override string Flag { get { return "float"; } }
        public override void Push()
        {
            value.Value = Convert.ToInt64(Convert.ToInt64(Control.Value) / times);
        }

        public override void Pull()
        {
            if (value.Value * times < (double)Control.Minimum) value.Value = Convert.ToInt64(Control.Minimum);
            if (value.Value * times > (double)Control.Maximum) value.Value = Convert.ToInt64(Control.Maximum);
            Control.Value = (decimal)(value.Value * times);
        }

        public override bool ValueIsChanged()
        {
            return value.Value * times == (double)Control.Value;
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("minvalue", 0, Help.Parameter.ArgumentType.Option);
            argument.SetArgument("maxvalue", int.MaxValue, Help.Parameter.ArgumentType.Option);
            argument.SetArgument("digit", 0, Help.Parameter.ArgumentType.Option);
            argument.SetArgument("increment", 1D, Help.Parameter.ArgumentType.Option);
            argument.SetArgument("times", 1D, Help.Parameter.ArgumentType.Option);
        }
        double times = 1D;
        public override void Reset()
        {
            base.Reset();
            if (Binding == null) return;
            Control.Maximum = argument.GetArgument<int>("MAXVALUE");
            Control.Minimum = argument.GetArgument<int>("MINVALUE");
            Control.DecimalPlaces = argument.GetArgument<int>("DIGIT");
            Control.Increment = (decimal)argument.GetArgument<double>("INCREMENT");
            times = argument.GetArgument<double>("TIMES");
        }
    }
}
