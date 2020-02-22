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
    public partial class Frame {
        private ContextMenuStrip popup_bin = new ContextMenuStrip();
        private ContextMenuStrip popup_dir = new ContextMenuStrip();
        private ContextMenuStrip popup_add = new ContextMenuStrip();

        private ToolStripMenuItem popup_dir_insert = null;
        private ToolStripMenuItem popup_dir_remove = null;
        private ToolStripMenuItem popup_add_addfile = null;
        private ToolStripMenuItem popup_add_addfolder = null;

        private ToolStripMenuItem popup_bin_openas = null;
        private ToolStripMenuItem popup_bin_remove = null;
        private ToolStripMenuItem popup_bin_import = null;
        private ToolStripMenuItem popup_bin_export = null;

        private void InitPopup() {
            popup_add_addfile = new ToolStripMenuItem("Add File", View.ImageFromFile("/img/menu/tree-add.png"));
            popup_add_addfolder = new ToolStripMenuItem("Add Folder", View.ImageFromFile("/img/menu/tree-folder.png"));
            popup_add_addfile.Click += new EventHandler(OnMenuInsert);
            popup_add_addfolder.Click += new EventHandler(OnMenuInsert);

            popup_dir_insert = new ToolStripMenuItem("Add ...", View.ImageFromFile("/img/menu/tree-add.png"));
            popup_dir_insert.DropDownItems.Add(popup_add_addfile);
            popup_dir_insert.DropDownItems.Add(popup_add_addfolder);

            popup_dir_remove = new ToolStripMenuItem("Delete", View.ImageFromFile("/img/menu/tree-delete.png"));
            popup_bin_openas = new ToolStripMenuItem("Open As...", View.ImageFromFile("/img/menu/tree-openas.png"));
            popup_bin_import = new ToolStripMenuItem("Import", View.ImageFromFile("/img/menu/tree-import.png"));
            popup_bin_export = new ToolStripMenuItem("Export", View.ImageFromFile("/img/menu/tree-export.png"));
            popup_bin_remove = new ToolStripMenuItem("Delete", View.ImageFromFile("/img/menu/tree-delete.png"));

            popup_dir_insert.Click += new EventHandler(OnMenuInsert);
            popup_dir_remove.Click += new EventHandler(OnMenuRemove);
            popup_bin_remove.Click += new EventHandler(OnMenuRemove);
            popup_bin_import.Click += new EventHandler(OnMenuImport);
            popup_bin_export.Click += new EventHandler(OnMenuExport);

            popup_dir.Items.Add(popup_dir_insert);
            popup_dir.Items.Add(popup_dir_remove);
            popup_bin.Items.Add(popup_bin_openas);
            popup_bin.Items.Add(new ToolStripSeparator());
            popup_bin.Items.Add(popup_bin_import);
            popup_bin.Items.Add(popup_bin_export);
            popup_bin.Items.Add(new ToolStripSeparator());
            popup_bin.Items.Add(popup_bin_remove);
        }

        private void OnMenuImport(object sender, EventArgs e) {
            TreeNode node = treeview.SelectedNode;
            if (node != null) {
                Record record = node.Tag as Record;
                if (record != null) {
                    ofd.Title = "Import File";
                    ofd.Filter = "All Files|*.*";
                    ofd.FileName = record.GetFileName();
                    if (ofd.ShowDialog() == DialogResult.OK) {
                        record.binary.ImportRaw(ofd.FileName);
                    }
                }
            }
        }

        private void OnMenuExport(object sender, EventArgs e) {
            TreeNode node = treeview.SelectedNode;
            if (node != null) {
                Record record = node.Tag as Record ;
                if (record != null) {
                    sfd.Title = "Export File";
                    sfd.Filter = "All Files|*.*";
                    sfd.FileName = record.GetFileName();
                    if (sfd.ShowDialog() == DialogResult.OK) {
                        record.binary.ExportRaw(sfd.FileName);
                    }
                }
            }
        }

        private void OnMenuInsert(object sender, EventArgs e) {
            TreeNode node = treeview.SelectedNode;
            if (node != null) {
                BaseClass obj = node.Tag as BaseClass ;
                if (obj != null) {
                    //TreeNode child = null;
                    //switch (node.Text) {
                    //case "Rooms":  child = new TreeNode("Room", 1, 1);  break;
                    //case "Actors": child = new TreeNode("Actor", 2, 2); break;
                    //case "Images": child = new TreeNode("Image", 3, 3); break;
                    //}
                    //
                    //if (child != null) {
                    //    node.Nodes.Add(child);
                    //    node.Expand();
                    //}
                }
            }
        }

        private void OnMenuRemove(object sender, EventArgs e) {
            TreeNode node = treeview.SelectedNode;
            if (node != null) {
                BaseClass obj = node.Tag as BaseClass ;
                if (obj != null) {
                    //switch (node.Text) {
                    //case "Zone": break;
                    //case "Rooms": break;
                    //case "Actors": break;
                    //case "Images": break;
                    //default:
                    //    TreeNode parent = node.Parent;
                    //    parent.Nodes.Remove(node);
                    //    break;
                    //}
                }
            }
        }
    }
}
