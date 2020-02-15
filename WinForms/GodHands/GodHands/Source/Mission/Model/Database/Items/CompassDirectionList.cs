using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public class CompassDirectionList {
        private List<string> items = new List<string> {
            "South", "West", "North", "East"
        };

        public bool Load() {
            items = new List<string> {
                "South", "West", "North", "East"
            };
            return true;
        }

        public bool Clear() {
            items.Clear();
            return true;
        }

        public List<string> GetList() {
            return items;
        }

        public string GetName(int index) {
            if ((index >= 0) && (index < items.Count)) {
                return items[index];
            }
            return "";
        }

        public int GetIndexByName(string name) {
            for (int i = 0; i < items.Count; i++) {
                if (items[i] == name) {
                    return i;
                }
            }
            return 0;
        }
    }
}
