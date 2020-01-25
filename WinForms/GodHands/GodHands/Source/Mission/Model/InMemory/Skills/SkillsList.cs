using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class SkillsList {
        private List<Skill> skills = new List<Skill>();

        public bool Clear() {
            skills.Clear();
            return true;
        }

        public bool Load() {
            skills.Clear();
            DirRec slus = Model.GetRec("SLUS");
            if (slus != null) {
                int pos = 0x0003C1DC;
                byte[] buf = new byte[0x34];
                for (int i = 0; i < 256; i++) {
                    string key = "SLUS/Skills/Skill_"+i;
                    Skill skill = new Skill(key, pos + i*0x34, slus); 
                    skills.Add(skill);
                }
            }
            return true;
        }

        public List<string> GetSkillNames() {
            List<string> list = new List<string>();
            DirRec slus = Model.GetRec("SLUS");
            if (slus != null) {
                foreach (Skill skill in skills) {
                    string str = skill.Name;
                    list.Add(str);
                }
            }
            return list;
        }

        public string GetName(int index) {
            Skill skill = skills.ElementAt(index);
            if (skill != null) {
                return skill.Name;
            }
            return "";
        }

        public int GetIndexByName(string name) {
            int i = 0;
            foreach (Skill skill in skills) {
                if (skill.Name == name) {
                    return i;
                }
                i++;
            }
            return 0;
        }
    }
}
