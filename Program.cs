using PlasticColorDistributor;
using System.Drawing;

const string ConfigurationFilePath = "C:\\Users\\HulaHut\\AppData\\Local\\plastic4\\branchexplorerusercolors.conf";
const int RandomRestartsForColorOptimization = 25;

UserColorConfigurationFile configurationFile = new UserColorConfigurationFile(ConfigurationFilePath);
ColorDistance colorDistance = new RelativeLuminanceColorDistance();
List<Color> initialColors = new List<Color>()
{
    Color.White, Color.Black,
    Color.Red, Color.FromArgb(0, 255, 0), Color.Blue,
    Color.Yellow, Color.Magenta, Color.Cyan,
    Color.FromArgb(128, 128, 128),
    Color.FromArgb(128, 128, 0), Color.FromArgb(128, 0, 128), Color.FromArgb(0, 128, 128),
    Color.FromArgb(128, 128, 255), Color.FromArgb(128, 255, 128), Color.FromArgb(255, 128, 128)
};
ColorSharpener sharpener = new ColorSharpener(colorDistance, initialColors, RandomRestartsForColorOptimization);
while (sharpener.TotalColors < configurationFile.TotalUsers)
{
    sharpener.InsertNextBestColor();
}
for (int i = 0; i < configurationFile.TotalUsers; i++)
{
    Color color = sharpener.GetColor(i);
    configurationFile.SetColorOfUser(color, i);
}
configurationFile.Write();