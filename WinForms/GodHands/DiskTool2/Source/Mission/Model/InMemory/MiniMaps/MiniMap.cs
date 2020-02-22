using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class MiniMap : InMemory {
        public MiniMap(BaseClass parent, string url, int pos, Record rec):
        base(parent, url, pos, rec) {
        }

        public override int GetLen() {
            return 0;
        }
    }
}
