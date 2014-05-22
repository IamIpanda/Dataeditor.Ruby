using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.ShapeShifter
{
    public partial class Search : Form
    {
        public Search()
        {
            InitializeComponent();
            this.Shown += Search_Shown;
        }

        void Search_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
            textBox1.SelectAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        public Help.Finder.SearchOption Option
        {
            get 
            {
                return new Help.Finder.SearchOption(textBox1.Text, checkBox1.Checked, checkBox2.Checked);
            }
            set
            {
                textBox1.Text = value.Target;
                checkBox1.Checked = value.Name;
                checkBox2.Checked = value.Value;
            }
        }
    }
}
