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
            return MathF.Sqrt(color1.R * color1.R + color1.G * color1.G + color1.B * color1.B);
        }
    }
}
