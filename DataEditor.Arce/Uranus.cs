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
            this.Shown += Uranus_Shown;
        }

        void Uranus_Shown(object sender, EventArgs e)
        {
            Conquer();
        }

        void Instance_Act(object sender, Help.Action.ActionEventArgs e)
        {
            撤销ToolStripMenuItem.Enabled = Help.Action.Instance.CanUndo;
            恢复ToolStripMenuItem.Enabled = Help.Action.Instance.CanRedo;
        }

        bool Lock = false;
        private void EditorWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (Lock) return;
            if (!(e.Alt)) return;
            msMain.Visible = !(msMain.Visible);
            if (!msMain.Visible) return;
            msMain.Focus();
            msMain.Items[0].Select();
            Lock = true;
        }
        private void EditorWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode & Keys.Menu) <= 0) return;
            Lock = false;
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

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Engine.Execute();
        }

        private void 一并更改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.Wave();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Help.Backup.Save();
        }
        public void Conquer()
        {
            timer1.Enabled = Help.Environment.Instance.EnableAutoSave;
            timer1.Interval = 60000 * Help.Environment.Instance.AutoSaveTimeSpan;
            toolTip1.Active = Help.Environment.Instance.EnableAutoHint;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Engine.OpenOption();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Engine.SaveAll();
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.About();
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
        public void Conquer() { Window.Conquer(); }
    }
}