using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class Binary : BaseClass {
        public Binary(Record parent, string url, int offset, int length):
        base(parent, url, offset) {
        }

        public override int GetPos() {
            Record record = parent as Record;
            if (record != null) {
                return record.LbaData*2048 + offset;
            }
            return offset;
        }
    }
}
