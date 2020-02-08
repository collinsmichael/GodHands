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
            return 0x04;
        }

        public override string GetText() {
            string text = ItemName;
            if (Exists == 0) {
                text = "(Deleted)" + text;
            }
            return text;
        }

        [ReadOnly(true)]
        [Category("01 Misc Item")]
        [DisplayName("Item Name Raw")]
        [Description("Name of the item")]
        private short ItemNameRaw {
            get { return RamDisk.GetS16(GetPos()+0x00); }
            set { UndoRedo.Exec(new BindS16(this, 0x00, value)); }
        }

        [Category("01 Misc Item")]
        [DisplayName("Item Name")]
        [Description("Name of the item")]
        [DefaultValue("")]
        [TypeConverter(typeof(ItemNamesListDropDown))]
        public string ItemName {
            get { return Model.itemnames.GetName(ItemNameRaw); }
            set { ItemNameRaw = (short)Model.itemnames.GetIndexByName(value); }
        }

        [Category("01 Misc Item")]
        [DisplayName("Exists")]
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
