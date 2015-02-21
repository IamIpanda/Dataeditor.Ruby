using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using DataEditor.Control.Event.DataModel;
using DataEditor.FuzzyData;

namespace DataEditor.Control.Wrapper
{
    public class Event : Control.WrapControlEditor<FuzzyData.FuzzyArray, Prototype.ProtoEventCommandList>
    {
        public override void Pull()
        {
            Control.Items.Clear();
            Control.ResetDo();
            foreach (var obj in value.OfType<FuzzyObject>())
                Control.Items.Add(new Command(obj));
            Control.SignColors = Help.Taint.Instance.Tag[Value] as Dictionary<object, Color> ?? new Dictionary<object, Color>();
        }

        public override void Push()
        {
            value.Clear();
            foreach (var command in Control.Items.OfType<Command>())
                value.Add(command.Link);
            Control.SaveDo();
            Help.Taint.Instance.Tag[value] = Control.SignColors;
        }

        public override bool ValueIsChanged()
        {
            return true;
        }

    }
}
