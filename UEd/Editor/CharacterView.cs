using System.Drawing;
using UEd.Editor.Zoom;

namespace UEd.Editor
{
    public class CharacterView
    {
        public static ZoomLevelList ZoomLevels { get; } = new ZoomLevelList();
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }
        public Font Font { get; set; }

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

        public void Draw(CharacterArea area, Graphics g, int viewportX, int viewportY, int viewportWidth, int viewportHeight)
        {
            var xpos = (double)viewportX;
            var ypos = (double)viewportY;
            var charWidth = viewportWidth/(double)ZoomLevels.GetCurrentZoom().Columns;
            var charHeight = viewportHeight/(double)ZoomLevels.GetCurrentZoom().Rows;
            using (var background = new SolidBrush(Color.Black))
            {
                using (var foreground = new SolidBrush(Color.FromArgb(0, 255, 0)))
                {
                    g.Clear(Color.Black);
                    var z = g.MeasureString("l", Font);
                    var charOffsetX = (float) (charWidth / 2 - z.Width / 2);
                    var charOffsetY = (float) (charHeight / 2 - z.Height / 2);
                    for (var y = 0; y < ZoomLevels.GetCurrentZoom().Rows; y++)
                    {
                        for (var x = 0; x < ZoomLevels.GetCurrentZoom().Columns; x++)
                        {
                            var physicalX = (float) xpos;
                            var physicalY = (float) ypos;
                            var physicalWidth = (float) charWidth;
                            var physicalHeight = (float) charHeight;
                            var s = area.GetCharacterAt(x + OffsetX, y + OffsetY);
                            if (x + OffsetX == area.CursorX && y + OffsetY == area.CursorY)
                            {
                                g.FillRectangle(foreground, physicalX, physicalY - 2, physicalWidth,
                                    physicalHeight + 2);
                                if (!string.IsNullOrWhiteSpace(s))
                                    g.DrawString(s, Font, background, physicalX + charOffsetX, physicalY + charOffsetY);
                            }
                            else
                            {
                                g.DrawString(s, Font, foreground, physicalX + charOffsetX, physicalY + charOffsetY);
                            }

                            xpos += charWidth;
                        }

                        xpos = viewportX;
                        ypos += charHeight;
                    }
                }
            }
        }
    }
}