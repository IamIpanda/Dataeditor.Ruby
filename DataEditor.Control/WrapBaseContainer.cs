﻿using System;
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
        public override bool ValueIsChanged() { return false; }  // 此代码应当无法触发 
        public virtual System.Windows.Forms.Control.ControlCollection Controls { get { return Binding.Controls; } }
        public virtual bool CanAdd(System.Windows.Forms.Control control) 
        {
            return !(control is System.Windows.Forms.TabPage || control is System.Windows.Forms.Form); 
        }
        public override void Pull()
        {
            foreach (System.Windows.Forms.Control control in Controls)
                if (control.Tag != null && control.Tag is ObjectEditor)
                    (control.Tag as ObjectEditor).Parent = GetBaseValue();
        }
        protected virtual FuzzyData.FuzzyObject GetBaseValue() { return value; }
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
            argument.SetArgument("backcolor", default(System.Drawing.Color), Help.Parameter.ArgumentType.Option);
            argument.OverrideArgument("label", 0, Help.Parameter.ArgumentType.HardlyEver);
            argument.OverrideArgument("actual", null, Help.Parameter.ArgumentType.Option);
        }
        public override void Putt()
        {
            // TODO
            /*
            var state = Help.Taint.Instance[this.value];
            if (state != Contract.TaintState.UnTainted)
                Binding.ForeColor = Help.Taint.DefaultColor(state); */
        }
        public virtual void SetSize(System.Drawing.Size size) 
        {
            if (Binding == null) return;
            if (Binding.Dock == System.Windows.Forms.DockStyle.Fill && Container != null)
            {
                int width_increment = Container.Binding.Size.Width - Binding.Size.Width;
                int height_increment = Container.Binding.Size.Height - Binding.Size.Height;
                Container.SetSize(new System.Drawing.Size(size.Width + width_increment, size.Height + height_increment));
            }
            else
                Binding.ClientSize = size;
        }
        public Control.ObjectEditor SearchChild(string Flag, int index = 0)
        {
            foreach (System.Windows.Forms.Control control in Controls)
                if (control.Tag != null && control.Tag is ObjectEditor)
                    if ((control.Tag as ObjectEditor).Flag == Flag)
                        if (index <= 0) return control.Tag as ObjectEditor;
                        else index -= 1;
            return null;
        }
        public List<Control.ObjectEditor> SearchChilds(string Flag)
        {
            List<Control.ObjectEditor> ans = new List<ObjectEditor>();
            foreach (System.Windows.Forms.Control control in Controls)
                if (control.Tag != null && control.Tag is ObjectEditor)
                    if ((control.Tag as ObjectEditor).Flag == Flag)
                        ans.Add(control.Tag as ObjectEditor);
            return ans;
        }
    }
    public abstract class WrapControlContainer<TControl> : WrapBaseContainer
        where TControl : System.Windows.Forms.Control, new()
    {
        protected TControl Control = new TControl();
        public override void Bind() { Binding = Control; }
    }
}
