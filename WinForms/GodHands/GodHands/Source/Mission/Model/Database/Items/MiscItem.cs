using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GodHands {
    public class MiscItem : InMemory {
        private DirRec name;
        private DirRec help;
        private int index;
        private int pos;

        public MiscItem(string url, int pos, int index, DirRec rec):
        base(url, 0, rec) {
            this.index = index;
            this.pos = pos;
            name = Model.GetRec("ITEMNAME.BIN");
            help = Model.GetRec("ITEMHELP.BIN");
        }

        public override int GetPos() {
            int address = GetRec().LbaData*2048;
            return address;
        }

        [Category("Misc Item")]
        [DisplayName("Name")]
        [Description("Name of the item (max 24 letters)")]
        public string Name {
            get {
                byte[] kildean = new byte[0x18];
                base.SetRec(name);
                SetPos(name.LbaData*2048);
                RamDisk.Get(GetPos() + index*0x18, 0x18, kildean);
                return Kildean.ToAscii(kildean);
            }
            set {
                string clip = value.Substring(0, Math.Min(0x18, value.Length));
                byte[] kildean = Kildean.ToKildean(clip, 0x18);
                base.SetRec(name);
                SetPos(name.LbaData*2048);
                UndoRedo.Exec(new BindArray(this, index*0x18, 0x18, kildean));
            }
        }

        [Category("Misc Item")]
        [DisplayName("Help")]
        [Description("Help info for item")]
        public string Help {
            get {
                base.SetRec(help);
                int ptr = 2*RamDisk.GetS16(GetPos() + index*0x02);
                int len = help.LenData - ptr;
                byte[] kildean = new byte[len];
                RamDisk.Get(GetPos() + ptr, len, kildean);
                return Kildean.ToAscii(kildean);
            }
            set {
                base.SetRec(help);
                int ptr = 2*RamDisk.GetS16(GetPos() + index*0x02);
                int len = help.LenData - ptr;
                string clip = value.Substring(0, Math.Min(len, value.Length));
                byte[] kildean = Kildean.ToKildean(clip, len);
                UndoRedo.Exec(new BindArray(this, ptr, len, kildean));
            }
        }
    }
}
