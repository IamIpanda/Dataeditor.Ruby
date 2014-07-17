using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public partial class ProtoExecuteDialog : Form
    {
        public const string __begin__ = "__BEGIN__";

        public ProtoExecuteDialog()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        public string Value
        {
            get { return GetText(); }
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

        private void ProtoExecuteDialog_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyData & Keys.Enter) > 0) btOK_Click(this, e);
            if ((e.KeyData & Keys.Menu) > 0 && (e.KeyData & Keys.Enter) > 0) btExecute_Click(this, e);

        }
        
        private void btExecute_Click(object sender, EventArgs e)
        {
            string code = GetText();
            object ans = Help.Bash.Call(code);
            string answer = ans == null ? "NULL POINTER" : ans.ToString();

            StringBuilder sb = new StringBuilder();
            string[] codes = protoLinedTextbox1.Text.Split('\n');
            foreach (string str in codes)
                if (str.Trim() != __begin__) sb.AppendLine(str);
            sb.AppendLine(">> " + answer);
            sb.AppendLine(__begin__);
            sb.AppendLine();

            protoLinedTextbox1.Text = sb.ToString();
            protoLinedTextbox1.Focus();
            protoLinedTextbox1.SetFocus(protoLinedTextbox1.Text.Length, 0);
        }

        protected string GetText()
        {
            StringBuilder sb = new StringBuilder();
            string[] code = protoLinedTextbox1.Text.Split('\n');
            foreach (string str in code)
                if (str.Trim() == __begin__) sb = new StringBuilder();
                else sb.AppendLine(str);
            return sb.ToString();
        }

    }
}
