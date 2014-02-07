using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class Complex : Control.WrapControlEditor<FuzzyData.FuzzyObject, Prototype.ProtoDropItem>
    {
        public override void Push()
        {
        }

        public override void Pull()
        {
            RefreshText();
        }

        public override bool ValueIsChanged()
        {
            return false;
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
        }
        public override string Flag { get { return "complex"; } }
        public override void Bind()
        {
            base.Bind();
            Control.ButtonClicked += Control_ButtonClicked;
        }

        void Control_ButtonClicked(object sender, EventArgs e)
        {
            var dialog = new Window.WindowWithOK.WrapWindowWithOK<Window.WindowWithOK>();
            var builder = argument.GetArgument<Contract.Runable>("WINDOW");
            if (builder == null) return;
            builder.call(dialog, value);
            if (dialog.Show() == System.Windows.Forms.DialogResult.OK)
            {
                RefreshText();
            }
        }
        void RefreshText()
        {
            var text = argument.GetArgument<Help.Parameter.Text>("TEXT");
            if (text == null) return;
            Control.Text = text.ToString(value);
            Control.Invalidate();
        }
    }
}
