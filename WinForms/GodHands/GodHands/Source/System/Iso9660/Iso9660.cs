using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public interface IEnumDir {
        bool Visit(string url, Record dir);
    }

    public static partial class Iso9660 {
        private static Dictionary<string,Record> Records = new Dictionary<string,Record>();
        private static Dictionary<string,int> Path2Pos = new Dictionary<string,int>();
        private static Dictionary<int,string> Lba2Path = new Dictionary<int,string>();

        public static Disk disk = null;
        public static Volume pvd = null;
        public static Record root = null;

        public static Record GetByPath(string url) {
            if (!Records.ContainsKey(url)) {
                return null;
            }
            return Records[url];
        }

        public static Record GetByLba(int lba) {
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

            RamDisk.map[0x11] = 0x6F;
            for (int i = 0; i < 0x10; i++) {
                RamDisk.map[i] = 0x6F;
            }

            disk = new Disk();

            pvd = new Volume(null, "CD:PVD", 0x10*2048);
            root = pvd.GetRootDir();
            Model.SetRec("CD:ROOT", root);
            int lba = root.LbaData;
            int len = root.LenData;
            for (int i = 0; i < len; i++) {
                RamDisk.Read(lba+i);
            }
            int lenp = pvd.PathTableSize;
            int lba1 = pvd.LbaPathTable1;
            int lba2 = pvd.LbaPathTable2;
            for (int i = 0; i < lenp; i++) {
                RamDisk.Read(lba1+i);
                RamDisk.Read(lba2+i);
            }

            Records.Add("CD:ROOT", root);
            Path2Pos.Add("CD:ROOT", root.GetPos());
            Lba2Path.Add(lba, "CD:ROOT");
            Publisher.Register(pvd);
            Publisher.Register(root);

            EnumDir("CD:ROOT", root, null);
            Model.Open();
            if (View.disktool != null) {
                View.disktool.OpenDisk();
            }

            string dir = AppDomain.CurrentDomain.BaseDirectory;
            File.WriteAllBytes(dir+"disk.map", RamDisk.map);

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

            foreach (KeyValuePair<string,Record> pair in Records) {
                Publisher.Unregister(pair.Key);
            }
            Records.Clear();
            Path2Pos.Clear();
            Lba2Path.Clear();
            Logger.SetProgress(100);
            return Logger.Pass("File closed");
        }

        public static bool ReadFile(Record dir) {
            int lba = dir.LbaData;
            int len = dir.LenData;
            for (int i = 0; i < len; i += 2048) {
                if (!RamDisk.Read(lba++)) {
                    return false;
                }
            }
            Logger.SetProgress(100);
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

        public static bool EnumDir(string url, Record dir, IEnumDir iterator) {
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
                    Record rec = null;
                    if (Records.ContainsKey(key)) {
                        rec = Records[key];
                    } else {
                        rec = new Record(dir, key, pos);
                        Model.SetRec(key, rec);
                        string name = rec.GetFileName();
                        int lba = rec.LbaData;
                        int num = (rec.LenData+2047)/2048;
                        if (rec.FileFlags_Directory) {
                            for (int i = 0; i < num; i++) {
                                RamDisk.Read(lba+i);
                            }
                        } else {
                            for (int i = 0; i < num; i++) {
                                if (RamDisk.map[lba+i] == 0) {
                                    RamDisk.map[lba+i] = 0x6F;
                                }
                            }
                        }
                        try {
                            Records.Add(key, rec);
                            Path2Pos.Add(key, rec.GetPos());
                            Lba2Path.Add(lba, key);
                            Publisher.Register(rec);
                        } catch (Exception e) {
                            Logger.Warn("Cannot register "+name+" "+e.Message);
                        }
                    }
                    if (rec.FileFlags_Directory) {
                        int lba = rec.LbaData;
                        int num = (rec.LenData+2047)/2048;
                        for (int i = 0; i < num; i++) {
                            RamDisk.Read(lba+i);
                        }

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
                    Record rec = Records[key];
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
            Record dir = GetByPath("CD:ROOT");
            return EnumDir("CD:ROOT", dir, iterator);
        }

        public static string FindCollision(Record rec, int sectors) {
            string collisions = "";
            int lba = rec.LbaData;
            int len = (rec.LenData+2047)/2048;
            for (int i = len; i < sectors; i++) {
                int key = lba + i;
                if (Lba2Path.ContainsKey(key)) {
                    collisions = collisions + "    " + Lba2Path[key] + "\r\n";
                }
            }
            return collisions;
        }

        public static int FindCapacity(Record rec) {
            int count = (rec.LenData+2047)/2048;
            int capacity = count;
            for (int lba = rec.LbaData+count; lba < RamDisk.count; lba++) {
                if (Lba2Path.ContainsKey(lba)) {
                    break;
                }
                capacity++;
            }
            return capacity;
        }

        private static int next = 0;
        public static int NextFit(int num_sectors) {
            int total = RamDisk.count;
            for (int lba = next; (lba+1) % total != next; lba = (lba+1) % total) {
                for (int i = 0; i < num_sectors; i++) {
                    int pos = (lba + i) % total;
                    if (RamDisk.map[pos] != 0) {
                        lba = lba + i;
                        break;
                    } else if (i == num_sectors-1) {
                        next = (pos+1) % total;
                        return lba;
                    }
                }
            }
            return 0;
        }

        public static bool ResizeRecord(Record rec, int len) {
            // first check if there is enough space
            int lba = rec.LbaData;
            int num = (rec.LenData+2047)/2048;
            for (int i = num; i < len; i++) {
                if (RamDisk.map[lba + i] != 0) {
                    return Logger.Fail("Cannot resize "+rec.GetFileName()+" not enough space");
                }
            }
            // claim new sectors
            for (int i = num; i < len; i++) {
                RamDisk.map[lba + i] = 0x6F;
            }
            // free old sectors
            for (int i = len; i < num; i++) {
                RamDisk.map[lba + i] = 0x00;
            }
            rec.LenData = len;
            return true;
        }

        public static bool MoveRecord(Record rec, int lba) {
            // first check if there is enough space
            int old = rec.LbaData;
            int len = (rec.LenData+2047)/2048;
            for (int i = 0; i < len; i++) {
                // self intersection is not a problem
                if ((lba+i >= old) && (lba+i < old+len)) {
                    continue;
                }
                // check for collisions
                if (RamDisk.map[lba + i] != 0) {
                    return Logger.Fail("Cannot move "+rec.GetFileName()+" not enough space");
                }
            }
            // swap the sectors
            for (int i = 0; i < len; i++) {
                int src = old + i;
                int des = lba + i;
                if (!RamDisk.Swap(src, des)) {
                    return false;
                }
            }
            rec.LbaData = lba;
            for (int i = 0; i < len; i++) {
                RamDisk.map[lba + i] = 0x78;
                RamDisk.map[old + i] = 0x00;
            }
            return true;
        }

        public static bool RenameRecord(Record rec, string name) {
            // compute new size of the record
            // make space for new record
            // update the parent records LenData
            // update affected records
            // rename the record

            // ALSO update the path tables if this record is a directory
            return true;
        }
    }
}
