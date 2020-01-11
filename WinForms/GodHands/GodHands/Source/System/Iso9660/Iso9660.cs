using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
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

            EnumDir("CD:ROOT", root);
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
            Publisher.Unregister("CD:ROOT");
            Publisher.Unregister("CD:PVD");
            RamDisk.Close();
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

        public static bool EnumDir(string url, DirRec dir) {
            if (dir == null) {
                return false;
            }
            int lba = (int)dir.LbaData;
            int len = (int)dir.LenData;

            int sector = lba;
            for (int ptr = 0; ptr < len; ptr += 2048) {
                RamDisk.Read(sector++);
            }

            int pos = lba*2048;
            int end = pos + len;
            pos += RamDisk.GetU8(pos); // skip current directory
            pos += RamDisk.GetU8(pos); // skip parent directory

            while (pos < end) {
                byte len_rec = RamDisk.GetU8(pos);
                if (len_rec == 0) {
                    pos = ((pos/2048)+1)*2048;
                    continue;
                }

                int len_str = RamDisk.GetU8(pos+32);
                for (int i = 0; i < len_str; i++) {
                    byte c = RamDisk.GetU8(pos+32+i);
                    if (c == ';') {
                        len_str = i-1;
                    }
                }

                string name = RamDisk.GetString(pos+33, len_str);
                string key = url+"/"+name;

                DirRec rec = new DirRec(key, pos);
                int rec_lba = (int)rec.LbaData;
                Records.Add(key, rec);
                Path2Pos.Add(key, pos);
                Lba2Path.Add(rec_lba, key);
                Publisher.Register(key, rec);
                if (rec.FileFlags_Directory) {
                    EnumDir(key, rec);
                }
                pos += len_rec;
            }
            return true;
        }

        public static bool EnumFileSystem(TreeNode tree, string url) {
            DirRec dir = Records[url];
            if (dir == null) {
                return false;
            }
            int lba = (int)dir.LbaData;
            int len = (int)dir.LenData;
            int end = lba*2048 + len;

            int pos = lba*2048;
            pos += RamDisk.GetU8(pos); // skip current directory
            pos += RamDisk.GetU8(pos); // skip parent directory
            while (pos < end) {
                byte len_rec = RamDisk.GetU8(pos);
                if (len_rec == 0) {
                    pos = ((pos/2048)+1)*2048;
                    continue;
                }

                int len_str = RamDisk.GetU8(pos+32);
                for (int i = 0; i < len_str; i++) {
                    byte c = RamDisk.GetU8(pos+32+i);
                    if (c == ';') {
                        len_str = i-1;
                    }
                }

                string name = RamDisk.GetString(pos+33, len_str);
                string key = url+"/"+name;

                DirRec rec = Records[key];
                if ((rec != null) && (rec.FileFlags_Directory)) {
                    TreeNode node = tree.Nodes.Add(key, name);
                    EnumFileSystem(node, key);
                }
                pos += len_rec;
            }

            pos = lba*2048;
            pos += RamDisk.GetU8(pos); // skip current directory
            pos += RamDisk.GetU8(pos); // skip parent directory
            while (pos < end) {
                byte len_rec = RamDisk.GetU8(pos);
                if (len_rec == 0) {
                    pos = ((pos/2048)+1)*2048;
                    continue;
                }

                int len_str = RamDisk.GetU8(pos+32);
                for (int i = 0; i < len_str; i++) {
                    byte c = RamDisk.GetU8(pos+32+i);
                    if (c == ';') {
                        len_str = i-1;
                    }
                }

                string name = RamDisk.GetString(pos+33, len_str);
                string key = url+"/"+name;

                DirRec rec = Records[key];
                if ((rec != null) && (!rec.FileFlags_Directory)) {
                    TreeNode node = tree.Nodes.Add(key, name);
                }
                pos += len_rec;
            }
            return true;
        }
    }
}
