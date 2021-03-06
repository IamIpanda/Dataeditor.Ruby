﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class Audio : WrapControlEditor<FuzzyData.FuzzyObject, Prototype.ProtoDropItem>
    {
        public override string Flag { get { return "audio"; } }
        bool NowTaint = false;
        string type = "";
        FuzzyData.FuzzyFixnum pitch;
        FuzzyData.FuzzyFixnum volume;
        FuzzyData.FuzzyString name;
        public const string volume_name = "@volume";
        public const string pitch_name = "@pitch";
        public const string file_name = "@name";

        public Audio()
        {
            _default = new FuzzyData.FuzzyObject();
            _default[volume_name] = new FuzzyData.FuzzyFixnum();
            _default[pitch_name] = new FuzzyData.FuzzyFixnum();
            _default[file_name] = new FuzzyData.FuzzyString();
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("type", "SE");
        }
        public override void Reset()
        {
            type = argument.GetArgument<string>("type");
            base.Reset();

        }
        public override void Pull()
        {
            volume = value[volume_name] as FuzzyData.FuzzyFixnum;
            pitch = value[pitch_name] as FuzzyData.FuzzyFixnum;
            name = value[file_name] as FuzzyData.FuzzyString;
            Control.Text = name.Text;
            if (Control.Text == "") Control.Text = "（无）";
        }
        public override void Push()
        {
            /* Van!shment */
        }
        public override bool ValueIsChanged()
        {
            return NowTaint;
        }
        public override void Bind()
        {
            base.Bind();
            Control.ButtonClicked += Control_ButtonClicked;
        }

        void Control_ButtonClicked(object sender, EventArgs e)
        {
            // Audio  Choser 设计上比较偷懒，因此这里的写法有所不同
            var window = new AudioChoser();
            window.Path = "Audio/" + type;
            window.Volume = Convert.ToInt32(volume.Value);
            window.Freq = Convert.ToInt32(pitch.Value);
            window.FileName = name.Text;
            
            if (window.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                volume.Value = window.Volume;
                pitch.Value = window.Freq;
                name.Text = window.FileName;
                NowTaint = true;
                Pull();
            }
        }
    }
    public class Audio_Window : Window.WrapAnyWindow<AudioChoser>
    {
        public Audio_Window()
        {
            _default = new FuzzyData.FuzzyObject();
            _default[Audio.volume_name] = new FuzzyData.FuzzyFixnum();
            _default[Audio.pitch_name] = new FuzzyData.FuzzyFixnum();
            _default[Audio.file_name] = new FuzzyData.FuzzyString();
        }
        public override string Flag { get { return "dialog_audio"; } }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("type", "SE");
        }
        public override void Reset()
        {
            base.Reset();
            string Type = argument.GetArgument<string>("type");
            Window.Path = "Audio/" + Type;
        }
        public override void Pull()
        {
            if (value == null) return;
            Window.Volume = Convert.ToInt32((value["@volume"] as FuzzyData.FuzzyFixnum).Value);
            Window.Freq = Convert.ToInt32((value["@pitch"] as FuzzyData.FuzzyFixnum).Value);
            Window.FileName = (value["@name"] as FuzzyData.FuzzyString).Text;
            base.Pull();
        }
        public override void Push()
        {
            (value["@volume"] as FuzzyData.FuzzyFixnum).Value = Window.Volume;
            (value["@pitch"] as FuzzyData.FuzzyFixnum).Value = Window.Freq;
            (value["@name"] as FuzzyData.FuzzyString).Text = Window.FileName;
        }
    }
}
