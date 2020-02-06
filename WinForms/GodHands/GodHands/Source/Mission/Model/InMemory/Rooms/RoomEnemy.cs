using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GodHands {
    public class RoomEnemy : InMemory {
        private Zone zone;
        private Room room;

        public RoomEnemy(string url, int pos, DirRec rec, Zone zone, Room room):
        base(url, pos, rec) {
            this.zone = zone;
            this.room = room;
        }

        public override string GetText() {
            int enemy_zid = ZndEnemyID_04;
            string name = "(" + enemy_zid.ToString("X2") + ") ";
            if ((enemy_zid >= 0) && (enemy_zid < zone.actors.Count)) {
                name = name + zone.actors[enemy_zid].Name;
            } else {
                name = name + "Out of bounds";
            }
            return name;
        }

        public override int GetLen() {
            return 0x28;
        }

//EnemySection:
//     .Enemy00: MPD_ENEMY 0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x04,0x08,0x00,0xBB,0x04,0x09,0x00,0x0B,0x02,0x00,0x20,0x3F,0x00,0x00,0x1D,0x00,0x00,0x00,0x00,0xD2,0x01,0xA9,0x01,0xB0,0x01,0x01,0x01,0xFF,0x02,0x01,0x00,0x00,0x02
//     .Enemy01: MPD_ENEMY 0x00,0x01,0x00,0x00,0x02,0x00,0x03,0x03,0x2F,0x00,0xBA,0x04,0x09,0x00,0x0B,0x02,0x01,0x20,0x3F,0x00,0x00,0x1D,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x44,0x01,0x00,0x00,0x0D,0x02,0x01,0x00,0x00,0x02
// lenEnemySection = $ - EnemySection
//0x00,0x00,0x00,0x00,0x00,0x00,0x01,0x04,0x08,0x00,0xBB,0x04,0x09,0x00,0x0B,0x02,
//0x00,0x20,0x3F,0x00,0x00,0x1D,0x00,0x00,0x00,0x00,0xD2,0x01,0xA9,0x01,0xB0,0x01,
//0x01,0x01,0xFF,0x02,0x01,0x00,0x00,0x02

        [Category("01 Enemy")]
        [DisplayName("00 Deleted")]
        [Description("Always 00 if enemy exists and non-zero if enemy was deleted")]
        public byte Unknown_00 {
            get { return RamDisk.GetU8(GetPos()+0x00); }
            set { UndoRedo.Exec(new BindU8(this, 0x00, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("01 Mpd Enemy ID")]
        [Description("ID used within this room")]
        public byte MpdEnemyID_01 {
            get { return RamDisk.GetU8(GetPos()+0x01); }
            set { UndoRedo.Exec(new BindU8(this, 0x01, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("02 Unknown (Always 00)")]
        [Description("Unknown")]
        public byte Unknown_02 {
            get { return RamDisk.GetU8(GetPos()+0x02); }
            set { UndoRedo.Exec(new BindU8(this, 0x02, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("03 Unknown (Always 00)")]
        [Description("Unknown")]
        public byte Unknown_03 {
            get { return RamDisk.GetU8(GetPos()+0x03); }
            set { UndoRedo.Exec(new BindU8(this, 0x03, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("04 Znd Enemy ID")]
        [Description("ID used by ZND files")]
        public byte ZndEnemyID_04 {
            // TODO Use Zone.GetEnemyName(id)
            get { return RamDisk.GetU8(GetPos()+0x04); }
            set { UndoRedo.Exec(new BindU8(this, 0x04, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("05 Unknown (Bosses)")]
        [Description("Always zero for normal enemies but bosses are non zero")]
        public byte UnknownBosses_05 {
            get { return RamDisk.GetU8(GetPos()+0x05); }
            set { UndoRedo.Exec(new BindU8(this, 0x05, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("06 Story Trigger Param")]
        [Description("Unknown")]
        public byte StoryEventOutcome_06 {
            get { return RamDisk.GetU8(GetPos()+0x06); }
            set { UndoRedo.Exec(new BindU8(this, 0x06, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("07 Locale Trigger")]
        [Description("Unknown")]
        public byte AshleyLocaleTrigger_07 {
            get { return RamDisk.GetU8(GetPos()+0x07); }
            set { UndoRedo.Exec(new BindU8(this, 0x07, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("08 Story Trigger")]
        [Description("Unknown")]
        public byte StoryEventTrigger_08 {
            get { return RamDisk.GetU8(GetPos()+0x08); }
            set { UndoRedo.Exec(new BindU8(this, 0x08, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("09 Unknown (Always 00)")]
        [Description("Unknown")]
        public byte Unknown_09 {
            get { return RamDisk.GetU8(GetPos()+0x09); }
            set { UndoRedo.Exec(new BindU8(this, 0x09, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("0A Locale Trigger Param 1")]
        [Description("Unknown")]
        public byte LocaleTriggerParam1_0A {
            get { return RamDisk.GetU8(GetPos()+0x0A); }
            set { UndoRedo.Exec(new BindU8(this, 0x0A, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("0B Locale Trigger Param 2")]
        [Description("Unknown")]
        public byte LocaleTriggerParam2_0B {
            get { return RamDisk.GetU8(GetPos()+0x0B); }
            set { UndoRedo.Exec(new BindU8(this, 0x0B, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("0C PosX")]
        [Description("Starting position within the room")]
        public byte PosX_0C {
            get { return RamDisk.GetU8(GetPos()+0x0C); }
            set { UndoRedo.Exec(new BindU8(this, 0x0C, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("0D Unknown")]
        [Description("Unknown")]
        public byte Unknown_0D {
            get { return RamDisk.GetU8(GetPos()+0x0D); }
            set { UndoRedo.Exec(new BindU8(this, 0x0D, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("0E PosY")]
        [Description("Starting position within the room")]
        public byte PosY_0E {
            get { return RamDisk.GetU8(GetPos()+0x0E); }
            set { UndoRedo.Exec(new BindU8(this, 0x0E, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("0F Direction")]
        [Description("Can be north/south/east/west")]
        public byte FacingDirection_0F {
            get { return RamDisk.GetU8(GetPos()+0x0F); }
            set { UndoRedo.Exec(new BindU8(this, 0x0F, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("10 Aggression")]
        [Description("Determines how aggressive and enemy is")]
        public byte Behavior_10 {
            get { return RamDisk.GetU8(GetPos()+0x10); }
            set { UndoRedo.Exec(new BindU8(this, 0x10, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("11 Unknown (Always 32)")]
        [Description("Unknown")]
        public byte Unknown_11 {
            get { return RamDisk.GetU8(GetPos()+0x11); }
            set { UndoRedo.Exec(new BindU8(this, 0x11, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("12 Unknown (Always 63)")]
        [Description("Unknown")]
        public byte Unknown_12 {
            get { return RamDisk.GetU8(GetPos()+0x12); }
            set { UndoRedo.Exec(new BindU8(this, 0x12, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("13 Unknown (Always 00)")]
        [Description("Unknown")]
        public byte Unknown_13 {
            get { return RamDisk.GetU8(GetPos()+0x13); }
            set { UndoRedo.Exec(new BindU8(this, 0x13, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("14 Unknown (Always 00)")]
        [Description("Unknown")]
        public byte Unknown_14 {
            get { return RamDisk.GetU8(GetPos()+0x14); }
            set { UndoRedo.Exec(new BindU8(this, 0x14, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("15 Unknown (Always 29 or 61)")]
        [Description("Unknown")]
        public byte Unknown_15 {
            get { return RamDisk.GetU8(GetPos()+0x15); }
            set { UndoRedo.Exec(new BindU8(this, 0x15, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("16 Unknown (Always 00)")]
        [Description("Unknown")]
        public byte Unknown_16 {
            get { return RamDisk.GetU8(GetPos()+0x16); }
            set { UndoRedo.Exec(new BindU8(this, 0x16, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("17 Unknown (Always 00)")]
        [Description("Unknown")]
        public byte Unknown_17 {
            get { return RamDisk.GetU8(GetPos()+0x17); }
            set { UndoRedo.Exec(new BindU8(this, 0x17, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("18 Unknown (Always 00)")]
        [Description("Unknown")]
        public byte Unknown_18 {
            get { return RamDisk.GetU8(GetPos()+0x18); }
            set { UndoRedo.Exec(new BindU8(this, 0x18, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("19 Unknown (Always 00)")]
        [Description("Unknown")]
        public byte Unknown_19 {
            get { return RamDisk.GetU8(GetPos()+0x19); }
            set { UndoRedo.Exec(new BindU8(this, 0x19, value)); }
        }

        [ReadOnly(true)]
        [Category("01 Enemy")]
        [DisplayName("1A Always Drop 1 Raw")]
        [Description("Item name of drop 1")]
        public ushort AlwaysDrop1Raw_1A {
            get { return RamDisk.GetU16(GetPos()+0x1A); }
            set { UndoRedo.Exec(new BindU16(this, 0x1A, value)); }
        }
        [Category("01 Enemy")]
        [DisplayName("1A Always Drop 1")]
        [Description("Item name of drop 1")]
        [DefaultValue("")]
        [TypeConverter(typeof(ItemNamesListDropDown))]
        public string AlwaysDrop1_1A {
            get {
                short index = RamDisk.GetS16(GetPos()+0x1A);
                return Model.itemnames.GetName(index);
            }
            set {
                short index = (short)Model.itemnames.GetIndexByName(value);
                UndoRedo.Exec(new BindS16(this, 0x1A, index));
            }
        }

        [ReadOnly(true)]
        [Category("01 Enemy")]
        [DisplayName("1C Always Drop 2 Raw")]
        [Description("Item name of drop 2")]
        public ushort AlwaysDrop2Raw_1C {
            get { return RamDisk.GetU16(GetPos()+0x1C); }
            set { UndoRedo.Exec(new BindU16(this, 0x1C, value)); }
        }
        [Category("01 Enemy")]
        [DisplayName("1C Always Drop 2")]
        [Description("Item name of drop 2")]
        [DefaultValue("")]
        [TypeConverter(typeof(ItemNamesListDropDown))]
        public string AlwaysDrop2_1C {
            get {
                short index = RamDisk.GetS16(GetPos()+0x1C);
                return Model.itemnames.GetName(index);
            }
            set {
                short index = (short)Model.itemnames.GetIndexByName(value);
                UndoRedo.Exec(new BindS16(this, 0x1C, index));
            }
        }

        [ReadOnly(true)]
        [Category("01 Enemy")]
        [DisplayName("1E Random Drop Raw")]
        [Description("Item name of random drop")]
        public ushort RandomDropRaw_1E {
            get { return RamDisk.GetU16(GetPos()+0x1E); }
            set { UndoRedo.Exec(new BindU16(this, 0x1E, value)); }
        }
        [Category("01 Enemy")]
        [DisplayName("1E Random Drop")]
        [Description("Item name of random drop")]
        [DefaultValue("")]
        [TypeConverter(typeof(ItemNamesListDropDown))]
        public string RandomDrop_1E {
            get {
                short index = RamDisk.GetS16(GetPos()+0x1E);
                return Model.itemnames.GetName(index);
            }
            set {
                short index = (short)Model.itemnames.GetIndexByName(value);
                UndoRedo.Exec(new BindS16(this, 0x1E, index));
            }
        }

        [Category("01 Enemy")]
        [DisplayName("20 Always Drop 1 Qty")]
        [Description("Quantity of 1st drop")]
        public byte AlwaysDrop1Qty_20 {
            get { return RamDisk.GetU8(GetPos()+0x20); }
            set { UndoRedo.Exec(new BindU8(this, 0x20, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("21 Always Drop 2 Qty")]
        [Description("Quantity of 2nd drop")]
        public byte AlwaysDrop2Qty_21 {
            get { return RamDisk.GetU8(GetPos()+0x21); }
            set { UndoRedo.Exec(new BindU8(this, 0x21, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("22 Random Drop %")]
        [Description("Drop % for the random drop")]
        public byte RandomDropPercent_22 {
            get { return RamDisk.GetU8(GetPos()+0x22); }
            set { UndoRedo.Exec(new BindU8(this, 0x22, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("23 Unknown")]
        [Description("Unknown")]
        public byte Unknown_23 {
            get { return RamDisk.GetU8(GetPos()+0x23); }
            set { UndoRedo.Exec(new BindU8(this, 0x23, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("24 Major Boss")]
        [Description("Always 0 for normal and minor bosses")]
        public byte MajorBoss_24 {
            get { return RamDisk.GetU8(GetPos()+0x24); }
            set { UndoRedo.Exec(new BindU8(this, 0x24, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("25 Model Texture")]
        [Description("Some enemies have multiple textures like blood lizards")]
        public byte ModelTexture_25 {
            get { return RamDisk.GetU8(GetPos()+0x25); }
            set { UndoRedo.Exec(new BindU8(this, 0x25, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("26 Initial State")]
        [Description("Can be sleeping / playing dead")]
        public byte InitialState_26 {
            get { return RamDisk.GetU8(GetPos()+0x26); }
            set { UndoRedo.Exec(new BindU8(this, 0x26, value)); }
        }

        [Category("01 Enemy")]
        [DisplayName("27 Unknown")]
        [Description("Unknown")]
        public byte Unknown_27 {
            get { return RamDisk.GetU8(GetPos()+0x27); }
            set { UndoRedo.Exec(new BindU8(this, 0x27, value)); }
        }
    }
}
