using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public partial class ProtoLinedPaper : UserControl
    {
        int id = -1;
        public ProtoLinedPaper()
        {
            InitializeComponent();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            GetInEdit();
        }

        protected List<string> _value;
        public List<string> Value {
            get { return _value; }
            set { _value = value; Pull(); }
        }
        public Help.Parameter.Text textbook { get; set; }
        public void Pull()
        {
            if (textbook == null) return; 
            listBox1.Items.Clear();
            for (int i = 0; i < _value.Count; i++)
            {
                var str = _value[i];
                var ans = textbook.ToString(i + 1, str);
                listBox1.Items.Add(ans);
            }
            listBox1.Items.Add("");
            int prefer = listBox1.PreferredHeight;
            if (prefer > panel1.Height) listBox1.Height = prefer;
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (id == Value.Count) Value.Add(textBox1.Text);
                else Value[id] = textBox1.Text;
                Pull();
                if (e.Shift)
                {
                    listBox1.SelectedIndex = id + 1;
                    GetInEdit();
                }
                else
                {
                    if (e.Control) ShowAll();
                    textBox1.Visible = false;
                    listBox1.Enabled = true;
                    listBox1.Focus();
                    listBox1.SelectedIndex = id;
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                textBox1.Visible = false;
                listBox1.Enabled = true;
                listBox1.Focus();
                listBox1.SelectedIndex = id;
                int x = id * listBox1.ItemHeight - panel1.VerticalScroll.Value;
                if (x > panel1.Height - textBox1.Height)
                    panel1.VerticalScroll.Value += x - panel1.Height + textBox1.Height;
            }
        }

        private void listBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                GetInEdit();
        }
        void GetInEdit()
        {
            if (Value == null || textbook == null) return;
            id = listBox1.SelectedIndex;
            if (id < 0 || id > Value.Count) return;
            listBox1.Enabled = false;
            int x = id * listBox1.ItemHeight - panel1.VerticalScroll.Value;
            if (x > panel1.Height - textBox1.Height)
            {
                int sub = x - panel1.Height + textBox1.Height;
                x -= sub;
                panel1.VerticalScroll.Value += sub;
            }
            textBox1.Visible = true;
            textBox1.Location = new Point(0, x - 2);
            textBox1.Text = id == Value.Count ? "" : Value[id];
            textBox1.Focus();
            textBox1.SelectAll();
        }

        private void ProtoLinedPaper_SizeChanged(object sender, EventArgs e)
        {
            textBox1.Size = panel1.Size;
            Pull();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            listBox1.Enabled = true;
        }
        public int SelectedIndex
        {
            get { return listBox1.SelectedIndex; }
            set { listBox1.SelectedIndex = value; }
        }

        private void textBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowAll();
        }

        protected void ShowAll()
        {
            var index = listBox1.SelectedIndex;
            var selectedText = 0;
            var dialog = new ProtoLinedPaperDialog();
            var text = new StringBuilder();
            string chosen;
            for (int i = 0; i < Value.Count; i++ )
            {
                chosen = Value[i];
                text.AppendLine(chosen);
                if (i < index)
                    selectedText += chosen.Length + 1;
            }
            text.Remove(text.Length - 1, 1);
            //FIXME: selectedText is WRONG
            //if (index > 0)
              //  dialog.SetFocus(selectedText, Value[index].Length);
            dialog.Value = text.ToString();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Value.Clear();
                Value.AddRange(dialog.Value.Split('\n'));
                Pull();
            }
        }

        private void panel1_SizeChanged(object sender, EventArgs e)
        {
            this.listBox1.Size = panel1.ClientSize;
        }
    }
}
