using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataEditor.Control.Event
{
    public class WrapTextWindow : Window.WrapAnyWindow<Prototype.ProtoLinedPaperDialog>
    {
        public override string Flag { get { return "dialog_text"; } }
        public override void Pull()
        {
            var ans = value.ToString();
            Window.Value = ans;
        }
        public override void Push()
        {
            if (value is FuzzyData.FuzzyString)
                (value as FuzzyData.FuzzyString).Text = Window.Value;
        }
    }
}
