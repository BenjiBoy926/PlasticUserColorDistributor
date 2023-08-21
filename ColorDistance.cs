using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlasticColorDistributor
{
    internal abstract class ColorDistance
    {
        public abstract float Get(Color color1, Color color2);
    }
}
