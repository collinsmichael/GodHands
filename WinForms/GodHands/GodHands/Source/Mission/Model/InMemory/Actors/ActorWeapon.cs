using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GodHands {
    public class ActorWeapon : InMemory {
        public ActorWeapon(string url, int pos, DirRec rec):
        base(url, pos, rec) {
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
        [DisplayName("Item Category")]
        [Description("Weapon category")]
        [DefaultValue("")]
        [TypeConverter(typeof(CategoryBladesDropDown))]
        public string ItemCategory {
            get {
                byte index = RamDisk.GetU8(GetPos()+0x04);
                return Model.category_blades.GetName(index);
            }
            set {
                byte index = (byte)Model.category_blades.GetIndexByName(value);
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

        [Category("01 Equipment")]
        [DisplayName("Unknown 1")]
        [Description("Unknown")]
        public byte Unknown_1 {
            get { return RamDisk.GetU8(GetPos()+0xF2); }
            set { UndoRedo.Exec(new BindU8(this, 0xF2, value)); }
        }

        [Category("01 Equipment")]
        [DisplayName("Unknown 2")]
        [Description("Unknown")]
        public byte Unknown_2 {
            get { return RamDisk.GetU8(GetPos()+0xF3); }
            set { UndoRedo.Exec(new BindU8(this, 0xF3, value)); }
        }
    }
}
