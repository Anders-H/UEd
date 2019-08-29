using System.Collections.Generic;

namespace UEditor.Zoom
{
    public class ZoomLevelList : List<ZoomLevel>
    {
        private int _zoomIndex;
        private const int DefaultZoomIndex = 6;

        public ZoomLevelList()
        {
            Add(new ZoomLevel(92, 52, "70%"));
            Add(new ZoomLevel(90, 50, "75%"));
            Add(new ZoomLevel(88, 48, "80%"));
            Add(new ZoomLevel(86, 46, "85%"));
            Add(new ZoomLevel(84, 44, "90%"));
            Add(new ZoomLevel(82, 42, "95%"));
            Add(new ZoomLevel(80, 40, "100%"));
            Add(new ZoomLevel(78, 38, "105%"));
            Add(new ZoomLevel(76, 36, "110%"));
            Add(new ZoomLevel(74, 34, "115%"));
            Add(new ZoomLevel(72, 32, "120%"));
            Add(new ZoomLevel(70, 30, "125%"));
            Add(new ZoomLevel(68, 28, "130%"));
            _zoomIndex = DefaultZoomIndex;
        }

        public bool IsDefault() =>
            _zoomIndex == DefaultZoomIndex;

        public void Restore() =>
            _zoomIndex = DefaultZoomIndex;

        public bool CanZoomIn() =>
            _zoomIndex < Count - 1;

        public bool CanZoomOut() =>
            _zoomIndex > 0;

        public ZoomLevel GetCurrentZoom() =>
            this[_zoomIndex];

        public string GetCurrentZoomName() =>
            this[_zoomIndex].Name;

        public string GetNextZoomInName() =>
            CanZoomIn() ? this[_zoomIndex + 1].Name : "";

        public string GetNextZoomOutName() =>
            CanZoomOut() ? this[_zoomIndex - 1].Name : "";

        public bool ZoomIn()
        {
            if (!CanZoomIn())
                return false;
            _zoomIndex++;
            return true;
        }

        public bool ZoomOut()
        {
            if (!CanZoomOut())
                return false;
            _zoomIndex--;
            return true;
        }
    }
}