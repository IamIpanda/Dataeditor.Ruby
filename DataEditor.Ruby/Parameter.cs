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
            return target;
        }
        public static Dictionary<object, object> RubyHashToDictionary(this DataEditor.Help.Parameter parameter, IronRuby.Builtins.Hash hash)
        {
            Dictionary<object, object> answer = new Dictionary<object, object>();
            foreach (object key in hash.Keys)
                answer.Add(RubyCheckValue(parameter, key), RubyCheckValue(parameter, hash[key]));
            return answer;
        }
        public static Parameter RubyStructToParameter(this DataEditor.Help.Parameter parameter, IronRuby.Builtins.RubyStruct target)
        {
            Parameter child = new Parameter();
            int count = 0;
            foreach (var name in target.GetNames())
                child.Arguments.Add(name, target.Values[count++]);
            return child;
        }
        public static void LoadFromClass(this DataEditor.Help.Parameter parameter, IronRuby.Builtins.RubyClass type)
        {
            
        }
    }
}
