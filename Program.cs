using PlasticColorDistributor;
using System.Drawing;

const string ConfigurationFilePath = "C:\\Users\\HulaHut\\AppData\\Local\\plastic4\\branchexplorerusercolors.conf";

EuclidianColorDistance colorDistance = new EuclidianColorDistance();
List<Color> initialColors = new List<Color>()
{
    Color.White
};
ColorSharpener sharpener = new ColorSharpener(colorDistance, initialColors);
