using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DataEditor.Control.Wrapper
{
    public abstract class ListWrapper<T> : Control.WrapControlEditor<T, Prototype.ProtoCircleTextListBox> where T : FuzzyData.FuzzyObject
    {
        static List<object> DefaultColors = new List<object>()
        {
            Color.DarkRed,
            Color.Orange,
            Color.Gray,
            Color.DarkGreen,
            Color.Blue,
            Color.Purple
        };
        protected List<int> Link = new List<int>(); // 正向：从控件中的顺序记录值到实际的值
        protected Help.Catalogue catalogue;
        public override string Flag { get { return "textlist"; } }
        public override bool CheckValue()
        {
            return false;
        }

        public override void Reset()
        {
            base.Reset();
            var file = argument.GetArgument<FuzzyData.FuzzyArray>("DATA");
            Help.Parameter.Text text = argument.GetArgument<Help.Parameter.Text>("TEXTBOOK");
            if (text != null) catalogue = new Help.Catalogue(Control.Items, text);
            if (file != null) catalogue.InitializeText(file);
            var value = argument.GetArgument<List<object>>("VALUE");
            var choices = argument.GetArgument<List<object>>("CHOICES");
            var colors = argument.GetArgument<List<object>>("COLORS");
            Control.TargetColor.Clear();
            Control.TargetText.Clear();
            Link.Clear();
            foreach (var _value in value)
                if (_value is int)
                    Link.Add(Convert.ToInt32(_value));
            foreach (var choice in choices)
                Control.TargetText.Add(choice.ToString());
            foreach (var color in colors)
                if (color is Color)
                    Control.TargetColor.Add((Color)color);
        }

        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("textbook", new List<object>());
            argument.SetArgument("value", new List<object>());
            argument.SetArgument("data", new FuzzyData.FuzzyArray());
            argument.SetArgument("colors", DefaultColors, Help.Parameter.ArgumentType.Option);
            argument.SetArgument("default", 0, Help.Parameter.ArgumentType.Option);
            argument.SetArgument("show", null, Help.Parameter.ArgumentType.HardlyEver);
        }
    }
}
