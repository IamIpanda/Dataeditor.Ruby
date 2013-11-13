using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public partial class ProtoLeftListBox : UserControl
    {
        public event EventHandler LeftBoxSelectedIndexChanged;
        public new string Text
        {
            get { return protoTitleBox1.Text; }
            set { protoTitleBox1.Text = value; }
        }
        public ProtoLeftListBox()
        {
            InitializeComponent();
        }

        private void protoListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LeftBoxSelectedIndexChanged != null)
                LeftBoxSelectedIndexChanged(this, new EventArgs());
        }
        public int SelectedIndex
        {
            get { return protoListBox1.SelectedIndex; }
            set { protoListBox1.SelectedIndex = value; }
        }
        public ListBox.ObjectCollection Items
        {
            get { return protoListBox1.Items; }
        }
        public ColorHashCollection ForeColors
        {
            get { return protoListBox1.ForeColors; }
        }
        public ColorHashCollection BackColors
        {
            get { return protoListBox1.BackColors; }
        }
        
    }
}
