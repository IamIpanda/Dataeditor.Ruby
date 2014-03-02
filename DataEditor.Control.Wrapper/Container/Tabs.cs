﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper.Container
{
    public class Tabs : WrapControlContainer<System.Windows.Forms.TabControl>
    {
        public override string Flag { get { return "tabs"; } }
        public override void Bind()
        {
            base.Bind();
            Control.Dock = System.Windows.Forms.DockStyle.Fill;
        }
        public override bool CanAdd(System.Windows.Forms.Control control)
        {
            return control is System.Windows.Forms.TabPage;
        }
        public override void SetSize(System.Drawing.Size size)
        {
            base.SetSize(new System.Drawing.Size(size.Width, size.Height + 25));
        }
    }
}
