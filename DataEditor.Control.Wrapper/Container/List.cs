using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper.Container
{
    // TODO: Finish it.
    public class List : WrapControlContainer<Prototype.ProtoFullListBox>
    {
        public override string Flag { get { return "List"; } }
        public override FuzzyData.FuzzyObject Value
        {
            get { return GetValue(Control.SelectedIndex); }
            set { base.Value = value; }
        }

        public override void Reset()
        {

        }
        protected Help.Parameter.Text text;
        void RefreshText(int index = -1)
        {
            if (index < 0) { for (int i = 0; i < Control.Items.Count; i++) RefreshText(i); return; }
            if (text != null) Control.Items[index] = text.ToString(index, text.Watch, index);
        }
        FuzzyData.FuzzyObject GetValue(int index)
        {
            throw new NotImplementedException();
        }
        public override void Bind()
        {
            base.Bind();
            Control.SelectedIndexChanged += Control_SelectedIndexChanged;
        }

        void Control_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
