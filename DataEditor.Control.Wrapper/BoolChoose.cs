using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    /// <summary>
    /// RM 设计人员脑洞大开的最终体现
    /// </summary>
    public class BoolChoose : WrapControlEditor<FuzzyData.FuzzyBool, Prototype.ProtoComboBox>
    {
        public override string Flag { get { return "bool_choose"; } }
        public override void Push()
        {
            value.Value = (Control.SelectedIndex == 0);
        }

        public override void Pull()
        {
            Control.SelectedIndex = value.Value ? 0 : 1;
        }

        public override bool ValueIsChanged()
        {
            return Control.SelectedIndex == 1 ? value.Value : !(value.Value);
        }
        public override void Reset()
        {
            Control.Items.Clear();
            string yes = argument.GetArgument<string>("yes"),
                no = argument.GetArgument<string>("no");
            Control.Items.Add(yes);
            Control.Items.Add(no);
            base.Reset();
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("yes", "是", Help.Parameter.ArgumentType.Option);
            argument.SetArgument("no", "否", Help.Parameter.ArgumentType.Option);
        }
        public override void Bind()
        {
            base.Bind();
            Control.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        }
    }
    public class IntCheck : WrapControlEditor<FuzzyData.FuzzyFixnum, System.Windows.Forms.CheckBox>
    {
        public override string Flag { get { return "int_check"; } }
        public int yes = 1, no = 0;
        public override void Push()
        {
            value.Value = Control.Checked ? yes : no;
        }

        public override void Pull()
        {
            Control.Checked = (value.Value == yes);
        }

        public override bool ValueIsChanged()
        {
            return Control.Checked ? (value.Value == yes) : (value.Value == no);
        }
        public override void Reset()
        {
            Control.Text = argument.GetArgument<string>("text");
            base.Reset();
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("yes", 1, Help.Parameter.ArgumentType.Option);
            argument.SetArgument("no", 0, Help.Parameter.ArgumentType.Option);
            argument.OverrideArgument("label", 0);
        }
    }
}
