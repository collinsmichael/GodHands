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
        private Dictionary<string,string> zones = new Dictionary<string,string>();
        private Zone zone = null;

        public ZndTool() {
            InitializeComponent();
            ShellIcons.GetShellIcons(treeview);
        }

        public void OpenDisk() {
            treeview.Nodes.Clear();
            combobox.Items.Clear();
            combobox.Text = "";
            foreach (Zone zone in Model.zones.Values) {
                string key = zone.GetUrl();
                string txt = zone.GetRec().GetFileName();
                zones.Add(txt, key);
                combobox.Items.Add(txt);
            }
        }

        public void CloseDisk() {
            combobox.Text = "";
            treeview.Nodes.Clear();
            combobox.Items.Clear();
            zones.Clear();
        }

        private void OnComboSelect(object sender, EventArgs e) {
            string key = combobox.GetItemText(combobox.SelectedItem);
            if (key.Length > 0) {
                if (!zones.ContainsKey(key)) {
                    return;
                }

                string url = zones[key];
                if (!Model.zones.ContainsKey(url)) {
                    return;
                }
                zone = Model.zones[url];
                zone.OpenZone();

                int normal = ShellIcons.GetDirIconIndex(false);
                int select = ShellIcons.GetDirIconIndex(true);
                int binary = ShellIcons.GetFileIconIndex("test.bin");

                treeview.Nodes.Clear();
                TreeNode node_zone = treeview.Nodes.Add("Zone", key, normal, select);
                TreeNode node_rooms = node_zone.Nodes.Add("Room", "Rooms", normal, select);
                TreeNode node_actors = node_zone.Nodes.Add("Actor", "Actors", normal, select);
                TreeNode node_textures = node_zone.Nodes.Add("Texture", "Textures", normal, select);

                //node_rooms.Nodes.Add("Zone:Room/0", "MAP000.MPD", binary, binary);
                //node_rooms.Nodes.Add("Zone:Room/1", "MAP001.MPD", binary, binary);
                //node_rooms.Nodes.Add("Zone:Room/2", "MAP002.MPD", binary, binary);
                int r = 0;
                foreach (Room room in zone.rooms) {
                    string room_key = url+"/Room/"+ r++;
                    string room_name = room.Name;
                    node_rooms.Nodes.Add(room_key, room_name, binary, binary);
                }

                //node_actors.Nodes.Add("Zone:Actor/0", "Alice", binary, binary);
                //node_actors.Nodes.Add("Zone:Actor/1", "Bob", binary, binary);
                //node_actors.Nodes.Add("Zone:Actor/2", "Carl", binary, binary);
                int a = 0;
                foreach (Actor actor in zone.actors) {
                    string actor_key = url+"/Actor/"+ a++;
                    string actor_name = actor.Name;
                    node_actors.Nodes.Add(actor_key, actor_name, binary, binary);
                }

                node_textures.Nodes.Add("Zone:Texture/0", "01.img", binary, binary);
                node_textures.Nodes.Add("Zone:Texture/1", "02.img", binary, binary);
                node_textures.Nodes.Add("Zone:Texture/2", "03.img", binary, binary);
                node_zone.Expand();
            }
        }

        private void OnTreeViewSelect(object sender, TreeViewEventArgs e) {
            TreeNode node = treeview.SelectedNode;
            if (node == null) {
                property.SelectedObject = null;
            } else {
                string key = node.Name;
                object obj = Model.Get(key);
                property.SelectedObject = obj;
            }
        }
    }
}
