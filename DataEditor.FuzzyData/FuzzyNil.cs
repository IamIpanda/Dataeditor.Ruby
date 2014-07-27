﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.FuzzyData
{
    [Serializable]
    public class FuzzyNil : FuzzyObject
    {
        private static FuzzyNil instance;

        private FuzzyNil() { this.ClassName = FuzzySymbol.GetSymbol("NilClass"); }

        public override string ToString()
        {
            return "Ruby::Nil";
        }

        public static FuzzyNil Instance {
            get {
                if (instance == null)
                    instance = new FuzzyNil();
                return instance; 
            }
        }
    }
}
