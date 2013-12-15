using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Wrapper
{
    public class View : DataEditor.Control.WrapControlEditor<FuzzyData.FuzzyArray, Prototype.ProtoListView>
    {
        public override string Flag { get { return "View"; } }


        public override void Push() { /* 已作废 */ }

        public override void Pull()
        {
        }

        public override bool CheckValue()
        {
            return false;
        }
        public override void Reset()
        {
            SetColumns(argument.GetAegument<IEnumerable<object>>("COLUMNS"));
            base.Reset();
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.Defaults.Add("WINDOW", new Help.Return(null));
            argument.Defaults.Add("COLUMNS", new string[0]);
            argument.Defaults.Add("NEW", null);
        }
        public override void Bind()
        {
            base.Bind();
            Control.DoubleClick += Control_DoubleClick;
        }

        void Control_DoubleClick(object sender, EventArgs e)
        {
            var window_proc = argument.GetAegument<Contract.Runable>("WINDOW");
            if (window_proc == null) return;
            var window = new Control.Window.WindowWithOK.WrapWindowWithOK<Control.Window.WindowWithOK>();
            window_proc.call(window, SELECTED_VALUE);
            window.Show();
        }

        protected void SetColumns(IEnumerable<object> value)
        {
            if (value == null) return;
            Control.Columns.Clear();
            foreach (object target in value)
                Control.Columns.Add(target.ToString());
        }

        public class SingleLineText
        {
            List<Help.Parameter.Text> texts = new List<Help.Parameter.Text>();
            public new string[] ToString(FuzzyData.FuzzyObject value)
            {
                string[] ans = new string[texts.Count];
                for (int i = 0; i < texts.Count; i++)
                    ans[i] = texts[i].ToString(value, texts[i].Watch);
                return ans;
            }
        }
    }
}
