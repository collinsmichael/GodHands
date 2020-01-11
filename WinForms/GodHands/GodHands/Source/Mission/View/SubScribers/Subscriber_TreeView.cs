using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
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
                TreeNode node = root.Nodes.Add("CD:ROOT", volume);
                return Iso9660.EnumFileSystem(node, "CD:ROOT");
            }
            return true;
        }
    }
}
