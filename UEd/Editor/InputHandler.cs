using System.Windows.Forms;

namespace UEd.Editor
{
    public class InputHandler
    {
        public void KeyDown(Keys keyCode, CharacterArea area, CharacterView view)
        {
            switch (keyCode)
            {
                case Keys.Down:
                    area.MoveDown(view);
                    break;
                case Keys.Up:
                    area.MoveUp(view);
                    break;
                case Keys.Left:
                    area.MoveRight(view);
                    break;
                case Keys.Right:
                    area.MoveLeft(view);
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
            if (!area.TypeCharacter(c, view))
                return false;
            area.MoveLeft(view);
            return true;
        }

        public void Indent(CharacterArea area, CharacterView view)
        {
            for (var t = 0; t < 3; t++)
            {
                area.TypeCharacter(' ', view);
                area.MoveLeft(view);
            }
        }

        public void Outdent(CharacterArea area, CharacterView view)
        {
            if (area.CurrentRow.Length <= 0)
                return;
            for (var t = 0; t < 3; t++)
            {
                if (area.CursorX == 0 && area.GetCharacterAt(area.CursorX, area.CursorX)[0] == 9)
                {
                    //area.Delete();
                    return;
                }
                if (area.CursorX == 0 && area.GetCharacterAt(area.CursorX, area.CursorX) == " ")
                {
                    //area.Delete();
                    continue;
                }
                if (area.CursorX > 0 && area.CurrentRow.Length >= area.CursorX && area.GetCharacterAt(area.CursorX, area.CursorX)[0] == 9)
                {
                    area.TypeBackspace(view);
                    return;
                }
                if (area.CursorX > 0 && area.CurrentRow.Length >= area.CursorX && area.GetCharacterAt(area.CursorX, area.CursorX) == " ")
                {
                    area.TypeBackspace(view);
                    continue;
                }
                if (area.CursorX > 0)
                {
                    area.TypeBackspace(view);
                }
            }
        }
    }
}