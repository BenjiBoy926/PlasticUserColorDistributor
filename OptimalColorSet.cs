using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlasticColorDistributor
{
    internal class OptimalColorSet
    {
        private List<Color> _colors;
        private ColorDistance _distance;
        private int _randomSearchRestarts;
        private int _randomColorRestarts;
        
        public OptimalColorSet(List<Color> colors, ColorDistance distance, int randomSearchRestarts, int randomColorRestarts)
        {
            _colors = colors;
            _distance = distance;
            _randomSearchRestarts = randomSearchRestarts;
            _randomColorRestarts = randomColorRestarts;
        }

        public Color GetColor(int index)
        {
            return _colors[index];
        }

        public void AddOptimalColors(int colorsToAdd)
        {
            ColorHillClimbSearch[] searchAttempts = new ColorHillClimbSearch[_randomSearchRestarts];
            for (int i = 0; i < searchAttempts.Length; i++)
            {
                searchAttempts[i] = new ColorHillClimbSearch(_colors, _distance, _randomColorRestarts);
                AddColorsToSearch(colorsToAdd, searchAttempts[i]);
            }
            ColorHillClimbSearch bestAttempt = searchAttempts.Select(RatingOfSearch).MaxBy(Rating).Search;
            _colors = new List<Color>(bestAttempt.Colors);
        }
        private void AddColorsToSearch(int colorsToAdd, ColorHillClimbSearch search)
        {
            for (int i = 0; i < colorsToAdd; i++)
            {
                search.InsertNextBestColor();
            }
        }
        private SearchRating RatingOfSearch(ColorHillClimbSearch search)
        {
            return search.GetRating();
        }
        private float Rating(SearchRating rating)
        {
            return rating.Rating;
        }
    }
}
