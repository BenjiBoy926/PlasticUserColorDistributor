using System.Drawing;

namespace PlasticColorDistributor
{
    internal static class ColorExtensions
    {
        public const float MaxLuminance = byte.MaxValue;
        private const float RedLuminanceContribution = 0.2126f;
        private const float GreenLuminanceContribution = 0.7152f;
        private const float BlueLuminanceContribution = 0.0722f;

        /// <summary>
        /// Range from 0 to 255
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static float Luminance(this Color color)
        {
            return RedLuminance(color.R) + GreenLuminance(color.G) + BlueLuminance(color.B);
        }
        public static Color Parse(string colorString)
        {
            return Color.FromArgb(int.Parse(colorString));
        }

        private static float RedLuminance(int r)
        {
            return RedLuminanceContribution * r;
        }
        private static float GreenLuminance(int g)
        {
            return GreenLuminanceContribution * g;
        }
        private static float BlueLuminance(int b)
        {
            return BlueLuminanceContribution * b;
        }

        // Credits: https://stackoverflow.com/questions/58572030/algorithm-to-successively-deliver-colors-of-maximum-contrast
        public static float Distance(Color a, Color b)
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
