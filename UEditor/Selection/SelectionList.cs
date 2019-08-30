using System.Collections.Generic;
using System.Linq;

namespace UEditor.Selection
{
    public class SelectionList : List<RowSelection>
    {
        public RowSelection GetRowSelection(int y) =>
            this.FirstOrDefault(x => x.LineIndex == y)
                ?? new RowSelection
                {
                    LineIndex = y,
                    SelectionType = SelectionType.NotSelected
                };
    }
}