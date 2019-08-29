using System.Drawing;

namespace UEd
{
    public class Options
    {
        private static bool? _autoIndent;
        private static Color? _backgroundColor;
        private static Color? _cursorColor;
        private static Color? _selectionColor;
        private static bool? _scrollAhead;
        private static bool? _showCurrentLineNumber;
        private static bool? _showTotalLines;

        public static bool AutoIndent
        {
            get
            {
                if (_autoIndent == null)
                    _autoIndent = GetBool(nameof(AutoIndent), true);
                return _autoIndent.Value;
            }
        }

        public static Color BackgroundColor
        {
            get
            {
                if (_backgroundColor == null)
                    _backgroundColor = GetColor(nameof(BackgroundColor), Color.FromArgb(0, 0, 0));
                return _backgroundColor.Value;
            }
        }

        public static Color CursorColor
        {
            get
            {
                if (_cursorColor == null)
                    _cursorColor = GetColor(nameof(CursorColor), Color.FromArgb(0, 255, 0));
                return _cursorColor.Value;
            }
        }

        public static Color SelectionColor
        {
            get
            {
                if (_selectionColor == null)
                    _selectionColor = GetColor(nameof(SelectionColor), Color.FromArgb(0, 128, 0));
                return _selectionColor.Value;
            }
        }

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

        private static Color GetColor(string name, Color defaultValue)
        {
            var result = System.Configuration.ConfigurationManager.AppSettings.Get(name);
            if (string.IsNullOrWhiteSpace(result))
                return defaultValue;
            try
            {
                return ColorTranslator.FromHtml(result);
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}