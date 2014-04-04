using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Wrapper
{
    public partial class Actor_Editor : Form
    {
        public Actor_Editor()
        {
            InitializeComponent();
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

        public void ClearPage()
        {
            tabControl1.TabPages.Clear();
        }
        public int AddPage(List<int> value,int max_value,int max_number,Color color,string text = "")
        {
            var page = new TabPage();
            page.Text = text;
            var editor = new Prototype.ProtoIntegerMixEditor();
            editor.Value = value;
            editor.MaxDisplay = max_number;
            editor.Maximum = max_value;
            editor.Color = color;
            editor.Dock = DockStyle.Fill;
            page.Controls.Add(editor);
            tabControl1.TabPages.Add(page);
            return tabControl1.TabPages.Count - 1;
        }
        public int SelectedIndex { set { tabControl1.SelectedIndex = value; } }

    }
}
