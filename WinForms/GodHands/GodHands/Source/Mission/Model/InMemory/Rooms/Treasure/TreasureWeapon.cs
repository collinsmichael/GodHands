using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GodHands {
    public class TreasureWeapon : InMemory {
        public TreasureWeapon(BaseClass parent, string url, int pos, Record rec):
        base(parent, url, pos, rec) {
        }

        public override int GetLen() {
            return 0xB8;
        }

        [Category("01 Equipment")]
        [DisplayName("Exists")]
        [Description("Exists=3 if exists")]
        public uint Exists {
            get { return RamDisk.GetU32(GetPos()+0x00); }
            set { UndoRedo.Exec(new BindU32(this, 0x00, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Name")]
        [Description("Weapon name (max 24 characters)")]
        public string Name {
            get {
                byte[] kildean = new byte[0x18];
                RamDisk.Get(GetPos()+0x94, 0x18, kildean);
                return Kildean.ToAscii(kildean);
            }
            set {
                string clip = value.Substring(0, Math.Min(0x18, value.Length));
                byte[] kildean = Kildean.ToKildean(clip, 0x18);
                UndoRedo.Exec(new BindArray(this, GetPos()+0x94, 0x18, kildean));
            }
        }

        [ReadOnly(true)]
        [Category("01 Equipment")]
        [DisplayName("Blade Raw")]
        [Description("Name of the item")]
        private byte BladeRaw {
            get { return RamDisk.GetU8(GetPos()+0x04); }
            set { UndoRedo.Exec(new BindU8(this, 0x04, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Blade")]
        [Description("Name of the blade")]
        [DefaultValue("")]
        [TypeConverter(typeof(ItemNamesListDropDown))]
        public string Blade {
            get { return Model.itemnames.GetName(BladeRaw); }
            set { BladeRaw = (byte)Model.itemnames.GetIndexByName(value); }
        }

        [ReadOnly(true)]
        [Category("01 Equipment")]
        [DisplayName("Item Category Raw")]
        [Description("Blade category")]
        private byte CategoryRaw {
            get { return RamDisk.GetU8(GetPos()+0x07); }
            set { UndoRedo.Exec(new BindU8(this, 0x07, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Category")]
        [Description("Blade category")]
        [DefaultValue("")]
        [TypeConverter(typeof(CategoryBladesDropDown))]
        public string Category {
            get { return Model.category_blades.GetName(CategoryRaw); }
            set { CategoryRaw = (byte)Model.category_blades.GetIndexByName(value); }
        }

        [Category("01 Equipment")]
        [DisplayName("Damage Raw")]
        [Description("Stat affected (1=MP 2=RISK 3=HP 4=PP 5=nothing)\n"
                    +"Type can be (1=BLUNT 2=EDGED 3=PIERCING)")]
        private byte DamageRaw {
            get { return RamDisk.GetU8(GetPos()+0x14); }
            set { UndoRedo.Exec(new BindU8(this, 0x14, value)); }
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

        [ReadOnly(true)]
        [Category("01 Equipment")]
        [DisplayName("Material Raw")]
        [Description("Material equipment is made of")]
        private ushort MaterialRaw {
            get { return RamDisk.GetU16(GetPos()+0x2C); }
            set { UndoRedo.Exec(new BindU16(this, 0x2C, value)); }
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

        [ReadOnly(true)]
        [Category("01 Equipment")]
        [DisplayName("Grip Names Raw")]
        [Description("Name of the item")]
        private short GripRaw {
            get { return RamDisk.GetS16(GetPos()+0x30); }
            set { UndoRedo.Exec(new BindS16(this, 0x30, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Grip")]
        [Description("Name of the item")]
        [DefaultValue("")]
        [TypeConverter(typeof(ItemNamesListDropDown))]
        public string Grip {
            get { return Model.itemnames.GetName(GripRaw); }
            set { GripRaw = (short)Model.itemnames.GetIndexByName(value); }
        }

        [Category("01 Equipment")]
        [DisplayName("Num Gems")]
        [Description("Number of gem slots")]
        public byte NumGems {
            get { return RamDisk.GetU8(GetPos()+0x34); }
            set { UndoRedo.Exec(new BindU8(this, 0x34, value)); }
        }

        [Category("02 Stats")]
        [DisplayName("STR")]
        [Description("Strength bonus")]
        public sbyte STR {
            get { return RamDisk.GetS8(GetPos()+0x10); }
            set { UndoRedo.Exec(new BindS8(this, 0x10, value)); }
        }

        [Category("02 Stats")]
        [DisplayName("INT")]
        [Description("Intelligence bonus")]
        public sbyte INT {
            get { return RamDisk.GetS8(GetPos()+0x11); }
            set { UndoRedo.Exec(new BindS8(this, 0x11, value)); }
        }

        [Category("02 Stats")]
        [DisplayName("AGL")]
        [Description("Agility bonus")]
        public sbyte AGL {
            get { return RamDisk.GetS8(GetPos()+0x12); }
            set { UndoRedo.Exec(new BindS8(this, 0x12, value)); }
        }

        [Category("02 Stats")]
        [DisplayName("Cur DP")]
        [Description("Damange points")]
        public double CurDP {
            get { return RamDisk.GetS16(GetPos()+0x0C)/100.0; }
            set { UndoRedo.Exec(new BindS16(this, 0x0C, (short)(value*100))); }
        }

        [Category("02 Stats")]
        [DisplayName("Max DP")]
        [Description("Maximum damange points")]
        public double MaxDP {
            get { return RamDisk.GetS16(GetPos()+0x08)/100.0; }
            set { UndoRedo.Exec(new BindS16(this, 0x08, (short)(value*100))); }
        }

        [Category("02 Stats")]
        [DisplayName("Cur PP")]
        [Description("Phantom points")]
        public double CurPP {
            get { return RamDisk.GetS16(GetPos()+0x0E); }
            set { UndoRedo.Exec(new BindS16(this, 0x0E, (short)(value))); }
        }

        [Category("02 Stats")]
        [DisplayName("Max PP")]
        [Description("Maximum phantom points")]
        public double MaxPP {
            get { return RamDisk.GetS16(GetPos()+0x0A); }
            set { UndoRedo.Exec(new BindS16(this, 0x0A, (short)(value))); }
        }
    }
}
