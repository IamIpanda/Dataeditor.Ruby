using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public partial class FileChoser : UserControl
    {
        public FileChoser()
        {
            InitializeComponent();
            protoShapeShifterData1.Load();
        }

        private void protoShapeShifterData1_ValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = protoShapeShifterData1.SelectedPath;
        }
        public object SelectedValue
        {
            get { return protoShapeShifterData1.SelectedNode.Tag; }
        }
    }
}
