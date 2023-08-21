using System.Drawing;
using System.Text;

namespace PlasticColorDistributor
{
    internal class UserColorConfiguration
    {
        public int TotalUsers => _users.Count;

        private List<UserColor> _users = new List<UserColor>();

        public void Add(string userString)
        {
            Add(UserColor.Parse(userString));
        }
        private void Add(UserColor userColor)
        {
            _users.Add(userColor);
        }

        public int IndexOfUser(string username)
        {
            bool WithName(UserColor userColor)
            {
                return userColor.Username == username;
            }
            return _users.FindIndex(WithName);
        }
        public void SetColorOfUser(Color color, int userIndex)
        {
            UserColor user = _users[userIndex];
            user.SetColor(color);
            _users[userIndex] = user;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(nameof(UserColorConfiguration));
            builder.Append(" { ");
            foreach (UserColor userColor in _users)
            {
                builder.Append('\n');
                builder.Append(userColor.ToString());
            }
            builder.Append('}');
            return builder.ToString();
        }
        public string ToParsableString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (UserColor userColor in _users)
            {
                builder.Append(userColor.ToParsableString());
                builder.Append('\n');
            }
            return builder.ToString();
        }
    }
}
