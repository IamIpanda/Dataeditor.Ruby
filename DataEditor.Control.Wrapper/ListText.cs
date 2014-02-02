using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DataEditor.Control.Wrapper
{
    public class ListText : ListWrapper<FuzzyData.FuzzyTable>
    {
        public override void Push()
        {
            for (int i = 0; i < value.xsize && i < Control.Items.Count; i++)
                value[i] = (short)Link[Control.Value[i]];
        }
        public override void Pull()
        {
            // FIXME : SAFE
            Control.Value.Clear();
            for (int i = 0; i < value.xsize && i < Control.Items.Count; i++)
                Control.Value.Add(Link.IndexOf(value[i]));
        }
    }
}
