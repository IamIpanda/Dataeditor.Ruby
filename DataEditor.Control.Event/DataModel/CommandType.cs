using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using DataEditor.FuzzyData;
using DataEditor.Control.Event.DataModel;

namespace DataEditor.Control.Event.DataModel
{
    public class CommandType
    {
        public const char ParameterIntSign = 'i',
            ParameterStringSign = 's',
            ParameterTextSign = 't',
            ParameterTextFollowSign = 'f',
            ParameterFloatSign = 'd',
            ParameterColorSign = 'c',
            ParameterTuneSign = 'e',
            ParameterAudioSign = 'a',
            ParameterBoolSign = 'b',
            ParameterMoveRouteSign = 'm',
            ParameterMoveCommandSign = 'o',
            ParameterUndetermindSign = 'u',
            ParameterArraySign = 'l';
        protected const string ParameterStrIntSign = "i",
            ParameterStrStringSign = "s",
            ParameterStrTextSign = "t",
            ParameterStrTextFollowSign = "f",
            ParameterStrFloatSign = "d",
            ParameterStrColorSign = "c",
            ParameterStrTuneSign = "e",
            ParameterStrAudioSign = "a",
            ParameterStrBoolSign = "b",
            ParameterStrMoveRouteSign = "m",
            ParameterStrMoveCommandSign = "o",
            ParameterStrUndetermindSign = "u",
            ParameterStrArraySign = "l";
        /// <summary>
        /// 指令 ID
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 若这一指令对应一个父亲指令，则此项给出父亲指令的 ID
        /// </summary>
        public int Follow { get; set; }
        /// <summary>
        /// 若此指令为父指令，那么此项给出结束指令的 ID。
        /// 若此指令需要延伸，那么此项等于父亲指令的 ID。
        /// </summary>
        public int Ends { get; set; }
        /// <summary>
        /// 进入此指令前，Indent 的平移值
        /// 此参数已停用。
        /// </summary>
        public int UpIndent { get; set; }
        /// <summary>
        /// 结束此指令后，Indent 的平移值
        /// 此参数已停用。
        /// </summary>
        public int DownIndent { get; set; }
        /// <summary>
        /// 当转化为指令代码时，这一指令对应的函数名
        /// </summary>
        public string Command { get; set; }
        /// <summary>
        /// 这一指令显示的名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 这一指令的参数类型。
        /// 这是一个字符串，将指定不同的类型。
        /// </summary>
        public string Parameters { get; set; }
        /// <summary>
        /// 使用的 Text 类型，用于显示后面的字符串
        /// </summary>
        public Help.Parameter.Text Text { get; set; }
        // 内部新模板
        protected List<FuzzyObject> model = null;
        /// <summary>
        /// 弹出一个窗口，编辑数据。
        /// 若为 null，指令将被直接压入
        /// 参数为一个 Command Fuzzy 结构
        /// 如果此后有若干个 Follow 位，则会追加至参数中
        /// </summary>
        public Contract.Runable Window = null;
        /// <summary>
        /// 给定一个 Parameters : FuzzyArray
        /// 返回一个追加指令包。
        /// </summary>
        public Contract.Runable With = null;
        public override string ToString()
        {
            return Name + (Window == null ? "" : "...");
        }
        public CommandType()
        {
            this.Code = -1;
            this.Command = "Unknown";
            this.DownIndent = 0;
            this.Follow = -1;
            this.Name = "未知指令";
            this.Window = null;
            this.Parameters = "";
            this.With = null;
            this.Ends = -1;
        }
        static public Dictionary<string, Dictionary<int, CommandType>> AdditionalGroups = new Dictionary<string, Dictionary<int, CommandType>>();
        static public Dictionary<int, CommandType> TargetGroup(string group = "")
        {
            Dictionary<int, CommandType> ans;
            if (AdditionalGroups.TryGetValue(group, out ans)) return ans;
            ans = new Dictionary<int, CommandType>();
            AdditionalGroups.Add(group, ans);
            return ans;
        }
        static public CommandType TryGetCommandType(int index,string group = "")
        {
            CommandType command;
            return TargetGroup(group).TryGetValue(index, out command) ? command : null;
        }
        public void AddToCategory(string group = "")
        {
            var Commands = TargetGroup(group);
            if (Commands.ContainsKey(this.Code)) Commands[this.Code] = this;
            else Commands.Add(this.Code, this);
            GenerateParameters();
        }
        public CommandType(int code, int follow, string command, string name, DataEditor.Help.Parameter.Text text, string parameters, DataEditor.Contract.Runable window, DataEditor.Contract.Runable with, int ends)
        {
            this.Code = code;
            this.Follow = follow;
            this.Command = command;
            this.Name = name;
            this.Text = text;
            this.Window = window;
            this.With = with;
            this.Ends = ends;
            this.Parameters = parameters;
        }
        public FuzzyObject GetParameter(char type, int index = -1, string option = "")
        {
            if (type >= 'A' && type <= 'Z') type = (char)(type + 32);
            switch (type)
            {
                case ParameterIntSign:
                    int i = 0;
                    if (int.TryParse(option, out i)) return new FuzzyFixnum(i);
                    else return new FuzzyFixnum(0);
                case ParameterStringSign:
                    return new FuzzyString(option);
                case ParameterTextSign:
                    if (index >= 0) text_position = index;
                    return new FuzzyString(option);
                case ParameterTextFollowSign:
                    if (index >= 0) follow_position = index;
                    return new FuzzyString(option);
                case ParameterFloatSign:
                    double d = 0;
                    if (double.TryParse(option, out d)) return new FuzzyFloat(d);
                    else return new FuzzyFloat(0);
                case ParameterBoolSign:
                    bool b = false;
                    FuzzyBool Bool = new FuzzyBool();
                    if (bool.TryParse(option, out b)) Bool.Value = b;
                    return Bool;
                case ParameterAudioSign:
                    return DataModel.Audio.GetAudio(option);
                case ParameterTuneSign:
                    return new FuzzyTone().FromString(option);
                case ParameterColorSign:
                    return new FuzzyColor().FromString(option);
                case ParameterUndetermindSign:
                    if (index > 0) undetermined_position = index;
                    return null;
                case ParameterArraySign:
                    return new FuzzyArray(GenerateParameters(option).OfType<object>());
                case ParameterMoveRouteSign:
                    return MoveRoute.CreateRoute();
                case ParameterMoveCommandSign:
                    return new MoveCommand(CommandType.TryGetCommandType(0, "Move")).Link;
            }
            return null;
        }
        public List<FuzzyObject> GetParameters()
        {
            if (this.Code < 0) return null;
            if (model != null)
                return model.Select(f => f == null ? null : f.Clone() as FuzzyObject).ToList();
            else return GenerateParameters();
        }

        public List<FuzzyObject> GenerateParameters(string source = null)
        {
            var answer = new List<FuzzyObject>();
            var reg = new System.Text.RegularExpressions.Regex("([A-Za-z])({(.+?)}){0,1}");
            var matches = reg.Matches(source ?? this.Parameters);
            for (var i = 0; i < matches.Count; i++)
            {
                var match = matches[i];
                var type = match.Groups.Count >= 2
                    ? Convert.ToChar(match.Groups[1].ToString())
                    : Convert.ToChar(match.Groups[0].ToString());
                answer.Add(match.Groups.Count >= 2
                    ? GetParameter(type, i, match.Groups[3].ToString())
                    : GetParameter(type, i));
            }
            if (source == null) model = answer;
            return answer;
        }

        public string Model
        {
            set { model = GenerateParameters(value); }
        }

        protected CommandGroup group = null;
        public CommandGroup Group
        {
            get { return group; }
            set { group = value; if (group != null) group.AddCommand(this); }
        }
        protected System.Drawing.Color? color = null;
        /// <summary>
        /// 这一指令的显示颜色
        /// </summary>
        public System.Drawing.Color Color
        {
            get { return color ?? (group != null ? group.Color : System.Drawing.Color.Black); }
            set { color = value; }
        }

        int text_position = -1, follow_position = -1, undetermined_position = -1;
        /// <summary>
        /// 若其中含有多行文字，则此参数指出 t 的位置。
        /// 否则，值为 -1.
        /// </summary>
        public int TextPosition { get { return text_position ; } }
        public int TextFollowPosition { get { return follow_position; } }
        public int UnderterminedPosition { get { return undetermined_position; } }
        /// <summary>
        /// 此指令是否是一个子指令？（Code > 400）
        /// </summary>
        public bool isChildCommand { get { return this.Follow > 0; } }
        /// <summary>
        /// 此指令是否是一个父指令？
        /// </summary>
        public Contract.Runable IsStartProc = null; // Fucking 301 Fix
        public bool isStartCommand { get { return this.Ends >= 0 && this.Ends != this.Follow; } }
        /// <summary>
        /// 此指令是否需要延长？
        /// </summary>
        public bool isProlongCommand { get { return this.Ends == this.Follow; } }
        /// <summary>
        /// 此指令是否一个文字指令？
        /// </summary>
        protected bool? is_text_command = null;
        public bool isTextCommand { get { return is_text_command ?? TextPosition >= 0; } set{ is_text_command = value; } }
        /// <summary>
        /// 指令的文字长度
        /// </summary>
        float? name_length = null;
        public float? NameLength { get { return name_length; } }
        public float GenerateNameLength(System.Drawing.Graphics g, System.Drawing.Font font)
        {
            name_length = g.MeasureString(this.Name, font).Width;
            return name_length.Value;
        }
        /// <summary>
        /// 父指令的文字长度
        /// </summary>
        float? parent_name_length = null;
        public float? ParentNameLength { get { return parent_name_length; } }
        public float GetParentTextCommandLength(System.Drawing.Graphics g, System.Drawing.Font font)
        {
            var command = TryGetCommandType(this.Follow);
            if (command == null) return (parent_name_length = 0).Value;
            return (parent_name_length = command.NameLength ?? command.GenerateNameLength(g, font)).Value;
        }
        /// <summary>
        /// 根据 Window 参数，生成一个窗口。
        /// </summary>
        /// <returns></returns>
        public Control.WrapBaseWindow GenerateWindow(Command command = null, List<Command> with = null)
        {
            var window = new Window.WindowWithOK.WrapWindowWithOK<Window.WindowWithOK>();
            if (command != null) window.Tag = command;
            if (Window == null) return window;
            var ans = Window.call(window, with) as Control.WrapBaseWindow;
            if (ans == null) return null;
            if (command != null) window.Tag = command;
            ans.Text = this.Name;
            return ans;
        }
    }
}