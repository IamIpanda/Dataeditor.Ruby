using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Help
{
    public class Measurement
    {
        public static Measurement Instance { get; set; }
        static Measurement() { Instance = new Measurement(); }
        private Measurement() { }
        protected Dictionary<string, int> width = new Dictionary<string, int>();
        protected Dictionary<string, int> height = new Dictionary<string, int>();
        public System.Drawing.Size this[string Index]
        {
            get
            {
                System.Drawing.Size size;
                Get(Index, out size);
                return size;
            }
            set { Set(Index, value.Width, value.Height); }
        }
        public bool Get(string Index, out System.Drawing.Size size)
        {
            size = new System.Drawing.Size();
            bool Success;
            int w = 0, h = 0;
            Success = width.TryGetValue(Index, out w);
            Success &= height.TryGetValue(Index, out h);
            if (Success)
                size = new System.Drawing.Size(w, h);
            return Success;
        }
        public void Set(string Index, int Width, int Height)
        {
            if (width.ContainsKey(Index))
            {
                width[Index] = Width;
                height[Index] = Height;
            }
            else
            {
                width.Add(Index, Width);
                height.Add(Index, Height);
            }
        }
    }
}
