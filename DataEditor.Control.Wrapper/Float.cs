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
            value.Value = Convert.ToInt64(Control.Value);
        }

        public override void Pull()
        {
            if (value.Value < (double)Control.Minimum) value.Value = Convert.ToInt64(Control.Minimum);
            if (value.Value > (double)Control.Maximum) value.Value = Convert.ToInt64(Control.Maximum);
            Control.Value = (decimal)value.Value;
        }

        public override bool CheckValue()
        {
            return value.Value == (double)Control.Value;
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.Defaults.Add("MINVALUE", 0);
            argument.Defaults.Add("MAXVALUE", int.MaxValue);
            argument.Defaults.Add("DIGIT", 0);
            argument.Defaults.Add("INCREMENT", 1D);
        }
        public override void Reset()
        {
            base.Reset();
            if (Binding == null) return;
            Control.Maximum = argument.GetAegument<int>("MAXVALUE");
            Control.Minimum = argument.GetAegument<int>("MINVALUE");
            Control.DecimalPlaces = argument.GetAegument<int>("DIGIT");
            Control.Increment = (decimal)argument.GetAegument<double>("INCREMENT");
        }
    }
}
