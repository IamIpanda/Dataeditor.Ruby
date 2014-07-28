using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public partial class ProtoAutoSizeTextbox_PopupWindow : Form
    {
        public ProtoAutoSizeTextbox_PopupWindow()
        {
            InitializeComponent();
        }
        public string Value
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            this.Close();
        }
        public Size MinSize { get; set; }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var prefer = this.textBox1.PreferredSize;
            var width = MinSize.Width;
            var height = MinSize.Height;
            if (prefer.Width > width) width = prefer.Width;
            if (prefer.Height > height) height = prefer.Height;
            var bound = Screen.PrimaryScreen.Bounds;
            if (Width + this.Location.X + 4 > bound.Width) width = bound.Width - this.Location.X - 4;
            if (height + this.Location.Y + 4 > bound.Height) 
            {
                height = bound.Height - this.Location.Y - 4;
                textBox1.ScrollBars = ScrollBars.Vertical;
            }
            else textBox1.ScrollBars = ScrollBars.None; 
            this.SetClientSizeCore(width, height);
        }

        private void ProtoAutoSizeTextbox_PopupWindow_Leave(object sender, EventArgs e)
        {
            this.Close();
        }
        public void SetColor(Color ForeColor, Color BackColor)
        {
            this.textBox1.ForeColor = ForeColor;
            this.textBox1.BackColor = BackColor;
        }
        public void SetIndex(int index, int length)
        {
            textBox1.SelectionStart = index;
            textBox1.SelectionLength = length;
        }

        private void ProtoAutoSizeTextbox_PopupWindow_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
