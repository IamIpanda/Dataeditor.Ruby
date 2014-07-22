using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.FuzzyData
{
    public partial class FuzzyBool : FuzzyObject,ICloneable
    {
        protected bool value;
        protected static List<FuzzyBool> trues = new List<FuzzyBool>();
        protected static List<FuzzyBool> falses = new List<FuzzyBool>();

        private FuzzyBool(bool t) { value = t; this.ClassName = FuzzySymbol.GetSymbol("Bool"); }

        public override string ToString()
        {
            return value ? "Ruby::True" : "Ruby::False";
        }

        public override bool Equals(object obj)
        {
            if (obj is FuzzyBool)
                return (obj as FuzzyBool).value ? value : !value;
            if (obj != null && (bool)obj != false)
                return value;
            return !value;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public event EventHandler<FuzzyValueChangedEventArgs<bool>> ValueChanged;
        public bool Value
        {
            get { return this.value; }
            set 
            {
                this.value = value;
                if (ValueChanged != null)
                    ValueChanged(this, new FuzzyValueChangedEventArgs<bool>(value));
            }
        }
        public static FuzzyBool True
        {
            get 
            {
                FuzzyBool b = new FuzzyBool(true);
                trues.Add(b);
                return b;
            }
        }
        public static FuzzyBool False
        {
            get
            {
                FuzzyBool b = new FuzzyBool(false);
                falses.Add(b);
                return b;
            }
        }
        public override object Clone()
        {
            return value ? True : False;
        }
        public override void Clone(FuzzyObject source)
        {
            base.Clone();
            FuzzyBool S = source as FuzzyBool;
            if (S != null)
            {
                if (value) trues.Remove(this); else falses.Remove(this);
                if (S.value) trues.Add(this); else falses.Add(this);
                value = S.value;
            }
        }
        public static explicit operator bool(FuzzyBool self)
        {
            return self.value;
        }
    }
}
