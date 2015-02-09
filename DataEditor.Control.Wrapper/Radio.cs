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
        static public void AddRadio(string group, Radio self)
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
                    radio.TurnOffRadio();
                else radio.Push();
           
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
            if (value == null) return;
            if (Control.Radio.Checked && radio_key > 0) value.Value = radio_key;
        }
        public override void Pull()
        {
            if (value != null && value.Value == radio_key)
                Control.Radio.Checked = true; 
            else Control.Radio.Checked = false;
            base.Pull();
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
                    EnableData = DataState.Enable;
                    SetEnabled();
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
            Control.EnabledChanged += Control_EnabledChanged;
            Control.Disposed += Control_Disposed;
        }

        void Control_EnabledChanged(object sender, EventArgs e)
        {
            if (Control.Enabled == false) Control.Radio.Checked = false;
        }

        void Control_Disposed(object sender, EventArgs e)
        {
            Radios[Control.Radio.Tag.ToString()].Remove(this);
        }

        void Radio_CheckedChanged(object sender, EventArgs e)
        {
            if (Control.Radio.Checked)
            {
                if (Control.Enabled && Control.Radio.Focused)
                {
                    var chosen = argument.GetArgument<Contract.Runable>("accept");
                    if (chosen != null)
                        chosen.call(value, parent, radio_key);
                    base.Pull();
                }
            }
        }
        /// <summary>
        /// 设置为 Unchecked。
        /// 由于不能确定前态，CheckedChanged被抛弃了。
        /// Deny 在此处触发。
        /// </summary>
        void TurnOffRadio()
        {
            if (Control.Radio.Checked)
            {
                Control.Radio.Checked = false;
                var chosen = argument.GetArgument<Contract.Runable>("deny");
                if (chosen != null)
                    chosen.call(value, parent, radio_key);
            }
            Push();
        }
        public override void Reset()
        {
            string group = argument.GetArgument<string>("GROUP");
            AddRadio(group, this);
            radio_key = argument.GetArgument<int>("KEY");
            Control.Radio.Tag = group;
            Control.Radio.Text = argument.GetArgument<string>("TEXT");
            Control.RadioWidth = argument.GetArgument<int>("INNER_WIDTH");
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
            argument.SetArgument("inner_width", 75, Help.Parameter.ArgumentType.Option);
        }
    }
    public class SingleRadio : Control.WrapControlEditor<FuzzyData.FuzzyFixnum, System.Windows.Forms.RadioButton>
    {
        // ======= STATIC Parts =======
        static Dictionary<string, List<SingleRadio>> Radios
            = new Dictionary<string, List<SingleRadio>>();
        static public void AddRadio(string group, SingleRadio self)
        {
            if (!(Radios.ContainsKey(group))) Radios.Add(group, new List<SingleRadio>());
            Radios[group].Add(self);
        }
        static void OnRadiosChanged(object sender, EventArgs e)
        {
            var son_radio = sender as System.Windows.Forms.RadioButton;
            if (son_radio == null || son_radio.Tag == null || !son_radio.Checked) return;
            string key = (son_radio.Tag as SingleRadio).group_key;
            List<SingleRadio> target_list;
            if (!Radios.TryGetValue(key, out target_list)) return;
            foreach (var radio in target_list)
                if (radio.Control != son_radio)
                    radio.Control.Checked = false;
        }
        // ========================
        int radio_key = -1;
        string group_key = "";
        public override string Flag { get { return "single_radio"; } }


        public override void Push()
        {
            if (Control.Checked) value.Value = radio_key;
        }

        public override void Pull()
        {
            if (value.Value == radio_key) Control.Checked = true;
            else Control.Checked = false;
        }

        public override bool ValueIsChanged()
        {
            return false;
        }
        public override void Reset()
        {
            base.Reset();
            string group = argument.GetArgument<string>("GROUP");
            AddRadio(group, this);
            group_key = group;
            radio_key = argument.GetArgument<int>("KEY");
            Control.Text = argument.GetArgument<string>("TEXT");
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("key", 0, Help.Parameter.ArgumentType.Must);
            argument.SetArgument("group", "ungrouped", Help.Parameter.ArgumentType.Option);
            argument.OverrideArgument("label", 0, Help.Parameter.ArgumentType.Option);
            argument.OverrideArgument("width", 60, Help.Parameter.ArgumentType.Option);
        }
        public override void Bind()
        {
            base.Bind();
            Control.Disposed += Control_Disposed;
        }

        void Control_Disposed(object sender, EventArgs e)
        {
            Radios[group_key].Remove(this);
        }
    }
}
