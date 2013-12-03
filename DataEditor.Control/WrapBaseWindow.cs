using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control
{
    public abstract class WrapBaseWindow : WrapBaseContainer
    {
        public override bool CheckValue() { return false; }
    }
    public class WrapBaseWindow<T> : WrapBaseWindow where T : System.Windows.Forms.Form, new()
    {
        private T window = new T();
        public T Window
        {
            get { return window; }
            set { window = value; }
        }
        public override void Bind() { Binding = Window; }
        public override string Flag { get { return typeof(T).Name; } }
        public System.Windows.Forms.DialogResult Show() { return Window.ShowDialog(); }
        public override void SetSize(System.Drawing.Size size) { Window.ClientSize = size; }

    }
}
