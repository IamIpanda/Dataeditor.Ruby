using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper.Container
{
    public class Actor : DataEditor.Control.WrapControlContainer< System.Windows.Forms.Panel>
    {
        public override string Flag { get { return "actor_parameters"; } }
        public override bool CanAdd(System.Windows.Forms.Control control)
        {
            if (control is Prototype.ProtoIntegerDisplayer || control is System.Windows.Forms.Label) return true;
            return false;
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.OverrideArgument("label", 0);
        }
        public void OnDoubleClick(DataEditor.Control.Wrapper.Actor sender)
        {
            if (value == null) return;
            Actor_Editor window = new Actor_Editor();
            foreach (System.Windows.Forms.Control control in Binding.Controls)
                if (control.Tag is Wrapper.Actor)
                {
                    var actor = control.Tag as Wrapper.Actor;
                    var label = actor.Label;
                    var title = label == null ? "" : label.Text;
                    int index = window.AddPage(actor.ListValue, actor.MaxValue, actor.MaxNumber, actor.color, title);
                    if (sender == actor) window.SelectedIndex = index;
                }
            if (window.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (System.Windows.Forms.Control control in Binding.Controls)
                    if (control.Tag is Wrapper.Actor)
                        (control.Tag as Wrapper.Actor).Push();
            }
            else
            {
                foreach (System.Windows.Forms.Control control in Binding.Controls)
                    if (control.Tag is Wrapper.Actor)
                        (control.Tag as Wrapper.Actor).Pull();
            }
        }
    }
}


namespace DataEditor.Control.Wrapper
{
    public class Actor : DataEditor.Control.WrapControlEditor<FuzzyData.FuzzyTable,DataEditor.Control.Prototype.ProtoIntegerDisplayer>
    {
        public int id { get; set; }
        public int MaxValue { get; set; }
        public int MaxNumber { get; set; }
        public System.Drawing.Color color { get; set; }
        public override string Flag { get { return "actor"; } }
        public override void Push() 
        {
            List<int> target = Control.Value;
            value.resize(value.xsize, target.Count);
            for (int i = 0; i < target.Count; i++)
                value[id, i] = Convert.ToInt16(target[i]);
            Control.Invalidate();
        }

        public override void Pull()
        {
            List<int> target = new List<int>();
            for (int i = 0; i < value.ysize; i++)
                target.Add(value[id, i]);
            Control.Value = target;
            Control.Invalidate();
        }
        public override bool ValueIsChanged()
        {
            return false;
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("index", 1);
            argument.SetArgument("color", 0);
            argument.SetArgument("max_number", 9999, Help.Parameter.ArgumentType.Option);
            argument.SetArgument("max_value", -1, Help.Parameter.ArgumentType.Option);
            argument.OverrideArgument("actual", null);
        }
        public override void Reset()
        {
            id = argument.GetArgument<int>("index");
            color = argument.GetArgument<System.Drawing.Color>("color");
            MaxNumber = argument.GetArgument<int>("max_number");
            MaxValue = argument.GetArgument<int>("max_value");
            if (MaxValue == -1) MaxValue = MaxNumber;
            // 数据前移
            Control.DataColor = color;
            Control.MaxNumber = MaxNumber;
            base.Reset();
        }
        public override void Bind()
        {
            base.Bind();
            Control.DoubleClick += Control_DoubleClick;
        }

        void Control_DoubleClick(object sender, EventArgs e)
        {
            var parent = base.Container as Wrapper.Container.Actor;
            if (parent == null) return;
            parent.OnDoubleClick(this);
        }
        public List<int> ListValue { get { return Control.Value; } }
    }
}
