using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class ItemNameArmourList {
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
            { 0x01, 0x007F }, // Buckler
            { 0x02, 0x0080 }, // Pelta Shield
            { 0x03, 0x0081 }, // Targe
            { 0x04, 0x0082 }, // Quad Shield
            { 0x05, 0x0083 }, // Circle Shield
            { 0x06, 0x0084 }, // Tower Shield
            { 0x07, 0x0085 }, // Spiked Shield
            { 0x08, 0x0086 }, // Round Shield
            { 0x09, 0x0087 }, // Kite Shield
            { 0x0A, 0x0088 }, // Casserole Shield
            { 0x0B, 0x0089 }, // Heater Shield
            { 0x0C, 0x008A }, // Oval Shield
            { 0x0D, 0x008B }, // Knight Shield
            { 0x0E, 0x008C }, // Hoplite Shield
            { 0x0F, 0x008D }, // Jazeraint Shield
            { 0x10, 0x008E }, // Dread Shield
            { 0x11, 0x008F }, // Bandana
            { 0x12, 0x0090 }, // Bear Mask
            { 0x13, 0x0091 }, // Wizard Hat
            { 0x14, 0x0092 }, // Bone Helm
            { 0x15, 0x0093 }, // Chain Coif
            { 0x16, 0x0094 }, // Spangenhelm
            { 0x17, 0x0095 }, // Cabasset
            { 0x18, 0x0096 }, // Sallet
            { 0x19, 0x0097 }, // Barbut
            { 0x1A, 0x0098 }, // Basinet
            { 0x1B, 0x0099 }, // Armet
            { 0x1C, 0x009A }, // Close Helm
            { 0x1D, 0x009B }, // Burgonet
            { 0x1E, 0x009C }, // Hoplite Helm
            { 0x1F, 0x009D }, // Jazeraint Helm
            { 0x20, 0x009E }, // Dread Helm
            { 0x21, 0x009F }, // Jerkin
            { 0x22, 0x00A0 }, // Hauberk
            { 0x23, 0x00A1 }, // Wizard Robe
            { 0x24, 0x00A2 }, // Cuirass
            { 0x25, 0x00A3 }, // Banded Mail
            { 0x26, 0x00A4 }, // Ring Mail
            { 0x27, 0x00A5 }, // Chain Mail
            { 0x28, 0x00A6 }, // Breastplate
            { 0x29, 0x00A7 }, // Segmentata
            { 0x2A, 0x00A8 }, // Scale Armor
            { 0x2B, 0x00A9 }, // Brigandine
            { 0x2C, 0x00AA }, // Plate Mail
            { 0x2D, 0x00AB }, // Fluted Armor
            { 0x2E, 0x00AC }, // Hoplite Armor
            { 0x2F, 0x00AD }, // Jazeraint Armor
            { 0x30, 0x00AE }, // Dread Armor
            { 0x31, 0x00AF }, // Sandals
            { 0x32, 0x00B0 }, // Boots
            { 0x33, 0x00B1 }, // Long Boots
            { 0x34, 0x00B2 }, // Cuisse
            { 0x35, 0x00B3 }, // Light Greave
            { 0x36, 0x00B4 }, // Ring Leggings
            { 0x37, 0x00B5 }, // Chain Leggings
            { 0x38, 0x00B6 }, // Fusskampf
            { 0x39, 0x00B7 }, // Poleyn
            { 0x3A, 0x00B8 }, // Jambeau
            { 0x3B, 0x00B9 }, // Missaglia
            { 0x3C, 0x00BA }, // Plate Leggings
            { 0x3D, 0x00BB }, // Fluted Leggings
            { 0x3E, 0x00BC }, // Hoplite Leggings
            { 0x3F, 0x00BD }, // Jazeraint Leggings
            { 0x40, 0x00BE }, // Dread Leggings
            { 0x41, 0x00BF }, // Bandage
            { 0x42, 0x00C0 }, // Leather Glove
            { 0x43, 0x00C1 }, // Reinforced Glove
            { 0x44, 0x00C2 }, // Knuckles
            { 0x45, 0x00C3 }, // Ring Sleeve
            { 0x46, 0x00C4 }, // Chain Sleeve
            { 0x47, 0x00C5 }, // Gauntlet
            { 0x48, 0x00C6 }, // Vambrace
            { 0x49, 0x00C7 }, // Plate Glove
            { 0x4A, 0x00C8 }, // Rondanche
            { 0x4B, 0x00C9 }, // Tilt Glove
            { 0x4C, 0x00CA }, // Freiturnier
            { 0x4D, 0x00CB }, // Fluted Glove
            { 0x4E, 0x00CC }, // Hoplite Glove
            { 0x4F, 0x00CD }, // Jazeraint Glove
            { 0x50, 0x00CE }, // Dread Glove
        };

        public Dictionary<int,int> rev = new Dictionary<int,int> {
            { 0x007F, 0x01 }, // Buckler
            { 0x0080, 0x02 }, // Pelta Shield
            { 0x0081, 0x03 }, // Targe
            { 0x0082, 0x04 }, // Quad Shield
            { 0x0083, 0x05 }, // Circle Shield
            { 0x0084, 0x06 }, // Tower Shield
            { 0x0085, 0x07 }, // Spiked Shield
            { 0x0086, 0x08 }, // Round Shield
            { 0x0087, 0x09 }, // Kite Shield
            { 0x0088, 0x0A }, // Casserole Shield
            { 0x0089, 0x0B }, // Heater Shield
            { 0x008A, 0x0C }, // Oval Shield
            { 0x008B, 0x0D }, // Knight Shield
            { 0x008C, 0x0E }, // Hoplite Shield
            { 0x008D, 0x0F }, // Jazeraint Shield
            { 0x008E, 0x10 }, // Dread Shield
            { 0x008F, 0x11 }, // Bandana
            { 0x0090, 0x12 }, // Bear Mask
            { 0x0091, 0x13 }, // Wizard Hat
            { 0x0092, 0x14 }, // Bone Helm
            { 0x0093, 0x15 }, // Chain Coif
            { 0x0094, 0x16 }, // Spangenhelm
            { 0x0095, 0x17 }, // Cabasset
            { 0x0096, 0x18 }, // Sallet
            { 0x0097, 0x19 }, // Barbut
            { 0x0098, 0x1A }, // Basinet
            { 0x0099, 0x1B }, // Armet
            { 0x009A, 0x1C }, // Close Helm
            { 0x009B, 0x1D }, // Burgonet
            { 0x009C, 0x1E }, // Hoplite Helm
            { 0x009D, 0x1F }, // Jazeraint Helm
            { 0x009E, 0x20 }, // Dread Helm
            { 0x009F, 0x21 }, // Jerkin
            { 0x00A0, 0x22 }, // Hauberk
            { 0x00A1, 0x23 }, // Wizard Robe
            { 0x00A2, 0x24 }, // Cuirass
            { 0x00A3, 0x25 }, // Banded Mail
            { 0x00A4, 0x26 }, // Ring Mail
            { 0x00A5, 0x27 }, // Chain Mail
            { 0x00A6, 0x28 }, // Breastplate
            { 0x00A7, 0x29 }, // Segmentata
            { 0x00A8, 0x2A }, // Scale Armor
            { 0x00A9, 0x2B }, // Brigandine
            { 0x00AA, 0x2C }, // Plate Mail
            { 0x00AB, 0x2D }, // Fluted Armor
            { 0x00AC, 0x2E }, // Hoplite Armor
            { 0x00AD, 0x2F }, // Jazeraint Armor
            { 0x00AE, 0x30 }, // Dread Armor
            { 0x00AF, 0x31 }, // Sandals
            { 0x00B0, 0x32 }, // Boots
            { 0x00B1, 0x33 }, // Long Boots
            { 0x00B2, 0x34 }, // Cuisse
            { 0x00B3, 0x35 }, // Light Greave
            { 0x00B4, 0x36 }, // Ring Leggings
            { 0x00B5, 0x37 }, // Chain Leggings
            { 0x00B6, 0x38 }, // Fusskampf
            { 0x00B7, 0x39 }, // Poleyn
            { 0x00B8, 0x3A }, // Jambeau
            { 0x00B9, 0x3B }, // Missaglia
            { 0x00BA, 0x3C }, // Plate Leggings
            { 0x00BB, 0x3D }, // Fluted Leggings
            { 0x00BC, 0x3E }, // Hoplite Leggings
            { 0x00BD, 0x3F }, // Jazeraint Leggings
            { 0x00BE, 0x40 }, // Dread Leggings
            { 0x00BF, 0x41 }, // Bandage
            { 0x00C0, 0x42 }, // Leather Glove
            { 0x00C1, 0x43 }, // Reinforced Glove
            { 0x00C2, 0x44 }, // Knuckles
            { 0x00C3, 0x45 }, // Ring Sleeve
            { 0x00C4, 0x46 }, // Chain Sleeve
            { 0x00C5, 0x47 }, // Gauntlet
            { 0x00C6, 0x48 }, // Vambrace
            { 0x00C7, 0x49 }, // Plate Glove
            { 0x00C8, 0x4A }, // Rondanche
            { 0x00C9, 0x4B }, // Tilt Glove
            { 0x00CA, 0x4C }, // Freiturnier
            { 0x00CB, 0x4D }, // Fluted Glove
            { 0x00CC, 0x4E }, // Hoplite Glove
            { 0x00CD, 0x4F }, // Jazeraint Glove
            { 0x00CE, 0x50 }, // Dread Glove
        };
    }
}
