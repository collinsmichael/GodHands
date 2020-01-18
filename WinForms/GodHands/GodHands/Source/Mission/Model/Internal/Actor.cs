using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class Actor : BaseClass {
        private DirRec rec;
        private ZUD zud;
        public string Name;

        public Actor(string url, int pos, DirRec rec) : base(url, pos) {
            this.rec = rec;
            zud = Model.zuds[rec.GetUrl()];
            Name = rec.GetFileName();
        }

        //public string Name {
        //    get { return RamDisk.GetString(GetPos()+0x00, 0x18); }
        //    set { UndoRedo.Exec(new BindString(this, 0x00, 0x18, value)); }
        //}

        public ushort HP {
            get { return RamDisk.GetU16(GetPos()+0x18); }
            set { UndoRedo.Exec(new BindU16(this, 0x18, value)); }
        }

        public ushort MP {
            get { return RamDisk.GetU16(GetPos()+0x1A); }
            set { UndoRedo.Exec(new BindU16(this, 0x1A, value)); }
        }

        public byte INT {
            get { return RamDisk.GetU8(GetPos()+0x1C); }
            set { UndoRedo.Exec(new BindU8(this, 0x1C, value)); }
        }

        public byte AGL {
            get { return RamDisk.GetU8(GetPos()+0x1D); }
            set { UndoRedo.Exec(new BindU8(this, 0x1D, value)); }
        }

        public byte STR {
            get { return RamDisk.GetU8(GetPos()+0x1E); }
            set { UndoRedo.Exec(new BindU8(this, 0x1E, value)); }
        }
    }
}
