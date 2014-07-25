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


        static Dictionary<string, System.Windows.Forms.Control> Controls
             = new Dictionary<string, System.Windows.Forms.Control>();
        static public System.Windows.Forms.Control GetControl(string Name)
        {
            System.Windows.Forms.Control ans;
            if (Controls.TryGetValue(Name, out ans)) return ans;
            else return null;
        }
        static public void SetControl(string Name, System.Windows.Forms.Control Control)
        {
            if (Controls.ContainsKey(Name)) Controls[Name] = Control;
            else Controls.Add(Name, Control);
        }

        static public System.Windows.Forms.ToolStripLabel StatusLabel;
        static public void SetStatus(string str)
        {
            if (StatusLabel != null) StatusLabel.Text = str;
            StatusLabel.Invalidate();
        }
        static public string GetStatus()
        {
            return StatusLabel == null ? null : StatusLabel.Text;
        }
        static public System.Windows.Forms.ToolTip ToolTip;
        static public void SetTip(System.Windows.Forms.Control Control, String Tip)
        {
            if (ToolTip != null) ToolTip.SetToolTip(Control, Tip);
        }

        static public class Sort
        {
            static Contract.Runable comparison;
            static public void SetComparison(Contract.Runable target) { comparison = target; }
            static public int InnerCompare(object T1, object T2)
            {
                return Convert.ToInt32(comparison.call(T1, T2));
            }
            static public Comparison<object> Compare = InnerCompare;
        }
    }
}
