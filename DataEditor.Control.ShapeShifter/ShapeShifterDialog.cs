using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.ShapeShifter
{
    public partial class ShapeShifterDialog : Form
    {
        protected FuzzyData.FuzzyObject value;
        protected List<string> values = new List<string>();
        public FuzzyData.FuzzyObject Value
        {
            get { Collect();  return value; }
            set { this.value = value; Realize(); }
        }
        public ShapeShifterDialog()
        {
            InitializeComponent();
            values.Add("0");
            values.Add("Str");
            values.Add("False");
            values.Add(":Sym");
            values.Add("/Reg/");
            values.Add("[255, 255, 255, 0]");
            values.Add("[0, 0, 640, 480]");
            values.Add("[0, 0, 0, 255]");
            values.Add("1073741824");
            values.Add("Ruby::Nil");
            this.Shown += ShapeShifterDialog_Shown;
        }

        void ShapeShifterDialog_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
            textBox1.SelectAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        public void Realize()
        {
            if (value == null) return;
            if (value is FuzzyData.FuzzyBignum || value is FuzzyData.FuzzyFixnum) comboBox1.SelectedIndex = 0;
            else if (value is FuzzyData.FuzzyString) comboBox1.SelectedIndex = 1;
            else if (value is FuzzyData.FuzzyBool) comboBox1.SelectedIndex = 2;
            else if (value is FuzzyData.FuzzySymbol) comboBox1.SelectedIndex = 3;
            else if (value is FuzzyData.FuzzyRegexp) comboBox1.SelectedIndex = 4;
            else if (value is FuzzyData.FuzzyColor) comboBox1.SelectedIndex = 5;
            else if (value is FuzzyData.FuzzyRect) comboBox1.SelectedIndex = 6;
            else if (value is FuzzyData.FuzzyTone) comboBox1.SelectedIndex = 7;
            else if (value is FuzzyData.FuzzyNil) comboBox1.SelectedIndex = 9;
            textBox1.Text = value.ToString();
            textBox1.Enabled = !(value is FuzzyData.FuzzyNil);
        }
        public void Collect()
        {
            string content = textBox1.Text;
            if (comboBox1.SelectedIndex == 0)
            {
                long value = 0;
                if (long.TryParse(content, out value))
                {
                    if (value <= FuzzyData.FuzzyFixnum.MaxValue && value >= FuzzyData.FuzzyFixnum.MinValue)
                        this.value = new FuzzyData.FuzzyFixnum(value);
                    else this.value = new FuzzyData.FuzzyBignumAdapter(content);
                }
                else this.value = new FuzzyData.FuzzyFixnum(0);
            }
            else if (comboBox1.SelectedIndex == 1) this.value = new FuzzyData.FuzzyString(content);
            else if (comboBox1.SelectedIndex == 2) this.value = content.Substring(content.Length - 5, 5).ToLower() == "false" ?
                FuzzyData.FuzzyBool.True : FuzzyData.FuzzyBool.False;
            else if (comboBox1.SelectedIndex == 3)
            {
                if (content.StartsWith(":")) content = content.Substring(1);
                this.value = FuzzyData.FuzzySymbol.GetSymbol(content);
            }
            else if (comboBox1.SelectedIndex == 4)
            {
                if (content.StartsWith("/")) content = content.Substring(1);
                if (content.EndsWith("/")) content = content.Substring(0, content.Length - 1);
                this.value = new FuzzyData.FuzzyRegexp(new FuzzyData.FuzzyString(content),
                new FuzzyData.FuzzyRegexpOptions());
            }
            else if (comboBox1.SelectedIndex == 5) this.value = Interpreter.GetColor(content);
            else if (comboBox1.SelectedIndex == 6) this.value = Interpreter.GetRect(content);
            else if (comboBox1.SelectedIndex == 7) this.value = Interpreter.GetTone(content);
            else if (comboBox1.SelectedIndex == 8) this.value = new FuzzyData.FuzzyBignumAdapter(content);
            else if (comboBox1.SelectedIndex == 9) this.value = FuzzyData.FuzzyNil.Instance;

        }
        public string Key { get { return textBox2.Text; } set { textBox2.Text = value; } }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(comboBox1.Focused)) return;
            textBox1.Text = values[comboBox1.SelectedIndex];
            if (comboBox1.SelectedIndex == 9) textBox1.Enabled = false;
            else
            {
                textBox1.Enabled = true;
                textBox1.Focus();
                textBox1.SelectAll();
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            values[comboBox1.SelectedIndex] = textBox1.Text;
        }
        public bool KeyEnabled { set { textBox2.ReadOnly = !value; } }
    }
}
