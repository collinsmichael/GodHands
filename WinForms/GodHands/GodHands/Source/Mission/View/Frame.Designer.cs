namespace GodHands {
    partial class Frame {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frame));
            this.menubar = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_open = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_save = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_close = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_undo = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_redo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_disktool = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_logtool = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_configtool = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_customtool = new System.Windows.Forms.ToolStripMenuItem();
            this.toolbar = new System.Windows.Forms.ToolStrip();
            this.tool_open = new System.Windows.Forms.ToolStripButton();
            this.tool_save = new System.Windows.Forms.ToolStripButton();
            this.tool_close = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tool_undo = new System.Windows.Forms.ToolStripButton();
            this.tool_redo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tool_disktool = new System.Windows.Forms.ToolStripButton();
            this.tool_logtool = new System.Windows.Forms.ToolStripButton();
            this.tool_configtool = new System.Windows.Forms.ToolStripButton();
            this.tool_customtool = new System.Windows.Forms.ToolStripButton();
            this.statusstrip = new System.Windows.Forms.StatusStrip();
            this.progressbar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusbar = new System.Windows.Forms.ToolStripStatusLabel();
            this.menu_database = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_monitor = new System.Windows.Forms.ToolStripMenuItem();
            this.tool_database = new System.Windows.Forms.ToolStripButton();
            this.tool_monitor = new System.Windows.Forms.ToolStripButton();
            this.zndtool = new GodHands.ZndTool();
            this.menubar.SuspendLayout();
            this.toolbar.SuspendLayout();
            this.statusstrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menubar
            // 
            this.menubar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menubar.Location = new System.Drawing.Point(0, 0);
            this.menubar.Name = "menubar";
            this.menubar.Size = new System.Drawing.Size(619, 24);
            this.menubar.TabIndex = 0;
            this.menubar.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_open,
            this.menu_save,
            this.toolStripSeparator1,
            this.menu_close,
            this.toolStripSeparator2,
            this.menu_exit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // menu_open
            // 
            this.menu_open.Name = "menu_open";
            this.menu_open.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menu_open.Size = new System.Drawing.Size(166, 22);
            this.menu_open.Text = "Open ...";
            this.menu_open.ToolTipText = "Open a CD Image";
            this.menu_open.Click += new System.EventHandler(this.OnMenu_FileOpen);
            // 
            // menu_save
            // 
            this.menu_save.Name = "menu_save";
            this.menu_save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menu_save.Size = new System.Drawing.Size(166, 22);
            this.menu_save.Text = "Save As ...";
            this.menu_save.ToolTipText = "Save CD Image under a different name";
            this.menu_save.Click += new System.EventHandler(this.OnMenu_FileSaveAs);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(163, 6);
            // 
            // menu_close
            // 
            this.menu_close.Name = "menu_close";
            this.menu_close.Size = new System.Drawing.Size(166, 22);
            this.menu_close.Text = "Close";
            this.menu_close.ToolTipText = "Close CD Image";
            this.menu_close.Click += new System.EventHandler(this.OnMenu_FileClose);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(163, 6);
            // 
            // menu_exit
            // 
            this.menu_exit.Name = "menu_exit";
            this.menu_exit.Size = new System.Drawing.Size(166, 22);
            this.menu_exit.Text = "Exit";
            this.menu_exit.ToolTipText = "Exit Application";
            this.menu_exit.Click += new System.EventHandler(this.OnMenu_FileExit);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_undo,
            this.menu_redo});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // menu_undo
            // 
            this.menu_undo.Name = "menu_undo";
            this.menu_undo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.menu_undo.Size = new System.Drawing.Size(144, 22);
            this.menu_undo.Text = "Undo";
            this.menu_undo.ToolTipText = "Undo last action";
            this.menu_undo.Click += new System.EventHandler(this.OnMenu_EditUndo);
            // 
            // menu_redo
            // 
            this.menu_redo.Name = "menu_redo";
            this.menu_redo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.menu_redo.Size = new System.Drawing.Size(144, 22);
            this.menu_redo.Text = "Redo";
            this.menu_redo.ToolTipText = "Redo last undone action";
            this.menu_redo.Click += new System.EventHandler(this.OnMenu_EditRedo);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_disktool,
            this.menu_database,
            this.menu_monitor,
            this.toolStripSeparator6,
            this.menu_logtool,
            this.menu_configtool,
            this.toolStripSeparator3,
            this.menu_customtool});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // menu_disktool
            // 
            this.menu_disktool.Name = "menu_disktool";
            this.menu_disktool.Size = new System.Drawing.Size(152, 22);
            this.menu_disktool.Text = "Disk Tool ...";
            this.menu_disktool.ToolTipText = "Open Disk Editor Tool";
            this.menu_disktool.Click += new System.EventHandler(this.OnMenu_ToolsDiskTool);
            // 
            // menu_logtool
            // 
            this.menu_logtool.Name = "menu_logtool";
            this.menu_logtool.Size = new System.Drawing.Size(152, 22);
            this.menu_logtool.Text = "Log File ...";
            this.menu_logtool.ToolTipText = "Open Log File";
            this.menu_logtool.Click += new System.EventHandler(this.OnMenu_ToolsLogFile);
            // 
            // menu_configtool
            // 
            this.menu_configtool.Name = "menu_configtool";
            this.menu_configtool.Size = new System.Drawing.Size(152, 22);
            this.menu_configtool.Text = "Options ...";
            this.menu_configtool.ToolTipText = "Open Configuration Editor";
            this.menu_configtool.Click += new System.EventHandler(this.OnMenu_ToolsConfig);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // menu_customtool
            // 
            this.menu_customtool.Name = "menu_customtool";
            this.menu_customtool.Size = new System.Drawing.Size(152, 22);
            this.menu_customtool.Text = "Custom ...";
            this.menu_customtool.ToolTipText = "Open a User Defined Windows Form";
            this.menu_customtool.Click += new System.EventHandler(this.OnMenu_ToolsCustom);
            // 
            // toolbar
            // 
            this.toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tool_open,
            this.tool_save,
            this.tool_close,
            this.toolStripSeparator4,
            this.tool_undo,
            this.tool_redo,
            this.toolStripSeparator5,
            this.tool_disktool,
            this.tool_database,
            this.tool_monitor,
            this.tool_logtool,
            this.tool_configtool,
            this.tool_customtool});
            this.toolbar.Location = new System.Drawing.Point(0, 24);
            this.toolbar.Name = "toolbar";
            this.toolbar.Size = new System.Drawing.Size(619, 25);
            this.toolbar.TabIndex = 1;
            this.toolbar.Text = "toolStrip1";
            // 
            // tool_open
            // 
            this.tool_open.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_open.Image = ((System.Drawing.Image)(resources.GetObject("tool_open.Image")));
            this.tool_open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_open.Name = "tool_open";
            this.tool_open.Size = new System.Drawing.Size(23, 22);
            this.tool_open.Text = "Open";
            this.tool_open.ToolTipText = "Open CD Image";
            this.tool_open.Click += new System.EventHandler(this.OnMenu_FileOpen);
            // 
            // tool_save
            // 
            this.tool_save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_save.Image = ((System.Drawing.Image)(resources.GetObject("tool_save.Image")));
            this.tool_save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_save.Name = "tool_save";
            this.tool_save.Size = new System.Drawing.Size(23, 22);
            this.tool_save.Text = "Save";
            this.tool_save.ToolTipText = "Save CD Image under a different filename";
            this.tool_save.Click += new System.EventHandler(this.OnMenu_FileSaveAs);
            // 
            // tool_close
            // 
            this.tool_close.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_close.Image = ((System.Drawing.Image)(resources.GetObject("tool_close.Image")));
            this.tool_close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_close.Name = "tool_close";
            this.tool_close.Size = new System.Drawing.Size(23, 22);
            this.tool_close.Text = "Close";
            this.tool_close.ToolTipText = "Close CD Image";
            this.tool_close.Click += new System.EventHandler(this.OnMenu_FileClose);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tool_undo
            // 
            this.tool_undo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_undo.Image = ((System.Drawing.Image)(resources.GetObject("tool_undo.Image")));
            this.tool_undo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_undo.Name = "tool_undo";
            this.tool_undo.Size = new System.Drawing.Size(23, 22);
            this.tool_undo.Text = "Undo";
            this.tool_undo.ToolTipText = "Undo last action";
            this.tool_undo.Click += new System.EventHandler(this.OnMenu_EditUndo);
            // 
            // tool_redo
            // 
            this.tool_redo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_redo.Image = ((System.Drawing.Image)(resources.GetObject("tool_redo.Image")));
            this.tool_redo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_redo.Name = "tool_redo";
            this.tool_redo.Size = new System.Drawing.Size(23, 22);
            this.tool_redo.Text = "Redo";
            this.tool_redo.ToolTipText = "Redo last undone action";
            this.tool_redo.Click += new System.EventHandler(this.OnMenu_EditRedo);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // tool_disktool
            // 
            this.tool_disktool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_disktool.Image = ((System.Drawing.Image)(resources.GetObject("tool_disktool.Image")));
            this.tool_disktool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_disktool.Name = "tool_disktool";
            this.tool_disktool.Size = new System.Drawing.Size(23, 22);
            this.tool_disktool.Text = "Disk Tool";
            this.tool_disktool.ToolTipText = "Open Disk Editor Tool";
            this.tool_disktool.Click += new System.EventHandler(this.OnMenu_ToolsDiskTool);
            // 
            // tool_logtool
            // 
            this.tool_logtool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_logtool.Image = ((System.Drawing.Image)(resources.GetObject("tool_logtool.Image")));
            this.tool_logtool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_logtool.Name = "tool_logtool";
            this.tool_logtool.Size = new System.Drawing.Size(23, 22);
            this.tool_logtool.Text = "Log File";
            this.tool_logtool.ToolTipText = "Open Log File";
            this.tool_logtool.Click += new System.EventHandler(this.OnMenu_ToolsLogFile);
            // 
            // tool_configtool
            // 
            this.tool_configtool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_configtool.Image = ((System.Drawing.Image)(resources.GetObject("tool_configtool.Image")));
            this.tool_configtool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_configtool.Name = "tool_configtool";
            this.tool_configtool.Size = new System.Drawing.Size(23, 22);
            this.tool_configtool.Text = "Options";
            this.tool_configtool.ToolTipText = "Open Configuration Editor";
            this.tool_configtool.Click += new System.EventHandler(this.OnMenu_ToolsConfig);
            // 
            // tool_customtool
            // 
            this.tool_customtool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_customtool.Image = ((System.Drawing.Image)(resources.GetObject("tool_customtool.Image")));
            this.tool_customtool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_customtool.Name = "tool_customtool";
            this.tool_customtool.Size = new System.Drawing.Size(23, 22);
            this.tool_customtool.Text = "Custom Tool";
            this.tool_customtool.ToolTipText = "Open a User Defined Windows Form";
            this.tool_customtool.Click += new System.EventHandler(this.OnMenu_ToolsCustom);
            // 
            // statusstrip
            // 
            this.statusstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressbar,
            this.statusbar});
            this.statusstrip.Location = new System.Drawing.Point(0, 424);
            this.statusstrip.Name = "statusstrip";
            this.statusstrip.Size = new System.Drawing.Size(619, 22);
            this.statusstrip.TabIndex = 2;
            this.statusstrip.Text = "statusStrip1";
            // 
            // progressbar
            // 
            this.progressbar.Name = "progressbar";
            this.progressbar.Size = new System.Drawing.Size(100, 16);
            // 
            // statusbar
            // 
            this.statusbar.Name = "statusbar";
            this.statusbar.Size = new System.Drawing.Size(26, 17);
            this.statusbar.Text = "Idle";
            // 
            // menu_database
            // 
            this.menu_database.Name = "menu_database";
            this.menu_database.Size = new System.Drawing.Size(152, 22);
            this.menu_database.Text = "Database ...";
            this.menu_database.ToolTipText = "Open the Database Tool";
            this.menu_database.Click += new System.EventHandler(this.OnMenu_ToolsDatabase);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(149, 6);
            // 
            // menu_monitor
            // 
            this.menu_monitor.Name = "menu_monitor";
            this.menu_monitor.Size = new System.Drawing.Size(152, 22);
            this.menu_monitor.Text = "Monitor ...";
            this.menu_monitor.ToolTipText = "Open the Data Monitor Tool";
            this.menu_monitor.Click += new System.EventHandler(this.OnMenu_ToolsMonitor);
            // 
            // tool_database
            // 
            this.tool_database.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_database.Image = ((System.Drawing.Image)(resources.GetObject("tool_database.Image")));
            this.tool_database.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_database.Name = "tool_database";
            this.tool_database.Size = new System.Drawing.Size(23, 22);
            this.tool_database.Text = "Database";
            this.tool_database.ToolTipText = "Open the Database Tools";
            this.tool_database.Click += new System.EventHandler(this.OnMenu_ToolsDatabase);
            // 
            // tool_monitor
            // 
            this.tool_monitor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_monitor.Image = ((System.Drawing.Image)(resources.GetObject("tool_monitor.Image")));
            this.tool_monitor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_monitor.Name = "tool_monitor";
            this.tool_monitor.Size = new System.Drawing.Size(23, 22);
            this.tool_monitor.Text = "Monitor";
            this.tool_monitor.ToolTipText = "Open the Data Monitor Tool";
            this.tool_monitor.Click += new System.EventHandler(this.OnMenu_ToolsMonitor);
            // 
            // zndtool
            // 
            this.zndtool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zndtool.Location = new System.Drawing.Point(0, 49);
            this.zndtool.Name = "zndtool";
            this.zndtool.Size = new System.Drawing.Size(619, 375);
            this.zndtool.TabIndex = 3;
            // 
            // Frame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 446);
            this.Controls.Add(this.zndtool);
            this.Controls.Add(this.statusstrip);
            this.Controls.Add(this.toolbar);
            this.Controls.Add(this.menubar);
            this.MainMenuStrip = this.menubar;
            this.Name = "Frame";
            this.Text = "GodHands";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
            this.menubar.ResumeLayout(false);
            this.menubar.PerformLayout();
            this.toolbar.ResumeLayout(false);
            this.toolbar.PerformLayout();
            this.statusstrip.ResumeLayout(false);
            this.statusstrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menubar;
        private System.Windows.Forms.ToolStrip toolbar;
        private System.Windows.Forms.StatusStrip statusstrip;
        private System.Windows.Forms.ToolStripProgressBar progressbar;
        private System.Windows.Forms.ToolStripStatusLabel statusbar;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_open;
        private System.Windows.Forms.ToolStripMenuItem menu_save;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menu_close;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menu_exit;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_undo;
        private System.Windows.Forms.ToolStripMenuItem menu_redo;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_disktool;
        private System.Windows.Forms.ToolStripMenuItem menu_logtool;
        private System.Windows.Forms.ToolStripMenuItem menu_configtool;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem menu_customtool;
        private ZndTool zndtool;
        private System.Windows.Forms.ToolStripButton tool_open;
        private System.Windows.Forms.ToolStripButton tool_save;
        private System.Windows.Forms.ToolStripButton tool_close;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tool_undo;
        private System.Windows.Forms.ToolStripButton tool_redo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tool_disktool;
        private System.Windows.Forms.ToolStripButton tool_logtool;
        private System.Windows.Forms.ToolStripButton tool_configtool;
        private System.Windows.Forms.ToolStripButton tool_customtool;
        private System.Windows.Forms.ToolStripMenuItem menu_database;
        private System.Windows.Forms.ToolStripMenuItem menu_monitor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tool_database;
        private System.Windows.Forms.ToolStripButton tool_monitor;
    }
}