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
            protoShapeShifterData1.Nodes.Clear();
            protoShapeShifterData1.Load();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 拆分ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splitContainer1.Select();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (protoShapeShifterData1.Focused && protoShapeShifterData1.SelectedNode != null)
            {
                if (protoShapeShifterData1.SelectedNode.Parent == null)
                {
                    MessageBox.Show("抱歉。出于程序运行的安全性考虑，"
                        + Environment.NewLine+"我们暂时不允许您删除 Data 中的条目。"
                        +Environment.NewLine+"如果您确实想要这么做，您可以通过执行代码来达成这一点。");
                    return;
                }
                if (MessageBox.Show("您正在试图删除一个大项。您确定要这么做么？", "删除确认", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    var parentNode = protoShapeShifterData1.SelectedNode.Parent;
                    var parent = parentNode.Tag as FuzzyData.FuzzyObject;
                    var target = protoShapeShifterData1.SelectedNode.Name;
                    Remove(parent, target);
                    protoShapeShifterData1.RecycleSelectedNode(parentNode);
                }
            }
            else if (this.protoShapeShifterValue1.Focused)
            {
                if (protoShapeShifterValue1.SelectedItems.Count == 0) return;
                int index = protoShapeShifterValue1.SelectedIndices[0];
                var parent = protoShapeShifterData1.SelectedNode.Tag as FuzzyData.FuzzyObject;
                var target = protoShapeShifterValue1.SelectedItems[0].Text;
                Remove(parent, target);
                protoShapeShifterValue1.RealizeObject(protoShapeShifterValue1.Value);
                if (index >= protoShapeShifterValue1.Items.Count - 1)
                    index = protoShapeShifterValue1.Items.Count - 2;
                if (index < 0) index = 0;
                protoShapeShifterValue1.SelectedIndices.Clear();
                protoShapeShifterValue1.SelectedIndices.Add(index);
                protoShapeShifterValue1.Focus();
            }
        }
        void Remove(FuzzyData.FuzzyObject Parent, FuzzyData.FuzzyObject Target)
        {
            if (Parent is FuzzyData.FuzzyArray)
                (Parent as FuzzyData.FuzzyArray).Remove(Target);
            if (Parent is FuzzyData.FuzzyHash)
            {
                var hash = Parent as FuzzyData.FuzzyHash;
                if (MessageBox.Show("您正在试图删除一个哈希表值。"
                    + Environment.NewLine + "这将导致一个关联项被删除。"
                    + Environment.NewLine + "您确定要那么做么？", "删除确认", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                    return;
                if (hash.ContainsKey(Target)) hash.Remove(Target);
                else if (hash.ContainsValue(Target))
                    foreach (var key in hash.Keys)
                        if (hash[key] == Target)
                        {
                            hash.Remove(key);
                            break;
                        }
            }
            if (Parent.InstanceVariables.ContainsValue(Target))
                foreach (var key in Parent.InstanceVariables.Keys)
                    if (Parent.InstanceVariables[key] == Target)
                    {
                        Parent.InstanceVariables.Remove(key);
                        break;
                    }
        }
        void Remove(FuzzyData.FuzzyObject Parent, string Target)
        {
            if (Target.StartsWith("[") && Parent is FuzzyData.FuzzyArray)
            {
                int index = 0;
                if (int.TryParse(Target.Substring(1, Target.Length - 2), out index))
                    (Parent as FuzzyData.FuzzyArray).RemoveAt(index);
            }
            if (Target.StartsWith("."))
            {
                FuzzyData.FuzzySymbol sym = FuzzyData.FuzzySymbol.GetSymbol(Target.Substring(1));
                if (Parent.InstanceVariables.ContainsKey(sym))
                    Parent.InstanceVariables.Remove(sym);
            }
            if (Target.StartsWith("@"))
            {
                FuzzyData.FuzzySymbol sym = FuzzyData.FuzzySymbol.GetSymbol(Target);
                if (Parent.InstanceVariables.ContainsKey(sym))
                    Parent.InstanceVariables.Remove(sym);
            }
            Target = Target.ToUpper();
            int cut = -1;
            if (Target.StartsWith(".KEYS")) cut = 5;
            if (Target.StartsWith(".VALUES")) cut = 7;
            if (Target.StartsWith("KEY")) cut = 3;
            if (Target.StartsWith("VALUE")) cut = 5;
            if (cut > 0 && Parent is FuzzyData.FuzzyHash)
            {
                var hash = Parent as FuzzyData.FuzzyHash;
                if (MessageBox.Show("您正在试图删除一个哈希表值。"
                    + Environment.NewLine + "这将导致一个关联项被删除。"
                    + Environment.NewLine + "您确定要那么做么？", "删除确认", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                    return;
                int index = 0;
                object key = null;
                if (int.TryParse(Target.Substring(cut + 1, Target.Length - cut - 2), out index))
                {
                    foreach (object Key in hash.Keys)
                    {
                        key = Key;
                        if (index-- == 0) break;
                    }
                    hash.Remove(key);
                }
            } 
        }
        private void 复制名称ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (protoShapeShifterData1.Focused)
                Clipboard.SetText(protoShapeShifterData1.SelectedNode.Text);
            else if (protoShapeShifterData1.Focused)
                Clipboard.SetText(protoShapeShifterValue1.SelectedItems[0].Text);
        }

        private void 执行代码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new Prototype.ProtoLinedPaperDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                object ans = Help.Bash.Call(dialog.Value);
                if (ans != null && ans is FuzzyData.FuzzyObject)
                    Help.ShapeShifter.ShowObject(ans as FuzzyData.FuzzyObject, "执行结果");
            }
        }

        TargetSelect ts = null;
        private void 改为链接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ts == null) ts = new TargetSelect();
            if (ts.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                if (protoShapeShifterData1.Focused)
                {
                }
                else if (protoShapeShifterValue1.Focused)
                { }
        }

        Search s = null;
        protected IEnumerator<FuzzyData.FuzzyObject> LastSearch = null;
        private void 查找ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (s == null) s = new Search();
            if (s.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LastSearch = Help.Finder.Search(s.Option);
                查找下一项ToolStripMenuItem_Click(sender, e);
            }
        }

        private void 查找下一项ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LastSearch == null) return;
            if (LastSearch.MoveNext())
            {
                FuzzyData.FuzzyObject[] ar = Help.Finder.Path.ToArray();
                List<FuzzyData.FuzzyObject> path = new List<FuzzyData.FuzzyObject>();
                foreach (var element in ar) path.Insert(0, element);
                if (!(protoShapeShifterData1.SearchViaPath(path)))
                    protoShapeShifterValue1.SearchValue(path[path.Count - 1]);
            }
            else
                if (MessageBox.Show("已搜索完毕.您要从头开始继续搜索么？", "搜索", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    查找ToolStripMenuItem_Click(sender, e);
        }

        private void 插入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            protoShapeShifterValue1.ProtoShapeShifterValue_DoubleClick(this, e);
        }

        private void 编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            查找下一项ToolStripMenuItem.Enabled = !(LastSearch == null);
        }

        Help.Clipboard clip = Help.Clipboard.GetClip();
        FuzzyData.FuzzyObject buffer;

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FuzzyData.FuzzyObject obj = null;
            if (contextMenuStrip1.SourceControl == protoShapeShifterData1) obj = protoShapeShifterData1.Value;
            else if (contextMenuStrip1.SourceControl == protoShapeShifterValue1) obj = protoShapeShifterValue1.SelectedValue;
            if (obj == null)
            {
                MessageBox.Show("没有可用项被选中。", "复制失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            clip.Set(obj as FuzzyData.FuzzyObject);
            buffer = obj;
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FuzzyData.FuzzyObject value = clip.Get();
            if (value == null) return;
            FuzzyData.FuzzyObject parent = protoShapeShifterData1.Value;
            if (parent == null) return;
            if (parent is FuzzyData.FuzzyArray)
                (parent as FuzzyData.FuzzyArray).Add(value);
            else if (parent is FuzzyData.FuzzyHash)
                (parent as FuzzyData.FuzzyHash).Add(FuzzyData.FuzzyNil.Instance, value);
            else if (parent is FuzzyData.FuzzyObject)
            {
                int i = 1;
                FuzzyData.FuzzySymbol sym;
                while (parent.InstanceVariables.ContainsKey(sym = FuzzyData.FuzzySymbol.GetSymbol("PasteValue" + i.ToString()))) i++;
                parent.InstanceVariables.Add(sym, value);
            }
            if (value is FuzzyData.FuzzyArray || value is FuzzyData.FuzzyHash || value.InstanceVariables.Count > 0)
                protoShapeShifterData1.RecycleSelectedNode();
            protoShapeShifterValue1.Value = this.protoShapeShifterData1.Value;
        }

        private void 删除ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            删除ToolStripMenuItem_Click(this, e);
        }
    }
}
