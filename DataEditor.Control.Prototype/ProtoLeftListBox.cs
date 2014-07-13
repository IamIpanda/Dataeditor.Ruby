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

        public void AddMenu(string Text, EventHandler Handle, Keys Key)
        {
            ToolStripMenuItem item = new ToolStripMenuItem(Text, null, Handle, Key);
            contextMenuStrip1.Items.Add(item);
        }
        public void ClearMenu()
        {
            contextMenuStrip1.Items.Clear();
        }

        private void protoListBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                int y = e.Y / protoListBox1.ItemHeight;
                if (y >= 0 && y < protoListBox1.Items.Count)
                    protoListBox1.SelectedIndex = y;
            }
        }
    }
}
