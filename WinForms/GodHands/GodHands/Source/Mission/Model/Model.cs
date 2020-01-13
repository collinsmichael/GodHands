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
            if ((extensions.Contains(ext)) || (name.Contains("SLUS"))) {
                //Iso9660.ReadFile(dir);
                int pos = dir.LbaData*2048;
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
            } else if (dir.FileFlags_Directory) {
                return Iso9660.EnumDir(url, dir, this);
            }
            return true;
        }
    }

    public static class Model {
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
            return true;
        }

        //public static bool Open() {
        //    actors.Add("/actor/0", new Actor("/actor/0", 0x0000));
        //    actors.Add("/actor/1", new Actor("/actor/1", 0x0800));
        //    actors.Add("/actor/2", new Actor("/actor/2", 0x1000));
        //    actors.Add("/actor/3", new Actor("/actor/3", 0x1800));
        //    Publisher.Register("/actor/0", actors["/actor/0"]);
        //    Publisher.Register("/actor/1", actors["/actor/1"]);
        //    Publisher.Register("/actor/2", actors["/actor/2"]);
        //    Publisher.Register("/actor/3", actors["/actor/3"]);
        //    return true;
        //}
    }
}
