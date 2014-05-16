using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataEditor.Control.Event;

namespace DataEditor.Control.Wrapper
{
    public class _Event : WrapControlEditor<FuzzyData.FuzzyArray, Prototype.ProtoEventList>
    {
        public override string Flag { get { return "event"; } }
        FuzzyData.FuzzySymbol sub_code = null, sub_indent = null, sub_parameters = null;
        Dictionary<int, DataEditor.Control.Event.EventCommand> commands = new  Dictionary<int,Control.Event.EventCommand>();
        List<Control.Event.CommandGroup> groups = new List<Control.Event.CommandGroup>();
        Control.Event.EventCommandChoser choser;
        Dictionary<int, int> Ranges = new Dictionary<int, int>();
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("code", "@code", Help.Parameter.ArgumentType.Option);
            argument.SetArgument("indent", "@indent", Help.Parameter.ArgumentType.Option);
            argument.SetArgument("parameters", "@parameters", Help.Parameter.ArgumentType.Option);
            argument.SetArgument("commands", null);
            argument.SetArgument("groups", null);
        }
        public override void Reset()
        {
            string text;
            text = argument.GetArgument<string>("code");
            sub_code = FuzzyData.FuzzySymbol.GetSymbol(text);
            text = argument.GetArgument<string>("indent");
            sub_indent = FuzzyData.FuzzySymbol.GetSymbol(text);
            text = argument.GetArgument<string>("parameters");
            sub_parameters = FuzzyData.FuzzySymbol.GetSymbol(text);
            // 载入事件指令集
            var dic = argument.GetArgument<Dictionary<object, object>>("commands");
            int code; EventCommand command;
            commands = new Dictionary<int, EventCommand>();
            foreach (var key in dic.Keys)
            {
                if (!(key is int)) continue;
                code = Convert.ToInt32(key);
                command = dic[key] as EventCommand;
                if (command == null) return;
                commands.Add(code, command);
            }
            // 载入事件分组信息
            var list = argument.GetArgument<List<object>>("groups");
            groups = new List<CommandGroup>();
            CommandGroup group;
            foreach (var obj in list)
            {
                group = obj as CommandGroup;
                if (group != null) groups.Add(group);
            }
            base.Reset();
        }

        public override void Bind()
        {
            base.Bind();
            Control.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            Control.Enter += Control_Enter;
            Control.Leave += Control_Leave;
            Control.DoubleClick += Control_DoubleClick;
            Control.KeyDown += Control_KeyDown;
            Control.SelectedIndexChanged += Control_SelectedIndexChanged;
            Control.ItemGoingDraw = this.ItemGoingDraw;
        }

        void Control_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == System.Windows.Forms.Keys.Up) Prev();
            else if (e.KeyData == System.Windows.Forms.Keys.Down) Next();
            else if (e.KeyData == System.Windows.Forms.Keys.Space) Change();
            else if (e.KeyData == System.Windows.Forms.Keys.Enter) Add();
            e.SuppressKeyPress = true;
        }

        void Control_SelectedIndexChanged(object sender, EventArgs e)
        {
            Control.Invalidate(false);
        }

        void Control_DoubleClick(object sender, EventArgs e)
        {
            Add();
        }

        void Control_Leave(object sender, EventArgs e)
        {
            choser.Dispose();
        }

        void Control_Enter(object sender, EventArgs e)
        {
            if (choser == null || choser.IsDisposed)
            {
                choser = new Control.Event.EventCommandChoser();
                choser.Commands = commands;
                choser.Groups = groups;
                choser.Initialize();
            }
        }

        public override void Push()
        {
            /* 已弃用 */
        }

        public override void Pull()
        {
            Control.Items.Clear();
            foreach (var obj in value)
            {
                FuzzyData.FuzzyObject fobj = obj as FuzzyData.FuzzyObject;
                if (fobj == null) throw new ArgumentNullException();
                Control.Items.Add(GetText(fobj));
            }
            BuildRange();
        }

        protected void BuildRange()
        {
            Ranges.Clear();
            int index = 0;
            int end;
            while (index < value.Size)
            {
                end = SearchToEnd(index);
                if (index != end) Ranges.Add(index, end);
                index++;
            }
        }

        protected int SearchToEnd(int index)
        {
            var target = value[index] as FuzzyData.FuzzyObject;
            int code = GetCode(target);
            int indent = GetIndent(target);
            if (code == 0) return index; 
            FuzzyData.FuzzyObject child;
            EventCommand childmodel;
            int childindent;
            while(index < value.Size - 1)
            {
                child = value[++index] as FuzzyData.FuzzyObject;
                childmodel = GetModel(child);
                childindent = GetIndent(child);
                if (childmodel.Follow != code && childindent == indent) break;
            }
            return index - 1;
        }

        public override bool ValueIsChanged()
        {
            return false;
        }
        string GetText(FuzzyData.FuzzyObject command)
        {
            var obj_code = command[sub_code] as FuzzyData.FuzzyFixnum;
            int code = Convert.ToInt32(obj_code.Value);
            var parameters = command[sub_parameters] as FuzzyData.FuzzyArray;
            EventCommand model;
            if (commands.TryGetValue(code, out model) == false)
            {
                Help.Log.log("事件解释器发现了未知的编码：" + code.ToString());
                return "未知指令";
            }
            else
            {
                var str =  model.Text == null ? "" : model.Text.ToString(parameters);
                str = (code > 0 && model.Follow < 0 ? model.Name : "") + (str.Length == 0 ? "" : 
                    (model.UpIndent == 0 && model.DownIndent == 0 ? " : " : ":"))  + str;
                return str;
            }
        }
        FuzzyData.FuzzyObject GetZero(int indent = 0)
        {
            var ans = new FuzzyData.FuzzyObject();
            ans[sub_code] = new FuzzyData.FuzzyFixnum(0);
            ans[sub_indent] = new FuzzyData.FuzzyFixnum(0);
            ans[sub_parameters] = new FuzzyData.FuzzyArray();
            return ans;
        }
        int GetCode(FuzzyData.FuzzyObject obj)
        {
            if (obj == null) return -1;
            FuzzyData.FuzzyFixnum fix = obj[sub_code] as FuzzyData.FuzzyFixnum;
            if (fix == null) return -1;
            return Convert.ToInt32(fix.Value);
        }
        int GetIndent(FuzzyData.FuzzyObject obj)
        {
            if (obj == null) return -1;
            FuzzyData.FuzzyFixnum fix = obj[sub_indent] as FuzzyData.FuzzyFixnum;
            if (fix == null) return -1;
            return Convert.ToInt32(fix.Value);
        }
        void SetIndent(FuzzyData.FuzzyObject obj, int value)
        {
            if (obj == null) return;
            FuzzyData.FuzzyFixnum fix = obj[sub_indent] as FuzzyData.FuzzyFixnum;
            if (fix == null) return;
            fix.Value = value;
        }
        FuzzyData.FuzzyArray GetParameter(FuzzyData.FuzzyObject obj)
        {
            if (obj == null) return null;
            return obj[sub_parameters] as FuzzyData.FuzzyArray;
        }
        EventCommand GetModel(FuzzyData.FuzzyObject obj)
        {
            if (obj == null) return null;
            int code = GetCode(obj);
            EventCommand ans;
            if (commands.TryGetValue(code, out ans) == false) return null;
            return ans;
        }
        void ItemGoingDraw(int index)
        {
            if (Control.SelectedIndex < 0) return;
            var value = this.value[index] as FuzzyData.FuzzyObject;
            var command = GetModel(value);
            Control.UsingFocus = false;
            foreach(int selected in Control.SelectedIndices)
            {
                int va = -1;
                if (Ranges.TryGetValue(selected, out va))
                    if (index >= selected && index <= va)
                    { Control.UsingFocus = true; break; }
            }
            Control.UsingNull = command.Follow >= 0;
            Control.Indent = GetIndent(value);
            Control.ItemColor = command.Color;
            if (Control.UsingNull)
            {
                var parent = commands[command.Follow];
                if (command.UpIndent == 0 && command.DownIndent == 0)
                     Control.AddOnString = commands[command.Follow].Name;
            }
        }
        protected void AddToIndex(EventCommand Type, WrapBaseWindow Window, int Index)
        {
            object raw_with = null;
            if (Type.With != null) raw_with = Type.With.call(Window);
        }
        public void Add()
        {
            var index = Control.SelectedIndex;
            if (choser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                value.Insert(index, choser.Value);
                if (choser.With != null) value.InsertRange(index + 1, choser.With);
                Pull();
                Control.SelectedIndex = index;
            }
        }
        public void Change()
        {
            // 获取 Index
            if (Control.SelectedIndex < 0) return;
            int index = Control.SelectedIndex;
            int origin_index = index;
            // 获取对象
            var target = value[index] as FuzzyData.FuzzyObject;
            var target_parameters = GetParameter(target);
            var command = GetModel(target);
            // 处理 t 型变量
            int Special_Text_Index = -1;
            if (command.Parameters.Contains("t")) Special_Text_Index = command.Parameters.IndexOf("t");
            if (command == null || command.Follow > 0) return;
            var with = new List<FuzzyData.FuzzyObject>();
            while (++index < Control.Items.Count)
            {
                var obj = value[index] as FuzzyData.FuzzyObject;
                var Model = GetModel(obj);
                if (Model.Follow != command.Code) break;
                with.Add(value[index] as FuzzyData.FuzzyObject);
                if (Model.Parameters == "f" && Special_Text_Index >= 0)
                    (target_parameters[Special_Text_Index] as FuzzyData.FuzzyString).Text += Environment.NewLine + (GetParameter(obj)[0] as FuzzyData.FuzzyString).Text;
            }
            if (Ranges.ContainsKey(origin_index)) index = Ranges[origin_index];
            // 申请窗口
            var window = command.ApplicateWindow(target_parameters, with);
            if (window == null) return;
            if (window.Show() == System.Windows.Forms.DialogResult.OK)
            {
                IEnumerable<object> NewWith = null;
                if (command.With != null) 
                {
                    NewWith = command.With.call(window, with) as IEnumerable<object>;
                    Check(NewWith);
                    if (NewWith != null)
                    {
                        value.RemoveRange(origin_index + 1, index - 1);
                        value.InsertRange(origin_index + 1, NewWith);
                        Pull();
                    }
                }
            }
        }
        public void Erase()
        { 
        }
        public void Next()
        {
            int i = Control.SelectedIndex;
            while (i < Control.Items.Count - 1)
                if (GetModel(value[++i] as FuzzyData.FuzzyObject).Follow < 0)
                    break;
            Control.SelectedIndex = i;

        }
        public void Prev()
        {
            int i = Control.SelectedIndex;
            while (i > 0)
                if (GetModel(value[--i] as FuzzyData.FuzzyObject).Follow < 0)
                    break;
            Control.SelectedIndex = i;
        }
        public int Check(IEnumerable<object> Items, int start = 0)
        {
            EventCommand command;
            EventCommand last = null;
            foreach(object obj in Items)
            {
                command = GetModel(obj as FuzzyData.FuzzyObject);
                if (command == null) continue;
                start += (last == null ? 0: last.DownIndent) + command.UpIndent;
                SetIndent(obj as FuzzyData.FuzzyObject, start);
                last = command;
            }
            return start;
        }
    }
}
