using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Window
{
    public partial class PaletteEditor : Form
    {
        public PaletteEditor()
        {
            InitializeComponent();
            foreach (string key in Help.Painter.Instance.Palette.Keys)
            {
                protoListBox1.Items.Add(key);
                if (Help.Painter.Instance.Palette[key] == Help.Painter.Instance.Now)
                    protoListBox1.ForeColors.Add(protoListBox1.Items.Count - 1, Color.Green);
            }
            if (protoListBox1.Items.Count <= 1) button3.Enabled = false;
            if (protoListBox1.Items.Count > 0) protoListBox1.SelectedIndex = 0;
        }

        private void protoListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            protoPainterList1.Palette = Help.Painter.Instance.Palette[protoListBox1.SelectedItem.ToString()];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Help.Painter.Save();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void protoPainterList1_DoubleClick(object sender, EventArgs e)
        {
            var palette = protoPainterList1.Palette;
            if (palette == null) return;
            Color? base_color = palette[protoPainterList1.SelectedIndex + 1];
            Color color = base_color ?? default(Color);
            cD.Color = color;
            if (cD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                palette[protoPainterList1.SelectedIndex + 1] = cD.Color;
        }

        private void protoPainterList1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Delete) return;
            var palette = protoPainterList1.Palette;
            if (palette == null) return;
            palette[protoPainterList1.SelectedIndex + 1] = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var list = Help.Painter.Instance.Palette;
            list.Add(textBox1.Text, new Help.Palette());
            protoListBox1.Items.Add(textBox1.Text);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var list = Help.Painter.Instance.Palette;
            if (textBox1.Text == "" || list.ContainsKey(textBox1.Text))
                button4.Enabled = false;
            else button4.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var list = Help.Painter.Instance.Palette;
            list.Remove(protoListBox1.SelectedItem.ToString());
            if (protoListBox1.Items.Count <= 1) button3.Enabled = false;
        }

        private void protoListBox1_DoubleClick(object sender, EventArgs e)
        {
            protoListBox1.ForeColors.Clear();
            protoListBox1.ForeColors.Add(protoListBox1.SelectedIndex, Color.Green);
            protoListBox1.Invalidate();
        }
    }
}
