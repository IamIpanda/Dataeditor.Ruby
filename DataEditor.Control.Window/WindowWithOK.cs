﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Window
{
    public partial class WindowWithOK : Form
    {
        public WindowWithOK()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        public class WrapWindowWithOK<T> : WrapBaseWindow<T> where T :WindowWithOK,new()
        {
            public FuzzyData.FuzzyObject origin;
            protected bool SetByParent = true;
            public override System.Windows.Forms.Control.ControlCollection Controls
            { get { return this.Window.pnMain.Controls; } }
            public override int end_y { get { return 30; } }
            public override int end_x { get { return 20; } }
            public override FuzzyData.FuzzyObject Value
            {
                get { return origin; }
                set
                {
                    SetByParent = false;
                    origin = value.Clone() as FuzzyData.FuzzyObject;
                    base.Value = value;
                }
            }
            public override FuzzyData.FuzzyObject Parent
            {
                get { return origin; }
                set
                {
                    SetByParent = true;
                    origin = value.Clone() as FuzzyData.FuzzyObject;
                    base.Parent = value;
                }
            }
            public override void Bind()
            {
                base.Bind();
                Window.FormClosing += Window_FormClosing;
            }
            void Window_FormClosing(object sender, FormClosingEventArgs e)
            {
                var form = sender as System.Windows.Forms.Form;
                if (form == null) return;
                FuzzyData.FuzzyObject temp;
                if (form.DialogResult == DialogResult.Cancel)
                    if (SetByParent)
                        temp = base.Parent & origin;
                    else
                        temp = base.Value & origin;
            }
        }
        public class AppliactionWindow : WrapWindowWithOK<WindowWithOK>
        {
            public override string Flag { get { return "dialog"; } }
        }
    }
}
