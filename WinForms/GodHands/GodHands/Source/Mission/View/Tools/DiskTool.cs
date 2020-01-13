using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public partial class DiskTool : Form {
        private Subscriber_PropertyGrid sub_property = null;
        private Subscriber_TreeView sub_treeview = null;

        public DiskTool() {
            InitializeComponent();
            Icon = View.IconFromFile("/img/tools-disk-16.png");
            ShellIcons.GetShellIcons(treeview);
            sub_property = new Subscriber_PropertyGrid(property);
            sub_treeview = new Subscriber_TreeView("CD:ROOT", treeview);
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
            return true;
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
