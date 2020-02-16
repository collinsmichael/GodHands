using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GodHands {
    public class TreasureGem : InMemory {
        private bool equipped;
        public TreasureGem(BaseClass parent, string url, int pos, Record rec, bool equipped):
        base(parent, url, pos, rec) {
            this.equipped = equipped;
        }

        public override int GetLen() {
            return 0x1C;
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
        [DisplayName("Item Name Raw")]
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
        public string ItemName {
            get { return Model.itemnames.GetName(ItemNameRaw); }
            set { ItemNameRaw = (short)Model.itemnames.GetIndexByName(value); }
        }

        [ReadOnly(true)]
        [Category("01 Equipment")]
        [DisplayName("Item List Raw")]
        [Description("Depends on item type")]
        private byte ItemListRaw {
            get { return RamDisk.GetU8(GetPos()+0x02); }
            set { UndoRedo.Exec(new BindU8(this, 0x02, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Item List")]
        [Description("Depends on item type")]
        [DefaultValue("")]
        [TypeConverter(typeof(ItemNameGemDropDown))]
        public string ItemsList {
            get { return Model.gem_names.GetName(ItemListRaw); }
            set { ItemListRaw = (byte)Model.gem_names.GetIndexByName(value); }
        }

        [Category("01 Equipment")]
        [DisplayName("Unknown 1")]
        [Description("Unknown")]
        public byte Unknown1 {
            get { return RamDisk.GetU8(GetPos()+0x03); }
            set { UndoRedo.Exec(new BindU8(this, 0x03, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Unknown 2")]
        [Description("Unknown")]
        public byte Unknown2 {
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

        [Category("02 Classes")]
        [DisplayName("Human")]
        [Description("Human class")]
        public sbyte ClassHuman {
            get { return RamDisk.GetS8(GetPos()+0x08); }
            set { UndoRedo.Exec(new BindS8(this, 0x08, value)); }
        }

        [Category("02 Classes")]
        [DisplayName("Beast")]
        [Description("Beast class")]
        public sbyte ClassBeast {
            get { return RamDisk.GetS8(GetPos()+0x09); }
            set { UndoRedo.Exec(new BindS8(this, 0x09, value)); }
        }

        [Category("02 Classes")]
        [DisplayName("Undead")]
        [Description("Undead class")]
        public sbyte ClassUndead {
            get { return RamDisk.GetS8(GetPos()+0x0A); }
            set { UndoRedo.Exec(new BindS8(this, 0x0A, value)); }
        }

        [Category("02 Classes")]
        [DisplayName("Phantom")]
        [Description("Phantom class")]
        public sbyte ClassPhantom {
            get { return RamDisk.GetS8(GetPos()+0x0B); }
            set { UndoRedo.Exec(new BindS8(this, 0x0B, value)); }
        }

        [Category("02 Classes")]
        [DisplayName("Dragon")]
        [Description("Dragon class")]
        public sbyte ClassDragon {
            get { return RamDisk.GetS8(GetPos()+0x0C); }
            set { UndoRedo.Exec(new BindS8(this, 0x0C, value)); }
        }

        [Category("02 Classes")]
        [DisplayName("Evil")]
        [Description("Evil class")]
        public sbyte ClassEvil {
            get { return RamDisk.GetS8(GetPos()+0x0D); }
            set { UndoRedo.Exec(new BindS8(this, 0x0D, value)); }
        }

        [Category("02 Classes")]
        [DisplayName("Padding 1")]
        [Description("Unused")]
        public sbyte ClassPadding1 {
            get { return RamDisk.GetS8(GetPos()+0x0E); }
            set { UndoRedo.Exec(new BindS8(this, 0x0E, value)); }
        }

        [Category("02 Classes")]
        [DisplayName("Padding 2")]
        [Description("Unused")]
        public sbyte ClassPadding2 {
            get { return RamDisk.GetS8(GetPos()+0x0F); }
            set { UndoRedo.Exec(new BindS8(this, 0x0F, value)); }
        }

        [Category("03 Affinities")]
        [DisplayName("Earth")]
        [Description("Earth elemental affinity")]
        public sbyte AffinityEarth {
            get { return RamDisk.GetS8(GetPos()+0x10); }
            set { UndoRedo.Exec(new BindS8(this, 0x10, value)); }
        }

        [Category("03 Affinities")]
        [DisplayName("Air")]
        [Description("Air elemental affinity")]
        public sbyte AffinityAir {
            get { return RamDisk.GetS8(GetPos()+0x11); }
            set { UndoRedo.Exec(new BindS8(this, 0x11, value)); }
        }

        [Category("03 Affinities")]
        [DisplayName("Fire")]
        [Description("fire elemental affinity")]
        public sbyte AffinityFire {
            get { return RamDisk.GetS8(GetPos()+0x12); }
            set { UndoRedo.Exec(new BindS8(this, 0x12, value)); }
        }

        [Category("03 Affinities")]
        [DisplayName("Water")]
        [Description("Water elemental affinity")]
        public sbyte AffinityWater {
            get { return RamDisk.GetS8(GetPos()+0x13); }
            set { UndoRedo.Exec(new BindS8(this, 0x13, value)); }
        }

        [Category("03 Affinities")]
        [DisplayName("Physical")]
        [Description("Physical affinity")]
        public sbyte AffinityPhysical {
            get { return RamDisk.GetS8(GetPos()+0x14); }
            set { UndoRedo.Exec(new BindS8(this, 0x14, value)); }
        }

        [Category("03 Affinities")]
        [DisplayName("Light")]
        [Description("Light affinity")]
        public sbyte AffinityLight {
            get { return RamDisk.GetS8(GetPos()+0x15); }
            set { UndoRedo.Exec(new BindS8(this, 0x15, value)); }
        }

        [Category("03 Affinities")]
        [DisplayName("Dark")]
        [Description("Dark affinity")]
        public sbyte AffinityDark {
            get { return RamDisk.GetS8(GetPos()+0x16); }
            set { UndoRedo.Exec(new BindS8(this, 0x16, value)); }
        }

        [Category("03 Affinities")]
        [DisplayName("Padding")]
        [Description("Unused")]
        public sbyte AffinityPadding {
            get { return RamDisk.GetS8(GetPos()+0x17); }
            set { UndoRedo.Exec(new BindS8(this, 0x17, value)); }
        }

        [Category("04 Unused")]
        [DisplayName("Unused 3")]
        [Description("Unused")]
        public sbyte Unused3 {
            get { return RamDisk.GetS8(GetPos()+0x18); }
            set { UndoRedo.Exec(new BindS8(this, 0x18, value)); }
        }

        [Category("04 Unused")]
        [DisplayName("Unused 4")]
        [Description("Unused")]
        public sbyte Unused4 {
            get { return RamDisk.GetS8(GetPos()+0x19); }
            set { UndoRedo.Exec(new BindS8(this, 0x19, value)); }
        }

        [Category("04 Unused")]
        [DisplayName("Unused 5")]
        [Description("Unused")]
        public sbyte Unused5 {
            get { return RamDisk.GetS8(GetPos()+0x1A); }
            set { UndoRedo.Exec(new BindS8(this, 0x1A, value)); }
        }

        [Category("04 Unused")]
        [DisplayName("Unused 6")]
        [Description("Unused")]
        public sbyte Unused6 {
            get { return RamDisk.GetS8(GetPos()+0x1B); }
            set { UndoRedo.Exec(new BindS8(this, 0x1B, value)); }
        }
    }
}
