using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestBed {
    public partial class ZoneEditor : Form {
        TreeNode TreeNodeActor(string text) {
            TreeNode node = new TreeNode(text, 1, 1);

            TreeNode weapon = new TreeNode("Weapon", 20, 20);
            weapon.Nodes.Add(new TreeNode("Blade",  20, 20));
            weapon.Nodes.Add(new TreeNode("Grip",   20, 20));
            weapon.Nodes.Add(new TreeNode("Gem1",   10, 10));
            weapon.Nodes.Add(new TreeNode("Gem2",   10, 10));
            weapon.Nodes.Add(new TreeNode("Gem3",   10, 10));

            TreeNode shield = new TreeNode("Shield", 15, 15);
            shield.Nodes.Add(new TreeNode("Gem1",   10, 10));
            shield.Nodes.Add(new TreeNode("Gem2",   10, 10));
            shield.Nodes.Add(new TreeNode("Gem3",   10, 10));

            node.Nodes.Add(weapon);
            node.Nodes.Add(shield);
            node.Nodes.Add(new TreeNode("Helmot", 14, 14));
            node.Nodes.Add(new TreeNode("Armour", 11, 11));
            node.Nodes.Add(new TreeNode("Gloves", 25, 25));
            node.Nodes.Add(new TreeNode("Boots",  9, 9));
            node.Nodes.Add(new TreeNode("Accessory", 13, 13));
            return node;
        }

        public ZoneEditor() {
            InitializeComponent();

            ImageList img = new ImageList();
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            foreach(string path in Directory.GetFiles(dir+"/img/zone")) {
                img.Images.Add(Image.FromFile(path));
            }
            foreach(string path in Directory.GetFiles(dir+"/img/zone/equip")) {
                img.Images.Add(Image.FromFile(path));
            }
            treeview.ImageList = img;

            TreeNode root = new TreeNode("Zone", 2, 2);
            TreeNode rooms = new TreeNode("Rooms", 4, 4);
            rooms.Nodes.Add(new TreeNode("Room01", 4, 4));
            rooms.Nodes.Add(new TreeNode("Room02", 4, 4));
            rooms.Nodes.Add(new TreeNode("Room03", 4, 4));
            rooms.Nodes.Add(new TreeNode("Room04", 4, 4));

            TreeNode actors = new TreeNode("Actors", 1, 1);
            actors.Nodes.Add(TreeNodeActor("Actor01"));
            actors.Nodes.Add(TreeNodeActor("Actor02"));
            actors.Nodes.Add(TreeNodeActor("Actor03"));
            actors.Nodes.Add(TreeNodeActor("Actor04"));

            TreeNode images = new TreeNode("Images", 3, 3);
            images.Nodes.Add(new TreeNode("Image01", 3, 3));
            images.Nodes.Add(new TreeNode("Image02", 3, 3));
            images.Nodes.Add(new TreeNode("Image03", 3, 3));

            root.Nodes.Add(rooms);
            root.Nodes.Add(actors);
            root.Nodes.Add(images);
            root.Expand();
            rooms.Expand();
            actors.Expand();
            images.Expand();
            treeview.Nodes.Add(root);
        }

        public static Image ImageFromFile(string path) {
            try {
                string dir = AppDomain.CurrentDomain.BaseDirectory;
                Image icon = Image.FromFile(dir+path);
                return icon;
            } catch {
                return null;
            }
        }

        private void OnTreeClick(object sender, TreeNodeMouseClickEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                string text = e.Node.Text;
                status.Text = text;

                ContextMenuStrip menu = new ContextMenuStrip();
                if (text.StartsWith("Room")) {
                    ToolStripMenuItem import = new ToolStripMenuItem("Import Room", ImageFromFile("/img/zone/import.png"));
                    ToolStripMenuItem export = new ToolStripMenuItem("Export Room", ImageFromFile("/img/zone/export.png"));
                    ToolStripMenuItem insert = new ToolStripMenuItem("Insert Room", ImageFromFile("/img/zone/insert.png"));
                    ToolStripMenuItem remove = new ToolStripMenuItem("Remove Room", ImageFromFile("/img/zone/remove.png"));
                    menu.Items.Add(import);
                    menu.Items.Add(export);
                    menu.Items.Add(new ToolStripSeparator());
                    menu.Items.Add(insert);
                    menu.Items.Add(remove);
                }
                if (text.StartsWith("Actor")) {
                    ToolStripMenuItem import = new ToolStripMenuItem("Import Actor", ImageFromFile("/img/zone/import.png"));
                    ToolStripMenuItem export = new ToolStripMenuItem("Export Actor", ImageFromFile("/img/zone/export.png"));
                    ToolStripMenuItem insert = new ToolStripMenuItem("Insert Actor", ImageFromFile("/img/zone/insert.png"));
                    ToolStripMenuItem remove = new ToolStripMenuItem("Remove Actor", ImageFromFile("/img/zone/remove.png"));
                    menu.Items.Add(import);
                    menu.Items.Add(export);
                    menu.Items.Add(new ToolStripSeparator());
                    menu.Items.Add(insert);
                    menu.Items.Add(remove);
                }
                if (text.StartsWith("Image")) {
                    ToolStripMenuItem import = new ToolStripMenuItem("Import Image", ImageFromFile("/img/zone/import.png"));
                    ToolStripMenuItem export = new ToolStripMenuItem("Export Image", ImageFromFile("/img/zone/export.png"));
                    ToolStripMenuItem insert = new ToolStripMenuItem("Insert Image", ImageFromFile("/img/zone/insert.png"));
                    ToolStripMenuItem remove = new ToolStripMenuItem("Remove Image", ImageFromFile("/img/zone/remove.png"));
                    menu.Items.Add(import);
                    menu.Items.Add(export);
                    menu.Items.Add(new ToolStripSeparator());
                    menu.Items.Add(insert);
                    menu.Items.Add(remove);
                }
                menu.Show(Cursor.Position);
            }
        }

        private void OnTreeSelect(object sender, TreeViewEventArgs e) {
            string text = e.Node.Text;
            ContextMenuStrip menu = new ContextMenuStrip();
            switch (text) {
            case "Weapon": property.SelectedObject = new Weapon(); break;
            case "Blade":  property.SelectedObject = new Blade(); break;
            case "Grip":   property.SelectedObject = new Grip(); break;
            case "Gem":    property.SelectedObject = new Gem(); break;
            case "Shield": property.SelectedObject = new Shield(); break;
            }
        }
    }
}
