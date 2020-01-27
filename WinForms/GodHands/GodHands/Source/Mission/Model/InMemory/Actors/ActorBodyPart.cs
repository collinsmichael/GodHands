using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GodHands {
    public class ActorBodyPart : InMemory {
        public ActorBodyPart(string url, int pos, DirRec rec):
        base(url, pos, rec) {
        }

        public override int GetLen() {
            return 0x5C;
        }

        [Category("01 Stats")]
        [DisplayName("HP")]
        [Description("Health points (depleted when taking damage)")]
        public short HP {
            get { return RamDisk.GetS16(GetPos()+0x00); }
            set { UndoRedo.Exec(new BindS16(this, 0x00, value)); }
        }

        [Category("01 Stats")]
        [DisplayName("AGL")]
        [Description("Agility defensive bonus")]
        public byte AGL {
            get { return RamDisk.GetU8(GetPos()+0x02); }
            set { UndoRedo.Exec(new BindU8(this, 0x02, value)); }
        }

        [Category("01 Stats")]
        [DisplayName("EVD")]
        [Description("Chain evasion bonus")]
        public byte EVD {
            get { return RamDisk.GetU8(GetPos()+0x03); }
            set { UndoRedo.Exec(new BindU8(this, 0x03, value)); }
        }

        [Category("02 Types")]
        [DisplayName("Blunt")]
        [Description("Resistence to blunt attacks")]
        public sbyte Blunt {
            get { return RamDisk.GetS8(GetPos()+0x05); }
            set { UndoRedo.Exec(new BindS8(this, 0x05, value)); }
        }

        [Category("02 Types")]
        [DisplayName("Edged")]
        [Description("Resistence to edged attacks")]
        public sbyte Edged {
            get { return RamDisk.GetS8(GetPos()+0x06); }
            set { UndoRedo.Exec(new BindS8(this, 0x06, value)); }
        }

        [Category("02 Types")]
        [DisplayName("Piercing")]
        [Description("Resistence to piercing attacks")]
        public sbyte Piercing {
            get { return RamDisk.GetS8(GetPos()+0x07); }
            set { UndoRedo.Exec(new BindS8(this, 0x07, value)); }
        }

        [Category("02 Types")]
        [DisplayName("Padding")]
        [Description("Unused")]
        public sbyte TypePadding {
            get { return RamDisk.GetS8(GetPos()+0x04); }
            set { UndoRedo.Exec(new BindS8(this, 0x04, value)); }
        }

        [Category("03 Affinities")]
        [DisplayName("Earth")]
        [Description("Earth elemental affinity")]
        public sbyte Earth {
            get { return RamDisk.GetS8(GetPos()+0x08); }
            set { UndoRedo.Exec(new BindS8(this, 0x08, value)); }
        }

        [Category("03 Affinities")]
        [DisplayName("Air")]
        [Description("Air elemental affinity")]
        public sbyte Air {
            get { return RamDisk.GetS8(GetPos()+0x09); }
            set { UndoRedo.Exec(new BindS8(this, 0x09, value)); }
        }

        [Category("03 Affinities")]
        [DisplayName("Fire")]
        [Description("fire elemental affinity")]
        public sbyte Fire {
            get { return RamDisk.GetS8(GetPos()+0x0A); }
            set { UndoRedo.Exec(new BindS8(this, 0x0A, value)); }
        }

        [Category("03 Affinities")]
        [DisplayName("Water")]
        [Description("Water elemental affinity")]
        public sbyte Water {
            get { return RamDisk.GetS8(GetPos()+0x0B); }
            set { UndoRedo.Exec(new BindS8(this, 0x0B, value)); }
        }

        [Category("03 Affinities")]
        [DisplayName("Physical")]
        [Description("Physical affinity")]
        public sbyte Physical {
            get { return RamDisk.GetS8(GetPos()+0x0C); }
            set { UndoRedo.Exec(new BindS8(this, 0x0C, value)); }
        }

        [Category("03 Affinities")]
        [DisplayName("Light")]
        [Description("Light affinity")]
        public sbyte Light {
            get { return RamDisk.GetS8(GetPos()+0x0D); }
            set { UndoRedo.Exec(new BindS8(this, 0x0D, value)); }
        }

        [Category("03 Affinities")]
        [DisplayName("Dark")]
        [Description("Dark affinity")]
        public sbyte Dark {
            get { return RamDisk.GetS8(GetPos()+0x0E); }
            set { UndoRedo.Exec(new BindS8(this, 0x0E, value)); }
        }

        [Category("03 Affinities")]
        [DisplayName("Padding")]
        [Description("Unused")]
        public sbyte AffinityPadding {
            get { return RamDisk.GetS8(GetPos()+0x0F); }
            set { UndoRedo.Exec(new BindS8(this, 0x0F, value)); }
        }

        [Category("04 Skills")]
        [DisplayName("Skill_0")]
        [Description("Skills which can be used")]
        [DefaultValue("")]
        [TypeConverter(typeof(SkillsDropDown))]
        public string Skill_0 {
            get {
                byte index = RamDisk.GetU8(GetPos()+0x10);
                return Model.skills.GetName(index);
            }
            set {
                byte index = (byte)Model.skills.GetIndexByName(value);
                UndoRedo.Exec(new BindU8(this, 0x10, index));
            }
        }

        [Category("04 Skills")]
        [DisplayName("Skill_0_Unknown1")]
        [Description("Unknown")]
        public byte Skill_0_Unknown1 {
            get { return RamDisk.GetU8(GetPos()+0x11); }
            set { UndoRedo.Exec(new BindU8(this, 0x11, value)); }
        }

        [Category("04 Skills")]
        [DisplayName("Skill_0_Unknown2")]
        [Description("Unknown")]
        public byte Skill_0_Unknown2 {
            get { return RamDisk.GetU8(GetPos()+0x12); }
            set { UndoRedo.Exec(new BindU8(this, 0x12, value)); }
        }

        [Category("04 Skills")]
        [DisplayName("Skill_0_LocalID")]
        [Description("Local Skill ID (0-3)")]
        public byte Skill_0_LocalID {
            get { return RamDisk.GetU8(GetPos()+0x13); }
            set { UndoRedo.Exec(new BindU8(this, 0x13, value)); }
        }

        [Category("04 Skills")]
        [DisplayName("Skill_1")]
        [Description("Skills which can be used")]
        public string Skill_1 {
            get {
                byte index = RamDisk.GetU8(GetPos()+0x14);
                return Model.skills.GetName(index);
            }
            set {
                byte index = (byte)Model.skills.GetIndexByName(value);
                UndoRedo.Exec(new BindU8(this, 0x14, index));
            }
        }

        [Category("04 Skills")]
        [DisplayName("Skill_1_Unknown1")]
        [Description("Unknown")]
        public byte Skill_1_Unknown1 {
            get { return RamDisk.GetU8(GetPos()+0x15); }
            set { UndoRedo.Exec(new BindU8(this, 0x15, value)); }
        }

        [Category("04 Skills")]
        [DisplayName("Skill_1_Unknown2")]
        [Description("Unknown")]
        public byte Skill_1_Unknown2 {
            get { return RamDisk.GetU8(GetPos()+0x16); }
            set { UndoRedo.Exec(new BindU8(this, 0x16, value)); }
        }

        [Category("04 Skills")]
        [DisplayName("Skill_1_LocalID")]
        [Description("Local Skill ID (0-3)")]
        public byte Skill_1_LocalID {
            get { return RamDisk.GetU8(GetPos()+0x17); }
            set { UndoRedo.Exec(new BindU8(this, 0x17, value)); }
        }

        [Category("04 Skills")]
        [DisplayName("Skill_2")]
        [Description("Skills which can be used")]
        public string Skill_2 {
            get {
                byte index = RamDisk.GetU8(GetPos()+0x18);
                return Model.skills.GetName(index);
            }
            set {
                byte index = (byte)Model.skills.GetIndexByName(value);
                UndoRedo.Exec(new BindU8(this, 0x18, index));
            }
        }

        [Category("04 Skills")]
        [DisplayName("Skill_2_Unknown1")]
        [Description("Unknown")]
        public byte Skill_2_Unknown1 {
            get { return RamDisk.GetU8(GetPos()+0x19); }
            set { UndoRedo.Exec(new BindU8(this, 0x19, value)); }
        }

        [Category("04 Skills")]
        [DisplayName("Skill_2_Unknown2")]
        [Description("Unknown")]
        public byte Skill_2_Unknown2 {
            get { return RamDisk.GetU8(GetPos()+0x1A); }
            set { UndoRedo.Exec(new BindU8(this, 0x1A, value)); }
        }

        [Category("04 Skills")]
        [DisplayName("Skill_2_LocalID")]
        [Description("Local Skill ID (0-3)")]
        public byte Skill_2_LocalID {
            get { return RamDisk.GetU8(GetPos()+0x1B); }
            set { UndoRedo.Exec(new BindU8(this, 0x1B, value)); }
        }

        [Category("04 Skills")]
        [DisplayName("Skill_3")]
        [Description("Skills which can be used")]
        public string Skill_3 {
            get {
                byte index = RamDisk.GetU8(GetPos()+0x1C);
                return Model.skills.GetName(index);
            }
            set {
                byte index = (byte)Model.skills.GetIndexByName(value);
                UndoRedo.Exec(new BindU8(this, 0x1C, index));
            }
        }

        [Category("04 Skills")]
        [DisplayName("Skill_3_Unknown1")]
        [Description("Unknown")]
        public byte Skill_3_Unknown1 {
            get { return RamDisk.GetU8(GetPos()+0x1D); }
            set { UndoRedo.Exec(new BindU8(this, 0x1D, value)); }
        }

        [Category("04 Skills")]
        [DisplayName("Skill_3_Unknown2")]
        [Description("Unknown")]
        public byte Skill_3_Unknown2 {
            get { return RamDisk.GetU8(GetPos()+0x1E); }
            set { UndoRedo.Exec(new BindU8(this, 0x1E, value)); }
        }

        [Category("04 Skills")]
        [DisplayName("Skill_3_LocalID")]
        [Description("Local Skill ID (0-3)")]
        public byte Skill_3_LocalID {
            get { return RamDisk.GetU8(GetPos()+0x1F); }
            set { UndoRedo.Exec(new BindU8(this, 0x1F, value)); }
        }

        [Category("05 Damage")]
        [DisplayName("DamageToPart0")]
        [Description("Damage distribution to other bodyparts")]
        public byte DamageToPart0 {
            get { return RamDisk.GetU8(GetPos()+0x54); }
            set { UndoRedo.Exec(new BindU8(this, 0x54, value)); }
        }

        [Category("05 Damage")]
        [DisplayName("DamageToPart1")]
        [Description("Damage distribution to other bodyparts")]
        public byte DamageToPart1 {
            get { return RamDisk.GetU8(GetPos()+0x55); }
            set { UndoRedo.Exec(new BindU8(this, 0x55, value)); }
        }

        [Category("05 Damage")]
        [DisplayName("DamageToPart2")]
        [Description("Damage distribution to other bodyparts")]
        public byte DamageToPart2 {
            get { return RamDisk.GetU8(GetPos()+0x56); }
            set { UndoRedo.Exec(new BindU8(this, 0x56, value)); }
        }

        [Category("05 Damage")]
        [DisplayName("DamageToPart3")]
        [Description("Damage distribution to other bodyparts")]
        public byte DamageToPart3 {
            get { return RamDisk.GetU8(GetPos()+0x57); }
            set { UndoRedo.Exec(new BindU8(this, 0x57, value)); }
        }

        [Category("05 Damage")]
        [DisplayName("DamageToPart4")]
        [Description("Damage distribution to other bodyparts")]
        public byte DamageToPart4 {
            get { return RamDisk.GetU8(GetPos()+0x58); }
            set { UndoRedo.Exec(new BindU8(this, 0x58, value)); }
        }

        [Category("05 Damage")]
        [DisplayName("DamageToPart5")]
        [Description("Damage distribution to other bodyparts")]
        public byte DamageToPart5 {
            get { return RamDisk.GetU8(GetPos()+0x59); }
            set { UndoRedo.Exec(new BindU8(this, 0x59, value)); }
        }

        [Category("05 Damage")]
        [DisplayName("DamagePadding1")]
        [Description("Unused")]
        public byte DamagePadding1 {
            get { return RamDisk.GetU8(GetPos()+0x5A); }
            set { UndoRedo.Exec(new BindU8(this, 0x5A, value)); }
        }

        [Category("05 Damage")]
        [DisplayName("DamagePadding2")]
        [Description("Unused")]
        public byte DamagePadding2 {
            get { return RamDisk.GetU8(GetPos()+0x5B); }
            set { UndoRedo.Exec(new BindU8(this, 0x5B, value)); }
        }
    }
}
