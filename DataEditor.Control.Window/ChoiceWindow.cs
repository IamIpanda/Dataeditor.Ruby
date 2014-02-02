using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Window
{
    public partial class ChoiceWindow : Form
    {
        public ChoiceWindow()
        {
            InitializeComponent();
        }
        public ChoiceWindow(string title, string description, params string[] choices)
        {
            InitializeComponent();
            this.Text = title;
            label1.Text = description;
            protoListBox1.Items.AddRange(choices);
        }

        protected virtual void protoListBox1_DoubleClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected virtual void protoListBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            else if (e.KeyData == Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        public int Value
        {
            get { return protoListBox1.SelectedIndex; }
        }

        protected virtual void ChoiceWindow_Load(object sender, EventArgs e)
        {
            if (protoListBox1.Items.Count > 0)
                protoListBox1.SelectedIndex = 0;
        }
    }
}
