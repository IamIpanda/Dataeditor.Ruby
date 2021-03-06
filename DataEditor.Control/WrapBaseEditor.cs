﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control
{
    public enum DataState { Enable, Default, Disable }
    public abstract class WrapBaseEditor<TValue> : ObjectEditor,Contract.TaintableEditor where TValue : FuzzyData.FuzzyObject, new()
    {
        protected TValue value, _default;
        protected FuzzyData.FuzzyObject parent;
        protected FuzzyData.FuzzySymbol key;
        protected Help.Parameter argument;
        abstract public void Push();
        abstract public void Pull();
        abstract public void Bind();
        public DataState EnableData { get; set; }
        public virtual string HelpDocument { get; set; }
        abstract public bool ValueIsChanged();
        public virtual void Putt() { Help.Taint.DefaultPutt(this); }
        public virtual string Flag { get { return this.GetType().Name; } }
        public virtual bool HighLight { get { return false; } }
        public virtual DataContainer Container { get; set; }
        public virtual System.Windows.Forms.Label Label { get; set; }
        public virtual System.Windows.Forms.Control Binding { get; set; }
        protected virtual TValue ConvertToValue(FuzzyData.FuzzyObject origin) { return origin as TValue; }

        public WrapBaseEditor() 
        {
            Bind(); 
            SetDefaultArgument(); 
            EnableData = DataState.Disable; 
            SetEnabled(); 
        }
        public virtual void OnEnter(object sender, EventArgs e) { }
        public virtual void OnLeave(object sender, EventArgs e) { }
        public virtual Help.Parameter Argument
        {
            get { return argument; }
            set
            {
                argument = value;
                argument.paradix = "[" + Flag + "] ";
                Reset();
            }
        }
        public virtual void Reset()
        {
            if (argument == null || Binding == null) return;
            int width = argument.GetArgument<int>("WIDTH");
            int height = argument.GetArgument<int>("HEIGHT");
            key = argument.GetArgument<FuzzyData.FuzzySymbol>("ACTUAL");
            if (width > 0) Binding.Width = width;
            if (height > 0) Binding.Height = height;
            if (width <= 0 && height <= 0)
            {
                System.Drawing.Size size;
                if (Help.Measurement.Instance.Get(this.Flag, out size))
                    Binding.Size = size;
            }
            Binding.Leave += OnLeave;
            Binding.Enter += OnEnter;
            if (this.value != null) Help.Link.Instance[this.Value] = this;
        }
        public virtual FuzzyData.FuzzyObject Value
        {
            get { return value; }
            set
            {
                TValue ans = ConvertToValue(value);
                if (ans != null)
                {
                    EnableData = DataState.Enable;
                    SetEnabled();
                    this.value = ans;
                    Pull();
                }
                else 
                {
                    this.value = null;
                    EnableData = DataState.Disable;
                    SetEnabled();
                }
            }
        }
        public virtual FuzzyData.FuzzyObject Parent
        {
            get { return parent; }
            set
            {
                parent = value;
                if (parent == null) { EnableData = DataState.Disable; return; }
                FuzzyData.FuzzyObject origin = GetValueFromChild(value);
                TValue ans = ConvertToValue(origin);
                if (ans == null)
                    //this.value = parent;
                    EnableData = SetDefault() ? DataState.Disable : DataState.Default;
                else
                {
                    EnableData = DataState.Enable;
                    this.value = ans;
                }
                SetEnabled();
                if (Binding.Enabled) Pull();
                Putt();
            }
        }
        protected FuzzyData.FuzzyObject GetValueFromChild(FuzzyData.FuzzyObject parent)
        {
            return GetValueFromChild(parent, key);
        }
        protected virtual FuzzyData.FuzzyObject GetValueFromChild(FuzzyData.FuzzyObject parent, FuzzyData.FuzzySymbol symbol)
        {
            if (symbol == null || symbol.Name == "" || symbol.Name == "@ORIGIN") return parent;
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
        protected virtual bool SetDefault()
        {
            if (key == null || EnableData != DataState.Default) return false;
            var _default = argument.GetArgument<FuzzyData.FuzzyObject>("default_value");
            if (_default == null) return false;
            _default = _default.Clone() as FuzzyData.FuzzyObject;
            if (key is FuzzyData.FuzzySymbolComplex)
            {
                var complex = key as FuzzyData.FuzzySymbolComplex;
                var value = _default as FuzzyData.FuzzyHash;
                if (value == null) return false;
                foreach (var component in complex.Extra.Keys)
                    if (value.ContainsKey(component))
                        parent[complex.Extra[component]] = value[component];
                    else return false;
            }
            else
            {
                var value = ConvertToValue(_default);
                if (value == null) return false;
                var name = key.Name.ToUpper();
                if (name.StartsWith("@INDEX") && parent is FuzzyData.FuzzyArray)
                {
                    int i = -1;
                    var array = parent as FuzzyData.FuzzyArray;
                    var index = name.Substring(6);
                    if (int.TryParse(index, out i))
                    {
                        while (array.Size < i - 1) array.Add(new FuzzyData.FuzzyObject());
                        array[i] = value;
                        return true;
                    }
                }
                parent.InstanceVariables.Add(key, value);
            }
            return true;
        }
        protected virtual void SetDefaultArgument()
        {
            argument = new Help.Parameter();
            argument.SetArgument("width", -1, Help.Parameter.ArgumentType.Option);
            argument.SetArgument("height", -1, Help.Parameter.ArgumentType.Option);
            argument.SetArgument("label", 1, Help.Parameter.ArgumentType.Option);
            argument.SetArgument("text", "Untitled", Help.Parameter.ArgumentType.Option);
            argument.SetArgument("actual", null, Help.Parameter.ArgumentType.Must);
            argument.SetArgument("default_value", _default ?? new TValue(), Help.Parameter.ArgumentType.Option);
        }
        protected virtual void SetEnabled()
        {
            Binding.Enabled = EnableData != DataState.Disable;
            if (Label != null)
                if (EnableData == DataState.Enable)
                    Label.ForeColor = System.Drawing.Color.Gray;
                else Putt();
        }
    }


    public abstract class WrapControlEditor<TValue, TControl> : WrapBaseEditor<TValue>
        where TValue : FuzzyData.FuzzyObject, new()
        where TControl : System.Windows.Forms.Control, new()
    {
        protected TControl Control = new TControl();
        public override void Bind() 
        { 
            Binding = Control;
            Control.MouseEnter += Control_MouseEnter;
            Control.MouseLeave += Control_MouseLeave;
            ValueHasChanged = false;
        }

        //string LastStatusBarText = null;
        protected virtual void Control_MouseLeave(object sender, EventArgs e)
        {

        }

        protected virtual void Control_MouseEnter(object sender, EventArgs e)
        {
            if (Help.Bash.ToolTip == null) return;
            var label = this.Label == null ? "" : this.Label.Text;
            Help.Bash.ToolTip.ToolTipTitle = label;
            if (!ValueHasChanged) return;
            ValueHasChanged = false;
            var builder = new StringBuilder();
            builder.AppendFormat("Editor = [{0}]", Flag);
            builder.AppendLine();
            if (this.value != null)
            {
                var type = this.Value.GetType().Name.Substring(5);
                var that = this.value.ToString();
                if (that.Length > 500) that = that.Substring(0, 500) + "...";
                var key = this.key == null ? " - " : this.key.Name;
                builder.AppendFormat("[{0}] {1}", type, key);
                builder.AppendLine();
                builder.AppendFormat("Value = {0}", that);
                builder.AppendLine();
            }
            else
                builder.AppendLine("No Value");
            builder.AppendLine(HelpDocument ?? "该控件未提供说明文档。");
            Help.Bash.ToolTip.SetToolTip(Control, builder.ToString());
        }
        protected bool ValueHasChanged { get; set; }
        public override void Reset()
        {
            base.Reset();
            ValueHasChanged = true;
        }
    }
}
