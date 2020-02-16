using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GodHands {
    public class TreasureBlade : InMemory {
        private bool equipped;
        public TreasureBlade(BaseClass parent, string url, int pos, Record rec, bool equipped):
        base(parent, url, pos, rec) {
            this.equipped = equipped;
        }

        public override int GetLen() {
             return 0x2C;
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
        private byte ItemNameRaw {
            get { return RamDisk.GetU8(GetPos()+0x00); }
            set { UndoRedo.Exec(new BindU8(this, 0x00, value)); }
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
            get { return RamDisk.GetU8(GetPos()+0x01); }
            set { UndoRedo.Exec(new BindU8(this, 0x01, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Items List")]
        [Description("Depends on item type")]
        [DefaultValue("")]
        [TypeConverter(typeof(ItemNameBladeDropDown))]
        public string ItemsList {
            get {
                byte index = RamDisk.GetU8(GetPos()+0x01);
                return Model.blade_names.GetName(index);
            }
            set {
                byte index = (byte)Model.blade_names.GetIndexByName(value);
                UndoRedo.Exec(new BindU8(this, 0x01, index));
            }
        }

        [Category("01 Equipment")]
        [DisplayName("Wep File")]
        [Description("3D model file")]
        public byte WepFile {
            get { return RamDisk.GetU8(GetPos()+0x02); }
            set { UndoRedo.Exec(new BindU8(this, 0x02, value)); }
        }

        [ReadOnly(true)]
        [Category("01 Equipment")]
        [DisplayName("Item Category Raw")]
        [Description("Blade category")]
        private byte ItemCategoryRaw {
            get { return RamDisk.GetU8(GetPos()+0x03); }
            set { UndoRedo.Exec(new BindU8(this, 0x03, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Item Category")]
        [Description("Blade category")]
        [DefaultValue("")]
        [TypeConverter(typeof(CategoryBladesDropDown))]
        public string ItemCategory {
            get {
                byte index = RamDisk.GetU8(GetPos()+0x03);
                return Model.category_blades.GetName(index);
            }
            set {
                byte index = (byte)Model.category_blades.GetIndexByName(value);
                UndoRedo.Exec(new BindU8(this, 0x03, index));
            }
        }

        [Category("01 Equipment")]
        [DisplayName("Max DP")]
        [Description("Maximum damange points")]
        public double MaxDP {
            get { return RamDisk.GetS16(GetPos()+0x04)/100.0; }
            set { UndoRedo.Exec(new BindS16(this, 0x04, (short)(value*100))); }
        }

        [Category("01 Equipment")]
        [DisplayName("Max PP")]
        [Description("Maximum phantom points")]
        public double MaxPP {
            get { return RamDisk.GetS16(GetPos()+0x06); }
            set { UndoRedo.Exec(new BindS16(this, 0x06, (short)(value))); }
        }

        [Category("01 Equipment")]
        [DisplayName("Cur DP")]
        [Description("Damange points")]
        public double CurDP {
            get { return RamDisk.GetS16(GetPos()+0x08)/100.0; }
            set { UndoRedo.Exec(new BindS16(this, 0x08, (short)(value*100))); }
        }

        [Category("01 Equipment")]
        [DisplayName("Cur PP")]
        [Description("Phantom points")]
        public double CurPP {
            get { return RamDisk.GetS16(GetPos()+0x0A); }
            set { UndoRedo.Exec(new BindS16(this, 0x0A, (short)(value))); }
        }

        [Category("01 Equipment")]
        [DisplayName("STR")]
        [Description("Strength bonus")]
        public sbyte STR {
            get { return RamDisk.GetS8(GetPos()+0x0C); }
            set { UndoRedo.Exec(new BindS8(this, 0x0C, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("INT")]
        [Description("Intelligence bonus")]
        public sbyte INT {
            get { return RamDisk.GetS8(GetPos()+0x0D); }
            set { UndoRedo.Exec(new BindS8(this, 0x0D, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("AGL")]
        [Description("Agility bonus")]
        public sbyte AGL {
            get { return RamDisk.GetS8(GetPos()+0x0E); }
            set { UndoRedo.Exec(new BindS8(this, 0x0E, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Damage Cost")]
        [Description("Stat cost value")]
        public byte DamageCost {
            get { return RamDisk.GetU8(GetPos()+0x0F); }
            set { UndoRedo.Exec(new BindU8(this, 0x0F, value)); }
        }

        [ReadOnly(true)]
        [Category("01 Equipment")]
        [DisplayName("Damage Raw")]
        [Description("Stat affected (1=MP 2=RISK 3=HP 4=PP 5=nothing)\n"
                    +"Type can be (1=BLUNT 2=EDGED 3=PIERCING)")]
        private byte DamageRaw {
            get { return RamDisk.GetU8(GetPos()+0x10); }
            set { UndoRedo.Exec(new BindU8(this, 0x10, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Damage Stat")]
        [Description("Stat consumed (1=MP 2=RISK 3=HP 4=PP 5=nothing)")]
        [DefaultValue("")]
        [TypeConverter(typeof(DamageStatsDropDown))]
        public string DamageStat {
            get { return Model.damage_stats.GetName(DamageRaw/4); }
            set {
                int index = Model.damage_stats.GetIndexByName(value) * 4;
                DamageRaw = (byte)((DamageRaw % 4) | index);
            }
        }

        [Category("01 Equipment")]
        [DisplayName("Damage Type")]
        [Description("Type can be (1=BLUNT 2=EDGED 3=PIERCING)")]
        [DefaultValue("")]
        [TypeConverter(typeof(DamageTypesDropDown))]
        public string DamageType {
            get { return Model.damage_types.GetName(DamageRaw % 4); }
            set {
                int index = Model.damage_types.GetIndexByName(value) % 4;
                DamageRaw = (byte)((DamageRaw & ~3) | index);
            }
        }

        [Category("01 Equipment")]
        [DisplayName("Unknown 1")]
        [Description("Unknown")]
        public byte Unknown1 {
            get { return RamDisk.GetU8(GetPos()+0x11); }
            set { UndoRedo.Exec(new BindU8(this, 0x11, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Unknown 2")]
        [Description("Unknown")]
        public byte Unknown2 {
            get { return RamDisk.GetU8(GetPos()+0x12); }
            set { UndoRedo.Exec(new BindU8(this, 0x12, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Unknown 3")]
        [Description("Unknown")]
        public byte Unknown3 {
            get { return RamDisk.GetU8(GetPos()+0x13); }
            set { UndoRedo.Exec(new BindU8(this, 0x13, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("RangeX")]
        [Description("Range horizontal")]
        public byte TargetRangeX {
            get { return RamDisk.GetU8(GetPos()+0x14); }
            set { UndoRedo.Exec(new BindU8(this, 0x14, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("RangeY")]
        [Description("Range vertical")]
        public byte TargetRangeY {
            get { return RamDisk.GetU8(GetPos()+0x15); }
            set { UndoRedo.Exec(new BindU8(this, 0x15, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("RangeZ")]
        [Description("Range depth")]
        public byte TargetRangeZ {
            get { return RamDisk.GetU8(GetPos()+0x16); }
            set { UndoRedo.Exec(new BindU8(this, 0x16, value)); }
        }

        [ReadOnly(true)]
        [Category("01 Equipment")]
        [DisplayName("Target Sphere Raw")]
        [Description("Target sphere shape")]
        private byte TargetSphereRaw {
            get { return RamDisk.GetU8(GetPos()+0x17); }
            set { UndoRedo.Exec(new BindU8(this, 0x17, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Target Sphere")]
        [Description("Target sphere shape")]
        [DefaultValue("")]
        [TypeConverter(typeof(TargetSphereDropDown))]
        public string TargetSphere {
            get {
                int index = TargetSphereRaw % 8;
                return Model.targets.GetName(index);
            }
            set {
                byte index = (byte)(Model.targets.GetIndexByName(value) % 8);
                TargetSphereRaw = (byte)((TargetSphereRaw & ~7) | index);
            }
        }

        [Category("01 Equipment")]
        [DisplayName("Target Angle")]
        [Description("Angle of target sphere")]
        public byte TargetAngle {
            get { return (byte)(TargetSphereRaw / 8); }
            set {
                int val = TargetSphereRaw % 8;
                TargetSphereRaw = (byte)(val | (4*(value % 32)));
            }
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
        [DisplayName("Earth")]
        [Description("Earth elemental affinity")]
        public sbyte AffinityEarth {
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
        [DisplayName("Water")]
        [Description("Water elemental affinity")]
        public sbyte AffinityWater {
            get { return RamDisk.GetS8(GetPos()+0x23); }
            set { UndoRedo.Exec(new BindS8(this, 0x23, value)); }
        }

        [Category("04 Affinities")]
        [DisplayName("Physical")]
        [Description("Physical affinity")]
        public sbyte AffinityPhysical {
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
            set { MaterialRaw = (byte)Model.materials.GetIndexByName(value); }
        }

        [Category("01 Equipment")]
        [DisplayName("Unknown 4")]
        [Description("Unknown")]
        public byte Unknown_4 {
            get { return RamDisk.GetU8(GetPos()+0x2A); }
            set { UndoRedo.Exec(new BindU8(this, 0x2A, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Unknown 5")]
        [Description("Unknown")]
        public byte Unknown_5 {
            get { return RamDisk.GetU8(GetPos()+0x2B); }
            set { UndoRedo.Exec(new BindU8(this, 0x2B, value)); }
        }
    }
}
