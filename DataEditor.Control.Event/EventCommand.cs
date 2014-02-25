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
        public FuzzyData.FuzzyArray GetParameter()
        {
            FuzzyData.FuzzyArray ans = new FuzzyData.FuzzyArray();
            foreach (char t in Parameters)
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
                    case 'u': ans.Add(null); break; // user define
                }
            }
            return ans;
        }
        static public List<FuzzyData.FuzzyObject> SperateText(string Text, EventCommand Follow, int FollowIndex = 0,
            FuzzyData.FuzzySymbol sub_code = null, FuzzyData.FuzzySymbol sub_parameters = null, FuzzyData.FuzzySymbol sub_class_name = null)
        {
            var ans = new List<FuzzyData.FuzzyObject>();
            string[] texts = Text.Split('\n');
            FuzzyData.FuzzyObject obj;
            foreach (string text in texts)
            {
                obj = new FuzzyData.FuzzyObject();;
                if (sub_class_name != null) obj.ClassName = sub_class_name;
                if (sub_code != null) obj.InstanceVariables.Add(sub_code, new FuzzyData.FuzzyFixnum(Follow.Code));
                FuzzyData.FuzzyArray arr = Follow.GetParameter();
                arr[FollowIndex] = new FuzzyData.FuzzyString(text);
                if (sub_parameters != null) obj.InstanceVariables.Add(sub_parameters, arr);
            }
            return ans;
        }
    }
}
