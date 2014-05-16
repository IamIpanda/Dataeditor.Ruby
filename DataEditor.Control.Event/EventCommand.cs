using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataEditor.Control.Event
{
    public class EventCommand
    {
        public const string FocusSign = "★";
        public int Code = -1, UpIndent = 0, DownIndent = 0, Follow = -1;
        public string Command = "Untitled", Name = "未知指令", Parameters = "";
        public Help.Parameter.Text Text = null;
        public System.Drawing.Color Color = System.Drawing.Color.Black;
        // 内部新模板
        protected FuzzyData.FuzzyObject model = null;
        // 内部锁定位
        protected Stack<int> user_locates = new Stack<int>();
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
        public EventCommand() { }
        protected FuzzyData.FuzzyArray GetParameter()
        {
            return GetArrray(Parameters);
        }
        protected FuzzyData.FuzzyArray GetArrray(string str, bool check_u = false)
        {
            FuzzyData.FuzzyArray ans = new FuzzyData.FuzzyArray();
            int i = 0;
            foreach (char t in str)
            {
                switch (t)
                {
                    case 'i': ans.Add(new FuzzyData.FuzzyFixnum(0)); break; // int
                    case 't':
                    case 'f':
                    case 's': ans.Add(new FuzzyData.FuzzyString("")); break; // text or string or follow
                    case 'a': ans.Add(new FuzzyData.FuzzyArray()); break; // array
                    case 'b': ans.Add(FuzzyData.FuzzyBool.False); break; // bool
                    case 'd': // audio
                    case 'c': ans.Add(new FuzzyData.FuzzyColor(0, 0, 0)); break; // color
                    case 'o': ans.Add(new FuzzyData.FuzzyTone()); break; // tone
                    case 'u': ans.Add(null); if (check_u) user_locates.Push(i + user_locates.Peek()); break;
                        // user define
                }
                i++;
            }
            return ans;
        }
        public void ReUniform(FuzzyData.FuzzyArray arr, string str)
        {
            int user_location = user_locates.Peek();
            arr.RemoveRange(user_location, arr.Length - user_location);
            arr.AddRange(GetArrray(str, true));
        }
        public void ResetUniform()
        {
            while (user_locates.Count > 1) user_locates.Pop();
        }
        public void PopUniform()
        {
            if (user_locates.Count > 1)
                user_locates.Pop();
            else Help.Log.log("事件的计算中，出现了一个过度的 Pop");
        }
        public void CheckUniform()
        {
            int pos = Parameters.LastIndexOf('u');
            if (pos > 0) user_locates.Push(pos);
        }
        public FuzzyData.FuzzyArray Model
        {
            get 
            {
                if (model == null) return GetParameter();
                else return model.Clone() as FuzzyData.FuzzyArray;
            }
            set { model = value; }
        }
        public WrapBaseWindow ApplicateWindow(FuzzyData.FuzzyArray paras = null, IEnumerable<FuzzyData.FuzzyObject> with = null)
        {
            if (Window == null) return null;
            if (paras == null) paras = Model;
            DataEditor.Control.WrapBaseWindow window;
            window = new DataEditor.Control.Window.WindowWithOK.WrapWindowWithOK<DataEditor.Control.Window.WindowWithOK>();
            if (with != null) window.Tag = with;
            window = Window.call(window, paras) as WrapBaseWindow;
            if (window != null)
            {
                window.Parent = paras;
                window.Binding.Text = Name;
            }
            return window;
        }
        static public FuzzyData.FuzzySymbol ClassName = FuzzyData.FuzzySymbol.GetSymbol("RPG::Event::EventCommand");
        static public FuzzyData.FuzzySymbol CodeSymbol =FuzzyData.FuzzySymbol.GetSymbol("@code");
        static public FuzzyData.FuzzySymbol IndentSymbol = FuzzyData.FuzzySymbol.GetSymbol("@indent");
        static public FuzzyData.FuzzySymbol ParametersSymbol = FuzzyData.FuzzySymbol.GetSymbol("@parameters");
        public FuzzyData.FuzzyObject GetStruct(FuzzyData.FuzzyArray parameter = null)
        {
            FuzzyData.FuzzyObject ans = new FuzzyData.FuzzyObject();
            ans.ClassName = ClassName;
            ans.InstanceVariables.Add(CodeSymbol, new FuzzyData.FuzzyFixnum(Code));
            ans.InstanceVariables.Add(IndentSymbol, new FuzzyData.FuzzyFixnum(0));
            ans.InstanceVariables.Add(ParametersSymbol, parameter ?? GetParameter());
            return ans;
        }
        public List<FuzzyData.FuzzyObject> SperateText(FuzzyData.FuzzyString Text, EventCommand Follow, int FollowIndex = 0,
            FuzzyData.FuzzySymbol sub_code = null, FuzzyData.FuzzySymbol sub_parameters = null, FuzzyData.FuzzySymbol sub_class_name = null)
        {
            var ans = new List<FuzzyData.FuzzyObject>();
            string[] texts = Text.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            Text.Text = texts[0];
            FuzzyData.FuzzyObject obj;
            bool isFirst = true;
            foreach (string text in texts)
            {
                if (isFirst) { isFirst = false; continue; }
                obj = Follow.GetStruct();
                var arr = obj[ParametersSymbol] as FuzzyData.FuzzyArray;
                arr[FollowIndex] = new FuzzyData.FuzzyString(text);
                ans.Add(obj);
            }
            return ans;
        }
    }
}
