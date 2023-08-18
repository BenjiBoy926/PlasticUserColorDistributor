using System.Drawing;
using System.Reflection.PortableExecutable;

namespace PlasticColorDistributor
{
    internal static class ColorExtensions
    {
        /// <summary>
        public static Color Parse(string colorString)
        {
            return Color.FromArgb(int.Parse(colorString));
        }

    }
}
