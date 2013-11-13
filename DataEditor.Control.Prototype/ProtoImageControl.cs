using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public interface ProtoImageControl
    {

    }
    public class ProtoImageControlHelp
    {
        static char[] SpecificChar = new char[] { '$', '%', '^', '&', '*' };
        public class ProtoImageSplit
        {
            Dictionary<char, SplitNumWidthManager> Width = new Dictionary<char,SplitNumWidthManager>();
            Dictionary<char, SplitNumHeightManager> Height = new Dictionary<char,SplitNumHeightManager>();
            public ProtoImageSplit(string str)
            {
                string[] parts = str.Split(',');
                if (parts.Length < 2)
                    throw new ArgumentOutOfRangeException("设置图形参数时，给出的参数少于 2 个。");
                string width = parts[0], height = parts[1];
                string[] widthParts = width.Split(SpecificChar),
                        heightParts = height.Split(SpecificChar);
                Width.Clear(); Height.Clear();
                int count = 1;
                this.Width.Add('\0', new SplitNumWidthManager(widthParts[0]));
                for (; count < widthParts.Length && count < SpecificChar.Length; count++)
                    this.Width.Add(SpecificChar[count - 1], new SplitNumWidthManager(widthParts[count]));
                count = 1;
                this.Height.Add('\0', new SplitNumHeightManager(heightParts[0])); 
                for (; count < heightParts.Length && count < SpecificChar.Length; count++)
                    this.Height.Add(SpecificChar[count - 1], new SplitNumHeightManager(heightParts[count]));
            }
            public Size this[Bitmap bit, string name]
            {
                get
                {
                    int width = GetManager(this.Width, name)[bit];
                    int height = GetManager(this.Height, name)[bit];
                    return new Size(width, height);
                }
            }
            public Rectangle this[Bitmap bit, string name, int Index]
            {
                get 
                {
                    if (bit == null) return default(Rectangle);
                    Size size = this[bit, name];
                    int wCount = bit.Width / size.Width;
                    int wIndex = Index % wCount;
                    int hIndex = Index / wCount;
                    return new Rectangle(wIndex * size.Width, hIndex * size.Height, size.Width, size.Height);
                }
            }
            SplitNumManager GetManager(Dictionary<char, SplitNumWidthManager> sn, string name)
            {
                char c = name[0];
                foreach (char key in sn.Keys)
                    if (key == c)
                        return sn[c];
                return sn['\0'];
            }
            SplitNumManager GetManager(Dictionary<char, SplitNumHeightManager> sn, string name)
            {
                char c = name[0];
                foreach (char key in sn.Keys)
                    if (key == c)
                        return sn[c];
                return sn['\0'];
            }
        }
        public enum SplitType { Pixel, Block };
        public abstract class SplitNumManager
        {
            protected int num;
            protected SplitType type;
            public SplitNumManager(string num)
            {
                num = num.Trim();
                if (num.StartsWith("@"))
                {
                    type = SplitType.Pixel;
                    num = num.Remove(0, 1);
                }
                else
                    type = SplitType.Block;
                if (!(int.TryParse(num, out this.num)))
                    this.num = (type == SplitType.Block ? 4 : 24);
            }
            abstract public int this[Image image] { get; }
        }
        public class SplitNumWidthManager : SplitNumManager
        {
            public SplitNumWidthManager(string num) : base(num) { }
            public override int this[Image image]
            {
                get 
                {
                    if (image == null) return 0;
                    return (type == SplitType.Pixel ? num : image.Width / num); }
            }
        }
        public class SplitNumHeightManager : SplitNumManager
        {
            public SplitNumHeightManager(string num) : base(num) { }
            public override int this[Image image]
            {
                get {
                    if (image == null) return 0;
                    return (type == SplitType.Pixel ? num : image.Height / num); }
            }
        }
    }
}
