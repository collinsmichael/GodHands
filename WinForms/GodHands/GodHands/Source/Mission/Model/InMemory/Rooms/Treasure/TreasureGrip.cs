using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GodHands {
    public class TreasureGrip : InMemory {
        private bool equipped;
        public TreasureGrip(BaseClass parent, string url, int pos, Record rec, bool equipped):
        base(parent, url, pos, rec) {
            this.equipped = equipped;
        }

        public override int GetLen() {
            return (equipped) ? 0x10 : 0x14;
        }

        [Category("01 Equipment")]
        [DisplayName("Exists")]
        [Description("Exists=3 if exists")]
        public int Exists {
            get {
                if (!equipped) return RamDisk.GetS32(GetPos()-0x04);
                return (ItemNameRaw != 0) ? 3 : 0;
            }
            set {
                if (equipped) {
                    UndoRedo.Exec(new BindS32(this, 0x00, value));
                } else {
                    Publisher.Publish(this);
                }
            }
        }

        [ReadOnly(true)]
        [Category("01 Equipment")]
        [DisplayName("Item Names List Raw")]
        [Description("Name of the item")]
        private short ItemNameRaw {
            get { return RamDisk.GetS16(GetPos()+0x00); }
            set { UndoRedo.Exec(new BindS16(this, 0x00, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Item Name")]
        [Description("Name of the item")]
        [DefaultValue("")]
        [TypeConverter(typeof(ItemNamesListDropDown))]
        public string ItemNames {
            get { return Model.itemnames.GetName(ItemNameRaw); }
            set { ItemNameRaw = (short)Model.itemnames.GetIndexByName(value); }
        }

        [ReadOnly(true)]
        [Category("01 Equipment")]
        [DisplayName("Items List Raw")]
        [Description("Depends on item type")]
        private byte ItemsListRaw {
            get { return RamDisk.GetU8(GetPos()+0x02); }
            set { UndoRedo.Exec(new BindU8(this, 0x02, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Items List")]
        [Description("Depends on item type")]
        [DefaultValue("")]
        [TypeConverter(typeof(ItemNameGripDropDown))]
        public string ItemsList {
            get { return Model.grip_names.GetName(ItemsListRaw); }
            set { ItemsListRaw = (byte)Model.grip_names.GetIndexByName(value); }
        }

        [ReadOnly(true)]
        [Category("01 Equipment")]
        [DisplayName("Category Raw")]
        [Description("Grip category")]
        private byte CategoryRaw {
            get { return RamDisk.GetU8(GetPos()+0x03); }
            set { UndoRedo.Exec(new BindU8(this, 0x03, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Category")]
        [Description("Grip category")]
        [DefaultValue("")]
        [TypeConverter(typeof(CategoryGripsDropDown))]
        public string Category {
            get { return Model.category_grips.GetName(CategoryRaw); }
            set { CategoryRaw = (byte)Model.category_grips.GetIndexByName(value); }
        }

        [Category("01 Equipment")]
        [DisplayName("Num Gems")]
        [Description("Number of gem slots")]
        public byte NumGems {
            get { return RamDisk.GetU8(GetPos()+0x04); }
            set { UndoRedo.Exec(new BindU8(this, 0x04, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("STR")]
        [Description("Strength bonus")]
        public sbyte STR {
            get { return RamDisk.GetS8(GetPos()+0x05); }
            set { UndoRedo.Exec(new BindS8(this, 0x05, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("INT")]
        [Description("Intelligence bonus")]
        public sbyte INT {
            get { return RamDisk.GetS8(GetPos()+0x06); }
            set { UndoRedo.Exec(new BindS8(this, 0x06, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("AGL")]
        [Description("Agility bonus")]
        public sbyte AGL {
            get { return RamDisk.GetS8(GetPos()+0x07); }
            set { UndoRedo.Exec(new BindS8(this, 0x07, value)); }
        }

        [Category("02 Types")]
        [DisplayName("Padding")]
        [Description("Unused")]
        public sbyte TypePadding {
            get { return RamDisk.GetS8(GetPos()+0x08); }
            set { UndoRedo.Exec(new BindS8(this, 0x08, value)); }
        }

        [Category("02 Types")]
        [DisplayName("Blunt")]
        [Description("Resistence to blunt attacks")]
        public sbyte TypeBlunt {
            get { return RamDisk.GetS8(GetPos()+0x09); }
            set { UndoRedo.Exec(new BindS8(this, 0x09, value)); }
        }

        [Category("02 Types")]
        [DisplayName("Edged")]
        [Description("Resistence to edged attacks")]
        public sbyte TypeEdged {
            get { return RamDisk.GetS8(GetPos()+0x0A); }
            set { UndoRedo.Exec(new BindS8(this, 0x0A, value)); }
        }

        [Category("02 Types")]
        [DisplayName("Piercing")]
        [Description("Resistence to piercing attacks")]
        public sbyte TypePiercing {
            get { return RamDisk.GetS8(GetPos()+0x0B); }
            set { UndoRedo.Exec(new BindS8(this, 0x0B, value)); }
        }

        [Category("03 Unknown")]
        [DisplayName("Unknown 1")]
        [Description("Unknown")]
        public byte Unknown1 {
            get { return RamDisk.GetU8(GetPos()+0x0C); }
            set { UndoRedo.Exec(new BindU8(this, 0x0C, value)); }
        }

        [Category("03 Unknown")]
        [DisplayName("Unknown 2")]
        [Description("Unknown")]
        public byte Unknown2 {
            get { return RamDisk.GetU8(GetPos()+0x0D); }
            set { UndoRedo.Exec(new BindU8(this, 0x0D, value)); }
        }

        [Category("03 Unknown")]
        [DisplayName("Unknown 3")]
        [Description("Unknown")]
        public byte Unknown3 {
            get { return RamDisk.GetU8(GetPos()+0x0E); }
            set { UndoRedo.Exec(new BindU8(this, 0x0E, value)); }
        }

        [Category("03 Unknown")]
        [DisplayName("Unknown 4")]
        [Description("Unknown")]
        public byte Unknown4 {
            get { return RamDisk.GetU8(GetPos()+0x0F); }
            set { UndoRedo.Exec(new BindU8(this, 0x0F, value)); }
        }

        [Category("03 Unknown")]
        [DisplayName("Unknown 5")]
        [Description("Unknown")]
        public uint Unknown5 {
            get { return (equipped) ? 0 : RamDisk.GetU32(GetPos()+0x10); }
            set {
                if (equipped) {
                    Publisher.Publish(this);
                } else {
                    UndoRedo.Exec(new BindU32(this, 0x10, value));
                }
            }
        }
    }
}
