using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.Event.DataModel
{
    static class Extension
    {
        static public FuzzyData.FuzzyColor FromString(this FuzzyData.FuzzyColor that, string str)
        {
            string[] parts = str.Split(',');
            if (parts.Length != 3 & parts.Length != 4) return that;
            that.red = TransformToInt(parts[0].Trim());
            that.green = TransformToInt(parts[1].Trim());
            that.blue = TransformToInt(parts[2].Trim());
            if (parts.Length == 4)
                that.alpha = TransformToInt(parts[3].Trim());
            else that.alpha = 255;
            return that;
        }
        static public FuzzyData.FuzzyTone FromString(this FuzzyData.FuzzyTone that, string str)
        {
            string[] parts = str.Split(',');
            if (parts.Length != 3 & parts.Length != 4) return that;
            that.red = TransformToInt(parts[0].Trim());
            that.green = TransformToInt(parts[1].Trim());
            that.blue = TransformToInt(parts[2].Trim());
            if (parts.Length == 4)
                that.gray = TransformToInt(parts[3].Trim());
            else that.gray = 0;
            return that;
        }
        static int TransformToInt(string str)
        {
            int i = 0;
            if (int.TryParse(str, out i)) return i;
            return 0;
        }
    }

    
}
