using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public partial class ZndEditor : UserControl {
        private Dictionary<string,string> zones = new Dictionary<string,string>();
        private Zone zone = null;

        private ContextMenuStrip menu = null;
        private ToolStripMenuItem menu_insert = null;
        private ToolStripMenuItem menu_remove = null;
        private ToolStripMenuItem menu_import = null;
        private ToolStripMenuItem menu_export = null;

        private OpenFileDialog ofd = new OpenFileDialog();
        private SaveFileDialog sfd = new SaveFileDialog();
        private TreeNode node = null;
        private Texture texture = null;
        private Image texture2d = null;

        public ZndEditor() {
            InitializeComponent();
            treeview.ImageList = View.ImageListFromDir("/img/zone");
            treeview.ShowNodeToolTips = true;
            property.PropertySort = PropertySort.NoSort;

            menu = new ContextMenuStrip();
            menu_insert = new ToolStripMenuItem("Insert", View.ImageFromFile("/img/zone/insert.png"));
            menu_remove = new ToolStripMenuItem("Remove", View.ImageFromFile("/img/zone/remove.png"));
            menu_import = new ToolStripMenuItem("Import", View.ImageFromFile("/img/zone/import.png"));
            menu_export = new ToolStripMenuItem("Export", View.ImageFromFile("/img/zone/export.png"));
            menu_insert.Click += new System.EventHandler(OnMenuInsert);
            menu_remove.Click += new System.EventHandler(OnMenuRemove);
            menu_import.Click += new System.EventHandler(OnMenuImport);
            menu_export.Click += new System.EventHandler(OnMenuExport);
            menu.Items.Add(menu_insert);
            menu.Items.Add(menu_remove);
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add(menu_import);
            menu.Items.Add(menu_export);
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
            combobox.SelectedIndex = 0;
        }

        public void CloseDisk() {
            combobox.Text = "";
            treeview.Nodes.Clear();
            combobox.Items.Clear();
            property.SelectedObject = null;
            zones.Clear();
            node = null;
            texture = null;
            texture2d = null;
            picturebox.Invalidate();
        }

        public void OpenZone() {
            treeview.Nodes.Clear();
            treeview.Nodes.Add(new TreeNode("Zone", 0, 0));
            treeview.Nodes[0].Nodes.Add("Zone/Rooms", "Rooms", 1, 1);
            treeview.Nodes[0].Nodes.Add("Zone/Actors", "Actors", 2, 2);
            treeview.Nodes[0].Nodes.Add("Zone/Images", "Images", 3, 3);
            treeview.Nodes[0].Expand();
            TreeNode rooms  = treeview.Nodes[0].Nodes[0];
            TreeNode actors = treeview.Nodes[0].Nodes[1];
            TreeNode images = treeview.Nodes[0].Nodes[2];

            treeview.Nodes[0].ToolTipText = "Zone";
            rooms.ToolTipText = "List of rooms";
            actors.ToolTipText = "List of actors";
            images.ToolTipText = "Texture pack";

            foreach (Room room in zone.rooms) {
                string url = room.GetUrl();
                TreeNode node = rooms.Nodes.Add(url, room.Name, 1, 1);
                node.Nodes.Add(url+"/Geometry", "Geometry", 28, 28);
                node.Nodes.Add(url+"/Collisions", "Collisions", 31, 31);
                node.Nodes.Add(url+"/Lighting", "Lighting", 32, 32);
                node.Nodes.Add(url+"/Doors", "Doors", 33, 34);
                node.Nodes.Add(url+"/Enemies", "Enemies", 35, 35);
                node.Nodes.Add(url+"/Script", "Script", 36, 36);
                node.Nodes.Add(url+"/Treasure", "Treasure", 37, 37);
            }
            foreach (Actor actor in zone.actors) {
                string url = actor.GetUrl();
                string znd_file = actor.GetZndFileName();
                TreeNode node = actors.Nodes.Add(url, actor.Name, 2, 2);
                TreeNode body = node.Nodes.Add(url+"/BodyParts", "BodyParts", 5, 5);
                TreeNode model = node.Nodes.Add(url+"/Model", znd_file, 28, 28);
                TreeNode weapon = node.Nodes.Add(url+"/Weapon", "Weapon", 12, 12);
                TreeNode shield = node.Nodes.Add(url+"/Shield", "Shield", 11, 11);
                body.Nodes.Add(url+"/BodyParts/BodyPart_1", "BodyPart_1", 5, 5);
                body.Nodes.Add(url+"/BodyParts/BodyPart_2", "BodyPart_2", 5, 5);
                body.Nodes.Add(url+"/BodyParts/BodyPart_3", "BodyPart_3", 5, 5);
                body.Nodes.Add(url+"/BodyParts/BodyPart_4", "BodyPart_4", 5, 5);
                body.Nodes.Add(url+"/BodyParts/BodyPart_5", "BodyPart_5", 5, 5);
                body.Nodes.Add(url+"/BodyParts/BodyPart_6", "BodyPart_6", 5, 5);

                node.Nodes.Add(url+"/Helmot",    "Helmot",     6,  6);
                node.Nodes.Add(url+"/Armour",    "Armour",     7,  7);
                node.Nodes.Add(url+"/Gloves",    "Gloves",     8,  8);
                node.Nodes.Add(url+"/Boots",     "Boots",      9,  9);
                node.Nodes.Add(url+"/Accessory", "Accessory", 10, 10);
                model.Nodes.Add(url+"/Model/SHP", "SHP", 28, 28);
                model.Nodes.Add(url+"/Model/WEP", "WEP", 28, 28);
                model.Nodes.Add(url+"/Model/SEQ", "SEQ Common", 29, 29);
                model.Nodes.Add(url+"/Model/SEQ", "SEQ Battle", 29, 29);

                weapon.Nodes.Add(url+"/Weapon/Blade", "Blade", 13, 13);
                weapon.Nodes.Add(url+"/Weapon/Grip",  "Grip",  14, 14);
                weapon.Nodes.Add(url+"/Weapon/Gem1",  "Gem1",  24, 24);
                weapon.Nodes.Add(url+"/Weapon/Gem2",  "Gem2",  25, 25);
                weapon.Nodes.Add(url+"/Weapon/Gem3",  "Gem3",  26, 26);
                shield.Nodes.Add(url+"/Shield/Gem1",  "Gem1",  24, 24);
                shield.Nodes.Add(url+"/Shield/Gem2",  "Gem2",  25, 25);
                shield.Nodes.Add(url+"/Shield/Gem3",  "Gem3",  26, 26);
            }
            foreach (Texture image in zone.images) {
                int index = zone.images.IndexOf(image);
                string text = "Image_"+index.ToString("D2");
                int icon = (image.IsLookUpTable) ? 4 : 3;
                images.Nodes.Add(image.GetUrl(), text, icon, icon);
            }
        }

        private void OnTreeSelect(object sender, TreeViewEventArgs e) {
            node = e.Node;
            if (node != null) {
                string url = node.Name;
                property.PropertySort = PropertySort.NoSort;
                property.SelectedObject = Model.Get(url);
                foreach (Texture image in zone.images) {
                    if (image.GetUrl() == url) {
                        texture = image;
                        texture2d = image.ToImage();
                        picturebox.Invalidate();
                    }
                }
            }
        }

        private void OnTreeClick(object sender, TreeNodeMouseClickEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                treeview.SelectedNode = node = e.Node;
                switch (node.Text) {
                case "Zone":
                    menu_insert.Enabled = false;
                    menu_remove.Enabled = false;
                    menu_import.Enabled = false;
                    menu_export.Enabled = false;
                    break;
                case "Rooms":
                case "Actors":
                case "Images":
                    menu_insert.Enabled = true;
                    menu_remove.Enabled = false;
                    menu_import.Enabled = false;
                    menu_export.Enabled = false;
                    break;
                default:
                    menu_insert.Enabled = false;
                    menu_remove.Enabled = true;
                    menu_import.Enabled = true;
                    menu_export.Enabled = true;
                    break;
                }
                menu.Show(Cursor.Position);
            }
        }

        private void OnMenuImport(object sender, EventArgs e) {
            if (node != null) {
                ofd.Title = "Import File";
                ofd.Filter = "CD Images|*.bin;*.img;*.iso|All Files|*.*";
                if (ofd.ShowDialog() == DialogResult.OK) {
                    //if (Iso9660.Open(ofd.FileName)) {
                    //    zndtool.OpenDisk();
                    //}
                }
            }
        }

        private void OnMenuExport(object sender, EventArgs e) {
            if (node != null) {
                sfd.Title = "Export File";
                sfd.Filter = "CD Images|*.bin;*.img;*.iso|All Files|*.*";
                if (node.Text.Contains("Image_")) {
                    sfd.Filter = "BMP Images|*.bmp|All Files|*.*";
                }

                if (sfd.ShowDialog() == DialogResult.OK) {
                    //if (Iso9660.Open(ofd.FileName)) {
                    //    zndtool.OpenDisk();
                    //}
                }
            }
        }

        private void OnMenuInsert(object sender, EventArgs e) {
            if (node != null) {
                TreeNode child = null;
                switch (node.Text) {
                case "Rooms":  child = new TreeNode("Room", 1, 1);  break;
                case "Actors": child = new TreeNode("Actor", 2, 2); break;
                case "Images": child = new TreeNode("Image", 3, 3); break;
                }

                if (child != null) {
                    node.Nodes.Add(child);
                    node.Expand();
                }
            }
        }

        private void OnMenuRemove(object sender, EventArgs e) {
            if (node != null) {
                switch (node.Text) {
                case "Zone": break;
                case "Rooms": break;
                case "Actors": break;
                case "Images": break;
                default:
                    TreeNode parent = node.Parent;
                    parent.Nodes.Remove(node);
                    break;
                }
            }
        }

        private void OnComboBoxSelect(object sender, EventArgs e) {
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
                OpenZone();
            }
        }

        private void OnPaintPictureBox(object sender, PaintEventArgs e) {
            if (texture2d != null) {
                double aspect = (double)texture2d.Width / (double)texture2d.Height;

                int ax = texture2d.Width*2;
                int ay = texture2d.Height;
                int bx = picturebox.Size.Width;
                int by = picturebox.Size.Height;
                int cx = picturebox.Location.X + bx/2;
                int cy = picturebox.Location.Y + by/2;

                int x1,y1,x2,y2;
                if (by*aspect <= bx) {
                    x1 = (int)(cx - by*aspect/2);
                    y1 = (int)(cy - by/2);
                    x2 = (int)(by*aspect);
                    y2 = (int)(by);
                } else {
                    x1 = (int)(cx - bx/2);
                    y1 = (int)(cy - bx/aspect/2);
                    x2 = (int)(bx);
                    y2 = (int)(bx/aspect);
                }

                Rectangle rect = new Rectangle(x1,y1,x2,y2);
                e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
                e.Graphics.DrawImage(texture2d, rect);
            }
        }
    }
}
