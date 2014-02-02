using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class ListBuff : ListWrapper<FuzzyData.FuzzyStruct>
    {
        int default_value = 0;
        public override string Flag { get { return "bufflist"; } }
        public override void Pull()
        {
        }
        public override void Push()
        {

        }
        public override void Reset()
        {
            base.Reset();
            default_value = argument.GetArgument<int>("DEFAULT");
        }
    }
}
