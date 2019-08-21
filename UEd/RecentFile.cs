using System.IO;

namespace UEd
{
    public class RecentFile
    {
        private string _filename;
        private string _displayFilename;

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
                    _displayFilename = Filename.Length > 50
                        ? $"{Filename.Substring(0, 20)}... ...{Filename.Substring(Filename.Length - 20)}"
                        : Filename;
                return _displayFilename;
            }
        }
    }
}