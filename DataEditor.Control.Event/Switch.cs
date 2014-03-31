using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper.Event
{
    public class Switch : Control.WrapControlEditor<FuzzyData.FuzzyFixnum, Prototype.ProtoDropItem>
    {
        protected FuzzyData.FuzzyArray data;
        protected Help.Parameter.Text text;
        protected bool NowTaint;
        public override string Flag { get { return "switch"; } }
        public override void Push()
        {
            NowTaint = false;
        }
        public override void Pull()
        {
            int index = Convert.ToInt32(value.Value);
            Control.Text = text.ToString(data[index]);
        }

        public override bool ValueIsChanged()
        {
            return NowTaint;
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("data", null);
            argument.OverrideArgument("text", null);
        }
        public override void Reset()
        {
            base.Reset();
            data = argument.GetArgument<FuzzyData.FuzzyArray>("data");
            text = argument.GetArgument<Help.Parameter.Text>("text");
        }
        public override void Bind()
        {
            base.Bind();
            Control.ButtonClicked += Control_ButtonClicked;
        }

        void Control_ButtonClicked(object sender, EventArgs e)
        {
            var window = new SwitchChoser();
            window.Switches = data;
            window.Value = value;
            if (window.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                NowTaint = true;
        }

    }
}
