using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class Buuton : WrapControlEditor<FuzzyData.FuzzyObject,System.Windows.Forms.Button>
    {
        public override string Flag { get { return "button"; } }
        Contract.Runable target = null;
        List<object> parameter = new List<object>();
        public override void Push() { }
        public override void Pull() { }
        public override bool CheckValue() { return false; }
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
            argument.Defaults["LABEL"] = 0;
            argument.Defaults.Add("PARAMETER", new List<object>());
            argument.Defaults.Add("RUN", null);
        }

        void Control_Click(object sender, EventArgs e)
        {
            if (target != null) target.call(parameter);
        }
    }
}
