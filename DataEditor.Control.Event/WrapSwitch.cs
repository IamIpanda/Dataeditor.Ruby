using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper.Event
{
    public class Switch : Control.WrapControlEditor<FuzzyData.FuzzyFixnum, Prototype.ProtoDropItem>
    {
        protected FuzzyData.FuzzyArray data;
        protected String Title = "开关";
        protected bool NowTaint;
        public override string Flag { get { return "switch"; } }
        public override void Push()
        {
            NowTaint = false;
        }
        public override void Pull()
        {
            if (value.Value <= 0) value.Value = 1;
            int index = Convert.ToInt32(value.Value);
            Control.Text = string.Format("{0:d3}: ", index) + data[index].ToString();
        }
        public override bool ValueIsChanged()
        {
            return NowTaint;
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("data", null);
        }
        public override void Reset()
        {
            base.Reset();
            data = argument.GetArgument<FuzzyData.FuzzyArray>("data");
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
            window.Text = Title;
            if (window.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                NowTaint = true;
                Pull();
            }
        }

    }
}
