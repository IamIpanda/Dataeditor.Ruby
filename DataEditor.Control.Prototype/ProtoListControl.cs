using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public interface ProtoListControl
    { 
    }
    public static class ProtoListControlHelp
    {
        static public List<Color> DefaultBackColors = new List<Color> { Color.White };
        static public List<Color> DefaultFocusColors = new List<Color> { Color.BlueViolet };
        static public Color DefaultFocusBorderColor = Color.FromArgb(0, 100, 200);
        static public Color DefaultForeColorOnFocus = Color.White;
        static ProtoListControlHelp()
        {
            LoadSettings();
        }
        static public void LoadSettings()
        {
            try
            {
                /*
                List<object> list = DataEditor.Help.Option.GetOption(typeof(ProtoListControl)) as List<object>;
                if (list == null)
                    return;
                List<Color> BackColors = list[0] as List<Color>;
                if (BackColors != null)
                    DefaultBackColors = BackColors;
                if (DefaultBackColors.Count == 0)
                    DefaultBackColors.Add(ListBox.DefaultBackColor);
                if (list.Count <= 1) { return; }
                List<Color> FocusColors = list[1] as List<Color>;
                if (FocusColors != null)
                    DefaultFocusColors = FocusColors;
                if (list.Count <= 2) { return; }
                DefaultFocusBorderColor = (Color)list[2];
                if (list.Count <= 3) { return; }
                DefaultForeColorOnFocus = (Color)list[3];
                 */
                DefaultBackColors = Help.Painter.Instance.Now.ListBackground;
                DefaultFocusColors = Help.Painter.Instance.Now.FocusColor;
                DefaultFocusBorderColor = Help.Painter.Instance[7];
                DefaultForeColorOnFocus = Help.Painter.Instance[6];
            }
            catch { }
        }
        static public void SaveSettings()
        {
            List<object> lb = new List<object>();
            lb.Add(DefaultBackColors);
            lb.Add(DefaultFocusColors);
            lb.Add(DefaultFocusBorderColor);
            lb.Add(DefaultForeColorOnFocus);
            DataEditor.Help.Option.SetOption(typeof(ProtoListControl), lb);
        }
        static public Brush GetFocusBrush(Rectangle? rect,Color DefaultColor)
        {
            switch (DefaultFocusColors.Count)
            {
                case 0:
                    return GetBrush(DefaultColor);
                case 1:
                    return GetBrush(DefaultFocusColors[0]);
                default:
                    if (rect.HasValue)
                        return new System.Drawing.Drawing2D.LinearGradientBrush
                            (rect.Value, DefaultFocusColors[0], DefaultFocusColors[1], 90F);
                    else
                        return GetBrush(DefaultFocusColors[0]);
            }
        }
        static public Color CheckEnabled(Color c,bool Enabled)
        {
            if (Enabled)
                return c;
            else
            {
                byte num = (byte)(c.R / 3 + c.G / 3 + c.B / 3);
                return Color.FromArgb(125, num, num, num);
            }
        }
        static public void DrawFocusRectangle(Graphics graphics, Rectangle rect)
        {
            ControlPaint.DrawBorder(graphics, rect,
                DefaultFocusBorderColor, 2, ButtonBorderStyle.Solid,
                DefaultFocusBorderColor, 2, ButtonBorderStyle.Solid,
                DefaultFocusBorderColor, 2, ButtonBorderStyle.Solid,
                DefaultFocusBorderColor, 2, ButtonBorderStyle.Solid);
        }
        static Dictionary<Color, SolidBrush> brushes = new Dictionary<Color,SolidBrush>();
        static public SolidBrush GetBrush(Color color)
        {
            SolidBrush brush = null;
            if (brushes.TryGetValue(color, out brush)) return brush;
            brush = new SolidBrush(color);
            brushes.Add(color, brush);
            return brush;
        }
    }
}
