using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public class RoomTreasureSection : InMemory {
        private int len;
        public RoomTreasureSection(string url, int pos, int len, DirRec rec):
        base(url, pos, rec) {
            this.len = len;
        }

        [ReadOnly(true)][Category(" INTERNAL")]
        public int LenSection {
            get { return len; }
            set {}
        }

        public bool OpenSection(TreeNode root) {
            return true;
        }
    }
}
