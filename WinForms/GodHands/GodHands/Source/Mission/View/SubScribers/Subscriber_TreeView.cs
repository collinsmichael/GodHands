using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public class EnumTreeView : IEnumDir {
        private TreeNode node;

        public EnumTreeView(TreeNode node) {
            this.node = node;
        }

        public bool Visit(string url, DirRec dir) {
            string name = dir.GetFileName();
            TreeNode leaf = node.Nodes.Add(url, name);
            if (dir.FileFlags_Directory) {
                leaf.ImageIndex = ShellIcons.GetDirIconIndex(false);
                leaf.SelectedImageIndex = ShellIcons.GetDirIconIndex(true);
                Iso9660.EnumDir(url, dir, new EnumTreeView(leaf));
            } else {
                int icon = ShellIcons.GetFileIconIndex(name);
                leaf.ImageIndex = leaf.SelectedImageIndex = icon;
            }
            return true;
        }
    }

    public class Subscriber_TreeView : ISubscriber {
        private string url = null;
        private object obj = null;
        private TreeView win;

        public Subscriber_TreeView(string url, TreeView win) {
            this.url = url;
            this.win = win;
            Publisher.Subscribe(url, this);
        }

        ~Subscriber_TreeView() {
            Publisher.Unsubscribe(url, this);
        }

        public bool Notify(object obj) {
            win.Nodes.Clear();
            this.obj = obj;
            if (obj != null) {
                string volume = Iso9660.pvd.VolumeIdentifier.Trim();
                TreeNode root = win.Nodes.Add("CD:PVD", "CDROM");
                root.ImageIndex = ShellIcons.GetFileIconIndex("test.iso");
                root.SelectedImageIndex = root.ImageIndex;

                TreeNode node = root.Nodes.Add("CD:ROOT", volume);
                node.ImageIndex = ShellIcons.GetFileIconIndex("test.iso");
                node.SelectedImageIndex = node.ImageIndex;
                return Iso9660.EnumFileSys(new EnumTreeView(node));
            }
            return true;
        }
    }
}
