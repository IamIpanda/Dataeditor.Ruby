using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DataEditor.Control.Wrapper
{
    public class Image : WrapControlEditor<FuzzyData.FuzzyComplex,Prototype.ProtoImageBackgroundDisplayer>
    {

        public class SplitManager
        {
            public Dictionary<string, Help.Parameter.Split> Main { get; set; }
            public SplitManager() 
            {
                Main = new Dictionary<string, Help.Parameter.Split>();
             }
            
            public Help.Parameter.Split SearchSplit(ref string name)
            {
                Help.Parameter.Split ans = null;
                foreach (var split in Main)
                    if (split.Key == "") ans = split.Value;
                    else if (name.StartsWith(split.Key))
                    {
                        name = name.Remove(0, split.Key.Length);
                        return split.Value;
                    }
                return ans;
            }
        }

        public override string Flag { get { return "image"; } }
        public SplitManager Split { get; set; }
        public SplitManager Show { get; set; }

        public override void Bind()
        {
            base.Bind();
            Control.FullBackgroundDraw = true;
            Control.Bitmap = new Bitmap(1,1);
            Control.DoubleClick += Control_DoubleClick;
            Split = new SplitManager();
            Show = new SplitManager();
        }

        public override void Push() { /* 已弃用 */ }

        public override void Pull()
        {
            Invalidate();
        }

        public override bool CheckValue()
        {
            return false;
        }
        public override void Reset()
        {
            var splits = argument.GetArgument<Dictionary<object, object>>("SPLIT");
            var shows = argument.GetArgument<Dictionary<object, object>>("SHOW");
            foreach (var pair in splits)
                if (pair.Value is Help.Parameter.Split)
                    Split.Main.Add(pair.Key.ToString(), pair.Value as Help.Parameter.Split);
            foreach (var pair in shows)
                if (pair.Value is Help.Parameter.Split)
                    Show.Main.Add(pair.Key.ToString(), pair.Value as Help.Parameter.Split);
            base.Reset();
        }
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.Defaults.Add("PATH", "Graphics/");
            argument.Defaults.Add("SPLIT", new Dictionary<object, object>());
            argument.Defaults.Add("SHOW", new Dictionary<object, object>());
        }
        protected void Invalidate()
        {
            var file_name = Value["name"] as FuzzyData.FuzzyString;
            var file_index = Value["index"] as FuzzyData.FuzzyFixnum;
            var file_hue = Value["hue"] as FuzzyData.FuzzyFixnum;
            string path = argument.GetArgument<string>("PATH");
            if (file_name == null) return;
            string string_file_name = System.IO.Path.Combine(path, file_name.Text);
            string file = "";
            Help.Path.Instance.SearchFile(string_file_name, out file, "project", "rtp");
            if (file == "") return;
            Bitmap bitmap = new Bitmap(file);
            Help.Parameter.Split show = Split.SearchSplit(ref string_file_name);
            Help.Parameter.Split image = Show.SearchSplit(ref string_file_name);
            var src_rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            if (file_index != null)
            {
                int index = Convert.ToInt32(file_index.Value);
                var first_clip = image[index, bitmap.Width, bitmap.Height];
                src_rect = image[-1, -1, first_clip.Width, first_clip.Height];
                src_rect.Offset(first_clip.X, first_clip.Y);
            }
            // TODO : Finish Bitmap Hue Change
            if (file_hue != null) { }
            Control.Bitmap = bitmap;
            Control.SrcRect = src_rect;
            Control.Invalidate();
        }
        void Control_DoubleClick(object sender, EventArgs e)
        {
            Image_Choser choser = new Image_Choser();
            choser.Split = Split;
            choser.Show = Show;
            choser.Path = argument.GetArgument<string>("PATH");
            choser.ShowDialog();
        }
    }
}
