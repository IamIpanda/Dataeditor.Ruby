using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataEditor.Control.Wrapper.Event
{
    public class Variable : Switch
    {
        public override string Flag { get { return "variable"; } }
        public Variable()
            : base()
        {
            Title = "变量";
        }
    }
}