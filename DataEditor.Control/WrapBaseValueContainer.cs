using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control
{
    public abstract class WrapBaseValueContainer<T> : WrapBaseContainer where T: FuzzyData.FuzzyObject
    {
        public abstract class WrapBaseSubEditor : WrapBaseEditor<T>
        {
 
        }
    }
}
