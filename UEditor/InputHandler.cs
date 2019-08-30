using System.Windows.Forms;

namespace UEditor
{
    public class InputHandler
    {
        private readonly IOptions _options;

        public InputHandler(IOptions options)
        {
            _options = options;
        }

        public void KeyDown(Keys keyCode, CharacterArea area, CharacterView view)
        {
            switch (keyCode)
            {
                case Keys.Delete:
                    area.TypeDelete(view);
                    break;
                case Keys.Down:
                    area.MoveDown(view, false);
                    break;
                case Keys.Up:
                    area.MoveUp(view);
                    break;
                case Keys.Left:
                    area.MoveRight(view, false);
                    break;
                case Keys.Right:
                    area.MoveLeft(view, false);
                    break;
                case Keys.Home:
                    area.MoveToHome(view);
                    break;
                case Keys.End:
                    area.MoveToEnd(view);
                    break;
                case Keys.PageUp:
                    area.PageUp(view);
                    break;
                case Keys.PageDown:
                    area.PageDown(view);
                    break;
            }
        }

        public bool KeyPress(char c, CharacterArea area, CharacterView view)
        {
            switch ((int)c)
            {
                case 8:
                    area.TypeBackspace(view);
                    return true;
                case 9:
                    break;
                case 10:
                    return false;
                case 13:
                    area.TypeEnter(view);
                    return true;
            }
            if (!area.TypeCharacter(c, view, false))
                return false;
            area.MoveLeft(view, false);
            return true;
        }

        public void Indent(CharacterArea area, CharacterView view)
        {
            for (var t = 0; t < 3; t++)
            {
                area.TypeCharacter(' ', view, true);
                area.MoveLeft(view, t == 2);
            }
        }

        public void Outdent(CharacterArea area, CharacterView view)
        {
            if (area.CurrentRowLength <= 0)
                return;
            for (var t = 0; t < 3; t++)
            {
                if (area.CurrentRow[0] == ' ')
                {
                    area.DeleteAt(0, area.CursorY);
                    if (area.CursorX > 0)
                        area.CursorX--;
                }
            }
            var offsetX = _options.ScrollAhead && area.CursorX > 0 ? -1 : 0;
            view.EnsurePositionIsVisible(area.CursorX + offsetX, area.CursorY);
        }
    }
}