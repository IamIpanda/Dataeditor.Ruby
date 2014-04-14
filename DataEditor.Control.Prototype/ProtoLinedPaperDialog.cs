using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public partial class ProtoLinedPaperDialog : Form
    {
        public ProtoLinedPaperDialog()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        public string Value
        {
            get { return protoLinedTextbox1.Text; }
            set { protoLinedTextbox1.Text = value; }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        public void SetFocus(int start, int length)
        {
            if (protoLinedTextbox1.Focused)
                protoLinedTextbox1.SetFocus(start, length);
            else
            { reserveLength = length; reserveStart = start; }
        }
        int reserveStart = -1, reserveLength = -1;
        private void ProtoLinedPaperDialog_Shown(object sender, EventArgs e)
        {
            if (reserveStart > 0 && reserveLength > 0)
            {
                protoLinedTextbox1.SetFocus(reserveStart, reserveLength);
                reserveLength = reserveStart = -1;
            }
        }
    }
}
