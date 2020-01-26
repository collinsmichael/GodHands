using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GodHands {
    public class ActorGem : InMemory {
        public ActorGem(string url, int pos, DirRec rec):
        base(url, pos, rec) {
        }

        [ReadOnly(true)]
        [Category("01 Equipment")]
        [DisplayName("Item Names List Raw")]
        [Description("Name of the item")]
        public short ItemNamesListRaw {
            get { return RamDisk.GetS16(GetPos()+0x00); }
            set { UndoRedo.Exec(new BindS16(this, 0x00, value)); }
        }

        [Category("01 Equipment")]
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
        [Category("01 Equipment")]
        [DisplayName("Items List Raw")]
        [Description("Depends on item type")]
        public byte ItemsListRaw {
            get { return RamDisk.GetU8(GetPos()+0x02); }
            set { UndoRedo.Exec(new BindU8(this, 0x02, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Items List")]
        [Description("Depends on item type")]
        [DefaultValue("")]
        [TypeConverter(typeof(ItemNameGemDropDown))]
        public string ItemsList {
            get {
                byte index = RamDisk.GetU8(GetPos()+0x02);
                return Model.gem_names.GetName(index);
            }
            set {
                byte index = (byte)Model.gem_names.GetIndexByName(value);
                UndoRedo.Exec(new BindU8(this, 0x02, index));
            }
        }

        [Category("01 Equipment")]
        [DisplayName("Wep File")]
        [Description("3D model file")]
        public byte WepFile {
            get { return RamDisk.GetU8(GetPos()+0x03); }
            set { UndoRedo.Exec(new BindU8(this, 0x03, value)); }
        }

        [ReadOnly(true)]
        [Category("01 Equipment")]
        [DisplayName("Item Category Raw")]
        [Description("Gem category")]
        public byte ItemCategoryRaw {
            get { return RamDisk.GetU8(GetPos()+0x04); }
            set { UndoRedo.Exec(new BindU8(this, 0x04, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Item Category")]
        [Description("Gem category")]
        [DefaultValue("")]
        [TypeConverter(typeof(CategoryArmoursDropDown))]
        public string ItemCategory {
            get {
                byte index = RamDisk.GetU8(GetPos()+0x04);
                return Model.category_armours.GetName(index);
            }
            set {
                byte index = (byte)Model.category_armours.GetIndexByName(value);
                UndoRedo.Exec(new BindU8(this, 0x04, index));
            }
        }

        [Category("01 Equipment")]
        [DisplayName("STR")]
        [Description("Strength bonus")]
        public byte STR {
            get { return RamDisk.GetU8(GetPos()+0x05); }
            set { UndoRedo.Exec(new BindU8(this, 0x05, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("INT")]
        [Description("Intelligence bonus")]
        public byte INT {
            get { return RamDisk.GetU8(GetPos()+0x06); }
            set { UndoRedo.Exec(new BindU8(this, 0x06, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("AGL")]
        [Description("Agility bonus")]
        public byte AGL {
            get { return RamDisk.GetU8(GetPos()+0x07); }
            set { UndoRedo.Exec(new BindU8(this, 0x07, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("CurDP")]
        [Description("Damange points")]
        public double CurDP {
            get { return RamDisk.GetS16(GetPos()+0x08)/100.0; }
            set { UndoRedo.Exec(new BindS16(this, 0x08, (short)(value*100))); }
        }

        [Category("01 Equipment")]
        [DisplayName("MaxDP")]
        [Description("Maximum amange points")]
        public double MaxDP {
            get { return RamDisk.GetS16(GetPos()+0x0A)/100.0; }
            set { UndoRedo.Exec(new BindS16(this, 0x0A, (short)(value*100))); }
        }

        [Category("01 Equipment")]
        [DisplayName("CurPP")]
        [Description("Phantom points")]
        public double CurPP {
            get { return RamDisk.GetS16(GetPos()+0x0C)/100.0; }
            set { UndoRedo.Exec(new BindS16(this, 0x0C, (short)(value*100))); }
        }

        [Category("01 Equipment")]
        [DisplayName("MaxDP")]
        [Description("Maximum phantom points")]
        public double MaxPP {
            get { return RamDisk.GetS16(GetPos()+0x0E)/100.0; }
            set { UndoRedo.Exec(new BindS16(this, 0x0E, (short)(value*100))); }
        }

        [ReadOnly(true)]
        [Category("01 Equipment")]
        [DisplayName("Damage Type Raw")]
        [Description("Damange type (blades)")]
        public byte DamageTypeRaw {
            get { return RamDisk.GetU8(GetPos()+0x0F); }
            set { UndoRedo.Exec(new BindU8(this, 0x0F, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Damage Type")]
        [Description("Damange type (blades)")]
        [DefaultValue("")]
        [TypeConverter(typeof(DamageTypesDropDown))]
        public string DamageType {
            get {
                byte index = RamDisk.GetU8(GetPos()+0x0F);
                return Model.damage_types.GetName(index);
            }
            set {
                byte index = (byte)Model.damage_types.GetIndexByName(value);
                UndoRedo.Exec(new BindU8(this, 0x0F, index));
            }
        }

        [ReadOnly(true)]
        [Category("01 Equipment")]
        [DisplayName("Damage Cost Stats Raw")]
        [Description("Stat affected (1=MP 2=RISK 3=HP 4=PP 5=nothing)")]
        public byte DamageCostStatsRaw {
            get { return RamDisk.GetU8(GetPos()+0x10); }
            set { UndoRedo.Exec(new BindU8(this, 0x10, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Damage Cost Stats")]
        [Description("Stat affected (1=MP 2=RISK 3=HP 4=PP 5=nothing)")]
        [DefaultValue("")]
        [TypeConverter(typeof(DamageStatsDropDown))]
        public string DamageCostStats {
            get {
                byte index = RamDisk.GetU8(GetPos()+0x10);
                return Model.damage_stats.GetName(index);
            }
            set {
                byte index = (byte)Model.damage_stats.GetIndexByName(value);
                UndoRedo.Exec(new BindU8(this, 0x10, index));
            }
        }

        [Category("01 Equipment")]
        [DisplayName("Damage Cost Value")]
        [Description("Stat cost value")]
        public byte DamageCostValue {
            get { return RamDisk.GetU8(GetPos()+0x12); }
            set { UndoRedo.Exec(new BindU8(this, 0x12, value)); }
        }

        [ReadOnly(true)]
        [Category("01 Equipment")]
        [DisplayName("Material Raw")]
        [Description("Material equipment is made of")]
        public byte MaterialRaw {
            get { return RamDisk.GetU8(GetPos()+0x13); }
            set { UndoRedo.Exec(new BindU8(this, 0x13, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Material")]
        [Description("Material equipment is made of")]
        [DefaultValue("")]
        [TypeConverter(typeof(ItemNameMaterialDropDown))]
        public string Material {
            get {
                byte index = RamDisk.GetU8(GetPos()+0x13);
                return Model.materials.GetName(index);
            }
            set {
                byte index = (byte)Model.materials.GetIndexByName(value);
                UndoRedo.Exec(new BindU8(this, 0x13, index));
            }
        }

        [Category("01 Equipment")]
        [DisplayName("Padding")]
        [Description("Unused")]
        public byte UnusedPadding1 {
            get { return RamDisk.GetU8(GetPos()+0x14); }
            set { UndoRedo.Exec(new BindU8(this, 0x14, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("NumSlots")]
        [Description("Number of gem slots")]
        public byte GemNumSlots {
            get { return RamDisk.GetU8(GetPos()+0x15); }
            set { UndoRedo.Exec(new BindU8(this, 0x15, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Gem Special Effects")]
        [Description("Special effects (used by accessories)")]
        public byte GemSpecialEffects {
            get { return RamDisk.GetU8(GetPos()+0x16); }
            set { UndoRedo.Exec(new BindU8(this, 0x16, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Index In GadgetBag")]
        [Description("RAM only")]
        public byte GadgetBagIndex {
            get { return RamDisk.GetU8(GetPos()+0x17); }
            set { UndoRedo.Exec(new BindU8(this, 0x17, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("RangeX")]
        [Description("Range horizontal")]
        public byte TargetRangeX {
            get { return RamDisk.GetU8(GetPos()+0x18); }
            set { UndoRedo.Exec(new BindU8(this, 0x18, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("RangeY")]
        [Description("Range vertical")]
        public byte TargetRangeY {
            get { return RamDisk.GetU8(GetPos()+0x19); }
            set { UndoRedo.Exec(new BindU8(this, 0x19, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("RangeZ")]
        [Description("Range depth")]
        public byte TargetRangeZ {
            get { return RamDisk.GetU8(GetPos()+0x1A); }
            set { UndoRedo.Exec(new BindU8(this, 0x1A, value)); }
        }

        [ReadOnly(true)]
        [Category("01 Equipment")]
        [DisplayName("Target Sphere Raw")]
        [Description("Target sphere shape")]
        public byte TargetSphereRaw {
            get { return RamDisk.GetU8(GetPos()+0x1B); }
            set { UndoRedo.Exec(new BindU8(this, 0x1B, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Target Sphere")]
        [Description("Target sphere shape")]
        [DefaultValue("")]
        [TypeConverter(typeof(TargetSphereDropDown))]
        public string TargetSphere {
            get {
                byte index = (byte)(RamDisk.GetU8(GetPos()+0x1B) % 8);
                return Model.targets.GetName(index);
            }
            set {
                byte val = (byte)(RamDisk.GetU8(GetPos()+0x1B) & ~7);
                byte index = (byte)(Model.targets.GetIndexByName(value) % 8);
                UndoRedo.Exec(new BindU8(this, 0x1B, (byte)(val | index)));
            }
        }

        [Category("01 Equipment")]
        [DisplayName("Target Angle")]
        [Description("Angle of target sphere")]
        public byte TargetAngle {
            get {
                byte val = RamDisk.GetU8(GetPos()+0x1B);
                return (byte)(val/8);
            }
            set {
                byte val = RamDisk.GetU8(GetPos()+0x1B);
                val = (byte)((val%8) | (4*(value % 32)));
                UndoRedo.Exec(new BindU8(this, 0x1B, val));
            }
        }

        [Category("02 Types")]
        [DisplayName("Blunt")]
        [Description("Resistence to blunt attacks")]
        public byte TypeBlunt {
            get { return RamDisk.GetU8(GetPos()+0x1D); }
            set { UndoRedo.Exec(new BindU8(this, 0x1D, value)); }
        }

        [Category("02 Types")]
        [DisplayName("Edged")]
        [Description("Resistence to edged attacks")]
        public byte TypeEdged {
            get { return RamDisk.GetU8(GetPos()+0x1E); }
            set { UndoRedo.Exec(new BindU8(this, 0x1E, value)); }
        }

        [Category("02 Types")]
        [DisplayName("Piercing")]
        [Description("Resistence to piercing attacks")]
        public byte TypePiercing {
            get { return RamDisk.GetU8(GetPos()+0x1F); }
            set { UndoRedo.Exec(new BindU8(this, 0x1F, value)); }
        }

        [Category("02 Types")]
        [DisplayName("Padding")]
        [Description("Unused")]
        public byte TypePadding {
            get { return RamDisk.GetU8(GetPos()+0x1C); }
            set { UndoRedo.Exec(new BindU8(this, 0x1C, value)); }
        }

        [Category("03 Classes")]
        [DisplayName("Evil")]
        [Description("Evil class")]
        public byte ClassEvil {
            get { return RamDisk.GetU8(GetPos()+0x20); }
            set { UndoRedo.Exec(new BindU8(this, 0x20, value)); }
        }

        [Category("03 Classes")]
        [DisplayName("Human")]
        [Description("Human class")]
        public byte ClassHuman {
            get { return RamDisk.GetU8(GetPos()+0x21); }
            set { UndoRedo.Exec(new BindU8(this, 0x21, value)); }
        }

        [Category("03 Classes")]
        [DisplayName("Beast")]
        [Description("Beast class")]
        public byte ClassBeast {
            get { return RamDisk.GetU8(GetPos()+0x22); }
            set { UndoRedo.Exec(new BindU8(this, 0x22, value)); }
        }

        [Category("03 Classes")]
        [DisplayName("Undead")]
        [Description("Undead class")]
        public byte ClassUndead {
            get { return RamDisk.GetU8(GetPos()+0x23); }
            set { UndoRedo.Exec(new BindU8(this, 0x23, value)); }
        }

        [Category("03 Classes")]
        [DisplayName("Phantom")]
        [Description("Phantom class")]
        public byte ClassPhantom {
            get { return RamDisk.GetU8(GetPos()+0x24); }
            set { UndoRedo.Exec(new BindU8(this, 0x24, value)); }
        }

        [Category("03 Classes")]
        [DisplayName("Dragon")]
        [Description("Dragon class")]
        public byte ClassDragon {
            get { return RamDisk.GetU8(GetPos()+0x25); }
            set { UndoRedo.Exec(new BindU8(this, 0x25, value)); }
        }

        [Category("03 Classes")]
        [DisplayName("Padding 1")]
        [Description("Unused")]
        public byte ClassPadding1 {
            get { return RamDisk.GetU8(GetPos()+0x26); }
            set { UndoRedo.Exec(new BindU8(this, 0x26, value)); }
        }

        [Category("03 Classes")]
        [DisplayName("Padding 2")]
        [Description("Unused")]
        public byte ClassPadding2 {
            get { return RamDisk.GetU8(GetPos()+0x27); }
            set { UndoRedo.Exec(new BindU8(this, 0x27, value)); }
        }

        [Category("04 Affinities")]
        [DisplayName("Earth")]
        [Description("Earth elemental affinity")]
        public byte AffinityEarth {
            get { return RamDisk.GetU8(GetPos()+0x28); }
            set { UndoRedo.Exec(new BindU8(this, 0x28, value)); }
        }

        [Category("04 Affinities")]
        [DisplayName("Air")]
        [Description("Air elemental affinity")]
        public byte AffinityAir {
            get { return RamDisk.GetU8(GetPos()+0x29); }
            set { UndoRedo.Exec(new BindU8(this, 0x29, value)); }
        }

        [Category("04 Affinities")]
        [DisplayName("Fire")]
        [Description("fire elemental affinity")]
        public byte AffinityFire {
            get { return RamDisk.GetU8(GetPos()+0x2A); }
            set { UndoRedo.Exec(new BindU8(this, 0x2A, value)); }
        }

        [Category("04 Affinities")]
        [DisplayName("Water")]
        [Description("Water elemental affinity")]
        public byte AffinityWater {
            get { return RamDisk.GetU8(GetPos()+0x2B); }
            set { UndoRedo.Exec(new BindU8(this, 0x2B, value)); }
        }

        [Category("04 Affinities")]
        [DisplayName("Physical")]
        [Description("Physical affinity")]
        public byte AffinityPhysical {
            get { return RamDisk.GetU8(GetPos()+0x2C); }
            set { UndoRedo.Exec(new BindU8(this, 0x2C, value)); }
        }

        [Category("04 Affinities")]
        [DisplayName("Light")]
        [Description("Light affinity")]
        public byte AffinityLight {
            get { return RamDisk.GetU8(GetPos()+0x2D); }
            set { UndoRedo.Exec(new BindU8(this, 0x2D, value)); }
        }

        [Category("04 Affinities")]
        [DisplayName("Dark")]
        [Description("Dark affinity")]
        public byte AffinityDark {
            get { return RamDisk.GetU8(GetPos()+0x2E); }
            set { UndoRedo.Exec(new BindU8(this, 0x2E, value)); }
        }

        [Category("04 Affinities")]
        [DisplayName("Padding")]
        [Description("Unused")]
        public byte AffinityPadding {
            get { return RamDisk.GetU8(GetPos()+0x2F); }
            set { UndoRedo.Exec(new BindU8(this, 0x2F, value)); }
        }
    }
}
