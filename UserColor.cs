using System.Drawing;
using System.Text.RegularExpressions;

namespace PlasticColorDistributor
{
    internal struct UserColor
    {
        private const string Separator = "_@#@_";
        private static readonly Regex LineSplitter = new Regex(Separator);
        private const int FieldCount = 4;

        public string Username => _username;

        private string _username;
        private string _clickMessage;
        private Color _color;
        private string _guid;

        public static UserColor Parse(string line)
        {
            string[] fields = LineSplitter.Split(line);
            if (fields.Length != FieldCount)
            {
                throw new FormatException($"Expected '{line}' to contain {FieldCount} fields, " +
                    $"but it contains {fields.Length} fields");
            }
            return new UserColor
            {
                _username = fields[0],
                _clickMessage = fields[1],
                _color = ColorExtensions.Parse(fields[2]),
                // Expecting a possible endline at the end of the last field, so trim it off
                _guid = fields[3].Trim()
            };
        }

        public void SetColor(Color color)
        {
            _color = color;
        }
        public override string ToString()
        {
            return $"UserColor {{ " +
                $"{nameof(_username)}={_username}, " +
                $"{nameof(_clickMessage)}={_clickMessage}, " +
                $"{nameof(_color)}={_color.ToArgb()}, " +
                $"{nameof(_guid)}={_guid} " +
                $"}}";
        }
        public string ToParsableString()
        {
            return $"{_username}{Separator}{_clickMessage}{Separator}{_color.ToArgb()}{Separator}{_guid}";
        }
    }
}
