using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Window
{
    public partial class SelectWindow : Form
    {
        public SelectWindow()
        {
            InitializeComponent();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }
        public string Description
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }
        public int ShowDialog(string title, string description = "", int default_choice = 0, bool can_close = true, params string[] choices)
        {
            this.Text = title;
            label1.Text = description;
            btCancel.Visible = can_close;
            protoListBox1.Items.Clear();
            protoListBox1.Items.AddRange(choices);
            protoListBox1.SelectedIndex = default_choice;
            if (ShowDialog() == System.Windows.Forms.DialogResult.OK) return protoListBox1.SelectedIndex;
            else return -1;
        }

        private void protoListBox1_DoubleClick(object sender, EventArgs e)
        {
            btOK_Click(sender, e);
        }
    }
}
