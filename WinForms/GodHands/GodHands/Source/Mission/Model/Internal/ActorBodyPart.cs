using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GodHands {
    public class ActorBodyPart : InMemory {
        public ActorBodyPart(string url, int pos, DirRec rec):
        base(url, pos, rec) {
        }

        [Category("Stats")]
        [DisplayName("HP")]
        [Description("Health points (depleted when taking damage)")]
        public short HP {
            get { return RamDisk.GetS16(GetPos()+0x00); }
            set { UndoRedo.Exec(new BindS16(this, 0x00, value)); }
        }

        [Category("Stats")]
        [DisplayName("AGL")]
        [Description("Agility defensive bonus")]
        public byte AGL {
            get { return RamDisk.GetU8(GetPos()+0x02); }
            set { UndoRedo.Exec(new BindU8(this, 0x02, value)); }
        }

        [Category("Stats")]
        [DisplayName("EVD")]
        [Description("Chain evasion bonus")]
        public byte EVD {
            get { return RamDisk.GetU8(GetPos()+0x02); }
            set { UndoRedo.Exec(new BindU8(this, 0x02, value)); }
        }

        [Category("Types")]
        [DisplayName("Blunt")]
        [Description("Resistence to blunt attacks")]
        public byte Blunt {
            get { return RamDisk.GetU8(GetPos()+0x03); }
            set { UndoRedo.Exec(new BindU8(this, 0x03, value)); }
        }

        [Category("Types")]
        [DisplayName("Edged")]
        [Description("Resistence to edged attacks")]
        public byte Edged {
            get { return RamDisk.GetU8(GetPos()+0x05); }
            set { UndoRedo.Exec(new BindU8(this, 0x05, value)); }
        }

        [Category("Types")]
        [DisplayName("Piercing")]
        [Description("Resistence to piercing attacks")]
        public byte Piercing {
            get { return RamDisk.GetU8(GetPos()+0x06); }
            set { UndoRedo.Exec(new BindU8(this, 0x06, value)); }
        }

        [Category("Types")]
        [DisplayName("Padding")]
        [Description("Unused")]
        public byte Padding {
            get { return RamDisk.GetU8(GetPos()+0x04); }
            set { UndoRedo.Exec(new BindU8(this, 0x04, value)); }
        }
    }
}
