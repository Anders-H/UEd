using System;
using System.Collections.Generic;
using System.IO;

namespace UEd
{
    public class RecentFileManager
    {
        public List<RecentFile> Load()
        {
            var f = GetFilename();
            var result = new List<RecentFile>();
            if (f == null || !f.Exists)
                return result;
            return null;
        }

        private FileInfo GetFilename()
        {
            var filename = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    @"UEd\RecentFiles.txt");
            try
            {
                var file = new FileInfo(filename);
                var dir = file.Directory;
                if (dir == null)
                    return null;
                if (!dir.Exists)
                    dir.Create();
                return file;
            }
            catch
            {
                return null;
            }
        }
    }
}