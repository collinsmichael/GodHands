using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GodHands {
    public class RoomEnemySection : InMemory {
        private int len;
        public RoomEnemySection(string url, int pos, int len, DirRec rec):
        base(url, pos, rec) {
            this.len = len;
            Enemies = new List<RoomEnemy>();
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
            int count = len/0x28;
            for (int i = 0; i < count; i++) {
                string key = GetUrl()+"/Enemy_"+i;
                RoomEnemy enemy = new RoomEnemy(key, pos+i*0x28, rec);
                root.Nodes.Add(key, "Enemy_"+i, 35, 35);
                Enemies.Add(enemy);
            }
            return true;
        }

        [ReadOnly(true)][Category(" INTERNAL")]
        public List<RoomEnemy> Enemies { get; set; }

        [ReadOnly(true)][Category(" INTERNAL")]
        public int NumEnemies {
            get { return Enemies.Count; }
            set {}
        }

    }
}
