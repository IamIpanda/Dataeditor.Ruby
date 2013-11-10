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
        }
        public FuzzyColor(int red, int green, int blue, int alpha = 255)
        {
            set(red, green, blue, alpha);
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
    }
}
