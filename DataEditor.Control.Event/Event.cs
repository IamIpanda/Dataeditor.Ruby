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
            Control.Enter += Control_Enter;
            Control.Leave += Control_Leave;
            Control.DoubleClick += Control_DoubleClick;
            Control.KeyUp += Control_KeyUp;
            Control.PreviewKeyDown += Control_PreviewKeyDown;
            Control.SelectedIndexChanged += Control_SelectedIndexChanged;
            Control.GetEventItemColor = this.ItemColor;
            Control.ItemEnabled = this.ItemSelectable;
            Control.ItemSelected = this.ItemFocused;
        }

        int next = -1;
        void Control_SelectedIndexChanged(object sender, EventArgs e)
        {
            next = -1;
            // 不存在，不检查
            if (Control.SelectedIndex < 0) { Control.Invalidate(); return; }
            if (Control.SelectedIndex == Control.Items.Count - 1) { Control.Invalidate(); return; }
            var target = value[Control.SelectedIndex] as FuzzyData.FuzzyObject;
            // 抓取信息
            int indent = GetIndent(target);
            var model = GetModel(target);
            // 子节点，不检查
            if (model.Follow > 0) { Control.Invalidate(); return; }
            int code = model.Code;
            next = Control.SelectedIndex;
            // 始动条件
            var fellow = value[next + 1] as FuzzyData.FuzzyObject;
            int sub_indent = GetIndent(fellow);
            if (sub_indent > indent || GetModel(fellow).Follow == code)
            {
                next++;
                while (next < value.Count)
                {
                    fellow = value[next + 1] as FuzzyData.FuzzyObject;
                    sub_indent = GetIndent(fellow);
                    if (sub_indent <= indent && GetModel(fellow).Follow != code) break;
                    next++;
                }
            }
            Control.Invalidate();
        }

        
        void Control_PreviewKeyDown(object sender, System.Windows.Forms.PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Space) e.IsInputKey = false;
        }

        void Control_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == System.Windows.Forms.Keys.I) Change();
            else if (e.KeyData == System.Windows.Forms.Keys.Enter) Add();
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
                str = (code > 0 && model.Follow < 0 ? model.Name : "") + (str.Length == 0 ? "" : " : ")  + str;
                int indent = GetIndent(command);
                string appendix = "";
                for (int i = 0; i < indent; i++) appendix += " ";
                appendix = String.Format("[{0}]", indent);
                if (model.Follow < 0) appendix += EventCommand.FocusSign;
                else appendix += "[" + model.Follow.ToString() + "]";
                return appendix + str;
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
            if (obj == null) return 0;
            FuzzyData.FuzzyFixnum fix = obj[sub_indent] as FuzzyData.FuzzyFixnum;
            if (fix == null) return 0;
            return Convert.ToInt32(fix.Value);
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
        // 
        System.Drawing.Color ItemColor(int index)
        {
            if (index < 0) return default(System.Drawing.Color);
            var command = GetModel(value[index] as FuzzyData.FuzzyObject);
            return command.Color;
        }
        bool ItemSelectable(int index)
        {
            if (index < 0) return false;
            var command = GetModel(value[index] as FuzzyData.FuzzyObject);
            return command.Follow < 0;
        }
        bool ItemFocused(int index)
        {
            return index > Control.SelectedIndex && index <= next;
        }

        public void Add()
        {
            if (choser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Add in;
            }
        }
        public void Change()
        {
            // 获取 Index
            if (Control.SelectedIndex < 0) return;
            int index = Control.SelectedIndex;
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
                    (target_parameters[Special_Text_Index] as FuzzyData.FuzzyString).Text += "\n" + (GetParameter(obj)[0] as FuzzyData.FuzzyString).Text;
            }
            // 申请窗口
            var window = command.ApplicateWindow(target_parameters);
            if (window == null) return;
            if (window.Show() == System.Windows.Forms.DialogResult.OK)
            {

            }
        }
        public void Erase()
        { 
        }
    }
}
