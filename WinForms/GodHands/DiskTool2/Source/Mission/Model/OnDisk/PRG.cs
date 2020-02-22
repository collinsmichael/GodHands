using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class PRG : BaseClass {
        public PRG(BaseClass parent, string url, int pos):
        base(parent, url, pos) {
        }

        public override int GetLen() {
            return 0;
        }
    }
}
