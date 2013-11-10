using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control
{
    public abstract class WrapBaseEditor<TValue> : ObjectEditor where TValue : FuzzyData.FuzzyObject
    {
        protected TValue value;
        protected FuzzyData.FuzzySymbol key;
        protected Help.Parameter argument;
        abstract public void Push();
        abstract public void Pull();
        abstract public void Bind();
        abstract public bool CheckValue();
        abstract public string Flag { get; }
        public virtual DataContainer Container { get; set; }
        public virtual System.Windows.Forms.Label Label { get; set; }
        public virtual System.Windows.Forms.Control Binding { get; set; }
        protected virtual TValue ConvertToValue(FuzzyData.FuzzyObject origin) { return origin as TValue; }

        public WrapBaseEditor() { Bind(); SetDefaultArgument(); }
        public virtual void OnEnter(object sender, EventArgs e) { }
        public virtual void OnLeave(object sender, EventArgs e) { }
        public virtual Help.Parameter Argument
        {
            get { return argument; }
            set { argument = value; Reset(); }
        }
        public virtual void Reset()
        {
            if (argument == null || Binding == null) return;
            int width = argument.GetAegument<int>("WIDTH");
            int height = argument.GetAegument<int>("HEIGHT");
            key = argument.GetAegument<FuzzyData.FuzzySymbol>("ACTUAL");
            if (width > 0) Binding.Width = width;
            if (height > 0) Binding.Height = height;
            Binding.Leave += OnLeave;
            Binding.Enter += OnEnter;
        }
        public virtual FuzzyData.FuzzyObject Value
        {
            get { return value; }
            set
            {
                TValue ans = ConvertToValue(value);
                if (ans == null) return;
                this.value = ans;
                Pull();
            }
        }
        public virtual FuzzyData.FuzzyObject Parent
        {
            set
            {
                FuzzyData.FuzzyObject origin = GetValueFromChild(value);
                TValue ans = ConvertToValue(origin);
                if (ans == null) return;
                this.value = ans;
                Pull();
            }
        }
        protected FuzzyData.FuzzyObject GetValueFromChild(FuzzyData.FuzzyObject parent)
        {
            return GetValueFromChild(parent, key);
        }
        protected virtual FuzzyData.FuzzyObject GetValueFromChild(FuzzyData.FuzzyObject parent, FuzzyData.FuzzySymbol symbol)
        {
            if (parent == null || symbol == null) return null;
            if (symbol is FuzzyData.FuzzySymbolComplex)
            {
                var sym = symbol as FuzzyData.FuzzySymbolComplex;
                FuzzyData.FuzzyComplex complex = new FuzzyData.FuzzyComplex();
                foreach (string key in sym.Extra.Keys)
                    complex.consistence.Add(key, GetValueFromChild(parent, sym.Extra[key]));
                return complex;
            }
            else
            {
                object temp = null;
                parent.InstanceVariables.TryGetValue(symbol, out temp);
                return temp as FuzzyData.FuzzyObject;
            }
        }
        protected virtual void SetDefaultArgument()
        {
            argument = new Help.Parameter();
            argument.Defaults.Add("WIDTH", -1);
            argument.Defaults.Add("HEIGHT", -1);
        }
    }
}
