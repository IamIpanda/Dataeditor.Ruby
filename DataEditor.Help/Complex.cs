using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.FuzzyData
{
    public class FuzzyComplex : FuzzyObject
    {
        public FuzzyComplex() { consistence = new Dictionary<string, FuzzyObject>(); }
        public Dictionary<string, FuzzyObject> consistence { get; set; }
        public new FuzzyObject this[string index]
        {
            get { FuzzyObject answer = null; consistence.TryGetValue(index, out answer); return answer; }
        }
        public override int GetHashCode()
        {
 	         int value = 0;
            foreach( object item in consistence.Values) value += item.GetHashCode();
            return value;
        }
        public override bool Equals(object obj)
        {
            FuzzyComplex com = obj as FuzzyComplex ;
            if (com == null) return false;
            foreach(var key in consistence.Keys )
                if (com[key] != consistence[key]) return false;
            return true;
        }
        public Dictionary<string,FuzzyObject>.KeyCollection AllKeys { get { return this.consistence.Keys; } }
        public Dictionary<string, FuzzyObject>.ValueCollection AllValues { get { return this.consistence.Values; } }
    }
    public class FuzzySymbolComplex : FuzzySymbol
    {
        protected FuzzySymbolComplex(string s) : base(s) { Extra = new Dictionary<string, FuzzySymbol>(); }
        public Dictionary<string, FuzzySymbol> Extra { get; set; }
        public new static FuzzySymbolComplex GetSymbol(string text)
        {
            var list = GetSymbols();
            if (list.ContainsKey(text) && list[text] is FuzzySymbolComplex) return list[text] as FuzzySymbolComplex;
            var ans = new FuzzySymbolComplex(text);
           // list.Add(text, ans);
            return ans;
        }
    }
}
