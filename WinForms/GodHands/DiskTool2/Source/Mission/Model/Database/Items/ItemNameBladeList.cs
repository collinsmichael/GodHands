using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class ItemNameBladeList {
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
            { 0x01, 0x0001 }, // Battle Knife
            { 0x02, 0x0002 }, // Scramasax
            { 0x03, 0x0003 }, // Dirk
            { 0x04, 0x0004 }, // Throwing Knife
            { 0x05, 0x0005 }, // Kudi
            { 0x06, 0x0006 }, // Cinquedea
            { 0x07, 0x0007 }, // Kris
            { 0x08, 0x0008 }, // Hatchet
            { 0x09, 0x0009 }, // Khukuri
            { 0x0A, 0x000A }, // Baselard
            { 0x0B, 0x000B }, // Stiletto
            { 0x0C, 0x000C }, // Jamadhar
            { 0x0D, 0x000D }, // Spatha
            { 0x0E, 0x000E }, // Scimitar
            { 0x0F, 0x000F }, // Rapier
            { 0x10, 0x0010 }, // Short Sword
            { 0x11, 0x0011 }, // Firangi
            { 0x12, 0x0012 }, // Shamshir
            { 0x13, 0x0013 }, // Falchion
            { 0x14, 0x0014 }, // Shotel
            { 0x15, 0x0015 }, // Khora
            { 0x16, 0x0016 }, // Khopesh
            { 0x17, 0x0017 }, // Wakizashi
            { 0x18, 0x0018 }, // Rhomphaia
            { 0x19, 0x0019 }, // Broad Sword
            { 0x1A, 0x001A }, // Norse Sword
            { 0x1B, 0x001B }, // Katana
            { 0x1C, 0x001C }, // Executioner
            { 0x1D, 0x001D }, // Claymore
            { 0x1E, 0x001E }, // Schiavona
            { 0x1F, 0x001F }, // Bastard Sword
            { 0x20, 0x0020 }, // Nodachi
            { 0x21, 0x0021 }, // Rune Blade
            { 0x22, 0x0022 }, // Holy Win
            { 0x23, 0x0023 }, // Hand Axe
            { 0x24, 0x0024 }, // Battle Axe
            { 0x25, 0x0025 }, // Francisca
            { 0x26, 0x0026 }, // Tabarzin
            { 0x27, 0x0027 }, // Chamkaq
            { 0x28, 0x0028 }, // Tabar
            { 0x29, 0x0029 }, // Bullova
            { 0x2A, 0x002A }, // Crescent
            { 0x2B, 0x002B }, // Goblin Club
            { 0x2C, 0x002C }, // Spiked Club
            { 0x2D, 0x002D }, // Ball Mace
            { 0x2E, 0x002E }, // Footman's Mace
            { 0x2F, 0x002F }, // Morning Star
            { 0x30, 0x0030 }, // War Hammer
            { 0x31, 0x0031 }, // Bec de Corbin
            { 0x32, 0x0032 }, // War Maul
            { 0x33, 0x0033 }, // Guisarme
            { 0x34, 0x0034 }, // Large Crescent
            { 0x35, 0x0035 }, // Sabre Halberd
            { 0x36, 0x0036 }, // Balbriggan
            { 0x37, 0x0037 }, // Double Blade
            { 0x38, 0x0038 }, // Halberd
            { 0x39, 0x0039 }, // Wizard Staff
            { 0x3A, 0x003A }, // Clergy Rod
            { 0x3B, 0x003B }, // Summoner Baton
            { 0x3C, 0x003C }, // Shamanic Staff
            { 0x3D, 0x003D }, // Bishop's Crosier
            { 0x3E, 0x003E }, // Sage's Cane
            { 0x3F, 0x003F }, // Langdebeve
            { 0x40, 0x0040 }, // Sabre Mace
            { 0x41, 0x0041 }, // Footman's Mace
            { 0x42, 0x0042 }, // Gloomwing
            { 0x43, 0x0043 }, // Mjolnir
            { 0x44, 0x0044 }, // Griever
            { 0x45, 0x0045 }, // Destroyer
            { 0x46, 0x0046 }, // Hand of Light
            { 0x47, 0x0047 }, // Spear
            { 0x48, 0x0048 }, // Glaive
            { 0x49, 0x0049 }, // Scorpion
            { 0x4A, 0x004A }, // Corcesca
            { 0x4B, 0x004B }, // Trident
            { 0x4C, 0x004C }, // Awl Pike
            { 0x4D, 0x004D }, // Boar Spear
            { 0x4E, 0x004E }, // Fauchard
            { 0x4F, 0x004F }, // Voulge
            { 0x50, 0x0050 }, // Pole Axe
            { 0x51, 0x0051 }, // Bardysh
            { 0x52, 0x0052 }, // Brandestoc
            { 0x53, 0x0053 }, // Gastraph Bow
            { 0x54, 0x0054 }, // Light Crossbow
            { 0x55, 0x0055 }, // Target Bow
            { 0x56, 0x0056 }, // Windlass
            { 0x57, 0x0057 }, // Cranequin
            { 0x58, 0x0058 }, // Lug Crossbow
            { 0x59, 0x0059 }, // Siege Bow
            { 0x5A, 0x005A }, // Arbalest
        };

        public Dictionary<int,int> rev = new Dictionary<int,int> {
            { 0x0001, 0x01 }, // Battle Knife
            { 0x0002, 0x02 }, // Scramasax
            { 0x0003, 0x03 }, // Dirk
            { 0x0004, 0x04 }, // Throwing Knife
            { 0x0005, 0x05 }, // Kudi
            { 0x0006, 0x06 }, // Cinquedea
            { 0x0007, 0x07 }, // Kris
            { 0x0008, 0x08 }, // Hatchet
            { 0x0009, 0x09 }, // Khukuri
            { 0x000A, 0x0A }, // Baselard
            { 0x000B, 0x0B }, // Stiletto
            { 0x000C, 0x0C }, // Jamadhar
            { 0x000D, 0x0D }, // Spatha
            { 0x000E, 0x0E }, // Scimitar
            { 0x000F, 0x0F }, // Rapier
            { 0x0010, 0x10 }, // Short Sword
            { 0x0011, 0x11 }, // Firangi
            { 0x0012, 0x12 }, // Shamshir
            { 0x0013, 0x13 }, // Falchion
            { 0x0014, 0x14 }, // Shotel
            { 0x0015, 0x15 }, // Khora
            { 0x0016, 0x16 }, // Khopesh
            { 0x0017, 0x17 }, // Wakizashi
            { 0x0018, 0x18 }, // Rhomphaia
            { 0x0019, 0x19 }, // Broad Sword
            { 0x001A, 0x1A }, // Norse Sword
            { 0x001B, 0x1B }, // Katana
            { 0x001C, 0x1C }, // Executioner
            { 0x001D, 0x1D }, // Claymore
            { 0x001E, 0x1E }, // Schiavona
            { 0x001F, 0x1F }, // Bastard Sword
            { 0x0020, 0x20 }, // Nodachi
            { 0x0021, 0x21 }, // Rune Blade
            { 0x0022, 0x22 }, // Holy Win
            { 0x0023, 0x23 }, // Hand Axe
            { 0x0024, 0x24 }, // Battle Axe
            { 0x0025, 0x25 }, // Francisca
            { 0x0026, 0x26 }, // Tabarzin
            { 0x0027, 0x27 }, // Chamkaq
            { 0x0028, 0x28 }, // Tabar
            { 0x0029, 0x29 }, // Bullova
            { 0x002A, 0x2A }, // Crescent
            { 0x002B, 0x2B }, // Goblin Club
            { 0x002C, 0x2C }, // Spiked Club
            { 0x002D, 0x2D }, // Ball Mace
            { 0x002E, 0x2E }, // Footman's Mace
            { 0x002F, 0x2F }, // Morning Star
            { 0x0030, 0x30 }, // War Hammer
            { 0x0031, 0x31 }, // Bec de Corbin
            { 0x0032, 0x32 }, // War Maul
            { 0x0033, 0x33 }, // Guisarme
            { 0x0034, 0x34 }, // Large Crescent
            { 0x0035, 0x35 }, // Sabre Halberd
            { 0x0036, 0x36 }, // Balbriggan
            { 0x0037, 0x37 }, // Double Blade
            { 0x0038, 0x38 }, // Halberd
            { 0x0039, 0x39 }, // Wizard Staff
            { 0x003A, 0x3A }, // Clergy Rod
            { 0x003B, 0x3B }, // Summoner Baton
            { 0x003C, 0x3C }, // Shamanic Staff
            { 0x003D, 0x3D }, // Bishop's Crosier
            { 0x003E, 0x3E }, // Sage's Cane
            { 0x003F, 0x3F }, // Langdebeve
            { 0x0040, 0x40 }, // Sabre Mace
            { 0x0041, 0x41 }, // Footman's Mace
            { 0x0042, 0x42 }, // Gloomwing
            { 0x0043, 0x43 }, // Mjolnir
            { 0x0044, 0x44 }, // Griever
            { 0x0045, 0x45 }, // Destroyer
            { 0x0046, 0x46 }, // Hand of Light
            { 0x0047, 0x47 }, // Spear
            { 0x0048, 0x48 }, // Glaive
            { 0x0049, 0x49 }, // Scorpion
            { 0x004A, 0x4A }, // Corcesca
            { 0x004B, 0x4B }, // Trident
            { 0x004C, 0x4C }, // Awl Pike
            { 0x004D, 0x4D }, // Boar Spear
            { 0x004E, 0x4E }, // Fauchard
            { 0x004F, 0x4F }, // Voulge
            { 0x0050, 0x50 }, // Pole Axe
            { 0x0051, 0x51 }, // Bardysh
            { 0x0052, 0x52 }, // Brandestoc
            { 0x0053, 0x53 }, // Gastraph Bow
            { 0x0054, 0x54 }, // Light Crossbow
            { 0x0055, 0x55 }, // Target Bow
            { 0x0056, 0x56 }, // Windlass
            { 0x0057, 0x57 }, // Cranequin
            { 0x0058, 0x58 }, // Lug Crossbow
            { 0x0059, 0x59 }, // Siege Bow
            { 0x005A, 0x5A }, // Arbalest
        };
    }
}
