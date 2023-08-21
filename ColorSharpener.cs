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
    internal class ColorSharpener
    {
        public int TotalColors => _colors.Count;

        private List<Color> _colors;
        private ColorDistance _distance;
        private int _randomRestarts;
        private Random _random;
        private byte[] _randomColorBuffer;

        public ColorSharpener(ColorDistance distance, List<Color> colors, int randomRestarts)
        {
            _colors = colors;
            _distance = distance;
            _randomRestarts = randomRestarts;
            _random = new Random();
            _randomColorBuffer = new byte[3];
        }

        public Color GetColor(int i)
        {
            return _colors[i];
        }
        // Another possible improvement: perform x insertions, randomly restart, rate all the configurations and pick the best one
        public void InsertNextBestColor()
        {
            _colors.Add(FindNextBestColor());
        }
        private Color FindNextBestColor()
        {
            if (_randomRestarts <= 1)
            {
                return FindNextBestColor(GetRandomColor());
            }
            List<Color> colors = new List<Color>();
            for (int i = 0; i < _randomRestarts; i++)
            {
                Color randomColor = GetRandomColor();
                Color nextBest = FindNextBestColor(randomColor);
                colors.Add(nextBest);
            }
            return colors.Select(RatingOfColor).MaxBy(Rating).Color;
        }
        private Color GetRandomColor()
        {
            _random.NextBytes(_randomColorBuffer);
            return Color.FromArgb(_randomColorBuffer[0], _randomColorBuffer[1], _randomColorBuffer[2]);
        }

        private Color FindNextBestColor(Color current)
        {
            Color next;
            do
            {
                next = FindNextBestNeighbor(current);
                if (current != next)
                {
                    current = next;
                }
            }
            while (current != next);
            return current;
        }
        /// <summary>
        /// If no neighbor is better than the argument, the argument value is returned
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        private Color FindNextBestNeighbor(Color color)
        {
            List<Color> neighbors = GetNeighbors(color);
            if (neighbors.Count == 0)
            {
                return color;
            }
            ColorRating currentRating = RatingOfColor(color);
            ColorRating bestNeighborRating = neighbors.Select(RatingOfColor).MaxBy(Rating);
            if (bestNeighborRating.Rating > currentRating.Rating)
            {
                return bestNeighborRating.Color;
            }
            return color;
        }
        private float Rating(ColorRating rating)
        {
            return rating.Rating;
        }
        private ColorRating RatingOfColor(Color colorToRate)
        {
            return new ColorRating(colorToRate, MinimumDistance(colorToRate));
        }
        private float MinimumDistance(Color color)
        {
            if (_colors == null || _colors.Count == 0)
            {
                return 0;
            }
            float DistanceOfArgumentToOtherColor(Color otherColor)
            {
                return _distance.Get(color, otherColor);
            }
            return _colors.Select(DistanceOfArgumentToOtherColor).Min();
        }
        private List<Color> GetNeighbors(Color color)
        {
            List<Color> neighbors = new List<Color>();
            for (int rOffset = -1; rOffset <= 1; rOffset++)
            {
                for (int gOffset = -1; gOffset <= 1; gOffset++)
                {
                    for (int bOffset = -1; bOffset <= 1; bOffset++)
                    {
                        if (rOffset == 0 && gOffset == 0 && bOffset == 0)
                        {
                            continue;
                        }
                        if (!IsOffsetValid(color, rOffset, gOffset, bOffset))
                        {
                            continue;
                        }
                        Color neighbor = Offset(color, rOffset, gOffset, bOffset);
                        neighbors.Add(neighbor);
                    }
                }
            }
            return neighbors;
        }
        private bool IsOffsetValid(Color color, int rOffset, int gOffset, int bOffset)
        {
            int newR = color.R + rOffset;
            int newG = color.G + gOffset;
            int newB = color.B + bOffset;
            return IsColorComponentValid(newR) && IsColorComponentValid(newG) && IsColorComponentValid(newB);
        }
        private bool IsColorComponentValid(int component)
        {
            return component >= 0 && component <= byte.MaxValue;
        }
        private Color Offset(Color color, int rOffset, int gOffset, int bOffset)
        {
            return Color.FromArgb(color.R + rOffset, color.G + gOffset, color.B + bOffset);
        }
    }
}
