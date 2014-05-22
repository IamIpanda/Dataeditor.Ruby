using System;
using System.Collections.Generic;
using System.Text;
using DataEditor.FuzzyData;

namespace DataEditor.Help
{
    static public class Finder
    {
        public struct SearchOption
        {
            public string Target;
            public bool Name, Value;
            public SearchOption(string target, bool name = true, bool value = true)
            {
                Target = target;
                Name = name;
                Value = value;
            }
        }
        static public FuzzyData.FuzzyObject Search(string target, bool name = true, bool value = true)
        {
            var x = Search(new SearchOption(target, name, value));
            return x.Current;
        }

        static public IEnumerator<FuzzyData.FuzzyObject> Search(SearchOption option)
        {
            List<FuzzyData.FuzzyObject> all = new List<FuzzyObject>();
            foreach (var name in Data.Instance.Names) all.Add(Data.Instance[name]);
            all.AddRange(Data.Instance.Map.Values);
            foreach (var obj in all)
                foreach (var ans in Deepin(obj, option))
                    yield return ans;
        }
        static public IEnumerable<FuzzyObject> Deepin(FuzzyData.FuzzyObject obj, SearchOption option)
        {
            if (obj == null) yield break;
            if (option.Value)
                if (Match(obj, option.Target))
                    yield return obj;
            if (obj.InstanceVariables.Count > 0 && option.Name)
                foreach (var symbol in obj.InstanceVariables.Keys)
                    if (Match(symbol, option.Target))
                        yield return obj[symbol] as FuzzyData.FuzzyObject;
            if (obj is FuzzyArray)
                foreach (object child in (obj as FuzzyArray))
                    foreach (var ans in Deepin(child as FuzzyObject, option)) 
                        yield return ans;
            if (obj is FuzzyHash)
            {
                var hash = obj as FuzzyHash;
                foreach (var key in hash.Keys)
                    foreach (var ans in Deepin(key as FuzzyObject, option))
                        yield return ans;
                foreach (var value in hash.Values)
                    foreach (var ans in Deepin(value as FuzzyObject, option))
                        yield return ans;
            }
            if (obj.InstanceVariables.Count > 0 && option.Value)
                foreach (var target in obj.InstanceVariables.Values)
                    foreach (var ans in Deepin(target as FuzzyObject, option))
                        yield return ans;
            yield break;
        }
        static public bool Match(FuzzyData.FuzzyObject obj, string target)
        {
            string str = obj.ToString();
            return str.Contains(target);
        }
    }
}
