namespace UEd
{
    public class Options
    {
        private static bool? _scrollAhead;
        private static bool? _showCurrentLineNumber;
        private static bool? _showTotalLines;

        public static bool ScrollAhead
        {
            get
            {
                if (_scrollAhead == null)
                    _scrollAhead = GetBool(nameof(ScrollAhead), true);
                return _scrollAhead.Value;
            }
        }

        public static bool ShowCurrentLineNumber
        {
            get
            {
                if (_showCurrentLineNumber == null)
                    _showCurrentLineNumber = GetBool(nameof(ShowCurrentLineNumber), true);
                return _showCurrentLineNumber.Value;
            }
        }

        public static bool ShowTotalLines
        {
            get
            {
                if (_showTotalLines == null)
                    _showTotalLines = GetBool(nameof(ShowTotalLines), true);
                return _showTotalLines.Value;
            }
        }

        private static bool GetBool(string name, bool defaultValue)
        {
            var result = System.Configuration.ConfigurationManager.AppSettings.Get(name);
            if (string.IsNullOrWhiteSpace(result))
                return defaultValue;
            switch (result.ToLower())
            {
                case "0":
                case "false":
                    return false;
                case "1":
                case "true":
                    return true;
                default:
                    return defaultValue;
            }
        }
    }
}