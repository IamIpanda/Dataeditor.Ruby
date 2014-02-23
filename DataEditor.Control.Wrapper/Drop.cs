using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class Drop : WrapControlEditor<FuzzyData.FuzzyObject,Prototype.ProtoDropItem>
    {
        Help.Parameter.Text text;
        bool NowTaint = false;
        public override string Flag { get { return "drop"; } }
        public override void Push() { NowTaint = false; }

        public override void Pull()
        {
            if (text != null)
                Control.Text = text.ToString(value);
        }

        public override bool ValueIsChanged()
        {
            return NowTaint;
        }

        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("textbook", null);
            argument.SetArgument("window", null);
            argument.SetArgument("window_type", 0, Help.Parameter.ArgumentType.Option);
            argument.OverrideArgument("actual", null, Help.Parameter.ArgumentType.Option);
        }
        public override void Bind()
        {
            base.Bind();
            Control.ButtonClicked += Control_ButtonClicked;
        }

        void Control_ButtonClicked(object sender, EventArgs e)
        {
            var window_proc = argument.GetArgument<Contract.Runable>("window");
            if (window_proc == null) return;
            var window_type = argument.GetArgument<int>("window_type");
            DataEditor.Control.WrapBaseWindow window;
            if (window_type == 0)
                window = new Control.Window.WindowWithOK.WrapWindowWithOK<Control.Window.WindowWithOK>();
            else window = new Control.Window.WindowWithRightOK.WrapWindowWithRightOK<Control.Window.WindowWithRightOK>();
            window_proc.call(window, value);
            if (window.Show() == System.Windows.Forms.DialogResult.OK)
            {
                NowTaint = true;
                Pull();
            }
        }
        public override void Reset()
        {
            text = argument.GetArgument<Help.Parameter.Text>("textbook");
            base.Reset();
        }
    }
}
