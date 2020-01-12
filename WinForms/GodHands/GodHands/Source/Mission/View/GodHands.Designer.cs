namespace GodHands {
    partial class GodHands {
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
            this.menubar = new System.Windows.Forms.MenuStrip();
            this.statusstrip = new System.Windows.Forms.StatusStrip();
            this.toolbar = new System.Windows.Forms.ToolStrip();
            this.progressbar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusbar = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabbar = new System.Windows.Forms.TabControl();
            this.tab_disk = new System.Windows.Forms.TabPage();
            this.tab_zone = new System.Windows.Forms.TabPage();
            this.tab_room = new System.Windows.Forms.TabPage();
            this.tab_actor = new System.Windows.Forms.TabPage();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menubar.SuspendLayout();
            this.statusstrip.SuspendLayout();
            this.tabbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // menubar
            // 
            this.menubar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menubar.Location = new System.Drawing.Point(0, 0);
            this.menubar.Name = "menubar";
            this.menubar.Size = new System.Drawing.Size(823, 24);
            this.menubar.TabIndex = 0;
            this.menubar.Text = "menuStrip1";
            // 
            // statusstrip
            // 
            this.statusstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressbar,
            this.statusbar});
            this.statusstrip.Location = new System.Drawing.Point(0, 493);
            this.statusstrip.Name = "statusstrip";
            this.statusstrip.Size = new System.Drawing.Size(823, 22);
            this.statusstrip.TabIndex = 1;
            this.statusstrip.Text = "statusStrip1";
            // 
            // toolbar
            // 
            this.toolbar.Location = new System.Drawing.Point(0, 24);
            this.toolbar.Name = "toolbar";
            this.toolbar.Size = new System.Drawing.Size(823, 25);
            this.toolbar.TabIndex = 2;
            this.toolbar.Text = "toolStrip1";
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
            // tabbar
            // 
            this.tabbar.Controls.Add(this.tab_disk);
            this.tabbar.Controls.Add(this.tab_zone);
            this.tabbar.Controls.Add(this.tab_room);
            this.tabbar.Controls.Add(this.tab_actor);
            this.tabbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabbar.Location = new System.Drawing.Point(0, 49);
            this.tabbar.Name = "tabbar";
            this.tabbar.SelectedIndex = 0;
            this.tabbar.Size = new System.Drawing.Size(823, 444);
            this.tabbar.TabIndex = 3;
            // 
            // tab_disk
            // 
            this.tab_disk.Location = new System.Drawing.Point(4, 22);
            this.tab_disk.Name = "tab_disk";
            this.tab_disk.Padding = new System.Windows.Forms.Padding(3);
            this.tab_disk.Size = new System.Drawing.Size(815, 418);
            this.tab_disk.TabIndex = 0;
            this.tab_disk.Text = "Disk";
            this.tab_disk.UseVisualStyleBackColor = true;
            // 
            // tab_zone
            // 
            this.tab_zone.Location = new System.Drawing.Point(4, 22);
            this.tab_zone.Name = "tab_zone";
            this.tab_zone.Padding = new System.Windows.Forms.Padding(3);
            this.tab_zone.Size = new System.Drawing.Size(815, 418);
            this.tab_zone.TabIndex = 1;
            this.tab_zone.Text = "Zone";
            this.tab_zone.UseVisualStyleBackColor = true;
            // 
            // tab_room
            // 
            this.tab_room.Location = new System.Drawing.Point(4, 22);
            this.tab_room.Name = "tab_room";
            this.tab_room.Padding = new System.Windows.Forms.Padding(3);
            this.tab_room.Size = new System.Drawing.Size(815, 418);
            this.tab_room.TabIndex = 2;
            this.tab_room.Text = "Room";
            this.tab_room.UseVisualStyleBackColor = true;
            // 
            // tab_actor
            // 
            this.tab_actor.Location = new System.Drawing.Point(4, 22);
            this.tab_actor.Name = "tab_actor";
            this.tab_actor.Padding = new System.Windows.Forms.Padding(3);
            this.tab_actor.Size = new System.Drawing.Size(815, 418);
            this.tab_actor.TabIndex = 3;
            this.tab_actor.Text = "Actor";
            this.tab_actor.UseVisualStyleBackColor = true;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.closeToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open ...";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveAsToolStripMenuItem.Text = "Save As ...";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.closeToolStripMenuItem.Text = "Close";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            // 
            // GodHands
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 515);
            this.Controls.Add(this.tabbar);
            this.Controls.Add(this.toolbar);
            this.Controls.Add(this.statusstrip);
            this.Controls.Add(this.menubar);
            this.MainMenuStrip = this.menubar;
            this.Name = "GodHands";
            this.Text = "GodHands";
            this.menubar.ResumeLayout(false);
            this.menubar.PerformLayout();
            this.statusstrip.ResumeLayout(false);
            this.statusstrip.PerformLayout();
            this.tabbar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menubar;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusstrip;
        private System.Windows.Forms.ToolStripProgressBar progressbar;
        private System.Windows.Forms.ToolStripStatusLabel statusbar;
        private System.Windows.Forms.ToolStrip toolbar;
        private System.Windows.Forms.TabControl tabbar;
        private System.Windows.Forms.TabPage tab_disk;
        private System.Windows.Forms.TabPage tab_zone;
        private System.Windows.Forms.TabPage tab_room;
        private System.Windows.Forms.TabPage tab_actor;
    }
}