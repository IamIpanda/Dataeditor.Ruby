using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class EventTone : DataEditor.Control.WrapControlEditor<FuzzyData.FuzzyTone, Prototype.ProtoEventTone>
    {
        public override string Flag { get { return "tone"; } }
        public override void Push()
        {
            value.red = Control.Value1;
            value.green = Control.Value2;
            value.blue = Control.Value3;
            value.gray = Control.Value4;
        }

        public override void Pull()
        {
            if (value == null) return;
            Control.Value1 = value.red;
            Control.Value2 = value.green;
            Control.Value3 = value.blue;
            Control.Value4 = value.gray;
        }

        public override bool ValueIsChanged()
        {
            return false;
        }
        public override void Bind()
        {
            base.Bind();
            Control.Mode = Prototype.ProtoEventTone.ToneMode.Tone;
        }
    }
}
