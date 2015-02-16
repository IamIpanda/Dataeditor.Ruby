using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Window
{
    public partial class WindowWithRightOK : Form
    {
        public WindowWithRightOK()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BackColor = Help.Painter.Instance[1];
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (!btOK.Focused) btOK.Focus();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        public class WrapWindowWithRightOK<T> : WrapBaseWindow<T> where T : WindowWithRightOK, new()
        {
            protected FuzzyData.FuzzyObject origin;
            protected bool SetByParent = true;
            public override System.Windows.Forms.Control.ControlCollection Controls
            { get { return this.Window.pnMain.Controls; } }
            public override int end_y { get { return 12; } }
            public override int end_x { get { return 100; } }
            public override FuzzyData.FuzzyObject Value
            {
                // get Changed. Now Value returns the real value
                // but not the origin value. 
                // I don't know what I'm thinking when I wrote down
                // this stupid code.
                get { return value; }
                set
                {
                    SetByParent = false;
                    origin = value.Clone() as FuzzyData.FuzzyObject;
                    base.Value = value;
                }
            }
            public override FuzzyData.FuzzyObject Parent
            {
                get { return parent; }
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
        public class ApplicationRightWindow : WrapWindowWithRightOK<WindowWithRightOK>
        {
            public override string Flag { get { return "dialog_r"; } }
        }
    }
}
