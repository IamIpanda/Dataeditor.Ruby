using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class Paper : WrapControlEditor<FuzzyData.FuzzyArray, Prototype.ProtoLinedPaper>
    {
        public override string Flag { get { return "paper"; } }
        protected Help.LinkTable<int, int> link = new Help.LinkTable<int, int>();
        public override void Pull()
        {
            // Link 做成
            int  j = 0;
            // List 做成
            List<string> text = new List<string>();
            for(int i = 1; i < value.Count; i++)
            {
                var str = value[i] as FuzzyData.FuzzyString;
                if (str != null)
                {
                    link.Add(i, j);
                    text.Add(str.Text);
                    j++;
                }
            }
            Help.Parameter.Text textbook = argument.GetArgument<Help.Parameter.Text>("textbook");
            Control.textbook = textbook;
            Control.Value = text;
        }
        public override void Push()
        {

        }
        public override bool ValueIsChanged()
        {
            return false;
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("textbook", null);
        }
    }
}
