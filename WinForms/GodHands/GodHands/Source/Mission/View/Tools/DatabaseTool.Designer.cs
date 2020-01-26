namespace GodHands {
    partial class DatabaseTool {
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
            this.statusstrip = new System.Windows.Forms.StatusStrip();
            this.progressbar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusbar = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeview = new System.Windows.Forms.TreeView();
            this.property = new System.Windows.Forms.PropertyGrid();
            this.statusstrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusstrip
            // 
            this.statusstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressbar,
            this.statusbar});
            this.statusstrip.Location = new System.Drawing.Point(0, 318);
            this.statusstrip.Name = "statusstrip";
            this.statusstrip.Size = new System.Drawing.Size(499, 22);
            this.statusstrip.TabIndex = 1;
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
            this.statusbar.Size = new System.Drawing.Size(118, 17);
            this.statusbar.Text = "toolStripStatusLabel1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(499, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeview);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.property);
            this.splitContainer1.Size = new System.Drawing.Size(499, 294);
            this.splitContainer1.SplitterDistance = 186;
            this.splitContainer1.TabIndex = 3;
            // 
            // treeview
            // 
            this.treeview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeview.Location = new System.Drawing.Point(0, 0);
            this.treeview.Name = "treeview";
            this.treeview.Size = new System.Drawing.Size(186, 294);
            this.treeview.TabIndex = 0;
            this.treeview.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnTreeSelect);
            this.treeview.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.OnTreeClick);
            // 
            // property
            // 
            this.property.Dock = System.Windows.Forms.DockStyle.Fill;
            this.property.Location = new System.Drawing.Point(0, 0);
            this.property.Name = "property";
            this.property.Size = new System.Drawing.Size(309, 294);
            this.property.TabIndex = 0;
            // 
            // DatabaseTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 340);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusstrip);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DatabaseTool";
            this.Text = "Database Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
            this.Load += new System.EventHandler(this.OnLoad);
            this.statusstrip.ResumeLayout(false);
            this.statusstrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusstrip;
        private System.Windows.Forms.ToolStripProgressBar progressbar;
        private System.Windows.Forms.ToolStripStatusLabel statusbar;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeview;
        private System.Windows.Forms.PropertyGrid property;
    }
}