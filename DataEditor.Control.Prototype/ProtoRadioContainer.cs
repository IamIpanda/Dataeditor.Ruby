﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public partial class ProtoRadioContainer : UserControl
    {
        public ProtoRadioContainer()
        {
            InitializeComponent();
        }
        public new string Text
        {
            get { return radioButton1.Text; }
            set { radioButton1.Text = value; }
        }

        public RadioButton Radio { get { return radioButton1; } }

        private void radioButton1_SizeChanged(object sender, EventArgs e)
        {
            ResetRadio();
        }

        private void panel2_SizeChanged(object sender, EventArgs e)
        {
            ResetRadio();
        }

        protected void ResetRadio()
        {
            radioButton1.Location = new Point(0, 
                (panel2.Height - radioButton1.Height) / 2);
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Enabled = radioButton1.Checked;
        }

        public int RadioWidth
        {
            get { return (int)tableLayoutPanel1.ColumnStyles[0].Width; }
            set { tableLayoutPanel1.ColumnStyles[0].Width = value; }
        }
        public System.Windows.Forms.Control.ControlCollection PanelCollection
        {
            get { return panel1.Controls; }
        }
    }
}
