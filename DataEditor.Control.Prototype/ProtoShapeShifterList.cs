using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public class ProtoShapeShifterList : System.Windows.Forms.TreeView
    {
        public ProtoShapeShifterList()
        {
            this.AfterSelect += ProtoShapeShifterList_AfterSelect;
        }

        void ProtoShapeShifterList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            OnValueChanged();
        }
        protected FuzzyData.FuzzyObject _container = FuzzyData.FuzzyNil.Instance;
        public virtual new FuzzyData.FuzzyObject Container
        {
            get { return _container; }
            set { _container = value; OnContainerChanged(); }
        }
        public virtual FuzzyData.FuzzyObject Value
        {
            get
            {
                var node = this.SelectedNode;
                return node.Tag as FuzzyData.FuzzyObject;
            }
        }
        protected virtual void OnContainerChanged()
        {
            var node = RealizeObject(_container);
            this.Nodes.Add(node);
            if (ContainerChanged != null) ContainerChanged(this, new EventArgs());
        }
        protected virtual void OnValueChanged()
        {
            if (ValueChanged != null) ValueChanged(this, new EventArgs());
        }
        public event EventHandler ValueChanged;
        public event EventHandler ContainerChanged;
        protected Stack<object> loading = new Stack<object>();
        protected virtual TreeNode RealizeObject(FuzzyData.FuzzyObject obj, string prefix = "")
        {
            if (obj == null) return null;
            else if (loading.Contains(obj)) return RealizeCircle(obj, prefix);
            else if (obj is FuzzyData.FuzzyArray) return RealizeArray(obj as FuzzyData.FuzzyArray, prefix);
            else if (obj is FuzzyData.FuzzyHash) return RealizeHash(obj as FuzzyData.FuzzyHash, prefix);
            else if (obj.InstanceVariables.Count == 0) return null;
            var ans = new TreeNode();
            ans.Text = prefix + (prefix == "" ? "" : ":") + "[" + obj.ClassName.Name + "]";
            loading.Push(obj);
            foreach(var key in obj.InstanceVariables.Keys)
            {
                var node = RealizeObject(obj[key] as FuzzyData.FuzzyObject, key.Name);
                if (node != null) ans.Nodes.Add(node);
            }
            ans.Tag = obj;
            loading.Pop();
            return ans;
        }
        protected virtual TreeNode RealizeArray(FuzzyData.FuzzyArray obj, string prefix = "")
        {
            loading.Push(obj);
            var ans = new TreeNode();
            ans.Text = prefix + (prefix == "" ? "" : ":") + "[Array:" + obj.Count + "]";
            for(int i = 0; i < obj.Count; i++)
            {
                var node = RealizeObject(obj[i] as FuzzyData.FuzzyObject, "[" + i.ToString() + "]");
                if (node != null) ans.Nodes.Add(node);
            }
            ans.Tag = obj;
            loading.Pop();
            return ans;
        }
        protected virtual TreeNode RealizeHash(FuzzyData.FuzzyHash obj, string prefix = "")
        {
            loading.Push(obj);
            var ans = new TreeNode();
            ans.Text = prefix + (prefix == "" ? "" : ":") + "[Hash:" + obj.Count + "]";
            int count = 0;
            foreach(var key in obj.Keys)
            {
                var nodeKey = RealizeObject(key as FuzzyData.FuzzyObject, "Key["+count.ToString() +"]");
                var nodeValue = RealizeObject(obj[key] as FuzzyData.FuzzyObject, "Value[" + count.ToString() + "]");
                if (nodeKey != null) ans.Nodes.Add(nodeKey);
                if (nodeValue != null) ans.Nodes.Add(nodeValue);
            }
            ans.Tag = obj;
            loading.Pop();
            return ans;
        }
        protected virtual TreeNode RealizeCircle(FuzzyData.FuzzyObject obj, string prefix = "")
        {
            var ans = new TreeNode();
            ans.Text = prefix + (prefix == "" ? "" : ":") + "[circle]";
            return ans;
        }
    }
    public class ProtoShapeShifterData : ProtoShapeShifterList
    {
        public void Load()
        {
            foreach (var name in Help.Data.Instance.Names)
            {
                var node = RealizeObject(Help.Data.Instance[name], name);
                this.Nodes.Add(node);
            }
            foreach(var key in Help.Data.Instance.Map.Keys)
            {
                var node = RealizeObject(Help.Data.Instance[key], "map[" + key.ToString() + "]");
                this.Nodes.Add(node);
            }
        }
        protected override void OnContainerChanged() { }
    }
}
