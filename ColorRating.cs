using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlasticColorDistributor
{
    internal struct ColorRating
    {
        public Color Color => _color;
        public float Rating => _rating;

        private Color _color;
        private float _rating;

        public ColorRating(Color color, float rating)
        {
            _color = color;
            _rating = rating;
        }
    }
}
