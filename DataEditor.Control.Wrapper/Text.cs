using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class Text : DataEditor.Control.WrapControlEditor<FuzzyData.FuzzyString, Prototype.ProtoAutoSizeTextBox>
    {
        public override string Flag { get { return "text"; } }
        public override void Bind()
        {
            base.Bind();
            Control.DoubleClick += Control_DoubleClick;
        }

        void Control_DoubleClick(object sender, EventArgs e)
        {

        }
        public override void Push()
        {
            if (value == null) return;
            value.Text = Control.Text;
        }
        public override void Pull()
        {
            if (value == null) return;
            Control.Text = value.Text;
        }
        public override bool ValueIsChanged()
        {
            if (value == null) return false;
            return Control.Text != value.Text;
        }
        public override bool HighLight { get { return true; } }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
        }
    }
}
