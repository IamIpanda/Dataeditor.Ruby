using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataEditor.Help
{
    public static class ParameterPlugin
    {
        public static void LoadFromRubyHash(this DataEditor.Help.Parameter parameter,IronRuby.Builtins.Hash hash)
        {
            foreach (object ob in hash.Keys)
            {
                IronRuby.Builtins.RubySymbol sym = ob as IronRuby.Builtins.RubySymbol;
                if (sym == null) continue;
                parameter.Arguments.Add(sym.ToString(), hash[ob]);
            }
        }
        public static object RubyCheckValue(this DataEditor.Help.Parameter parameter, object target)
        {
            if (target is IronRuby.Builtins.Hash) return RubyHashToDictionary(parameter, target as IronRuby.Builtins.Hash);
            else if (target is IronRuby.Builtins.RubyStruct) return RubyStructToParameter(parameter, target as IronRuby.Builtins.RubyStruct);
            else if (target is IronRuby.Builtins.MutableString) return target.ToString();
            else if (target is IronRuby.Builtins.RubySymbol) return target.ToString();
            else if (target is IronRuby.Builtins.Proc) return new Ruby.Proc(target as IronRuby.Builtins.Proc);
            else if (target is IronRuby.Builtins.RubyArray) return RubyArrayToList(parameter, target as IronRuby.Builtins.RubyArray);
            return target;
        }
        public static Dictionary<object, object> RubyHashToDictionary(this DataEditor.Help.Parameter parameter, IronRuby.Builtins.Hash hash)
        {
            Dictionary<object, object> answer = new Dictionary<object, object>();
            foreach (object key in hash.Keys)
                answer.Add(RubyCheckValue(parameter, key), RubyCheckValue(parameter, hash[key]));
            return answer;
        }
        public static List<object> RubyArrayToList(this DataEditor.Help.Parameter parameter, IronRuby.Builtins.RubyArray array)
        {
            List<object> answer = new List<object>();
            foreach (var item in array)
                answer.Add(item);
            return answer;
        }
        public static Parameter RubyStructToParameter(this DataEditor.Help.Parameter parameter, IronRuby.Builtins.RubyStruct target)
        {
            Parameter child = new Parameter();
            int count = 0;
            foreach (var name in target.GetNames())
                child.Arguments.Add(name, RubyCheckValue(parameter, target.Values[count++]));
            return child;
        }
    }
}
