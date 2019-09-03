using System.Collections.Generic;
using System.Linq;

namespace UEditor.Selection
{
    public class SelectionList : List<RowSelection>
    {
        public int StartX { get; set; }
        public int StartY { get; set; }

        public RowSelection GetRowSelection(int y) =>
            this.FirstOrDefault(x => x.LineIndex == y)
                ?? new RowSelection
                {
                    LineIndex = y,
                    SelectionType = SelectionType.NotSelected
                };

        public void StartAt(int x, int y)
        {
            StartX = x;
            StartY = y;
        }

        public void ClearSelection()
        {
            StartAt(-1, -1);
            Clear();
        }
    }
}