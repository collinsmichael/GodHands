using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class ItemNameGemList {
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
            { 0x01, 0x0105 }, // Talos Feldspar
            { 0x02, 0x0106 }, // Titan Malachite
            { 0x03, 0x0107 }, // Sylphid Topaz
            { 0x04, 0x0108 }, // Djinn Amber
            { 0x05, 0x0109 }, // Salamander Ruby
            { 0x06, 0x010A }, // Ifrit Carnelian
            { 0x07, 0x010B }, // Gnome Emerald
            { 0x08, 0x010C }, // Dao Moonstone
            { 0x09, 0x010D }, // Undine Jasper
            { 0x0A, 0x010E }, // Marid Aquamarine
            { 0x0B, 0x010F }, // Angel Pearl
            { 0x0C, 0x0110 }, // Seraphim Diamond
            { 0x0D, 0x0111 }, // Morlock Jet
            { 0x0E, 0x0112 }, // Berial Blackpearl
            { 0x0F, 0x0113 }, // Haeralis
            { 0x10, 0x0114 }, // Orlandu
            { 0x11, 0x0115 }, // Orion
            { 0x12, 0x0116 }, // Ogmius
            { 0x13, 0x0117 }, // Iocus
            { 0x14, 0x0118 }, // Balvus
            { 0x15, 0x0119 }, // Trinity
            { 0x16, 0x011A }, // Beowulf
            { 0x17, 0x011B }, // Dragonite
            { 0x18, 0x011C }, // Sigguld
            { 0x19, 0x011D }, // Demonia
            { 0x1A, 0x011E }, // Altema
            { 0x1B, 0x011F }, // Polaris
            { 0x1C, 0x0120 }, // Basivalen
            { 0x1D, 0x0121 }, // Galerian
            { 0x1E, 0x0122 }, // Vedivier
            { 0x1F, 0x0123 }, // Berion
            { 0x20, 0x0124 }, // Gervin
            { 0x21, 0x0125 }, // Tertia
            { 0x22, 0x0126 }, // Lancer
            { 0x23, 0x0127 }, // Arturos
            { 0x24, 0x0128 }, // Braveheart
            { 0x25, 0x0129 }, // Hellraiser
            { 0x26, 0x012A }, // Nightkiller
            { 0x27, 0x012B }, // Manabreaker
            { 0x28, 0x012C }, // Powerfist
            { 0x29, 0x012D }, // Brainshield
            { 0x2A, 0x012E }, // Speedster
            { 0x2B, 0x012F }, // untitled
            { 0x2C, 0x0130 }, // Silent Queen
            { 0x2D, 0x0131 }, // Dark Queen
            { 0x2E, 0x0132 }, // Death Queen
            { 0x2F, 0x0133 }, // White Queen
            { 0x30, 0x0134 }, // untitled
            { 0x31, 0x0135 }, // untitled
            { 0x32, 0x0136 }, // untitled
            { 0x33, 0x0137 }, // untitled
            { 0x34, 0x0138 }, // untitled
            { 0x35, 0x0139 }, // untitled
            { 0x36, 0x013A }, // untitled
            { 0x37, 0x013B }, // untitled
            { 0x38, 0x013C }, // untitled
            { 0x39, 0x013D }, // untitled
            { 0x3A, 0x013E }, // untitled
            { 0x3B, 0x013F }, // untitled
            { 0x3C, 0x0140 }, // untitled
            { 0x3D, 0x0141 }, // untitled
            { 0x3E, 0x0142 }, // untitled
        };

        public Dictionary<int,int> rev = new Dictionary<int,int> {
            { 0x0105, 0x01 }, // Talos Feldspar
            { 0x0106, 0x02 }, // Titan Malachite
            { 0x0107, 0x03 }, // Sylphid Topaz
            { 0x0108, 0x04 }, // Djinn Amber
            { 0x0109, 0x05 }, // Salamander Ruby
            { 0x010A, 0x06 }, // Ifrit Carnelian
            { 0x010B, 0x07 }, // Gnome Emerald
            { 0x010C, 0x08 }, // Dao Moonstone
            { 0x010D, 0x09 }, // Undine Jasper
            { 0x010E, 0x0A }, // Marid Aquamarine
            { 0x010F, 0x0B }, // Angel Pearl
            { 0x0110, 0x0C }, // Seraphim Diamond
            { 0x0111, 0x0D }, // Morlock Jet
            { 0x0112, 0x0E }, // Berial Blackpearl
            { 0x0113, 0x0F }, // Haeralis
            { 0x0114, 0x10 }, // Orlandu
            { 0x0115, 0x11 }, // Orion
            { 0x0116, 0x12 }, // Ogmius
            { 0x0117, 0x13 }, // Iocus
            { 0x0118, 0x14 }, // Balvus
            { 0x0119, 0x15 }, // Trinity
            { 0x011A, 0x16 }, // Beowulf
            { 0x011B, 0x17 }, // Dragonite
            { 0x011C, 0x18 }, // Sigguld
            { 0x011D, 0x19 }, // Demonia
            { 0x011E, 0x1A }, // Altema
            { 0x011F, 0x1B }, // Polaris
            { 0x0120, 0x1C }, // Basivalen
            { 0x0121, 0x1D }, // Galerian
            { 0x0122, 0x1E }, // Vedivier
            { 0x0123, 0x1F }, // Berion
            { 0x0124, 0x20 }, // Gervin
            { 0x0125, 0x21 }, // Tertia
            { 0x0126, 0x22 }, // Lancer
            { 0x0127, 0x23 }, // Arturos
            { 0x0128, 0x24 }, // Braveheart
            { 0x0129, 0x25 }, // Hellraiser
            { 0x012A, 0x26 }, // Nightkiller
            { 0x012B, 0x27 }, // Manabreaker
            { 0x012C, 0x28 }, // Powerfist
            { 0x012D, 0x29 }, // Brainshield
            { 0x012E, 0x2A }, // Speedster
            { 0x012F, 0x2B }, // untitled
            { 0x0130, 0x2C }, // Silent Queen
            { 0x0131, 0x2D }, // Dark Queen
            { 0x0132, 0x2E }, // Death Queen
            { 0x0133, 0x2F }, // White Queen
            { 0x0134, 0x30 }, // untitled
            { 0x0135, 0x31 }, // untitled
            { 0x0136, 0x32 }, // untitled
            { 0x0137, 0x33 }, // untitled
            { 0x0138, 0x34 }, // untitled
            { 0x0139, 0x35 }, // untitled
            { 0x013A, 0x36 }, // untitled
            { 0x013B, 0x37 }, // untitled
            { 0x013C, 0x38 }, // untitled
            { 0x013D, 0x39 }, // untitled
            { 0x013E, 0x3A }, // untitled
            { 0x013F, 0x3B }, // untitled
            { 0x0140, 0x3C }, // untitled
            { 0x0141, 0x3D }, // untitled
            { 0x0142, 0x3E }, // untitled
        };
    }
}
