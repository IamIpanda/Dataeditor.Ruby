using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DataEditor.Control.Prototype
{
    public interface ProtoFrameControl
    {
    }
    public class ProtoFrameControlHelp
    {
        public static void DrawBorder(Graphics graphics, Rectangle clipRectangle, Color FrameColor)
        {
            ControlPaint.DrawBorder(graphics, clipRectangle,
                FrameColor, 1, ButtonBorderStyle.Solid,
                FrameColor, 1, ButtonBorderStyle.Solid,
                FrameColor, 1, ButtonBorderStyle.Solid,
                FrameColor, 1, ButtonBorderStyle.Solid);
        }
        public static void DrawFocusRectangle(Graphics graphics, Rectangle rectangle, int offset = 2)
        {
            ControlPaint.DrawFocusRectangle(graphics, new Rectangle(
                rectangle.X + offset, rectangle.Y + offset,
                rectangle.Width - 2 * offset, rectangle.Height - 2 * offset));
        }
    }
}
