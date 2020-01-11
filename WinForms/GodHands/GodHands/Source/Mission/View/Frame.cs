using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public partial class Frame : Form {
        private OpenFileDialog ofd = new OpenFileDialog();
        private SaveFileDialog sfd = new SaveFileDialog();
        private Subscriber_PropertyGrid sub_property = null;

        public Frame() {
            InitializeComponent();
            sub_property = new Subscriber_PropertyGrid(propertyGrid1);
            Logger.AddStatusBar(statusbar);
            Logger.AddProgressBar(progressbar);
        }

        private void OnClosing(object sender, FormClosingEventArgs e) {
            Logger.RemoveStatusBar(statusbar);
            Logger.RemoveProgressBar(progressbar);
        }

        private void OnMenu_FileOpen(object sender, EventArgs e) {
            ofd.Title = "Open CD Image";
            ofd.Filter = "CD Images|*.bin;*.img;*.iso|All Files|*.*";
            if (ofd.ShowDialog() == DialogResult.OK) {
                if (Iso9660.Open(ofd.FileName)) {
                    //sub_property.Notify(Iso9660.pvd);
                    sub_property.Notify(Iso9660.root);
                }
            }
        }

        private void OnMenu_FileSaveAs(object sender, EventArgs e) {
            sfd.Title = "Save CD Image";
            sfd.Filter = "CD Images|*.bin;*.img;*.iso|All Files|*.*";
            if (sfd.ShowDialog() == DialogResult.OK) {
                //Iso9660.Open(ofd.FileName);
            }
        }

        private void OnMenu_FileClose(object sender, EventArgs e) {
            Iso9660.Close();
        }

        private void OnMenu_FileExit(object sender, EventArgs e) {
            Close();
        }

        private void OnMenu_EditUndo(object sender, EventArgs e) {
            UndoRedo.Undo();
        }

        private void OnMenu_EditRedo(object sender, EventArgs e) {
            UndoRedo.Redo();
        }

        private void OnMenu_ToolsDiskTool(object sender, EventArgs e) {
            if (View.disktool == null) {
                View.disktool = new DiskTool();
                View.disktool.Show();
            } else {
                View.disktool.BringToFront();
            }
        }

        private void OnMenu_ToolsLogFile(object sender, EventArgs e) {
            if (View.logtool == null) {
                View.logtool = new LogTool();
                View.logtool.Show();
            } else {
                View.logtool.BringToFront();
            }
        }
    }
}
