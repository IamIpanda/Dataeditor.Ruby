using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DataEditor.Control.Wrapper
{
    public class Icon : WrapControlEditor<FuzzyData.FuzzyFixnum, Prototype.ProtoImageBackgroundDisplayer>
    {
        public bool NowTaint;
        protected Help.Parameter.Split split;
        protected Bitmap full;
        public override string Flag { get { return "icon"; } }
        public override void Push()
        {
            NowTaint = false;
        }

        public override void Pull()
        {
            if (full == null) return; 
            var rect = split[Convert.ToInt32(value.Value), full.Width, full.Height];
            Control.Bitmap = full;
            Control.SrcRect = rect;
        }

        public override bool ValueIsChanged()
        {
            return NowTaint;
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.SetArgument("split", null);
            argument.SetArgument("image", "");
            argument.SetArgument("version", "RPGVXAce", Help.Parameter.ArgumentType.Option);
        }
        public override void Reset()
        {
            split = argument.GetArgument<Help.Parameter.Split>("split");
            var image_path = argument.GetArgument<string>("image");
            var version = argument.GetArgument<string>("version");
            var full_path = "";
            Help.Path.Instance.SearchFile(image_path, out full_path, "project", version, "rtp");
            if (full_path == "") { EnableData = false; return; }
            full = new Bitmap(full_path);
            base.Reset();
        }
        public override void Bind()
        {
            base.Bind();
            Control.Scale = false;
            Control.FullBackgroundDraw = true;
            Control.ImageAlignCenter = true;
            Control.DoubleClick += Control_DoubleClick;
        }

        void Control_DoubleClick(object sender, EventArgs e)
        {
            if (full == null || split == null) return;
            var window = new Icon_Choser();
            window.set(full, split);
            window.Value = Convert.ToInt32(value.Value);
            if (window.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                value.Value = window.Value;
                NowTaint = true;
            }
        }
    }
}
