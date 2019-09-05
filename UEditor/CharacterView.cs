using System.Drawing;
using UEditor.Zoom;

namespace UEditor
{
    public class CharacterView
    {
        private readonly IOptions _options;
        public static ZoomLevelList ZoomLevels { get; } = new ZoomLevelList();
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }
        public Font Font { get; set; }

        public CharacterView(IOptions options)
        {
            _options = options;
        }

        public void SetOffset(int x, int y)
        {
            OffsetX = x;
            OffsetY = y;
        }

        public bool IsInView(Point p) =>
            IsInView(p.X, p.Y);

        public bool IsInView(int x, int y) =>
            x >= OffsetX
            && x < OffsetX + ZoomLevels.GetCurrentZoom().Columns
            && y >= OffsetY
            && y < OffsetY + ZoomLevels.GetCurrentZoom().Rows;

        public void EnsurePositionIsVisible(Point p) =>
            EnsurePositionIsVisible(p.X, p.Y);

        public void EnsurePositionIsVisible(int x, int y)
        {
            while (x < OffsetX)
                OffsetX--;
            while (x > OffsetX + ZoomLevels.GetCurrentZoom().Columns - 1)
                OffsetX++;
            while (y < OffsetY)
                OffsetY--;
            while (y > OffsetY + ZoomLevels.GetCurrentZoom().Rows - 1)
                OffsetY++;
        }

        public void Draw(CharacterArea area, Graphics g, Font secondaryFont, int viewportX, int viewportY, int viewportWidth, int viewportHeight)
        {
            var xpos = (double)viewportX;
            var ypos = (double)viewportY;
            var charWidth = viewportWidth / (double)ZoomLevels.GetCurrentZoom().Columns;
            var charHeight = viewportHeight / (double)ZoomLevels.GetCurrentZoom().Rows;
            var physicalWidth = (float)charWidth;
            var physicalHeight = (float)charHeight;
            using (var background = new SolidBrush(_options.BackgroundColor))
            {
                using (var foreground = new SolidBrush(_options.CursorColor))
                {
                    using (var selection = new SolidBrush(_options.SelectionColor))
                    {
                        g.Clear(Color.Black);
                        var z = g.MeasureString("l", Font);
                        var charOffsetX = (float)(charWidth / 2 - z.Width / 2);
                        var charOffsetY = (float)(charHeight / 2 - z.Height / 2);
                        for (var y = 0; y < ZoomLevels.GetCurrentZoom().Rows; y++)
                        {
                            var rowSelection = area.GetRowSelection(y + OffsetY);
                            var physicalY = (float)ypos;
                            for (var x = 0; x < ZoomLevels.GetCurrentZoom().Columns; x++)
                            {
                                var physicalX = (float)xpos;
                                var s = area.GetCharacterAt(x + OffsetX, y + OffsetY);
                                if (rowSelection.CharacterIsSelected(x + OffsetX))
                                    g.FillRectangle(selection, physicalX, physicalY, physicalWidth, physicalHeight);
                                var charX = physicalX + charOffsetX;
                                var charY = physicalY + charOffsetY;
                                if (x + OffsetX == area.CursorX && y + OffsetY == area.CursorY)
                                {
                                    g.FillRectangle(foreground, physicalX, physicalY, physicalWidth, physicalHeight);
                                    if (!string.IsNullOrWhiteSpace(s))
                                        g.DrawString(s, Font, background, charX, charY);
                                }
                                else
                                {
                                    g.DrawString(s, Font, foreground, charX, charY);
                                }
#if DEBUG
                                var whitespace = area.GetCharacterOrWhitespaceAt(x + OffsetX, y + OffsetY);
                                if (whitespace != null)
                                {
                                    if (whitespace == " ")
                                        g.FillRectangle(Brushes.Yellow, (physicalX + (int)(charWidth / 2)) - 1,
                                            (physicalY + (int)(charHeight / 2)) - 1, 2, 2);
                                    else if (whitespace[0] == 9)
                                        g.FillRectangle(Brushes.Pink, (physicalX + (int)(charWidth / 2)) - 1,
                                            (physicalY + (int)(charHeight / 2)) - 1, 2, 2);
                                }
#endif
                                xpos += charWidth;
                            }

                            xpos = viewportX;
                            ypos += charHeight;
                        }
                    }
                }
            }
            if (!_options.ShowCurrentLineNumber && !_options.ShowTotalLines && !_options.ShowColumnNumber)
                return;
            string row;
            if (_options.ShowColumnNumber && (_options.ShowCurrentLineNumber || _options.ShowTotalLines))
                row = $"{area.CursorX + 1}, ";
            else if (_options.ShowColumnNumber)
                row = $"{area.CursorX + 1}";
            else
                row = "";
            row += _options.ShowCurrentLineNumber && _options.ShowTotalLines ? $"{area.CursorY + 1}/{area.RowCount}"
                : _options.ShowCurrentLineNumber
                ? $"{area.CursorY + 1}" : $"{area.RowCount}";
            var m = g.MeasureString(row, secondaryFont);
            const int margin = 3;
            using (var b = new SolidBrush(Color.FromArgb(50, 255, 255, 255)))
                g.DrawString(row, secondaryFont, b, viewportX + viewportWidth - m.Width - margin, viewportY + margin);
        }
    }
}