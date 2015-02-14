using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class EventVariable : EventSwitch
    {
        public override string Flag { get { return "variable"; } }
        public EventVariable()
            : base()
        {
            Title = "变量";
        }
    }
}