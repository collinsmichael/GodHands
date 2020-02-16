using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class Disk : BaseClass {
        public Volume volume;
        public Record root;

        public Disk() : base(null, "CD:", 0) {
            volume = new Volume(this, "CD:PVD", 0x10*2048);
            root = volume.GetRootDir();
        }
    }
}
