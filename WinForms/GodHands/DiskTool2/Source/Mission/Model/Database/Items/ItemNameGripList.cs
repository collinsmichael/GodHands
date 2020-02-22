using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class ItemNameGripList {
        public List<string> GetList() {
            List<string> list = new List<string>();
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
            { 0x01, 0x0060 }, // Short Hilt
            { 0x02, 0x0061 }, // Swept Hilt
            { 0x03, 0x0062 }, // Cross Guard
            { 0x04, 0x0063 }, // Knuckle Guard
            { 0x05, 0x0064 }, // Counter Guard
            { 0x06, 0x0065 }, // Side Ring
            { 0x07, 0x0066 }, // Power Palm
            { 0x08, 0x0067 }, // Murderer's Hilt
            { 0x09, 0x0068 }, // Spiral Hilt
            { 0x0A, 0x0069 }, // Wooden Grip
            { 0x0B, 0x006A }, // Sand Face
            { 0x0C, 0x006B }, // Czekan Type
            { 0x0D, 0x006C }, // Sarissa Grip
            { 0x0E, 0x006D }, // Gendarme
            { 0x0F, 0x006E }, // Heavy Grip
            { 0x10, 0x006F }, // Runkasyle
            { 0x11, 0x0070 }, // Bhuj Type
            { 0x12, 0x0071 }, // Grimoire Grip
            { 0x13, 0x0072 }, // Elephant
            { 0x14, 0x0073 }, // Wooden Pole
            { 0x15, 0x0074 }, // Spiculum Pole
            { 0x16, 0x0075 }, // Winged Pole
            { 0x17, 0x0076 }, // Framea Pole
            { 0x18, 0x0077 }, // Ahlspies
            { 0x19, 0x0078 }, // Spiral Pole
            { 0x1A, 0x0079 }, // Simple Bolt
            { 0x1B, 0x007A }, // Steel Bolt
            { 0x1C, 0x007B }, // Javelin Bolt
            { 0x1D, 0x007C }, // Falarica Bolt
            { 0x1E, 0x007D }, // Stone Bullet
            { 0x1F, 0x007E }, // Sonic Bullet
        };

        public Dictionary<int,int> rev = new Dictionary<int,int> {
            { 0x0060, 0x01 }, // Short Hilt
            { 0x0061, 0x02 }, // Swept Hilt
            { 0x0062, 0x03 }, // Cross Guard
            { 0x0063, 0x04 }, // Knuckle Guard
            { 0x0064, 0x05 }, // Counter Guard
            { 0x0065, 0x06 }, // Side Ring
            { 0x0066, 0x07 }, // Power Palm
            { 0x0067, 0x08 }, // Murderer's Hilt
            { 0x0068, 0x09 }, // Spiral Hilt
            { 0x0069, 0x0A }, // Wooden Grip
            { 0x006A, 0x0B }, // Sand Face
            { 0x006B, 0x0C }, // Czekan Type
            { 0x006C, 0x0D }, // Sarissa Grip
            { 0x006D, 0x0E }, // Gendarme
            { 0x006E, 0x0F }, // Heavy Grip
            { 0x006F, 0x10 }, // Runkasyle
            { 0x0070, 0x11 }, // Bhuj Type
            { 0x0071, 0x12 }, // Grimoire Grip
            { 0x0072, 0x13 }, // Elephant
            { 0x0073, 0x14 }, // Wooden Pole
            { 0x0074, 0x15 }, // Spiculum Pole
            { 0x0075, 0x16 }, // Winged Pole
            { 0x0076, 0x17 }, // Framea Pole
            { 0x0077, 0x18 }, // Ahlspies
            { 0x0078, 0x19 }, // Spiral Pole
            { 0x0079, 0x1A }, // Simple Bolt
            { 0x007A, 0x1B }, // Steel Bolt
            { 0x007B, 0x1C }, // Javelin Bolt
            { 0x007C, 0x1D }, // Falarica Bolt
            { 0x007D, 0x1E }, // Stone Bullet
            { 0x007E, 0x1F }, // Sonic Bullet
        };
    }
}
