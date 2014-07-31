using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DataEditor.Help
{
    [Serializable]
    public class Palette
    {
        private Color?[] Colors = new Color?[30];
        /// <summary>
        /// 根据索引，返回目标颜色值。
        /// 根据索引具有下述的含义：
        /// 1、容器控件底色
        /// 2、被选中控件的底色
        /// 3、被选中控件的字色
        /// 4、列表框的焦点色 1
        /// 5、列表框的焦点色 2
        /// 6、列表框的焦点字色
        /// 7、列表框的描边色
        /// 8-10、列表框的底色
        /// 11、图像框的底色
        /// 12-15、图像的底色
        /// 16-25、虹色。根据不同的颜色，来指定：
        /// RTP 的默认颜色
        /// TextListbox 的默认颜色
        /// 26-30、其他自定义颜色
        /// </summary>
        /// <param name="index">索引值</param>
        /// <returns>目标颜色</returns>
        public Color? this[int index]
        {
            get { return Colors[index]; }
            set { Colors[index] = value; }
        }
        public List<Color> ColorRange(int start, int end)
        {
            List<Color> ans = new List<Color>();
            for (int i = start; i <= end; i++)
                if (Colors[i].HasValue) ans.Add(Colors[i].Value);
            return ans;
        }
        public List<Color> ListBackground { get { return ColorRange(8, 10); } }
        public List<Color> ImageBackground { get { return ColorRange(12, 15); } }
        public List<Color> RainbowColor { get { return ColorRange(16, 25); } }
        public List<Color> FocusColor { get { return ColorRange(4, 5); } }
    }

    [Serializable]
    public class Painter
    {
        private Painter() 
        {
            palette = new Dictionary<string, Help.Palette>();
            now = new Palette();
            palette.Add("Arce", now);
            now[1] = Color.FromArgb(255, 206, 222, 243);
            now[2] = Color.FromArgb(255, 0, 98, 196);
            now[3] = Color.FromArgb(255, 254, 214, 146);
            now[4] = Color.FromArgb(255, 0, 100, 200);
            now[5] = Color.FromArgb(255, 0, 158, 247);
            now[6] = Color.FromArgb(255, 255, 255, 255);
            now[7] = Color.FromArgb(255, 0, 100, 200);
            now[8] = Color.FromArgb(255, 228, 236, 242);
            now[9] = Color.FromArgb(255, 255, 255, 255);
            now[11] = Color.FromArgb(255, 255, 255, 255);
            now[12] = Color.FromArgb(255, 0, 0, 128);
            now[13] = Color.FromArgb(255, 0, 0, 65);
            now[16] = Color.FromArgb(255, 183, 40, 143);
            now[17] = Color.FromArgb(255, 174, 46, 78);
            now[18] = Color.FromArgb(255, 185, 102, 37);
            now[19] = Color.FromArgb(255, 197, 164, 27);
            now[20] = Color.FromArgb(255, 109, 193, 7);
            now[21] = Color.FromArgb(255, 59, 146, 34);
            now[22] = Color.FromArgb(255, 62, 192, 91);
            now[23] = Color.FromArgb(255, 62, 102, 224);
            now[24] = Color.FromArgb(255, 68, 158, 221);
            now[25] = Color.FromArgb(255, 173, 54, 166);
            
        }
        public static Painter Instance { get; set; }
        static Painter() 
        {
            var painter = Option.GetOption(typeof(Painter)) as Painter;
            Instance = painter ?? new Painter();
            if (painter == null) Option.SetOption(typeof(Painter), Instance);
        }

        static public void Save()
        {
            Option.SetOption(Instance, Instance);
        }

        Palette now;
        Dictionary<string, Palette> palette;
        public Dictionary<string, Palette> Palette { get { return palette; } }
        public Palette Now 
        {
            get { return now; } 
            set { now = value; if (PaletteChanged != null) PaletteChanged(this, new EventArgs()); } 
        }
        public event EventHandler PaletteChanged;

        public Color this[int index]
        {
            get { return now[index] ?? default(Color); }
        }
        Color? Fore = null, Back = null;
        public void PushColor(System.Windows.Forms.Control Control)
        {
            if (now[2].HasValue)
            {
                Back = Control.BackColor;
                Control.BackColor = now[2].Value;
            }
            if (now[3].HasValue)
            {
                Fore = Control.ForeColor;
                Control.ForeColor = now[3].Value;
            }
        }
        public void PopColor(System.Windows.Forms.Control Control)
        {
            if (Fore != null)
            {
                Control.ForeColor = Fore.Value;
                Fore = null;
            }
            if (Back != null)
            {
                Control.BackColor = Back.Value;
                Back = null;
            }
        }
    }
}
