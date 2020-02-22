using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GodHands {
    public class TreasureArmour : InMemory {
        public TreasureArmour(BaseClass parent, string url, int pos, Record rec):
        base(parent, url, pos, rec) {
        }

        public override int GetLen() {
            return 0x2C;
        }

        [Category("01 Equipment")]
        [DisplayName("Exists")]
        [Description("Exists=3 if exists")]
        public uint Exists {
            get { return RamDisk.GetU32(GetPos()+0x00); }
            set { UndoRedo.Exec(new BindU32(this, 0x00, value)); }
        }

        [ReadOnly(true)]
        [Category("01 Equipment")]
        [DisplayName("Item Name Raw")]
        [Description("Name of the item")]
        private byte ItemNameRaw {
            get { return RamDisk.GetU8(GetPos()+0x04); }
            set { UndoRedo.Exec(new BindU8(this, 0x04, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Item Name")]
        [Description("Name of the item")]
        [DefaultValue("")]
        [TypeConverter(typeof(ItemNamesListDropDown))]
        public string ItemName {
            get { return Model.itemnames.GetName(ItemNameRaw); }
            set { ItemNameRaw = (byte)Model.itemnames.GetIndexByName(value); }
        }

        [ReadOnly(true)]
        [Category("01 Equipment")]
        [DisplayName("Items List Raw")]
        [Description("Depends on item type")]
        private byte ItemsListRaw {
            get { return RamDisk.GetU8(GetPos()+0x05); }
            set { UndoRedo.Exec(new BindU8(this, 0x05, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Items List")]
        [Description("Depends on item type")]
        [DefaultValue("")]
        [TypeConverter(typeof(ItemNameArmourDropDown))]
        public string ItemsList {
            get { return Model.armour_names.GetName(ItemsListRaw); }
            set { ItemsListRaw = (byte)Model.armour_names.GetIndexByName(value); }
        }

        [Category("01 Equipment")]
        [DisplayName("Unknown 1")]
        [Description("Unknown")]
        public byte Unknown1 {
            get { return RamDisk.GetU8(GetPos()+0x06); }
            set { UndoRedo.Exec(new BindU8(this, 0x06, value)); }
        }

        [ReadOnly(true)]
        [Category("01 Equipment")]
        [DisplayName("Category Raw")]
        [Description("Armour category")]
        private byte CategoryRaw {
            get { return RamDisk.GetU8(GetPos()+0x07); }
            set { UndoRedo.Exec(new BindU8(this, 0x07, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Category")]
        [Description("Armour category")]
        [DefaultValue("")]
        [TypeConverter(typeof(CategoryArmoursDropDown))]
        public string Category {
            get { return Model.category_armours.GetName(CategoryRaw); }
            set { CategoryRaw = (byte)Model.category_armours.GetIndexByName(value); }
        }

        [Category("01 Equipment")]
        [DisplayName("Max DP")]
        [Description("Maximum damange points")]
        public double MaxDP {
            get { return RamDisk.GetS16(GetPos()+0x08)/100.0; }
            set { UndoRedo.Exec(new BindS16(this, 0x08, (short)(value*100))); }
        }

        [Category("01 Equipment")]
        [DisplayName("Max PP")]
        [Description("Maximum phantom points")]
        public double MaxPP {
            get { return RamDisk.GetS16(GetPos()+0x0A); }
            set { UndoRedo.Exec(new BindS16(this, 0x0A, (short)(value))); }
        }

        [Category("01 Equipment")]
        [DisplayName("Cur DP")]
        [Description("Damange points")]
        public double CurDP {
            get { return RamDisk.GetS16(GetPos()+0x0C)/100.0; }
            set { UndoRedo.Exec(new BindS16(this, 0x0C, (short)(value*100))); }
        }

        [Category("01 Equipment")]
        [DisplayName("Cur PP")]
        [Description("Phantom points")]
        public double CurPP {
            get { return RamDisk.GetS16(GetPos()+0x0E); }
            set { UndoRedo.Exec(new BindS16(this, 0x0E, (short)(value))); }
        }

        [Category("01 Equipment")]
        [DisplayName("Unknown 2")]
        [Description("Unknown")]
        public sbyte Unknown2 {
            get { return RamDisk.GetS8(GetPos()+0x10); }
            set { UndoRedo.Exec(new BindS8(this, 0x10, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("STR")]
        [Description("Strength bonus")]
        public sbyte STR {
            get { return RamDisk.GetS8(GetPos()+0x11); }
            set { UndoRedo.Exec(new BindS8(this, 0x11, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("INT")]
        [Description("Intelligence bonus")]
        public sbyte INT {
            get { return RamDisk.GetS8(GetPos()+0x12); }
            set { UndoRedo.Exec(new BindS8(this, 0x12, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("AGL")]
        [Description("Agility bonus")]
        public sbyte AGL {
            get { return RamDisk.GetS8(GetPos()+0x13); }
            set { UndoRedo.Exec(new BindS8(this, 0x13, value)); }
        }

        [Category("02 Types")]
        [DisplayName("Padding")]
        [Description("Unused")]
        public sbyte TypePadding {
            get { return RamDisk.GetS8(GetPos()+0x14); }
            set { UndoRedo.Exec(new BindS8(this, 0x14, value)); }
        }

        [Category("02 Types")]
        [DisplayName("Blunt")]
        [Description("Resistence to blunt attacks")]
        public sbyte TypeBlunt {
            get { return RamDisk.GetS8(GetPos()+0x15); }
            set { UndoRedo.Exec(new BindS8(this, 0x15, value)); }
        }

        [Category("02 Types")]
        [DisplayName("Edged")]
        [Description("Resistence to edged attacks")]
        public sbyte TypeEdged {
            get { return RamDisk.GetS8(GetPos()+0x16); }
            set { UndoRedo.Exec(new BindS8(this, 0x16, value)); }
        }

        [Category("02 Types")]
        [DisplayName("Piercing")]
        [Description("Resistence to piercing attacks")]
        public sbyte TypePiercing {
            get { return RamDisk.GetS8(GetPos()+0x17); }
            set { UndoRedo.Exec(new BindS8(this, 0x17, value)); }
        }

        [Category("03 Classes")]
        [DisplayName("Human")]
        [Description("Human class")]
        public sbyte ClassHuman {
            get { return RamDisk.GetS8(GetPos()+0x18); }
            set { UndoRedo.Exec(new BindS8(this, 0x18, value)); }
        }

        [Category("03 Classes")]
        [DisplayName("Beast")]
        [Description("Beast class")]
        public sbyte ClassBeast {
            get { return RamDisk.GetS8(GetPos()+0x19); }
            set { UndoRedo.Exec(new BindS8(this, 0x19, value)); }
        }

        [Category("03 Classes")]
        [DisplayName("Undead")]
        [Description("Undead class")]
        public sbyte ClassUndead {
            get { return RamDisk.GetS8(GetPos()+0x1A); }
            set { UndoRedo.Exec(new BindS8(this, 0x1A, value)); }
        }

        [Category("03 Classes")]
        [DisplayName("Phantom")]
        [Description("Phantom class")]
        public sbyte ClassPhantom {
            get { return RamDisk.GetS8(GetPos()+0x1B); }
            set { UndoRedo.Exec(new BindS8(this, 0x1B, value)); }
        }

        [Category("03 Classes")]
        [DisplayName("Dragon")]
        [Description("Dragon class")]
        public sbyte ClassDragon {
            get { return RamDisk.GetS8(GetPos()+0x1C); }
            set { UndoRedo.Exec(new BindS8(this, 0x1C, value)); }
        }

        [Category("03 Classes")]
        [DisplayName("Evil")]
        [Description("Evil class")]
        public sbyte ClassEvil {
            get { return RamDisk.GetS8(GetPos()+0x1D); }
            set { UndoRedo.Exec(new BindS8(this, 0x1D, value)); }
        }

        [Category("03 Classes")]
        [DisplayName("Padding 1")]
        [Description("Unused")]
        public sbyte ClassPadding1 {
            get { return RamDisk.GetS8(GetPos()+0x1E); }
            set { UndoRedo.Exec(new BindS8(this, 0x1E, value)); }
        }

        [Category("03 Classes")]
        [DisplayName("Padding 2")]
        [Description("Unused")]
        public sbyte ClassPadding2 {
            get { return RamDisk.GetS8(GetPos()+0x1F); }
            set { UndoRedo.Exec(new BindS8(this, 0x1F, value)); }
        }

        [Category("04 Affinities")]
        [DisplayName("Physical")]
        [Description("Physical affinity")]
        public sbyte AffinityPhysical {
            get { return RamDisk.GetS8(GetPos()+0x20); }
            set { UndoRedo.Exec(new BindS8(this, 0x20, value)); }
        }

        [Category("04 Affinities")]
        [DisplayName("Air")]
        [Description("Air elemental affinity")]
        public sbyte AffinityAir {
            get { return RamDisk.GetS8(GetPos()+0x21); }
            set { UndoRedo.Exec(new BindS8(this, 0x21, value)); }
        }

        [Category("04 Affinities")]
        [DisplayName("Fire")]
        [Description("fire elemental affinity")]
        public sbyte AffinityFire {
            get { return RamDisk.GetS8(GetPos()+0x22); }
            set { UndoRedo.Exec(new BindS8(this, 0x22, value)); }
        }

        [Category("04 Affinities")]
        [DisplayName("Earth")]
        [Description("Earth elemental affinity")]
        public sbyte AffinityEarth {
            get { return RamDisk.GetS8(GetPos()+0x23); }
            set { UndoRedo.Exec(new BindS8(this, 0x23, value)); }
        }

        [Category("04 Affinities")]
        [DisplayName("Water")]
        [Description("Water elemental affinity")]
        public sbyte AffinityWater {
            get { return RamDisk.GetS8(GetPos()+0x24); }
            set { UndoRedo.Exec(new BindS8(this, 0x24, value)); }
        }

        [Category("04 Affinities")]
        [DisplayName("Light")]
        [Description("Light affinity")]
        public sbyte AffinityLight {
            get { return RamDisk.GetS8(GetPos()+0x25); }
            set { UndoRedo.Exec(new BindS8(this, 0x25, value)); }
        }

        [Category("04 Affinities")]
        [DisplayName("Dark")]
        [Description("Dark affinity")]
        public sbyte AffinityDark {
            get { return RamDisk.GetS8(GetPos()+0x26); }
            set { UndoRedo.Exec(new BindS8(this, 0x26, value)); }
        }

        [Category("04 Affinities")]
        [DisplayName("Padding")]
        [Description("Unused")]
        public sbyte AffinityPadding {
            get { return RamDisk.GetS8(GetPos()+0x27); }
            set { UndoRedo.Exec(new BindS8(this, 0x27, value)); }
        }

        [ReadOnly(true)]
        [Category("01 Equipment")]
        [DisplayName("Material Raw")]
        [Description("Material equipment is made of")]
        private ushort MaterialRaw {
            get { return RamDisk.GetU16(GetPos()+0x28); }
            set { UndoRedo.Exec(new BindU16(this, 0x28, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Material")]
        [Description("Material equipment is made of")]
        [DefaultValue("")]
        [TypeConverter(typeof(ItemNameMaterialDropDown))]
        public string Material {
            get { return Model.materials.GetName(MaterialRaw); }
            set { MaterialRaw = (ushort)Model.materials.GetIndexByName(value); }
        }

        [Category("01 Equipment")]
        [DisplayName("Unknown 3")]
        [Description("Unknown")]
        public byte Unknown3 {
            get { return RamDisk.GetU8(GetPos()+0x2A); }
            set { UndoRedo.Exec(new BindU8(this, 0x2A, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Unknown 4")]
        [Description("Unknown")]
        public byte Unknown4 {
            get { return RamDisk.GetU8(GetPos()+0x2B); }
            set { UndoRedo.Exec(new BindU8(this, 0x2B, value)); }
        }
    }
}
