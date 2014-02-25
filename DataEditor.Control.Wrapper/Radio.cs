﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class Radio : Control.WrapControlValueContainer<FuzzyData.FuzzyFixnum, Prototype.ProtoRadioContainer>
    {
        public override string Flag { get { return "radio"; } }
        // ======= STATIC Parts ====f===
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
            if (value != null && value.Value == radio_key) Control.Radio.Checked = true;
            else Control.Radio.Checked = false;
        }
        public override FuzzyData.FuzzyObject Parent
        {
            get { return base.Parent; }
            set
            {
                base.Parent = value;
                var ison = argument.GetArgument<Contract.Runable>("ison");
                if (ison != null)
                {
                    bool on = (bool)ison.call(value, parent, radio_key);
                    if (on) Control.Radio.Checked = true;
                    base.Pull();
                }
            }
        }

        public override bool ValueIsChanged()
        {
            return false;
        }
        public override void Bind()
        {
            base.Bind();
            Control.Radio.CheckedChanged += OnRadiosChanged;
            Control.Radio.CheckedChanged += Radio_CheckedChanged;
        }

        void Radio_CheckedChanged(object sender, EventArgs e)
        {
            if (Control.Radio.Checked)
            {
                if (Control.Enabled && Control.Focused)
                {
                    var chosen = argument.GetArgument<Contract.Runable>("accept");
                    if (chosen != null)
                        chosen.call(value, parent, radio_key);
                }
            }
            else
            {
                var chosen = argument.GetArgument<Contract.Runable>("deny");
                if (chosen != null)
                    chosen.call(value, parent, radio_key);
            }
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
            argument.OverrideArgument("actual", null, Help.Parameter.ArgumentType.Option);
            argument.SetArgument("key", -1,Help.Parameter.ArgumentType.Option);
            argument.SetArgument("group", "ungrouped", Help.Parameter.ArgumentType.Option);
            argument.SetArgument("ison", null, Help.Parameter.ArgumentType.Option);
            argument.SetArgument("deny", null, Help.Parameter.ArgumentType.Option);
            argument.SetArgument("accept", null, Help.Parameter.ArgumentType.Option);
        }
    }
}
