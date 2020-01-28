using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GodHands {
    public class RoomEnemy : InMemory {
        public RoomEnemy(string url, int pos, DirRec rec):
        base(url, pos, rec) {
        }

//EnemySection:
//     .Enemy00: MPD_ENEMY 0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x04,0x08,0x00,0xBB,0x04,0x09,0x00,0x0B,0x02,0x00,0x20,0x3F,0x00,0x00,0x1D,0x00,0x00,0x00,0x00,0xD2,0x01,0xA9,0x01,0xB0,0x01,0x01,0x01,0xFF,0x02,0x01,0x00,0x00,0x02
//     .Enemy01: MPD_ENEMY 0x00,0x01,0x00,0x00,0x02,0x00,0x03,0x03,0x2F,0x00,0xBA,0x04,0x09,0x00,0x0B,0x02,0x01,0x20,0x3F,0x00,0x00,0x1D,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x44,0x01,0x00,0x00,0x0D,0x02,0x01,0x00,0x00,0x02
// lenEnemySection = $ - EnemySection
//0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x04,0x08,0x00,0xBB,0x04,0x09,0x00,0x0B,0x02,
//0x00,0x20,0x3F,0x00,0x00,0x1D,0x00,0x00,0x00,0x00,0xD2,0x01,0xA9,0x01,0xB0,0x01,
//0x01,0x01,0xFF,0x02,0x01,0x00,0x00,0x02

        [Category("01 Enemy")]
        [DisplayName("Unknown_00")]
        [Description("Unknown")]
        public byte Unknown_00 {
            get { return RamDisk.GetU8(GetPos()+0x00); }
            set { UndoRedo.Exec(new BindU8(this, 0x00, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_01")]
        [Description("Unknown")]
        public byte Unknown_01 {
            get { return RamDisk.GetU8(GetPos()+0x01); }
            set { UndoRedo.Exec(new BindU8(this, 0x01, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_02")]
        [Description("Unknown")]
        public byte Unknown_02 {
            get { return RamDisk.GetU8(GetPos()+0x02); }
            set { UndoRedo.Exec(new BindU8(this, 0x02, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_03")]
        [Description("Unknown")]
        public byte Unknown_03 {
            get { return RamDisk.GetU8(GetPos()+0x03); }
            set { UndoRedo.Exec(new BindU8(this, 0x03, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_04")]
        [Description("Unknown")]
        public byte Unknown_04 {
            get { return RamDisk.GetU8(GetPos()+0x04); }
            set { UndoRedo.Exec(new BindU8(this, 0x04, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_05")]
        [Description("Unknown")]
        public byte Unknown_05 {
            get { return RamDisk.GetU8(GetPos()+0x05); }
            set { UndoRedo.Exec(new BindU8(this, 0x05, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_06")]
        [Description("Unknown")]
        public byte Unknown_06 {
            get { return RamDisk.GetU8(GetPos()+0x06); }
            set { UndoRedo.Exec(new BindU8(this, 0x06, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_07")]
        [Description("Unknown")]
        public byte Unknown_07 {
            get { return RamDisk.GetU8(GetPos()+0x07); }
            set { UndoRedo.Exec(new BindU8(this, 0x07, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_08")]
        [Description("Unknown")]
        public byte Unknown_08 {
            get { return RamDisk.GetU8(GetPos()+0x08); }
            set { UndoRedo.Exec(new BindU8(this, 0x08, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_09")]
        [Description("Unknown")]
        public byte Unknown_09 {
            get { return RamDisk.GetU8(GetPos()+0x09); }
            set { UndoRedo.Exec(new BindU8(this, 0x09, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_0A")]
        [Description("Unknown")]
        public byte Unknown_0A {
            get { return RamDisk.GetU8(GetPos()+0x0A); }
            set { UndoRedo.Exec(new BindU8(this, 0x0A, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_0B")]
        [Description("Unknown")]
        public byte Unknown_0B {
            get { return RamDisk.GetU8(GetPos()+0x0B); }
            set { UndoRedo.Exec(new BindU8(this, 0x0B, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_0C")]
        [Description("Unknown")]
        public byte Unknown_0C {
            get { return RamDisk.GetU8(GetPos()+0x0C); }
            set { UndoRedo.Exec(new BindU8(this, 0x0C, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_0D")]
        [Description("Unknown")]
        public byte Unknown_0D {
            get { return RamDisk.GetU8(GetPos()+0x0D); }
            set { UndoRedo.Exec(new BindU8(this, 0x0D, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_0E")]
        [Description("Unknown")]
        public byte Unknown_0E {
            get { return RamDisk.GetU8(GetPos()+0x0E); }
            set { UndoRedo.Exec(new BindU8(this, 0x0E, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_0F")]
        [Description("Unknown")]
        public byte Unknown_0F {
            get { return RamDisk.GetU8(GetPos()+0x0F); }
            set { UndoRedo.Exec(new BindU8(this, 0x0F, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_10")]
        [Description("Unknown")]
        public byte Unknown_10 {
            get { return RamDisk.GetU8(GetPos()+0x10); }
            set { UndoRedo.Exec(new BindU8(this, 0x10, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_11")]
        [Description("Unknown")]
        public byte Unknown_11 {
            get { return RamDisk.GetU8(GetPos()+0x11); }
            set { UndoRedo.Exec(new BindU8(this, 0x11, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_12")]
        [Description("Unknown")]
        public byte Unknown_12 {
            get { return RamDisk.GetU8(GetPos()+0x12); }
            set { UndoRedo.Exec(new BindU8(this, 0x12, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_13")]
        [Description("Unknown")]
        public byte Unknown_13 {
            get { return RamDisk.GetU8(GetPos()+0x13); }
            set { UndoRedo.Exec(new BindU8(this, 0x13, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_14")]
        [Description("Unknown")]
        public byte Unknown_14 {
            get { return RamDisk.GetU8(GetPos()+0x14); }
            set { UndoRedo.Exec(new BindU8(this, 0x14, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_15")]
        [Description("Unknown")]
        public byte Unknown_15 {
            get { return RamDisk.GetU8(GetPos()+0x15); }
            set { UndoRedo.Exec(new BindU8(this, 0x15, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_16")]
        [Description("Unknown")]
        public byte Unknown_16 {
            get { return RamDisk.GetU8(GetPos()+0x16); }
            set { UndoRedo.Exec(new BindU8(this, 0x16, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_17")]
        [Description("Unknown")]
        public byte Unknown_17 {
            get { return RamDisk.GetU8(GetPos()+0x17); }
            set { UndoRedo.Exec(new BindU8(this, 0x17, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_18")]
        [Description("Unknown")]
        public byte Unknown_18 {
            get { return RamDisk.GetU8(GetPos()+0x18); }
            set { UndoRedo.Exec(new BindU8(this, 0x18, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_19")]
        [Description("Unknown")]
        public byte Unknown_19 {
            get { return RamDisk.GetU8(GetPos()+0x19); }
            set { UndoRedo.Exec(new BindU8(this, 0x19, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_1A")]
        [Description("Unknown")]
        public byte Unknown_1A {
            get { return RamDisk.GetU8(GetPos()+0x1A); }
            set { UndoRedo.Exec(new BindU8(this, 0x1A, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_1B")]
        [Description("Unknown")]
        public byte Unknown_1B {
            get { return RamDisk.GetU8(GetPos()+0x1B); }
            set { UndoRedo.Exec(new BindU8(this, 0x1B, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_1C")]
        [Description("Unknown")]
        public byte Unknown_1C {
            get { return RamDisk.GetU8(GetPos()+0x1C); }
            set { UndoRedo.Exec(new BindU8(this, 0x1C, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_1D")]
        [Description("Unknown")]
        public byte Unknown_1D {
            get { return RamDisk.GetU8(GetPos()+0x1D); }
            set { UndoRedo.Exec(new BindU8(this, 0x1D, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_1E")]
        [Description("Unknown")]
        public byte Unknown_1E {
            get { return RamDisk.GetU8(GetPos()+0x1E); }
            set { UndoRedo.Exec(new BindU8(this, 0x1E, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_1F")]
        [Description("Unknown")]
        public byte Unknown_1F {
            get { return RamDisk.GetU8(GetPos()+0x1F); }
            set { UndoRedo.Exec(new BindU8(this, 0x1F, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_20")]
        [Description("Unknown")]
        public byte Unknown_20 {
            get { return RamDisk.GetU8(GetPos()+0x20); }
            set { UndoRedo.Exec(new BindU8(this, 0x20, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_21")]
        [Description("Unknown")]
        public byte Unknown_21 {
            get { return RamDisk.GetU8(GetPos()+0x21); }
            set { UndoRedo.Exec(new BindU8(this, 0x21, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_22")]
        [Description("Unknown")]
        public byte Unknown_22 {
            get { return RamDisk.GetU8(GetPos()+0x22); }
            set { UndoRedo.Exec(new BindU8(this, 0x22, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_23")]
        [Description("Unknown")]
        public byte Unknown_23 {
            get { return RamDisk.GetU8(GetPos()+0x23); }
            set { UndoRedo.Exec(new BindU8(this, 0x23, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_24")]
        [Description("Unknown")]
        public byte Unknown_24 {
            get { return RamDisk.GetU8(GetPos()+0x24); }
            set { UndoRedo.Exec(new BindU8(this, 0x24, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_25")]
        [Description("Unknown")]
        public byte Unknown_25 {
            get { return RamDisk.GetU8(GetPos()+0x25); }
            set { UndoRedo.Exec(new BindU8(this, 0x25, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_26")]
        [Description("Unknown")]
        public byte Unknown_26 {
            get { return RamDisk.GetU8(GetPos()+0x26); }
            set { UndoRedo.Exec(new BindU8(this, 0x26, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("Unknown_27")]
        [Description("Unknown")]
        public byte Unknown_27 {
            get { return RamDisk.GetU8(GetPos()+0x27); }
            set { UndoRedo.Exec(new BindU8(this, 0x27, value)); }
        }
    }
}
