using PlasticColorDistributor;
using System.Drawing;

const string ConfigurationFilePath = "C:\\Users\\HulaHut\\AppData\\Local\\plastic4\\branchexplorerusercolors.conf";

FileStream configurationFile = File.Open(ConfigurationFilePath, FileMode.Open);

if (configurationFile != null)
{
    Console.WriteLine("Successfully opened configuration file");
    configurationFile.Close();
}
else
{
    Console.WriteLine("Failed to open configuration file");
}

UserColor test = UserColor.Parse("itzeljuarez@owlchemylabs.com_@#@_Click to set other name_@#@_-15728641_@#@_c6ae9714-1124-47e6-a5ac-d2a78162aba6\n");
Console.WriteLine(test.ToString());
test.SetColor(Color.Red);
Console.WriteLine(test.ToString());
