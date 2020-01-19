using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TestBed {
    public partial class NewProject : Form {
        public static Icon IconFromFile(string path) {
            try {
                string dir = AppDomain.CurrentDomain.BaseDirectory;
                Bitmap bitmap = new Bitmap(dir+path);
                return Icon.FromHandle(bitmap.GetHicon());
            } catch {
                return null;
            }
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

        public NewProject() {
            InitializeComponent();
            this.Icon = IconFromFile("/img/new-project.png");
            btn_cdrom.Image = ImageFromFile("/img/file-open.png");
            btn_emu.Image = ImageFromFile("/img/file-open.png");
        }

        private void btn_cancel_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btn_ok_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_cdrom_Click(object sender, EventArgs e) {

        }
    }
}
