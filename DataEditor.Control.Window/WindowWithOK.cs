using System;
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
            public override System.Windows.Forms.Control.ControlCollection Controls
            { get { return this.Window.pnMain.Controls; } }
            public override int end_y { get { return 30; } }
        }
    }
}
