using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public partial class ProtoArticleInput : Form
    {
        public ProtoArticleInput()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            protoLinedTextbox1.TextChanged += protoLinedTextbox1_TextChanged;
        }

        void protoLinedTextbox1_TextChanged(object sender, EventArgs e)
        {
            var size = protoLinedTextbox1.PreferredSize;
            int width = size.Width;
            if (width < MinWidth) width = MinWidth;
            if (width > MaxWidth) width = MaxWidth;
            int height = size.Height;
            if (height < MinHeight) height = MinHeight;
            if (height > MaxHeight) height = MaxHeight;
            this.SetClientSizeCore(width, height);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        public override string Text
        {
            get { return protoLinedTextbox1.Text; }
            set { protoLinedTextbox1.Text = value; }
        }
        public const int MinHeight = 250;
        public const int MaxHeight = 600;
        public const int MinWidth = 400;
        public const int MaxWidth = 800;

    }
}
