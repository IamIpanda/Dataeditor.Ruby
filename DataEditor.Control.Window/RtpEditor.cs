using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataEditor.Help;

namespace DataEditor.Control.Window
{
    public partial class RtpEditor : Form
    {
        public RtpEditor()
        {
            InitializeComponent();
            List<Rtp> list = Help.Path.RTPManager.RtpList;
            list.ForEach(rtp => this.protoRtpList1.Items.Add(rtp));
            protoRtpList1.Items.Add("");
            protoRtpList1.SelectedIndex = 0;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void protoRtpList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var obj = protoRtpList1.SelectedItem;
            if (!(obj is Rtp))
            {
                tbPath.Text = tbName.Text = tbVersion.Text = "";
                button1.Text = "添加";
                button3.Enabled = button1.Enabled = true;
                button4.Enabled = false;
                tbPath.ReadOnly = tbName.ReadOnly = tbVersion.ReadOnly = false;
                tbName.Focus();
                return;
            }
            var rtp = (Rtp)obj;
            tbName.Text = rtp.Name;
            tbVersion.Text = rtp.Version;
            tbPath.Text = rtp.Path;
            tbPath.ReadOnly = tbName.ReadOnly = tbVersion.ReadOnly = rtp.IsFromReg;
            button3.Enabled = button4.Enabled = button1.Enabled = !rtp.IsFromReg;
            button1.Text = "修改";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (fbD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbPath.Text = fbD.SelectedPath;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            protoRtpList1.Items.RemoveAt(protoRtpList1.SelectedIndex);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Rtp rtp = new Rtp();
            rtp.Path = tbPath.Text;
            rtp.Version = tbVersion.Text;
            rtp.Name = tbName.Text;
            rtp.IsFromReg = false;
            
            protoRtpList1.Items.Insert(protoRtpList1.Items.Count - 1, rtp);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Rtp> rtp = new List<Rtp>();
            foreach(object item in protoRtpList1.Items)
                if (item is Rtp) rtp.Add((Rtp)(item));
            Help.Path.RTPManager.RtpList = rtp;
            Help.Path.RTPManager.Save();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
