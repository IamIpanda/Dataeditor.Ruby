using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class WrapFuckingTimer : WrapControlEditor<FuzzyData.FuzzyFixnum, Prototype.ProtoFuckingTimer>
    {
        public override string Flag { get { return "timer"; } }
        public override void Push()
        {
            long target = Control.Value1 * Control.MaxValue2 + Control.Value2;
            value.Value = target;
        }

        public override void Pull()
        {
            int target = Convert.ToInt32(value.Value);
            Control.Value1 = target / Control.MaxValue2;
            Control.Value2 = target % Control.MaxValue2;
        }

        public override bool ValueIsChanged()
        {
            return false;
        }
        public override void Reset()
        {
            base.Reset();
            int step = this.argument.GetArgument<int>("Step");
            int max = this.argument.GetArgument<int>("MaxValue");
            Control.MaxValue2 = step;
            Control.MaxValue1 = max / step;
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("MaxValue", 3599, Help.Parameter.ArgumentType.Option);
            argument.SetArgument("Step", 60, Help.Parameter.ArgumentType.HardlyEver);
        }
    }
}
