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
        private Color[] colors = null;
        private Texture texture = null;
        private Image texture2d = null;

        public ZndEditor() {
            InitializeComponent();
            treeview.ImageList = View.ImageListFromDir("/img/zone");
            treeview.ShowNodeToolTips = true;

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
        }

        public void CloseDisk() {
            combobox.Text = "";
            treeview.Nodes.Clear();
            combobox.Items.Clear();
            zones.Clear();
            node = null;
            colors = null;
            texture = null;
            texture2d = null;
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
                rooms.Nodes.Add(room.GetUrl(), room.Name, 1, 1);
            }
            foreach (Actor actor in zone.actors) {
                actors.Nodes.Add(actor.GetUrl(), actor.Name, 2, 2);
            }
            foreach (Texture image in zone.images) {
                int index = zone.images.IndexOf(image);
                string text = "Image_"+index.ToString("D2");
                images.Nodes.Add(image.GetUrl(), text, 3, 3);
            }

            int c = 0;
            colors = new Color[256*zone.cluts.Count()];
            foreach (Clut clut in zone.cluts) {
                int index = zone.cluts.IndexOf(clut);
                string text = "Clut_"+index.ToString("D2");
                images.Nodes.Add(clut.GetUrl(), text, 4, 4);
                Color[] cols = clut.ToRgb();
                foreach (Color rgb in cols) {
                    colors[c++] = rgb;
                }
            }
        }

        private void OnTreeSelect(object sender, TreeViewEventArgs e) {
            node = e.Node;
            if (node != null) {
                if (node.Text.Contains("Image_")) {
                    string url = node.Name;
                    foreach (Texture image in zone.images) {
                        if (image.GetUrl() == url) {
                            texture = image;
                            texture2d = image.ToImage(null);
                            property.SelectedObject = image;
                            picturebox.Invalidate();
                        }
                    }
                } else if (node.Text.Contains("Clut_")) {
                    string url = node.Name;
                    foreach (Clut image in zone.cluts) {
                        if (image.GetUrl() == url) {
                            texture = image;
                            texture2d = texture.ToImage(null);
                            property.SelectedObject = image;
                            picturebox.Invalidate();
                        }
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
                ofd.Title = "Export File";
                ofd.Filter = "CD Images|*.bin;*.img;*.iso|All Files|*.*";
                if (ofd.ShowDialog() == DialogResult.OK) {
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

                int ax = texture2d.Width;
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
