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
    public partial class Frame : Form {
        private OpenFileDialog ofd = new OpenFileDialog();
        private SaveFileDialog sfd = new SaveFileDialog();
        public bool UsingSysIcons;

        public Frame() {
            InitializeComponent();
            Icon = View.IconFromFile("/img/menu/tools-disk-16.png");
            SysIcons.GetSysIcons(treeview);

            Logger.AddStatusBar(statusbar);
            Logger.AddProgressBar(progressbar);

            treeview.AllowDrop = true;
            InitPopup();
        }

        private void OnClosing(object sender, FormClosingEventArgs e) {
            CloseDisk();
            Logger.RemoveStatusBar(statusbar);
            Logger.RemoveProgressBar(progressbar);
            View.frame = null;
        }

        private void OnLoad(object sender, EventArgs e) {
            menu_open.Image = View.ImageFromFile("/img/menu/file-open-16.png");
            menu_save.Image = View.ImageFromFile("/img/menu/file-save-16.png");
            menu_close.Image = View.ImageFromFile("/img/menu/file-close-16.png");
            menu_exit.Image = View.ImageFromFile("/img/menu/file-exit-16.png");
            menu_undo.Image = View.ImageFromFile("/img/menu/edit-undo-16.png");
            menu_redo.Image = View.ImageFromFile("/img/menu/edit-redo-16.png");
            menu_log.Image = View.ImageFromFile("/img/menu/tools-logfile-16.png");
            menu_config.Image = View.ImageFromFile("/img/menu/tools-options-16.png");
            menu_custom.Image = View.ImageFromFile("/img/menu/tools-custom-16.png");
            tool_open.Image = View.ImageFromFile("/img/menu/file-open-16.png");
            tool_save.Image = View.ImageFromFile("/img/menu/file-save-16.png");
            tool_close.Image = View.ImageFromFile("/img/menu/file-close-16.png");
            tool_undo.Image = View.ImageFromFile("/img/menu/edit-undo-16.png");
            tool_redo.Image = View.ImageFromFile("/img/menu/edit-redo-16.png");
            tool_log.Image = View.ImageFromFile("/img/menu/tools-logfile-16.png");
            tool_config.Image = View.ImageFromFile("/img/menu/tools-options-16.png");
            tool_custom.Image = View.ImageFromFile("/img/menu/tools-custom-16.png");
        }

        private void OnMenu_FileOpen(object sender, EventArgs e) {
            if (RamDisk.IsBusy) {
                MessageBox.Show("Operation in progress\r\nPlease wait");
            } else {
                ofd.Title = "Open CD Image";
                ofd.Filter = "CD Images|*.bin;*.img;*.iso|All Files|*.*";
                if (ofd.ShowDialog() == DialogResult.OK) {
                    if (Iso9660.Open(ofd.FileName)) {
                        OpenDisk();
                    }
                }
            }
        }

        public bool OpenDisk() {
            property.Notify(Iso9660.pvd);
            treeview.OpenDisk();
            return true;
        }

        private void OnMenu_FileSaveAs(object sender, EventArgs e) {
            if (RamDisk.IsBusy) {
                MessageBox.Show("Operation in progress\r\nPlease wait");
            } else {
                sfd.Title = "Save CD Image";
                sfd.Filter = "CD Images|*.bin;*.img;*.iso|All Files|*.*";
                if (sfd.ShowDialog() == DialogResult.OK) {
                    Iso9660.SaveAs(sfd.FileName);
                }
            }
        }

        private void OnMenu_FileClose(object sender, EventArgs e) {
            if (RamDisk.IsBusy) {
                MessageBox.Show("Operation in progress\r\nPlease wait");
            } else {
                CloseDisk();
                Iso9660.Close();
            }
        }

        public bool CloseDisk() {
            property.Notify(null);
            treeview.CloseDisk();
            return true;
        }

        private void OnMenu_FileExit(object sender, EventArgs e) {
            if (RamDisk.IsBusy) {
                MessageBox.Show("Operation in progress\r\nPlease wait");
            } else {
                Close();
            }
        }

        private void OnMenu_EditUndo(object sender, EventArgs e) {
            UndoRedo.Undo();
        }

        private void OnMenu_EditRedo(object sender, EventArgs e) {
            UndoRedo.Redo();
        }

        private void OnTree_NodeSelected(object sender, TreeViewEventArgs e) {
            TreeNode node = treeview.SelectedNode;
            if (node != null) {
                property.Notify(node.Tag);
            } else {
                property.Notify(null);
            }
        }

        private void OnTree_NodeClicked(object sender, TreeNodeMouseClickEventArgs e) {
            TreeNode node = treeview.SelectedNode = e.Node;
            if (e.Button == MouseButtons.Right) {
                if (node != null) {
                    Record record = node.Tag as Record;
                    if (record != null) {
                        if (record.FileFlags_Directory) {
                            popup_dir.Show(Cursor.Position);
                        } else {
                            popup_bin.Show(Cursor.Position);
                        }
                    }
                }
            }
        }
    }

    class FlickerFreeSplitter : SplitContainer {
        protected override CreateParams CreateParams {
            get {
                const int WS_EX_COMPOSITED = 0x02000000;
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= WS_EX_COMPOSITED;
                return handleParam;
            }
        }
    }
}
