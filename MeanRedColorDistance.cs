using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlasticColorDistributor
{
    internal class MeanRedColorDistance : ColorDistance
    {
        public override float Get(Color color1, Color color2)
        {
            float avgRed = AverageRed(color1, color2);

            float redDist = RedDistance(color1, color2);
            float greenDist = GreenDistance(color1, color2);
            float blueDist = BlueDistance(color1, color2);

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
