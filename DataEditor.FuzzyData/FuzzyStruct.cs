using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.FuzzyData
{
    [Serializable]
    public partial class FuzzyStruct : FuzzyObject
    {
        public FuzzyStruct()
            : base()
        {
            this.ClassName = FuzzySymbol.GetSymbol("Struct");
        }
    }
}
