using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.ShapeShifter
{
    public partial class ShapeShifter : Form
    {
        public ShapeShifter()
        {
            InitializeComponent();
        }

        private void protoShapeShifterData1_ValueChanged(object sender, EventArgs e)
        {
            protoShapeShifterValue1.Value = this.protoShapeShifterData1.Value;
            this.toolStripStatusLabel1.Text = protoShapeShifterData1.SelectedPath;
        }

        private void ShapeShifter_Load(object sender, EventArgs e)
        {
            protoShapeShifterData1.Load();
        }

        private void protoShapeShifterValue1_SelectedValueChanged(object sender, EventArgs e)
        {
            protoShapeShifterData1.RecycleSelectedNode();
        }

        private void 状态栏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            状态栏ToolStripMenuItem.Checked = !(状态栏ToolStripMenuItem.Checked);
            statusStrip1.Visible = 状态栏ToolStripMenuItem.Checked;
        }

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            protoShapeShifterData1.Refresh();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
