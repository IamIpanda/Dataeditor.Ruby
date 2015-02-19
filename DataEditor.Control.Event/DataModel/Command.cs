using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataEditor.FuzzyData;

namespace DataEditor.Control.Event.DataModel
{
    public class Command
    {
        public const string FocusSign = "■";
        public const string ChildSign = "·";
        static public FuzzyData.FuzzySymbol ClassName = FuzzyData.FuzzySymbol.GetSymbol("RPG::EventCommand");
        static public FuzzyData.FuzzySymbol CodeSymbol = FuzzyData.FuzzySymbol.GetSymbol("@code");
        static public FuzzyData.FuzzySymbol IndentSymbol = FuzzyData.FuzzySymbol.GetSymbol("@indent");
        static public FuzzyData.FuzzySymbol ParametersSymbol = FuzzyData.FuzzySymbol.GetSymbol("@parameters");

        public List<FuzzyObject> Parameters { get; set; }
        public CommandType Type { get; set; }
        public int Indent { get; set; }
        public int Code { get { return this.Type == null ? -2 : this.Type.Code; } }
        public FuzzyData.FuzzyObject Link { get; set; }

        public T GetParameter<T>(int index)
        {
            if (Parameters == null) return default(T);
            if (index < 0 || index >= Parameters.Count) return default(T);
            object answer = Parameters[index];
            if (answer is T) return (T)answer;
            return default(T);
        }
        public Command(CommandType Origin, int indent = 0) 
        {
            this.Type = Origin;
            this.Indent = indent;
            this.Parameters = this.Type.GetParameters();
            this.Link = new FuzzyObject();
            isChecked = true;
            Link.ClassName = ClassName;
            Link.InstanceVariables.Add(CodeSymbol, new FuzzyFixnum(this.Code));
            Link.InstanceVariables.Add(IndentSymbol, new FuzzyFixnum(this.Indent));
            Link.InstanceVariables.Add(ParametersSymbol, new FuzzyArray(Parameters.ConvertAll<object>((x) => x)));
        }
        public Command(FuzzyData.FuzzyObject command, int indent = 0)
        {
            object obj;
            FuzzyData.FuzzyFixnum fuzzy_code = null, fuzzy_indent = null;
            FuzzyData.FuzzyArray fuzzy_parameter = null;
            Link = command;
            fuzzy_code = command.InstanceVariables.TryGetValue(CodeSymbol, out obj) ? obj as FuzzyData.FuzzyFixnum : null;
            fuzzy_indent = command.InstanceVariables.TryGetValue(IndentSymbol, out obj) ? obj as FuzzyData.FuzzyFixnum : null;
            fuzzy_parameter = command.InstanceVariables.TryGetValue(ParametersSymbol, out obj) ? obj as FuzzyData.FuzzyArray : null;
            if (fuzzy_code != null) this.Type = CommandType.TryGetCommandType(Convert.ToInt32(fuzzy_code.Value));
            if (fuzzy_indent != null) this.Indent = Convert.ToInt32(fuzzy_indent.Value);
            if (fuzzy_parameter != null)
                this.Parameters = new List<FuzzyObject>(fuzzy_parameter.ConvertAll((o) => o as FuzzyObject));
            this.Indent = indent;
        }
        #region 带缓存的 ToString 机制
        string StringVersion = null;
        public override string ToString()
        {
            return StringVersion ?? GenerateString();
        }
        public virtual string GenerateString()
        {
            if (Type == null) return "Unknown Command";
            if (Type.Text == null) return Type.Name;
            if (Code == 0) return "";
            List<FuzzyObject>[] target = new List<FuzzyObject>[2] { Parameters, new List<FuzzyObject>() };
            return StringVersion = Type.Text.ToString(target);
        }
        #endregion

        public WrapBaseWindow GenerateWindow(List<Command> With)
        {
            if (Type == null) return null;
            WrapBaseWindow window = this.Type.GenerateWindow(this, With);
            if (window == null) return null;
            // 倘若并未为窗口赋值，那么才进行赋值。
            if (window.Value == null)
                window.Parent = CheckWith(With);
            window.Tag = this;
            return window;
        }

        public FuzzyArray CheckWith(List<Command> With)
        {
            FuzzyArray ans = new FuzzyArray(this.Parameters.ConvertAll(x => x == null ? null : x.Clone()));
            if (With == null) return ans;
            if (this.Type.TextPosition < 0) return ans;
            var fstr = this.GetParameter<FuzzyString>(this.Type.TextPosition);
            var sb = new StringBuilder();
            if (fstr != null) sb.Append(fstr.Text);
            foreach (var command in With)
                if (command.Type.TextFollowPosition >= 0)
                    sb.Append(Environment.NewLine + command.GetParameter<FuzzyString>(command.Type.TextFollowPosition).Text);
            var target = ans[this.Type.TextPosition] as FuzzyString;
            if (target == null) ans[Type.TextPosition] = new FuzzyString(sb.ToString());
            else target.Text = sb.ToString();
            return ans;
        }

        Stack<int> pos_station = null;
        public void SetUndetermined(FuzzyArray target, IEnumerable<FuzzyObject> Follows)
        {
            // 若不是可变长的，则拒绝之。
            if (this.Type.UnderterminedPosition < 0) return;
            // 若从未使用过堆栈，那么设栈。
            if (pos_station == null)
            {
                pos_station = new Stack<int>();
                pos_station.Push(this.Type.UnderterminedPosition);
            }
            int index;
            // 若其中包含空元素，则将之作为位置并入栈。
            if (target.Contains(null))
            {
                index = target.LastIndexOf(null);
                if (pos_station.Peek() < index) pos_station.Push(index);
            }
            // 反之，选择堆栈顶作为位置。
            else index = pos_station.Peek();
            // 移除后面的参数，并以参数重置之。
            target.RemoveRange(index, target.Count - index);
            target.AddRange(Follows);
        }

        public void SetUndetermined(FuzzyArray target, string type_defination)
        {
            List<FuzzyObject> fobj = new List<FuzzyObject>();
            foreach(char c in type_defination)
                fobj.Add(this.Type.GetParameter(c));
            SetUndetermined(target, fobj);
        }
        public void RevokeUndetermined(FuzzyArray target)
        {
            if (pos_station == null) return;
            int top = pos_station.Pop();
            target.RemoveRange(top, Parameters.Count - top + 1);
            target.Add(null);
        }

        #region 将所作的更改反射到 FuzzyObject 架构中
        protected bool isChecked = false;
        public virtual void SyncToLink()
        {
            if (!isChecked) CheckStructure();
            var FuzzyIndent = Link.InstanceVariables[IndentSymbol] as FuzzyFixnum;
            var FuzzyParameter = Link.InstanceVariables[ParametersSymbol] as FuzzyArray;
            FuzzyIndent.Value = Indent;
            FuzzyParameter.Clear();
            Parameters.ForEach((x) => FuzzyParameter.Add(x));
        }

        public FuzzyArray FuzzyParameters
        {
            set
            {
                this.Parameters.Clear();
                this.Parameters.AddRange(value.OfType<FuzzyObject>());
            }
        }

        public virtual void CheckStructure()
        {
            if (!(Link.InstanceVariables.ContainsKey(IndentSymbol)))
                Link.InstanceVariables.Add(IndentSymbol, new FuzzyFixnum(0));
            if (!(Link.InstanceVariables.ContainsKey(ParametersSymbol)))
                Link.InstanceVariables.Add(ParametersSymbol, new FuzzyArray());
            if (!(Link.InstanceVariables[IndentSymbol] is FuzzyFixnum))
                Link.InstanceVariables[IndentSymbol] = new FuzzyFixnum(0);
            if (!(Link.InstanceVariables[ParametersSymbol] is FuzzyArray))
                Link.InstanceVariables[ParametersSymbol] = new FuzzyArray();
            isChecked = true;
        }
        #endregion

        #region Fucking 301 Fix

        public bool isStartCommand
        {
            get { return Type.IsStartProc == null ? Type.isStartCommand : (bool)Type.IsStartProc.call(this.Parameters); }
        }
        #endregion
    }
}
