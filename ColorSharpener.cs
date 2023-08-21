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
        private List<Color> _colors;
        private ColorDistance _distance;

        public ColorSharpener(ColorDistance distance, List<Color> colors)
        {
            _colors = colors;
            _distance = distance;
        }

        public Color FindNextBestColor(Color current)
        {
            while (true)
            {
                Color next = FindNextBestNeighbor(current);
                if (next == current)
                {
                    return current;
                }
                current = next;
            }
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
            float minimum = _distance.Get(color, _colors[0]);
            for (int i = 1; i < _colors.Count; i++)
            {
                float current = _distance.Get(color, _colors[i]);
                minimum = MathF.Min(minimum, current);
            }
            return minimum;
        }
        private List<Color> GetNeighbors(Color color)
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
