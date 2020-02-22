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
    class DiskView : TreeView, ISubscriber {
        int icon_disk;
        int icon_dir1;
        int icon_dir2;

        public DiskView() : base() {
            SysIcons.GetSysIcons(this);
            icon_disk = SysIcons.GetDiskIconIndex();
            icon_dir1 = SysIcons.GetDirIconIndex(false);
            icon_dir2 = SysIcons.GetDirIconIndex(true);

            ItemDrag += new ItemDragEventHandler(OnTreeDrag);
            DragEnter += new DragEventHandler(OnDrag);
            DragDrop += new DragEventHandler(OnDrop);
            //AllowDrop = true;
        }

        public bool CloseDisk() {
            Publisher.Unsubscribe("*", this);
            Nodes.Clear();
            return true;
        }

        public bool OpenDisk() {
            Volume volume = Iso9660.pvd;
            string key = volume.Key;
            Record root = Iso9660.root;
            string text = volume.VolumeIdentifier.Trim();
            BeginUpdate();
                Nodes.Clear();
                TreeNode node1 = Nodes.Add(key, "CDROM", icon_disk, icon_disk);
                node1.Tag = volume;
                TreeNode node2 = node1.Nodes.Add(root.Key, text, icon_disk, icon_disk);
                node2.Tag = root;
                Iso9660.EnumFileSys(new EnumDiskView(node2));
                node1.Expand();
                node2.Expand();
            EndUpdate();
            Publisher.Subscribe("*", this);
            return true;
        }

        // ********************************************************************
        // Publish and Subscribe
        // ********************************************************************
        public bool Insert(object obj) {
            Record item = obj as Record;
            if (item != null) {
                BaseClass parent = item.parent;
                if (parent != null) {
                    int icon1 = icon_dir1;
                    int icon2 = icon_dir2;
                    if (!item.FileFlags_Directory) {
                        icon1 = icon2 = SysIcons.GetFileIconIndex(item.FileName);
                    }

                    TreeNode[] nodes = Nodes.Find(parent.Key, true);
                    if (nodes.Length > 0) {
                        string text = item.GetText();
                        nodes[0].Nodes.Add(item.Key, text, icon1, icon2).Tag = obj;
                        return true;
                    }
                }
            }
            return true;
        }

        public bool Remove(object obj) {
            BaseClass item = obj as BaseClass;
            if (item != null) {
                if ((item as Record) == null) {
                    return true;
                }

                TreeNode[] nodes = Nodes.Find(item.Key, true);
                foreach (TreeNode node in nodes.ToList()) {
                    Nodes.Remove(node);
                }
            }
            return true;
        }

        public bool Notify(object obj) {
            BaseClass item = obj as BaseClass;
            if (item != null) {
                TreeNode[] nodes = Nodes.Find(item.Key, true);
                foreach (TreeNode node in nodes) {
                    node.Text = item.GetText();
                }
            }
            return true;
        }

        // ********************************************************************
        // Drag and Drop
        // ********************************************************************
        private void OnTreeDrag(object sender, ItemDragEventArgs e) {
            TreeNode node = e.Item as TreeNode;
            if (node != null) {
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
        }

        private void OnDrag(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effect = DragDropEffects.Copy;
            }
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files) {
                Logger.Info("Dragging file "+file);
            }
        }

        private void OnDrop(object sender, DragEventArgs e) {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            Point pt = PointToClient(new Point(e.X, e.Y));

            Record rec = null;
            TreeNode node = GetNodeAt(pt);
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
    }
}
