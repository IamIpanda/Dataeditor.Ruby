using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper.Container
{
    public class List : WrapControlContainer<Prototype.ProtoFullListBox>
    {
        FuzzyData.FuzzyArray target_array;
        public override string Flag { get { return "list"; } }
        public override FuzzyData.FuzzyObject Value
        {
            get
            {
                if (value == null || catalogue == null) return null;
                int j = Control.SelectedIndex;
                if (j < 0) return null;
                int i = catalogue.Link.Reverse[j];
                return target_array[i] as FuzzyData.FuzzyObject;
            }
            set 
            {
                base.Value = value;
            }
        }
        public override void Pull()
        {
            target_array = base.Value as FuzzyData.FuzzyArray;
            if (target_array == null) return;
            catalogue.InitializeText(target_array.list);
        }

        public override void Reset()
        {
            var text = argument.GetArgument<Help.Parameter.Text>("TEXTBOOK");
            var filter = argument.GetArgument<Contract.Runable>("FILTER");
            catalogue = new Help.Catalogue(Control.Items, text, filter);
            Control.Dock = System.Windows.Forms.DockStyle.Fill;
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("textbook", new Help.Parameter.Text("卖萌的阿尔西斯"));
            argument.SetArgument("filter", null, Help.Parameter.ArgumentType.Option);
        }
        protected Help.Catalogue catalogue = null;
        public override void Bind()
        {
            base.Bind();
            Control.SelectedIndexChanged += Control_SelectedIndexChanged;
        }
        public override System.Windows.Forms.Control.ControlCollection Controls
        {
            get
            {
                return Control.InnerControls;
            }
        }

        void Control_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.Pull();
        }
    }
}
