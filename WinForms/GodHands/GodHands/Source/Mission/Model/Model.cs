using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class EnumModelFiles : IEnumDir {
        private string[] extensions = new string[] {
            ".DAT", ".PRG", ".SYD", ".ARM", ".ZND",
            ".MPD", ".ZUD", ".SHP", ".WEP", ".SEQ"
        };

        public bool Visit(string url, DirRec dir) {
            string name = dir.GetFileName();
            string ext = dir.GetFileExt();
            if (extensions.Contains(ext)) {
                //Iso9660.ReadFile(dir);
                int lba = dir.LbaData;
                int len = dir.LenData;
                int pos = lba*2048;
                switch (ext) {
                case ".DAT": Model.dats.Add(url, new DAT(url, pos)); break;
                case ".PRG": Model.prgs.Add(url, new PRG(url, pos)); break;
                case ".SYD": Model.syds.Add(url, new SYD(url, pos)); break;
                case ".ARM": Model.arms.Add(url, new ARM(url, pos)); break;
                case ".ZND": Model.znds.Add(url, new ZND(url, pos)); break;
                case ".MPD": Model.mpds.Add(url, new MPD(url, pos)); break;
                case ".ZUD": Model.zuds.Add(url, new ZUD(url, pos)); break;
                case ".SHP": Model.shps.Add(url, new SHP(url, pos)); break;
                case ".WEP": Model.weps.Add(url, new WEP(url, pos)); break;
                case ".SEQ": Model.seqs.Add(url, new SEQ(url, pos)); break;
                }

                switch (ext) {
                case ".DAT": case ".PRG": case ".SYD": case ".ARM": case ".ZND":
                case ".MPD": case ".ZUD": case ".SEQ": case ".SHP": case ".WEP":
                    Model.SetPos(name, pos);
                    Model.SetLen(name, len);
                    Model.SetRec(name, dir);
                    if (name == "00.SHP") {
                        Model.lba_00_shp = lba;
                    }
                    if (name == "01.WEP") {
                        Model.lba_01_wep = lba;
                    }
                    break;
                }

                if (ext == ".ZND") {
                    string[] parts = url.Split(new char[] {':'});
                    if (parts.Length > 1) {
                        string key = "APP:" + parts[1];
                        Zone zone = new Zone(key, 0, dir);
                        Model.zones.Add(key, zone);
                        Model.Add(key, zone);
                        Publisher.Register(zone);
                    }
                }

            } else if (name.Contains("SLUS")) {
                int pos = dir.LbaData*2048;
                int len = dir.LenData;
                Model.SetPos("SLUS", pos);
                Model.SetLen("SLUS", len);
                Model.SetRec("SLUS", dir);
                PRG prg = new PRG(url, pos);
                Model.prgs.Add(url, prg);
                Publisher.Register(prg);
            } else if (dir.FileFlags_Directory) {
                return Iso9660.EnumDir(url, dir, this);
            }
            return true;
        }
    }

    public static class Model {
        public static Dictionary<string, int> file_pos = new Dictionary<string, int>();
        public static Dictionary<string, int> file_len = new Dictionary<string, int>();
        public static Dictionary<string, DirRec> file_rec = new Dictionary<string, DirRec>();

        public static Dictionary<string, object> map = new Dictionary<string, object>();
        public static Dictionary<string, PRG> prgs = new Dictionary<string, PRG>();
        public static Dictionary<string, DAT> dats = new Dictionary<string, DAT>();
        public static Dictionary<string, SYD> syds = new Dictionary<string, SYD>();
        public static Dictionary<string, ARM> arms = new Dictionary<string, ARM>();
        public static Dictionary<string, ZND> znds = new Dictionary<string, ZND>();
        public static Dictionary<string, MPD> mpds = new Dictionary<string, MPD>();
        public static Dictionary<string, ZUD> zuds = new Dictionary<string, ZUD>();
        public static Dictionary<string, SHP> shps = new Dictionary<string, SHP>();
        public static Dictionary<string, WEP> weps = new Dictionary<string, WEP>();
        public static Dictionary<string, SEQ> seqs = new Dictionary<string, SEQ>();
        public static Dictionary<string, Zone> zones = new Dictionary<string, Zone>();
        public static Dictionary<string, Room> rooms = new Dictionary<string, Room>();
        public static Dictionary<string, Actor> actors = new Dictionary<string, Actor>();

        public static int lba_00_shp = 0;
        public static int lba_01_wep = 0;

        // ********************************************************************
        // initialize model from file
        // ********************************************************************
        public static bool Open() {
            Iso9660.EnumFileSys(new EnumModelFiles());
            return true;
        }

        // ********************************************************************
        // release all resources
        // ********************************************************************
        public static bool Close() {
            zones.Clear();
            rooms.Clear();
            actors.Clear();
            prgs.Clear();
            dats.Clear();
            syds.Clear();
            arms.Clear();
            znds.Clear();
            mpds.Clear();
            zuds.Clear();
            shps.Clear();
            weps.Clear();
            seqs.Clear();
            map.Clear();

            file_pos.Clear();
            file_len.Clear();
            file_rec.Clear();
            lba_00_shp = 0;
            lba_01_wep = 0;
            return true;
        }

        public static bool Add(string key, object obj) {
            if (!map.ContainsKey(key)) {
                map.Add(key, obj);
            }
            return true;
        }

        public static object Get(string key) {
            if (map.ContainsKey(key)) {
                return map[key];
            }
            return null;
        }

        public static int GetPos(string url) {
            if (file_pos.ContainsKey(url)) {
                return file_pos[url];
            }
            return 0;
        }

        public static void SetPos(string url, int pos) {
            if (file_pos.ContainsKey(url)) {
                file_pos[url] = pos;
            } else {
                file_pos.Add(url, pos);
            }
        }

        public static int GetLen(string url) {
            if (file_len.ContainsKey(url)) {
                return file_len[url];
            }
            return 0;
        }

        public static void SetLen(string url, int len) {
            if (file_len.ContainsKey(url)) {
                file_len[url] = len;
            } else {
                file_len.Add(url, len);
            }
        }

        public static DirRec GetRec(string url) {
            if (file_rec.ContainsKey(url)) {
                return file_rec[url];
            }
            return null;
        }

        public static void SetRec(string url, DirRec rec) {
            if (file_rec.ContainsKey(url)) {
                file_rec[url] = rec;
            } else {
                file_rec.Add(url, rec);
            }
        }

        public static bool UpdateLbaTable(string ext, int index, int lba, int len) {
            int pos = 0;
            int ptr = 0;
            try {
                switch (ext) {
                case ".ARM": pos = file_pos["MENU5.PRG"];  ptr = 0x00000300; break;
                case ".ZND": pos = file_pos["SLUS"];       ptr = 0x0003FCCC; break;
                case ".SHP": pos = file_pos["BATTLE.PRG"]; ptr = 0x0007FE60; break;
                case ".WEP": pos = file_pos["BATTLE.PRG"]; ptr = 0x00080500; break;
                default: return true;
                }
            } catch {
                return Logger.Fail("File not found!");
            }
            // TODO
            return true;
        }
    }
}
