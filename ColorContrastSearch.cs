using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlasticColorDistributor
{
    // Credits: https://stackoverflow.com/questions/58572030/algorithm-to-successively-deliver-colors-of-maximum-contrast
    // http://www.usrsb.in/picking-colors.html
    internal class ColorContrastSearch
    {
        private static float FindMinimumDistance(Color color, List<Color> colorsToCheck)
        {
            if (colorsToCheck == null || colorsToCheck.Count == 0)
            {
                return 0;
            }
            float minimum = Distance(color, colorsToCheck[0]);
            for (int i = 1; i < colorsToCheck.Count; i++)
            {
                float current = Distance(color, colorsToCheck[i]);
                minimum = MathF.Min(minimum, current);
            }
            return minimum;
        }
        private static List<Color> Neighbors(Color color)
        {
            List<Color> neighbors = new List<Color>();
            for (int rOffset = 0; rOffset < 3; rOffset++)
            {
                for (int gOffset = 0; gOffset < 3; gOffset++)
                {
                    for (int bOffset = 0; bOffset < 3; bOffset++)
                    {
                        if (rOffset == 0 && gOffset == 0 && bOffset == 0)
                        {
                            continue;
                        }
                        if (!IsOffsetValid(color, rOffset, gOffset, bOffset))
                        {
                            continue;
                        }
                        Color offset = Color.FromArgb(rOffset, gOffset, bOffset);
                        Color neighbor = Add(color, offset);
                        neighbors.Add(neighbor);
                    }
                }
            }
            return neighbors;
        }
        private static bool IsOffsetValid(Color color, int rOffset, int gOffset, int bOffset)
        {
            return rOffset + color.R <= byte.MaxValue &&
                gOffset + color.G <= byte.MaxValue &&
                bOffset + color.B <= byte.MaxValue;
        }
        private static Color Add(Color a, Color b)
        {
            return Color.FromArgb(
                a.R + b.R,
                a.G + b.G,
                a.B + b.B);
        }
        private static float Distance(Color a, Color b)
        {
            float avgRed = AverageRed(a, b);

            float redDist = RedDistance(a, b);
            float greenDist = GreenDistance(a, b);
            float blueDist = BlueDistance(a, b);

            float redCoeff = 2 + (avgRed / 256);
            float greenCoeff = 4;
            float blueCoeff = 2 + ((255 - avgRed) / 256);

            return MathF.Sqrt(redCoeff * redDist * redDist + greenCoeff * greenDist * greenDist + blueCoeff * blueDist * blueDist);
        }

        private static float AverageRed(Color a, Color b)
        {
            return (a.R + b.R) / 2;
        }
        private static float RedDistance(Color a, Color b)
        {
            return a.R - b.R;
        }
        private static float GreenDistance(Color a, Color b)
        {
            return a.G - b.G;
        }
        private static float BlueDistance(Color a, Color b)
        {
            return a.B - b.B;
        }
    }
}
