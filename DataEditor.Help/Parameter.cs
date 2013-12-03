using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Help
{
    /// <summary>
    /// 从控制文件中给予控件的描述数据的抽象类。
    /// <remarks>
    /// Parameter 只接受下述之类型：
    ///     int
    ///     string
    ///     runable
    ///     color
    ///     text
    ///     list
    ///     dictionary
    ///     parameter
    /// </remarks>
    /// </summary>
    public partial class Parameter
    {
        public Dictionary<string, object> Arguments { get; set; }
        public Dictionary<string, object> Defaults { get; set; }
        public Parameter()
        {
            Defaults = new Dictionary<string, object>();
            Arguments = new Dictionary<string, object>();
        }
        public T GetAegument<T>(string key)
        {
            key = key.ToUpper();
            object ob = null;
            Arguments.TryGetValue(key, out ob);
            if (ob is T) return (T)ob;
            Defaults.TryGetValue(key, out ob);
            if (ob is T) return (T)ob;
            return default(T);
        }
        public class Text
        {
            Contract.Runable GetString { get; set; }
            public List<object> Watch { get; set; }
            public Text(Contract.Runable get_string = null)
            {
                GetString = get_string;
                Watch = new List<object>();
            }
            public Text(string default_value)
            {
                GetString = new Help.Return(default_value);
                Watch = new List<object>();
            }
            public string ToString(params object[] argument)
            {
                object value = GetString.call(argument);
                return value.ToString();
            }
        }
    }
}
