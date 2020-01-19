using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;

namespace TestBed {
    public partial class Frame : Form {
        private MdiClient client;

        public static Image ImageFromFile(string path) {
            try {
                string dir = AppDomain.CurrentDomain.BaseDirectory;
                Image icon = Image.FromFile(dir+path);
                return icon;
            } catch {
                return null;
            }
        }

        public Frame() {
            InitializeComponent();
            this.ResizeEnd += MdiResizeEnd;
            client = this.Controls.OfType<MdiClient>().First();
            client.BringToFront();

            MenuFileNew.Image = ImageFromFile("/img/file-new.png");
            MenuFileOpen.Image = ImageFromFile("/img/file-open.png");
            MenuFileSave.Image = ImageFromFile("/img/file-save.png");
            MenuFileClose.Image = ImageFromFile("/img/file-close.png");
            MenuFileExit.Image = ImageFromFile("/img/file-exit.png");

            MenuEditUndo.Image = ImageFromFile("/img/edit-undo.png");
            MenuEditRedo.Image = ImageFromFile("/img/edit-redo.png");

            MenuProjectBuild.Image = ImageFromFile("/img/project-build.png");
            MenuProjectDebug.Image = ImageFromFile("/img/project-debug.png");
            MenuProjectSettings.Image = ImageFromFile("/img/project-settings.png");

            Form child1 = new NewProject();
            Form child2 = new NewProject();
            child1.MdiParent = this;
            child2.MdiParent = this;
            child1.Show();
            child2.Show();
        }

        //protected override void WndProc(ref Message m) {
        //    const int WM_ENTERSIZEMOVE     = 0x0231;
        //    const int WM_EXITSIZEMOVE      = 0x0232;
        //    const int WM_MOVE              = 0x0003;
        //    const int WM_SIZE              = 0x0005;
        //    const int WM_SIZING            = 0x0214;
        //    const int WM_MOVING            = 0x0216;
        //    const int WM_WINDOWPOSCHANGING = 0x0046;
        //    const int WM_WINDOWPOSCHANGED  = 0x0047;
        //
        //    //if (m.Msg != WM_WINDOWPOSCHANGED) {
        //    //    base.WndProc(ref m);
        //    //}
        //
        //    base.WndProc(ref m);
        //    base.WndProc(ref m);
        //    switch (m.Msg) {
        //    case WM_SIZING:            case WM_SIZE:
        //    case WM_MOVING:            case WM_MOVE:
        //    case WM_ENTERSIZEMOVE:     case WM_EXITSIZEMOVE:
        //    case WM_WINDOWPOSCHANGING: case WM_WINDOWPOSCHANGED:
        //        client.Location = mdi_panel.Location;
        //        client.Size = mdi_panel.Size;
        //        break;
        //    }
        //}

        void MdiResizeEnd(object sender, EventArgs e) {
            MdiClient mc = this.Controls.OfType<MdiClient>().First();
            mc.Location = mdi_panel.Location;
            mc.Size = mdi_panel.Size;
            mc.BringToFront();
        }

        private void MenuFileNew_Click(object sender, EventArgs e) {
            NewProject form = new NewProject();
            DialogResult ok_cancel = form.ShowDialog();
            if (ok_cancel == DialogResult.OK) {
                MessageBox.Show("OK");
            }
            if (ok_cancel == DialogResult.Cancel) {
                MessageBox.Show("Cancel");
            }
        }
    }

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public class BlockMdiSize : IMessageFilter {
        private MdiClient client;
        private Panel panel;

        public BlockMdiSize(MdiClient mc, Panel p) {
            client = mc;
            panel = p;
        }

        public bool PreFilterMessage(ref Message m) {
            const int WM_ENTERSIZEMOVE     = 0x0231;
            const int WM_EXITSIZEMOVE      = 0x0232;
            const int WM_MOVE              = 0x0003;
            const int WM_SIZE              = 0x0005;
            const int WM_SIZING            = 0x0214;
            const int WM_MOVING            = 0x0216;
            const int WM_WINDOWPOSCHANGING = 0x0046;
            const int WM_WINDOWPOSCHANGED  = 0x0047;

            switch (m.Msg) {
            case WM_SIZING:            case WM_SIZE:
            case WM_MOVING:            case WM_MOVE:
            case WM_ENTERSIZEMOVE:     case WM_EXITSIZEMOVE:
            case WM_WINDOWPOSCHANGING: case WM_WINDOWPOSCHANGED:
                client.Location = panel.Location;
                client.Size = panel.Size;
                break;
            }

            if (m.Msg == WM_WINDOWPOSCHANGED) {
                return true;
            }
            return false;
        }
    }
}
