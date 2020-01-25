using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class Room : InMemory {
        private MPD mpd;

        public Room(string url, int pos, DirRec rec):
        base(url, pos, rec) {
            mpd = Model.mpds[rec.GetUrl()];
            Name = rec.GetFileName();
        }

        public string Name { get; set; }
        public int NumSections { get; set; }
    }
}
