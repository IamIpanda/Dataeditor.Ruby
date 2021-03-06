﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control
{
    public abstract class WrapBaseWindow : WrapBaseContainer
    {
        public override bool ValueIsChanged() { return false; }
        abstract public System.Windows.Forms.DialogResult Show();
        public object Tag { get; set; }
        protected override void SetEnabled() { }
        public string Text 
        {
            get { return Binding.Text; }
            set { Binding.Text = value; }
        }
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
        //public override string Flag { get { return typeof(T).Name; } }
        public override System.Windows.Forms.DialogResult Show() { return Window.ShowDialog(); }
        public bool ShowAndTell() { return (Window.ShowDialog() == System.Windows.Forms.DialogResult.OK); }
        public override void SetSize(System.Drawing.Size size) { Window.ClientSize = size; }
    }
}
