using System.Drawing;

namespace PlasticColorDistributor
{
    internal class UserColorConfigurationFile
    {
        public int TotalUsers => _configuration.TotalUsers;

        private string _filePath;
        private UserColorConfiguration _configuration;
        private bool[] _usersWithSetColors;

        public UserColorConfigurationFile(string filePath) 
        {
            _filePath = filePath;
            _configuration = new UserColorConfiguration();
            foreach (string line in File.ReadAllLines(_filePath))
            {
                AddLine(line);
            }
            _usersWithSetColors = new bool[_configuration.TotalUsers];
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

        public void SetColorOfNextUser(Color color)
        {
            int index = GetIndexOfNextUser();
            if (index >= 0)
            {
                SetColorOfUser(color, index);
            }
        }
        private int GetIndexOfNextUser()
        {
            for (int i = 0; i < _usersWithSetColors.Length; i++)
            {
                if (!_usersWithSetColors[i])
                {
                    return i;
                }
            }
            return -1;
        }

        public void SetColorOfUser(Color color, string username)
        {
            int index = IndexOfUser(username);
            if (index >= 0)
            {
                SetColorOfUser(color, index);
            }
        }
        public int IndexOfUser(string username)
        {
            return _configuration.IndexOfUser(username);
        }

        private void SetColorOfUser(Color color, int userIndex)
        {
            _configuration.SetColorOfUser(color, userIndex);
            _usersWithSetColors[userIndex] = true;
        }

        public void Write()
        {
            File.WriteAllText(_filePath, _configuration.ToParsableString());
        }
    }
}
