using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Wrapper.Container
{
    public partial class List_MaxSelector : Form
    {
        public List_MaxSelector()
        {
            InitializeComponent();
            this.Shown += List_MaxSelector_Shown;
        }

        void List_MaxSelector_Shown(object sender, EventArgs e)
        {
            numericUpDown1.Focus();
            numericUpDown1.Select(0, 99);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        public int Value
        {
            get { return Convert.ToInt32(numericUpDown1.Value); }
            set { numericUpDown1.Value = value; }
        }
        public bool Checked
        {
            get { return checkBox1.Checked; }
        }
    }
}
