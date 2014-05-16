using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class ListBuff : ListWrapper<FuzzyData.FuzzyComplex>
    {
        public override string Flag { get { return "bufflist"; } }
        public override void Push()
        {
            List<string> keys = Control.TargetText;
            foreach (var obj in value.AllValues)
                if (obj is FuzzyData.FuzzyArray)
                    (obj as FuzzyData.FuzzyArray).Clear();
            string key;
            for (int i = 0; i < Control.Items.Count; i++)
            {
                int _value = i < Control.Value.Count ? Control.Value[i] : Control.DefaultValue;
                key = Control.TargetText[_value];
                var target = value[key] as FuzzyData.FuzzyArray;
                if (target == null) continue;
                target.Add(new FuzzyData.FuzzyFixnum(catalogue.Link.Reverse[i]));
            }
        }
        public override void Pull()
        {
            Control.Value.Clear();
            List<string> keys = Control.TargetText;
            for (int i = 0; i < keys.Count; i++)
            {
                var key = keys[i];
                FuzzyData.FuzzyArray _value = value[key] as FuzzyData.FuzzyArray;
                if (_value == null) continue;
                foreach (var obj in _value)
                {
                    FuzzyData.FuzzyFixnum j = obj as FuzzyData.FuzzyFixnum;
                    if (j != null)
                    {
                        int k = catalogue.Link.Verse[(int)j.Value];
                        while (Control.Value.Count <= k + 1) Control.Value.Add(Control.DefaultValue);
                        Control.Value[k] = i;
                    }
                }
            }
            Control.Invalidate();
        }
        public override void Reset()
        {
            var text = argument.GetArgument<Help.Parameter.Text>("textbook");
            var file = argument.GetArgument<FuzzyData.FuzzyArray>("data");
            var actual = argument.GetArgument<FuzzyData.FuzzySymbolComplex>("actual");
            var colors = argument.GetArgument<List<object>>("colors");
            var _default = argument.GetArgument<string>("default");

            if (text != null) catalogue = new Help.Catalogue(Control.Items, text);
            if (file != null) catalogue.InitializeText(file);

            Control.TargetText.Clear();
            foreach (var obj in actual.Extra.Keys) Control.TargetText.Add(obj.ToString());
            Control.TargetText.Add(_default);
            Control.DefaultValue = Control.TargetText.Count - 1;
            foreach (var color in colors)
                if (color is System.Drawing.Color)
                    Control.TargetColor.Add((System.Drawing.Color)color);
            OriginReset();
            
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.OverrideArgument("value", null);
            argument.OverrideArgument("choices", null);
        }
        public override void Bind()
        {
            base.Bind();
            Control.ItemCircled += Control_ItemCircled;
        }

        void Control_ItemCircled(object sender, Prototype.ProtoCircleListValueChangeEventArgs e)
        {
            NowTaint = true;
        }
    }
}
