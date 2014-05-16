using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control
{
    public abstract class WrapBaseList<TValue> : DataEditor.Control.WrapBaseEditor<TValue>, Contract.TaintableList where TValue : FuzzyData.FuzzyObject
    {

        protected Contract.TaintCollection Collection = null;
        public virtual void PuttList(Contract.TaintCollection Collection = null)
        {
            if (TargetDictionary == null) return;
            if (Collection != null) this.Collection = Collection;
            if (this.Collection == null) return;
            for (int i = 0; i < GetCount(); i++)
                PuttSingle(i, this.Collection[i]);
        }
        public override FuzzyData.FuzzyObject Parent
        {
            get { return base.Parent; }
            set
            {
                base.Parent = value;
                Collection = Help.Taint.RequireBook(this);
                PuttList(Collection);
            }
        }
        protected Dictionary<int, System.Drawing.Color> TargetDictionary = null;
        protected abstract void SetDictionary();
        protected abstract int GetCount();
        protected virtual Contract.TaintState GetState(int index) { return Contract.TaintState.Tainted; }
        protected virtual void PuttSingle(int index, Contract.TaintState state)
        {
            var color = Help.Taint.DefaultColor(state);
            if (state == Contract.TaintState.UnTainted)
                TargetDictionary.Remove(index);
            else if (TargetDictionary.ContainsKey(index))
                TargetDictionary[index] = color;
            else TargetDictionary.Add(index, color);
        }
        protected virtual void TaintSingle(int index)
        {
            var state = GetState(index);
            Collection[index] = state;
            PuttSingle(index, state);
        }
    }
    public abstract class WrapControlList<TValue, TControl> : WrapBaseList<TValue>
        where TValue: FuzzyData.FuzzyObject
        where TControl : System.Windows.Forms.Control, new()
    {
        protected TControl Control = new TControl();
        public override void Bind() { Binding = Control; SetDictionary(); }
    }
}
