using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DataEditor.Help
{
    public class Filter
    {
        public class ToneFilter
        {
            public int temp_result_r = 0, temp_result_g = 0, temp_result_b = 0, temp_result_a = 255;
            int tone_r = 0, tone_g = 0, tone_b = 0, tone_a = 0, grayfull;
            public void ChangeTone(Color c)
            {
                temp_result_a = c.A;
                ChangeTone(temp_result_r, temp_result_g, temp_result_b);
            }
            public void ChangeTone(int r, int g, int b)
            {
                grayfull = (r * 38 + g * 75 + b * 15) >> 7;
                temp_result_r = (int)(tone_r + r + (grayfull - r) * tone_a / 256F);
                temp_result_g = (int)(tone_g + g + (grayfull - g) * tone_a / 256F);
                temp_result_b = (int)(tone_b + b + (grayfull - b) * tone_a / 256F);
                if (temp_result_r > 255) temp_result_r = 255;
                if (temp_result_g > 255) temp_result_g = 255;
                if (temp_result_b > 255) temp_result_b = 255;
                if (temp_result_r < 0) temp_result_r = 0;
                if (temp_result_g < 0) temp_result_g = 0;
                if (temp_result_b < 0) temp_result_b = 0;
            }
            public ToneFilter(int r, int g, int b, int a)
            {
                tone_r = r;
                tone_g = g;
                tone_b = b;
                tone_a = a;
            }
            public int ToneRed { get { return tone_r; } set { tone_r = value; } }
            public int ToneGreen { get { return tone_g; } set { tone_g = value; } }
            public int ToneBlue { get { return tone_b; } set { tone_b = value; } }
            public int ToneGray { get { return tone_a; } set { tone_a = value; } }
        }
        public static class HSVtoRGBConverter
        {
            static public float temp_result_r, temp_result_g, temp_result_b;
            static float f, p, q, t;
            static int hi;
            /// <summary>
            /// 将 HSV/HSB 变换为 RGB 的计算。
            /// </summary>
            /// <param name="h">色相值（Hue）。0-360之间，以角度计数的角度值。</param>
            /// <param name="s">饱和度（Saturation）。0-100之间的百分比。</param>
            /// <param name="v">深度（Value）。0-100之间的百分比。又称饱和度（Brightness）</param>
            static public void ConvertToRGB(float h, float s, float v)
            {
                hi = (int)(h / 60F) % 6;
                f = h / 60 - hi;
                p = v * (1 - s);
                q = v * (1 - f * s);
                t = v * (1 - (1 - f) * s);
                switch(hi)
                {
                    case 0:
                        temp_result_r = v;
                        temp_result_g = t;
                        temp_result_b = p;
                        break;
                    case 1:
                        temp_result_r = q;
                        temp_result_g = v;
                        temp_result_b = p;
                        break;
                    case 2:
                        temp_result_r = p;
                        temp_result_g = v;
                        temp_result_b = t;
                        break;
                    case 3:
                        temp_result_r = p;
                        temp_result_g = q;
                        temp_result_b = v;
                        break;
                    case 4:
                        temp_result_r = t;
                        temp_result_g = p;
                        temp_result_b = v;
                        break;
                    case 5:
                        temp_result_r = v;
                        temp_result_g = p;
                        temp_result_b = q;
                        break;
                }
                temp_result_r *= 255;
                temp_result_g *= 255;
                temp_result_b *= 255;
            }
        }
        public static class HSLtoRGBConverter
        {
            static public float temp_result_r, temp_result_g, temp_result_b;
            static float hue2rgb(float p, float q, float t)
            {
                if (t < 0) t += 1;
                if (t > 1) t -= 1;
                if (t < 1 / 6F) return p + (q - p) * 6 * t;
                if (t < 1 / 2F) return q;
                if (t < 2 / 3F) return p + (q - p) * (2 / 3F - t) * 6;
                return p;
            }
            /// <summary>
            /// 将 HSL 变换到 RGB 的计算。
            /// </summary>
            /// <param name="h">色相值（Hue）。0-360之间，以角度计数的角度值。</param>
            /// <param name="s">饱和度（Saturation）。0-100之间的百分比。</param>
            /// <param name="l">亮度（Brightness）。0-100之间的百分比。</param>
            static public void ConvertToRGB(float h, float s, float l)
            {
                h /= 360F;
                var q = l < 0.5 ? l * (1 + s) : l + s - l * s;
                var p = 2 * l - q;
                temp_result_r = hue2rgb(p, q, h + 1 / 3F);
                temp_result_g = hue2rgb(p, q, h);
                temp_result_b = hue2rgb(p, q, h - 1 / 3F);
                temp_result_r *= 255;
                temp_result_g *= 255;
                temp_result_b *= 255;
            }
        }
    }
}
