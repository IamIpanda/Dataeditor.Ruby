using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.FuzzyData
{
    [Serializable]
    public partial class FuzzyFixnum : FuzzyObject,ICloneable
    {
        protected long value;

        public FuzzyFixnum(long value)
        {
            this.value = value;
            this.ClassName = FuzzySymbol.GetSymbol("Fixnum");
        }

        public FuzzyFixnum(int value)
        {
            this.value = value;
            this.ClassName = FuzzySymbol.GetSymbol("Fixnum");
        }

        public override string ToString()
        {
            return this.value.ToString();
        }

        public event EventHandler<FuzzyValueChangedEventArgs<long>> ValueChanged;
        public long Value
        {
            get { return value; }
            set 
            {
                this.value = value;
                if (ValueChanged != null)
                    ValueChanged(this, new FuzzyValueChangedEventArgs<long>(value));
            }
        }
        public override bool Equals(object obj)
        {
            FuzzyFixnum f = obj as FuzzyFixnum;
            if (f == null)
                return base.Equals(obj);
            return f.value == value;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override object Clone()
        {
            return new FuzzyFixnum(value);
        }
        public override void Clone(FuzzyObject source)
        {
            base.Clone(source);
            FuzzyFixnum S = source as FuzzyFixnum;
            if (S != null)
                value = S.value;
        }
        public static long MaxValue { get { return 1073741823; } }
        public static long MinValue { get { return -1073741824; } }
        public static implicit operator FuzzyFixnum(int i) { return new FuzzyFixnum(i); }
    }
}
