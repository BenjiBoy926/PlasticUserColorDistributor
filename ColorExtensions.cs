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
    }
}
