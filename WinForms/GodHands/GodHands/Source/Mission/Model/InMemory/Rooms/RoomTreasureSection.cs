using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public class RoomTreasureSection : InMemory {
        private int len;
        public RoomTreasureSection(BaseClass parent, string url, int pos, int len, Record rec):
        base(parent, url, pos, rec) {
            this.len = len;
        }

        [ReadOnly(true)][Category(" INTERNAL")]
        public int LenSection {
            get { return len; }
            set {}
        }

        public bool OpenSection(TreeNode root) {
            Record rec = GetRec();
            int lba = rec.LbaData;
            int pos = GetPos() - lba*2048;

            if (len >= 0x220) {
                string key = GetUrl()+"/Treasure/";

                Weapon = new TreasureWeapon(this, key+"Weapon", pos+0x000, rec);
                WeaponBlade = new TreasureBlade(this, key+"Weapon/Blade", pos+0x004, rec, true);
                WeaponGrip = new TreasureGrip(this, key+"Weapon/Grip", pos+0x030, rec, true);
                WeaponGem1 = new TreasureGem(this, key+"Weapon/Gem1", pos+0x040, rec, true);
                WeaponGem2 = new TreasureGem(this, key+"Weapon/Gem2", pos+0x05C, rec, true);
                WeaponGem3 = new TreasureGem(this, key+"Weapon/Gem3", pos+0x078, rec, true);

                Blade = new TreasureBlade(this, key+"Blade", pos+0x0B0, rec, false);
                Grip = new TreasureGrip(this, key+"Grip", pos+0x0DC, rec, false);

                Shield = new TreasureShield(this, key+"Shield", pos+0x0F0, rec);
                ShieldGem1 = new TreasureGem(this, key+"Shield/Gem1", pos+0x11C, rec, true);
                ShieldGem2 = new TreasureGem(this, key+"Shield/Gem2", pos+0x138, rec, true);
                ShieldGem3 = new TreasureGem(this, key+"Shield/Gem3", pos+0x154, rec, true);

                Armour1 = new TreasureArmour(this, key+"Armour1", pos+0x170, rec);
                Armour2 = new TreasureArmour(this, key+"Armour2", pos+0x19C, rec);
                Accessory = new TreasureAccessory(this, key+"Accessory", pos+0x1C8, rec);
                Gem = new TreasureGem(this, key+"Gem", pos+0x1F8, rec, false);

                TreeNode tv_weapon = root.Nodes.Add(key+"Weapon", Weapon.GetText(), 12, 12);
                tv_weapon.Nodes.Add(key+"Weapon/Blade", WeaponBlade.GetText(), 13, 13);
                tv_weapon.Nodes.Add(key+"Weapon/Grip", WeaponGrip.GetText(), 14, 14);
                tv_weapon.Nodes.Add(key+"Weapon/Gem1", WeaponGem1.GetText(), 24, 24);
                tv_weapon.Nodes.Add(key+"Weapon/Gem2", WeaponGem2.GetText(), 25, 25);
                tv_weapon.Nodes.Add(key+"Weapon/Gem3", WeaponGem3.GetText(), 26, 26);

                root.Nodes.Add(key+"Blade", Blade.GetText(), 13, 13);
                root.Nodes.Add(key+"Grip", Grip.GetText(), 14, 14);

                TreeNode node2 = root.Nodes.Add(key+"Shield", Shield.GetText(), 11, 11);
                node2.Nodes.Add(key+"Shield/Gem1", ShieldGem1.GetText(), 24, 24);
                node2.Nodes.Add(key+"Shield/Gem2", ShieldGem2.GetText(), 25, 25);
                node2.Nodes.Add(key+"Shield/Gem3", ShieldGem3.GetText(), 26, 26);

                root.Nodes.Add(key+"Armour1", Armour1.GetText(), 7, 7);
                root.Nodes.Add(key+"Armour2", Armour2.GetText(), 7, 7);
                root.Nodes.Add(key+"Accessory", Accessory.GetText(), 10, 10);
                root.Nodes.Add(key+"Gem", Gem.GetText(), 24, 24);

                Items = new List<TreasureMiscItem>();
                for (int ptr = 0x214; ptr < this.len; ptr += 4) {
                    string k = key+"Item"+Items.Count;
                    TreasureMiscItem item = new TreasureMiscItem(this, k, pos+ptr, rec);
                    root.Nodes.Add(k, item.GetText(), 44, 44);
                    Items.Add(item);
                }
            }
            return true;
        }

        [ReadOnly(true)][Category(" INTERNAL")]
        public TreasureWeapon Weapon { get; set; }
        [ReadOnly(true)][Category(" INTERNAL")]
        public TreasureBlade WeaponBlade { get; set; }
        [ReadOnly(true)][Category(" INTERNAL")]
        public TreasureGrip WeaponGrip { get; set; }
        [ReadOnly(true)][Category(" INTERNAL")]
        public TreasureGem WeaponGem1 { get; set; }
        [ReadOnly(true)][Category(" INTERNAL")]
        public TreasureGem WeaponGem2 { get; set; }
        [ReadOnly(true)][Category(" INTERNAL")]
        public TreasureGem WeaponGem3 { get; set; }
        [ReadOnly(true)][Category(" INTERNAL")]
        public TreasureBlade Blade { get; set; }
        [ReadOnly(true)][Category(" INTERNAL")]
        public TreasureGrip Grip { get; set; }
        [ReadOnly(true)][Category(" INTERNAL")]

        public TreasureShield Shield { get; set; }
        [ReadOnly(true)][Category(" INTERNAL")]
        public TreasureGem ShieldGem1 { get; set; }
        [ReadOnly(true)][Category(" INTERNAL")]
        public TreasureGem ShieldGem2 { get; set; }
        [ReadOnly(true)][Category(" INTERNAL")]
        public TreasureGem ShieldGem3 { get; set; }
        [ReadOnly(true)][Category(" INTERNAL")]
        public TreasureArmour Armour1 { get; set; }
        [ReadOnly(true)][Category(" INTERNAL")]
        public TreasureArmour Armour2 { get; set; }
        [ReadOnly(true)][Category(" INTERNAL")]
        public TreasureAccessory Accessory { get; set; }
        [ReadOnly(true)][Category(" INTERNAL")]
        public TreasureGem Gem { get; set; }

        [ReadOnly(true)][Category(" INTERNAL")]
        public List<TreasureMiscItem> Items { get; set; }
    }
}
