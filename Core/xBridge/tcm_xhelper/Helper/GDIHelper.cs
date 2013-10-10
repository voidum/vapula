using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TCM.Helper
{
    public class GDIHelper
    {
        /// <summary>
        /// 获取加深的色彩
        /// </summary>
        public static Color GetDeepColor(Color origin, int delta = 15) 
        {
            int A = origin.A;
            int R = origin.R - delta;
            R = R < 0 ? 0 : R;
            int G = origin.G - delta;
            G = G < 0 ? 0 : G;
            int B = origin.B - delta;
            B = B < 0 ? 0 : B;
            return Color.FromArgb(A,R,G,B);
        }

        /// <summary>
        /// 已知色彩HSB获取RGB色彩
        /// </summary>
        public static Color GetColorByHSB(float H, float S, float B)
        {
            double r = 0;
            double g = 0;
            double b = 0;

            if (S == 0) r = g = b = B;
            else
            {
                // the color wheel consists of 6 sectors. 
                double sectorPos = H / 60.0;
                int sectorNumber = (int)(Math.Floor(sectorPos));

                // get the fractional part of the sector
                double fractionalSector = sectorPos - sectorNumber;
                
                // calculate values for the three axes of the color.
                double p = B * (1.0 - S);
                double q = B * (1.0 - (S * fractionalSector));
                double t = B * (1.0 - (S * (1 - fractionalSector)));

                // assign the fractional colors to r, g, and b based on the sector the angle is in.
                switch (sectorNumber)
                {
                    case 0:
                        r = B;
                        g = t;
                        b = p;
                        break;
                    case 1:
                        r = q;
                        g = B;
                        b = p;
                        break;
                    case 2:
                        r = p;
                        g = B;
                        b = t;
                        break;
                    case 3:
                        r = p;
                        g = q;
                        b = B;
                        break;
                    case 4:
                        r = t;
                        g = p;
                        b = B;
                        break;
                    case 5:
                        r = B;
                        g = p;
                        b = q;
                        break;
                }
            }

            r *= 255.0;
            g *= 255.0;
            b *= 255.0;
            int Red = (int)r;
            int Green = (int)g;
            int Blue = (int)b;
            if (Red < 0) Red = 0;
            if (Red > 255) Red = 255;
            if (Green < 0) Green = 0;
            if (Green > 255) Green = 255;
            if (Blue < 0) Blue = 0;
            if (Blue > 255) Blue = 255;
            return Color.FromArgb(Red, Green, Blue);
        }

        /// <summary>
        /// <para>根据已知色彩表，获取新色彩</para>
        /// <para>可以指定是否允许重复</para>
        /// <para>TODO: 不仅支持明亮色彩的轮转</para>
        /// </summary>
        public static Color GetNewColor(List<Color> colors, bool allowRepeat = false)
        {
            float h = 0;
            Color c;
            int delta = 67;
            if (allowRepeat)
            {
                h = colors[colors.Count - 1].GetHue() + delta;
                c = GetColorByHSB(h, 1, 1);
            }
            else
            {
                int total = 0;
                c = GetColorByHSB(0, 1, 1);
                while (colors.Contains(c))
                {
                    if (total > 250) throw new Exception("无法生成新色彩。");
                    h += 67;
                    if (h > 240) h -= 240;
                    c = GetColorByHSB(h, 1, 1);
                    total++;
                }
            }
            return c;
        }

        /// <summary>
        /// <para>获取指定尺寸的缩略图</para>
        /// <para>不适合用于较大的源图像</para>
        /// </summary>
        public static Image GetThumbImage(Image src, int width, int height)
        {
            int cwidth = src.Width;
            int cheight = src.Height;

            Bitmap bt = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bt))
            {
                g.Clear(Color.White);
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(src, new Rectangle(0, 0, width, height));
                g.Dispose();
            }
            return bt;
        }
    }
}
