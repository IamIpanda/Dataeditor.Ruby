using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public partial class ProtoArticleInput : Form
    {
        public ProtoArticleInput()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void protoAutoSizeTextBox1_Resize(object sender, EventArgs e)
        {
            this.ClientSize = protoAutoSizeTextBox1.Size;
            btCancel.Location = new Point(protoAutoSizeTextBox1.Width - btCancel.Width, protoAutoSizeTextBox1.Height - btCancel.Height);   
        }
    }
}
