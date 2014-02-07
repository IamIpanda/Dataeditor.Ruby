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
    ///     split
    ///     list
    ///     dictionary
    ///     parameter
    /// </remarks>
    /// </summary>
    public partial class Parameter
    {
        public Dictionary<string, object> Arguments { get; set; }
        protected Dictionary<string, object> Defaults { get; set; }
        protected Dictionary<string, ArgumentType> Types = new Dictionary<string, ArgumentType>();
        public bool Unreliable { get; set; }
        public Parameter()
        {
            Defaults = new Dictionary<string, object>();
            Arguments = new Dictionary<string, object>();
            Unreliable = false;
        }
        public T GetArgument<T>(string key)
        {
            key = key.ToUpper();
            ArgumentType type = ArgumentType.Option;
            if (!Unreliable)
                if (Types.TryGetValue(key, out type) == false)
                    Help.Log.log("程序正在试图请求一个未被设定的参数值：" + key);
            object ob = null;
            Arguments.TryGetValue(key, out ob);
            if (ob != null)
            {
                if (!Unreliable && type == ArgumentType.HardlyEver) 
                    Help.Log.log("程序正在试图修改一个不被推荐的值 " + key);
                if (ob is T) return (T)ob;
                else Help.Log.log("程序在正在请求" + key + ":" + typeof(T).ToString() + " 但获得了一个" + ob.GetType().ToString());
            }
            if (!Unreliable)
                 if (type == ArgumentType.Must) Help.Log.log("未提供参数 " + key + "，程序将返回一个不可靠的值");
            Defaults.TryGetValue(key, out ob);
            if (ob is T) return (T)ob;
            return default(T);
        }

        public enum ArgumentType { Must, Option, HardlyEver }
        public void SetArgument(string name, object _default, ArgumentType type = ArgumentType.Must)
        {
            name = name.ToUpper();
            if (Defaults.ContainsKey(name)) System.Diagnostics.Debugger.Break();
            Defaults.Add(name, _default);
            Types.Add(name, type);
        }
        public void OverrideArgument(string name, object _default, ArgumentType type = ArgumentType.HardlyEver)
        {
            name = name.ToUpper();
            if (!(Defaults.ContainsKey(name))) System.Diagnostics.Debugger.Break();
            Defaults[name] = _default;
            Types[name] = type;
        }
        public void RemoveArgument(string name)
        {
            if (!(Defaults.ContainsKey(name))) System.Diagnostics.Debugger.Break();
            Defaults.Remove(name);
            Types.Remove(name);
        }
        public bool CheckArgument()
        {
            foreach (string key in Defaults.Keys)
                if (Types[key] == ArgumentType.Must && !(Arguments.ContainsKey(key))) return false;
            return true;
        }

        public bool CheckArgument(out string str)
        {
            StringBuilder sb = new StringBuilder();
            bool ans = true;
            foreach (string key in Defaults.Keys)
                if (Types[key] == ArgumentType.Must && !(Arguments.ContainsKey(key)))
                {
                    ans = false;
                    sb.Append("没有提供参数 ");
                    sb.AppendLine(key);
                }
            str = sb.ToString();
            return ans;
        }

        public class Text : ICloneable
        {
            Contract.Runable GetString { get; set; }
            public List<FuzzyData.FuzzyObject> Watch { get; set; }
            public Text(Contract.Runable get_string = null)
            {
                GetString = get_string;
                if (GetString == null) System.Diagnostics.Debugger.Break();
                Watch = new List<FuzzyData.FuzzyObject>();
            }
            public Text(Text origin) 
            {
                GetString = origin.GetString;
                Watch = new List<FuzzyData.FuzzyObject>();
            }
            public Text(string default_value)
            {
                GetString = new Help.Return(default_value);
                Watch = new List<FuzzyData.FuzzyObject>();
            }
            public virtual string ToString(params object[] argument)
            {
                object value = GetString.call(argument);
                return value.ToString();
            }
            public object Clone() { return new Text(this); }
        }
        public class Split
        {
            public enum SplitType { Count, Value };
            public SplitType TypeX { get; set; }
            public SplitType TypeY { get; set; }
            public int ValueX { get; set; }
            public int ValueY { get; set; }
            public int LastXIndex { get; set; }
            public int LastYIndex { get; set; }
            public int LastWidth { get; set; }
            public int LastHeight { get; set; }
            public System.Drawing.Rectangle this[int x = -1, int y = -1, int w = -1, int h = -1]
            {
                get
                {
                    if (x == -1) x = LastXIndex; else LastXIndex = x;
                    if (y == -1) y = LastYIndex; else LastYIndex = y;
                    if (w == -1)  w= LastWidth; else LastHeight = w;
                    if (h == -1) h = LastHeight; else LastHeight = h;
                    int count_x = TypeX == SplitType.Count ? ValueX : w / ValueX;
                    int count_y = TypeY == SplitType.Count ? ValueY : h / ValueY;
                    int part_w = w / count_x;
                    int part_h = h / count_y;
                    return new System.Drawing.Rectangle(x * part_w, y * part_h, part_w, part_h);
                }
            }
            public System.Drawing.Rectangle this[int index, int w = -1, int h = -1]
            {
                get
                {
                    if (w == -1) w = LastWidth; else LastHeight = w;
                    if (h == -1) h = LastHeight; else LastHeight = h;
                    int count_x = TypeX == SplitType.Count ? ValueX : w / ValueX;
                    return this[index % count_x, index / count_x, w, h];
                }
            }
            public Split(SplitType type_x, int x_value, SplitType type_y, int y_value, int x = 0, int y = 0)
            {
                this.TypeX = type_x;
                this.TypeY = type_y;
                this.ValueX = x_value;
                this.ValueY = y_value;
                LastXIndex = x;
                LastYIndex = y;
            }
        }
    }
}
