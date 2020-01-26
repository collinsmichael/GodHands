using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public class EnumDiskView : IEnumDir {
        private TreeNode node;

        public EnumDiskView(TreeNode node) {
            this.node = node;
        }

        public bool Visit(string url, DirRec dir) {
            string name = dir.GetFileName();
            TreeNode leaf = node.Nodes.Add(url, name);
            if (dir.FileFlags_Directory) {
                leaf.ImageIndex = ShellIcons.GetDirIconIndex(false);
                leaf.SelectedImageIndex = ShellIcons.GetDirIconIndex(true);
                Iso9660.EnumDir(url, dir, new EnumDiskView(leaf));
            } else {
                int icon = ShellIcons.GetFileIconIndex(name);
                leaf.ImageIndex = leaf.SelectedImageIndex = icon;
            }
            return true;
        }
    }

    public class Subscriber_DiskView : ISubscriber {
        private string url = null;
        private object obj = null;
        private TreeView win;

        public Subscriber_DiskView(string url, TreeView win) {
            this.url = url;
            this.win = win;
            Publisher.Subscribe(url, this);
        }

        ~Subscriber_DiskView() {
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
                return Iso9660.EnumFileSys(new EnumDiskView(node));
            }
            return true;
        }
    }
}
