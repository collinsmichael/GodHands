using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class Actor : IBound {
        private string url;
        private int pos;

        public Actor(string url, int pos) {
            this.url = url;
            this.pos = pos;
        }

        public string GetUrl() {
            return url;
        }

        public int GetPos() {
            return pos;
        }

        public void SetPos(int pos) {
            this.pos = pos;
        }

        public string Name {
            get { return RamDisk.GetString(pos+0x00, 0x18); }
            set { UndoRedo.Exec(new BindString(this, 0x00, 0x18, value)); }
        }

        public ushort HP {
            get { return RamDisk.GetU16(pos+0x18); }
            set { UndoRedo.Exec(new BindU16(this, 0x18, value)); }
        }

        public ushort MP {
            get { return RamDisk.GetU16(pos+0x1A); }
            set { UndoRedo.Exec(new BindU16(this, 0x1A, value)); }
        }

        public byte INT {
            get { return RamDisk.GetU8(pos+0x1C); }
            set { UndoRedo.Exec(new BindU8(this, 0x1C, value)); }
        }

        public byte AGL {
            get { return RamDisk.GetU8(pos+0x1D); }
            set { UndoRedo.Exec(new BindU8(this, 0x1D, value)); }
        }

        public byte STR {
            get { return RamDisk.GetU8(pos+0x1E); }
            set { UndoRedo.Exec(new BindU8(this, 0x1E, value)); }
        }
    }
}
