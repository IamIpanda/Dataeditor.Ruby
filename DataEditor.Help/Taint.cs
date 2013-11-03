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
        protected Dictionary<FuzzyData.FuzzyObject, bool> records = new Dictionary<FuzzyData.FuzzyObject, bool>();
        public bool this[FuzzyData.FuzzyObject key]
        {
 
        }
    }
}
