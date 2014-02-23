using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper.Event
{
    public class Switch : Control.WrapControlEditor<FuzzyData.FuzzyFixnum, Prototype.ProtoDropItem>
    {
        public override string Flag { get { return "switch"; } }
        public override void Push()
        {
        }
        public override void Pull()
        {
           
        }

        public override bool ValueIsChanged()
        {
            return false;
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("textbook", null);
        }
        public override void Reset()
        {
            base.Reset();
        }
        public override void Bind()
        {
            base.Bind();
            Control.ButtonClicked += Control_ButtonClicked;
        }

        void Control_ButtonClicked(object sender, EventArgs e)
        {

        }

    }
}
