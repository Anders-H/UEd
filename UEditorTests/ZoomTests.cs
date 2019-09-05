using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UEditor.Zoom;

namespace UEditorTests
{
    [TestClass]
    public class ZoomTests
    {
        [TestMethod]
        public void ZoomListCanIdentifyDefaultAndRestore()
        {
            var list = new ZoomLevelList();
            list.IsDefault().Should().BeTrue();
            list.GetCurrentZoomName().Should().Be("100%");
            list.ZoomOut();
            list.IsDefault().Should().BeFalse();
            list.GetCurrentZoomName().Should().Be("95%");
            list.Restore();
            list.IsDefault().Should().BeTrue();
            list.GetCurrentZoomName().Should().Be("100%");
        }

        [TestMethod]
        public void CanZoomInAndCanZoomOut()
        {
            var list = new ZoomLevelList();
            list.CanZoomOut().Should().BeTrue();
            list.CanZoomIn().Should().BeTrue();
            list.GetCurrentZoomName().Should().Be("100%");
            list.GetNextZoomInName().Should().Be("105%");
            list.GetNextZoomOutName().Should().Be("95%");
            Times(5, list.ZoomOut);
            list.CanZoomOut().Should().BeTrue();
            list.CanZoomIn().Should().BeTrue();
            list.GetCurrentZoomName().Should().Be("75%");
            list.GetNextZoomInName().Should().Be("80%");
            list.GetNextZoomOutName().Should().Be("70%");
            list.ZoomOut();
            list.CanZoomOut().Should().BeFalse();
            list.CanZoomIn().Should().BeTrue();
            list.GetCurrentZoomName().Should().Be("70%");
            list.GetNextZoomInName().Should().Be("75%");
            list.GetNextZoomOutName().Should().Be("");
            Times(11, list.ZoomIn);
            list.CanZoomOut().Should().BeTrue();
            list.CanZoomIn().Should().BeTrue();
            list.GetCurrentZoomName().Should().Be("125%");
            list.GetNextZoomInName().Should().Be("130%");
            list.GetNextZoomOutName().Should().Be("120%");
            list.ZoomIn();
            list.CanZoomOut().Should().BeTrue();
            list.CanZoomIn().Should().BeFalse();
            list.GetCurrentZoomName().Should().Be("130%");
            list.GetNextZoomInName().Should().Be("");
            list.GetNextZoomOutName().Should().Be("125%");
        }

        [TestMethod]
        public void CanNotZoomOutsideOfLimit()
        {
            var list = new ZoomLevelList();
            Times(5, list.ZoomOut);
            list.CanZoomOut().Should().BeTrue();
            list.GetCurrentZoomName().Should().Be("75%");
            list.ZoomOut();
            list.CanZoomOut().Should().BeFalse();
            list.GetCurrentZoomName().Should().Be("70%");
            list.ZoomOut();
            list.CanZoomOut().Should().BeFalse();
            list.GetCurrentZoomName().Should().Be("70%");
            Times(11, list.ZoomIn);
            list.CanZoomIn().Should().BeTrue();
            list.GetCurrentZoomName().Should().Be("125%");
            list.ZoomIn();
            list.CanZoomIn().Should().BeFalse();
            list.GetCurrentZoomName().Should().Be("130%");
            list.ZoomIn();
            list.CanZoomIn().Should().BeFalse();
            list.GetCurrentZoomName().Should().Be("130%");
        }

        private static void Times(int count, Func<bool> f)
        {
            for (var i = 0; i < count; i++)
                _ = f.Invoke();
        }
    }
}