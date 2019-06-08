using System;
using System.Drawing;
using System.Windows.Forms;

namespace UEd
{
    public partial class MainWindow : Form
    {
        private Font _font;
        private readonly CharacterArea _area = new CharacterArea();
        private readonly CharacterView _view = new CharacterView();
        private readonly InputHandler _inputHandler = new InputHandler();
        private bool _recalcFontSize = true;
        private int ViewportWidth => ClientRectangle.Width;
        private int ViewportHeight => ClientRectangle.Height - menuStrip1.Height;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            _inputHandler.KeyDown(e.KeyCode, _area, _view);
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (_recalcFontSize)
                RecalcFontSize(e.Graphics);
            _view.Draw(
                _area,
                e.Graphics,
                0,
                menuStrip1.Height,
                ViewportWidth,
                ViewportHeight,
                _font);
        }

        private void RecalcFontSize(Graphics g)
        {
            var size = 3f;
            var charWidth = ViewportWidth / (double)CharacterView.ZoomLevels.GetCurrentZoom().Columns;
            var charHeight = ViewportHeight / (double)CharacterView.ZoomLevels.GetCurrentZoom().Rows;
            if (!FontFitsInSquare(g, size, charWidth, charHeight))
            {
                SetFontSize(size);
                return;
            }
            var retryCount = 0;
            do
            {
                size += 0.1f;
                if (!FontFitsInSquare(g, size, charWidth, charHeight))
                {
                    SetFontSize(size + 1f);
                    return;
                }
                retryCount++;
            } while (retryCount < 200);
            SetFontSize(size);
        }

        private static bool FontFitsInSquare(Graphics g, double size, double width, double height)
        {
            using (var f = new Font(FontFamily.GenericMonospace, (float)size))
            {
                var z = g.MeasureString("l", f);
                if (z.Width > width || z.Height > height)
                    return false;
            }
            return true;
        }

        private void SetFontSize(double size)
        {
            _font?.Dispose();
            _font = new Font(FontFamily.GenericMonospace, (float)size);
            _recalcFontSize = false;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            _recalcFontSize = true;
            Invalidate();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            _inputHandler.KeyPress(e.KeyChar, _area, _view);
            Invalidate();
        }

        private void viewToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            var inName = CharacterView.ZoomLevels.GetNextZoomInName();
            if (string.IsNullOrWhiteSpace(inName))
            {
                zoomInToolStripMenuItem.Text = @"Zoom in (Ctrl +)";
                zoomInToolStripMenuItem.Enabled = false;
            }
            else
            {
                zoomInToolStripMenuItem.Text = $@"Zoom in to {inName} (Ctrl +)";
                zoomInToolStripMenuItem.Enabled = true;
            }
            if (CharacterView.ZoomLevels.IsDefault())
            {
                restoreZoomToolStripMenuItem.Text = @"Restore zoom to 100% (Ctrl 0)";
                restoreZoomToolStripMenuItem.Enabled = false;
            }
            else
            {
                restoreZoomToolStripMenuItem.Text = $@"Restore zoom ({CharacterView.ZoomLevels.GetCurrentZoomName()}) to 100% (Ctrl 0)";
                restoreZoomToolStripMenuItem.Enabled = true;

            }
            var outName = CharacterView.ZoomLevels.GetNextZoomOutName();
            if (string.IsNullOrWhiteSpace(outName))
            {
                zoomOutToolStripMenuItem.Text = @"Zoom out (Ctrl -)";
                zoomOutToolStripMenuItem.Enabled = false;
            }
            else
            {
                zoomOutToolStripMenuItem.Text = $@"Zoom out to {outName} (Ctrl -)";
                zoomOutToolStripMenuItem.Enabled = true;
            }
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CharacterView.ZoomLevels.ZoomIn();
            _view.EnsurePositionIsVisible(_area.CursorX, _area.CursorY);
            _recalcFontSize = true;
            Invalidate();
        }

        private void restoreZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CharacterView.ZoomLevels.Restore();
            _view.EnsurePositionIsVisible(_area.CursorX, _area.CursorY);
            _recalcFontSize = true;
            Invalidate();
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CharacterView.ZoomLevels.ZoomOut();
            _view.EnsurePositionIsVisible(_area.CursorX, _area.CursorY);
            _recalcFontSize = true;
            Invalidate();
        }
    }
}