using System.Collections.Generic;
using System.Drawing;

namespace apsBalanca
{
    public static class Tema
    {
        public static List<string> ColorList = new List<string>() {
            "0x008789",
            "0xAC2E58",
            "0xFF9900",
            "0x0A6284",
            "0x8A73C7"
        };
        public static Color ChangeColorBrightness(Color color, double correctionFactor)
        {
            double red = color.R;
            double green = color.G;
            double blue = color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor; blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }
            return Color.FromArgb(color.A,(byte)red,(byte)green,(byte)blue);
        }
    }
}
