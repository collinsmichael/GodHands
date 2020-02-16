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
        private Subscriber_PropertyGrid sub_property = null;
        private Subscriber_TreeView sub_treeview = null;
        private OpenFileDialog ofd = new OpenFileDialog();
        private SaveFileDialog sfd = new SaveFileDialog();
        private Dictionary<string,string> zones = new Dictionary<string,string>();
        private Zone zone = null;

        private ContextMenuStrip menu = null;
        private ToolStripMenuItem menu_insert = null;
        private ToolStripMenuItem menu_remove = null;
        private ToolStripMenuItem menu_import = null;
        private ToolStripMenuItem menu_export = null;

        private TreeNode node = null;
        private Texture texture = null;
        private Image texture2d = null;

        public ZndEditor() {
            InitializeComponent();
            treeview.ImageList = View.ImageListFromDir("/img/zone");
            treeview.ShowNodeToolTips = true;
            sub_property = new Subscriber_PropertyGrid(property);
            sub_treeview = new Subscriber_TreeView(treeview);

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

        protected override CreateParams CreateParams {
            get {
                const int WS_EX_COMPOSITED = 0x02000000;
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= WS_EX_COMPOSITED;
                return handleParam;
            }
        }

        public void OpenDisk() {
            treeview.Nodes.Clear();
            combobox.Items.Clear();
            combobox.Text = "";
            foreach (Zone zone in Model.zones.Values) {
                string key = zone.GetUrl();
                Record rec = zone.GetRec();
                string txt = rec.GetFileName();
                zones.Add(txt, key);
                combobox.Items.Add(txt);
            }
            sub_property.Notify(null);
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
            sub_property.Notify(null);
        }

        private void OnTreeSelect(object sender, TreeViewEventArgs e) {
            node = e.Node;
            if (node != null) {
                string url = node.Name;
                sub_property.Notify(Model.Get(url));
                foreach (Texture image in zone.images) {
                    if (image.GetUrl() == url) {
                        texture = image;
                        texture2d = image.ToImage(true);
                        picturebox.Invalidate();
                    }
                }
            } else {
                sub_property.Notify(null);
            }
            treeview.Focus();
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
            treeview.Focus();
        }

        private void OnMenuImport(object sender, EventArgs e) {
            if (node != null) {
                InMemory obj = Model.Get(node.Name) as InMemory;
                if (obj != null) {
                    ofd.Title = "Import File";
                    ofd.Filter = obj.GetExportFilter();
                    ofd.FileName = obj.GetExportName();
                    if (ofd.ShowDialog() == DialogResult.OK) {
                        obj.ImportRaw(ofd.FileName);
                    }
                }
            }
        }

        private void OnMenuExport(object sender, EventArgs e) {
            if (node != null) {
                InMemory obj = Model.Get(node.Name) as InMemory;
                if (obj != null) {
                    sfd.Title = "Export File";
                    sfd.Filter = obj.GetExportFilter();
                    sfd.FileName = obj.GetExportName();
                    if (sfd.ShowDialog() == DialogResult.OK) {
                        obj.ExportRaw(sfd.FileName);
                    }
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
            object obj = combobox.SelectedItem;
            if (obj != null) {
                string key = combobox.GetItemText(obj);
                if (key.Length > 0) {
                    if (!zones.ContainsKey(key)) {
                        return;
                    }

                    string url = zones[key];
                    if (!Model.zones.ContainsKey(url)) {
                        return;
                    }
                    zone = Model.zones[url];
                    zone.OpenZone(treeview);
                }
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
