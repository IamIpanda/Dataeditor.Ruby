using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DataEditor.Control.Wrapper
{
    public class ListText : WrapControlEditor<FuzzyData.FuzzyTable, Prototype.ProtoCircleTextListBox>
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
        List<int> Link = new List<int>(); // 正向：从控件中的顺序记录值到实际的值
        Help.Catalogue catalogue;
        public override string Flag { get { return "textlist"; } }
        public override void Push()
        {
            for (int i = 0; i < value.xsize && i < Control.Items.Count; i++)
                value[i] = (short)Link[Control.Value[i]];
        }
        public override void Pull()
        {
            // FIXME : SAFE
            Control.Value.Clear();
            for (int i = 0; i < value.xsize && i < Control.Items.Count; i++)
                Control.Value.Add(Link.IndexOf(value[i]));
        }

        public override bool CheckValue()
        {
            return false;
        }

        public override void Reset()
        {
            base.Reset();
            var file = argument.GetArgument<FuzzyData.FuzzyArray>("FILE");
            Help.Parameter.Text text = argument.GetArgument<Help.Parameter.Text>("SHOW");
            if (text != null) catalogue = new Help.Catalogue(Control.Items, text);
            if (file != null) catalogue.InitializeText(file);
            var value = argument.GetArgument<List<object>>("VALUE");
            var choices = argument.GetArgument<List<object>>("CHOICE");
            var colors = argument.GetArgument<List<object>>("COLOR");
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
            argument.Defaults.Add("CHOICE", new List<object>());
            argument.Defaults.Add("VALUE", new List<object>());
            argument.Defaults.Add("COLOR", DefaultColors);
            argument.Defaults.Add("DEFAULT", 0);
            argument.Defaults.Add("FILE", "");
            argument.Defaults.Add("SHOW", null);
        }
    }
}
