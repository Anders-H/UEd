using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using UEd.Recent;

namespace UEd
{
    public partial class MainWindow : Form
    {
        private Font _font;
        private readonly CharacterArea _area = new CharacterArea();
        private readonly CharacterView _view = new CharacterView();
        private readonly InputHandler _inputHandler = new InputHandler();
        private readonly RecentFileManager _recentFileManager = new RecentFileManager();
        private RecentFileList _recentFiles = new RecentFileList();
        private bool _recalcFontSize = true;
        private int ViewportWidth => ClientRectangle.Width;
        private int ViewportHeight => ClientRectangle.Height - menuStrip1.Height;
        private string _filename;
        private bool _changed;

        public MainWindow()
        {
            InitializeComponent();
        }

        private string Filename
        {
            get => _filename;
            set
            {
                _filename = value;
                UpdateWindowText();
            }
        }

        private bool Changed
        {
            get => _changed;
            set
            {
                _changed = value;
                UpdateWindowText();
            }
        }

        private void UpdateWindowText() =>
            Text = string.IsNullOrWhiteSpace(_filename)
                ? $"U-Ed{(Changed ? "*" : "")}"
                : $"U-Ed - {Filename}{(Changed ? "*" : "")}";

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
            if (_inputHandler.KeyPress(e.KeyChar, _area, _view))
                Changed = true;
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

        private void NewDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Changed = false;
            Filename = "";
            _area.Clear();
            Invalidate();
        }

        private void OpenDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: Save.
            const string title = "Open document";
            using (var x = new OpenFileDialog())
            {
                x.Title = $@"{title} (max 100 Mb)";
                x.Filter = @"All files (*.*)|*.*";
                if (x.ShowDialog() != DialogResult.OK)
                    return;
                OpenDocument(x.FileName, x.Title);
            }
        }

        private void OpenDocument(string filename, string title)
        {
            var fi = new FileInfo(filename);
            if (fi.Length > 104857600)
            {
                MessageBox.Show(@"The file is too large. Maximum size is 100 Mb.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Cursor = Cursors.WaitCursor;
            string fileContent;
            try
            {
                using (var sr = new StreamReader(fi.FullName, Encoding.UTF8))
                {
                    fileContent = sr.ReadToEnd();
                    sr.Close();
                }
            }
            catch
            {
                Cursor = Cursors.Default;
                MessageBox.Show(@"The file is too large. Maximum size is 100 Mb.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _area.SetData(fileContent);
            _view.OffsetX = 0;
            _view.OffsetY = 0;
            Filename = fi.FullName;
            Changed = false;
            Invalidate();
            _recentFiles.Add(fi.FullName);
            ReconstructRecentDocumentList();
            Cursor = Cursors.Default;
        }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: Save.
            Close();
        }

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            _recentFiles = _recentFileManager.Load();
            ReconstructRecentDocumentList();
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e) =>
            _recentFileManager.Save(_recentFiles);

        private void ReconstructRecentDocumentList()
        {
            openRecentToolStripMenuItem.DropDownItems.Clear();
            if (_recentFiles.Count <= 0)
            {
                openRecentToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem
                {
                    Text = @"No recent files",
                    Enabled = false
                });
                return;
            }
            foreach (var r in _recentFiles)
            {
                var recentMenu = new ToolStripMenuItem
                {
                    Tag = r,
                    Text = r.DisplayFilename
                };
                recentMenu.Click += ClickOpenRecent;
                openRecentToolStripMenuItem.DropDownItems.Add(recentMenu);
            }
        }

        private void ClickOpenRecent(object sender, EventArgs e)
        {
            //TODO: Save.
            var m = (ToolStripMenuItem)sender;
            var f = (RecentFile)m.Tag;
            if (f.Exists)
            {
                OpenDocument(f.Filename, "Open recent");
                return;
            }
            _recentFiles.Remove(f);
            ReconstructRecentDocumentList();
        }
    }
}