using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class ItemNameAccessoryList {
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
            { 0x61, 0x00DF }, // Rood Necklace
            { 0x62, 0x00E0 }, // Rune Earrings
            { 0x63, 0x00E1 }, // Lionhead
            { 0x64, 0x00E2 }, // Rusted Nails
            { 0x65, 0x00E3 }, // Sylphid Ring
            { 0x66, 0x00E4 }, // Marduk
            { 0x67, 0x00E5 }, // Salamander Ring
            { 0x68, 0x00E6 }, // Tamulis Tongue
            { 0x69, 0x00E7 }, // Gnome Bracelet
            { 0x6A, 0x00E8 }, // Palolo's Ring
            { 0x6B, 0x00E9 }, // Undine Bracelet
            { 0x6C, 0x00EA }, // Talian Ring
            { 0x6D, 0x00EB }, // Agrias's Balm
            { 0x6E, 0x00EC }, // Kadesh Ring
            { 0x6F, 0x00ED }, // Agrippa's Choker
            { 0x70, 0x00EE }, // Diadra's Earring
            { 0x71, 0x00EF }, // Titan's Ring
            { 0x72, 0x00F0 }, // Lau Fei's Armlet
            { 0x73, 0x00F1 }, // Swan Song
            { 0x74, 0x00F2 }, // Pushpaka
            { 0x75, 0x00F3 }, // Edgar's Earrings
            { 0x76, 0x00F4 }, // Cross Choker
            { 0x77, 0x00F5 }, // Ghost Hound
            { 0x78, 0x00F6 }, // Beaded Anklet
            { 0x79, 0x00F7 }, // Dragonhead
            { 0x7A, 0x00F8 }, // Faufnir's Tear
            { 0x7B, 0x00F9 }, // Agales's Chain
            { 0x7C, 0x00FA }, // Balam Ring
            { 0x7D, 0x00FB }, // Nimje Coif
            { 0x7E, 0x00FC }, // Morgan's Nails
            { 0x7F, 0x00FD }, // Marlene's Ring
        };

        public Dictionary<int,int> rev = new Dictionary<int,int> {
            { 0x00DF, 0x61 }, // Rood Necklace
            { 0x00E0, 0x62 }, // Rune Earrings
            { 0x00E1, 0x63 }, // Lionhead
            { 0x00E2, 0x64 }, // Rusted Nails
            { 0x00E3, 0x65 }, // Sylphid Ring
            { 0x00E4, 0x66 }, // Marduk
            { 0x00E5, 0x67 }, // Salamander Ring
            { 0x00E6, 0x68 }, // Tamulis Tongue
            { 0x00E7, 0x69 }, // Gnome Bracelet
            { 0x00E8, 0x6A }, // Palolo's Ring
            { 0x00E9, 0x6B }, // Undine Bracelet
            { 0x00EA, 0x6C }, // Talian Ring
            { 0x00EB, 0x6D }, // Agrias's Balm
            { 0x00EC, 0x6E }, // Kadesh Ring
            { 0x00ED, 0x6F }, // Agrippa's Choker
            { 0x00EE, 0x70 }, // Diadra's Earring
            { 0x00EF, 0x71 }, // Titan's Ring
            { 0x00F0, 0x72 }, // Lau Fei's Armlet
            { 0x00F1, 0x73 }, // Swan Song
            { 0x00F2, 0x74 }, // Pushpaka
            { 0x00F3, 0x75 }, // Edgar's Earrings
            { 0x00F4, 0x76 }, // Cross Choker
            { 0x00F5, 0x77 }, // Ghost Hound
            { 0x00F6, 0x78 }, // Beaded Anklet
            { 0x00F7, 0x79 }, // Dragonhead
            { 0x00F8, 0x7A }, // Faufnir's Tear
            { 0x00F9, 0x7B }, // Agales's Chain
            { 0x00FA, 0x7C }, // Balam Ring
            { 0x00FB, 0x7D }, // Nimje Coif
            { 0x00FC, 0x7E }, // Morgan's Nails
            { 0x00FD, 0x7F }, // Marlene's Ring
        };
    }
}
