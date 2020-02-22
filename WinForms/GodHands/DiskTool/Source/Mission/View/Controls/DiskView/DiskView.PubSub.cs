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
    public partial class DiskView : ISubscriber {
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
    }
}
