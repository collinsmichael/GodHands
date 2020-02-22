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
            this.statusstrip = new System.Windows.Forms.StatusStrip();
            this.progressbar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusbar = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_log = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_config = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_custom = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tool_open = new System.Windows.Forms.ToolStripButton();
            this.tool_save = new System.Windows.Forms.ToolStripButton();
            this.tool_close = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tool_undo = new System.Windows.Forms.ToolStripButton();
            this.tool_redo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tool_log = new System.Windows.Forms.ToolStripButton();
            this.tool_config = new System.Windows.Forms.ToolStripButton();
            this.tool_custom = new System.Windows.Forms.ToolStripButton();
            this.splitter = new GodHands.FlickerFreeSplitter();
            this.treeview = new GodHands.DiskView();
            this.property = new GodHands.PropertyView();
            this.statusstrip.SuspendLayout();
            this.menubar.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).BeginInit();
            this.splitter.Panel1.SuspendLayout();
            this.splitter.Panel2.SuspendLayout();
            this.splitter.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusstrip
            // 
            this.statusstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressbar,
            this.statusbar});
            this.statusstrip.Location = new System.Drawing.Point(0, 387);
            this.statusstrip.Name = "statusstrip";
            this.statusstrip.Size = new System.Drawing.Size(497, 22);
            this.statusstrip.TabIndex = 0;
            this.statusstrip.Text = "statusStrip1";
            // 
            // progressbar
            // 
            this.progressbar.Margin = new System.Windows.Forms.Padding(3);
            this.progressbar.Name = "progressbar";
            this.progressbar.Size = new System.Drawing.Size(100, 16);
            // 
            // statusbar
            // 
            this.statusbar.Margin = new System.Windows.Forms.Padding(3);
            this.statusbar.Name = "statusbar";
            this.statusbar.Size = new System.Drawing.Size(26, 16);
            this.statusbar.Text = "Idle";
            // 
            // menubar
            // 
            this.menubar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menubar.Location = new System.Drawing.Point(0, 0);
            this.menubar.Name = "menubar";
            this.menubar.Size = new System.Drawing.Size(497, 24);
            this.menubar.TabIndex = 2;
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
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_log,
            this.menu_config,
            this.menu_custom});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // menu_log
            // 
            this.menu_log.Name = "menu_log";
            this.menu_log.Size = new System.Drawing.Size(143, 22);
            this.menu_log.Text = "Log File";
            this.menu_log.ToolTipText = "Open Log File";
            // 
            // menu_config
            // 
            this.menu_config.Name = "menu_config";
            this.menu_config.Size = new System.Drawing.Size(143, 22);
            this.menu_config.Text = "Options";
            this.menu_config.ToolTipText = "Open Options Menu";
            // 
            // menu_custom
            // 
            this.menu_custom.Name = "menu_custom";
            this.menu_custom.Size = new System.Drawing.Size(143, 22);
            this.menu_custom.Text = "Custom Tool";
            this.menu_custom.ToolTipText = "Open Custom Tool";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tool_open,
            this.tool_save,
            this.tool_close,
            this.toolStripSeparator3,
            this.tool_undo,
            this.tool_redo,
            this.toolStripSeparator4,
            this.tool_log,
            this.tool_config,
            this.tool_custom});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(497, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tool_open
            // 
            this.tool_open.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_open.Image = ((System.Drawing.Image)(resources.GetObject("tool_open.Image")));
            this.tool_open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_open.Name = "tool_open";
            this.tool_open.Size = new System.Drawing.Size(23, 22);
            this.tool_open.Text = "Open";
            this.tool_open.ToolTipText = "Open a CD Image";
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
            this.tool_save.ToolTipText = "Save CD Image under a different name";
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
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tool_log
            // 
            this.tool_log.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_log.Image = ((System.Drawing.Image)(resources.GetObject("tool_log.Image")));
            this.tool_log.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_log.Name = "tool_log";
            this.tool_log.Size = new System.Drawing.Size(23, 22);
            this.tool_log.Text = "Log File";
            this.tool_log.ToolTipText = "Open Log File";
            // 
            // tool_config
            // 
            this.tool_config.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_config.Image = ((System.Drawing.Image)(resources.GetObject("tool_config.Image")));
            this.tool_config.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_config.Name = "tool_config";
            this.tool_config.Size = new System.Drawing.Size(23, 22);
            this.tool_config.Text = "Options";
            this.tool_config.ToolTipText = "Open Options Menu";
            // 
            // tool_custom
            // 
            this.tool_custom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tool_custom.Image = ((System.Drawing.Image)(resources.GetObject("tool_custom.Image")));
            this.tool_custom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_custom.Name = "tool_custom";
            this.tool_custom.Size = new System.Drawing.Size(23, 22);
            this.tool_custom.Text = "Custom Tool";
            this.tool_custom.ToolTipText = "Open Custom Tool";
            // 
            // splitter
            // 
            this.splitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitter.Location = new System.Drawing.Point(0, 49);
            this.splitter.Name = "splitter";
            // 
            // splitter.Panel1
            // 
            this.splitter.Panel1.Controls.Add(this.treeview);
            // 
            // splitter.Panel2
            // 
            this.splitter.Panel2.Controls.Add(this.property);
            this.splitter.Size = new System.Drawing.Size(497, 338);
            this.splitter.SplitterDistance = 200;
            this.splitter.TabIndex = 5;
            // 
            // treeview
            // 
            this.treeview.AllowDrop = true;
            this.treeview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeview.Location = new System.Drawing.Point(0, 0);
            this.treeview.Name = "treeview";
            this.treeview.Size = new System.Drawing.Size(200, 338);
            this.treeview.TabIndex = 3;
            this.treeview.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnTree_NodeSelected);
            this.treeview.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.OnTree_NodeClicked);
            // 
            // property
            // 
            this.property.Dock = System.Windows.Forms.DockStyle.Fill;
            this.property.Location = new System.Drawing.Point(0, 0);
            this.property.Name = "property";
            this.property.Size = new System.Drawing.Size(293, 338);
            this.property.TabIndex = 0;
            // 
            // Frame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 409);
            this.Controls.Add(this.splitter);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menubar);
            this.Controls.Add(this.statusstrip);
            this.Name = "Frame";
            this.Text = "Disk Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
            this.Load += new System.EventHandler(this.OnLoad);
            this.statusstrip.ResumeLayout(false);
            this.statusstrip.PerformLayout();
            this.menubar.ResumeLayout(false);
            this.menubar.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitter.Panel1.ResumeLayout(false);
            this.splitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitter)).EndInit();
            this.splitter.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusstrip;
        private System.Windows.Forms.ToolStripProgressBar progressbar;
        private System.Windows.Forms.ToolStripStatusLabel statusbar;
        private System.Windows.Forms.MenuStrip menubar;
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
        private DiskView treeview;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tool_open;
        private System.Windows.Forms.ToolStripButton tool_save;
        private System.Windows.Forms.ToolStripButton tool_close;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tool_undo;
        private System.Windows.Forms.ToolStripButton tool_redo;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_log;
        private System.Windows.Forms.ToolStripMenuItem menu_config;
        private System.Windows.Forms.ToolStripMenuItem menu_custom;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tool_log;
        private System.Windows.Forms.ToolStripButton tool_config;
        private System.Windows.Forms.ToolStripButton tool_custom;
        private FlickerFreeSplitter splitter;
        private PropertyView property;
    }
}