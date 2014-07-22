using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.FuzzyData
{
    public partial class FuzzySymbol : FuzzyObject
    {
        private string name;
        private static Dictionary<string, FuzzySymbol> symbols = new Dictionary<string, FuzzySymbol>();
        internal static FuzzySymbol SymbolClassName;
        internal FuzzyString rubyString;

        protected FuzzySymbol(string s)
        {
            this.name = s;
            symbols.Add(s, this);
        }

        internal FuzzySymbol(float nul)
        {
            this.name = "Symbol";
            symbols.Add("Symbol", this);
        }

        public override FuzzySymbol ClassName
        {
            get
            {
                return SymbolClassName;
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        static FuzzySymbol()
        {
            FuzzySymbol.SymbolClassName = new FuzzySymbol(0.0f);
        }

        public static Dictionary<string, FuzzySymbol> GetSymbols()
        {
            return symbols;
        }

        public static FuzzySymbol GetSymbol(string s)
        {
            if (symbols.ContainsKey(s)) return symbols[s];
            return new FuzzySymbol(s);
        }

        public static FuzzySymbol GetSymbol(FuzzyString str)
        {
            string s = str.Text;
            if (symbols.ContainsKey(s)) return symbols[s];
            FuzzySymbol sym = new FuzzySymbol(s);
            sym.rubyString = str;
            return sym;
        }

        public FuzzyString GetRubyString()
        {
            return this.rubyString;
        }

        public string GetString()
        {
            return this.name;
        }

        public override string ToString()
        {
            return (":" + this.name);
        }

        public string Name
        {
            get { return this.name; }
        }

        public FuzzyClass GetClass()
        {
            return FuzzyClass.GetClass(this);
        }

        public FuzzyModule GetModule()
        {
            return FuzzyModule.GetModule(this);
        }
        public override object Clone()
        {
            return this;
        }
        public override void Clone(FuzzyObject source)
        {
            throw new Exception("正在试图修改一个FuzzySymbol");
        }
    }
}
