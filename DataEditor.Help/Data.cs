using System;
using System.Text;
using DataEditor.FuzzyData;
using System.Collections.Generic;

namespace DataEditor.Help
{
    public class Data
    {
        public static Data Instance { get; set; }
        static Data() { Instance = new Data(); }
        protected Data () { }
        protected Dictionary<string, FuzzyObject> Datas = new Dictionary<string, FuzzyObject>();
        public FuzzyObject this[string name]
        {
            get
            {
                if ( Datas.ContainsKey(name) )
                    return Datas[name];
                else
                    // ask for the file.
                    return null;
            }
            set
            {
                if ( Datas.ContainsKey(name) )
                    Datas[name] = value;
                else
                    Datas.Add(name, value);
            }
        }
    }
}