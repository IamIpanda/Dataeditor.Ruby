using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public partial class ProtoIntegerMixEditor : UserControl
    {
        protected static List<string> SavedStrings = new List<string>();
        protected List<int> PreviousData = null;
        protected Color? PreviousColor;

        protected static int MaxRecordCount = 20;
        protected static string HintString = "在此输入或选择公式以生成...";

        public ProtoIntegerMixEditor()
        {
            InitializeComponent();
            SyncToSaved();
            protoComboBox1.Text = HintString;
        }
        static ProtoIntegerMixEditor()
        {
            LoadSettings();
        }
        protected static void SaveSettings()
        {
            DataEditor.Help.Option.SetOption(typeof(ProtoIntegerMixEditor), SavedStrings);
        }
        protected static void LoadSettings()
        {
            object argument;
            try
            {
                argument = DataEditor.Help.Option.GetOption(typeof(ProtoIntegerMixEditor));
            }
            catch { return; }
            List<string> saved = argument as List<string>;
            if (saved == null)
                return;
            SavedStrings = saved;
        }

        public List<int> Value
        {
            get { return protoIntegerEditor1.Value; }
            set { protoIntegerEditor1.Value = value; numericUpDown1.Value = value.Count; }
        }
        public Color Color
        {
            get { return protoIntegerEditor1.DataColor; }
            set
            {
                protoIntegerEditor1.DataColor = value;
                protoIntegerEditor1.Invalidate();
                protoIntegerText1.NewBackGroundColor = value;
                protoIntegerText1.NewForeColor = Color.White;
                protoIntegerText1.Invalidate();
            }
        }
        public int Maximum
        {
            get { return protoIntegerEditor1.MaxAdmitValue; }
            set { protoIntegerEditor1.MaxAdmitValue = value; }
        }
        public int Minimum
        {
            get { return protoIntegerEditor1.MinAdmitValue; }
            set { protoIntegerEditor1.MinAdmitValue = value; }
        }
        public int MaxDisplay
        {
            get { return protoIntegerEditor1.MaxNumber; }
            set { protoIntegerEditor1.MaxNumber = value; protoIntegerEditor1.Invalidate(); }
        }

        protected void SyncToSaved()
        {
            protoComboBox1.Items.Clear();
            foreach (string s in SavedStrings)
                protoComboBox1.Items.Add(s);
        }
        private void protoComboBox1_TextChanged(object sender, EventArgs e)
        {
            if (protoComboBox1.Text == "" || protoComboBox1.Text == HintString)
                return;
            Help.Calculator.QuickCalculator qc = null;
            try
            {
                qc = DataEditor.Help.Calculator.QuickCalculator.FromString(protoComboBox1.Text);
                List<int> li = new List<int>();
                for (int i = 0; i < protoIntegerEditor1.Value.Count; i++)
                    li.Add(Check(Convert.ToInt32(qc[i])));
                if (PreviousData == null)
                    PreviousData = protoIntegerEditor1.Value;
                if (PreviousColor == null)
                    PreviousColor = protoIntegerEditor1.DataColor;
                protoIntegerEditor1.DataColor = Color.Gray;
                protoIntegerEditor1.Value = li;
                protoIntegerEditor1.Invalidate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private void protoComboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                ConcernValue();
            }
            else if (e.KeyChar == 27)
                CancelValue();
        }

        private void protoComboBox1_Leave(object sender, EventArgs e)
        {
            CancelValue();
        }
        protected void ConcernValue()
        {
            PreviousData.Clear();
            PreviousData.AddRange(Value);
            Value = PreviousData;
            PreviousData = null;
            if (PreviousColor != null)
                protoIntegerEditor1.DataColor = PreviousColor ?? Color.Gray;
            protoIntegerEditor1.Invalidate();
            if (protoComboBox1.Text != "" && protoComboBox1.Text != HintString)
                if (!(SavedStrings.Contains(protoComboBox1.Text)))
                {
                    SavedStrings.Add(protoComboBox1.Text);
                    if (SavedStrings.Count > 50)
                        SavedStrings.RemoveAt(0);
                    SyncToSaved();
                    SaveSettings();
                }
            protoComboBox1.Text = HintString;
            protoComboBox1.ForeColor = Color.Silver;
            protoIntegerEditor1.Focus();
        }
        protected void CancelValue()
        {
            if (PreviousData != null)
                protoIntegerEditor1.Value = PreviousData;
            PreviousData = null;
            if (PreviousColor != null)
                protoIntegerEditor1.DataColor = PreviousColor ?? Color.Gray;
            protoIntegerEditor1.Invalidate();
            protoComboBox1.Text = HintString;
            protoComboBox1.ForeColor = Color.Silver;
            if (protoComboBox1.Focused)
                protoIntegerEditor1.Focus();
        }

        protected int Check(int i)
        {
            if (i < Minimum)
                return Minimum;
            if (i > Maximum)
                return Maximum;
            return i;
        }

        private void protoComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void protoComboBox1_Enter(object sender, EventArgs e)
        {
            protoComboBox1.Text = "";
            protoComboBox1.ForeColor = Color.Black;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            ChangeLength(Convert.ToInt32(numericUpDown1.Value));
            protoIntegerEditor1.Invalidate();
            protoIntegerText1.OnFullValueChanged(this, new EventArgs());
        }
        public void ChangeLength(int length)
        {
            int origin = protoIntegerEditor1.Value.Count;
            int target = length;
            if (origin > target) protoIntegerEditor1.Value.RemoveRange(target, origin - target);
            if (origin < target)
                for (int i = 0; i < target - origin; i++)
                    protoIntegerEditor1.Value.Add(0);
        }
    }
}
