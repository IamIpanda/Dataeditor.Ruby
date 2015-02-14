using System;
using System.Collections.Generic;
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
            foreach (var obj in value.OfType<FuzzyObject>())
                Control.Items.Add(new Command(obj));
        }

        public override void Push()
        {
            value.Clear();
            foreach (var command in Control.Items.OfType<Command>())
                value.Add(command.Link);
        }

        public override bool ValueIsChanged()
        {
            return false;
        }

    }
}
