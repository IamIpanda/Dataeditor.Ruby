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
}
