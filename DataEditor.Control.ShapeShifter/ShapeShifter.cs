using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.ShapeShifter
{
    public partial class ShapeShifter : Form
    {
        public ShapeShifter()
        {
            InitializeComponent();
        }

        private void protoShapeShifterData1_ValueChanged(object sender, EventArgs e)
        {
            protoShapeShifterValue1.Value = this.protoShapeShifterData1.Value;
        }

        private void ShapeShifter_Load(object sender, EventArgs e)
        {
            protoShapeShifterData1.Load();
        }
    }
}
