using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PlasticColorDistributor
{
    // Credit: https://www.w3.org/TR/WCAG20/#relativeluminancedef
    // https://medium.muz.li/the-science-of-color-contrast-an-expert-designers-guide-33e84c41d156
    internal class RelativeLuminanceColorDistance : ColorDistance
    {
        private float MaxColorComponentValue = byte.MaxValue;
        private const float RedLuminanceContribution = 0.2126f;
        private const float GreenLuminanceContribution = 0.7152f;
        private const float BlueLuminanceContribution = 0.0722f;

        public override float Get(Color color1, Color color2)
        {
            float luminance1 = Luminance(color1);
            float luminance2 = Luminance(color2);
            if (luminance1 > luminance2)
            {
                return luminance1 / luminance2;
            }
            else
            {
                return luminance2 / luminance1;
            }
        }

        private float Luminance(Color color)
        {
            return RedLuminance(color) + GreenLuminance(color) + BlueLuminance(color);
        }

        private float RedLuminance(Color color)
        {
            return LuminanceComponent(color.R) * RedLuminanceContribution;
        }
        private float GreenLuminance(Color color)
        {
            return LuminanceComponent(color.G) * GreenLuminanceContribution;
        }
        private float BlueLuminance(Color color)
        {
            return LuminanceComponent(color.B) * BlueLuminanceContribution;
        }

        private float LuminanceComponent(byte value)
        {
            float percent = ComponentPercent(value);
            if (percent <= 0.03928f)
            {
                return percent / 12.92f;
            }
            else
            {
                float baseNumber = (percent + 0.055f) / 1.055f;
                return MathF.Pow(baseNumber, 2.4f);
            }
        }
        private float ComponentPercent(byte component)
        {
            return component / MaxColorComponentValue;
        }
    }
}
