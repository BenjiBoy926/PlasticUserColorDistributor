using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlasticColorDistributor
{
    internal struct SearchRating
    {
        public ColorHillClimbSearch Search => _search;
        public float Rating => _rating;

        private ColorHillClimbSearch _search;
        private float _rating;

        public SearchRating(ColorHillClimbSearch search, float rating)
        {
            _search = search;
            _rating = rating;
        }
    }
}
