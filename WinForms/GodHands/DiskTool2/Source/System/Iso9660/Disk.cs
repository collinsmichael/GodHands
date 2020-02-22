using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class Disk : BaseClass {
        public Volume volume;
        public Record root;
        public Folder dir;

        public Disk() : base(null, "CD:", 0) {
            //Publisher.Register(volume = new Volume("CD:PVD", 0x10*2048));
            //Publisher.Register(root = volume.GetRootDir());
            //Iso9660.ReadFile(root);
            //Publisher.Register(dir = new Folder(root, "CD:ROOT/", 0, root.LenData));
        }
    }
}
