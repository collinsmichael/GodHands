using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GodHands {
    public class Skill : InMemory {
        private List<string> categories = new List<string> {
            "Nothing",
            "Spell (costs MP)",
            "Battle Ability (costs RISK)",
            "Break Art (costs HP)",
            "Unused (costs PP)",
            "Item Effect (costs nothing)",
            "Normal Effect (costs depends on weapon)",
            "Trap"
        };

        public Skill(string url, int pos, DirRec rec):
        base(url, pos, rec) {
        }

        [ReadOnly(true)]
        [Category("Stats")]
        [DisplayName("Skills List ID")]
        [Description("Index into the skills list")]
        public byte SkillsListID {
            get { return RamDisk.GetU8(GetPos()+0x00); }
            set { UndoRedo.Exec(new BindU8(this, 0x00, value)); }
        }

        [ReadOnly(true)]
        [Category("Stats")]
        [DisplayName("Purge List ID")]
        [Description("Index into the EFFPURGE list")]
        public byte PurgeListID {
            get { return RamDisk.GetU8(GetPos()+0x01); }
            set { UndoRedo.Exec(new BindU8(this, 0x01, value)); }
        }

        [ReadOnly(true)]
        [Category("Skill")]
        [DisplayName("Skill Type")]
        [Description("Main flags for skill")]
        public byte SkillType {
            get { return RamDisk.GetU8(GetPos()+0x02); }
            set { UndoRedo.Exec(new BindU8(this, 0x02, value)); }
        }

        [Category("Skill")]
        [DisplayName("Skill Type Unknown")]
        [Description("Unknown")]
        public bool Unknown {
            get {
                return ((SkillType & 1) != 0);
            }
            set {
                byte flags = SkillType;
                SkillType = (value) ? (byte)(flags | 1) : (byte)(flags & ~1);
            }
        }

        [Category("Skill")]
        [DisplayName("Skill Category")]
        [Description("The skill category")]
        public string SkillCategory {
            get {
                int index = (SkillType/2) & 7;
                return categories[index];
            }
            set {
                int index = categories.IndexOf(value);
                byte flags = SkillType;
                SkillType = (byte)((flags & ~0x0E) | (index*2));
            }
        }

        [Category("Skill")]
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

        [Category("Skill")]
        [DisplayName("Cost value")]
        [Description("The cost of using this skill")]
        public byte CostValue {
            get { return RamDisk.GetU8(GetPos()+0x03); }
            set { UndoRedo.Exec(new BindU8(this, 0x03, value)); }
        }

        [Category("Skill")]
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
                UndoRedo.Exec(new BindArray(this, 0x1C, 0x18, kildean));
            }
        }
    }
}
