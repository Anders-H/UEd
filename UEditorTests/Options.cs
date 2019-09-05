using System.Drawing;
using UEditor;

namespace UEditorTests
{
    public class Options : IOptions
    {
        public Options()
        {
            AutoIndent = true;
            BackgroundColor = Color.Black;
            CursorColor = Color.Yellow;
            SelectionColor = Color.Blue;
            ScrollAhead = true;
            ShowColumnNumber = true;
            ShowCurrentLineNumber = true;
            ShowTotalLines = true;
        }

        public bool AutoIndent { get; set; }
        public Color BackgroundColor { get; set; }
        public Color CursorColor { get; set; }
        public Color SelectionColor { get; set; }
        public bool ScrollAhead { get; set; }
        public bool ShowColumnNumber { get; set; }
        public bool ShowCurrentLineNumber { get; set; }
        public bool ShowTotalLines { get; set; }
    }
}