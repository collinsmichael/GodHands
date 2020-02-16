using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public partial class DiskTool : Form {
        private Subscriber_PropertyGrid sub_property = null;
        private Subscriber_DiskView sub_treeview = null;
        public bool UsingSysIcons;

        public DiskTool() {
            InitializeComponent();
            Icon = View.IconFromFile("/img/menu/tools-disk-16.png");
            SysIcons.GetSysIcons(treeview);
            treeview.AllowDrop = true;
            treeview.ItemDrag += new ItemDragEventHandler(OnTreeDrag);
            treeview.DragEnter += new DragEventHandler(OnDrag);
            treeview.DragDrop += new DragEventHandler(OnDrop);

            sub_property = new Subscriber_PropertyGrid(property);
            sub_treeview = new Subscriber_DiskView("CD:ROOT", treeview);
            Logger.AddStatusBar(statusbar);
            Logger.AddProgressBar(progressbar);
            OpenDisk();
        }

        private void OnClosing(object sender, FormClosingEventArgs e) {
            Publisher.Unsubscribe("CD:ROOT", sub_treeview);
            Logger.RemoveStatusBar(statusbar);
            Logger.RemoveProgressBar(progressbar);
            View.disktool = null;
        }

        public bool CloseDisk() {
            sub_property.Notify(null);
            sub_treeview.Notify(null);
            return true;
        }

        public bool OpenDisk() {
            sub_property.Notify(Iso9660.pvd);
            sub_treeview.Notify(Iso9660.root);

            if (treeview.Nodes.Count > 0) {
                treeview.Nodes[0].Expand();
                if (treeview.Nodes[0].Nodes.Count > 0) {
                    treeview.Nodes[0].Nodes[0].Expand();
                }
            }
            return true;
        }
        
        private void OnTreeDrag(object sender, ItemDragEventArgs e) {
            TreeNode node = e.Item as TreeNode;
            if (node == null) {
                return;
            }

            Record rec = Iso9660.GetByPath(node.Name);
            if (rec != null) {
                string path = (rec.FileFlags_Directory)
                    ? Iso9660.ExportDir(rec)
                    : Iso9660.ExportFile(rec);
                if (path != null) {
                    string[] files = new string[] { path };
                    DataObject obj = new DataObject(DataFormats.FileDrop, files);
                    DoDragDrop(obj, DragDropEffects.Copy|DragDropEffects.Move);
                }
            }
        }

        private void OnDrag(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effect = DragDropEffects.Copy;
            }
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files) {
                Console.WriteLine(file);
            }
        }

        private void OnDrop(object sender, DragEventArgs e) {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            Point pt = treeview.PointToClient(new Point(e.X, e.Y));

            Record rec = null;
            TreeNode node = treeview.GetNodeAt(pt);
            if (node == null) {
                rec = Iso9660.GetByPath("CD:ROOT");
            } else {
                rec = Iso9660.GetByPath(node.Name);
                if (!rec.FileFlags_Directory) {
                    node = node.Parent;
                    rec = Iso9660.GetByPath(node.Name);
                }
            }
            if (node != null) {
                node.Expand();
            }
            Iso9660.ImportFiles(rec, files);
        }

        private void OnTree_NodeChange(object sender, TreeViewEventArgs e) {
            TreeNode node = treeview.SelectedNode;
            if (node != null) {
                string url = node.Name;
                if (url == "CD:PVD") {
                    sub_property.Notify(Iso9660.pvd);
                } else {
                    Record rec = Iso9660.GetByPath(url);
                    sub_property.Notify(rec);
                }
            } else {
                sub_property.Notify(null);
            }
        }

        private void OnClick_Close(object sender, EventArgs e) {
            Close();
        }

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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeview = new System.Windows.Forms.TreeView();
            this.property = new System.Windows.Forms.PropertyGrid();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_close = new System.Windows.Forms.Button();
            this.statusstrip.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusstrip
            // 
            this.statusstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressbar,
            this.statusbar});
            this.statusstrip.Location = new System.Drawing.Point(0, 387);
            this.statusstrip.Name = "statusstrip";
            this.statusstrip.Size = new System.Drawing.Size(606, 22);
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(606, 387);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeview);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.property);
            this.splitContainer1.Size = new System.Drawing.Size(600, 333);
            this.splitContainer1.SplitterDistance = 255;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeview
            // 
            this.treeview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeview.Location = new System.Drawing.Point(0, 0);
            this.treeview.Name = "treeview";
            this.treeview.Size = new System.Drawing.Size(255, 333);
            this.treeview.TabIndex = 0;
            this.treeview.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnTree_NodeChange);
            // 
            // property
            // 
            this.property.Dock = System.Windows.Forms.DockStyle.Fill;
            this.property.Location = new System.Drawing.Point(0, 0);
            this.property.Name = "property";
            this.property.Size = new System.Drawing.Size(341, 333);
            this.property.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.tableLayoutPanel2.Controls.Add(this.btn_close, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 342);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(600, 42);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // btn_close
            // 
            this.btn_close.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_close.Location = new System.Drawing.Point(507, 3);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(90, 36);
            this.btn_close.TabIndex = 1;
            this.btn_close.Text = "Close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.OnClick_Close);
            // 
            // DiskTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 409);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusstrip);
            this.Name = "DiskTool";
            this.Text = "Disk Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
            this.statusstrip.ResumeLayout(false);
            this.statusstrip.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusstrip;
        private System.Windows.Forms.ToolStripProgressBar progressbar;
        private System.Windows.Forms.ToolStripStatusLabel statusbar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeview;
        private System.Windows.Forms.PropertyGrid property;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btn_close;
    }
}