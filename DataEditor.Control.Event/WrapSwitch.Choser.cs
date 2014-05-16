using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Wrapper.Event
{
    public partial class SwitchChoser : Form
    {
        static public int Page { get; set; }
        public SwitchChoser()
        {
            InitializeComponent();
            protoLinedPaper1.textbook = new Help.Parameter.Text(GetString);
            this.Shown += SwitchChoser_Shown;
        }

        void SwitchChoser_Shown(object sender, EventArgs e)
        {
            protoListBox1.SelectedIndex = 0;
        }
        static SwitchChoser() { Page = 25; }
        public object GetString(params object[] args)
        {
            int index = Convert.ToInt32(args[0]);
            index += protoListBox1.SelectedIndex * Page;
            string value = args[1].ToString();
            return string.Format("{0:d3}: {1}", index, value);
        }
        private void btOK_Click(object sender, EventArgs e)
        {
            _value.Value = Index;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        FuzzyData.FuzzyFixnum _value;
        FuzzyData.FuzzyFixnum origin;
        public FuzzyData.FuzzyArray Switches { get; set; }
        public FuzzyData.FuzzyFixnum Value
        {
            get { return _value; }
            set { _value = value.Clone() as FuzzyData.FuzzyFixnum; origin = value; Pull(); }
        }
        void Pull()
        {
            int count = (Switches.Count - 2) / Page + 1;
            protoListBox1.Items.Clear();
            for (int i = 0; i < count; i++)
                protoListBox1.Items.Add(GetNumStr(i));
        }
        string GetNumStr(int index)
        {
            return string.Format("[{0:d3}..{1:d3}]", index * Page + 1, index * Page + Page);
        }
        public int Index
        {
            get { return protoListBox1.SelectedIndex * 10 + protoLinedPaper1.SelectedIndex; }
        }

        private void protoListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PullAnother();
        }

        void PullAnother()
        {
            int index = protoListBox1.SelectedIndex;
            List<object> target = new FuzzyData.FuzzyArray();
            if (index == protoListBox1.Items.Count - 1)
                target.AddRange(Switches.GetRange(index * Page + 1, (Switches.Count - 2) % Page + 1));
            else
                target.AddRange(Switches.GetRange(index * Page + 1, Page));
            var text = target.ConvertAll<string>(a => a.ToString());
            protoLinedPaper1.Value = text;
            protoLinedPaper1.Pull();
        }

        private void protoLinedPaper1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btOK_Click(this, e);
        }
    }
}
