using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.FuzzyData
{
    public partial class FuzzyFloat: FuzzyObject,ICloneable
    {
        protected double value;

        public FuzzyFloat(double value)
        {
            this.value = value;
            this.ClassName = FuzzySymbol.GetSymbol("Float");
        }

        public FuzzyFloat(float value)
        {
            this.value = value;
            this.ClassName = FuzzySymbol.GetSymbol("Float");
        }

        public override string ToString()
        {
            return this.value.ToString();
        }

        public event EventHandler<FuzzyValueChangedEventArgs<double>> ValueChanged;
        public double Value
        {
            get { return value; }
            set 
            {
                this.value = value;
                if (ValueChanged != null)
                    ValueChanged(this, new FuzzyValueChangedEventArgs<double>(value));
            }
        }
        public override object Clone()
        {
            return new FuzzyFloat(Value);
        }
        public override void Clone(FuzzyObject source)
        {
            base.Clone(source);
            FuzzyFloat S = source as FuzzyFloat;
            if (S != null)
                value = S.value;
        }
    }
}
