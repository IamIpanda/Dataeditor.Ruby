using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Window
{
    public abstract class WrapAnyWindow<T> : Control.WrapBaseWindow<T> 
        where T : System.Windows.Forms.Form,new()
    {
        protected FuzzyData.FuzzyObject origin;
        // 把底层的值记录下来
        public override FuzzyData.FuzzyObject Value
        {
            get { return base.Value; }
            set
            {
                origin = value.Clone() as FuzzyData.FuzzyObject;
                base.Value = value;
            }
        }
        // 把底层的值 Hook 进来
        protected override FuzzyData.FuzzyObject GetValueFromChild(FuzzyData.FuzzyObject parent, FuzzyData.FuzzySymbol symbol)
        {
            var ans = base.GetValueFromChild(parent, symbol);
            origin = ans.Clone() as FuzzyData.FuzzyObject;
            return ans;
        }
        protected void FormClosing()
        {
            Push();
            var form = Window;
            if (form == null) return;
            FuzzyData.FuzzyObject temp;
            if (form.DialogResult == System.Windows.Forms.DialogResult.Cancel && origin != null)
                temp = base.Value & origin;
        }
        public override void Bind()
        {
            base.Bind();
            Window.FormClosing += Window_FormClosing;
        }

        protected virtual void Window_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            FormClosing();
        }
        public void BaseSetSize(System.Drawing.Size size) { base.SetSize(size); }
        public override void SetSize(System.Drawing.Size size) { }
        protected virtual void OnValueChanged() { }
        public override bool ValueIsChanged()
        {
            return false;
        }
    }
}
