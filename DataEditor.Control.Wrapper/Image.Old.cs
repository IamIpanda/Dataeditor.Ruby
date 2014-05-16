using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    public class Image_Old : WrapControlEditor<FuzzyData.FuzzyComplex, Prototype.ProtoDropItem>
    {
        bool NowTaint;
        string path;
        public override string Flag { get { return "oldimage"; } }
        public override void Bind()
        {
            base.Bind();
            Control.ButtonClicked += Control_ButtonClicked;
        }

        void Control_ButtonClicked(object sender, EventArgs e)
        {
            var window = new Image_Choser();
            Image.SplitManager split = new Image.SplitManager();
            split.Main.Add("", new Help.Parameter.Split(Help.Parameter.Split.SplitType.Count, 1, Help.Parameter.Split.SplitType.Count, 1));
            window.Split = split;
            window.Show = split;
            window.Path = path;
            var name = value["name"] as FuzzyData.FuzzyString;
            var hue = value["hue"] as FuzzyData.FuzzyFixnum;
            window.FileName = name.Text;
            // TODO : Finish HUE.
            if (hue != null) { }
            if (window.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Control.Text = window.FileName;
                name.Text = window.FileName;
                if (hue != null) { }
                NowTaint = true;
            }
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("path", "Graphics/characters", Help.Parameter.ArgumentType.Option);
        }
        public override void Reset()
        {
            path = argument.GetArgument<string>("path");
            base.Reset();
        }
        public override void Pull()
        {
            Control.Text = (value["name"] as FuzzyData.FuzzyString).Text;
        }
        public override void Push()
        {
            NowTaint = true;
        }
        public override bool ValueIsChanged()
        {
            return NowTaint;
        }
    }
    public class Image_Window : Window.WrapAnyWindow<Image_Choser>
    {
        string path;
        public override string Flag { get { return "dialog_image"; } }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("path", "Graphics/characters", Help.Parameter.ArgumentType.Option);
        }
        public override void Pull()
        {
            var complex = value as FuzzyData.FuzzyComplex;
            if (complex == null) { return; }
            Image.SplitManager split = new Image.SplitManager();
            split.Main.Add("", new Help.Parameter.Split(Help.Parameter.Split.SplitType.Count, 1, Help.Parameter.Split.SplitType.Count, 1));
            Window.Split = split;
            Window.Show = split;
            Window.Path = path;
            var name = complex["name"] as FuzzyData.FuzzyString;
            var hue = complex["hue"] as FuzzyData.FuzzyFixnum;
            Window.FileName = name.Text;
            // TODO : Finish HUE.
            if (hue != null) { }
        }
        public override void Push()
        {
            base.Push();
            var complex = value as FuzzyData.FuzzyComplex;
            if (complex == null) { return; }
            var name = complex["name"] as FuzzyData.FuzzyString;
            var hue = complex["hue"] as FuzzyData.FuzzyFixnum;
            name.Text = Window.FileName;
            if (hue != null) { }
        }
        public override void Reset()
        {
            path = argument.GetArgument<string>("path");
            base.Reset();
        }
    }
}
