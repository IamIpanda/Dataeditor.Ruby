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
        public FuzzyData.FuzzyObject Value { get; set; }
        public FuzzyData.FuzzyObject SelectedValue { get; set; }
        public FileChoser()
        {
            InitializeComponent();
            protoShapeShifterData1.Load();
        }

        private void protoShapeShifterData1_ValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = protoShapeShifterData1.SelectedNode.Name;
        }
    }
}
