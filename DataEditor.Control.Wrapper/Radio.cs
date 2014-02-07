using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class Radio : Control.WrapControlValueContainer<FuzzyData.FuzzyFixnum, Prototype.ProtoRadioContainer>
    {
        public override string Flag { get { return "radio"; } }
        // ======= STATIC Parts =======
        static Dictionary<string, List<Radio>> Radios = new Dictionary<string, List<Radio>>();
        static void AddRadio(string group, Radio self)
        {
            if (!(Radios.ContainsKey(group))) Radios.Add(group, new List<Radio>());
            Radios[group].Add(self);
        }

        static void OnRadiosChanged(object sender, EventArgs e)
        {
            var son_radio = sender as System.Windows.Forms.RadioButton;
            if (son_radio == null || son_radio.Tag == null || !son_radio.Checked) return;
            string key = son_radio.Tag.ToString();
            List<Radio> target_list;
            if (!Radios.TryGetValue(key, out target_list)) return;
            foreach (var radio in target_list)
                if (radio.Control.Radio != son_radio)
                    radio.Control.Radio.Checked = false;
        }
        // ========================

        protected int radio_key = -1;
        public override void SetSize(System.Drawing.Size size)
        {
            if (size.Height < 18) size.Height = 18;
            Control.Size = new System.Drawing.Size(
                size.Width + Control.RadioWidth + Control.Margin.Horizontal, size.Height);
        }

        public override System.Windows.Forms.Control.ControlCollection Controls 
        { get { return Control.PanelCollection; } }

        public override void Push() 
        {
            if (Control.Radio.Checked) value.Value = radio_key;
        }
        public override void Pull()
        {
            base.Pull();
            if (value.Value == radio_key) Control.Radio.Checked = true;
            else Control.Radio.Checked = false;
        }

        public override bool ValueIsChanged()
        {
            return false;
        }
        public override void Bind()
        {
            base.Bind();
            Control.Radio.CheckedChanged += OnRadiosChanged;
        }
        public override void Reset()
        {
            string group = argument.GetArgument<string>("GROUP");
            AddRadio(group, this);
            radio_key = argument.GetArgument<int>("KEY");
            Control.Radio.Tag = group;
            Control.Radio.Text = argument.GetArgument<string>("TEXT");
            base.Reset();
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("key", -1);
            argument.SetArgument("group", "ungrouped", Help.Parameter.ArgumentType.Option);
        }
    }
}
