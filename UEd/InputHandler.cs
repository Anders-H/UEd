using System.Windows.Forms;

namespace UEd
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
    }
}