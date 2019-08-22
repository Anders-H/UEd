using System.IO;

namespace UEd.Recent
{
    public class RecentFile
    {
        private string _filename;
        private string _displayFilename;

        public RecentFile()
        {
        }

        public RecentFile(string filename)
        {
            Filename = filename;
        }

        public string Filename
        {
            get => _filename;
            set
            {
                _displayFilename = null;
                _filename = value;
            }
        }

        public bool Exists
        {
            get
            {
                try
                {
                    return File.Exists(Filename);
                }
                catch
                {
                    return false;
                }
            }
        }

        public string DisplayFilename
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_displayFilename))
                    _displayFilename = Filename.Length > 70
                        ? $"{Filename.Substring(0, 32)}... ...{Filename.Substring(Filename.Length - 32)}"
                        : Filename;
                return _displayFilename;
            }
        }
    }
}