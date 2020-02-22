using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GodHands {
    public class Skill : InMemory {
        public Skill(BaseClass parent, string url, int pos, Record rec):
        base(parent, url, pos, rec) {
        }

        public override int GetLen() {
            return 0x34;
        }

        [ReadOnly(true)]
        [Category("01 Skill")]
        [DisplayName("Skills List ID")]
        [Description("Index into the skills list")]
        public byte SkillsListID {
            get { return RamDisk.GetU8(GetPos()+0x00); }
            set { UndoRedo.Exec(new BindU8(this, 0x00, value)); }
        }

        [ReadOnly(true)]
        [Category("01 Skill")]
        [DisplayName("Purge List ID")]
        [Description("Index into the EFFPURGE list")]
        public byte PurgeListID {
            get { return RamDisk.GetU8(GetPos()+0x01); }
            set { UndoRedo.Exec(new BindU8(this, 0x01, value)); }
        }

        [ReadOnly(true)]
        [Category("01 Skill")]
        [DisplayName("Skill Type")]
        [Description("Main flags for skill")]
        public byte SkillType {
            get { return RamDisk.GetU8(GetPos()+0x02); }
            set { UndoRedo.Exec(new BindU8(this, 0x02, value)); }
        }

        [Category("01 Skill")]
        [DisplayName("Unknown Bit Flag")]
        [Description("Unknown")]
        public bool UnknownBitFlag {
            get {
                return ((SkillType & 1) != 0);
            }
            set {
                byte flags = SkillType;
                SkillType = (value) ? (byte)(flags | 1) : (byte)(flags & ~1);
            }
        }

        [Category("01 Skill")]
        [DisplayName("Skill Category")]
        [Description("Skill category")]
        [DefaultValue("")]
        [TypeConverter(typeof(CategorySkillsDropDown))]
        public string SkillCategory {
            get {
                int index = (SkillType/2) & 7;
                return Model.category_skills.GetName(index);
            }
            set {
                byte index = (byte)Model.category_skills.GetIndexByName(value);
                byte flags = SkillType;
                SkillType = (byte)((flags & ~0x0E) | (index*2));
            }
        }

        [Category("01 Skill")]
        [DisplayName("Target Params")]
        [Description("This skill is a special attack")]
        public byte TargetParams {
            get {
                byte flags = (byte)(SkillType / 16);
                return flags;
            }
            set {
                byte flags = SkillType;
                byte val = (byte)(value % 16);
                SkillType = (byte)((flags & 15) | (val*16));
            }
        }

        [Category("01 Skill")]
        [DisplayName("Cost value")]
        [Description("The cost of using this skill")]
        public byte CostValue {
            get { return RamDisk.GetU8(GetPos()+0x03); }
            set { UndoRedo.Exec(new BindU8(this, 0x03, value)); }
        }

        [Category("02 Range")]
        [DisplayName("Target RangeX")]
        [Description("Range horizontal")]
        public byte TargetRangeX {
            get { return RamDisk.GetU8(GetPos()+0x04); }
            set { UndoRedo.Exec(new BindU8(this, 0x04, value)); }
        }

        [Category("02 Range")]
        [DisplayName("Target RangeY")]
        [Description("Range vertical")]
        public byte TargetRangeY {
            get { return RamDisk.GetU8(GetPos()+0x05); }
            set { UndoRedo.Exec(new BindU8(this, 0x05, value)); }
        }

        [Category("02 Range")]
        [DisplayName("Target RangeZ")]
        [Description("Range depth")]
        public byte TargetRangeZ {
            get { return RamDisk.GetU8(GetPos()+0x06); }
            set { UndoRedo.Exec(new BindU8(this, 0x06, value)); }
        }

        [ReadOnly(true)]
        [Category("02 Range")]
        [DisplayName("Target Sphere Raw")]
        [Description("Target sphere shape")]
        public byte TargetSphereRaw {
            get { return RamDisk.GetU8(GetPos()+0x07); }
            set { UndoRedo.Exec(new BindU8(this, 0x07, value)); }
        }

        [Category("02 Range")]
        [DisplayName("Target Sphere")]
        [Description("Target sphere shape")]
        [DefaultValue("")]
        [TypeConverter(typeof(TargetSphereDropDown))]
        public string TargetSphere {
            get {
                byte index = (byte)(RamDisk.GetU8(GetPos()+0x07) % 8);
                return Model.targets.GetName(index);
            }
            set {
                byte val = (byte)(RamDisk.GetU8(GetPos()+0x07) & ~7);
                byte index = (byte)(Model.targets.GetIndexByName(value) % 8);
                UndoRedo.Exec(new BindU8(this, 0x07, (byte)(val | index)));
            }
        }

        [Category("02 Range")]
        [DisplayName("Target Angle")]
        [Description("Angle of target sphere")]
        public byte TargetAngle {
            get {
                byte val = RamDisk.GetU8(GetPos()+0x07);
                return (byte)(val/8);
            }
            set {
                byte val = RamDisk.GetU8(GetPos()+0x07);
                val = (byte)((val%8) | (4*(value % 32)));
                UndoRedo.Exec(new BindU8(this, 0x07, val));
            }
        }

        [Category("03 Area")]
        [DisplayName("Area RangeX")]
        [Description("Area range horizontal")]
        public byte AreaRangeX {
            get { return RamDisk.GetU8(GetPos()+0x08); }
            set { UndoRedo.Exec(new BindU8(this, 0x08, value)); }
        }

        [Category("03 Area")]
        [DisplayName("Area RangeY")]
        [Description("Area range vertical")]
        public byte AreaRangeY {
            get { return RamDisk.GetU8(GetPos()+0x09); }
            set { UndoRedo.Exec(new BindU8(this, 0x09, value)); }
        }

        [Category("03 Area")]
        [DisplayName("Area RangeZ")]
        [Description("Area range depth")]
        public byte AreaRangeZ {
            get { return RamDisk.GetU8(GetPos()+0x0A); }
            set { UndoRedo.Exec(new BindU8(this, 0x0A, value)); }
        }

        [ReadOnly(true)]
        [Category("03 Area")]
        [DisplayName("Area Sphere Raw")]
        [Description("Area sphere shape")]
        public byte AreaSphereRaw {
            get { return RamDisk.GetU8(GetPos()+0x0B); }
            set { UndoRedo.Exec(new BindU8(this, 0x0B, value)); }
        }

        [Category("03 Area")]
        [DisplayName("Area Sphere")]
        [Description("Area sphere shape")]
        [DefaultValue("")]
        [TypeConverter(typeof(TargetSphereDropDown))]
        public string AreaSphere {
            get {
                byte index = (byte)(RamDisk.GetU8(GetPos()+0x0B) % 8);
                return Model.targets.GetName(index);
            }
            set {
                byte val = (byte)(RamDisk.GetU8(GetPos()+0x0B) & ~7);
                byte index = (byte)(Model.targets.GetIndexByName(value) % 8);
                UndoRedo.Exec(new BindU8(this, 0x0B, (byte)(val | index)));
            }
        }

        [Category("03 Area")]
        [DisplayName("Area Angle")]
        [Description("Angle of target sphere")]
        public byte AreaAngle {
            get {
                byte val = RamDisk.GetU8(GetPos()+0x0B);
                return (byte)(val/8);
            }
            set {
                byte val = RamDisk.GetU8(GetPos()+0x0B);
                val = (byte)((val%8) | (4*(value % 32)));
                UndoRedo.Exec(new BindU8(this, 0x0B, val));
            }
        }

        [Category("04 Misc")]
        [DisplayName("WT")]
        [Description("Unknown")]
        public byte WT {
            get { return RamDisk.GetU8(GetPos()+0x0C); }
            set { UndoRedo.Exec(new BindU8(this, 0x0C, value)); }
        }

        [Category("04 Misc")]
        [DisplayName("Unknown1")]
        [Description("Unknown")]
        public byte Unknown1 {
            get { return RamDisk.GetU8(GetPos()+0x0D); }
            set { UndoRedo.Exec(new BindU8(this, 0x0D, value)); }
        }

        [Category("04 Misc")]
        [DisplayName("Unknown2")]
        [Description("Unknown")]
        public byte Unknown2 {
            get { return RamDisk.GetU8(GetPos()+0x0E); }
            set { UndoRedo.Exec(new BindU8(this, 0x0E, value)); }
        }

        [Category("04 Misc")]
        [DisplayName("Unknown3")]
        [Description("Unknown")]
        public byte Unknown3 {
            get { return RamDisk.GetU8(GetPos()+0x0F); }
            set { UndoRedo.Exec(new BindU8(this, 0x0F, value)); }
        }

        [Category("04 Misc")]
        [DisplayName("Unknown4")]
        [Description("Unknown")]
        public byte Unknown4 {
            get { return RamDisk.GetU8(GetPos()+0x10); }
            set { UndoRedo.Exec(new BindU8(this, 0x10, value)); }
        }

        [Category("04 Misc")]
        [DisplayName("Unknown5")]
        [Description("Unknown")]
        public byte Unknown5 {
            get { return RamDisk.GetU8(GetPos()+0x11); }
            set { UndoRedo.Exec(new BindU8(this, 0x11, value)); }
        }

        [Category("04 Misc")]
        [DisplayName("Unknown6")]
        [Description("Unknown")]
        public byte Unknown6 {
            get { return RamDisk.GetU8(GetPos()+0x12); }
            set { UndoRedo.Exec(new BindU8(this, 0x12, value)); }
        }

        [Category("04 Misc")]
        [DisplayName("Unknown7")]
        [Description("Unknown")]
        public byte Unknown7 {
            get { return RamDisk.GetU8(GetPos()+0x13); }
            set { UndoRedo.Exec(new BindU8(this, 0x13, value)); }
        }

        [Category("04 Misc")]
        [DisplayName("Is Learnt")]
        [Description("Unknown")]
        public bool IsLearnt {
            get {
                byte val = Unknown1;
                return ((val & 0x80) == 0x80);
            }
            set {
                byte val = Unknown1;
                val = (value) ? (byte)(val|0x80) : (byte)(val&0x7F);
                Unknown1 = val;
            }
        }

        [Category("05 First Hit")]
        [DisplayName("Skill Effect 1")]
        [Description("Unknown")]
        public byte SkillEffect1 {
            get {
                byte val = RamDisk.GetU8(GetPos()+0x14);
                return (byte)(val & 0x7F);
            }
            set {
                byte val = RamDisk.GetU8(GetPos()+0x14);
                val = (byte)((val & 0x80) | (value & 0x7F));
                UndoRedo.Exec(new BindU8(this, 0x14, val));
            }
        }

        [Category("05 First Hit")]
        [DisplayName("HitRate PreReqs 1")]
        [Description("Unknown")]
        public byte HitRatePreReqs1 {
            get {
                ushort val = RamDisk.GetU16(GetPos()+0x14);
                val = (byte)(val & 0x1FF8);
                return (byte)(val/8);
            }
            set {
                ushort val = RamDisk.GetU16(GetPos()+0x14);
                val = (byte)(val & 0xE007);
                value = (byte)((value*8) & 0x1FF8);
                val = (byte)(val | value);
                UndoRedo.Exec(new BindU16(this, 0x14, val));
            }
        }

        [Category("05 First Hit")]
        [DisplayName("HitRate Calc 1")]
        [Description("Unknown")]
        public byte HitRateCalc1 {
            get {
                ushort val = RamDisk.GetU16(GetPos()+0x14);
                val = (byte)(val & 0xE000);
                return (byte)(val/4096);
            }
            set {
                ushort val = RamDisk.GetU16(GetPos()+0x14);
                val = (byte)(val & 0x1FFF);
                value = (byte)((value*4096) & 0xE000);
                val = (byte)(val | value);
                UndoRedo.Exec(new BindU16(this, 0x14, val));
            }
        }

        [Category("05 First Hit")]
        [DisplayName("Skill Damage 1")]
        [Description("Unknown")]
        public byte SkillDamage1 {
            get {
                ushort val = RamDisk.GetU16(GetPos()+0x16);
                return (byte)(val & 0x003F);
            }
            set {
                ushort val = RamDisk.GetU16(GetPos()+0x16);
                val = (byte)(val & 0xFF40);
                value = (byte)(value*0x3F);
                val = (byte)(val | value);
                UndoRedo.Exec(new BindU16(this, 0x16, val));
            }
        }

        [Category("05 First Hit")]
        [DisplayName("Attack Multiplier 1")]
        [Description("Unknown")]
        public byte AttackMultiplier1 {
            get {
                ushort val = RamDisk.GetU16(GetPos()+0x16);
                return (byte)((val/0x40) & 0x001F);
            }
            set {
                ushort val = RamDisk.GetU16(GetPos()+0x16);
                val = (byte)(val & 0xF03F);
                value = (byte)((value*0x40) & 0x0F80);
                val = (byte)(val | value);
                UndoRedo.Exec(new BindU16(this, 0x16, val));
            }
        }

        [Category("05 First Hit")]
        [DisplayName("Damage Type 1")]
        [Description("Unknown")]
        public byte DamageType1 {
            get {
                ushort val = RamDisk.GetU16(GetPos()+0x16);
                return (byte)((val/0x1000) & 0x0F);
            }
            set {
                ushort val = RamDisk.GetU16(GetPos()+0x16);
                val = (byte)(val & 0x0FFF);
                value = (byte)((value*0x1000)*0xF000);
                val = (byte)(val | value);
                UndoRedo.Exec(new BindU16(this, 0x16, val));
            }
        }

        [Category("05 First Hit")]
        [DisplayName("Damage Affinity 1")]
        [Description("Unknown")]
        public byte DamageAffinity1 {
            get {
                ushort val = RamDisk.GetU16(GetPos()+0x16);
                return (byte)((val/0x2000) & 0x07);
            }
            set {
                ushort val = RamDisk.GetU16(GetPos()+0x16);
                val = (byte)(val & 0x1FFF);
                value = (byte)((value*0x2000)*0xE000);
                val = (byte)(val | value);
                UndoRedo.Exec(new BindU16(this, 0x16, val));
            }
        }

        [Category("06 Second Hit")]
        [DisplayName("Skill Effect 2")]
        [Description("Unknown")]
        public byte SkillEffect2 {
            get {
                byte val = RamDisk.GetU8(GetPos()+0x16);
                return (byte)(val & 0x7F);
            }
            set {
                byte val = RamDisk.GetU8(GetPos()+0x16);
                val = (byte)((val & 0x80) | (value & 0x7F));
                UndoRedo.Exec(new BindU8(this, 0x16, val));
            }
        }

        [Category("06 Second Hit")]
        [DisplayName("HitRate PreReqs 2")]
        [Description("Unknown")]
        public byte HitRatePreReqs2 {
            get {
                ushort val = RamDisk.GetU16(GetPos()+0x16);
                val = (byte)(val & 0x1FF8);
                return (byte)(val/8);
            }
            set {
                ushort val = RamDisk.GetU16(GetPos()+0x16);
                val = (byte)(val & 0xE007);
                value = (byte)((value*8) & 0x1FF8);
                val = (byte)(val | value);
                UndoRedo.Exec(new BindU16(this, 0x16, val));
            }
        }

        [Category("06 Second Hit")]
        [DisplayName("HitRate Calc 2")]
        [Description("Unknown")]
        public byte HitRateCalc2 {
            get {
                ushort val = RamDisk.GetU16(GetPos()+0x16);
                val = (byte)(val & 0xE000);
                return (byte)(val/4096);
            }
            set {
                ushort val = RamDisk.GetU16(GetPos()+0x16);
                val = (byte)(val & 0x1FFF);
                value = (byte)((value*4096) & 0xE000);
                val = (byte)(val | value);
                UndoRedo.Exec(new BindU16(this, 0x16, val));
            }
        }

        [Category("06 Second Hit")]
        [DisplayName("Skill Damage 2")]
        [Description("Unknown")]
        public byte SkillDamage2 {
            get {
                ushort val = RamDisk.GetU16(GetPos()+0x18);
                return (byte)(val & 0x003F);
            }
            set {
                ushort val = RamDisk.GetU16(GetPos()+0x18);
                val = (byte)(val & 0xFF40);
                value = (byte)(value*0x3F);
                val = (byte)(val | value);
                UndoRedo.Exec(new BindU16(this, 0x18, val));
            }
        }

        [Category("06 Second Hit")]
        [DisplayName("Attack Multiplier 2")]
        [Description("Unknown")]
        public byte AttackMultiplier2 {
            get {
                ushort val = RamDisk.GetU16(GetPos()+0x18);
                return (byte)((val/0x40) & 0x001F);
            }
            set {
                ushort val = RamDisk.GetU16(GetPos()+0x18);
                val = (byte)(val & 0xF03F);
                value = (byte)((value*0x40) & 0x0F80);
                val = (byte)(val | value);
                UndoRedo.Exec(new BindU16(this, 0x18, val));
            }
        }

        [Category("06 Second Hit")]
        [DisplayName("Damage Type 2")]
        [Description("Unknown")]
        public byte DamageType2 {
            get {
                ushort val = RamDisk.GetU16(GetPos()+0x18);
                return (byte)((val/0x1000) & 0x0F);
            }
            set {
                ushort val = RamDisk.GetU16(GetPos()+0x18);
                val = (byte)(val & 0x0FFF);
                value = (byte)((value*0x1000)*0xF000);
                val = (byte)(val | value);
                UndoRedo.Exec(new BindU16(this, 0x18, val));
            }
        }

        [Category("06 Second Hit")]
        [DisplayName("Damage Affinity 2")]
        [Description("Unknown")]
        public byte DamageAffinity2 {
            get {
                ushort val = RamDisk.GetU16(GetPos()+0x18);
                return (byte)((val/0x2000) & 0x07);
            }
            set {
                ushort val = RamDisk.GetU16(GetPos()+0x18);
                val = (byte)(val & 0x1FFF);
                value = (byte)((value*0x2000)*0xE000);
                val = (byte)(val | value);
                UndoRedo.Exec(new BindU16(this, 0x18, val));
            }
        }

        [Category("01 Skill")]
        [DisplayName("Name")]
        [Description("Name of the skill (max 24 letters)")]
        public string Name {
            get {
                byte[] kildean = new byte[0x18];
                RamDisk.Get(GetPos()+0x1C, 0x18, kildean);
                return Kildean.ToAscii(kildean);
            }
            set {
                string clip = value.Substring(0, Math.Min(0x18, value.Length));
                byte[] kildean = Kildean.ToKildean(clip, 0x18);
                UndoRedo.Exec(new BindArray(this, GetPos()+0x1C, 0x18, kildean));
            }
        }
    }
}
