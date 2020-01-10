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

    public static class Model {
        public static Dictionary<string, Actor> actors = new Dictionary<string, Actor>();

        // ********************************************************************
        // initialize model from file
        // ********************************************************************
        public static bool Open() {
            actors.Add("/actor/0", new Actor("/actor/0", 0x0000));
            actors.Add("/actor/1", new Actor("/actor/1", 0x0800));
            actors.Add("/actor/2", new Actor("/actor/2", 0x1000));
            actors.Add("/actor/3", new Actor("/actor/3", 0x1800));
            Publisher.Register("/actor/0", actors["/actor/0"]);
            Publisher.Register("/actor/1", actors["/actor/1"]);
            Publisher.Register("/actor/2", actors["/actor/2"]);
            Publisher.Register("/actor/3", actors["/actor/3"]);
            return true;
        }
    }
}
