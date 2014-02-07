using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Help
{
    public class Catalogue
    {
        public System.Collections.IList Items { get; set; }
        public Help.Parameter.Text Text { get; set; }
        public Contract.Runable Filter { get; set; }
        /// <summary>
        ///  i 表示 FuzzyArray 上的 ID
        ///  j 表示 ListBox 上的 ID
        /// </summary>
        public Help.LinkTable<int, int> Link { get; set; }
        List<Help.Parameter.Text> catalogue = new List<Parameter.Text>();
        List<object> using_value = null;
        public Catalogue(System.Collections.IList items, Help.Parameter.Text origin, Contract.Runable filter = null)
        {
            Items = items;
            Text = origin;
            Filter = filter;
            Link = new LinkTable<int, int>();
        }
        public void InitializeText(List<object> value)
        {
            if (value == null) { Help.Log.log("传给了 catalogue 一个不存在的值。"); return; }
            using_value = value;
            Items.Clear();
            Link.Clear();
            catalogue.Clear();
            int i = 0, j = 0; //  i 表示 List 上的 ID，j 表示 ListBox 的ID（显示顺序）
            for (; i < value.Count; i++)
                if (IsFix(value[i]))
                {
                    Link.Add(i, j);
                    var text = new Parameter.Text(Text);
                    catalogue.Add(text);
                    Items.Add(text.ToString(value[i], text.Watch, i, j));
                    if (text.Watch != null && text.Watch.Count != 0) 
                        Monitor.Watch(text, TextChanged, text.Watch.ToArray());
                    j++;
                }
        }
        void TextChanged(object sender, EventArgs e)
        {
            if (!(sender is Parameter.Text)) return;
            Parameter.Text text = sender as Parameter.Text;
            int j = catalogue.IndexOf(text);
            int i = Link.Reverse[j];
            object target = using_value[i];
            Items[j] = (text.ToString(target, text.Watch, i, j));
            Monitor.Remove(text);
            Monitor.Watch(text, TextChanged, text.Watch.ToArray());
        }
        bool IsFix(object value)
        {
            if (Filter == null)
                if (value is FuzzyData.FuzzyString)
                    return (value as FuzzyData.FuzzyString).Text.Length != 0;
                else return (value != null && value != FuzzyData.FuzzyNil.Instance);
            else return Convert.ToBoolean(Filter.call(value));
        }
    }
}