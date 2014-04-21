using System;
using System.Collections.Generic;
using System.Text;

namespace DataEditor.Control.ShapeShifter
{
    public class Interpreter
    {
        public static int[] Inter(string str)
        {
            if (str.StartsWith("{") || str.StartsWith("[")) str = str.Substring(1);
            if (str.EndsWith("}") || str.EndsWith("]")) str = str.Substring(0, str.Length - 1);
            string[] components = str.Split(',');
            int[] ans = new int[components.Length];
            for(int i = 0; i < components.Length; i++)
                if (!(int.TryParse(components[i], out ans[i]))) ans[i] = -1;
            return ans;
        }
        public static FuzzyData.FuzzyColor GetColor(string str)
        {
            int[] component = Inter(str);
            if (component.Length < 3)
            {
                System.Windows.Forms.MessageBox.Show("转换到 Color 时发生了错误。", "输入格式不正确");
                return new FuzzyData.FuzzyColor(0, 0, 0, 0);
            }
            for( int i = 0 ; i < component.Length; i++)
                if (component[i] < 0) component[i] = 0;
                else if (component[i] > 255) component[i] = 255;
            if (str.Length >= 4)
                return new FuzzyData.FuzzyColor(component[0], component[1], component[2], component[3]);
            else
                return new FuzzyData.FuzzyColor(component[0], component[1], component[2]);
        }
        public static FuzzyData.FuzzyRect GetRect(string str)
        {
            int[] component = Inter(str);
            if (component.Length < 4)
            {
                System.Windows.Forms.MessageBox.Show("转换到 Rect 时发生了错误。", "输入格式不正确");
                return new FuzzyData.FuzzyRect(0, 0, 0, 0);
            }
            return new FuzzyData.FuzzyRect(component[0], component[1], component[2], component[3]);
        }
        public static FuzzyData.FuzzyTone GetTone(string str)
        {
            var color = GetColor(str);
            return new FuzzyData.FuzzyTone(color.red, color.green, color.blue, color.alpha);
        }
    }
}
