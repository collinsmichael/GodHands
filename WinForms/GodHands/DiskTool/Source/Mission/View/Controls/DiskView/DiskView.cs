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
    public partial class DiskView : TreeView {
        int icon_disk;
        int icon_dir1;
        int icon_dir2;

        public DiskView() : base() {
            SysIcons.GetSysIcons(this);
            icon_disk = SysIcons.GetDiskIconIndex();
            icon_dir1 = SysIcons.GetDirIconIndex(false);
            icon_dir2 = SysIcons.GetDirIconIndex(true);
            InitDragDrop();
        }

        public bool OpenDisk() {
            Volume volume = Iso9660.pvd;
            string key = volume.Key;
            Record root = Iso9660.root;
            string text = volume.VolumeIdentifier.Trim();
            BeginUpdate();
                Nodes.Clear();
                TreeNode node1 = Nodes.Add(key, "CDROM", icon_disk, icon_disk);
                node1.Tag = volume;
                TreeNode node2 = node1.Nodes.Add(root.Key, text, icon_disk, icon_disk);
                node2.Tag = root;
                Iso9660.EnumFileSys(new EnumDiskView(node2));
                node1.Expand();
                node2.Expand();
            EndUpdate();
            Publisher.Subscribe("*", this);
            return true;
        }

        public bool CloseDisk() {
            Publisher.Unsubscribe("*", this);
            Nodes.Clear();
            return true;
        }
    }
}
