using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class LazyChoose : WrapControlEditor<FuzzyData.FuzzyFixnum, Prototype.ProtoComboBox>
    {
        public override string Flag { get { return "lazy_choose"; } }
        Help.LinkTable<int, int> stats = new Help.LinkTable<int, int>();
        Help.LinkTable<int, int> flexiable = new Help.LinkTable<int, int>();
        List<Help.Parameter.Text> static_text = new List<Help.Parameter.Text>();
        List<Help.Parameter.Text> flexiable_text = new List<Help.Parameter.Text>();
        public override void Push()
        {
            int i = Control.SelectedIndex;
            if (stats.RawVerse.ContainsKey(i)) { value.Value = stats.Verse[i]; return; }
            if (flexiable.RawVerse.ContainsKey(i)) { value.Value = flexiable.Verse[i]; return; }
            Help.Log.log("无法查找此值（i）：" + i.ToString());
        }

        public override void Pull()
        {
            if (parent == null) return;
            int j = Convert.ToInt32(value.Value);
            if (stats.RawReverse.ContainsKey(j)) { Control.SelectedIndex = stats.Reverse[j]; return; }
            if (flexiable.RawReverse.ContainsKey(j)) { Control.SelectedIndex = flexiable.Reverse[j]; return; }
            Help.Log.log("无法查找此值（j）：" + j.ToString());
            if(Control.Items.Count == 0) return;
            Control.SelectedIndex = 0;
            Push();
        }

        public override bool ValueIsChanged()
        {
            if (parent == null) return false;
            int i = Control.SelectedIndex;
            if (stats.RawVerse.ContainsKey(i)) { return stats.Verse[i] != value.Value ; }
            if (flexiable.RawVerse.ContainsKey(i)) { return flexiable.Verse[i] != value.Value; }
            return false;
        }
        public override void Bind()
        {
            base.Bind();
            Control.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("source", null);
            argument.SetArgument("choice", new Dictionary<object, object>());
            argument.SetArgument("textbook", null, Help.Parameter.ArgumentType.Must);
            argument.SetArgument("id", FuzzyData.FuzzySymbol.GetSymbol("@id"), Help.Parameter.ArgumentType.HardlyEver);
        }
        /// <summary>
        /// 根据给定的内容，设定目标值
        /// 参数没有意义，仅为了和原有的 Reset 相分别。
        /// </summary>
        /// <param name="i"></param>
        public void Reset(int i)
        {
            // 清零重置
            flexiable.Clear();
            flexiable_text.Clear();
            Control.Items.Clear();
            // 静态恢复
            foreach (Help.Parameter.Text text in static_text) Control.Items.Add(text.ToString(1));
            // 动态抓取
            var source = argument.GetArgument<Contract.Runable>("source");
            var symbol = argument.GetArgument<FuzzyData.FuzzySymbol>("id");
            var textbook = argument.GetArgument<Help.Parameter.Text>("textbook");
            var list = (IEnumerable<object>)source.call(value, parent, this);
            // i 表示用户看到的 ID j 表示对应的值
            i = stats.RawVerse.Count - 1; int j = -1;
            // 获取的值
            foreach (var target in list)
            {
                FuzzyData.FuzzyObject obj = target as FuzzyData.FuzzyObject;
                if (obj == null) continue;
                // 配置 i 和 j
                i++;
                j = Convert.ToInt32((obj[symbol] as FuzzyData.FuzzyFixnum).Value);
                // 索引
                flexiable.Add(i, j);
                // 文本
                var target_text = textbook.Clone() as Help.Parameter.Text;
                flexiable_text.Add(target_text);
                Control.Items.Add(target_text.ToString(target, target_text.Watch, i, j));
            }
        }
        public override void Reset()
        {
            base.Reset();
            var choices = argument.GetArgument<Dictionary<object,object>>("CHOICE");
            // i 表示用户看到的选项ID，j 表示实际上的值
            int i = -1, j = -1;
            foreach (object ob in choices.Keys)
                if (ob is int)
                {
                    i++;
                    j = Convert.ToInt32(ob);
                    string text = choices[ob].ToString();
                    stats.Add(i, j);
                    static_text.Add(new Help.Parameter.Text(text));
                }
        }
        public override FuzzyData.FuzzyObject Parent
        {
            get { return base.parent; }
            set
            {
                parent = value;
                Reset(0);
                base.Parent = value;
            }
        }
    }
}
