using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;

namespace DecisionTreeUI.Models
{
    public class NodeClass : NodeBase
    {
        private Color _ClassColor;

        public NodeClass()
        {
            _Type = NodeType.Class;
        }

        public Color ClassColor
        {
            get { return _ClassColor; }
            set { _ClassColor = value; }
        }

        public static Color GetColorByHSB(float H, float S, float B)
        {
            double r = 0;
            double g = 0;
            double b = 0;

            if (S == 0)
            {
                r = g = b = B;
            }
            else
            {
                // the color wheel consists of 6 sectors. Figure out which sector
                // you're in.
                double sectorPos = H / 60.0;
                int sectorNumber = (int)(Math.Floor(sectorPos));
                // get the fractional part of the sector
                double fractionalSector = sectorPos - sectorNumber;

                // calculate values for the three axes of the color.
                double p = B * (1.0 - S);
                double q = B * (1.0 - (S * fractionalSector));
                double t = B * (1.0 - (S * (1 - fractionalSector)));

                // assign the fractional colors to r, g, and b based on the sector
                // the angle is in.
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

        public static Color GetNewColor(List<Color> colors)
        {
            int h = 0;
            Color c = GetColorByHSB(0, 1, 1);
            int total = 0;
            while (colors.Contains(c))
            {
                if (total > 10000) throw new Exception("无法生成新色彩。");
                h += 67;
                if (h > 240) h -= 240;
                c = GetColorByHSB(h, 1, 1);
                total++;
            }
            return c;
        }

        public override XElement ToXml()
        {
            XElement xe = base.ToXml();
            xe.Add(new XElement("color",
                    new XCData(string.Format("{0},{1},{2}",
                        _ClassColor.R, _ClassColor.G, _ClassColor.B))));
            return xe;
        }
    }
}
