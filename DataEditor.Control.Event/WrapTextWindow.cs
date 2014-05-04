using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataEditor.Control.Event
{
    public class WrapTextWindow : Window.WrapAnyWindow<Prototype.ProtoLinedPaperDialog>
    {
        public override string Flag { get { return "dialog_text"; } }
        protected override void OnValueChanged()
        {
            Window.Value = base.value.ToString();
        }
    }
}
