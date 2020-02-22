using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class EnumModelFiles : IEnumDir {
        private string[] extensions = new string[] {
            ".BIN", ".DAT", ".PRG", ".SYD",
            ".ARM", ".ZND", ".MPD",
            ".ZUD", ".SHP", ".WEP", ".SEQ"
        };

        public bool Visit(string url, Record dir) {
            string name = dir.GetFileName();
            string ext = dir.GetFileExt();
            if (extensions.Contains(ext)) {
                int lba = dir.LbaData;
                int len = dir.LenData;
                int pos = lba*2048;
                switch (ext) {
                case ".BIN": Model.bins.Add(url, new BIN(null, url, pos)); break;
                case ".DAT": Model.dats.Add(url, new DAT(null, url, pos)); break;
                case ".PRG": Model.prgs.Add(url, new PRG(null, url, pos)); break;
                case ".SYD": Model.syds.Add(url, new SYD(null, url, pos)); break;
                case ".ARM": Model.arms.Add(url, new ARM(null, url, pos)); break;
                case ".ZND": Model.znds.Add(url, new ZND(null, url, pos)); break;
                case ".MPD": Model.mpds.Add(url, new MPD(null, url, pos)); break;
                case ".ZUD": Model.zuds.Add(url, new ZUD(null, url, pos)); break;
                case ".SHP": Model.shps.Add(url, new SHP(null, url, pos)); break;
                case ".WEP": Model.weps.Add(url, new WEP(null, url, pos)); break;
                case ".SEQ": Model.seqs.Add(url, new SEQ(null, url, pos)); break;
                }

                switch (ext) {
                case ".BIN": case ".DAT": case ".PRG": case ".SYD":
                case ".ARM": case ".ZND": case ".MPD":
                case ".ZUD": case ".SEQ": case ".SHP": case ".WEP":
                    Model.SetRec(name, dir);
                    if (name == "ITEMNAME.BIN") {
                        Model.SetRec("ITEMNAME.BIN", dir);
                    }
                    if (name == "ITEMHELP.BIN") {
                        Model.SetRec("ITEMHELP.BIN", dir);
                    }
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
                        Zone zone = new Zone(null, key, 0, dir);
                        Model.zones.Add(key, zone);
                        Model.Add(key, zone);
                        Publisher.Register(zone);
                    }
                }

            } else if (name.Contains("SLUS")) {
                int pos = dir.LbaData*2048;
                int len = dir.LenData;
                Model.SetRec("SLUS", dir);
                PRG prg = new PRG(null, url, pos);
                Model.prgs.Add(url, prg);
                Publisher.Register(prg);
            } else if (dir.FileFlags_Directory) {
                return Iso9660.EnumDir(url, dir, this);
            }
            return true;
        }
    }

    public static class Model {
        public static Dictionary<string, Record> file_rec = new Dictionary<string, Record>();
        public static Dictionary<string, object> map = new Dictionary<string, object>();
        public static Dictionary<string, BIN> bins = new Dictionary<string, BIN>();
        public static Dictionary<string, DAT> dats = new Dictionary<string, DAT>();
        public static Dictionary<string, PRG> prgs = new Dictionary<string, PRG>();
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

        public static ItemNamesList itemnames = new ItemNamesList();
        public static ItemNameAccessoryList accessory_names = new ItemNameAccessoryList();
        public static ItemNameArmourList armour_names = new ItemNameArmourList();
        public static ItemNameBladeList blade_names = new ItemNameBladeList();
        public static ItemNameGripList grip_names = new ItemNameGripList();
        public static ItemNameGemList gem_names = new ItemNameGemList();
        
        public static CategoryArmours category_armours = new CategoryArmours();
        public static CategoryBlades category_blades = new CategoryBlades();
        public static CategoryGrips category_grips = new CategoryGrips();
        public static CategorySkills category_skills = new CategorySkills();

        public static TargetSphere targets = new TargetSphere();
        public static ItemNameMaterialList materials = new ItemNameMaterialList();
        public static DamageStats damage_stats = new DamageStats();
        public static DamageTypes damage_types = new DamageTypes();
        public static SkillsList skills = new SkillsList();
        public static CompassDirectionList compass = new CompassDirectionList();

        public static int lba_00_shp = 0;
        public static int lba_01_wep = 0;

        // ********************************************************************
        // initialize model from file
        // ********************************************************************
        public static bool Open() {
            Model.Close();
            Iso9660.EnumFileSys(new EnumModelFiles());
            itemnames.Load();
            skills.Load();
            return true;
        }

        // ********************************************************************
        // release all resources
        // ********************************************************************
        public static bool Close() {
            Program.MainForm.Reset();
            itemnames.Clear();
            skills.Clear();

            zones.Clear();
            rooms.Clear();
            actors.Clear();
            bins.Clear();
            dats.Clear();
            prgs.Clear();
            syds.Clear();
            arms.Clear();
            znds.Clear();
            mpds.Clear();
            zuds.Clear();
            shps.Clear();
            weps.Clear();
            seqs.Clear();
            map.Clear();

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

        public static Record GetRec(string url) {
            if (file_rec.ContainsKey(url)) {
                return file_rec[url];
            }
            return null;
        }

        public static void SetRec(string url, Record rec) {
            if (file_rec.ContainsKey(url)) {
                file_rec[url] = rec;
            } else {
                file_rec.Add(url, rec);
            }
        }
    }
}
