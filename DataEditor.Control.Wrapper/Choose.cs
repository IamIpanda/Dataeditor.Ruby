using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class Choose : WrapControlEditor<FuzzyData.FuzzyFixnum,Prototype.ProtoComboBox>
    {
        public override void Bind()
        {
            base.Bind();
            Control.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        }
        public override string Flag { get { return "choose"; } }
        Help.LinkTable<int, int> Dictionary = new Help.LinkTable<int, int>();
        List<Help.Parameter.Text> Texts = new List<Help.Parameter.Text>();
        public override void Reset()
        {
            base.Reset();;
            // 重置所有选项
            Control.Items.Clear();
            Dictionary.Clear();
            Texts.Clear();
            // i 表示用户看到的选项ID，j 表示实际上的值
            int i = -1, j = -1;
            // 抓取主参数
            Dictionary<object, object> choices = argument.GetArgument<Dictionary<object, object>>("CHOICE");
            // 依次遍历
            foreach (object ob in choices.Keys)
            {
                // 校验索引状态
                // 是单项的场合
                if (ob is int)
                {
                    i++;
                    j = Convert.ToInt32(ob);
                    string text = choices[ob].ToString();
                    Dictionary.Add(i, j);
                    Texts.Add(new Help.Parameter.Text(text));
                    Control.Items.Add(text);
                }
                // 是复项的场合
                else
                {
                    // 转换成选项格式
                    Help.Parameter file_choice = choices[ob] as Help.Parameter;
                    if (file_choice == null) continue;
                    FuzzyData.FuzzyArray targets;
                    // 尝试直接获取值
                    targets = file_choice.GetArgument<FuzzyData.FuzzyArray>("DATA");
                    if (targets == null)
                    {
                        //  从选项中获取目标文件的短名
                        string key = file_choice.GetArgument<string>("DATA");
                        // 将文件数据读取
                        targets = Help.Data.Instance[key] as FuzzyData.FuzzyArray;
                    }
                    if (targets == null) continue;
                    // 获取指定的Text文档
                    Help.Parameter.Text text = file_choice.GetArgument<Help.Parameter.Text>("TEXT");
                    if (text == null) continue;
                    // 获取指定的过滤器
                    Contract.Runable filter = file_choice.GetArgument<Contract.Runable>("FILTER");
                    // 获取指定的ID值
                    string id_symbol = file_choice.GetArgument<string>("ID");
                    FuzzyData.FuzzySymbol id_fuzzy_symbol = null;
                    if (id_symbol != null && id_symbol != "")
                        id_fuzzy_symbol = FuzzyData.FuzzySymbol.GetSymbol("@" + id_symbol);
                    // 对于 FuzzyString 组，需先滤掉所有开头的空字符串
                    //（对于一般的字符串组来说，滤掉的是 0）
                    foreach (var target in targets)
                    {
                        var str = target as FuzzyData.FuzzyString;
                        if (str == null) break;
                        if (str.Text.Length > 0) break;
                        j++;
                    }
                    int w = j;
                    foreach (FuzzyData.FuzzyObject target in targets)
                    {
                        // 过滤器的过滤值
                        if (w >= 0) { w--; continue; }
                        // 计数器前进
                        j++;
                        // 如果是空值，那么把它忽略
                        if (target == null || target == FuzzyData.FuzzyNil.Instance) continue;
                        // 如果指定了过滤器，并且过滤器宣告此值无效，那么忽略之。
                        if (filter != null && Convert.ToBoolean(filter.call(target, parent)) == false) continue;
                        // 如果指定了ID，那么依次结算
                        if (id_symbol != null && id_symbol != "")
                        {
                            // 搜索 ID
                            FuzzyData.FuzzyFixnum j_fix = target[id_fuzzy_symbol] as FuzzyData.FuzzyFixnum;
                            // 没有指定的 ID 项就直接跳过
                            if (j_fix == null) continue;
                            j = Convert.ToInt32(j_fix.Value);
                        }
                        // 如果没有指定 ID，那么依次向后放置作为结算。
                        else j++;
                        // 最后，把它们加进目标表中。
                        Dictionary.Add(++i, j);
                        Texts.Add(text);
                        // 参数约定为：目标来源、观察表、i、j。
                        Control.Items.Add(text.ToString(target, text.Watch, i, j));
                        // TODO: Monitor the Text.
                    }
                }
            }

        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("choice", new Dictionary<object, object>());
        }

        public override void Push()
        {
            value.Value = Dictionary.Verse[Control.SelectedIndex];
        }

        public override void Pull()
        {
            int short_value = Dictionary.Reverse[Convert.ToInt32(value.Value)];
            if (short_value < Control.Items.Count) Control.SelectedIndex = short_value;
            else
            {
                Help.Log.log("由于值过短，choose 控件抛弃了一个值：" + short_value);
                if (Control.Items.Count > 0) Control.SelectedIndex = 0; // 没有Push。
                else Control.Enabled = false;
            }
        }

        public override bool ValueIsChanged()
        {
            return value.Value != Dictionary.Verse[Control.SelectedIndex];
        }
        protected void RefreshText(int id = -1)
        {
            if (id == -1)
                for (int i = 0; i < Control.Items.Count; i++) RefreshText(i);
            Control.Items[id] = Texts[id].ToString();
        }
    }
}
