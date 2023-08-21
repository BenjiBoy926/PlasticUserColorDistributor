using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace PlasticColorDistributor
{
    internal class UserColorConfigurationFile
    {
        public int TotalUsers => _configuration.TotalUsers;

        private string _filePath;
        private UserColorConfiguration _configuration;

        public UserColorConfigurationFile(string filePath) 
        {
            _filePath = filePath;
            _configuration = new UserColorConfiguration();
            foreach (string line in File.ReadAllLines(_filePath))
            {
                AddLine(line);
            }
        }
        private void AddLine(string line)
        {
            try
            {
                _configuration.Add(line);
            }
            catch (Exception ex) 
            { 
                Console.WriteLine($"Failed to add the line '{line}' to the configuration. See following error for more information");
                Console.WriteLine(ex);
            }
        }

        public void SetColorOfUser(Color color, int userIndex)
        {
            _configuration.SetColorOfUser(color, userIndex);
        }
        public void Write()
        {
            File.WriteAllText(_filePath, _configuration.ToParsableString());
        }
    }
}
