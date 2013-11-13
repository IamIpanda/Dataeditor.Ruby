using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public partial class ProtoIndexTextBox : UserControl
    {
        public ProtoIndexTextBox()
        {
            InitializeComponent();
        }

        internal class IndexBox : UserControl
        {
            protected DataEditor.Help.DefaultList<int> indecies = new Help.DefaultList<int>();
            public DataEditor.Help.DefaultList<int> Indecies { get { return indecies; } set { indecies = value; } }
            public TextBox Bound { get; set; }
            public new Font Font { get; set; }
            protected StringFormat Format = new StringFormat();
            public IndexBox()
            {
                this.SetStyle(ControlStyles.Selectable, false);
                this.SetStyle(ControlStyles.UserPaint, true);
                this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
                this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
                Format.LineAlignment = StringAlignment.Center;
                Format.LineAlignment = StringAlignment.Center;
            }
            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                if (Bound == null) return;
                Font Font = this.Font ?? Bound.Font;
                Brush b = new SolidBrush(Bound.ForeColor);
                for (int i = 0; i < indecies.Count; i++)
                    e.Graphics.DrawString(indecies[i].ToString(), Font, b,
                        new RectangleF(0, i * 12, this.Width, 12), Format);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void RefreshAllText(string Text)
        {
            Help.DefaultList<string> texts = new Help.DefaultList<string>();
            string[] parts = Text.Split('\n');
            int now = -1;
            string text;
            for (int i = 0; i < parts.Length; i++)
            {
                text = parts[i];
                int? num = Check(ref text);
                if (num.HasValue) texts[now = num.Value] = text;
                else texts[++now] = text;
            }
            Help.DefaultList<int> index = new Help.DefaultList<int>();
            for (int i = 0; i < texts.Count; i++)
                index[i] = i;
            StringBuilder sb = new StringBuilder();
            foreach (string str in texts)
                sb.AppendLine(str);
            textBox1.Text = sb.ToString();
            indexBox1.Indecies = index;
            indexBox1.Invalidate();
        }

        protected int? Check(ref string str)
        {
            if (str == "") return null;
            int index = 1;
            int num = 0;
            char c = str[0];
            for (; c >= '0' && c <= '9' && index < str.Length; c = str[index++])
                num = num * 10 + c - '0';
            if (index == 1) return null;
            str = str.Remove(0, index - 1);
            return num;
        }
        [Browsable(true)]
        public new string Text
        {
            get { return textBox1.Text; }
            set { RefreshAllText(value); }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            RefreshAllText(textBox1.Text);
        }
    }
}
