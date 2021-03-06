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
            Control.SelectedIndexChanged += Control_SelectedIndexChanged;
            Control.ControlAdded += Control_ControlAdded;
            //Control.
        }

        void Control_ControlAdded(object sender, System.Windows.Forms.ControlEventArgs e)
        {
            if (Control.TabPages.Count == 1)
            {
                Control.SelectedIndex = 0;
                Control_SelectedIndexChanged(this, e);
            }
        }

        void Control_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tabpage = Control.SelectedTab;
            var tab = tabpage.Tag as Tab;
            if (tab == null) return;
            var size = tab.RememberSize;
            SetSize(size);
        }
        public override bool CanAdd(System.Windows.Forms.Control control)
        {
            return control is System.Windows.Forms.TabPage;
        }
        public override void SetSize(System.Drawing.Size size)
        {
            base.SetSize(new System.Drawing.Size(size.Width, size.Height + 25));
        }
        protected override void SetEnabled() {
        }
    }
}
