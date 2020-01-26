using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class InMemory : BaseClass {
        private DirRec rec;

        public InMemory(string url, int pos, DirRec rec) : base(url, pos) {
            this.rec = rec;
        }

        public DirRec GetRec() {
            return rec;
        }

        public void SetRec(DirRec rec) {
            this.rec = rec;
        }

        public override int GetPos() {
            int address = rec.LbaData*2048;
            int offset = base.GetPos();
            return address + offset;
        }
    }
}
