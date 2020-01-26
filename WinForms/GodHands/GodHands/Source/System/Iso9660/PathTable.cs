using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class PathTable : BaseClass {
        public PathTable(string url, int pos) : base(url, pos) {
            if (RamDisk.map[pos/2048] == 0) {
                RamDisk.map[pos/2048] = 0x6F;
            }
        }

        public override int GetLen() {
            return 0;
        }

        public byte LenDirName { get; set; }
        public byte LenXA { get; set; }
        public int LbaData { get; set; }
        public short ParentDirNo { get; set; }
        public string DirName { get; set; }
    }
}
