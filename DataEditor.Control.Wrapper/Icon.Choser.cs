using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Wrapper
{
    public partial class Icon_Choser : Form
    {
        public Icon_Choser()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            protoImageIndexDisplayer1.Index = Convert.ToInt32(numericUpDown1.Value);
        }

        private void protoImageIndexDisplayer1_SelectedIndexChanged(object sender, EventArgs e)
        {
            numericUpDown1.Value = protoImageIndexDisplayer1.Index;
        }
        public int Value
        {
            get { return Convert.ToInt32(numericUpDown1.Value); }
            set { numericUpDown1.Value = value; }
        }
        public void set(System.Drawing.Bitmap full, Help.Parameter.Split split)
        {
            protoImageIndexDisplayer1.Bitmap = full;
            var rect = split[0, full.Width, full.Height];
            protoImageIndexDisplayer1.ClipSize = rect.Size;
            protoImageIndexDisplayer1.Size = full.Size;
            numericUpDown1.Maximum = protoImageIndexDisplayer1.Blocks;
        }

        private void Icon_Choser_Shown(object sender, EventArgs e)
        {
            protoImageIndexDisplayer1.Focus();
        }
    }
}
