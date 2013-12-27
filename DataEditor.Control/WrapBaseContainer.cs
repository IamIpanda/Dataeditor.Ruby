using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control
{
    public abstract class WrapBaseContainer : WrapBaseEditor<FuzzyData.FuzzyObject>, DataContainer
    {
        public virtual int end_x { get { return 0; } }
        public virtual int end_y { get { return 0; } }
        public virtual int start_x { get { return 0; } }
        public virtual int start_y { get { return 0; } }
        public override bool CheckValue() { return false; }  // 此代码应当无法触发 
        public virtual System.Windows.Forms.Control.ControlCollection Controls { get { return Binding.Controls; } }
        public virtual void SetSize(System.Drawing.Size size) { if (Binding != null) Binding.ClientSize = size; }
        public virtual bool CanAdd(System.Windows.Forms.Control control) { return !(control is System.Windows.Forms.TabPage); }
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
            Binding.BackColor = argument.GetArgument<System.Drawing.Color>("BACKCOLOR");
            Binding.Text = argument.GetArgument<string>("TEXT");
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.Defaults.Add("BACKCOLOR", default(System.Drawing.Color));
            argument.Defaults["LABEL"] = 0;
        }
    }
    public abstract class WrapControlContainer<TControl> : WrapBaseContainer
        where TControl : System.Windows.Forms.Control, new()
    {
        protected TControl Control = new TControl();
        public override void Bind() { Binding = Control; }
    }
}
