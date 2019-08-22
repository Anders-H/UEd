using System;
using System.Collections.Generic;

namespace UEd.Recent
{
    public class RecentFileList : List<RecentFile>
    {
        public void Add(string fullName)
        {
            Insert(0, new RecentFile(fullName));
            if (Count < 1)
                return;
            for (var i = 1; i < Count; i++)
            {
                if (string.Compare(this[i].Filename, fullName, StringComparison.CurrentCultureIgnoreCase) != 0)
                    continue;
                RemoveAt(i);
                break;
            }
            if (Count > 20)
                RemoveAt(Count - 1);
        }
    }
}