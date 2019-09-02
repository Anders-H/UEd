using System.Drawing;
using UEditor;

namespace UEd
{
    public class Options : IOptions
    {
        private bool? _autoIndent;
        private Color? _backgroundColor;
        private Color? _cursorColor;
        private Color? _selectionColor;
        private bool? _scrollAhead;
        private bool? _showColumnNumber;
        private bool? _showCurrentLineNumber;
        private bool? _showTotalLines;

        public bool AutoIndent
        {
            get
            {
                if (_autoIndent == null)
                    _autoIndent = GetBool(nameof(AutoIndent), true);
                return _autoIndent.Value;
            }
        }

        public Color BackgroundColor
        {
            get
            {
                if (_backgroundColor == null)
                    _backgroundColor = GetColor(nameof(BackgroundColor), Color.FromArgb(0, 0, 0));
                return _backgroundColor.Value;
            }
        }

        public Color CursorColor
        {
            get
            {
                if (_cursorColor == null)
                    _cursorColor = GetColor(nameof(CursorColor), Color.FromArgb(0, 255, 0));
                return _cursorColor.Value;
            }
        }

        public Color SelectionColor
        {
            get
            {
                if (_selectionColor == null)
                    _selectionColor = GetColor(nameof(SelectionColor), Color.FromArgb(0, 128, 0));
                return _selectionColor.Value;
            }
        }

        public bool ScrollAhead
        {
            get
            {
                if (_scrollAhead == null)
                    _scrollAhead = GetBool(nameof(ScrollAhead), true);
                return _scrollAhead.Value;
            }
        }

        public bool ShowColumnNumber
        {
            get
            {
                if (_showColumnNumber == null)
                    _showColumnNumber = GetBool(nameof(ShowColumnNumber), true);
                return _showColumnNumber.Value;
            }
        }

        public bool ShowCurrentLineNumber
        {
            get
            {
                if (_showCurrentLineNumber == null)
                    _showCurrentLineNumber = GetBool(nameof(ShowCurrentLineNumber), true);
                return _showCurrentLineNumber.Value;
            }
        }

        public bool ShowTotalLines
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