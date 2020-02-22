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
    public partial class DiskView {
        private void InitDragDrop() {
            ItemDrag += new ItemDragEventHandler(OnTreeDrag);
            DragEnter += new DragEventHandler(OnDrag);
            DragDrop += new DragEventHandler(OnDrop);
            //AllowDrop = true;
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
