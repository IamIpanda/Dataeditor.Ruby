using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class ListCheck : Control.WrapControlEditor<FuzzyData.FuzzyTable, System.Windows.Forms.CheckedListBox>
    {
        Help.Catalogue catalogue;
        public override string Flag { get { return "checklist"; } }
        public override void Push()
        {
            FuzzyData.FuzzyTable table = new FuzzyData.FuzzyTable(Control.SelectedIndices.Count);
            for (int i = 0; i < table.xsize; i++)
                table[i] = (short)catalogue.Link.Reverse[Control.SelectedIndices[i]];
        }

        public override void Pull()
        {
            for (int i = 0; i < value.xsize; i++)
                Control.SetItemChecked(catalogue.Link.Verse[i], true);
        }

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
        }

        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("data", new FuzzyData.FuzzyArray());
            argument.SetArgument("textbook", null);
        }
    }
}
