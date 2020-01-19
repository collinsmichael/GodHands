using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public partial class Base {
        public string URL { get; set; }
        public string Text { get; set; }
        public string ToolTip { get; set; }
        public int IconNormal { get; set; }
        public int IconSelect { get; set; }

        public virtual byte[] ToArray() {
            return null;
        }

        public virtual bool FromArray(byte[] data) {
            return false;
        }

        public void Export(string path) {
            File.WriteAllBytes(path, ToArray());
        }

        public bool Import(string path) {
            return FromArray(File.ReadAllBytes(path));
        }

        public void OnTreeClick(object sender, TreeNodeMouseClickEventArgs e) {
        }

        public void OnTreeSelect(object sender, TreeViewEventArgs e) {
        }
    }
}
