using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Wrapper
{
    public partial class Layers_Dialog : Form
    {
        public List<Layers_layer> layers = new List<Layers_layer>();
        public Layers_Dialog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        public int LayerCount
        {
            set
            {
                this.ClientSize = new System.Drawing.Size(60 * value + 380, 480);
                tableLayoutPanel3.ColumnCount = value + 1;
                tableLayoutPanel3.ColumnStyles.Clear();
                layers.Clear();
                Layers_layer layer;
                for (int i = 0; i < value; i++)
                {
                    layer = new Layers_layer();
                    layers.Add(layer);
                    layer.Dock = DockStyle.Fill;
                    tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60));
                    tableLayoutPanel3.Controls.Add(layer, i, 0);
                    layer.SelectedIndexChanged += layer_SelectedIndexChanged;
                }
                tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
                tableLayoutPanel3.SetColumn(protoImageDisplayer1, value);
            }
        }

        void layer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void Setup(Help.Rtp rtp, List<Layers.LayerData> layers)
        {
            this.LayerCount = layers.Count;
            Layers.LayerData data;
            Layers_layer inner_control;
            for (int i = 0; i < layers.Count; i++)
            {
                data = layers[i];
                inner_control = this.layers[i];
                inner_control.Text = data.name;
                inner_control.Path = System.IO.Path.Combine(rtp.Path, data.dir);
            }
        }
             
    }
}
