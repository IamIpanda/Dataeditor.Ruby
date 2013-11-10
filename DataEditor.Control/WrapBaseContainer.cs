using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control
{
    public abstract class WrapBaseContainer : WrapBaseEditor<FuzzyData.FuzzyObject>, DataContainer
    {
        public void SetSize(System.Drawing.Size size) { if (Binding != null) Binding.ClientSize = size; }
        public System.Windows.Forms.Control.ControlCollection Controls { get { return Binding.Controls; } }
        public override void Pull()
        {
            foreach (System.Windows.Forms.Control control in Controls)
                if (control.Tag != null && control.Tag is ObjectEditor)
                    (control.Tag as ObjectEditor).Parent = Value;
        }
        public override void Push() { /* 弃用 */ }
        public override void Reset()
        {
            base.Reset();
            Binding.BackColor = argument.GetAegument<System.Drawing.Color>("BACKCOLOR");
            Binding.Text = argument.GetAegument<string>("TEXT");
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.Arguments.Add("BACKCOLOR", default(System.Drawing.Color));
            argument.Arguments.Add("TEXT", "");
        }
    }
}
