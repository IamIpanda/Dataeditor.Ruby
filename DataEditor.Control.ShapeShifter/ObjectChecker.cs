using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.ShapeShifter
{
    public partial class ObjectChecker : Form
    {
        public ObjectChecker()
        {
            InitializeComponent();
        }

        private void protoShapeShifterList1_ValueChanged(object sender, EventArgs e)
        {
            protoShapeShifterValue1.Value = this.protoShapeShifterList1.Value;
        }
        protected FuzzyData.FuzzyObject _value;
        public FuzzyData.FuzzyObject Value
        {
            get { return _value; }
            set { _value = value; protoShapeShifterList1.Container = value; }
        }
    }
}


namespace DataEditor.Help
{
    public class ShapeShifter
    {
        public static void ShowObject(FuzzyData.FuzzyObject obj,string text = "ans")
        {
            var form = new DataEditor.Control.ShapeShifter.ObjectChecker();
            form.Value = obj;
            form.ShowDialog();
            form.Text = text;
        }
    }
}
