using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlasticColorDistributor
{
    internal class EuclidianColorDistance : ColorDistance
    {
        public override float Get(Color color1, Color color2)
        {
            int rOffset = color2.R - color1.R;
            int gOffset = color2.G - color1.G;
            int bOffset = color2.B - color1.B;
            return MathF.Sqrt(rOffset * rOffset + gOffset * gOffset + bOffset * bOffset);
        }
    }
}
