using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
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
