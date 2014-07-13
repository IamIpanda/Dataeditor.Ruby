using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Wrapper
{
    /// <summary>
    /// 此控件的复用率为 0.
    /// 约定之数据如下。
    /// enemy_id 
    /// 敌人 ID。
    /// x 
    /// 脚下的 X 座标。
    /// y 
    /// 脚下的 Y 座标。
    /// hidden 
    /// 选项「中途出现」的真伪值。
    /// immortal 
    /// 选项「不死之身」的真伪值。
    /// </summary>
    public class TroopMember : Control.WrapControlEditor<FuzzyData.FuzzyArray,Prototype.ProtoTroopMember>
    {
        public override string Flag { get { return "troop_member"; } }
        Help.Catalogue catalogue;
        FuzzyData.FuzzySymbol sub_enemy_id = FuzzyData.FuzzySymbol.GetSymbol("@enemy_id"),
                                              sub_x = FuzzyData.FuzzySymbol.GetSymbol("@x"),
                                              sub_y = FuzzyData.FuzzySymbol.GetSymbol("@y"),
                                              sub_hidden = FuzzyData.FuzzySymbol.GetSymbol("@hidden"),
                                              sub_immortal = FuzzyData.FuzzySymbol.GetSymbol("@immortal"),
                                              image = FuzzyData.FuzzySymbol.GetSymbol("@battler_name"),
                                              image_hue = FuzzyData.FuzzySymbol.GetSymbol("@battler_hue"),
                                              class_name = FuzzyData.FuzzySymbol.GetSymbol("RPG::Troop::Member"),
                                              name;
        FuzzyData.FuzzyArray data = null;
        string path = "Graphics\\Battlers", background = "Graphics\\Battlebacks", backgroundname = "041-EvilCastle01";
        protected override void SetDefaultArgument()
        {
            base.SetDefaultArgument();
            argument.OverrideArgument("text", "@name", Help.Parameter.ArgumentType.Option);
            argument.OverrideArgument("label", 0);
            argument.SetArgument("textbook", null);
            argument.SetArgument("data", null);
        }
        public override void Reset()
        {
            var text = argument.GetArgument<string>("text");
            if (!(text.StartsWith("@"))) text = "@" + text;
            name = FuzzyData.FuzzySymbol.GetSymbol(text);
            data = argument.GetArgument<FuzzyData.FuzzyArray>("data");
            var textbook = argument.GetArgument<Help.Parameter.Text>("textbook");
            catalogue = new Help.Catalogue(Control.Items, textbook);
            catalogue.InitializeText(data);
            base.Reset();
        }
        public override void Pull()
        {
            bitmaps.Clear();
            var name_str = parent[name] as FuzzyData.FuzzyString;
            if (name_str != null) Control.Text = name_str.Text;
            var ids = new List<int>();
            var points =  new List<System.Drawing.Point>();
            var hiddens = new List<bool>();
            var immortals = new List<bool>();
            int enemy_id, x, y;
            bool hidden, immortal;
            foreach (var obj in value)
            {
                FuzzyData.FuzzyObject fobj = obj as FuzzyData.FuzzyObject;
                enemy_id = Convert.ToInt32((fobj[sub_enemy_id] as FuzzyData.FuzzyFixnum).Value);
                x = Convert.ToInt32((fobj[sub_x] as FuzzyData.FuzzyFixnum).Value);
                y = Convert.ToInt32((fobj[sub_y] as FuzzyData.FuzzyFixnum).Value);
                hidden = (fobj[sub_hidden] as FuzzyData.FuzzyBool).Value;
                immortal = (fobj[sub_hidden] as FuzzyData.FuzzyBool).Value;
                ids.Add(catalogue.Link.Verse[enemy_id]);
                points.Add(new System.Drawing.Point(x, y));
                hiddens.Add(hidden);
                immortals.Add(immortal);
            }
            Control.Flash = hiddens;
            Control.Undead = immortals;
            Control.Set(ids, points);
            SetBackGround();
        }
        public override void Push()
        {
            value.Clear();
            var list = Control.Indecis;
            FuzzyData.FuzzyObject now;
            System.Drawing.Point pos;
            int enemy_id;
            bool immaortal, hidden;
            for (int i = 0; i < Control.Indecis.Count; i++ )
            {
                enemy_id = catalogue.Link.Reverse[Control.Indecis[i]];
                pos = Control.Main.Coodinates[i];
                immaortal = Control.Undead[i];
                hidden = Control.Flash[i];
                now = new FuzzyData.FuzzyObject();
                now.ClassName = class_name;
                now[sub_enemy_id] = new FuzzyData.FuzzyFixnum(enemy_id);
                now[sub_hidden] = hidden ? FuzzyData.FuzzyBool.True : FuzzyData.FuzzyBool.False;
                now[sub_immortal] = immaortal ? FuzzyData.FuzzyBool.True : FuzzyData.FuzzyBool.False;
                now[sub_x] = new FuzzyData.FuzzyFixnum(pos.X);
                now[sub_y] = new FuzzyData.FuzzyFixnum(pos.Y);
                value.Add(now);
            }
        }
        public override bool ValueIsChanged()
        {
            return true;
        }
        Dictionary<int, System.Drawing.Bitmap> bitmaps = new Dictionary<int, System.Drawing.Bitmap>();
        protected System.Drawing.Bitmap bitmap(int j)
        {
            if (bitmaps.ContainsKey(j)) return bitmaps[j];
            if (catalogue == null) return null;
            int i = catalogue.Link.Reverse[j];
            FuzzyData.FuzzyObject target = data[i] as FuzzyData.FuzzyObject;
            if (target == null || image == null) return null;
            FuzzyData.FuzzyString name = target[image] as FuzzyData.FuzzyString;
            if (name == null) return null;
            string filename = System.IO.Path.Combine(path, name.Text);
            string fullname = "";
            if (Help.Path.Instance.SearchFile(filename, out fullname, "rtp", "project") == false) return null;
            var bitmap = new System.Drawing.Bitmap(fullname);
            bitmaps.Add(j, bitmap);
            return bitmap;
        }
        public override void Bind()
        {
            base.Bind();
            Control.BackgroundClicked += Control_BackgroundClicked;
            Control.BattleTestClicked += Control_BattleTestClicked;
            Control.NameClicked += Control_NameClicked;
            Control.Bitmaps = this.bitmap;
            
        }

        void Control_NameClicked(object sender, EventArgs e)
        {
            var index = Control.Indecis;
            var components = new List<String>();
            var grouped = new Dictionary<int, int>();
            foreach (int i in index)
                if (grouped.ContainsKey(i))
                    grouped[i]++;
                else grouped.Add(i, 1);
            foreach (int i in grouped.Keys)
            {
                var obj = data[catalogue.Link.Reverse[i]] as FuzzyData.FuzzyObject;
                int count = grouped[i];
                components.Add((obj[name] as FuzzyData.FuzzyString).Text + (count > 1 ? "*" + count.ToString() : ""));
            }
            Control.Text = string.Join(", ", components.ToArray());
            (parent[name] as FuzzyData.FuzzyString).Text = Control.Text;
        }

        void Control_BattleTestClicked(object sender, EventArgs e)
        {
        }
        
        void Control_BackgroundClicked(object sender, EventArgs e)
        {
            Image_Choser window = new Image_Choser();
            var manager = new Image.SplitManager();
            manager.Main.Add("", new Help.Parameter.Split(Help.Parameter.Split.SplitType.Count, 1, Help.Parameter.Split.SplitType.Count, 1));
            window.Path = background;
            window.Split = window.Show = manager;
            window.FileName = backgroundname;
            if (window.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                backgroundname = window.FileName;
                SetBackGround();
            }
        }

        void SetBackGround()
        {
            string filename = System.IO.Path.Combine(background, backgroundname);
            string fullname;
            if (Help.Path.Instance.SearchFile(filename, out fullname, "rtp", "project") == false)
                Control.Main.Background = new System.Drawing.Bitmap(Control.Main.Size.Width, Control.Main.Size.Height);
            else
                Control.Main.Background = new System.Drawing.Bitmap(fullname);
        }
    }
}
