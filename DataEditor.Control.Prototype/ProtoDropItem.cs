using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public partial class ProtoDropItem : UserControl
    {
        public event EventHandler ButtonClicked;
        
        [Browsable(true)]
        [EditorBrowsable]
        public override string Text
        {
            get { return protoSelectable1.Text; }
            set { protoSelectable1.Text = value; Invalidate(); }
        }
        public ProtoDropItem ()
        {
            InitializeComponent();
        }

        private void button1_Click (object sender, EventArgs e)
        {
            if ( ButtonClicked != null ) ButtonClicked(this, e);
        }

        private void ProtoDropItem_Resize (object sender, EventArgs e)
        {
            button1.Width = button1.Height;
        }

        private void protoSelectable1_DoubleClick (object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }
    }
}
