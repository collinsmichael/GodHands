using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class ItemNameMaterialsList {
        public List<string> GetList() {
            List<string> list = new List<string>();
            list.Add("");
            List<string> items = Model.itemnames.GetList();
            if (items != null) {
                string[] array = items.ToArray();
                foreach (int i in fwd.Values) {
                    string str = array[i];
                    list.Add(str);
                }
            }
            return list;
        }

        public string GetName(int index) {
            if (fwd.ContainsKey(index)) {
                List<string> items = Model.itemnames.GetList();
                if (items != null) {
                    string[] array = items.ToArray();
                    int i = fwd[index];
                    return array[i];
                }
            }
            return "";
        }

        public int GetIndexByName(string name) {
            List<string> items = Model.itemnames.GetList();
            if (items != null) {
                string[] array = items.ToArray();
                for (int i = 0; i < array.Length; i++) {
                    if (name == array[i]) {
                        if (rev.ContainsKey(i)) {
                            return rev[i];
                        }
                    }
                }
            }
            return 0;
        }

        public Dictionary<int,int> fwd = new Dictionary<int,int> {
            { 0x01, 0x00FE }, // Wood
            { 0x02, 0x00FF }, // Leather
            { 0x03, 0x0100 }, // Bronze
            { 0x04, 0x0101 }, // Iron
            { 0x05, 0x0102 }, // Hagane
            { 0x06, 0x0103 }, // Silver
            { 0x07, 0x0104 }, // Damascus
        };

        public Dictionary<int,int> rev = new Dictionary<int,int> {
            { 0x00FE, 0x01 }, // Wood
            { 0x00FF, 0x02 }, // Leather
            { 0x0100, 0x03 }, // Bronze
            { 0x0101, 0x04 }, // Iron
            { 0x0102, 0x05 }, // Hagane
            { 0x0103, 0x06 }, // Silver
            { 0x0104, 0x07 }, // Damascus
        };
    }
}
