using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class ListCheck : Control.WrapControlEditor<FuzzyData.FuzzyArray, System.Windows.Forms.CheckedListBox>
    {
        bool NowTaint = false;
        Help.Catalogue catalogue;
        public override string Flag { get { return "checklist"; } }
        public override void Push()
        {
            NowTaint = false;
            value.Clear();
            for (int i = 0; i < Control.Items.Count; i++)
                if (Control.GetItemChecked(i))
                    value.Add(new FuzzyData.FuzzyFixnum(catalogue.Link.Reverse[i]));
        }

        public override void Pull()
        {
            for (int i = 0; i < Control.Items.Count; i++)
                Control.SetItemChecked(i, false);
            for (int i = 0; i < value.Count; i++)
            {
                var num = value[i] as FuzzyData.FuzzyFixnum;
                if (num != null)
                    Control.SetItemChecked(catalogue.Link.Verse[(int)num.Value], true);
            }

        }

        public override bool ValueIsChanged()
        {
            return NowTaint;
        }

        public override void Reset()
        {
            base.Reset();
            var file = argument.GetArgument<FuzzyData.FuzzyArray>("DATA");
            Help.Parameter.Text text = argument.GetArgument<Help.Parameter.Text>("TEXTBOOK");
            if (text != null) catalogue = new Help.Catalogue(Control.Items, text);
            if (file != null) catalogue.InitializeText(file);
        }

        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("data", new FuzzyData.FuzzyArray());
            argument.SetArgument("textbook", null);
            argument.OverrideArgument("height", 200, Help.Parameter.ArgumentType.Option);
        }
        public override void Bind()
        {
            base.Bind();
            Control.ItemCheck += Control_ItemCheck;
            Control.Leave += Control_Leave;
            Control.CheckOnClick = true;
        }

        void Control_Leave(object sender, EventArgs e)
        {
            Control.SelectedIndex = -1;
        }

        void Control_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
        {
            NowTaint = true;
        }

    }
}
