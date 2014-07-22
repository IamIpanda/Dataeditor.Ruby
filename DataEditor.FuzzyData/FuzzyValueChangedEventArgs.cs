using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.FuzzyData
{
    public partial class FuzzyValueChangedEventArgs<T> : EventArgs
    {
        protected T newValue;
        public T NewValue { get { return newValue; } }
        public FuzzyValueChangedEventArgs(T NewValue) 
        {
            this.newValue = NewValue;
        }
    }
}
