using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DataEditor.Control.Wrapper
{
    public class ListText : ListWrapper<FuzzyData.FuzzyTable>, Contract.TaintableList
    {
        public override void Push()
        {
            for (int i = 0; i < value.xsize && i < Control.Items.Count; i++)
                value[i] = (short)Link[Control.Value[base.catalogue.Link.Verse[i]]];
            NowTaint = false;
        }
        public override void Pull()
        {
            // FIXME : SAFE
            Control.Value.Clear();
            for (int i = 0; i < value.xsize && i < Control.Items.Count; i++)
                Control.Value.Add(Link.IndexOf(value[base.catalogue.Link.Reverse[i]]));

            Control.Invalidate();
        }
        public override void Bind()
        {
            base.Bind();
            Control.ItemCircled += Control_ItemCircled;
        }
        void Control_ItemCircled(object sender, Prototype.ProtoCircleListValueChangeEventArgs e)
        {
            NowTaint = true;
            TaintSingle(e.index);
        }
        
    }
}
