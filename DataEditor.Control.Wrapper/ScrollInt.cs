using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class ScrollInt : WrapControlEditor<FuzzyData.FuzzyFixnum,Prototype.ProtoScrollIntBar>
    {
        public override string Flag { get { return "scrollint"; } }
        public override void Push()
        {
            value.Value = Control.value;
        }

        public override void Pull()
        {
            Control.value = Convert.ToInt32(value.Value);
        }

        public override bool ValueIsChanged()
        {
            return value.Value == Control.value;
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("minvalue", 0);
            argument.SetArgument("maxvalue", 100);
        }
        public override void Reset()
        {
            Control.MinValue = argument.GetArgument<int>("minvalue");
            Control.MaxValue = argument.GetArgument<int>("maxvalue");
            base.Reset();
        }
    }
}
