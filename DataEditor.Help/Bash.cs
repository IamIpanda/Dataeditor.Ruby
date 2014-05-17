using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DataEditor.Help
{
    public class Bash
    {
        static Regex reg = new Regex("\\[.+?\\]");
        public static FuzzyData.FuzzyObject GetTarget(string str)
        {
            str = reg.Replace(str, (m => "." + m.Value.Substring(1, m.Value.Length - 2))); // the Great Lambda!
            string[] parts = str.Split('.');
            if (parts.Length < 2) return null;
            if (parts[0].ToUpper() != "DATA") return null;
            int i = 2;
            return FuzzyData.FuzzyNil.Instance;
        }

    }
}
