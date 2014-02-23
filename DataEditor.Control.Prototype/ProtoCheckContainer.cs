using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public partial class ProtoCheckContainer : UserControl
    {
        public ProtoCheckContainer()
        {
            InitializeComponent();
        }
        public new string Text
        {
            get { return checkBox1.Text; }
            set { checkBox1.Text = value; }
        }

        public CheckBox Radio { get { return checkBox1; } }

        private void CheckBox1_SizeChanged(object sender, EventArgs e)
        {
            ResetRadio();
        }

        private void panel2_SizeChanged(object sender, EventArgs e)
        {
            ResetRadio();
        }

        protected void ResetRadio()
        {
            /* Van!shment */
        }


        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Enabled = checkBox1.Checked;
        }
        public int RadioWidth
        {
            get { return (int)tableLayoutPanel1.ColumnStyles[0].Width; }
            set { tableLayoutPanel1.ColumnStyles[0].Width = value; }
        }
        public System.Windows.Forms.Control.ControlCollection PanelCollection
        {
            get { return panel1.Controls; }
        }
    }
}
