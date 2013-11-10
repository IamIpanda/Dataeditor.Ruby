using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DataEditor.FuzzyData
{
    public partial class FuzzyRect : FuzzyObject
    {
        Rectangle value;
        int X, Y, w, h;
        public FuzzyRect(Rectangle value)
        {
            this.value = value;
            GetParam();
        }
        public FuzzyRect(int x, int y, int width, int height)
        {
            set(x, y, width, height);
        }
        private void GetRect()
        {
            value = new Rectangle(X, Y, w, h);
        }
        private void GetParam()
        {
            X = value.X;
            Y = value.Y;
            w = value.Width;
            h = value.Height;
        }
        public void set(int x,int y,int w,int h)
        {
            this.X = x;
            this.Y = y;
            this.w = w;
            this.h = h;
            GetRect();
        }
        public int x      { get { return X; } set { X = value; GetRect(); } }
        public int y      { get { return Y; } set { Y = value; GetRect(); } }
        public int width  { get { return w; } set { w = value; GetRect(); } }
        public int height { get { return h; } set { h = value; GetRect(); } }
        public Rectangle Value { get { return value; } set { this.value = value; GetParam(); } }
    }
}
