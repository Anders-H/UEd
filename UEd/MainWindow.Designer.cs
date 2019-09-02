﻿namespace UEd
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.openDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openRecentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreZoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.scrollLeftCtrlLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scrollRightCtrlRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scrollUpCtrlUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scrollDownCtrlDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scrollIntoViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(884, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newDocumentToolStripMenuItem,
            this.toolStripMenuItem1,
            this.openDocumentToolStripMenuItem,
            this.openRecentToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripMenuItem2,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newDocumentToolStripMenuItem
            // 
            this.newDocumentToolStripMenuItem.Name = "newDocumentToolStripMenuItem";
            this.newDocumentToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newDocumentToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.newDocumentToolStripMenuItem.Text = "&New document...";
            this.newDocumentToolStripMenuItem.Click += new System.EventHandler(this.NewDocumentToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(199, 6);
            // 
            // openDocumentToolStripMenuItem
            // 
            this.openDocumentToolStripMenuItem.Name = "openDocumentToolStripMenuItem";
            this.openDocumentToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openDocumentToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.openDocumentToolStripMenuItem.Text = "&Open document...";
            this.openDocumentToolStripMenuItem.Click += new System.EventHandler(this.OpenDocumentToolStripMenuItem_Click);
            // 
            // openRecentToolStripMenuItem
            // 
            this.openRecentToolStripMenuItem.Name = "openRecentToolStripMenuItem";
            this.openRecentToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.openRecentToolStripMenuItem.Text = "Open recent";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.saveAsToolStripMenuItem.Text = "Save as...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(199, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.QuitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scrollLeftCtrlLeftToolStripMenuItem,
            this.scrollRightCtrlRightToolStripMenuItem,
            this.scrollUpCtrlUpToolStripMenuItem,
            this.scrollDownCtrlDownToolStripMenuItem,
            this.toolStripMenuItem4,
            this.scrollIntoViewToolStripMenuItem,
            this.toolStripMenuItem3,
            this.zoomInToolStripMenuItem,
            this.restoreZoomToolStripMenuItem,
            this.zoomOutToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.viewToolStripMenuItem.Text = "&View";
            this.viewToolStripMenuItem.DropDownOpened += new System.EventHandler(this.viewToolStripMenuItem_DropDownOpened);
            // 
            // zoomInToolStripMenuItem
            // 
            this.zoomInToolStripMenuItem.Name = "zoomInToolStripMenuItem";
            this.zoomInToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Oemplus)));
            this.zoomInToolStripMenuItem.ShowShortcutKeys = false;
            this.zoomInToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.zoomInToolStripMenuItem.Text = "Zoom in (Ctrl+)";
            this.zoomInToolStripMenuItem.Click += new System.EventHandler(this.zoomInToolStripMenuItem_Click);
            // 
            // restoreZoomToolStripMenuItem
            // 
            this.restoreZoomToolStripMenuItem.Name = "restoreZoomToolStripMenuItem";
            this.restoreZoomToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)));
            this.restoreZoomToolStripMenuItem.ShowShortcutKeys = false;
            this.restoreZoomToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.restoreZoomToolStripMenuItem.Text = "Restore zoom to 100% (Ctrl 0)";
            this.restoreZoomToolStripMenuItem.Click += new System.EventHandler(this.restoreZoomToolStripMenuItem_Click);
            // 
            // zoomOutToolStripMenuItem
            // 
            this.zoomOutToolStripMenuItem.Name = "zoomOutToolStripMenuItem";
            this.zoomOutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.OemMinus)));
            this.zoomOutToolStripMenuItem.ShowShortcutKeys = false;
            this.zoomOutToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.zoomOutToolStripMenuItem.Text = "Zoom out (Ctrl -)";
            this.zoomOutToolStripMenuItem.Click += new System.EventHandler(this.zoomOutToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(212, 6);
            // 
            // scrollLeftCtrlLeftToolStripMenuItem
            // 
            this.scrollLeftCtrlLeftToolStripMenuItem.Name = "scrollLeftCtrlLeftToolStripMenuItem";
            this.scrollLeftCtrlLeftToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.scrollLeftCtrlLeftToolStripMenuItem.Text = "Scroll left (Ctrl Left)";
            this.scrollLeftCtrlLeftToolStripMenuItem.Click += new System.EventHandler(this.ScrollLeftCtrlLeftToolStripMenuItem_Click);
            // 
            // scrollRightCtrlRightToolStripMenuItem
            // 
            this.scrollRightCtrlRightToolStripMenuItem.Name = "scrollRightCtrlRightToolStripMenuItem";
            this.scrollRightCtrlRightToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.scrollRightCtrlRightToolStripMenuItem.Text = "Scroll right (Ctrl Right)";
            this.scrollRightCtrlRightToolStripMenuItem.Click += new System.EventHandler(this.ScrollRightCtrlRightToolStripMenuItem_Click);
            // 
            // scrollUpCtrlUpToolStripMenuItem
            // 
            this.scrollUpCtrlUpToolStripMenuItem.Name = "scrollUpCtrlUpToolStripMenuItem";
            this.scrollUpCtrlUpToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.scrollUpCtrlUpToolStripMenuItem.Text = "Scroll up (Ctrl Up)";
            this.scrollUpCtrlUpToolStripMenuItem.Click += new System.EventHandler(this.ScrollUpCtrlUpToolStripMenuItem_Click);
            // 
            // scrollDownCtrlDownToolStripMenuItem
            // 
            this.scrollDownCtrlDownToolStripMenuItem.Name = "scrollDownCtrlDownToolStripMenuItem";
            this.scrollDownCtrlDownToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.scrollDownCtrlDownToolStripMenuItem.Text = "Scroll down (Ctrl Down)";
            this.scrollDownCtrlDownToolStripMenuItem.Click += new System.EventHandler(this.ScrollDownCtrlDownToolStripMenuItem_Click);
            // 
            // scrollIntoViewToolStripMenuItem
            // 
            this.scrollIntoViewToolStripMenuItem.Name = "scrollIntoViewToolStripMenuItem";
            this.scrollIntoViewToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.scrollIntoViewToolStripMenuItem.Text = "Scroll into view (Ctrl P)";
            this.scrollIntoViewToolStripMenuItem.Click += new System.EventHandler(this.ScrollIntoViewToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(212, 6);
            // 
            // MainWindow
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(500, 300);
            this.Name = "MainWindow";
            this.Text = "U-Ed";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.Shown += new System.EventHandler(this.MainWindow_Shown);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainWindow_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainWindow_DragEnter);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreZoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newDocumentToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openDocumentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openRecentToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scrollLeftCtrlLeftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scrollRightCtrlRightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scrollUpCtrlUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scrollDownCtrlDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem scrollIntoViewToolStripMenuItem;
    }
}

