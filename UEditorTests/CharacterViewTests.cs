using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UEditor;

namespace UEditorTests
{
    [TestClass]
    public class CharacterViewTests
    {
        [TestMethod]
        public void CanTellIfPointIsInView()
        {
            var view = new CharacterView(new Options());
            view.IsInView(71, 0).Should().BeTrue();
            view.IsInView(80, 0).Should().BeFalse();
            view.IsInView(0, 39).Should().BeTrue();
            view.IsInView(0, 40).Should().BeFalse();
        }

        [TestMethod]
        public void ZoomingAffectsView()
        {
            var view = new CharacterView(new Options());
            view.IsInView(71, 39).Should().BeTrue();
            view.IsInView(80, 40).Should().BeFalse();
            CharacterView.ZoomLevels.ZoomOut();
            view.IsInView(81, 41).Should().BeTrue();
            view.IsInView(82, 42).Should().BeFalse();
            CharacterView.ZoomLevels.ZoomIn();
            view.IsInView(71, 39).Should().BeTrue();
            view.IsInView(80, 40).Should().BeFalse();
        }

        [TestMethod]
        public void CanEnsurePositionVisibilityByPanning()
        {
            var view = new CharacterView(new Options
            {
                ScrollAhead = false
            });
            view.IsInView(300, 400).Should().BeFalse();
            view.OffsetX.Should().Be(0);
            view.OffsetY.Should().Be(0);
            view.EnsurePositionIsVisible(300, 400);
            view.IsInView(300, 400).Should().BeTrue();
            view.OffsetX.Should().Be(220);
            view.OffsetY.Should().Be(360);
        }

        [TestMethod]
        public void CanEnsurePositionVisibilityByPanningWithScrollAhead()
        {
            var view = new CharacterView(new Options
            {
                ScrollAhead = true
            });
            view.IsInView(300, 400).Should().BeFalse();
            view.OffsetX.Should().Be(0);
            view.OffsetY.Should().Be(0);
            view.EnsurePositionIsVisible(300, 400);
            view.IsInView(300, 400).Should().BeTrue();
            view.OffsetX.Should().Be(221);
            view.OffsetY.Should().Be(361);
        }
    }
}