using System;
using System.IO;
using System.Text;

namespace UEd.Recent
{
    public class RecentFileManager
    {
        public RecentFileList Load()
        {
            var f = GetFilename();
            var result = new RecentFileList();
            if (f == null || !f.Exists)
                return result;
            try
            {
                using (var sr = new StreamReader(f.FullName, Encoding.UTF8))
                {
                    while (!sr.EndOfStream)
                    {
                        var recentFile = (sr.ReadLine() ?? "").Trim();
                        if (string.IsNullOrWhiteSpace(recentFile))
                            continue;
                        var recent = new RecentFile(recentFile);
                        if (recent.Exists)
                            result.Add(recent);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Load recent files: {ex.Message}");
            }
            return result;
        }

        public void Save(RecentFileList list)
        {
            var f = GetFilename();
            if (f == null)
                return;
            try
            {
                using (var sw = new StreamWriter(f.FullName, false, Encoding.UTF8))
                {
                    foreach (var l in list)
                        sw.WriteLine(l.Filename);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Save recent files: {ex.Message}");
            }
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