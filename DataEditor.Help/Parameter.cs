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
