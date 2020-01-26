using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class SYD : BaseClass {
        public SYD(string url, int pos) : base(url, pos) {
        }

        public override int GetLen() {
            return 0;
        }
    }
}
