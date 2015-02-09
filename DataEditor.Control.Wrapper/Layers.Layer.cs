using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DataEditor.Control.Wrapper
{
    public partial class Layers_layer : UserControl
    {
        public List<string> files;
        public Layers_layer()
        {
            InitializeComponent();
        }

        private void protoListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null)
                SelectedIndexChanged(this, e);
        }
        public event EventHandler SelectedIndexChanged;
        public override string Text
        {
            get { return protoTitleBox1.Text; }
            set { protoTitleBox1.Text = value; }
        }
        public string Path
        {
            set
            {
                DirectoryInfo dir = new DirectoryInfo(value);
                if (!(dir.Exists)) return;
                this.protoRtpViewList1.Items.Clear();
                this.protoRtpViewList1.AddFile(null, Help.Painter.Instance[15]);
                this.protoRtpViewList1.AddChildDirectory(dir, Help.Painter.Instance[16]);
            }
        }
        public FileInfo SelectedFile
        {
            get
            {
                return this.protoRtpViewList1.ChosenFile;
            }
        }
    }
}
