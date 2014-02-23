using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Wrapper
{
    public partial class AudioChoser : Form
    {
        public AudioChoser()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            protoRtpViewList1.Extands = new List<string>() { ".ogg", ".wav", ".mp3", ".mid" };
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

        public int Volume
        {
            get { return trackBar1.Value; }
            set { trackBar1.Value = value; }
        }
        public int Freq
        {
            get { return trackBar2.Value; }
            set { trackBar2.Value = value; }
        }
        public string FileName
        {
            get 
            {
                return System.IO.Path.GetFileNameWithoutExtension(protoRtpViewList1.ChosenFile.FullName);
            }
            set 
            {
                var list = protoRtpViewList1.Files;
                for (int i = 0; i < list.Count; i++)
                    if (list[i] != null && list[i].Name.StartsWith(value))
                        protoRtpViewList1.SelectedIndex = i;
            }
        }
        public string Path
        {
            set 
            {
                protoRtpViewList1.Items.Clear();
                protoRtpViewList1.AddFlag("（无）", Color.Gray);
                foreach (Help.Rtp rtp in Help.Path.RTPManager.RtpList)
                    SearchFile(rtp, value);
            }
        }
        protected void SearchFile(Help.Rtp rtp, string path)
        {
            string full = System.IO.Path.Combine(rtp.Path, path);
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(full);
            if (!dir.Exists) return;
            this.protoRtpViewList1.AddChildFile(dir, rtp.Color);
        }
    }
}
