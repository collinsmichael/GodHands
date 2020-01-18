using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class Room : BaseClass {
        private DirRec rec;
        private MPD mpd;
        public string Name { get; set; }
        public int NumSections { get; set; }

        public Room(string url, int pos, DirRec rec) : base(url, pos) {
            this.rec = rec;
            mpd = Model.mpds[rec.GetUrl()];
            Name = rec.GetFileName();
        }

        public DirRec GetRec() {
            return rec;
        }
    }
}
