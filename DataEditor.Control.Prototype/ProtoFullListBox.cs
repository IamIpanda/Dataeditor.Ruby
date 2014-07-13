using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public partial class ProtoFullListBox : UserControl
    {
        public event EventHandler SelectedIndexChanged;
        public ProtoFullListBox()
        {
            InitializeComponent();
        }
        public System.Windows.Forms.Control.ControlCollection InnerControls
        {
            get { return panel1.Controls; }
        }
        public new string Text
        {
            get { return protoLeftListBox1.Text; }
            set { protoLeftListBox1.Text = value; }
        }
        public ListBox.ObjectCollection Items
        {
            get { return protoLeftListBox1.Items; }
        }
        public int SelectedIndex
        {
            get { return protoLeftListBox1.SelectedIndex; }
            set { protoLeftListBox1.SelectedIndex = value; }
        }
        public ProtoLeftListBox ListBox
        {
            get { return protoLeftListBox1; }
        }
        public ColorHashCollection BackColors
        {
            get { return protoLeftListBox1.BackColors; }
        }
        public ColorHashCollection ForeColors
        {
            get { return protoLeftListBox1.ForeColors; }
        }
        private void protoLeftListBox1_LeftBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, new EventArgs());
        }
        public int DeltaWidth
        {
            get { return this.Width - panel1.Width; }
        }
        public void AddMenu(string Text, EventHandler Handle, Keys Key)
        {
            protoLeftListBox1.AddMenu(Text, Handle, Key);
        }
        public void ClearMenu()
        {
            protoLeftListBox1.ClearMenu();
        }
    }
}
