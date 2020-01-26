using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public class Subscriber_TreeView : ISubscriber {
        private BaseClass data = null;
        private TreeView win;

        public Subscriber_TreeView(TreeView win) {
            this.win = win;
            Publisher.Subscribe("*", this);
        }

        ~Subscriber_TreeView() {
            Publisher.Unsubscribe("*", this);
        }

        public bool Notify(object obj) {
            data = obj as BaseClass;
            if (data != null) {
                string key = data.GetUrl();
                TreeNode[] nodes = win.Nodes.Find(key, true);
                foreach (TreeNode node in nodes) {
                    node.Text = data.GetText();
                }
            }
            return true;
        }
    }
}
