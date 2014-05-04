using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class WrapColor : DataEditor.Control.WrapControlEditor<FuzzyData.FuzzyColor, Prototype.ProtoTone>
    {
        public override string Flag { get { return "color"; } }
        public override void Push()
        {
            value.set(Control.Value1, Control.Value2, Control.Value3, Control.Value4);
        }

        public override void Pull()
        {
            Control.Value1 = value.red;
            Control.Value2 = value.green;
            Control.Value3 = value.blue;
            Control.Value4 = value.alpha;
        }

        public override bool ValueIsChanged()
        {
            return false;
        }
        public override void Bind()
        {
            base.Bind();
            Control.Mode = Prototype.ProtoTone.ToneMode.Color;
        }
    }
}
