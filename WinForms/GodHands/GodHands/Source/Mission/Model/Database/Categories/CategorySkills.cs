using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class CategorySkills {
        private List<string> list = new List<string> {
            "Nothing",
            "Spell (costs MP)",
            "Battle Ability (costs RISK)",
            "Break Art (costs HP)",
            "Unused (costs PP)",
            "Item Effect (costs nothing)",
            "Normal Effect (costs depends on weapon)",
            "Trap"
        };

        public List<string> GetList() {
            return list;
        }

        public string GetName(int index) {
            string name = list.ElementAt(index);
            if (name != null) {
                return name;
            }
            return "";
        }

        public int GetIndexByName(string name) {
            int i = 0;
            foreach (string item in list) {
                if (item == name) {
                    return i;
                }
                i++;
            }
            return 0;
        }
    }
}
