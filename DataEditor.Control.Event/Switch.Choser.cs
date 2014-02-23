using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Wrapper.Event
{
    public partial class SwitchChoser : Form
    {
        public SwitchChoser()
        {
            InitializeComponent();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        FuzzyData.FuzzyArray _value;
        FuzzyData.FuzzyArray origin;
        public FuzzyData.FuzzyArray Value
        {
            get { return _value; }
            set { _value = value.Clone() as FuzzyData.FuzzyArray; origin = value; Pull(); }
        }
        void Pull()
        {
            int count = (_value.Count - 1) / 10 + 1;
            protoListBox1.Items.Clear();
            for (int i = 0; i < count; i++)
                protoListBox1.Items.Add(GetNumStr(i));
        }
        string GetNumStr(int index)
        {
            return string.Format("[{0:d3}..{0:d3}]", index * 10 + 1, index * 10 + 10);
        }
        void PullAnother()
        {
            int index = protoListBox1.SelectedIndex;
        }
    }
}
