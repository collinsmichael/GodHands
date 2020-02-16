using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GodHands {
    public class ActorWeapon : InMemory {
        public ActorWeapon(BaseClass parent, string url, int pos, Record rec):
        base(parent, url, pos, rec) {
        }

        public override int GetLen() {
            return 0x10C;
        }

        [Category("01 Equipment")]
        [DisplayName("Name")]
        [Description("Weapon name (max 24 characters)")]
        public string Name {
            get {
                byte[] kildean = new byte[0x18];
                RamDisk.Get(GetPos()+0xF4, 0x18, kildean);
                return Kildean.ToAscii(kildean);
            }
            set {
                string clip = value.Substring(0, Math.Min(0x18, value.Length));
                byte[] kildean = Kildean.ToKildean(clip, 0x18);
                UndoRedo.Exec(new BindArray(this, GetPos()+0xF4, 0x18, kildean));
            }
        }

        [Category("01 Equipment")]
        [DisplayName("Item Name")]
        [Description("Name of the item")]
        [DefaultValue("")]
        [TypeConverter(typeof(ItemNamesListDropDown))]
        public string ItemName {
            get {
                short index = RamDisk.GetS16(GetPos()+0x00);
                return Model.itemnames.GetName(index);
            }
            set {
                short index = (short)Model.itemnames.GetIndexByName(value);
                UndoRedo.Exec(new BindS16(this, 0x00, index));
            }
        }

        [Category("01 Equipment")]
        [DisplayName("Items List")]
        [Description("Depends on item type")]
        [DefaultValue("")]
        [TypeConverter(typeof(ItemNameBladeDropDown))]
        public string ItemsList {
            get {
                byte index = RamDisk.GetU8(GetPos()+0x02);
                return Model.blade_names.GetName(index);
            }
            set {
                byte index = (byte)Model.blade_names.GetIndexByName(value);
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

        [Category("01 Equipment")]
        [DisplayName("Category")]
        [Description("Weapon category")]
        [DefaultValue("")]
        [TypeConverter(typeof(CategoryBladesDropDown))]
        public string Category {
            get {
                byte index = RamDisk.GetU8(GetPos()+0x04);
                return Model.category_blades.GetName(index);
            }
            set {
                byte index = (byte)Model.category_blades.GetIndexByName(value);
                UndoRedo.Exec(new BindU8(this, 0x04, index));
            }
        }

        [ReadOnly(true)]
        [Category("01 Equipment")]
        [DisplayName("STR")]
        [Description("Strength (computed from Blade+Grip+Gems)")]
        public int STR {
            get {
                int blade = RamDisk.GetS8(GetPos()+0x00+0x05);
                int grip  = RamDisk.GetS8(GetPos()+0x30+0x05);
                int gem1  = RamDisk.GetS8(GetPos()+0x60+0x05);
                int gem2  = RamDisk.GetS8(GetPos()+0x90+0x05);
                int gem3  = RamDisk.GetS8(GetPos()+0xC0+0x05);
                return blade + grip + gem1 + gem2 + gem3;
            }
            set { }
        }

        [ReadOnly(true)]
        [Category("01 Equipment")]
        [DisplayName("INT")]
        [Description("Intelligence (computed from Blade+Grip+Gems)")]
        public int INT {
            get {
                int blade = RamDisk.GetS8(GetPos()+0x00+0x06);
                int grip  = RamDisk.GetS8(GetPos()+0x30+0x06);
                int gem1  = RamDisk.GetS8(GetPos()+0x60+0x06);
                int gem2  = RamDisk.GetS8(GetPos()+0x90+0x06);
                int gem3  = RamDisk.GetS8(GetPos()+0xC0+0x06);
                return blade + grip + gem1 + gem2 + gem3;
            }
            set { }
        }

        [ReadOnly(true)]
        [Category("01 Equipment")]
        [DisplayName("AGL")]
        [Description("Agility (computed from Blade+Grip+Gems)")]
        public int AGL {
            get {
                int blade = RamDisk.GetS8(GetPos()+0x00+0x07);
                int grip  = RamDisk.GetS8(GetPos()+0x30+0x07);
                int gem1  = RamDisk.GetS8(GetPos()+0x60+0x07);
                int gem2  = RamDisk.GetS8(GetPos()+0x90+0x07);
                int gem3  = RamDisk.GetS8(GetPos()+0xC0+0x07);
                return blade + grip + gem1 + gem2 + gem3;
            }
            set { }
        }

        [Category("01 Equipment")]
        [DisplayName("Cur DP")]
        [Description("Damange points")]
        public double CurDP {
            get { return RamDisk.GetS16(GetPos()+0x08)/100.0; }
            set { UndoRedo.Exec(new BindS16(this, 0x08, (short)(value*100))); }
        }

        [Category("01 Equipment")]
        [DisplayName("Max DP")]
        [Description("Maximum damange points")]
        public double MaxDP {
            get { return RamDisk.GetS16(GetPos()+0x0A)/100.0; }
            set { UndoRedo.Exec(new BindS16(this, 0x0A, (short)(value*100))); }
        }

        [Category("01 Equipment")]
        [DisplayName("Cur PP")]
        [Description("Phantom points")]
        public double CurPP {
            get { return RamDisk.GetS16(GetPos()+0x0C); }
            set { UndoRedo.Exec(new BindS16(this, 0x0C, (short)(value))); }
        }

        [Category("01 Equipment")]
        [DisplayName("Max PP")]
        [Description("Maximum phantom points")]
        public double MaxPP {
            get { return RamDisk.GetS16(GetPos()+0x0E); }
            set { UndoRedo.Exec(new BindS16(this, 0x0E, (short)(value))); }
        }

        [Category("01 Equipment")]
        [DisplayName("NumSlots")]
        [Description("Number of gem slots")]
        public byte GemNumSlots {
            get { return RamDisk.GetU8(GetPos()+0x75); }
            set { UndoRedo.Exec(new BindU8(this, 0x75, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Material")]
        [Description("Material equipment is made of")]
        [DefaultValue("")]
        [TypeConverter(typeof(ItemNameMaterialDropDown))]
        public string Material {
            get {
                byte index = RamDisk.GetU8(GetPos()+0xF0);
                return Model.materials.GetName(index);
            }
            set {
                byte index = (byte)Model.materials.GetIndexByName(value);
                UndoRedo.Exec(new BindU8(this, 0xF0, index));
            }
        }

        [Category("01 Equipment")]
        [DisplayName("Drop Chance")]
        [Description("Probability that the item will be dropped (unit is percent)")]
        public double DropChance {
            get {
                byte val = RamDisk.GetU8(GetPos()+0xF1);
                return val*100/255.0;
            }
            set {
                byte val = (byte)Math.Min(Math.Max(0, value*255/100), 255);
                UndoRedo.Exec(new BindU8(this, 0xF1, val));
            }
        }

        [ReadOnly(true)]
        [Category("01 Equipment")]
        [DisplayName("Damage Type Raw")]
        [Description("Damange type (blades)")]
        private byte DamageTypeRaw {
            get { return RamDisk.GetU8(GetPos()+0x10); }
            set { UndoRedo.Exec(new BindU8(this, 0x10, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Damage Type")]
        [Description("Damange type (blades)")]
        [DefaultValue("")]
        [TypeConverter(typeof(DamageTypesDropDown))]
        public string DamageType {
            get { return Model.damage_types.GetName(DamageTypeRaw); }
            set { DamageTypeRaw = (byte)Model.damage_types.GetIndexByName(value); }
        }

        [ReadOnly(true)]
        [Category("01 Equipment")]
        [DisplayName("Damage Stat Raw")]
        [Description("Stat affected (1=MP 2=RISK 3=HP 4=PP 5=nothing)")]
        private byte DamageStatRaw {
            get { return RamDisk.GetU8(GetPos()+0x11); }
            set { UndoRedo.Exec(new BindU8(this, 0x11, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Damage Stats")]
        [Description("Stat affected (1=MP 2=RISK 3=HP 4=PP 5=nothing)")]
        [DefaultValue("")]
        [TypeConverter(typeof(DamageStatsDropDown))]
        public string DamageStats {
            get { return Model.damage_stats.GetName(DamageStatRaw); }
            set { DamageStatRaw = (byte)Model.damage_stats.GetIndexByName(value); }
        }

        [Category("01 Equipment")]
        [DisplayName("Damage Cost")]
        [Description("Stat cost value")]
        public byte DamageCost {
            get { return RamDisk.GetU8(GetPos()+0x12); }
            set { UndoRedo.Exec(new BindU8(this, 0x12, value)); }
        }


















        [ReadOnly(true)]
        [Category("02 Types")]
        [DisplayName("Blunt")]
        [Description("Resistence to blunt attacks (computed from Blade+Grip+Gems)")]
        public int TypeBlunt {
            get {
                int blade = RamDisk.GetS8(GetPos()+0x00+0x1D);
                int grip  = RamDisk.GetS8(GetPos()+0x30+0x1D);
                int gem1  = RamDisk.GetS8(GetPos()+0x60+0x1D);
                int gem2  = RamDisk.GetS8(GetPos()+0x90+0x1D);
                int gem3  = RamDisk.GetS8(GetPos()+0xC0+0x1D);
                return blade + grip + gem1 + gem2 + gem3;
            }
            set { }
        }

        [ReadOnly(true)]
        [Category("02 Types")]
        [DisplayName("Edged")]
        [Description("Resistence to edged attacks (computed from Blade+Grip+Gems)")]
        public int TypeEdged {
            get {
                int blade = RamDisk.GetS8(GetPos()+0x00+0x1E);
                int grip  = RamDisk.GetS8(GetPos()+0x30+0x1E);
                int gem1  = RamDisk.GetS8(GetPos()+0x60+0x1E);
                int gem2  = RamDisk.GetS8(GetPos()+0x90+0x1E);
                int gem3  = RamDisk.GetS8(GetPos()+0xC0+0x1E);
                return blade + grip + gem1 + gem2 + gem3;
            }
            set { }
        }

        [ReadOnly(true)]
        [Category("02 Types")]
        [DisplayName("Piercing")]
        [Description("Resistence to piercing attacks (computed from Blade+Grip+Gems)")]
        public int TypePiercing {
            get {
                int blade = RamDisk.GetS8(GetPos()+0x00+0x1F);
                int grip  = RamDisk.GetS8(GetPos()+0x30+0x1F);
                int gem1  = RamDisk.GetS8(GetPos()+0x60+0x1F);
                int gem2  = RamDisk.GetS8(GetPos()+0x90+0x1F);
                int gem3  = RamDisk.GetS8(GetPos()+0xC0+0x1F);
                return blade + grip + gem1 + gem2 + gem3;
            }
            set { }
        }




        [ReadOnly(true)]
        [Category("03 Classes")]
        [DisplayName("Human")]
        [Description("Human class (computed from Blade+Grip+Gems)")]
        public int ClassHuman {
            get {
                int blade = RamDisk.GetS8(GetPos()+0x00+0x20);
                int grip  = RamDisk.GetS8(GetPos()+0x30+0x20);
                int gem1  = RamDisk.GetS8(GetPos()+0x60+0x20);
                int gem2  = RamDisk.GetS8(GetPos()+0x90+0x20);
                int gem3  = RamDisk.GetS8(GetPos()+0xC0+0x20);
                return blade + grip + gem1 + gem2 + gem3;
            }
            set { }
        }

        [ReadOnly(true)]
        [Category("03 Classes")]
        [DisplayName("Beast")]
        [Description("Beast class (computed from Blade+Grip+Gems)")]
        public int ClassBeast {
            get {
                int blade = RamDisk.GetS8(GetPos()+0x00+0x21);
                int grip  = RamDisk.GetS8(GetPos()+0x30+0x21);
                int gem1  = RamDisk.GetS8(GetPos()+0x60+0x21);
                int gem2  = RamDisk.GetS8(GetPos()+0x90+0x21);
                int gem3  = RamDisk.GetS8(GetPos()+0xC0+0x21);
                return blade + grip + gem1 + gem2 + gem3;
            }
            set { }
        }

        [ReadOnly(true)]
        [Category("03 Classes")]
        [DisplayName("Undead")]
        [Description("Undead class (computed from Blade+Grip+Gems)")]
        public int ClassUndead {
            get {
                int blade = RamDisk.GetS8(GetPos()+0x00+0x22);
                int grip  = RamDisk.GetS8(GetPos()+0x30+0x22);
                int gem1  = RamDisk.GetS8(GetPos()+0x60+0x22);
                int gem2  = RamDisk.GetS8(GetPos()+0x90+0x22);
                int gem3  = RamDisk.GetS8(GetPos()+0xC0+0x22);
                return blade + grip + gem1 + gem2 + gem3;
            }
            set { }
        }

        [ReadOnly(true)]
        [Category("03 Classes")]
        [DisplayName("Phantom")]
        [Description("Phantom class (computed from Blade+Grip+Gems)")]
        public int ClassPhantom {
            get {
                int blade = RamDisk.GetS8(GetPos()+0x00+0x23);
                int grip  = RamDisk.GetS8(GetPos()+0x30+0x23);
                int gem1  = RamDisk.GetS8(GetPos()+0x60+0x23);
                int gem2  = RamDisk.GetS8(GetPos()+0x90+0x23);
                int gem3  = RamDisk.GetS8(GetPos()+0xC0+0x23);
                return blade + grip + gem1 + gem2 + gem3;
            }
            set { }
        }

        [ReadOnly(true)]
        [Category("03 Classes")]
        [DisplayName("Dragon")]
        [Description("Dragon class (computed from Blade+Grip+Gems)")]
        public int ClassDragon {
            get {
                int blade = RamDisk.GetS8(GetPos()+0x00+0x24);
                int grip  = RamDisk.GetS8(GetPos()+0x30+0x24);
                int gem1  = RamDisk.GetS8(GetPos()+0x60+0x24);
                int gem2  = RamDisk.GetS8(GetPos()+0x90+0x24);
                int gem3  = RamDisk.GetS8(GetPos()+0xC0+0x24);
                return blade + grip + gem1 + gem2 + gem3;
            }
            set { }
        }

        [ReadOnly(true)]
        [Category("03 Classes")]
        [DisplayName("Evil")]
        [Description("Evil class (computed from Blade+Grip+Gems)")]
        public int ClassEvil {
            get {
                int blade = RamDisk.GetS8(GetPos()+0x00+0x25);
                int grip  = RamDisk.GetS8(GetPos()+0x30+0x25);
                int gem1  = RamDisk.GetS8(GetPos()+0x60+0x25);
                int gem2  = RamDisk.GetS8(GetPos()+0x90+0x25);
                int gem3  = RamDisk.GetS8(GetPos()+0xC0+0x25);
                return blade + grip + gem1 + gem2 + gem3;
            }
            set { }
        }





        [ReadOnly(true)]
        [Category("04 Affinities")]
        [DisplayName("Physical")]
        [Description("Physical affinity (computed from Blade+Grip+Gems)")]
        public int AffinityPhysical {
            get {
                int blade = RamDisk.GetS8(GetPos()+0x00+0x28);
                int grip  = RamDisk.GetS8(GetPos()+0x30+0x28);
                int gem1  = RamDisk.GetS8(GetPos()+0x60+0x28);
                int gem2  = RamDisk.GetS8(GetPos()+0x90+0x28);
                int gem3  = RamDisk.GetS8(GetPos()+0xC0+0x28);
                return blade + grip + gem1 + gem2 + gem3;
            }
            set { }
        }

        [ReadOnly(true)]
        [Category("04 Affinities")]
        [DisplayName("Air")]
        [Description("Air elemental affinity (computed from Blade+Grip+Gems)")]
        public int AffinityAir {
            get {
                int blade = RamDisk.GetS8(GetPos()+0x00+0x29);
                int grip  = RamDisk.GetS8(GetPos()+0x30+0x29);
                int gem1  = RamDisk.GetS8(GetPos()+0x60+0x29);
                int gem2  = RamDisk.GetS8(GetPos()+0x90+0x29);
                int gem3  = RamDisk.GetS8(GetPos()+0xC0+0x29);
                return blade + grip + gem1 + gem2 + gem3;
            }
            set { }
        }

        [ReadOnly(true)]
        [Category("04 Affinities")]
        [DisplayName("Fire")]
        [Description("fire elemental affinity (computed from Blade+Grip+Gems)")]
        public int AffinityFire {
            get {
                int blade = RamDisk.GetS8(GetPos()+0x00+0x2A);
                int grip  = RamDisk.GetS8(GetPos()+0x30+0x2A);
                int gem1  = RamDisk.GetS8(GetPos()+0x60+0x2A);
                int gem2  = RamDisk.GetS8(GetPos()+0x90+0x2A);
                int gem3  = RamDisk.GetS8(GetPos()+0xC0+0x2A);
                return blade + grip + gem1 + gem2 + gem3;
            }
            set { }
        }

        [ReadOnly(true)]
        [Category("04 Affinities")]
        [DisplayName("Earth")]
        [Description("Earth elemental affinity (computed from Blade+Grip+Gems)")]
        public int AffinityEarth {
            get {
                int blade = RamDisk.GetS8(GetPos()+0x00+0x2B);
                int grip  = RamDisk.GetS8(GetPos()+0x30+0x2B);
                int gem1  = RamDisk.GetS8(GetPos()+0x60+0x2B);
                int gem2  = RamDisk.GetS8(GetPos()+0x90+0x2B);
                int gem3  = RamDisk.GetS8(GetPos()+0xC0+0x2B);
                return blade + grip + gem1 + gem2 + gem3;
            }
            set { }
        }

        [ReadOnly(true)]
        [Category("04 Affinities")]
        [DisplayName("Water")]
        [Description("Water elemental affinity (computed from Blade+Grip+Gems)")]
        public int AffinityWater {
            get {
                int blade = RamDisk.GetS8(GetPos()+0x00+0x2C);
                int grip  = RamDisk.GetS8(GetPos()+0x30+0x2C);
                int gem1  = RamDisk.GetS8(GetPos()+0x60+0x2C);
                int gem2  = RamDisk.GetS8(GetPos()+0x90+0x2C);
                int gem3  = RamDisk.GetS8(GetPos()+0xC0+0x2C);
                return blade + grip + gem1 + gem2 + gem3;
            }
            set { }
        }

        [ReadOnly(true)]
        [Category("04 Affinities")]
        [DisplayName("Light")]
        [Description("Light affinity (computed from Blade+Grip+Gems)")]
        public int AffinityLight {
            get {
                int blade = RamDisk.GetS8(GetPos()+0x00+0x2D);
                int grip  = RamDisk.GetS8(GetPos()+0x30+0x2D);
                int gem1  = RamDisk.GetS8(GetPos()+0x60+0x2D);
                int gem2  = RamDisk.GetS8(GetPos()+0x90+0x2D);
                int gem3  = RamDisk.GetS8(GetPos()+0xC0+0x2D);
                return blade + grip + gem1 + gem2 + gem3;
            }
            set { }
        }

        [ReadOnly(true)]
        [Category("04 Affinities")]
        [DisplayName("Dark")]
        [Description("Dark affinity (computed from Blade+Grip+Gems)")]
        public int AffinityDark {
            get {
                int blade = RamDisk.GetS8(GetPos()+0x00+0x2E);
                int grip  = RamDisk.GetS8(GetPos()+0x30+0x2E);
                int gem1  = RamDisk.GetS8(GetPos()+0x60+0x2E);
                int gem2  = RamDisk.GetS8(GetPos()+0x90+0x2E);
                int gem3  = RamDisk.GetS8(GetPos()+0xC0+0x2E);
                return blade + grip + gem1 + gem2 + gem3;
            }
            set { }
        }
    }
}
