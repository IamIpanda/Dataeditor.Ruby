using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Arce
{
    public partial class ChangeInWaveDialog : Form
    {
        public ChangeInWaveDialog()
        {
            InitializeComponent();
            this.Shown += ChangeInWaveDialog_Shown;
        }

        void ChangeInWaveDialog_Shown(object sender, EventArgs e)
        {
            protoListBox4.Items.Clear();
            foreach (var name in Help.Data.Instance.Names)
                protoListBox4.Items.Add(name);
            protoListBox3.Items.Clear();
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<FuzzyData.FuzzyObject> target = new List<FuzzyData.FuzzyObject>();
            foreach (object obj in target_array)
                target.Add(obj as FuzzyData.FuzzyObject);
            int min = (int)numericUpDown1.Value, max = (int)numericUpDown2.Value;
            int mode = 0;
            string ruby_code = "";
            Contract.Runable ruby_proc = null;
            if (radioButton1.Checked == true) mode += 4;
            else 
            {
                ruby_code = "r = Proc.new do |obj|\n" + textBox1.Text + "\nend\nr.to_p";
                ruby_proc = Help.Bash.Call(ruby_code) as Contract.Runable;
            }
            if (target_example[target_symbol] is FuzzyData.FuzzyString) mode += 2;
            FuzzyData.FuzzyObject source = null;
            FuzzyData.FuzzyFixnum target_fixnum = null;
            FuzzyData.FuzzyString target_string = null;
            for (int i = min; i <= max; i++)
            {
                source = target[i];
                if (mode % 4 == 0) // FuzzyFixnum
                {
                    target_fixnum = source[target_symbol] as FuzzyData.FuzzyFixnum;
                    if (target_fixnum == null) continue;
                    if (mode == 4) target_fixnum.Value = value[i - min]; // 机器生成
                    else // Ruby 生成
                    {
                        object obj = ruby_proc.call(source);
                        if (obj is int || obj is long)
                            target_fixnum.Value = Convert.ToInt64(obj);
                        else Help.Log.log("Ruby returned a variable that is not fixnum.");
                        if (obj == null)
                        {
                            MessageBox.Show("由于Ruby返回了一个空值，批量更改已取消。", "批量更改");
                            break;
                        }
                    }
                }
                else // FuzzyString
                {
                    target_string = source[target_symbol] as FuzzyData.FuzzyString;
                    if (target_string == null) continue;
                    object obj = ruby_proc.call(source); 
                    if (obj == null)
                    {
                        MessageBox.Show("由于Ruby返回了一个空值，批量更改已取消。", "批量更改");
                        break;
                    }
                    target_string.Text = obj.ToString();
                }
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton2.Checked) return;
            groupBox1.Enabled = false;
            label2.Enabled = true;
            textBox1.Enabled = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (!radioButton1.Checked) return;
            groupBox1.Enabled = true;
            label2.Enabled = false;
            textBox1.Enabled = false;
        }

        private void protoListBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            protoListBox3.Items.Clear();
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            var target = Help.Data.Instance[protoListBox4.SelectedItem.ToString()] as FuzzyData.FuzzyArray;
            if (target == null) { protoListBox3.Items.Add("不支持的种类"); return; }
            target_array = target.SkipWhile<object>((o) => o == FuzzyData.FuzzyNil.Instance || (!(o is FuzzyData.FuzzyObject)));
            target_example = target_array.First<object>((o) => o is FuzzyData.FuzzyObject) as FuzzyData.FuzzyObject;
            IEnumerable<FuzzyData.FuzzySymbol> result = target_example.InstanceVariables.Keys;
            IEnumerable<FuzzyData.FuzzySymbol> keys;
            foreach(FuzzyData.FuzzyObject obj in target_array)
            {
                keys = obj.InstanceVariables.Keys;
                result = result.Intersect<FuzzyData.FuzzySymbol>(keys);
            }
            List<FuzzyData.FuzzySymbol> ans1 = new List<FuzzyData.FuzzySymbol>();
            List<FuzzyData.FuzzySymbol> ans2 = new List<FuzzyData.FuzzySymbol>();
            object shot;
            foreach(var k in result)
            {
                shot = target_example[k];
                if (shot == null) continue;
                if (shot is FuzzyData.FuzzyFixnum) ans1.Add(k);
                if (shot is FuzzyData.FuzzyString) ans2.Add(k);
            }
            var ans = new List<FuzzyData.FuzzySymbol>();
            ans.AddRange(ans1);
            ans.AddRange(ans2);
            foreach (var key in ans)
                protoListBox3.Items.Add("[" + target_example[key].GetType().Name.Substring(5) + "] " + key.Name);
            syms.Clear();
            syms.AddRange(ans);
            if (protoListBox3.Items.Count == 0)
                protoListBox3.Items.Add("没有可用项");
            else
            {
                panel1.Enabled = true;
                numericUpDown1.Maximum = numericUpDown2.Maximum = target_array.Count<object>() - 1;
                numericUpDown1.Value = 0;
                numericUpDown2.Value = numericUpDown2.Maximum;
            }
        }
        List<FuzzyData.FuzzySymbol> syms = new List<FuzzyData.FuzzySymbol>();
        FuzzyData.FuzzyObject target_example;
        FuzzyData.FuzzySymbol target_symbol;
        IEnumerable<object> target_array;
        private void protoListBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            target_symbol = null;
            var index = protoListBox3.SelectedIndex;
            if (index < 0 || index >= syms.Count) return;
            if (target_array == null) return;
            target_symbol = syms[index];
            var target = target_example[target_symbol];
            if (target is FuzzyData.FuzzyString)
            {
                radioButton1.Enabled = false;
                radioButton2.Checked = true;
            }
            else radioButton1.Enabled = true;
            radioButton2.Enabled = true;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown2.Value < numericUpDown1.Value)
                numericUpDown2.Value = numericUpDown1.Value;
            CalcValue();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown2.Value < numericUpDown1.Value)
                numericUpDown1.Value = numericUpDown2.Value;
            CalcValue();
        }

        private void protoAutoSizeTextBox1_TextChanged(object sender, EventArgs e)
        {
            CalcValue();
        }
        List<int> value = new List<int>();
        void CalcValue()
        {
            protoListBox1.Items.Clear();
            Help.Calculator.QuickCalculator qc = null;
            try
            {
                int min = Convert.ToInt32(numericUpDown1.Value), max = Convert.ToInt32(numericUpDown2.Value);
                qc = DataEditor.Help.Calculator.QuickCalculator.FromString(this.protoAutoSizeTextBox1.Text);
                int ans;
                for (int i = min; i < max; i++)
                {
                    ans = (int)qc[i];
                    value.Add(ans);
                    protoListBox1.Items.Add(i.ToString() + " => " + ans.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                protoListBox1.Items.Clear();
                protoListBox1.Items.Add("Error");
                return;
            }
        }

        private void protoListBox1_EnabledChanged(object sender, EventArgs e)
        {
            protoListBox1.Items.Clear();
            protoAutoSizeTextBox1.Text = "";
        }
    }
}
