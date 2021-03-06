﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper.Container
{
    public class Group : WrapControlContainer<System.Windows.Forms.GroupBox>
    {
        public override string Flag { get { return "group"; } }
        public override int start_x { get { return 4; } }
        public override int start_y { get { return 16; } }
        public override int end_x { get { return 4; } }
        public override int end_y { get { return 8; } }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.OverrideArgument("text", "", Help.Parameter.ArgumentType.Option);
        }
        public override void Bind()
        {
            base.Bind();
            Control.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        }
    }
}
