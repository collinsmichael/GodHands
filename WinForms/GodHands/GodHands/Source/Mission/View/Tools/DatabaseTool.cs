using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public partial class DatabaseTool : Form {
        private Subscriber_PropertyGrid sub_property = null;
        private OpenFileDialog ofd = new OpenFileDialog();
        private SaveFileDialog sfd = new SaveFileDialog();
        private TreeNode node = null;
        private ContextMenuStrip menu = null;
        private ToolStripMenuItem menu_import = null;
        private ToolStripMenuItem menu_export = null;

        public DatabaseTool() {
            InitializeComponent();
            Icon = View.IconFromFile("/img/menu/tools-database-16.png");
            treeview.ImageList = View.ImageListFromDir("/img/database");
            treeview.ShowNodeToolTips = true;
            sub_property = new Subscriber_PropertyGrid(property);

            menu = new ContextMenuStrip();
            menu_import = new ToolStripMenuItem("Import", View.ImageFromFile("/img/zone/import.png"));
            menu_export = new ToolStripMenuItem("Export", View.ImageFromFile("/img/zone/export.png"));
            menu_import.Click += new System.EventHandler(OnMenuImport);
            menu_export.Click += new System.EventHandler(OnMenuExport);
            menu.Items.Add(menu_import);
            menu.Items.Add(menu_export);

        }

        private void OnClosing(object sender, FormClosingEventArgs e) {
            View.databasetool = null;
        }

        private void OnLoad(object sender, EventArgs e) {
            treeview.Nodes.Clear();

            TreeNode root = treeview.Nodes.Add("DB", "Database", 0, 0);
            Model.itemnames.Open(root.Nodes.Add("DB:Items", "Items", 1, 1));
            Model.skills.Open(root.Nodes.Add("DB:Skills", "Skills", 3, 3));
            root.Expand();
        }

        private void OnTreeSelect(object sender, TreeViewEventArgs e) {
            node = e.Node;
            if (node != null) {
                string url = node.Name;
                sub_property.Notify(Model.Get(url));
            } else {
                sub_property.Notify(null);
            }
            treeview.Focus();
        }

        private void OnTreeClick(object sender, TreeNodeMouseClickEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                treeview.SelectedNode = node = e.Node;
                menu.Show(Cursor.Position);
            }
            treeview.Focus();
        }

        private void OnMenuImport(object sender, EventArgs e) {
            if (node != null) {
                ofd.Title = "Import File";
                ofd.Filter = "CD Images|*.bin;*.img;*.iso|All Files|*.*";
                if (ofd.ShowDialog() == DialogResult.OK) {
                    //if (Iso9660.Open(ofd.FileName)) {
                    //    zndtool.OpenDisk();
                    //}
                }
            }
        }

        private void OnMenuExport(object sender, EventArgs e) {
            if (node != null) {
                sfd.Title = "Export File";
                sfd.Filter = "CD Images|*.bin;*.img;*.iso|All Files|*.*";
                if (node.Text.Contains("Image_")) {
                    sfd.Filter = "BMP Images|*.bmp|All Files|*.*";
                }

                if (sfd.ShowDialog() == DialogResult.OK) {
                    //if (Iso9660.Open(ofd.FileName)) {
                    //    zndtool.OpenDisk();
                    //}
                }
            }
        }
    }
}
