using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GodHands {
    public class Actor : BaseClass {
        private DirRec rec;
        private ZUD zud;

        public Actor(string url, int pos, DirRec rec) : base(url, pos) {
            this.rec = rec;
            zud = Model.zuds[rec.GetUrl()];
        }

        public string GetZndFileName() {
            return rec.GetFileName();
        }

        [Category("Stats")]
        [DisplayName("Unknown_01")]
        [Description("No information")]
        public byte Unknown_01 {
            get { return RamDisk.GetU8(GetPos()+0x00); }
            set { UndoRedo.Exec(new BindU8(this, 0x00, value)); }
        }

        [Category("Stats")]
        [DisplayName("Unknown_02")]
        [Description("No information")]
        public byte Unknown_02 {
            get { return RamDisk.GetU8(GetPos()+0x01); }
            set { UndoRedo.Exec(new BindU8(this, 0x01, value)); }
        }

        [Category("Stats")]
        [DisplayName("3D Effects")]
        [Description("3D model special effects")]
        public byte Index3DFX {
            get { return RamDisk.GetU8(GetPos()+0x02); }
            set { UndoRedo.Exec(new BindU8(this, 0x02, value)); }
        }

        [Category("Stats")]
        [DisplayName("Unknown_04")]
        [Description("No information")]
        public byte Unknown_04 {
            get { return RamDisk.GetU8(GetPos()+0x03); }
            set { UndoRedo.Exec(new BindU8(this, 0x03, value)); }
        }

        [Category("Stats")]
        [DisplayName("Name")]
        [Description("Name of the character (max 24 letters)")]
        public string Name {
            get {
                //return RamDisk.GetString(GetPos()+0x00, 0x18);
                byte[] kildean = new byte[0x18];
                RamDisk.Get(GetPos()+0x04, 0x18, kildean);
                return Kildean.ToAscii(kildean);
            }
            set {
                //UndoRedo.Exec(new BindString(this, 0x00, 0x18, value));
                string clip = value.Substring(0, Math.Min(0x18, value.Length));
                byte[] kildean = Kildean.ToKildean(clip);
                UndoRedo.Exec(new BindArray(this, 0x04, 0x18, kildean));
            }
        }

        [Category("Stats")]
        [DisplayName("HP")]
        [Description("Health points (depleted when taking damage)")]
        public ushort HP {
            get { return RamDisk.GetU16(GetPos()+0x1C); }
            set { UndoRedo.Exec(new BindU16(this, 0x1C, value)); }
        }

        [Category("Stats")]
        [DisplayName("MP")]
        [Description("Magic points (depleted by casting spells)")]
        public ushort MP {
            get { return RamDisk.GetU16(GetPos()+0x1E); }
            set { UndoRedo.Exec(new BindU16(this, 0x1E, value)); }
        }

        [Category("Stats")]
        [DisplayName("INT")]
        [Description("Intelligence (affects ability to cast magic)")]
        public byte INT {
            get { return RamDisk.GetU8(GetPos()+0x20); }
            set { UndoRedo.Exec(new BindU8(this, 0x20, value)); }
        }

        [Category("Stats")]
        [DisplayName("AGL")]
        [Description("Agility (affects ability to evade attacks)")]
        public byte AGL {
            get { return RamDisk.GetU8(GetPos()+0x21); }
            set { UndoRedo.Exec(new BindU8(this, 0x21, value)); }
        }

        [Category("Stats")]
        [DisplayName("STR")]
        [Description("Agility (affects ability to deal damage)")]
        public byte STR {
            get { return RamDisk.GetU8(GetPos()+0x22); }
            set { UndoRedo.Exec(new BindU8(this, 0x22, value)); }
        }

        [Category("Stats")]
        [DisplayName("Unknown_05")]
        [Description("No information")]
        public byte Unknown_05 {
            get { return RamDisk.GetU8(GetPos()+0x23); }
            set { UndoRedo.Exec(new BindU8(this, 0x23, value)); }
        }

        [Category("Stats")]
        [DisplayName("Unknown_06")]
        [Description("No information")]
        public byte Unknown_06 {
            get { return RamDisk.GetU8(GetPos()+0x24); }
            set { UndoRedo.Exec(new BindU8(this, 0x24, value)); }
        }

        [Category("Stats")]
        [DisplayName("Carry Speed")]
        [Description("Walking speed whilst carrying crates")]
        public byte CarrySpeed {
            get { return RamDisk.GetU8(GetPos()+0x25); }
            set { UndoRedo.Exec(new BindU8(this, 0x25, value)); }
        }

        [Category("Stats")]
        [DisplayName("Unknown_07")]
        [Description("No information")]
        public byte Unknown_07 {
            get { return RamDisk.GetU8(GetPos()+0x26); }
            set { UndoRedo.Exec(new BindU8(this, 0x26, value)); }
        }


        [Category("Stats")]
        [DisplayName("Run Speed")]
        [Description("Speed whilst running")]
        public byte RunSpeed {
            get { return RamDisk.GetU8(GetPos()+0x27); }
            set { UndoRedo.Exec(new BindU8(this, 0x27, value)); }
        }

        [Category("Stats")]
        [DisplayName("Unknown_08")]
        [Description("No information")]
        public byte Unknown_08 {
            get { return RamDisk.GetU8(GetPos()+0x28); }
            set { UndoRedo.Exec(new BindU8(this, 0x28, value)); }
        }

        [Category("Stats")]
        [DisplayName("Unknown_09")]
        [Description("No information")]
        public byte Unknown_09 {
            get { return RamDisk.GetU8(GetPos()+0x29); }
            set { UndoRedo.Exec(new BindU8(this, 0x29, value)); }
        }

        [Category("Stats")]
        [DisplayName("Unknown_10")]
        [Description("No information")]
        public byte Unknown_10 {
            get { return RamDisk.GetU8(GetPos()+0x2A); }
            set { UndoRedo.Exec(new BindU8(this, 0x2A, value)); }
        }

        [Category("Stats")]
        [DisplayName("Unknown_11")]
        [Description("No information")]
        public byte Unknown_11 {
            get { return RamDisk.GetU8(GetPos()+0x2B); }
            set { UndoRedo.Exec(new BindU8(this, 0x2B, value)); }
        }

        [Category("Stats")]
        [DisplayName("Enemy ID")]
        [Description("Enemy identifier (used by MPD files)")]
        public int EnemyID {
            get { return RamDisk.GetS32(GetPos()+0x460); }
            set { UndoRedo.Exec(new BindS32(this, 0x460, value)); }
        }
    }
}
