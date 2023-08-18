using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlasticColorDistributor
{
    internal class UserColorConfiguration
    {
        private List<UserColor> _users = new List<UserColor>();

        public void Add(string userString)
        {
            Add(UserColor.Parse(userString));
        }
        private void Add(UserColor userColor)
        {
            _users.Add(userColor);
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
