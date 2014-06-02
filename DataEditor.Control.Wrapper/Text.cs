using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class Text : DataEditor.Control.WrapBaseEditor<FuzzyData.FuzzyString>
    {
        Prototype.ProtoAutoSizeTextBox control = new Prototype.ProtoAutoSizeTextBox();
        public override string Flag { get { return "text"; } }
        public override void Bind() { Binding = control; }
        public override void Push()
        {
            if (value == null) return;
            value.Text = control.Text;
        }
        public override void Pull()
        {
            if (value == null) return;
            control.Text = value.Text;
        }
        public override bool ValueIsChanged()
        {
            if (value == null) return false;
            return control.Text != value.Text;
        }
        protected System.Drawing.Color Fore, Back;
        public override void OnEnter(object sender, EventArgs e)
        {
            Fore = control.ForeColor;
            Back = control.BackColor;
            control.ForeColor = Help.Painter.Instance[2];
            control.BackColor = Help.Painter.Instance[3];
            base.OnEnter(sender, e);
        }
        public override void OnLeave(object sender, EventArgs e)
        {
            control.ForeColor = Fore;
            control.BackColor = Back;
            base.OnLeave(sender, e);
        }
    }
}
