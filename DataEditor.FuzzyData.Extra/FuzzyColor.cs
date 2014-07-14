using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using DataEditor.FuzzyData.Serialization;

namespace DataEditor.FuzzyData
{
    public partial class FuzzyColor : FuzzyObject
    {
        Color value;
        int r, g, b, a;
        public FuzzyColor(Color value)
        {
            this.value = value;
            GetParam();
            this.ClassName = FuzzySymbol.GetSymbol("Color");
        }
        public FuzzyColor(int red, int green, int blue, int alpha = 255)
        {
            set(red, green, blue, alpha);
            this.ClassName = FuzzySymbol.GetSymbol("Color");
        }
        private void GetColor()
        {
            value = Color.FromArgb(a, r, g, b);
        }
        private void GetParam()
        {
            r = value.R;
            g = value.G;
            b = value.B;
            a = value.A;
        }
        public void set(int red, int green, int blue, int alpha = 255)
        {
            r = red;
            g = green;
            b = blue;
            a = alpha;
            GetColor();
        }
        public int red   { get { return r; } set { r = value; GetColor(); } }
        public int green { get { return g; } set { g = value; GetColor(); } }
        public int blue  { get { return b; } set { b = value; GetColor(); } }
        public int alpha { get { return a; } set { a = value; GetColor(); } }
        public Color Value { get { return value; } set { this.value = value; GetParam(); } }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("{");
            sb.Append(red);
            sb.Append(", ");
            sb.Append(green);
            sb.Append(", ");
            sb.Append(blue);
            sb.Append(", ");
            sb.Append(alpha);
            sb.Append("}");
            return sb.ToString();
        }
        public override void Clone(FuzzyObject source)
        {
            FuzzyColor from = source as FuzzyColor;
            if (from == null) return;
            this.r = from.r;
            this.g = from.g;
            this.b = from.b;
            this.a = from.a;
            base.Clone(source);
        }
    }
}
