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
        public FuzzyData.FuzzyObject Value
        {
            get { return _value; }
            set { _value = value; OnValueChanged(); }
        }
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
            this.Columns.Add("值", 200);
            this.DoubleClick += ProtoShapeShifterValue_DoubleClick;
        }

        void ProtoShapeShifterValue_DoubleClick(object sender, EventArgs e)
        {

        }
        public void RealizeObject(FuzzyObject obj)
        {
            if (obj == null) return;
            if (obj is FuzzyArray) { RealizeArray(obj as FuzzyArray); return; }
            else if (obj is FuzzyHash) { RealizeHash(obj as FuzzyHash); return; }
            foreach (var key in obj.InstanceVariables.Keys)
                RealizeSmalls(obj[key] as FuzzyObject, key.Name);
        }
        public void RealizeArray(FuzzyArray obj)
        {
            for (int i = 0; i < obj.Size; i++)
                RealizeSmalls(obj[i] as FuzzyObject, i.ToString());
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
            if (obj.InstanceVariables.Count > 0 || obj.GetType() == typeof(FuzzyArray) || obj.GetType() == typeof(FuzzyHash)) return;
            string type = obj.GetType().Name;
            type = type.Substring(5);
            string value = obj.ToString();
            this.Items.Add(new ListViewItem(new string[] { index, type, value }));
        }
    }
}
