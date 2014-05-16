﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class Text : DataEditor.Control.WrapBaseEditor<FuzzyData.FuzzyString>
    {
        Prototype.ProtoAutoSizeTextBox control = new Prototype.ProtoAutoSizeTextBox();
        public override string Flag { get { return "text"; } }
        public override void Bind() { Binding = control; }
        public override void Push()
        {
            if (value == null) return;
            value.Text = control.Text;
        }
        public override void Pull()
        {
            if (value == null) return;
            control.Text = value.Text;
        }
        public override bool ValueIsChanged()
        {
            if (value == null) return false;
            return control.Text != value.Text;
        }
    }
}
