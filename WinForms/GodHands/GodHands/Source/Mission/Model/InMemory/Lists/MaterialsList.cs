using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class MaterialsList {
        private List<string> list = new List<string> {
            "", "Wood", "Leather", "Bronze",
            "Iron", "Hagane", "Silver", "Damascus",
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
