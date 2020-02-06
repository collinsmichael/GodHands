using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GodHands {
    public class TreasureMiscItem : InMemory {
        public TreasureMiscItem(string url, int pos, DirRec rec):
        base(url, pos, rec) {
        }

        public override int GetLen() {
            return 0x34;
        }

        [ReadOnly(true)]
        [Category("01 Misc Item")]
        [DisplayName("Item Names List Raw")]
        [Description("Name of the item")]
        public short ItemNamesListRaw {
            get { return RamDisk.GetS16(GetPos()+0x00); }
            set { UndoRedo.Exec(new BindS16(this, 0x00, value)); }
        }

        [Category("01 Misc Item")]
        [DisplayName("Item Names List")]
        [Description("Name of the item")]
        [DefaultValue("")]
        [TypeConverter(typeof(ItemNamesListDropDown))]
        public string ItemNamesList {
            get {
                short index = RamDisk.GetS16(GetPos()+0x00);
                return Model.itemnames.GetName(index);
            }
            set {
                short index = (short)Model.itemnames.GetIndexByName(value);
                UndoRedo.Exec(new BindS16(this, 0x00, index));
            }
        }

        [ReadOnly(true)]
        [Category("01 Misc Item")]
        [DisplayName("Unknown")]
        [Description("Always 3 if it exists zero otherwise")]
        public byte Exists {
            get { return RamDisk.GetU8(GetPos()+0x02); }
            set { UndoRedo.Exec(new BindU8(this, 0x02, value)); }
        }

        [Category("01 Misc Item")]
        [DisplayName("Quantity")]
        [Description("Quantity")]
        public byte Quantity {
            get { return RamDisk.GetU8(GetPos()+0x03); }
            set { UndoRedo.Exec(new BindU8(this, 0x03, value)); }
        }
    }
}
