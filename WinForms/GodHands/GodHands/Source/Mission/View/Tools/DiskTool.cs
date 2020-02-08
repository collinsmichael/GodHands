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

            DirRec rec = Iso9660.GetByPath(node.Name);
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

            DirRec rec = null;
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
                    DirRec rec = Iso9660.GetByPath(url);
                    sub_property.Notify(rec);
                }
            } else {
                sub_property.Notify(null);
            }
        }

        private void OnClick_Close(object sender, EventArgs e) {
            Close();
        }
    }
}
