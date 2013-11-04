using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Help
{
    public class Taint
    {
        public static Taint Instance { get; set; }
        static Taint() { Instance = new Taint(); }
        protected Taint() { }
        protected Dictionary<FuzzyData.FuzzyObject, Contract.TaintState> records = new Dictionary<FuzzyData.FuzzyObject, Contract.TaintState>();
        public Contract.TaintState this[FuzzyData.FuzzyObject key]
        {
            get
            {
                var state = Contract.TaintState.UnTainted;
                records.TryGetValue(key, out state);
                return state;
            }
            set
            {
                if (records.ContainsKey(key)) records[key] = value;
                else records.Add(key, value);
            }
        }
    }
}
