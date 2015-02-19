using System;
using System.Collections.Generic;
using System.Text;
using DataEditor.Help;

namespace DataEditor.Control
{
    public abstract class WrapBaseValueContainer<T> : WrapBaseEditor<T>, DataContainer where T : FuzzyData.FuzzyObject, new()
    {
        public virtual int end_x { get { return 0; } }
        public virtual int end_y { get { return 0; } }
        public virtual int start_x { get { return 0; } }
        public virtual int start_y { get { return 0; } }
        public abstract void SetSize(System.Drawing.Size size);
        public abstract System.Windows.Forms.Control.ControlCollection Controls { get; }
        public virtual bool CanAdd(System.Windows.Forms.Control control) { return !(control is System.Windows.Forms.TabPage); }
        public override void Pull()
        {
            if (parent == null) return;
            foreach (System.Windows.Forms.Control control in Controls )
                if (control.Tag is ObjectEditor)
                    (control.Tag as ObjectEditor).Parent = parent;
        }
        public override void Push()
        {
            // 理论上而言，这段代码是必须用手动调用触发，而不是通过机制触发的。
            Log.log(this.Flag + " called a forced push");
            foreach (System.Windows.Forms.Control control in Controls)
                if (control.Tag is ObjectEditor && (control.Tag as ObjectEditor).Value != null)
                    (control.Tag as ObjectEditor).Push();
        }
        public override void Reset()
        {
            base.Reset();
            Binding.BackColor = argument.GetArgument<System.Drawing.Color>("BACKCOLOR");
            Binding.Text = argument.GetArgument<string>("TEXT");
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("backcolor", default(System.Drawing.Color), Help.Parameter.ArgumentType.Option);
            argument.OverrideArgument("label", 0);
        }
    }
    public abstract class WrapControlValueContainer<TValue, TControl> : WrapBaseValueContainer<TValue>
        where TValue : FuzzyData.FuzzyObject, new()
        where TControl : System.Windows.Forms.Control, new()
    {
        protected TControl Control = new TControl();
        public override void Bind() { Binding = Control; }
    }
}
