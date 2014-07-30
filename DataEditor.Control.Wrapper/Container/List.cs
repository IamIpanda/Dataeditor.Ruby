using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper.Container
{
    public class List : WrapControlContainer<Prototype.ProtoFullListBox>
    {
        FuzzyData.FuzzyArray target_array;
        FuzzyData.FuzzyObject default_value = null;
        public override string Flag { get { return "list"; } }
        public string ClipboardFormat { get; set; }
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
            Control.Dock = System.Windows.Forms.DockStyle.Fill;
            target_array = base.Value as FuzzyData.FuzzyArray;
            if (target_array == null) return;
            catalogue.InitializeText(target_array.list);
            if (Control.Items.Count <= 0) return;
            Control.SelectedIndex = 0;
            Help.Log.log("正在载入 " + Control.Text + " 的值");
        }

        public override void Reset()
        {
            var text = argument.GetArgument<Help.Parameter.Text>("textbook");
            var filter = argument.GetArgument<Contract.Runable>("filter");
            catalogue = new Help.Catalogue(Control.Items, text, filter);
            Control.Text = argument.GetArgument<string>("text");
            this.ClipboardFormat = argument.GetArgument<string>("clipboardformat");
            default_value = argument.GetArgument<FuzzyData.FuzzyObject>("default");
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("textbook", new Help.Parameter.Text("卖萌的阿尔西斯"));
            argument.SetArgument("filter", null, Help.Parameter.ArgumentType.Option);
            argument.OverrideArgument("text", "未明位面", Help.Parameter.ArgumentType.Option);
            argument.SetArgument("clipboardformat", "Arce" + this.GetHashCode().ToString(), Help.Parameter.ArgumentType.Option);
            argument.SetArgument("default", "", Help.Parameter.ArgumentType.Option);
        }
        protected Help.Catalogue catalogue = null;

        protected override FuzzyData.FuzzyObject GetBaseValue()
        {
            return Value;
        }
        public override void Bind()
        {
            base.Bind();
            Control.SelectedIndexChanged += Control_SelectedIndexChanged;
            Control.AddMenu("复制", CopyItemClicked, System.Windows.Forms.Keys.C | System.Windows.Forms.Keys.Control);
            Control.AddMenu("粘贴", PasteItemClicked,System.Windows.Forms.Keys.V | System.Windows.Forms.Keys.Control);
            Control.AddMenu("清除", ClearItemClicked, System.Windows.Forms.Keys.Delete);
        
        }
        public override System.Windows.Forms.Control.ControlCollection Controls
        {
            get
            {
                return Control.InnerControls;
            }
        }
        public override void SetSize(System.Drawing.Size size)
        {
            base.SetSize(new System.Drawing.Size(Control.DeltaWidth + size.Width, size.Height));
        }

        void Control_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.Pull();
        }
        public override int start_x { get { return 3; } }
        public override int start_y { get { return 0; } }
        public override int end_x { get { return 3; } }
        public override int end_y { get { return 3; } }
        public override void Putt()
        {
            var index = Control.SelectedIndex;
            var obj = Value;
            if (obj == null) return;
            var state = Help.Taint.Instance[obj];
            if (state == Contract.TaintState.UnTainted) return;
            var color = Help.Taint.DefaultColor(state);
            var target = Control.ForeColors;
            if (target.ContainsKey(index))
                target[index] = color;
            else target.Add(index, color);
        }
        void CopyItemClicked(object sender, EventArgs e)
        {
            var value = this.Value;
            if (value == null) return;
            var target = Help.Serialization.TrySetValue(this.Value, "[m]");
            System.Windows.Forms.DataObject db = new System.Windows.Forms.DataObject(ClipboardFormat, target);
            System.Windows.Forms.Clipboard.SetDataObject(db, true, 5, 100);
        }
        void PasteItemClicked(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Clipboard.ContainsData(ClipboardFormat) == false) return;
            var db = System.Windows.Forms.Clipboard.GetDataObject();
            var str = (byte[])db.GetData(ClipboardFormat);
            var target = Help.Serialization.TryGetValue(str, "[m]") as FuzzyData.FuzzyObject;
            if (target != null && this.Value != null)
            {
                var temp = this.Value & target;
                base.Pull();
                
            }
        }
        void ClearItemClicked(object sender, EventArgs e)
        {
            if (default_value != null)
            {
                var temp = this.Value & default_value;
                base.Pull();
            }
        }
        
    }
}