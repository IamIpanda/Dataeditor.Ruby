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
    public partial class Option : Form
    {
        public Option()
        {
            InitializeComponent();
            checkBox1.Checked = Help.Environment.Instance.EnableAutoSave;
            checkBox2.Checked = Help.Environment.Instance.EnableAutoHint;
            checkBox3.Checked = Help.Environment.Instance.EnableLoading;
            checkBox4.Checked = Help.Environment.Instance.EnableFloatWindow;
            comboBox1.Text = Help.Environment.Instance.AutoSaveTimeSpan.ToString();
            comboBox1.Enabled = checkBox1.Enabled;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Control.Window.RtpEditor().ShowDialog();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Control.Window.PaletteEditor().ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Help.Environment.Instance.EnableAutoSave = checkBox1.Checked;
            Help.Environment.Instance.EnableAutoHint = checkBox2.Checked;
            Help.Environment.Instance.EnableLoading = checkBox3.Checked;
            Help.Environment.Instance.EnableFloatWindow = checkBox4.Checked;
            int value;
            if (int.TryParse(comboBox1.Text, out value))
                Help.Environment.Instance.AutoSaveTimeSpan = value;
            else Help.Log.log("无法格式化此数值：" + comboBox1.Text);
            Help.Environment.Save();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
