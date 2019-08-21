using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UEd
{
    public class CharacterArea
    {
        private readonly List<string> _rows = new List<string>();
        public int CursorX { get; set; }
        public int CursorY { get; set; }
        
        public CharacterArea()
        {
            Clear();
        }

        public void Clear()
        {
            _rows.Clear();
            CursorX = 0;
            CursorY = 0;
            _rows.Add("");
        }

        public void SetData(string text)
        {
            if (text == null)
            {
                Clear();
                return;
            }
            var rows = Regex.Split(text, @"\n");
            _rows.Clear();
            _rows.AddRange(rows);
            CursorX = 0;
            CursorY = 0;
        }

        public string CurrentRow =>
            CursorY >= 0 && CursorY < _rows.Count
                ? _rows[CursorY]
                : null;

        public int CurrentRowLength =>
            CurrentRow?.Length ?? 0;

        public void MoveDown(CharacterView view)
        {
            if (CursorY < _rows.Count - 1)
                CursorY++;
            if (CursorY > view.OffsetY + CharacterView.ZoomLevels.GetCurrentZoom().Rows - 1)
                view.OffsetY++;
            while (CursorX > CurrentRowLength)
                MoveRight(view);
        }

        public void MoveUp(CharacterView view)
        {
            if (CursorY > 0)
                CursorY--;
            if (CursorY < view.OffsetY)
                view.OffsetY--;
            while (CursorX > CurrentRowLength)
                MoveRight(view);
        }

        public void MoveLeft(CharacterView view)
        {
            if (CursorX < CurrentRowLength)
            {
                CursorX++;
                if (CursorX > view.OffsetX + CharacterView.ZoomLevels.GetCurrentZoom().Columns - 1)
                    view.OffsetX++;
                return;
            }
            if (CursorY >= _rows.Count - 1)
                return;
            MoveDown(view);
            CursorX = 0;
            JumpInHorizontalView(view);
        }

        public void MoveRight(CharacterView view)
        {
            if (CursorX > 0)
            {
                CursorX--;
                if (CursorX < view.OffsetX)
                    view.OffsetX--;
                return;
            }
            if (CursorY <= 0)
                return;
            MoveUp(view);
            CursorX = CurrentRowLength;
            JumpInHorizontalView(view);
        }

        public string GetCharacterAt(int x, int y)
        {
            if (y < 0 || y >= _rows.Count)
                return null;
            var row = _rows[y];
            if (string.IsNullOrWhiteSpace(row))
                return null;
            return row.Length > x ? row.Substring(x, 1) : null;
        }

        public void MoveToHome(CharacterView view)
        {
            if (CurrentRowLength == 0)
            {
                CursorX = 0;
                JumpInHorizontalView(view);
                return;
            }
            var indentDepth = GetIndentDepth(CursorY);
            if (CursorX > indentDepth)
            {
                CursorX = indentDepth;
                JumpInHorizontalView(view);
                return;
            }
            if (CursorX == indentDepth)
            {
                CursorX = 0;
                JumpInHorizontalView(view);
                return;
            }
            CursorX = CursorX == 0 ? indentDepth : 0;
            JumpInHorizontalView(view);
        }

        public void MoveToEnd(CharacterView view)
        {
            CursorX = CurrentRowLength;
            JumpInHorizontalView(view);
        }

        public void TypeBackspace(CharacterView view)
        {

        }

        public void PageUp(CharacterView view)
        {
            if (CursorY <= 0)
                return;
            view.OffsetY -= CharacterView.ZoomLevels.GetCurrentZoom().Rows;
            if (view.OffsetY < 0)
                view.OffsetY = 0;
            CursorY -= CharacterView.ZoomLevels.GetCurrentZoom().Rows;
            if (CursorY < 0)
                CursorY = 0;
        }

        public void PageDown(CharacterView view)
        {
            if (CursorY >= _rows.Count - 1)
                return;
            view.OffsetY += CharacterView.ZoomLevels.GetCurrentZoom().Rows;
            if (view.OffsetY > _rows.Count - CharacterView.ZoomLevels.GetCurrentZoom().Rows)
                view.OffsetY = _rows.Count - CharacterView.ZoomLevels.GetCurrentZoom().Rows;
            if (_rows.Count <= CharacterView.ZoomLevels.GetCurrentZoom().Rows)
                view.OffsetY = 0;
            CursorY += CharacterView.ZoomLevels.GetCurrentZoom().Rows;
            if (CursorY >= _rows.Count)
                CursorY = _rows.Count - 1;
        }

        public bool TypeCharacter(char c, CharacterView view)
        {
            var r = _rows[CursorY];
            if (string.IsNullOrEmpty(r))
            {
                _rows[CursorY] = new string(c, 1);
                JumpInHorizontalView(view);
                return true;
            }
            if (CursorX < r.Length)
            {
                _rows[CursorY] = r.Insert(CursorX, new string(c, 1));
                return true;
            }
            if (CursorX > r.Length)
            {
                CursorX = r.Length;
                JumpInHorizontalView(view);
            }
            try
            {
                _rows[CursorY] = r.Insert(CursorX, new string(c, 1));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void TypeEnter(CharacterView view)
        {
            if (CursorX == 0)
            {
                _rows.Insert(CursorY, "");
                MoveDown(view);
                JumpInHorizontalView(view);
                return;
            }
            if (CursorX == CurrentRowLength)
            {
                if (CursorY >= _rows.Count - 1)
                    _rows.Add("");
                else
                    _rows.Insert(CursorY + 1, "");
                MoveDown(view);
                return;
            }
            var leftPart = _rows[CursorY].Substring(0, CursorX);
            var rightPart = _rows[CursorY].Substring(CursorX);
            _rows[CursorY] = leftPart;
            if (CursorY >= _rows.Count - 1)
                _rows.Add(rightPart);
            else
                _rows.Insert(CursorY + 1, rightPart);
            MoveDown(view);
            MoveToHome(view);
        }

        private void JumpInHorizontalView(CharacterView view)
        {
            while (CursorX < view.OffsetX)
                view.OffsetX--;
            while (CursorX > view.OffsetX + CharacterView.ZoomLevels.GetCurrentZoom().Columns - 1)
                view.OffsetX++;
        }

        private int GetIndentDepth(int row)
        {
            var s = _rows[row];
            if (string.IsNullOrWhiteSpace(s))
                return s?.Length ?? 0;
            var s2 = s.Trim();
            var result = s.IndexOf(s2, StringComparison.Ordinal);
            return result < 0 ? 0 : result;
        }
    }
}