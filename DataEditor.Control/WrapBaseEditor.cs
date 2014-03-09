using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control
{
    public abstract class WrapBaseEditor<TValue> : ObjectEditor where TValue : FuzzyData.FuzzyObject
    {
        protected TValue value;
        protected FuzzyData.FuzzyObject parent;
        protected FuzzyData.FuzzySymbol key;
        protected Help.Parameter argument;
        abstract public void Push();
        abstract public void Pull();
        abstract public void Bind();
        public bool EnableData { get; set; }
        abstract public bool ValueIsChanged();
        public virtual string Flag { get { return this.GetType().Name; } }
        public virtual DataContainer Container { get; set; }
        public virtual System.Windows.Forms.Label Label { get; set; }
        public virtual System.Windows.Forms.Control Binding { get; set; }
        protected virtual TValue ConvertToValue(FuzzyData.FuzzyObject origin) { return origin as TValue; }

        public WrapBaseEditor() { Bind(); SetDefaultArgument(); EnableData = true; }
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
            int width = argument.GetArgument<int>("WIDTH");
            int height = argument.GetArgument<int>("HEIGHT");
            key = argument.GetArgument<FuzzyData.FuzzySymbol>("ACTUAL");
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
                if (ans != null)
                {
                    Binding.Enabled = true;
                    this.value = ans;
                    Pull();
                }
                else if (Binding != null && EnableData) Binding.Enabled = false;
            }
        }
        public virtual FuzzyData.FuzzyObject Parent
        {
            get { return parent; }
            set
            {
                parent = value;
                FuzzyData.FuzzyObject origin = GetValueFromChild(value);
                TValue ans = ConvertToValue(origin);
                if (ans == null) return;
                this.value = ans;
                if (Binding.Enabled) Pull();
            }
        }
        protected FuzzyData.FuzzyObject GetValueFromChild(FuzzyData.FuzzyObject parent)
        {
            return GetValueFromChild(parent, key);
        }
        protected virtual FuzzyData.FuzzyObject GetValueFromChild(FuzzyData.FuzzyObject parent, FuzzyData.FuzzySymbol symbol)
        {
            if (symbol == null || symbol.Name == "") return parent;
            if (parent == null) return null;
            if (symbol is FuzzyData.FuzzySymbolComplex)
            {
                var sym = symbol as FuzzyData.FuzzySymbolComplex;
                FuzzyData.FuzzyComplex complex = new FuzzyData.FuzzyComplex();
                foreach (string key in sym.Extra.Keys)
                    complex.consistence.Add(key, GetValueFromChild(parent, sym.Extra[key]));
                return complex;
            }
            else if (parent is FuzzyData.FuzzyArray && symbol.Name.StartsWith("@INDEX"))
            {
                var array = parent as FuzzyData.FuzzyArray;
                var index = symbol.Name.Substring(6);
                int i = -1;
                if (int.TryParse(index, out i))
                    if (i < array.Count && i >= 0) return array[i] as FuzzyData.FuzzyObject;
                    else Help.Log.log("在" + Flag + "中数组超界：" + i.ToString());
            }
            object temp = null;
            parent.InstanceVariables.TryGetValue(symbol, out temp);
            if (temp == null) Help.Log.log("在 " + Flag + " 中未找到所宣告的下述值：" + symbol.Name);
            return temp as FuzzyData.FuzzyObject;
        }
        protected virtual void SetDefaultArgument()
        {
            argument = new Help.Parameter();
            argument.SetArgument("width", -1, Help.Parameter.ArgumentType.Option);
            argument.SetArgument("height", -1, Help.Parameter.ArgumentType.Option);
            argument.SetArgument("label", 1, Help.Parameter.ArgumentType.Option);
            argument.SetArgument("text", "Untitled", Help.Parameter.ArgumentType.Option);
            argument.SetArgument("actual", null, Help.Parameter.ArgumentType.Must);
        }
    }
    public abstract class WrapControlEditor<TValue, TControl> : WrapBaseEditor<TValue>
        where TValue : FuzzyData.FuzzyObject
        where TControl : System.Windows.Forms.Control, new()
    {
        protected TControl Control = new TControl();
        public override void Bind() { Binding = Control; }
    }
}
