using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class Button : WrapControlEditor<FuzzyData.FuzzyObject,System.Windows.Forms.Button>
    {
        public override string Flag { get { return "button"; } }
        Contract.Runable target = null;
        List<object> parameter = new List<object>();
        public override void Push() { }
        public override void Pull() { }
        public override bool ValueIsChanged() { return false; }
        public override FuzzyData.FuzzyObject Parent { set { } }
        public override void Bind()
        {
            base.Bind();
            Control.Click += Control_Click;
        }
        public override void Reset() 
        {
            base.Reset();
            target = argument.GetArgument<Contract.Runable>("RUN");
            parameter = argument.GetArgument<List<object>>("PARAMETER");
            Control.Text = argument.GetArgument<string>("TEXT");
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.OverrideArgument("label", 0, Help.Parameter.ArgumentType.HardlyEver);
            argument.SetArgument("parameter", new List<object>(), Help.Parameter.ArgumentType.Option);
            argument.SetArgument("run", null, Help.Parameter.ArgumentType.Must);
            argument.OverrideArgument("actual", null);
        }

        void Control_Click(object sender, EventArgs e)
        {
            if (target != null) target.call(parameter);
        }
    }
}
