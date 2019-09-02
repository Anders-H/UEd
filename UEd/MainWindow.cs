using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using UEd.Recent;
using UEditor;

namespace UEd
{
    public partial class MainWindow : Form
    {
        private static readonly IOptions Options = new Options();
        private readonly CharacterArea _area = new CharacterArea(Options);
        private readonly CharacterView _view = new CharacterView(Options);
        private readonly InputHandler _inputHandler = new InputHandler(Options);
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
                Font,
                0,
                menuStrip1.Height,
                ViewportWidth,
                ViewportHeight);
        }

        private void RecalcFontSize(Graphics g)
        {
            const double scale = 1.5;
            var size = 3f;
            var charWidth = ViewportWidth / (double) CharacterView.ZoomLevels.GetCurrentZoom().Columns;
            var charHeight = ViewportHeight / (double) CharacterView.ZoomLevels.GetCurrentZoom().Rows;
            if (!FontFitsInSquare(g, size, charWidth, charHeight))
            {
                SetFontSize(size * scale);
                return;
            }

            var retryCount = 0;
            do
            {
                size += 0.1f;
                if (!FontFitsInSquare(g, size, charWidth, charHeight))
                {
                    SetFontSize(size * scale);
                    return;
                }

                retryCount++;
            } while (retryCount < 200);

            SetFontSize(size * scale);
        }

        private static bool FontFitsInSquare(Graphics g, double size, double width, double height)
        {
            using (var f = new Font(FontFamily.GenericMonospace, (float) size))
            {
                var z = g.MeasureString("l", f);
                if (z.Width > width || z.Height > height)
                    return false;
            }
            return true;
        }

        private void SetFontSize(double size)
        {
            _view.Font?.Dispose();
            _view.Font = new Font(FontFamily.GenericMonospace, (float) size);
            _recalcFontSize = false;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            _recalcFontSize = true;
            Invalidate();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up | Keys.Control:
                    ScrollUpCtrlUpToolStripMenuItem_Click(null, null);
                    return true;
                case Keys.Down | Keys.Control:
                    ScrollDownCtrlDownToolStripMenuItem_Click(null, null);
                    return true;
                case Keys.Left | Keys.Control:
                    ScrollLeftCtrlLeftToolStripMenuItem_Click(null, null);
                    return true;
                case Keys.Right | Keys.Control:
                    ScrollRightCtrlRightToolStripMenuItem_Click(null, null);
                    return true;
                case Keys.P | Keys.Control:
                    ScrollIntoViewToolStripMenuItem_Click(null, null);
                    return true;
                case Keys.Tab | Keys.Shift:
                    _inputHandler.Outdent(_area, _view);
                    Changed = true;
                    Invalidate();
                    return true;
                case Keys.Tab:
                    _inputHandler.Indent(_area, _view);
                    Changed = true;
                    Invalidate();
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (_inputHandler.KeyPress(e.KeyChar, _area, _view))
                Changed = true;
            Invalidate();
        }

        private void viewToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            EnableScrollItems();
            EnableZoomItems();
        }

        private void EnableScrollItems()
        {
            scrollIntoViewToolStripMenuItem.Enabled = !_view.IsInView(_area.CursorX, _area.CursorY);
            scrollLeftCtrlLeftToolStripMenuItem.Enabled = _view.OffsetX > 0;
            scrollUpCtrlUpToolStripMenuItem.Enabled = _view.OffsetY > 0;
        }

        private void EnableZoomItems()
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
                restoreZoomToolStripMenuItem.Text =
                    $@"Restore zoom ({CharacterView.ZoomLevels.GetCurrentZoomName()}) to 100% (Ctrl 0)";
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
            const string title = "Open document";
            if (Changed)
            {
                if (PromptSave(title) == DialogResult.Cancel)
                    return;
            }

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
                MessageBox.Show(@"The file is too large. Maximum size is 100 Mb.", title, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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
                MessageBox.Show(@"The file is too large. Maximum size is 100 Mb.", title, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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

        public DialogResult PromptSave(string title)
        {
            var response = MessageBox.Show(
                @"Do you want to save your document first?",
                title,
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1);
            if (response == DialogResult.Yes)
                return SaveDocument() ? DialogResult.Yes : DialogResult.Cancel;
            return response;
        }

        public bool SaveDocument()
        {
            if (string.IsNullOrWhiteSpace(Filename))
                return SaveDocumentAs();
            Cursor = Cursors.WaitCursor;
            try
            {
                using (var sw = new StreamWriter(Filename, false, Encoding.UTF8))
                {
                    sw.Write(_area.GetData());
                    sw.Flush();
                    sw.Close();
                }
                Changed = false;
                Cursor = Cursors.Default;
                return true;
            }
            catch
            {
                Cursor = Cursors.Default;
                MessageBox.Show(
                    @"The document could not be saved.",
                    @"Save failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }

        private bool SaveDocumentAs()
        {
            using (var x = new SaveFileDialog())
            {
                x.Title = @"Save document";
                x.Filter = @"All files (*.*)|*.*";
                if (x.ShowDialog() != DialogResult.OK)
                    return false;
                Cursor = Cursors.WaitCursor;
                try
                {
                    using (var sw = new StreamWriter(x.FileName, false, Encoding.UTF8))
                    {
                        sw.Write(_area.GetData());
                        sw.Flush();
                        sw.Close();
                    }
                    Changed = false;
                    Filename = x.FileName;
                    _recentFiles.Add(x.FileName);
                    ReconstructRecentDocumentList();
                    Cursor = Cursors.Default;
                    return true;
                }
                catch
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show(
                        @"The document could not be saved.",
                        @"Save failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private void QuitToolStripMenuItem_Click(object sender, EventArgs e) =>
            Close();

        private void MainWindow_Shown(object sender, EventArgs e)
        {
            _recentFiles = _recentFileManager.Load();
            ReconstructRecentDocumentList();
            Refresh();
            var args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                try
                {
                    var fi = new FileInfo(args[1]);
                    if (fi.Exists)
                    {
                        OpenDocument(fi.FullName, Text);
                        return;
                    }

                    if (MessageBox.Show($@"The file ""{fi.FullName}"" does not exist. Do you want to create it?", Text,
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        Filename = fi.FullName;
                }
                catch
                {
                    MessageBox.Show(
                        $@"The file ""{args[0]}"" could not be loaded.",
                        Text,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
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
            const string title = "Open recent";
            if (Changed)
            {
                if (PromptSave(title) == DialogResult.Cancel)
                    return;
            }
            var m = (ToolStripMenuItem) sender;
            var f = (RecentFile) m.Tag;
            if (f.Exists)
            {
                OpenDocument(f.Filename, title);
                return;
            }

            _recentFiles.Remove(f);
            ReconstructRecentDocumentList();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e) =>
            SaveDocument();

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Changed)
                return;
            if (PromptSave("Quit") == DialogResult.Cancel)
                e.Cancel = true;
        }

        private void MainWindow_DragEnter(object sender, DragEventArgs e) =>
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop, false)
                ? DragDropEffects.Copy
                : DragDropEffects.None;

        private void MainWindow_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop, false))
                return;
            var files = (string[])e.Data.GetData("FileDrop", false);
            if (files.Length <= 0)
                return;
            const string title = "Open document";
            if (Changed)
            {
                if (PromptSave(title) == DialogResult.Cancel)
                    return;
            }
            OpenDocument(files[0], title);
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e) =>
            SaveDocumentAs();

        private void ScrollLeftCtrlLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_view.OffsetX > 0)
                _view.OffsetX--;
            Invalidate();
        }

        private void ScrollRightCtrlRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _view.OffsetX++;
            Invalidate();
        }

        private void ScrollUpCtrlUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_view.OffsetY > 0)
                _view.OffsetY--;
            Invalidate();
        }

        private void ScrollDownCtrlDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _view.OffsetY++;
            Invalidate();
        }

        private void ScrollIntoViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _view.EnsurePositionIsVisible(_area.CursorX, _area.CursorY);
            Invalidate();
        }
    }
}