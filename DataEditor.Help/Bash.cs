using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DataEditor.Help
{
    public static class Bash
    {
        static Regex reg = new Regex("\\[.+?\\]");
        public static FuzzyData.FuzzyObject GetTarget(string str)
        {
            str = reg.Replace(str, (m => "." + m.Value.Substring(1, m.Value.Length - 2))); // the Great Lambda!
            string[] parts = str.Split('.');
            if (parts.Length < 2) return null;
            string start = parts[0].ToUpper();
            FuzzyData.FuzzyObject ans;
            if (start == "DATA")
                ans = Data.Instance[parts[1]];
            else if (start == "MAP")
            {
                int j = 0;
                if (int.TryParse(parts[1], out j))
                    ans = Data.Instance[parts[1]];
                else return null;
            }
            else return null;
            int i = 2;
            while (i < parts.Length)
            {
                if (ans is FuzzyData.FuzzyArray)
                {
                    int j = 0;
                    if (int.TryParse(parts[i++], out j))
                        ans = (ans as FuzzyData.FuzzyArray)[j] as FuzzyData.FuzzyObject;
                    else return null;
                }
                else if (ans is FuzzyData.FuzzyHash)
                {
                    if (i == parts.Length - 2) return null;
                    var hash = ans as FuzzyData.FuzzyHash;
                    string choose = parts[i++].ToUpper();
                    int index = 0;
                    if (!(int.TryParse(parts[i++], out index)))
                        return null;
                    if (choose == "KEY" || choose == "KEYS")
                        foreach (object key in hash.Keys)
                            if (index-- == 0)
                            { ans = key as FuzzyData.FuzzyObject; break; }
                    if (choose == "VALUE" || choose == "VALUES")
                        foreach (object value in hash.Values)
                            if (index-- == 0)
                            { ans = value as FuzzyData.FuzzyObject; break; }
                }
                else if (ans is FuzzyData.FuzzyObject)
                {
                    string choice = parts[i++];
                    if (!(choice.StartsWith("@"))) choice = "@" + choice;
                    var obj = ans as FuzzyData.FuzzyObject;
                    object target;
                    if (obj.InstanceVariables.TryGetValue(FuzzyData.FuzzySymbol.GetSymbol(choice), out target))
                        ans = target as FuzzyData.FuzzyObject;
                    else return null;
                }
                else return null;
            }
            return ans;
        }
        public delegate object Execute(string str);
        static public Execute RubyEngine;
        static public object Call(string str)
        {
            try
            {
                if (RubyEngine != null)
                    return RubyEngine.Invoke(str);
                return null;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("执行用户脚本中发生了错误。" + Environment.NewLine + ex.ToString());
                return null;
            }
        }
    }
}
