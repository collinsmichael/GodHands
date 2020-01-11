using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public static class Iso9660 {
        public static VolDesc pvd = null;
        public static DirRec root = null;

        public static bool Open(string path) {
            Logger.SetProgress(0);
            Close();
            if (!RamDisk.Open(path)) {
                return false;
            }

            if (!RamDisk.Read(0x10)) {
                return false;
            }

            pvd = new VolDesc("PVD", 0x10*2048);
            Publisher.Register("PVD", pvd);
            root = pvd.GetRootDir();
            Publisher.Register("ROOT", root);

            Logger.SetProgress(100);
            return Logger.Pass("File opened successfully "+path);
        }

        public static bool Close() {
            RamDisk.Close();
            pvd = null;
            Publisher.Unregister("PVD");
            return Logger.Pass("File closed");
        }
    }
}
