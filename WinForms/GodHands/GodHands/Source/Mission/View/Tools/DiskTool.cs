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
        private TreeNode tv_root = null;

        public DiskTool() {
            InitializeComponent();
            sub_property = new Subscriber_PropertyGrid(property);
            Logger.AddStatusBar(statusbar);
            Logger.AddProgressBar(progressbar);
            OpenDisk();
        }

        private void OnClosing(object sender, FormClosingEventArgs e) {
            Logger.RemoveStatusBar(statusbar);
            Logger.RemoveProgressBar(progressbar);
            View.disktool = null;
        }

        public bool CloseDisk() {
            sub_property.Notify(null);
            treeview.Nodes.Clear();
            return true;
        }

        public bool OpenDisk() {
            treeview.Nodes.Clear();
            if (Iso9660.pvd != null) {
                string volume = Iso9660.pvd.VolumeIdentifier.Trim();
                tv_root = treeview.Nodes.Add("ROOT", volume);
                sub_property.Notify(Iso9660.pvd);
            }
            return true;
        }
    }
}
