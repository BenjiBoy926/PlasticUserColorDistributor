using PlasticColorDistributor;
using System.Drawing;

const string ConfigurationFilePath = "C:\\Users\\HulaHut\\AppData\\Local\\plastic4\\branchexplorerusercolors.conf";
const string EveryonesEmailServer = "@owlchemylabs.com";
const int RandomRestartsForColorOptimization = 25;

List<Color> colors = new List<Color>()
{
    Color.White,
    Color.Black,
    Color.Red,
    Color.FromArgb(0, 255, 0),
    Color.Blue,
    Color.Yellow,
    Color.Magenta,
    Color.Cyan,
    Color.FromArgb(128, 128, 128),
    Color.FromArgb(128, 128, 0),
    Color.FromArgb(128, 0, 128),
    Color.FromArgb(0, 128, 128),
    Color.FromArgb(128, 128, 255),
    Color.FromArgb(128, 255, 128),
    Color.FromArgb(255, 128, 128)
};
string[] importantUsers = new string[]
{
    "alexcovert",
    "bogdanrybak",
    "chrishall",
    "codeyhuntting",
    "dalenewcomb",
    "davidbusch",
    "graeme",
    "itzeljuarez",
    "jasongordy",
    "kellyjohnson",
    "marchuet",
    "rachelemery",
};

UserColorConfigurationFile configurationFile = new UserColorConfigurationFile(ConfigurationFilePath);
ColorDistance colorDistance = new RelativeLuminanceColorDistance();
ColorSharpener sharpener = new ColorSharpener(colorDistance, colors, RandomRestartsForColorOptimization);
int firstNonImportantColorIndex = Math.Min(colors.Count, importantUsers.Length);

for (int i = 0; i < firstNonImportantColorIndex; i++)
{
    configurationFile.SetColorOfUser(colors[i], importantUsers[i] + EveryonesEmailServer);
}
while (sharpener.TotalColors < configurationFile.TotalUsers)
{
    sharpener.InsertNextBestColor();
}
for (int i = firstNonImportantColorIndex; i < configurationFile.TotalUsers; i++)
{
    Color color = sharpener.GetColor(i);
    configurationFile.SetColorOfNextUser(color);
}
configurationFile.Write();