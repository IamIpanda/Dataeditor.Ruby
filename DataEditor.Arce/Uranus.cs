using System;
using System.Drawing;
using System.Windows.Forms;

namespace DataEditor.Arce
{
    public partial class Uranus : Form
    {
        public Uranus()
        {
            InitializeComponent();
            Help.Bash.StatusLabel = this.toolStripStatusLabel1;
            Help.Bash.ToolTip = this.toolTip1;
            Help.Action.Instance.Act += Instance_Act;
        }

        void Instance_Act(object sender, Help.Action.ActionEventArgs e)
        {
            撤销ToolStripMenuItem.Enabled = Help.Action.Instance.CanUndo;
            恢复ToolStripMenuItem.Enabled = Help.Action.Instance.CanRedo;
        }

        private void WindowEditor_Load(object sender, EventArgs e)
        {
        }

        private void EditorWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(e.Alt)) return;
            msMain.Visible = !(msMain.Visible);
            if (!msMain.Visible) return;
            msMain.Focus();
            msMain.Items[0].Select();
        }

        private void msMain_Leave(object sender, EventArgs e)
        {
            msMain.Visible = false;
        }

        private void Uranus_FormClosed(object sender, FormClosedEventArgs e)
        {
            Help.Log.Flush();
            (Help.Window.Instance["lead"] as WrapLead).Window.Close();
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.Save();
        }

        private void 执行脚本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.Run();
        }
        private void 呼出通用编辑器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.CallEditor();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 撤销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.Undo();
        }

        private void 恢复ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.Redo();
        }

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.SaveAs();
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }
    }

    public class WrapUranus : DataEditor.Control.WrapBaseWindow<Uranus>
    {
        public override void SetSize(Size size)
        {
            base.SetSize(new Size(size.Width + 15, size.Height + 40));
        }

        public override System.Windows.Forms.Control.ControlCollection Controls
        {
            get
            {
                return Window.pnMain.Controls;
            }
        }
    }
}