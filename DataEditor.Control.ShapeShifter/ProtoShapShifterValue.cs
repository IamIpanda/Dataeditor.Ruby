using System;
using System.Collections.Generic;
using System.Text;
using DataEditor.FuzzyData;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public class ProtoShapeShifterValue : Prototype.ProtoListView
    {
        protected FuzzyData.FuzzyObject _value;
        protected List<string> keys = new List<string>();
        protected List<FuzzyData.FuzzyObject> unders = new List<FuzzyObject>();
        public bool ReadOnly { get; set; }
        public FuzzyData.FuzzyObject Value
        {
            get { return _value; }
            set { _value = value; OnValueChanged(); }
        }
        public event EventHandler SelectedValueChanged;
        public void OnValueChanged()
        {
            this.Items.Clear();
            RealizeObject(_value);
            if (ValueChanged != null)
                ValueChanged(this, new EventArgs());                                                                                                                 
        }
        public event EventHandler ValueChanged;
        public ProtoShapeShifterValue()
        {
            this.Columns.Add("索引", 200);
            this.Columns.Add("类型", 120);
            this.Columns.Add("值", 600);
            this.DoubleClick += ProtoShapeShifterValue_DoubleClick;
        }

        public void ProtoShapeShifterValue_DoubleClick(object sender, EventArgs e)
        {
            if (this.SelectedIndices.Count == 0) return;
            ShapeShifter.ShapeShifterDialog dialog = new ShapeShifter.ShapeShifterDialog();
            var value = unders[this.SelectedIndices[0]];
            string old_key = "";
            if (value == null) 
            {
                dialog.Value = DataEditor.FuzzyData.FuzzyNil.Instance;
                // 烦成傻逼的 Hash 必须单独处理
                if (_value is FuzzyHash)
                {
                    var hash = _value as FuzzyHash;
                    var count = hash.Count;
                    dialog.KeyEnabled = false;
                    dialog.Key = String.Format("Key[{0}]", count);
                    if (dialog.ShowDialog() != DialogResult.OK) return;
                    var key = dialog.Value;
                    if (hash.ContainsKey(key))
                    {
                        MessageBox.Show("下述的键已存在：" + key.ToString());
                        return;
                    }
                    dialog.Key = String.Format("Value[{0}]", count);
                    dialog.Value = FuzzyNil.Instance;
                    if (dialog.ShowDialog() != DialogResult.OK) return;
                    hash.Add(key, dialog.Value);
                    RealizeObject(_value);
                    if (SelectedValueChanged != null) SelectedValueChanged(this, new EventArgs());
                    return;
                }
                else if (_value is FuzzyArray) dialog.Key = "[" + (_value as FuzzyArray).Count.ToString() + "]";
                else dialog.Key = "@new_variable_" + _value.InstanceVariables.Count;
            }
            else
            {
                dialog.Value = value;
                old_key = keys[this.SelectedIndices[0]];
                dialog.Key = old_key;
            }
            dialog.KeyEnabled = _value is FuzzyObject;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (value == null)
                    InsertValue(dialog);
                else
                {
                    if (_value is FuzzyArray) (_value as FuzzyArray)[this.SelectedIndices[0]] = dialog.Value;
                    else if (_value is FuzzyHash)
                    {
                        // TODO : Finish it.
                    }
                    else
                    {
                        _value.InstanceVariables.Remove(FuzzySymbol.GetSymbol(old_key));
                        InsertValue(dialog);
                    }
                }
                RealizeObject(_value);
                if (SelectedValueChanged != null) SelectedValueChanged(this, new EventArgs());
            }
        }
        protected void InsertValue(ShapeShifter.ShapeShifterDialog dialog)
        {
            if (_value is FuzzyArray) (_value as FuzzyArray).Add(dialog.Value);
            else if (_value.InstanceVariables.ContainsKey(FuzzySymbol.GetSymbol(dialog.Key)))
                MessageBox.Show("这一 Key 已经存在：" + dialog.Key, "无法插入值", MessageBoxButtons.OK);
            else _value.InstanceVariables.Add(FuzzySymbol.GetSymbol(dialog.Key), dialog.Value);
        }
        public void RealizeObject(FuzzyObject obj)
        {
            this.Items.Clear();
            if (obj == null) return;
            unders.Clear();
            keys.Clear();
            if (obj is FuzzyArray) { RealizeArray(obj as FuzzyArray); }
            else if (obj is FuzzyHash) { RealizeHash(obj as FuzzyHash); }
            else
            {
                foreach (var key in obj.InstanceVariables.Keys)
                    RealizeSmalls(obj[key] as FuzzyObject, key.Name);
            }
            if (!ReadOnly)
            {
                this.keys.Add("");
                this.Items.Add("");
                this.unders.Add(null);
            }
        }
        public void RealizeArray(FuzzyArray obj)
        {
            for (int i = 0; i < obj.Size; i++)
                RealizeSmalls(obj[i] as FuzzyObject, "[" + i.ToString() + "]");
        }
        public void RealizeHash(FuzzyHash obj)
        {
            int count = 0;
            foreach(var pair in obj)
            {
                RealizeSmalls(pair.Key as FuzzyObject, "key[" + count.ToString() + "]");
                RealizeSmalls(pair.Value as FuzzyObject, "value[" + count.ToString() + "]");
                count++;
            }
        }
        public void RealizeSmalls(FuzzyObject obj, string index)
        {
            if (obj == null) return;
            if (obj.GetType() == typeof(FuzzyObject) || obj.GetType() == typeof(FuzzyArray) || obj.GetType() == typeof(FuzzyHash)) return;
            string type = obj.GetType().Name;
            type = type.Substring(5);
            string value = obj.ToString();
            this.unders.Add(obj);
            this.keys.Add(index);
            this.Items.Add(new ListViewItem(new string[] { index, type, value }));
        }
        public bool SearchValue(FuzzyObject target)
        {
            for (int i = 0; i < this.Items.Count; i++)
                if (unders[i] == target)
                {
                    this.SelectedIndices.Clear();
                    this.SelectedIndices.Add(i);
                    this.Focus();
                    return true;
                }
            return false;
        }
        public FuzzyObject SelectedValue
        {
            get
            {
                if (SelectedIndices.Count == 0) return null;
                int index = SelectedIndices[0];
                if (index >= unders.Count) return null;
                return unders[index];
            }
        }
    }
}
