using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public partial class ZndTool : UserControl {
        public ZndTool() {
            InitializeComponent();
            ShellIcons.GetShellIcons(treeview);

            for (int i = 0; i < 256; i++) {
                string name = string.Format("ZONE{0}.ZND", i.ToString("D3"));
                combobox.Items.Add(name);
            }
        }

        private void OnComboSelect(object sender, EventArgs e) {
            string key = combobox.GetItemText(combobox.SelectedItem);
            if (key.Length > 0) {
                int normal = ShellIcons.GetDirIconIndex(false);
                int select = ShellIcons.GetDirIconIndex(true);
                int binary = ShellIcons.GetFileIconIndex("test.bin");

                treeview.Nodes.Clear();

                TreeNode zone = treeview.Nodes.Add(key, key, normal, select);
                TreeNode rooms = zone.Nodes.Add(key+"/Rooms", "Rooms", normal, select);
                rooms.Nodes.Add(key+"/Room/0", "MAP000.MPD", binary, binary);
                rooms.Nodes.Add(key+"/Room/1", "MAP001.MPD", binary, binary);
                rooms.Nodes.Add(key+"/Room/2", "MAP002.MPD", binary, binary);

                TreeNode actors = zone.Nodes.Add(key+"/Actors", "Actors", normal, select);
                actors.Nodes.Add(key+"/Actor/0", "Alice", binary, binary);
                actors.Nodes.Add(key+"/Actor/1", "Bob", binary, binary);
                actors.Nodes.Add(key+"/Actor/2", "Carl", binary, binary);

                TreeNode textures = zone.Nodes.Add(key+"/Texture", "Texture", normal, select);
                textures.Nodes.Add(key+"/Texture/0", "01.img", binary, binary);
                textures.Nodes.Add(key+"/Texture/1", "02.img", binary, binary);
                textures.Nodes.Add(key+"/Texture/2", "03.img", binary, binary);
            }
        }

        private void OnTreeViewSelect(object sender, TreeViewEventArgs e) {
            TreeNode node = treeview.SelectedNode;
            property.SelectedObject = node;
        }
    }
}
