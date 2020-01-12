using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public interface IEnumDir {
        bool Visit(string url, DirRec dir);
    }

    public static class Iso9660 {
        private static Dictionary<string,DirRec> Records = new Dictionary<string,DirRec>();
        private static Dictionary<string,int> Path2Pos = new Dictionary<string,int>();
        private static Dictionary<int,string> Lba2Path = new Dictionary<int,string>();
        public static VolDesc pvd = null;
        public static DirRec root = null;

        public static DirRec GetByPath(string url) {
            if (!Records.ContainsKey(url)) {
                return null;
            }
            return Records[url];
        }

        public static DirRec GetByLba(int lba) {
            if (!Lba2Path.ContainsKey(lba)) {
                return null;
            }
            string url = Lba2Path[lba];
            return GetByPath(url);
        }

        public static bool Open(string path) {
            Logger.SetProgress(0);
            Close();
            if (!RamDisk.Open(path)) {
                return false;
            }

            if (!RamDisk.Read(0x10)) {
                return false;
            }

            pvd = new VolDesc("CD:PVD", 0x10*2048);
            root = pvd.GetRootDir();
            Records.Add("CD:ROOT", root);
            Path2Pos.Add("CD:ROOT", root.GetPos());
            Lba2Path.Add((int)root.LbaData, "CD:ROOT");

            Publisher.Register("CD:PVD", pvd);
            Publisher.Register("CD:ROOT", root);

            EnumDir("CD:ROOT", root, null);
            Model.Open();
            if (View.disktool != null) {
                View.disktool.OpenDisk();
            }
            Logger.SetProgress(100);
            return Logger.Pass("File opened successfully "+path);
        }

        public static bool Close() {
            if (View.disktool != null) {
                View.disktool.CloseDisk();
            }
            Model.Close();
            Publisher.Unregister("CD:ROOT");
            Publisher.Unregister("CD:PVD");
            RamDisk.Close();
            UndoRedo.Reset();
            pvd = null;
            root = null;

            foreach (KeyValuePair<string,DirRec> pair in Records) {
                Publisher.Unregister(pair.Key);
            }
            Records.Clear();
            Path2Pos.Clear();
            Lba2Path.Clear();
            return Logger.Pass("File closed");
        }

        public static bool ReadFile(DirRec dir) {
            int lba = dir.LbaData;
            int len = dir.LenData;
            for (int i = 0; i < len; i += 2048) {
                if (!RamDisk.Read(lba++)) {
                    return false;
                }
            }
            return Logger.Pass("Read file "+dir.FileName);
        }

        public static string GetRecordFileName(int pos) {
            int len = RamDisk.GetU8(pos+32);
            for (int i = 0; i < len; i++) {
                byte c = RamDisk.GetU8(pos+33+i);
                if (c == ';') {
                    len = i;
                }
            }
            return RamDisk.GetString(pos+33, len);
        }

        public static bool EnumDir(string url, DirRec dir, IEnumDir iterator) {
            int pos = dir.LbaData*2048;
            int end = pos + dir.LenData;

            pos += RamDisk.GetU8(pos); // skip current directory
            pos += RamDisk.GetU8(pos); // skip parent directory
            while (pos < end) {
                byte len = RamDisk.GetU8(pos);
                if (len == 0) {
                    pos = ((pos/2048)+1)*2048;
                } else {
                    string key = url+"/"+GetRecordFileName(pos);
                    DirRec rec = null;
                    if (Records.ContainsKey(key)) {
                        rec = Records[key];
                    } else {
                        rec = new DirRec(key, pos);
                        Records.Add(key, rec);
                        Path2Pos.Add(key, rec.GetPos());
                        Lba2Path.Add(rec.LbaData, key);
                        Publisher.Register(key, rec);
                    }
                    if (rec.FileFlags_Directory) {
                        if (iterator != null) {
                            iterator.Visit(key, rec);
                        }
                    }
                    pos += len;
                }
            }

            pos = dir.LbaData*2048;
            pos += RamDisk.GetU8(pos); // skip current directory
            pos += RamDisk.GetU8(pos); // skip parent directory
            while (pos < end) {
                byte len = RamDisk.GetU8(pos);
                if (len == 0) {
                    pos = ((pos/2048)+1)*2048;
                } else {
                    string key = url+"/"+GetRecordFileName(pos);
                    DirRec rec = Records[key];
                    if (!rec.FileFlags_Directory) {
                        if (iterator != null) {
                            iterator.Visit(key, rec);
                        }
                    }
                    pos += len;
                }
            }
            return true;
        }

        public static bool EnumFileSys(IEnumDir iterator) {
            DirRec dir = Iso9660.GetByPath("CD:ROOT");
            return Iso9660.EnumDir("CD:ROOT", dir, iterator);
        }
    }
}
