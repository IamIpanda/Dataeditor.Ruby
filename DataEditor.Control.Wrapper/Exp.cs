using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class Exp : WrapControlEditor<FuzzyData.FuzzyComplex, Prototype.ProtoDropItem>
    {
        int min = 0, max = 60, lv = 99;
        new Contract.Runable value;
        public override string Flag { get { return "exp"; } }
        bool isChanged = false;
        public override void Push()
        {
            isChanged = false;
        }

        public override void Pull()
        {
            var sb = new StringBuilder();
            sb.Append("[");
            foreach (var obj in base.value.AllValues)
                sb.Append(obj.ToString() + ",");
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            Control.Text = sb.ToString();
        }

        public override bool ValueIsChanged()
        {
            return isChanged;
        }

        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("Min", 0, Help.Parameter.ArgumentType.Option);
            argument.SetArgument("Max", 50, Help.Parameter.ArgumentType.Option);
            argument.SetArgument("Level", 99, Help.Parameter.ArgumentType.Option);
            argument.SetArgument("Value", new Help.Return(1));
        }
        public override void Reset()
        {
            base.Reset();
            min = argument.GetArgument<int>("Min");
            max = argument.GetArgument<int>("Max");
            lv = argument.GetArgument<int>("Level");
            value = argument.GetArgument<Contract.Runable>("Value");
        }
        public override void Bind()
        {
            base.Bind();
            Control.ButtonClicked += Control_ButtonClicked;
        }
        void Control_ButtonClicked(object sender, EventArgs e)
        {
            Exp_Dialog dialog = new Exp_Dialog();
            dialog.MinValue = min;
            dialog.MaxValue = max;
            dialog.MaxLevel = lv;
            dialog.Complex = base.value;
            dialog.Value = value;
            dialog.Load();
            dialog.ShowDialog();
        }
    }
}
