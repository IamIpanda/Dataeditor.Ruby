﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper.Container
{
    public class Tab : WrapControlContainer<System.Windows.Forms.TabPage>
    {
        public override string Flag { get { return "tab"; } }
        public override int start_x { get { return 2; } }
        public override int start_y { get { return 2; } }
        public override int end_x { get { return 2; } }
        public override int end_y { get { return 2; } }
    }
}