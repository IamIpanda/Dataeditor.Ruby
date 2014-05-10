using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class SelfSwitch : Control.WrapControlEditor<FuzzyData.FuzzyString, Prototype.ProtoComboBox>
    {
        public override string Flag { get { return "self_switch"; } }
        public override void Bind()
        {
            base.Bind();
            Control.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("defaults", new List<object>() { "A", "B", "C", "D" }, Help.Parameter.ArgumentType.Option);
        }
        public override void Reset()
        {
            base.Reset();
            var defaults = argument.GetArgument<List<Object>>("defaults");
            foreach (object obj in defaults)
                Control.Items.Add(obj.ToString());
        }
        public override void Pull()
        {
            Control.Text = value.Text;
        }
        public override void Push()
        {
            value.Text = Control.Text;
        }
        public override bool ValueIsChanged()
        {
            return value.Text != Control.Text;
        }
    }
}
