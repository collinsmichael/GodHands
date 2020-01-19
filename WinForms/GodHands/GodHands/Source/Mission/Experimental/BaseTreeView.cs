using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    // TreeView that updates nodes on publish
    public class BaseTreeView : TreeView {
        public BaseTreeView() : base() {
            AfterSelect += new System.Windows.Forms.TreeViewEventHandler(OnTreeSelect);
            NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(OnTreeClick);
            ShowNodeToolTips = true;
        }

        public void SetImageList(string path) {
            ImageList = View.ImageListFromDir(path);
        }

        public void Notify(Base obj) {
            TreeNode[] nodes = Nodes.Find(obj.URL, true);
            foreach (TreeNode node in nodes) {
                if (node.Tag == obj) {
                    node.Text = obj.Text;
                    node.ImageIndex = obj.IconNormal;
                    node.StateImageIndex = obj.IconSelect;
                }
            }
        }

        public void Insert(TreeNode parent, Base obj) {
            TreeNode node = parent.Nodes.Add(obj.URL, obj.Text);
            node.ImageIndex = obj.IconNormal;
            node.SelectedImageIndex = obj.IconSelect;
            node.ToolTipText = obj.ToolTip;
        }


        private void OnTreeSelect(object sender, TreeViewEventArgs e) {
            if (e.Node != null) {
                Base obj = e.Node.Tag as Base;
                if (obj != null) {
                    obj.OnTreeSelect(sender, e);
                }
            }
        }

        private void OnTreeClick(object sender, TreeNodeMouseClickEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                SelectedNode = e.Node;
                Base obj = e.Node.Tag as Base;
                if (obj != null) {
                    obj.OnTreeClick(sender, e);
                }
            }
        }
    }
}
