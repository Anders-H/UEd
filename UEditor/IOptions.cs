using System.Drawing;

namespace UEditor
{
    public interface IOptions
    {
        bool AutoIndent { get; }
        Color BackgroundColor { get; }
        Color CursorColor { get; }
        Color SelectionColor { get; }
        bool ScrollAhead { get; }
        bool ShowColumnNumber { get; }
        bool ShowCurrentLineNumber { get; }
        bool ShowTotalLines { get; }
    }
}