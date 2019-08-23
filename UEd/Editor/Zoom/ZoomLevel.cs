namespace UEd.Editor.Zoom
{
    public class ZoomLevel
    {
        public int Columns { get; }
        public int Rows { get; }
        public string Name { get; }

        public ZoomLevel(int columns, int rows, string name)
        {
            Columns = columns;
            Rows = rows;
            Name = name;
        }
    }
}