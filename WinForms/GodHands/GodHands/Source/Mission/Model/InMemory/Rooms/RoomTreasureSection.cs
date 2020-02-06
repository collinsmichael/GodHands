using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public class RoomTreasureSection : InMemory {
        private int len;
        public RoomTreasureSection(string url, int pos, int len, DirRec rec):
        base(url, pos, rec) {
            this.len = len;
        }

        [ReadOnly(true)][Category(" INTERNAL")]
        public int LenSection {
            get { return len; }
            set {}
        }

        public bool OpenSection(TreeNode root) {
            DirRec rec = GetRec();
            int lba = rec.LbaData;
            int pos = GetPos() - lba*2048;

            if (len >= 0x220) {
                string key = GetUrl()+"/Treasure/";

                Weapon = new TreasureWeapon(key+"Weapon", pos+0x000, rec);
                TreeNode node1 = root.Nodes.Add(key+"Weapon", Weapon.GetText(), 0, 0);
                WeaponBlade = new TreasureBlade(key+"Weapon/Blade", pos+0x000, rec);
                node1.Nodes.Add(key+"Weapon/Blade", WeaponBlade.GetText(), 0, 0);
                WeaponGrip = new TreasureGrip(key+"Weapon/Grip", pos+0x030, rec);
                node1.Nodes.Add(key+"Weapon/Grip", WeaponGrip.GetText(), 0, 0);
                WeaponGem1 = new TreasureGem(key+"Weapon/Gem1", pos+0x040, rec);
                node1.Nodes.Add(key+"Weapon/Gem1", WeaponGem1.GetText(), 0, 0);
                WeaponGem2 = new TreasureGem(key+"Weapon/Gem2", pos+0x05C, rec);
                node1.Nodes.Add(key+"Weapon/Gem2", WeaponGem2.GetText(), 0, 0);
                WeaponGem3 = new TreasureGem(key+"Weapon/Gem3", pos+0x078, rec);
                node1.Nodes.Add(key+"Weapon/Gem3", WeaponGem3.GetText(), 0, 0);

                Blade = new TreasureBlade(key+"Blade", pos+0x0AC, rec);
                root.Nodes.Add(key+"Blade", Blade.GetText(), 0, 0);
                Grip = new TreasureGrip(key+"Grip", pos+0x0CC, rec);
                root.Nodes.Add(key+"Grip", Grip.GetText(), 0, 0);

                Shield = new TreasureShield(key+"Shield", pos+0x0DC, rec);
                TreeNode node2 = root.Nodes.Add(key+"Shield", Shield.GetText(), 0, 0);
                ShieldGem1 = new TreasureGem(key+"Shield/Gem1", pos+0x11C, rec);
                node2.Nodes.Add(key+"Shield/Gem1", ShieldGem1.GetText(), 0, 0);
                ShieldGem2 = new TreasureGem(key+"Shield/Gem2", pos+0x138, rec);
                node2.Nodes.Add(key+"Shield/Gem2", ShieldGem2.GetText(), 0, 0);
                ShieldGem3 = new TreasureGem(key+"Shield/Gem3", pos+0x154, rec);
                node2.Nodes.Add(key+"Shield/Gem3", ShieldGem3.GetText(), 0, 0);

                Armour1 = new TreasureArmour(key+"Armour1", pos+0x170, rec);
                root.Nodes.Add(key+"Armour1", Armour1.GetText(), 0, 0);
                Armour2 = new TreasureArmour(key+"Armour2", pos+0x1A0, rec);
                root.Nodes.Add(key+"Armour2", Armour2.GetText(), 0, 0);

                Accessory = new TreasureAccessory(key+"Accessory", pos+0x1D0, rec);
                root.Nodes.Add(key+"Accessory", Accessory.GetText(), 0, 0);
                Gem = new TreasureGem(key+"Gem", pos+0x200, rec);
                root.Nodes.Add(key+"Gem", Gem.GetText(), 0, 0);

                Item1 = new TreasureMiscItem(key+"Item1", pos+0x220, rec);
                root.Nodes.Add(key+"Item1", Item1.GetText(), 0, 0);
                Item2 = new TreasureMiscItem(key+"Item2", pos+0x224, rec);
                root.Nodes.Add(key+"Item2", Item2.GetText(), 0, 0);
                Item3 = new TreasureMiscItem(key+"Item3", pos+0x228, rec);
                root.Nodes.Add(key+"Item3", Item3.GetText(), 0, 0);
                Item4 = new TreasureMiscItem(key+"Item4", pos+0x22C, rec);
                root.Nodes.Add(key+"Item4", Item4.GetText(), 0, 0);
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
        public TreasureMiscItem Item1 { get; set; }
        [ReadOnly(true)][Category(" INTERNAL")]
        public TreasureMiscItem Item2 { get; set; }
        [ReadOnly(true)][Category(" INTERNAL")]
        public TreasureMiscItem Item3 { get; set; }
        [ReadOnly(true)][Category(" INTERNAL")]
        public TreasureMiscItem Item4 { get; set; }
    }
}
