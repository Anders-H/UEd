using System;

namespace UEditor.Selection
{
    public class RowSelection
    {
        public int LineIndex { get; set; }
        public SelectionType SelectionType { get; set; }
        public int RangeStart { get; set; }
        public int RangeLength { get; set; }

        public bool CharacterIsSelected(int x)
        {
            switch (SelectionType)
            {
                case SelectionType.NotSelected:
                    return false;
                case SelectionType.FullRow:
                    return true;
                case SelectionType.Range:
                    return x >= RangeStart && x < RangeStart + RangeLength;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}